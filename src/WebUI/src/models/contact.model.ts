export class Contact {
  id: number = 0;
  firstName: string = "";
  lastName: string = "";
  email: string = "";
  phoneNumber: string = "";
  address: string = "";
  city: string = "";
  state: string = "";
  zipCode: string = "";
  country: string = "";
  notes: string = "";

  constructor(data?: Partial<Contact>) {
    if (data) {
      Object.assign(this, data);
    }
  }
}
