import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

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

  constructor(private contactService: ContactService, private fb: FormBuilder) {
    this.contactForm = this.fb.group({
      id: [0],
      firstName: [''],
      lastName: [''],
      email: [''],
      phoneNumber: [''],
      address: [''],
      city: [''],
      state: [''],
      zipCode: [''],
      country: [''],
      notes: [''],
    });
  }

  ngOnInit() {
    this.contactService.getAllContacts().subscribe(result => {
      this.contactList = result;
      this.count = this.contactList.length;
    });
  }

  // Search function
  onGetByIdClick(id: number) {
    this.contactService.getContactById(id).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage: string;
        if (error.status === 400 || error.status === 404) {
          errorMessage = error.error || "Unknown error";
        } else {
          errorMessage = "Unknown error";
        }

        console.error("Error: " + errorMessage);
        return of(null);
      })
    ).subscribe(result => {
      if (result) {
        this.contactList = [];
        this.contactList.push(result);
      }
    });
  }

  // Add function
  onAddClick() {
    const formData = this.contactForm.value;
    const contact = new Contact(formData);
    this.count = this.count + 1;
    contact.id = this.count;
    this.isUpdating = false;

    if (!this.contactForm.valid) {
      console.error("form not valid");
      return;
    }

    this.contactService.addContact(contact).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage: string;
        if (error.status === 400 || error.status === 404) {
          errorMessage = error.error || "Unknown error";
        }
        else {
          errorMessage = "Unknown error";
        }

        console.error("Error: " + errorMessage);
        return of(null);
      })
    ).subscribe(result => {
      if (result) {
        this.contactList.push(result);
        window.location.reload();
      }
    })
  }

  // Update function
  onUpdateClick() {
    const contact = new Contact(this.contactForm.value);

    this.contactService.updateContact(contact).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage: string;
        if (error.status === 400 || error.status === 404) {
          errorMessage = error.error || "Unknown error";
        }
        else {
          errorMessage = "Unknown error";
        }

        console.error("Error: " + errorMessage);
        return of(null);
      })
    ).subscribe(result => {
      if (result) {
        window.location.reload();
      }
    })
  }

  onSelectContactFromTable(contact: Contact) {
    const fb = FormBuilder;
    this.isUpdating = true;

    this.contactForm = this.fb.group({
      id: contact.id ?? [0],
      firstName: contact.firstName ?? [''],
      lastName: contact.lastName ?? [''],
      email: contact.email ?? [''],
      phoneNumber: contact.phoneNumber ?? [''],
      address: contact.address ?? [''],
      city: contact.city ?? [''],
      state: contact.state ?? [''],
      zipCode: contact.zipCode ?? [''],
      country: contact.country ?? [''],
      notes: contact.notes ?? [''],
    });
  }

  // Remove function
  onRemoveClick(id: number) {
    this.contactService.removeContactById(id).subscribe(result => {
      if (result) {
        window.location.reload();
        this.removeResults = "Contact removed!";
      }
    })
  }

  // add id on change in input
  change(event: any) {
    this.contactId = event.target.value;
  }
}
