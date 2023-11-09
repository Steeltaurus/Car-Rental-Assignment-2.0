using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;
using System;

namespace Car_Rental.Business.Classes;

public class BookingProcessor
{
    private readonly IData _db;

    public string ssn =  String.Empty;
    public string firstName = String.Empty;
    public string lastName = String.Empty;

    public int customerId = 1;
    public int distance;

    public string regNr = String.Empty;
    public string make = String.Empty;
    public int odometer;
    public double costPerKm;
    public VehicleTypes vehicleType = VehicleTypes.Sedan;

    public bool processing = false;

    public string message = String.Empty;

    public BookingProcessor(IData db) => _db = db;

    public IEnumerable<Customer> GetCustomers()
    {
        return _db.GetList<Customer>(null);
    }
    public IEnumerable<Vehicle> GetVehicles(VehicleStatuses status = default)
    {
        return _db.GetList<Vehicle>(null);
    }
    public IEnumerable<IBooking> GetBookings()
    {        
        return _db.GetList<IBooking>(null);
    }

    public IPerson GetPerson(string ssn)
    {
        try
        {
            return _db.GetSingle<Customer>(c => c.SocialSecurityNumber == ssn) ?? throw new NullReferenceException();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public Vehicle GetVehicle(int vehicleId)
    {
        try
        {
            return _db.GetSingle<Vehicle>(v => v.Id == vehicleId) ?? throw new NullReferenceException();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public Vehicle GetVehicle(string regNr)
    {
        try
        {
            return _db.GetSingle<Vehicle>(v => v.RegistrationNumber == regNr) ?? throw new NullReferenceException();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public void AddCustomer(string ssn, string firstName, string lastName)
    {
        resetErrorMessage();
        
        if(ssn != "" && firstName != "" && lastName != "")
        {
            _db.Add<Customer>(new Customer(_db.NextPersonId, ssn, firstName, lastName));
            resetCustomerFields();
        }
    }
    public void AddVehicle(string regNr, string make, int odometer, double costPerKm, VehicleTypes vehicleType)
    {
        resetErrorMessage();

        if(regNr != "" && make != "" && costPerKm > 0)
        {
            if (vehicleType == VehicleTypes.Motorcycle)
                _db.Add<Vehicle>(new Motorcycle(_db.NextVehicleId, regNr, make, odometer, costPerKm, vehicleType));
            else
                _db.Add<Vehicle>(new Car(_db.NextVehicleId, regNr, make, odometer, costPerKm, vehicleType));

            resetVehicleFields();
        }
    }

    public async Task RentVehicle(int vehicleId, int customerId)
    {
        resetErrorMessage();
        try
        {
            var booking = _db.RentVehicle(vehicleId, customerId);
            processing = true;
            await Task.Delay(1000);
            _db.Add<IBooking>(booking);
            processing = false;

        }
        catch (Exception)
        {
            message = "Something went wrong!";
        }
    }
    public void ReturnVehicle(int vehicleId, int distance)
    {
        resetErrorMessage();
        try
        {
            var booking = _db.ReturnVehicle(vehicleId);
            var vehicle = GetVehicle(vehicleId);
            booking.OdometerReturned = booking.OdometerRented + distance;
            booking.DateReturned = DateTime.Today;
            booking.Cost = vehicle.CostPerDay * ((booking.DateReturned - booking.DateRented).TotalDays + 1)
                         + vehicle.CostPerKm * (booking.OdometerReturned - booking.OdometerRented);
            booking.Status = BookingStatuses.Closed;
        }
        catch (Exception)
        {
            message = "Something went wrong!";
        }
        resetVehicleFields();
    }

    public void resetCustomerFields()
    {
        ssn = String.Empty;
        firstName = String.Empty; 
        lastName = String.Empty;
    }
    public void resetVehicleFields()
    {
        regNr = String.Empty;
        make = String.Empty;
        odometer = 0;
        costPerKm = 0;
        distance = 0;
    }
    public void resetErrorMessage()
    {
        message = string.Empty;
    }

    public string[] VehicleStatusNames => _db.VehicleStatusNames;
    public string[] VehicleTypeNames => _db.VehicleTypeNames;
}
