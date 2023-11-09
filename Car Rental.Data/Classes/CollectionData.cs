using Car_Rental.Common.Enums;
using Car_Rental.Data.Interfaces;
using Car_Rental.Common.Interfaces;
using Car_Rental.Common.Classes;
using System.Linq.Expressions;
using System.Reflection;
using Car_Rental.Common.Extensions;

namespace Car_Rental.Data.Classes;

public class CollectionData : IData
{
    readonly List<Customer> _persons = new();
    readonly List<Vehicle> _vehicles = new();
    readonly List<IBooking> _bookings = new();

    public CollectionData() => SeedData();

    public void SeedData()
    {
        _persons.Add(new Customer(NextPersonId, "12345", "Adam", "And"));
        _persons.Add(new Customer(NextPersonId, "34567", "Berit", "Bengtson"));
        _persons.Add(new Customer(NextPersonId, "56789", "Ceasar", "Carlsson"));

        _vehicles.Add(new Car(NextVehicleId, "ABC123", "Volvo", 10000, 1.0, VehicleTypes.Combi));
        _vehicles.Add(new Car(NextVehicleId, "DEF456", "Saab", 20000, 1.0, VehicleTypes.Sedan));
        _vehicles.Add(new Car(NextVehicleId, "GHI789", "Tesla", 1000, 3.0, VehicleTypes.Sedan));
        _vehicles.Add(new Car(NextVehicleId, "JKL012", "Jeep", 5000, 1.5, VehicleTypes.Van, VehicleStatuses.Booked));
        _vehicles.Add(new Motorcycle(NextVehicleId, "MNO234", "Yamaha", 30000, 0.5, VehicleTypes.Motorcycle));

        _bookings.Add(new Booking(NextBookingId, "JKL012", "Adam And (12345)", 5000, new DateTime(2023, 11, 08)));
    }

    public List<T> GetList<T>(Func<T, bool>? expression) where T : class
    {
        try
        {
            return GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FindCollection<T>()
                .GetData<T>(this)
                .ToQueryable<T>()
                .Filter(expression);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public T? GetSingle<T>(Func<T, bool> expression) where T : class
    {
        try
        {
            return GetList<T>(null).SingleOrDefault(expression);

        }
        catch (Exception)
        {
            throw;
        }
    }
    public void Add<T>(T item) where T : class
    {
        try
        {
            GetType()
            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            .FindCollection<T>()
            .GetData<T>(this)?.Add(item);
        }
        catch (Exception)
        {
            throw;
        }

    }

    public int NextVehicleId  => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(b => b.Id) + 1;
    public int NextPersonId => _persons.Count.Equals(0) ? 1 : _persons.Max(b => b.Id) + 1;
    public int NextBookingId => _bookings.Count.Equals(0) ? 1 : _bookings.Max(b => b.Id) + 1;

    public IBooking RentVehicle(int vehicleId, int customerId)
    {
        try
        {
            var vehicle = _vehicles.SingleOrDefault<Vehicle>(v => v.Id == vehicleId) ?? throw new NullReferenceException();
            var customer = _persons.SingleOrDefault<Customer>(c => c.Id == customerId) ?? throw new NullReferenceException();
            vehicle.Status = VehicleStatuses.Booked;
            return new Booking(NextBookingId, vehicle.RegistrationNumber, $"{ customer.FirstName } { customer.LastName } ({customer.SocialSecurityNumber})" , vehicle.Odometer, DateTime.Today);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public IBooking ReturnVehicle(int vehicleId)
    {
        try
        {
            var vehicle = _vehicles.SingleOrDefault<Vehicle>(v => v.Id == vehicleId) ?? throw new NullReferenceException();
            var booking = _bookings.SingleOrDefault<IBooking>(b => b.RegistrationNumber == vehicle.RegistrationNumber && b.Status == BookingStatuses.Open) ?? throw new NullReferenceException();
            vehicle.Status = VehicleStatuses.Available;
            return booking;
        }
        catch (Exception)
        {
            throw;
        }
    }
}