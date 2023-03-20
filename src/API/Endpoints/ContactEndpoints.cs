using System.ComponentModel;
using Application.Services.Interfaces;

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
    }

    private static void MapPostRoutes(this WebApplication app)
    {
        // Add contact
        app.MapPost("/addContact/", (Contact? contact, IContactService contactService) =>
        {
            if (contact is null) return Results.BadRequest("Invalid data.");

            var checkedContact = contactService.Exist(contact);
            if (checkedContact) return Results.BadRequest("This id is already present.");

            var addedContact = contactService.Add(contact);
            return Results.Created($"/contacts/{addedContact.Id}", addedContact);

        }).WithName("AddContact").Produces<IReadOnlyCollection<Contact>>();
    }

    private static void MapGetRoutes(this WebApplication app)
    {
        // Get all contacts
        app.MapGet("/getAllContacts", (IContactService contactService) =>
        {
            var contacts = contactService.GetAll();
            return contacts;

        }).WithName("GetAllContacts").Produces<IReadOnlyCollection<Contact>>();

        app.MapGet("/getContactById/{id:int}", (int id, IContactService contactService) =>
        {
            var contact = contactService.GetById(id);
            return contact.Id != 0 ? Results.Ok(contact) : Results.NotFound("Not contact found with Id: " + id);

        });
    }
}
