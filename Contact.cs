using System;

namespace contact_management_system;

public class Contact
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string PhoneNumber { get; set; }
  public string Email { get; set; }
  public DateTime Birthday { get; set; }
  public ContactType Type { get; set; }

  private static List<Contact> contacts = new List<Contact>();
  private static readonly ContactValidator validator = new ContactValidator();

  public int Age
  {
    get
    {
      var today = DateTime.Today;
      var age = today.Year - Birthday.Year;
      if (Birthday.Date > today.AddYears(-age)) age--;
      return age;
    }
  }
  public (bool isValid, string errorMessage) Validate()
  {
    return validator.ValidateContact(this);
  }

  public static bool AddContact(Contact contact)
  {
    var (isValid, errorMessage) = contact.Validate();
    if (!isValid)
    {
      Console.WriteLine($"Error: {errorMessage}");
      return false;
    }

    if (contacts.Any(c => c.Email.Equals(contact.Email, StringComparison.OrdinalIgnoreCase)))
    {
      Console.WriteLine("Error: Email already exists!");
      return false;
    }

    contact.DisplaySummary();

    Console.Write("\nSave contact? (Y/N): ");
    if (Console.ReadLine().Trim().ToUpper() != "Y")
    {
      Console.WriteLine("Contact not saved.");
      return false;
    }

    contacts.Add(contact);
    return true;
  }

  public static List<Contact> GetAllContacts()
  {
    return contacts;
  }
  public static List<Contact> SearchContacts(string search)
  {
    return contacts.Where(c => c.FirstName.ToLower().Contains(search.ToLower()) ||
    c.LastName.ToLower().Contains(search.ToLower()))
    .ToList();
  }
  public static bool UpdateContact(string email, string newFirstName, string newLastName, string newPhone)
  {
    var contact = contacts.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    if (contact == null)
    {
      return false;
    }

    var tempContact = new Contact
    {
      FirstName = !string.IsNullOrWhiteSpace(newFirstName) ? newFirstName : contact.FirstName,
      LastName = !string.IsNullOrWhiteSpace(newLastName) ? newLastName : contact.LastName,
      PhoneNumber = !string.IsNullOrWhiteSpace(newPhone) ? newPhone : contact.PhoneNumber,
      Email = contact.Email,
      Birthday = contact.Birthday,
      Type = contact.Type
    };

    var (isValid, errorMessage) = tempContact.Validate();
    if (!isValid)
    {
      Console.WriteLine($"Error: {errorMessage}");
      return false;
    }

    contact.FirstName = tempContact.FirstName;
    contact.LastName = tempContact.LastName;
    contact.PhoneNumber = tempContact.PhoneNumber;

    return true;
  }
  public static bool DeleteContact(string email)
  {
    var contact = contacts.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    if (contact == null)
    {
      return false;
    }

    return contacts.Remove(contact);
  }
  public void DisplaySummary()
  {
    System.Console.WriteLine("\n Contact Summary");
    System.Console.WriteLine($"Name: {FirstName} {LastName}");
    System.Console.WriteLine($"Email: {Email}");
    System.Console.WriteLine($"PhoneNumber: {PhoneNumber}");
    System.Console.WriteLine($"Birthday: {Birthday}");
    System.Console.WriteLine($"Age: {Age}");
    System.Console.WriteLine($"Type: {Type}");
  }
}