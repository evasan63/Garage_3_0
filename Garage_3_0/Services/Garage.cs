
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

using Garage_3_0.Models;
using static Garage_3_0.Services.Utilities;

namespace Garage_3_0.Services
{
    public class Garage<T> : IEnumerable<T> where T : Vehicle
    {
        private int garageId = 0;
        private int totalNumberOfParkingLots = 0;
        private int numberOfElectricParkingLots = 0;
        private int currentVehicleCount;
        private int currentElectricVehicleCount;
        private int currentOccupiedSpots;
        private int currentOccupiedElectricSpots;
        private int pricePerHour;
        private List<Vehicle> vehicles = new();

        //****************************************************************************
        // Constructor for the Garage class, used when creating a new garage with
        // specified number of parking lots, electric parking lots and price per hour.
        //****************************************************************************
        public Garage(int numberOfSpots, int numberOfElectricSpots, int pricePerHour)
        {
            garageId++;
            TotalNumberOfParkingLots = numberOfSpots;
            NumberOfElectricParkingLots = numberOfElectricSpots;
            PricePerHour = pricePerHour;
            CurrentVehicleCount = 0;
            CurrentElectricVehicleCount = 0;
            CurrentOccupiedSpots = 0;
            CurrentOccupiedElectricSpots = 0;
        }

        //****************************************************************************
        // Constructor for the Garage class, used when reading garage information
        // from a file
        //****************************************************************************
        public Garage(int garageId, 
                      int numberOfSpots, 
                      int numberOfElectricSpots, 
                      int pricePerHour, 
                      int currentVehicleCount = 0, 
                      int currentElectricVehicleCount = 0, 
                      int currentOccupiedSpots = 0, 
                      int currentOccupiedElectricSpots = 0)
        {
            this.garageId = garageId;
            TotalNumberOfParkingLots = numberOfSpots;
            NumberOfElectricParkingLots = numberOfElectricSpots;
            PricePerHour = pricePerHour;
            CurrentVehicleCount = currentVehicleCount;
            CurrentElectricVehicleCount = currentElectricVehicleCount;
            CurrentOccupiedSpots = currentOccupiedSpots;
            CurrentOccupiedElectricSpots = currentOccupiedElectricSpots;
        }
        //**********************************************************
        // Getter for GarageId
        //**********************************************************
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        //**********************************************************
        // Getter for GarageId
        //**********************************************************
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //**********************************************************
        // Getter for GarageId
        //**********************************************************
        public int GarageId
        {
            get { return garageId; }
            set { garageId = value; }
        }
        //**********************************************************
        // Getter and setter for TotalNumberOfParkingLots
        //**********************************************************
        public int TotalNumberOfParkingLots
        {
            get { return totalNumberOfParkingLots; }
            set { totalNumberOfParkingLots = value; }
        }       

        //**********************************************************
        // Getter and setter for NumberOfElectricParkingLots
        //**********************************************************
        public int NumberOfElectricParkingLots
        {
            get { return numberOfElectricParkingLots; }
            set { numberOfElectricParkingLots = value; }
        }

        //**********************************************************
        // Getter and setter for PricePerHour
        //**********************************************************
        public int PricePerHour
        {
            get { return pricePerHour; }
            set { pricePerHour = value; }
        }

        //**********************************************************
        // Getter and setter for CurrentVehicleCount
        //**********************************************************
        public int CurrentVehicleCount
        { 
            get { return currentVehicleCount; }
            set { currentVehicleCount = value; }
        }

        //**********************************************************
        // Getter and setter for CurrentVehicleCount
        //**********************************************************
        public int CurrentElectricVehicleCount
        {
            get { return currentElectricVehicleCount; }
            set { currentElectricVehicleCount = value; }
        }

        //**********************************************************
        // Getter and setter for CurrentOccupiedSpots
        //**********************************************************
        public int CurrentOccupiedSpots
        {
            get { return currentOccupiedSpots; }
            set { currentOccupiedSpots = value; }
        }

        //**********************************************************
        // Getter and setter for CurrentOccupiedElectricSpots
        //**********************************************************
        public int CurrentOccupiedElectricSpots
        {
            get { return currentOccupiedElectricSpots; }
            set { currentOccupiedElectricSpots = value; }
        }

        //*************************************************************************************
        // This method reads the vehicles from a garage file and adds it to the recreated garage       
        //**************************************************************************************
        public void AddVehicleFromFile(StreamReader reader)
        {
            //var cmdArgs;
            reader.ReadLine(); // Skip empty line and 4th line with vehicle information header

            string vehicleInfo = reader.ReadLine();

            Console.WriteLine(vehicleInfo);
            var cmdArgs = vehicleInfo.Split();
          
            while ((cmdArgs = reader.ReadLine()?.Split()) != null)
            {
                AddVehicle(cmdArgs[0], cmdArgs[1], cmdArgs[2], cmdArgs[3],
                           cmdArgs[4], int.Parse(cmdArgs[5]), int.Parse(cmdArgs[6]));
            }
        }

        //*************************************************************************************
        // This method is used to park a vehicle of specified type in the garage. It   
        // also increments the currentVehicleCount, currentElectricVehicleCount and 
        // currentOccupiedSpots based on the type of vehicle being parked.        
        //**************************************************************************************
        public void AddVehicle(string vehicleType,
                               string regNumber,
                               string brand, 
                               string inputfuelType, 
                               string color, 
                               int numOfSeats = 0,
                               int numOfWheels = 0 )
        {
            try
            {
                FuelType fuelType = (FuelType)Enum.Parse(typeof(FuelType), inputfuelType, true);

                switch (vehicleType.ToLower())
                {
                    case "bil":  // Implementation for parking a car in the garage
                    case "car":
                        if (CurrentOccupiedSpots < totalNumberOfParkingLots)
                        {
                            if (fuelType == FuelType.El || fuelType == FuelType.Hybrid)
                            {
                                if (NumberOfElectricParkingLots > CurrentOccupiedElectricSpots)
                                {
                                    CurrentOccupiedElectricSpots++;
                                }
                                else
                                {
                                    PrintWarningMessage("Sorry, inga lediga laddplatser!");
                                    return;
                                }
                            }

                            vehicles.Add(new Car(regNumber, brand, fuelType, color, numOfSeats));

                            PrintSuccessfulMessage($"Bil med registrerings nummer " +
                                $"{vehicles[CurrentVehicleCount].RegistrationNumber} parkerad!");

                            CurrentVehicleCount++;
                            CurrentOccupiedSpots++;
                        }
                        else
                        {
                            PrintErrorMessage("Sorry Garage is full, cannot add more cars!");
                            return;
                        }
                        break;

                    case "mc":  // Implementation for parking a motorcycle in the garage
                    case "motorcycle": 
                        if (CurrentOccupiedSpots < totalNumberOfParkingLots)
                        {
                            vehicles.Add(new Motorcycle(regNumber, brand, fuelType, color));

                            PrintSuccessfulMessage($"Motorcycel med registrerings nummer " +
                                $"{vehicles[CurrentVehicleCount].RegistrationNumber} parkerad!");

                            CurrentVehicleCount++;
                            CurrentOccupiedSpots++;
                        }
                        else
                        {
                            PrintErrorMessage("Sorry Garage is full, cannot add more motorcycles!");
                            return;
                        }
                        break;

                    case "buss":  // Implementation for parking a bus in the garage
                    case "bus":
                        if (CurrentOccupiedSpots < totalNumberOfParkingLots - 2)
                        {
                            vehicles.Add(new Bus(regNumber, brand, fuelType, color, numOfSeats, numOfWheels));

                            PrintSuccessfulMessage($"Buss med registrerings nummer " +
                                $"{vehicles[CurrentVehicleCount].RegistrationNumber} parkerad!");

                            CurrentVehicleCount++;
                            CurrentOccupiedSpots = CurrentOccupiedSpots + 2;
                        }
                        else
                        {
                            PrintErrorMessage("Sorry Garage is full, cannot add more buses.");
                            return;
                        }
                        break;

                    case "båt":  // Implementation for parking a boat in the garage  
                    case "boat":
                        if (CurrentOccupiedSpots < totalNumberOfParkingLots - 2)
                        {
                            vehicles.Add(new Boat(regNumber, brand, fuelType, color, numOfSeats));

                            PrintSuccessfulMessage($"Båt med registrerings nummer " +
                                $"{vehicles[CurrentVehicleCount].RegistrationNumber} parkerad!");

                            CurrentVehicleCount++;
                            CurrentOccupiedSpots = CurrentOccupiedSpots + 2; ;
                        }
                        else
                        {
                            PrintErrorMessage("Sorry Garage is full, cannot add more boats!");
                            return;
                        }
                        break;
                    case "flygplan":  // Implementation for parking an airplane in the garage
                    case "airplane":
                        if (CurrentOccupiedSpots < totalNumberOfParkingLots - 3)
                        {
                            vehicles.Add(new Airplane(regNumber, brand, fuelType, color, numOfSeats));

                            PrintSuccessfulMessage($"Flygplan med registrerings nummer " +
                                $"{vehicles[CurrentVehicleCount].RegistrationNumber} parkerad!");

                            CurrentVehicleCount++;
                            CurrentOccupiedSpots = CurrentOccupiedSpots + 3;
                        }
                        else
                        {
                            PrintErrorMessage("Sorry, Garage is full, not enough space for an airplanes!");
                            return;
                        }
                        break;

                    default:
                        PrintErrorMessage("Invalid vehicle type. Please enter a valid vehicle type.");
                        return;
                }  
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error while parking a vehicle: {ex.Message}");
                return;
            }    
        }

        //*******************************************************************************************
        // This method is used to retrieve a vehicle from the garage. It takes a string parameter
        // representing the registration number of the vehicle to be retrieved.
        //*******************************************************************************************
        public bool GetVehicle(string regNumber)
        {
            // Implementation for retrieving a vehicle
            try
            {
                if (CurrentVehicleCount == 0)
                {
                    PrintWarningMessage("Garage is empty. No vehicles to retrieve.");
                    return false;
                }

                foreach (var vehicle in vehicles)
                {
                    if (vehicle.RegistrationNumber.Equals(regNumber.Replace(" ", "").ToUpperInvariant(),
                        StringComparison.OrdinalIgnoreCase))
                    {
                        Random random = new Random();
                        int hoursParked = random.Next(1, 24);

                        PrintSuccessfulMessage($"Fordon med registrerings nummer {vehicle.RegistrationNumber} hämtad!");
                        PrintSuccessfulMessage($"Kvitto: {vehicle.RegistrationNumber} parkerad {hoursParked} timmar, " +
                            $"avgift {Utilities.CalculateParkingFee(hoursParked, pricePerHour)} kr!");

                        // Remove the vehicle from the list
                        vehicles.Remove(vehicle);
                        CurrentVehicleCount--;
                        return true;
                    }
                }   

                PrintErrorMessage($"No vehicle found with registration number: {regNumber}");
                return false;
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error retrieving specified vehicle: {ex.Message}");
                return false;
            }
        }

        //**********************************************************************
        // This method is used to list all vehicles parked in the garage.
        //**********************************************************************
        public void ListAllVehicles()
        {
            try
            {
                if (vehicles == null || CurrentVehicleCount == 0)
                {
                    PrintWarningMessage("No vehicles in the garage.");
                    return;
                }
                else
                {
                    PrintHeading();

                    foreach (var vehicle in vehicles)
                    {
                        Console.WriteLine(vehicle.ToString());
                    }

                    Console.WriteLine("-------------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error listing vehicles: {ex.Message}");
            }   
        }

        //**************************************************************
        // Show number of vehicles per type parked in the garage
        //**************************************************************
        public void ShowVehiclesCount()
        {
            try
            {
                Console.WriteLine($"\nTotalt Antal fordon i garaget: {CurrentVehicleCount}");
                Console.WriteLine($"Antal bilar: {vehicles.Count(v => v is Car)}");
                Console.WriteLine($"Antal motorcyklar: {vehicles.Count(v => v is Motorcycle)}");
                Console.WriteLine($"Antal bussar: {vehicles.Count(v => v is Bus)}");
                Console.WriteLine($"Antal båtar: {vehicles.Count(v => v is Boat)}");
                Console.WriteLine($"Antal flygplan: {vehicles.Count(v => v is Airplane)}");
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error showing vehicle count: {ex.Message}");
            }
        }

        //**********************************************************************
        // This method is used to list vehicles with certain type/brand/color etc
        // parked in the garage.
        //**********************************************************************
        public void SearchVehicles(SearchCriteria searchCriteria)
        {
            // Implementation for searching specified vehicles
            try
            {
                if (vehicles == null || vehicles.Count == 0 || CurrentVehicleCount == 0)
                {
                    PrintWarningMessage("No vehicles in the garage.");
                    return;
                }
                else
                {
                    // Print heading for the list of vehicles
                    PrintHeading();

                    // Find vehicles matching the search criteria
                    List<Vehicle> listOfSelectedVehicles = vehicles.Where(vehicle =>
                        ((string.IsNullOrWhiteSpace(searchCriteria.SearchVehicleType) || 
                            vehicle.GetType().Name.Equals(searchCriteria.SearchVehicleType.Trim(), StringComparison.OrdinalIgnoreCase)) &&

                        (string.IsNullOrWhiteSpace(searchCriteria.SearchBrand) ||
                            string.Equals(vehicle.Brand, searchCriteria.SearchBrand.Trim(), StringComparison.OrdinalIgnoreCase)) &&

                        (string.IsNullOrWhiteSpace(searchCriteria.SearchFuelType) ||
                            string.Equals(vehicle.FuelType.ToString(), searchCriteria.SearchFuelType.Trim(), StringComparison.OrdinalIgnoreCase)) &&

                        (string.IsNullOrWhiteSpace(searchCriteria.SearchColor) ||
                            string.Equals(vehicle.Color, searchCriteria.SearchColor.Trim(), StringComparison.OrdinalIgnoreCase)) &&

                        ((searchCriteria.SearchNumOfSeats == 0) || (searchCriteria.SearchNumOfSeats == vehicle.NumberOfSeats)) &&

                        ((searchCriteria.SearchNumOfWheels == 0) || (searchCriteria.SearchNumOfWheels == vehicle.NumberOfWheels))))
                        .ToList();

                    foreach (var vehicle in listOfSelectedVehicles)
                    {
                        // Print the details of each selected vehicle
                        Console.WriteLine(vehicle.ToString());
                    }

                    Console.WriteLine("-------------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error listing vehicles: {ex.Message}");
            }
        }

        //***************************************************************************
        // This method checks if vehichle with given reg number exists in the garage
        //***************************************************************************
        public bool IsVehicleWithRegNumInGarage(string regNumber)
        {
            string regNum = regNumber.Replace(" ", "").ToUpperInvariant();
            bool Found = false;

            // Verify if a vehicle with same reg number alreay exists in the garage
            foreach (var vehicle in vehicles)
            {
                if (regNum == vehicle.RegistrationNumber)
                {
                    Found = true;
                }
            }

            return Found;
        }

        //***************************************************************************
        // This method checks if vehichle with given reg number exists in the garage
        //***************************************************************************
        public void AddVehicleFromFile(int antal)
        {
            string folder = @"C:\Users\evasa\source\repos\Garage_3_0\Garage_3_0\";
            string filePath = Path.Combine(folder, $"fordon.txt");

            if (antal > 0 && antal <= 20)
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    for (int i = 0; i < antal; i++)
                    {
                        string fordon = reader.ReadLine();

                        if (fordon == null)
                            break;

                        var cmdArgs = fordon.Split();

                        // Check if registration number already parked in the garage
                        if (IsVehicleWithRegNumInGarage(cmdArgs[1]))
                        {
                            PrintErrorMessage("Bil med samma reg nummer finns redan i garaget!");
                        }
                        else
                        {
                            if (cmdArgs.Length > 4 && cmdArgs.Length < 8)
                            {
                                switch (cmdArgs[0])
                                {
                                    case "bil":
                                        AddVehicle(cmdArgs[0], cmdArgs[1], cmdArgs[2], cmdArgs[3], cmdArgs[4], int.Parse(cmdArgs[5]));
                                        break;

                                    case "mc":
                                        AddVehicle(cmdArgs[0], cmdArgs[1], cmdArgs[2], cmdArgs[3], cmdArgs[4]);
                                        break;

                                    case "buss":
                                        AddVehicle(cmdArgs[0], cmdArgs[1], cmdArgs[2], cmdArgs[3], cmdArgs[4], int.Parse(cmdArgs[5]), int.Parse(cmdArgs[6]));
                                        break;

                                    case "båt":
                                        AddVehicle(cmdArgs[0], cmdArgs[1], cmdArgs[2], cmdArgs[3], cmdArgs[4], int.Parse(cmdArgs[5]));
                                        break;

                                    case "flygplan":
                                        AddVehicle(cmdArgs[0], cmdArgs[1], cmdArgs[2], cmdArgs[3], cmdArgs[4], int.Parse(cmdArgs[5]));
                                        break;

                                    default:
                                        PrintWarningMessage("Okänd fordons type!");
                                        break;
                                }
                            }
                        }

                    }
                }
            }
        }

        //***************************************************************************
        // This method stores the garage information to a file
        //***************************************************************************
        public async Task StoreGarageInfo()
        {
            string folder = @"C:\Users\evasa\source\repos\Garage_3_0\Garage_3_0\";
            string filePath = Path.Combine(folder, $"Garage-{GarageId}.txt");

            try
            {
                // Check if folder exists, if not create it
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                // Create garage information lines to be written to the file
                string[] garageLines =
                    [
                       $"Garage-{GarageId}",
                       $"{TotalNumberOfParkingLots} {NumberOfElectricParkingLots} {CurrentVehicleCount} " +
                       $"{CurrentElectricVehicleCount} {CurrentOccupiedSpots} {CurrentOccupiedElectricSpots} " +
                       $"{PricePerHour}",
                       ""
                    ];

                    // Write garage and vehicle information to file
                    await WriteLinesAsync(filePath, garageLines);
 
                    PrintSuccessfulMessage($"Garage-{GarageId} - sparat till filen: {filePath}");
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error writing garage information to file: {ex.Message}");
            }
        }

        //***************************************************************************
        // Method for writing lines to a file asynchronously using StreamWriter
        //***************************************************************************
        private async Task WriteLinesAsync(string filePath, string[] lines)
        {
            await using (StreamWriter writer = new(filePath, append: false))
            {
                foreach (string line in lines)
                {
                    await writer.WriteLineAsync(line);
                }

                if (vehicles.Count == 0)
                {
                    await writer.WriteLineAsync("No vehicles in the garage!");
                }
                else
                {
                    await writer.WriteLineAsync($"Number of vehicles in the garage: {vehicles.Count}");

                    foreach (var vehicle in vehicles)
                    {
                        await writer.WriteLineAsync(vehicle.ToFile());
                    }
                }   
            }
        }
    }
}

