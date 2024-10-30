using contact_management_system;

class Program
{
  static void Main(String[] args)
  {
    while (true)
    {
      Console.Clear();
      ShowMenu();

      System.Console.WriteLine("Enter Your choice...(1-6): ");
      var choice = Console.ReadLine();
      try
      {

        switch (choice)
        {
          case "1":
            DisplayContact();
            break;
          case "2":
            AddContact();
            break;
          case "3":
            SearchContacts();
            break;
          case "4":
            UpdateContacts();
            break;
          case "5":
            DeleteContact();
            break;
          case "6":
            System.Console.WriteLine("Thanks!");
            return;
          default:
            System.Console.WriteLine("Invalid choice please try again.");
            break;
        }
      }
      catch (Exception ex)
      {
        System.Console.WriteLine($"Error: {ex.Message}");
      }
      System.Console.WriteLine("\nPress any key to continue...");
      Console.ReadKey();
    }
  }
  static void ShowMenu()
  {
    System.Console.WriteLine("!!!! Welcome to Contact Management System !!!!");
    System.Console.WriteLine("1.Display All contacts");
    System.Console.WriteLine("2.Add a new contact");
    System.Console.WriteLine("3.Search for contacts");
    System.Console.WriteLine("4.Update contact");
    System.Console.WriteLine("5.Delete contact");
    System.Console.WriteLine("6.Exit");
  }
  static void AddContact()
  {
    System.Console.WriteLine("Add a new contact");

    Contact contact = new Contact();
    System.Console.Write("Enter First name: ");
    contact.FirstName = Console.ReadLine();

    System.Console.WriteLine("Enter Last name: ");
    contact.LastName = Console.ReadLine();

    System.Console.Write("Enter Phone Number ((XXX) XXX-XXXX or XXX-XXX-XXXX): ");
    contact.PhoneNumber = Console.ReadLine();

    System.Console.WriteLine("Enter Email: ");
    contact.Email = Console.ReadLine();

    while (true)
    {
      System.Console.WriteLine("Enter BirthDay (DD/MM/YYYY): ");
      if (DateTime.TryParse(Console.ReadLine(), out DateTime birthday))
      {
        contact.Birthday = birthday;
        break;
      }
      System.Console.WriteLine("Invalid date format");
    }
    System.Console.WriteLine("Enter contcat type (1 for Personal, 2 for Professional): ");
    contact.Type = Console.ReadLine() == "1" ? ContactType.Personal : ContactType.Professional;

    if (Contact.AddContact(contact))
    {
      System.Console.WriteLine("Contact added successfully");
    }

  }
  static void DisplayContact()
  {
    var contacts = Contact.GetAllContacts();
    if (contacts.Count == 0)
    {
      System.Console.Write("\n No contacts found");
      return;
    }
    System.Console.WriteLine("Contact List: ");
    foreach (var contact in contacts)
    {
      contact.DisplaySummary();
    }
  }
  static void SearchContacts()
  {
    System.Console.Write("\n Enter a keyword");
    string search = Console.ReadLine();

    var results = Contact.SearchContacts(search);

    if (results.Count == 0)
    {
      System.Console.WriteLine("No matches found");
      return;
    }
    System.Console.WriteLine("  Search results... : ");
    foreach (var contact in results)
    {
      contact.DisplaySummary();
    }
  }
  static void UpdateContacts()
  {
    System.Console.Write("\n Enter email of contact to update :");
    string email = Console.ReadLine();

    System.Console.Write("\n Enter new First Name(or press Enter to keep existing): ");
    string newFirstName = Console.ReadLine();

    System.Console.Write("\n enter new Last Name(or press Enter to keep existing): ");
    string newLastName = Console.ReadLine();

    System.Console.Write("\n Enter new Phone number(or press Enter to keep existing): ");
    string newPhoneNumber = Console.ReadLine();

    if (Contact.UpdateContact(email, newFirstName, newLastName, newPhoneNumber))
    {
      System.Console.WriteLine("Contact updated successfully");
    }
    else
    {
      System.Console.WriteLine("Contact not found or invalid data");
    }

  }
  static void DeleteContact()
  {
    System.Console.Write("\n Enter email of contact to delete: ");
    string email = Console.ReadLine();

    System.Console.Write("\n Are you sure you want to delete? Y/N : ");
    if (Console.ReadLine().ToUpper() == "Y")
    {
      if (Contact.DeleteContact(email))
      {
        System.Console.WriteLine("Contact deleted successfully");
      }
      else
      {
        System.Console.WriteLine("Contact not found");
      }
    }
  }
}