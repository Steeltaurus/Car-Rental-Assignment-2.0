using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Car : Vehicle
{
    public Car(int id, string registrationNumber, string make, int odometer, double costPerKm,
        VehicleTypes vehicleType, VehicleStatuses status = VehicleStatuses.Available)
        : base(id, registrationNumber, make, odometer, costPerKm, vehicleType, status)
    {
        switch (vehicleType)
        {
            case VehicleTypes.Sedan:
                CostPerDay = 100;
                break;
            case VehicleTypes.Combi:
                CostPerDay = 200;
                break;
            case VehicleTypes.Van:
                CostPerDay = 300;
                break;
        }
    }
}
