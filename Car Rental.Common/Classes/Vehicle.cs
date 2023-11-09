using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Classes;

public class Vehicle
{
    public int Id { get; set; }
    public string RegistrationNumber { get; init; }
    public string Make { get; init; }
    public int Odometer { get; private set; }
    public double CostPerKm { get; init; }
    public int CostPerDay { get; init; }
    public VehicleTypes VehicleType { get; init; }
    public VehicleStatuses Status { get; set; }

    public Vehicle(int id, string registrationNumber, string make, int odometer, double costPerKm,
        VehicleTypes vehicleType, VehicleStatuses status = VehicleStatuses.Available)
    {
        Id = id;
        RegistrationNumber = registrationNumber;
        Make = make;
        Odometer = odometer;
        CostPerKm = costPerKm;
        VehicleType = vehicleType;
        Status = status;
    }
}
