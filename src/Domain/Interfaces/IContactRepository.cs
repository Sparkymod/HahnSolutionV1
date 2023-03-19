namespace Domain.Interfaces;

public interface IContactRepository
{
    public Contact Create(Contact contact);
    public bool Update(Contact updatedContact);
    public bool DeleteById(int id);
    public Contact GetById(int id);
    public IReadOnlyCollection<Contact> GetAll();
}