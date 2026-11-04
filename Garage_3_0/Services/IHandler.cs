using Garage_3_0.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace Garage_3_0.Services
{
    public interface IHandler
    {
        int GarageId { get; }
        int TotalNumberOfParkingLots { get; }
        int NumberOfElectricParkingLots { get; }

        //*************************************************************************************
        // The method AddVehicle is used to park a vehicle of specified type in the garage.    
        // It also increments the currentVehicleCount, currentElectricVehicleCount and 
        // currentOccupiedSpots based on the type of vehicle being parked.        
        //*************************************************************************************
        void AddVehicle(string vehicleType,
                               string regNumber,
                               string brand,
                               string inputfuelType,
                               string color,
                               int numOfSeats = 0,
                               int numOfWheels = 0);

        //***********************************************************************************
        // The method AddVehicleFromFile is used to add a vehicle in the garage from a file.
        //************************************************************************************
        void AddVehicleFromFile(int antal);

        //*************************************************************************************
        // The method ReadVehicleFromFile reads the vehicles from a garage file and adds it
        // to the recreated garage.       
        //**************************************************************************************
        void ReadVehicleFromFile(StreamReader reader);

        //****************************************************************************************
        // The method GetVehicle is used to retrieve a vehicle from the garage. It takes a string 
        // parameter representing the registration number of the vehicle to be retrieved.
        //****************************************************************************************
        bool GetVehicle(string regNumber);

        //***********************************************************************************
        // The method ListAllVehicles is used to list all vehicles parked in the garage.
        //***********************************************************************************
        void ListAllVehicles();

        //***********************************************************************************
        // The method SearchVehicles is used to list vehicles with certain criterias such as 
        // vehicle type/brand/fuelType/color/numberOfSeats/numberOfWheels parked in the garage.
        //***********************************************************************************
        void SearchVehicles(SearchCriteria searchCriteria);

        //***********************************************************************************
        // The method IsVehicleWithRegNumInGarage checks if vehichle with given reg number
        // exists in the garage.
        //***********************************************************************************
        bool IsVehicleWithRegNumInGarage(string regNumber);

        //***********************************************************************************
        // This method stores the garage information to a file when the application is closed.
        // It uses a StreamWriter to write the garage information to a file (GarageId-1.txt).
        //************************************************************************************
        Task StoreGarageInfo();
    }
}
