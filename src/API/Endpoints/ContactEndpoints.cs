using System.ComponentModel;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;

namespace API.Endpoints;

public static class ContactEndpoints
{
    /// <summary>
    ///     Configures and maps contact routes in the Program.cs file for a WebApplication instance.
    /// </summary>
    /// <remarks>
    ///     Invoke this method in the Program.cs file to map all contact routes (GET, POST, PUT, DELETE) for the WebApplication instance.
    /// </remarks>
    public static void MapContactRoutes(this WebApplication app)
    {
        app.MapGetRoutes();

        app.MapPostRoutes();

        app.MapPostRoutes();

        app.MapDeleteRoutes();
    }

    #region POST
    /// <summary>
    ///     Maps POST routes for the WebApplication instance.
    /// </summary>
    /// <param name="app">The WebApplication instance for which the POST routes will be mapped.</param>
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
    }

    #endregion

    #region PUT
    /// <summary>
    ///     Maps PUT routes for the WebApplication instance.
    /// </summary>
    /// <param name="app">The WebApplication instance for which the PUT routes will be mapped.</param>
    private static void MapPutRoutes(this WebApplication app)
    {
        // Update contact
        app.MapPut("/updateContact", (Contact? contact, IContactService contactService) =>
        {
            if (contact is null) return Results.BadRequest("Invalid data.");

            var checkedContact = contactService.Exist(contact);
            if (!checkedContact) return Results.NotFound("This contact doesn't exist.");

            var addedContact = contactService.Update(contact);
            return Results.Ok(addedContact);
        });
    }
    #endregion

    #region DELETE
    /// <summary>
    ///     Maps DELETE routes for the WebApplication instance.
    /// </summary>
    /// <param name="app">The WebApplication instance for which the DELETE routes will be mapped.</param>
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
    #endregion

    #region GET
    /// <summary>
    ///     Maps GET routes for the WebApplication instance.
    /// </summary>
    /// <param name="app">The WebApplication instance for which the GET routes will be mapped.</param>
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

    #endregion
}
