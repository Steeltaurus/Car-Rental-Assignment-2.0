using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Booking : IBooking
{
    public int Id { get; init; }
    public string RegistrationNumber { get; init; }
    public string Customer { get; init; }
    public int OdometerRented { get; init; }
    public int OdometerReturned { get; set; }
    public DateTime DateRented { get; init; }
    public DateTime DateReturned { get; set; }
    public double Cost { get; set; }
    public BookingStatuses Status { get; set; }

    public Booking(int id, string registrationNumber, string customer, int odometerRented,
        DateTime dateRented)
    {
        Id = id;
        RegistrationNumber = registrationNumber;
        Customer = customer;
        OdometerRented = odometerRented;
        DateRented = dateRented;
        Status = BookingStatuses.Open;
    }

    public void ReturnVehicle(Vehicle vehicle, int odometer, DateTime date)
    {
        OdometerReturned = odometer;
        DateReturned = date;
        Status = BookingStatuses.Closed;
        Cost = vehicle.CostPerDay * (DateReturned - DateRented).TotalDays + 1
             + vehicle.CostPerKm * (OdometerReturned - OdometerRented);
    }
}
