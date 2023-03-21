using Infrastructure.Storages;

namespace Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly ContactStorage _database;

    public ContactRepository(ContactStorage database)
    {
        _database = database;
    }

    public Contact Create(Contact contact)
    {
        if (_database.Contacts.Contains(contact)) return contact;

        _database.Contacts.Add(contact);
        return contact;
    }

    public IReadOnlyCollection<Contact> GetAll() => _database.Contacts;

    public Contact GetById(int id)
    {
        var contact = _database.Contacts.FirstOrDefault(c => c.Id.Equals(id));
        return contact ?? new Contact();
    } 

    public bool Update(Contact updatedContact)
    {
        var contact = _database.Contacts.FirstOrDefault(c => c.Id.Equals(updatedContact.Id));

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
        var contact = _database.Contacts.FirstOrDefault(c => c.Id.Equals(id));

        if (contact is null) return false;
        if (!_database.Contacts.Contains(contact)) return false;

        _database.Contacts.Remove(contact);
        return true;
    }
}