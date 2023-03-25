import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, map, Observable } from 'rxjs';
import { Contact } from '../models/contact.model';
import { of } from 'rxjs';


var apiUrl = "http://localhost:5000";

var httpLink = {
  getAllContacts: apiUrl + "/getAllContacts",
  getContactById: apiUrl + "/getContactById/",
  addContact: apiUrl + "/addContact",
  removeContactById: apiUrl + "/removeContactById/",
  updateContact: apiUrl + "/updateContact"
}

@Injectable({
  providedIn: 'root'
})

export class ContactService {

  constructor(private httpClient: HttpClient) { }

  // GET all contacts
  public getAllContacts(): Observable<Array<Contact>> {
    return this.httpClient.get(httpLink.getAllContacts).pipe(
      map((response: any) => response));
  }

  // GET contact by Id
  public getContactById(id: number): Observable<Contact> {
    return this.httpClient.get(httpLink.getContactById + id).pipe(
      map((response: any) => response));
  }

  // POST contact
  public addContact(contact: Contact): Observable<Contact> {
    return this.httpClient.post(httpLink.addContact, contact).pipe(
      map((response: any) => response));
  }

  // DELETE contact
  public removeContactById(id: number): Observable<boolean> {
    return this.httpClient.delete(httpLink.removeContactById + id).pipe(
      map((response: any) => response));
  }

  // PUT contact
  public updateContact(contact: Contact): Observable<Contact> {
    return this.httpClient.put(httpLink.updateContact, contact).pipe(
      map((response: any) => response));
  }
}
