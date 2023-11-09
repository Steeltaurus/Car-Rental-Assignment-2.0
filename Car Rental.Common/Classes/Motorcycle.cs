using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Motorcycle : Vehicle
{
    public Motorcycle(int id, string registrationNumber, string make, int odometer, double costPerKm,
        VehicleTypes vehicleType, VehicleStatuses status = VehicleStatuses.Available)
        : base(id, registrationNumber, make, odometer, costPerKm, vehicleType, status)
    {
        CostPerDay = 50;
    }
}
