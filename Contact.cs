using System;

namespace contact_management_system;



class Contact
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string PhoneNumber { get; set; }
  public string Email { get; set; }
  public DateTime Birthday { get; set; }
  public ContactType Type { get; set; }

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
  private static List<Contact> contacts = new List<Contact>();

  public static bool AddContact(Contact contact)
  {
    if (string.IsNullOrWhiteSpace(contact.FirstName) || string.IsNullOrWhiteSpace(contact.LastName))
    {
      Console.WriteLine("Error: name cannot be empty");
      return false;
    }

    if (contacts.Any(c => c.Email == contact.Email))
    {
      System.Console.WriteLine("error: Email already exists");
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
  public static bool UpdateContact(string email, string newFirstName, string newLastName, string newPhoneNumber)
  {
    Contact contact = contacts.FirstOrDefault(c => c.Email == email);
    if (contact == null) return false;

    if (!string.IsNullOrWhiteSpace(newFirstName))
      contact.FirstName = newFirstName;

    if (!string.IsNullOrWhiteSpace(newLastName))
      contact.LastName = newLastName;

    if (!string.IsNullOrWhiteSpace(newPhoneNumber))
      contact.PhoneNumber = newPhoneNumber;

    return true;
  }
  public static bool DeleteContact(string email)
  {
    Contact contact = contacts.FirstOrDefault(c => c.Email == email);
    if (contact == null) return false;
    return contacts.Remove(contact);
  }
  public void DisplaySummary(){
    System.Console.WriteLine("\n Contact Summary");
    System.Console.WriteLine($"Name: {FirstName} {LastName}");
    System.Console.WriteLine($"Email: {Email}");
    System.Console.WriteLine($"PhoneNumber: {PhoneNumber}");
    System.Console.WriteLine($"Birthday: {Birthday}");
    System.Console.WriteLine($"Age: {Age}");
    System.Console.WriteLine($"Type: {Type}");
  }
}
