using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Interfaces;

public interface IVehicle
{
    public int Id { get; set; }
    public string RegistrationNumber { get; init; }
    public string Make { get; init; }
    public int Odometer { get; }
    public double CostPerKm { get; init; }
    public int CostPerDay { get; init; }
    public VehicleTypes VehicleType { get; init; }
    public VehicleStatuses Status { get; }
}
