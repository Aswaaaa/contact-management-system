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
}
