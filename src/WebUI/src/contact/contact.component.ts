import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

import { Contact } from '../models/contact.model';
import { ContactService } from '../services/contact.service';

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

  constructor(private contactService: ContactService, private fb: FormBuilder) {
    this.contactForm = this.fb.group({
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
    this.contactService.getContactById(id).subscribe(result => {
      this.contactList = [];
      this.contactList.push(result);
    })
  }

  // Add function
  onAddClick() {
    const formData = this.contactForm.value;
    const contact = new Contact(formData);
    this.count = this.count + 1;
    contact.id = this.count;
    this.contactService.addContact(contact).subscribe(result => {
      this.contactList.push(result);
    })
  }

  // Update function
  onUpdateClick(contact: Contact) {
    this.contactService.updateContact(contact).subscribe(result => {
      
    })
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

  // 
  change(event: any) {
    this.contactId = event.target.value;
  }
}
