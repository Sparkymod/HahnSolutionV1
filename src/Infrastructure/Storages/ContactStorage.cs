namespace Infrastructure.Storages;

public class ContactStorage
{
    public List<Contact> Contacts { get; set; } = new ()
    {
        new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "15551234567" },
        new Contact { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com", PhoneNumber = "15559876543" },
        new Contact { Id = 3, FirstName = "Alice", LastName = "Smith", Email = "alice.smith@example.com", PhoneNumber = "15555678910" },
        new Contact { Id = 4, FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@example.com", PhoneNumber = "15552468013" },
        new Contact { Id = 5, FirstName = "Charlie", LastName = "Brown", Email = "charlie.brown@example.com", PhoneNumber = "15553691472" }
    };
}