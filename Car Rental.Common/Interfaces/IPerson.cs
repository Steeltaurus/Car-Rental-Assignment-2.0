namespace Car_Rental.Common.Interfaces;

public interface IPerson
{
    public int Id { get; set; }
    public string SocialSecurityNumber { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
}
