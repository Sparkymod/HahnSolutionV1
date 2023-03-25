# HahnSolutionV1
This repository contains the source code for a WebApi and an Angular CRUD application based on the Domain-Driven Design (DDD) pattern using .Net6.

## The solution consists of two projects:

### API 
- Contains the WebApi based on the DDD pattern using .Net6. 
- The API exposes endpoints to manage the data for an entity called Applicant. 
- The data is validated using the FluentValidation nuget package.

### WebUI
- Contains the Angular CRUD application which provides a user interface to manage the data for the Contact entity. 
- The front-end is also validated using fluentvalidation.


## How to Run the Application
- Clone this repository.

### API
- Right click on the API project and `Open in Terminal` then run `dotnet run`.
- Alternatively, you can run a Docker container by pulling the sparkymod/hahnapp:latest image using the following command: 
- `docker pull sparkymod/hahnapp:latest`. Then, start the container using the command `docker run -p 8080:80 sparkymod/hahnapp:latest`.

### Angular 
- Right click on the WebUI project and `Open in Terminal`.
- Run `npm install` to install the required packages.
- Run `ng serve`. If you don't have it run `npm i @angular/cli` to install it.
- Navigate to http://localhost:4200/contact in your browser to access the application.

### The API is dockerized and can be run in a container using the sparkymod/hahnapp:latest image.

## Technology Stack
- .Net6 (minimal API).
- Domain-Driven Design (DDD) Pattern.
- Angular
- Typescript
Docker
API Endpoints

## The API exposes the following endpoints:

- GET: /getAllContacts - retrieves a list of all contacts
- GET: /getContactById/{id} - retrieves a contact by id
- POST: /addContact - adds a new contact
- PUT: /updateContact - updates an existing contact
- DELETE: /removeContactById/{id} - deletes a contact by id

### The Angular application provides the following functionality:
- View a list of all contacts
- Add a new contact
- Edit an existing contact
- Delete an existing contact
