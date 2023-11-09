using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using System.Linq.Expressions;

namespace Car_Rental.Data.Interfaces;

public interface IData
{
    List<T> GetList<T>(Func<T, bool>? expression) where T : class;
    T? GetSingle<T>(Func<T, bool> expression) where T : class;
    public void Add<T>(T item) where T : class;

    int NextVehicleId { get; }
    int NextPersonId { get; }
    int NextBookingId { get; }

    IBooking RentVehicle(int vehicleId, int customerId);
    IBooking ReturnVehicle(int vehicleId);

    // Default Interface Methods
    public string[] VehicleStatusNames => Enum.GetNames(typeof(VehicleStatuses));
    public string[] VehicleTypeNames => Enum.GetNames(typeof(VehicleTypes));
    public VehicleTypes GetVehicleType(string name) => Enum.Parse<VehicleTypes>(name);
}