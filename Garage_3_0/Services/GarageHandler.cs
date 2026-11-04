using Garage_3_0.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Garage_3_0.Services
{
    public class GarageHandler : IHandler
    {
        private readonly Garage<Vehicle> garage;
        private readonly List<Garage<Vehicle>> garages = new List<Garage<Vehicle>>();
            
        public int GarageId { get { return garage.GarageId; }}
        public int TotalNumberOfParkingLots { get { return garage.TotalNumberOfParkingLots; }}
        public int NumberOfElectricParkingLots { get { return garage.NumberOfElectricParkingLots; }}

        //****************************************************************************
        // Constructor for the Garage class
        //****************************************************************************
        public GarageHandler(int numberOfSpots, 
                             int numberOfElectricSpots, 
                             int pricePerHour)
        {
            garage = new Garage<Vehicle>(numberOfSpots, 
                                         numberOfElectricSpots, 
                                         pricePerHour);

            garages.Add(garage);
        }

        //****************************************************************************
        // Constructor for the Garage class, used when recreating garage information
        // from a file
        //****************************************************************************
        public GarageHandler(int garageId,
                             int numberOfSpots,
                             int numberOfElectricSpots,
                             int pricePerHour,
                             int currentVehicleCount = 0,
                             int currentElectricVehicleCount = 0,
                             int currentOccupiedSpots = 0,
                             int currentOccupiedElectricSpots = 0)
        {
            garage = new Garage<Vehicle>(garageId, 
                                         numberOfSpots, 
                                         numberOfElectricSpots, 
                                         pricePerHour, 
                                         currentVehicleCount, 
                                         currentElectricVehicleCount, 
                                         currentOccupiedSpots, 
                                         currentOccupiedElectricSpots);

            garages.Add(garage);
        }

        //**************************************************************************
        // Method to add a vehicle to the garage
        //**************************************************************************
        public void AddVehicle(string vehicleType, 
                               string regNumber, 
                               string brand, 
                               string inputfuelType, 
                               string color, 
                               int numOfSeats = 0, 
                               int numOfWheels = 0)
        {
            garage.AddVehicle(vehicleType, 
                              regNumber, 
                              brand, 
                              inputfuelType, 
                              color, 
                              numOfSeats, 
                              numOfWheels);
        }

        //**************************************************************************
        // Method to add a vehicle to the garage from a file
        //**************************************************************************
        public void AddVehicleFromFile(int antal)
        {
            garage.AddVehicleFromFile(antal);
        }

        //*************************************************************************************
        // Method reads the vehicles from a garage file and adds it to the recreated garage       
        //**************************************************************************************
        public void ReadVehicleFromFile(StreamReader reader)
        {
            garage.AddVehicleFromFile(reader);
        }

        //**************************************************************************
        // Method to retrieve a vehicle from the garage
        //**************************************************************************
        public bool GetVehicle(string regNumber)
        {
            return garage.GetVehicle(regNumber);
        }

        //**************************************************************************
        // Method to check if a vehicle with the given registration number exists
        // in the garage
        //**************************************************************************
        public bool IsVehicleWithRegNumInGarage(string regNumber)
        {
            return garage.IsVehicleWithRegNumInGarage(regNumber);
        }

        //**************************************************************************
        // Method to list all vehicles in the garage
        //**************************************************************************
        public void ListAllVehicles()
        {
            garage.ListAllVehicles();
        }

        //**************************************************************************
        // Method to search for vehicles in the garage based on search criteria
        //**************************************************************************    
        public void SearchVehicles(SearchCriteria searchCriteria)
        {
            garage.SearchVehicles(searchCriteria);
        }

        //***************************************************************************
        // This method stores the garage information to a file
        //***************************************************************************
        public async Task StoreGarageInfo()
        {
            await garage.StoreGarageInfo();
        }
    }
}
