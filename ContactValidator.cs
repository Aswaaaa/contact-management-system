using System;
using System.Text.RegularExpressions;

namespace contact_management_system;

public class ContactValidator
{
  public (bool isValid, string errorMessage) ValidateContact(Contact contact)
  {
    if (!ValidateName(contact.FirstName))
    {
      return (false, "First Name must be 2-50 characters and contain only letters");
    }
    if (!ValidateName(contact.LastName))
    {
      return (false, "Last name must be 2-50 characters and contain only letters");
    }
    if (!ValidatePhone(contact.PhoneNumber))
    {
      return (false, "Phone number must be in format (XXX) XXX-XXXX or XXX-XXX-XXXX");
    }
    if (!ValidateEmail(contact.Email))
    {
      return (false, "Email must contain @  and a valid domain (.com, ,org, .in)");
    }
    if (!ValidateBirthday(contact.Birthday))
    {
      return (false, "Invalid Birthday. Person must be a 18 years old.");
    }
    return (true, " ");
  }
  public bool ValidateName(string name)
  {
    if (string.IsNullOrWhiteSpace(name))
      return false;


    return name.Length >= 2 &&
           name.Length <= 50 &&
           name.All(char.IsLetter);
  }
  public bool ValidatePhone(string PhoneNumber)
  {
    if (string.IsNullOrWhiteSpace(PhoneNumber))
      return false;

    string pattern = @"^\(\d{3}\) \d{3}-\d{4}$|^\d{3}-\d{3}-\d{4}$";
    return Regex.IsMatch(PhoneNumber, pattern);
  }
  public bool ValidateEmail(string email)
  {
    if (string.IsNullOrWhiteSpace(email))
      return false;

    try
    {
      var addr = new System.Net.Mail.MailAddress(email);
      var domain = email.Split('@')[1];
      return domain.Contains(".") &&
             (domain.EndsWith(".com") ||
              domain.EndsWith(".org") ||
              domain.EndsWith(".in") ||
              domain.EndsWith(".edu"));
    }
    catch
    {
      return false;
    }
  }
  public bool ValidateBirthday(DateTime birthday)
  {
    var today = DateTime.Today;
    var age = today.Year - birthday.Year;
    if (birthday.Date > today.AddYears(-age)) age--;

    return birthday.Date <= today && age >= 18;
  }
}
