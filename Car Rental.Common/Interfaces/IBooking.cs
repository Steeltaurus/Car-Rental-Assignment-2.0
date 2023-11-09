using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Interfaces;

public interface IBooking
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

    public void ReturnVehicle(Vehicle vehicle, int odometer, DateTime date);
}
