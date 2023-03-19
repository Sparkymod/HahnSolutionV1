using Application.Services.Interfaces;

namespace API.Endpoints;

public static class ContactEndpoints
{
    /// <summary>
    ///     Map all <see cref="Contact"/> Routes.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication MapContactRoutes(this WebApplication app)
    {
        app.MapGet("/getAllContacts", (IContactService contactService) =>
            {
                var contacts = contactService.GetAll();
                return contacts;

            }).WithName("GetAllContacts").Produces<IReadOnlyCollection<Contact>>();

        app.MapPost("/addContact", (Contact? contact, IContactService contactService) =>
        {
            if (contact is null) return Results.BadRequest("Invalid contact data.");

            var addedContact = contactService.Add(contact);
            return Results.Created($"/contacts/{addedContact.Id}", addedContact);

        }).WithName("AddContact").Produces<IReadOnlyCollection<Contact>>();

        return app;
    }
}
