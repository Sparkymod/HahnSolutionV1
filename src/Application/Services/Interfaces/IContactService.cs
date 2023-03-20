namespace Application.Services.Interfaces;

public interface IContactService
{
    public Contact Add(Contact contact);
    public bool Update(Contact contact);
    public bool RemoveById(int id);
    public IReadOnlyCollection<Contact> GetAll();
    public Contact GetById(int id);
    public bool Exist(Contact contact);
}