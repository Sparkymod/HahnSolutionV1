namespace Application.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly ContactValidator _contactValidator;

    public ContactService(IContactRepository contactRepository, ContactValidator contactValidator)
    {
        _contactRepository = contactRepository;
        _contactValidator = contactValidator;
    }

    public Contact Add(Contact contact)
    {
        _contactValidator.ValidateAndThrow(contact);
        return _contactRepository.Create(contact);
    }

    public IReadOnlyCollection<Contact> GetAll()
    {
        return _contactRepository.GetAll();
    }

    public Contact GetById(int id)
    {
        return _contactRepository.GetById(id);
    }

    public bool Update(Contact contact)
    {
        _contactValidator.ValidateAndThrow(contact);
        return _contactRepository.Update(contact);
    }

    public bool RemoveById(int id)
    {
        return _contactRepository.DeleteById(id);
    }

    public bool Exist(Contact contact)
    {
        var foundContact = _contactRepository.GetById(contact.Id);
        return foundContact.Id == contact.Id;
    }
}