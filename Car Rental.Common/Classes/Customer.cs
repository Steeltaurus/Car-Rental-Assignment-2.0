using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Customer : IPerson
{
    public int Id { get; set; }
    public string SocialSecurityNumber { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }

    public Customer(int id, string socialSecurityNumber, string firstName, string lastName) =>
        (Id, SocialSecurityNumber, FirstName, LastName) = 
        (id, socialSecurityNumber, firstName, lastName);
}