using Infrastructure.Storages;

namespace Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    public ContactStorage Database { get; set; } = new ();

    public Contact Create(Contact contact)
    {
        if (Database.Contacts.Contains(contact)) return contact;

        contact.Id = Database.Contacts.Count > 0 ? Database.Contacts.Max(c => c.Id) + 1 : 1;
        Database.Contacts.Add(contact);
        return contact;
    }

    public IReadOnlyCollection<Contact> GetAll() => Database.Contacts;

    public Contact GetById(int id)
    {
        var contact = Database.Contacts.FirstOrDefault(c => c.Id.Equals(id));
        return contact ?? new Contact();
    } 

    public bool Update(Contact updatedContact)
    {
        var contact = Database.Contacts.FirstOrDefault(c => c.Id.Equals(updatedContact.Id));

        if (contact is null) return false;

        contact.FirstName = updatedContact.FirstName;
        contact.LastName = updatedContact.LastName;
        contact.Email = updatedContact.Email;
        contact.City = updatedContact.City;
        contact.Country = updatedContact.Country;
        contact.State = updatedContact.State;
        contact.ZipCode = updatedContact.ZipCode;
        contact.Address = updatedContact.Address;
        contact.PhoneNumber = updatedContact.PhoneNumber;
        contact.Notes = updatedContact.Notes;

        return true;
    }

    public bool DeleteById(int id)
    {
        var contact = Database.Contacts.FirstOrDefault(c => c.Id.Equals(id));

        if (contact is null) return false;
        if (!Database.Contacts.Contains(contact)) return false;

        Database.Contacts.Remove(contact);
        return true;
    }
}