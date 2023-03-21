using System.ComponentModel;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;

namespace API.Endpoints;

public static class ContactEndpoints
{
    /// <summary>
    ///     Map all <see cref="Contact"/> Routes.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static void MapContactRoutes(this WebApplication app)
    {
        app.MapGetRoutes();
        app.MapPostRoutes();
        app.MapDeleteRoutes();
    }

    /// <summary>
    ///     Map POST routes.
    /// </summary>
    /// <param name="app"></param>
    private static void MapPostRoutes(this WebApplication app)
    {
        // Add contact
        app.MapPost("/addContact/", (Contact? contact, IContactService contactService) =>
        {
            if (contact is null) return Results.BadRequest("Invalid data.");

            var checkedContact = contactService.Exist(contact);
            if (checkedContact) return Results.BadRequest("This id is already present.");

            var addedContact = contactService.Add(contact);
            return Results.Ok(addedContact);

        }).WithName("AddContact").Produces<IReadOnlyCollection<Contact>>();

        // Update contact
        app.MapPut("/updateContact", (Contact? contact, IContactService contactService) =>
        {
            if (contact is null) return Results.BadRequest("Invalid data.");

            var checkedContact = contactService.Exist(contact);
            if (!checkedContact) return Results.BadRequest("This contact doesn't exist.");

            var addedContact = contactService.Update(contact);
            return Results.Ok(addedContact);
        });
    }

    /// <summary>
    ///     Map DELETE routes.
    /// </summary>
    /// <param name="app"></param>
    private static void MapDeleteRoutes(this WebApplication app)
    {
        app.MapDelete("/removeContactById/{id:int}", (int id, IContactService contactService) =>
        {
            var isRemoved = contactService.RemoveById(id);

            return isRemoved 
                ? Results.Ok("Contact was removed successfully!") 
                : Results.BadRequest("Contact wasn't removed.");
        });
    }

    /// <summary>
    ///     Map GET routes.
    /// </summary>
    /// <param name="app"></param>
    private static void MapGetRoutes(this WebApplication app)
    {
        // All contacts
        app.MapGet("/getAllContacts", (IContactService contactService) =>
        {
            var contacts = contactService.GetAll();
            return contacts;

        }).WithName("GetAllContacts").Produces<IReadOnlyCollection<Contact>>();

        // Contact by id
        app.MapGet("/getContactById/{id:int}", (int id, IContactService contactService) =>
        {
            var contact = contactService.GetById(id);
            return contact.Id != 0 ? Results.Ok(contact) : Results.NotFound("Not contact found with Id: " + id);

        }).WithName("GetContactById").Produces<Contact>();


    }
}
