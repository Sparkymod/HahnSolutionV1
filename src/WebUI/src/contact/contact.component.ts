import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { Contact } from '../models/contact.model';
import { ContactService } from '../services/contact.service';

import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {
  contactList: Array<Contact> = [];
  contactId: number = 0;
  removeResults: string = "";
  count: number = 0;
  contactForm: FormGroup;
  isUpdating: boolean = false;

  constructor(private contactService: ContactService) {
    this.contactForm = new FormGroup({
      id: new FormControl(0),
      firstName: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(70),
      ]),
      lastName: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(70),
      ]),
      email: new FormControl('', [
        Validators.required,
        Validators.email,
        Validators.maxLength(40),
      ]),
      phoneNumber: new FormControl(''),
      address: new FormControl(''),
      city: new FormControl(''),
      state: new FormControl(''),
      zipCode: new FormControl(''),
      country: new FormControl(''),
      notes: new FormControl('')
    });
  }

  ngOnInit() {
    this.contactService.getAllContacts().subscribe(result => {
      this.contactList = result;
      this.count = this.contactList.length;
    });
  }

  // Get all the contacts
  onGetAll() {
    this.contactService.getAllContacts().subscribe(result => {
      this.contactList = result;
      this.count = this.contactList.length;
    });
  }

  // Search function
  onGetByIdClick(id: number) {

    // Return all if the number pass is 0
    if (id === 0) {
      this.onGetAll();
      return;
    }
    this.contactService.getContactById(id).pipe(
      catchError((errorResponse: HttpErrorResponse) => {
        let errorMessage: string;
        if (errorResponse.status === 400 || errorResponse.status === 404) {
          errorMessage = errorResponse.error || "Unknown error";
        }
        else {
          errorMessage = "Unknown error";
        }

        console.error("Error: " + errorMessage);
        return of(null);
      })).subscribe(result => {
        if (result) {
          this.contactList = [];
          this.contactList.push(result);
        }
      });
  }

  // Add function
  onAddClick() {
    if (!this.contactForm.valid) {
      console.log('Form is not valid');
      return;
    }

    const formData = this.contactForm.value;
    const contact = new Contact(formData);
    this.count = this.count + 1;
    contact.id = this.count;
    this.isUpdating = false;

    this.contactService.addContact(contact).subscribe(result => {
      if (result) {
        this.contactList.push(result);
        this.onGetAll();
      }
    });
  }

  // Update function
  onUpdateClick() {

    // Form validation
    if (!this.contactForm.valid) {
      console.log('Form is not valid');
      return;
    }
    const formData = this.contactForm.value;
    const contact = new Contact(formData);

    this.contactService.updateContact(contact).subscribe(result => {
      if (result) {
        this.onGetAll();
      }
    });
  }

  // function to select the contact you want to update
  onSelectContactFromTable(contact: Contact) {
    this.isUpdating = true;

    this.contactForm = new FormGroup({
      id: new FormControl(contact.id),
      firstName: new FormControl(contact.firstName ?? ''),
      lastName: new FormControl(contact.lastName ?? ''),
      email: new FormControl(contact.email ?? ''),
      phoneNumber: new FormControl(contact.phoneNumber ?? ''),
      address: new FormControl(contact.address ?? ''),
      city: new FormControl(contact.city ?? ''),
      state: new FormControl(contact.state ?? ''),
      zipCode: new FormControl(contact.zipCode ?? ''),
      country: new FormControl(contact.country ?? ''),
      notes: new FormControl(contact.notes ?? ''),
    });
  }

  // Clear the form
  onClearForm() {
    this.isUpdating = false;
    this.contactForm = new FormGroup({
      id: new FormControl(0),
      firstName: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(70),
      ]),
      lastName: new FormControl('', [
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(70),
      ]),
      email: new FormControl('', [
        Validators.required,
        Validators.email,
        Validators.maxLength(40),
      ]),
      phoneNumber: new FormControl(''),
      address: new FormControl(''),
      city: new FormControl(''),
      state: new FormControl(''),
      zipCode: new FormControl(''),
      country: new FormControl(''),
      notes: new FormControl('')
    });
  }

  // Remove function
  onRemoveClick(id: number) {
    this.contactService.removeContactById(id).subscribe(result => {
      if (result) {
        this.onGetAll();
        this.onClearForm();
      }
    })
  }

  // add id on change in input
  change(event: any) {
    this.contactId = event.target.value;
  }
}
