using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

using Garage_3_0.Models;
using Garage_3_0.Services;
using static Garage_3_0.Services.Utilities;

namespace Garage_3_0.UI
{
    internal class ConsoleUI : IUI
    {
        //***********************************************************************************
        // Method to run the Garage application which let you create a garage or recreate an
        // existing garage from file. It is possible to park and get vehicles in the garage.
        // The garage is generic and can hold different types of vehicles(Car/Bus/Mc/Boat/Airplane).
        // It is also possible to list all vehicles or a specific type of vehicles in the garage.
        //***********************************************************************************
        public void Run()
        {
            IHandler garageHandler = null;
            bool running = true;

            Console.WriteLine();
            Console.WriteLine("***  Välkommen till Evas GarageApp 3.0  ***");
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");

            while (running)
            {
                Console.WriteLine("1. Skapa ett nytt garage");
                Console.WriteLine("2. Hämta ett befintligt garaget");
                Console.WriteLine("0. Avsluta");
                Console.Write("Välj ett alternativ: ");

                while (garageHandler == null && running)
                {
                    switch (Console.ReadLine()?.Trim())
                    {
                        case "1": // Create a new garage
                            if ((garageHandler = CreateGarage()) != null)
                            {
                                PrintSuccessfulMessage($"Nytt garage med ID {garageHandler.GarageId} skapat!");
                                PrintSuccessfulMessage($"Totalt antal parkeringsplatser: {garageHandler.TotalNumberOfParkingLots}" +
                                    $" varav {garageHandler.NumberOfElectricParkingLots} laddplatser ");
                                AddVehiclesFromFile(garageHandler);
                            }
                            else
                            {
                                PrintWarningMessage("Tyvärr kunde inget garage skapas!");
                            }
                            break;
                        case "2": // Recreate an existing garage from file
                            Console.WriteLine("Ange garage-ID (only ID 1 supported now):");
                            if (int.TryParse(Console.ReadLine()?.Trim(), out int garageId))
                            {
                                garageHandler = RecreateGarage(garageId);
                                if (garageHandler != null)
                                {
                                    PrintSuccessfulMessage($"***  Garage med ID {garageHandler.GarageId} återställt från fil!  ***" +
                                        $"\nTotalt antal parkeringsplatser/lediga platser: " +
                                        $"{ garageHandler.TotalNumberOfParkingLots}/{ garageHandler.TotalNumberOfParkingLots - garageHandler.CurrentOccupiedSpots}" +
                                        "\nvarav antal laddplatser/lediga laddplatser: " +
                                        $"{garageHandler.NumberOfElectricParkingLots}/{garageHandler.NumberOfElectricParkingLots - garageHandler.CurrentOccupiedElectricSpots} ");
                                }
                            }
                            else
                            {
                                PrintWarningMessage("Ogiltigt garage-ID!");
                            }
                            break;
                        case "0": // Exit the program
                            running = false;
                            break;

                        default:
                            PrintWarningMessage("Ogiltigt val, försök igen!");
                            Console.WriteLine("1. Skapa ett nytt garage");
                            Console.WriteLine("2. Hämta ett befintligt garaget");
                            Console.WriteLine("0. Avsluta");
                            Console.Write("Välj ett alternativ: ");
                            break;
                    }
                }

                while (running)
                {
                    Console.WriteLine();
                    Console.WriteLine("1. Parkera ett fordon i garaget");
                    Console.WriteLine("2. Hämta ut ett fordon ur garaget");
                    Console.WriteLine("3. Lista alla fordon i garaget");
                    Console.WriteLine("4. Lista specifika fordon i garaget");
                    Console.WriteLine("5. Visa antal fordon i garaget");
                    Console.WriteLine("6. Parkera lista av fordon i garaget");
                    Console.WriteLine("0. Avsluta");
                    Console.Write("Välj ett alternativ: ");

                    switch (Console.ReadLine()?.Trim())
                    {
                        case "1": // Park a vehicle into the garage
                            AddVehicle(garageHandler);
                            break;

                        case "2": // Get a vehicle from the garage
                            GetVehicle(garageHandler);
                            break;

                        case "3": // List all vehicles in the garage
                            ListAllVehicles(garageHandler);
                            break;

                        case "4": // Search for a specified vehicles in the garage
                            SearchVehicles(garageHandler);
                            break;

                        case "5": // Show vehicle count in the garage
                            ShowVehiclesCount(garageHandler);
                            break;

                        case "6": // Add list of vehicles into the garage (Fordon.txt)
                            AddVehiclesFromFile(garageHandler);
                            break;

                        case "0": // Write garage information to file and exit the program
                            if (garageHandler != null)
                                garageHandler.StoreGarageInfo();

                            running = false;
                            break;

                        default:
                            PrintWarningMessage("Ogiltigt val, försök igen!");
                            break;
                    }
                }
            }
        }

        //**************************************************************
        // Method to create a new garage with user-defined total number
        // of parking spots and electric spots
        //**************************************************************
        static public IHandler CreateGarage()
        {
            int totalNumberOfSpots = 0;
            int numberOfElectricSpots = 0;
            int pricePerHour = 0;

            try
            {
                Console.WriteLine("Ange totalt antal parkeringsplatser garaget ska ha?");
                if (!int.TryParse(Console.ReadLine(), out totalNumberOfSpots) || totalNumberOfSpots < 1)
                {
                    throw new Exception("Ogiltigt antal parkeringsplatser");
                }

                Console.WriteLine("Hur många av parkeringsplatserna ska vara laddplatser?");
                if (!int.TryParse(Console.ReadLine(), out numberOfElectricSpots))
                {
                    throw new Exception("Ogiltigt antal laddplatser");
                }
                else if (numberOfElectricSpots > totalNumberOfSpots)
                {
                    throw new Exception("Antal laddplatser kan inte vara större än totala antalet platser");
                }

                Console.WriteLine("Ange pris per timme ( 0 - 100 kr), de 2 första timmarna är gratis?");
                if (!int.TryParse(Console.ReadLine(), out pricePerHour))
                {
                    throw new Exception("Ogiltigt pris!");
                }
                else if (pricePerHour > 100 || pricePerHour < 0)
                {
                    throw new Exception("Ogiltigt pris, skall vara mellan ( 0 - 100 kr )!");
                }

                // Creating a new garage
                return new GarageHandler(totalNumberOfSpots, 
                                         numberOfElectricSpots, 
                                         pricePerHour);
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error creating garage: {ex.Message}");
                return null;
            }
        }

        //*******************************************************************
        // Method to recreate a garage from file with user-defined garage ID
        //******************************************************************
        static public IHandler RecreateGarage(int garageId)
        {
            try
            {
                // Implementation for recreating garage from file
                string folder = @"C:\Users\evasa\source\repos\Garage_3_0\Garage_3_0\";
                string filePath = Path.Combine(folder, $"Garage-{garageId}.txt");

                if (!IsValidGarageId(filePath, garageId))
                {
                    return null;
                }

                // Read garage information from file and recreate the garage
                using (StreamReader reader = new StreamReader(filePath))
                {
                    reader.ReadLine(); // Skip first line with garage ID
                    string garageInfo = reader.ReadLine();

                    var cmdArgs = garageInfo.Split();

                    IHandler garageHandler = new GarageHandler(garageId, int.Parse(cmdArgs[0]),
                        int.Parse(cmdArgs[1]), int.Parse(cmdArgs[2]));

                    garageHandler.ReadVehicleFromFile(reader);

                    return garageHandler;
                }
            }
            catch (IOException ex)
            {
                PrintErrorMessage($"IO error while recreating garage: {ex.Message}");
                return null;
            }
            catch (ArgumentException ex)
            {
                PrintErrorMessage($"Format error while recreating garage: {ex.Message}");
                return null;
            }
            catch (FormatException ex)
            {
                PrintErrorMessage($"Format error while recreating garage: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error recreating garage: {ex.Message}");
                return null;
            }
        }

        //**************************************************************
        // Method to park a vehicle in the garage
        //**************************************************************
        static public void AddVehicle(IHandler garageHandler)
        {
            try
            {
                if (garageHandler == null)
                {
                    PrintWarningMessage("Tyvärr finns inget garage tillgängligt nu!");
                    return;
                }

                // Ask for vehicle type and check given type
                Console.WriteLine("Ange typ av fordon (Bil, Mc, Buss, Båt eller Flygplan):");
                string vehicleType = Console.ReadLine().ToLower();
                if (!IsValidVehicleType(vehicleType))
                {
                    PrintWarningMessage("Ogiltigt fordonstyp, försök igen!");

                    Console.WriteLine("Ange typ av fordon (Bil, Mc, Buss, Båt eller Flygplan):");
                    vehicleType = Console.ReadLine().ToLower();
                    if (!IsValidVehicleType(vehicleType))
                    {
                        PrintErrorMessage("Ogiltigt fordonstyp!");
                        return;
                    }
                }

                // Ask for registration number and check given reg number
                Console.WriteLine("Ange registreringsnummer på fordonet:");
                string regNumber = Console.ReadLine();
                if (!IsValidRegistrationNumber(regNumber))
                {
                    PrintWarningMessage("Ogiltigt registeringsnummer, försök igen!");

                    Console.WriteLine("Ange registreringsnummer på fordonet:");
                    regNumber = Console.ReadLine();
                    if (!IsValidRegistrationNumber(regNumber))
                    {
                        PrintErrorMessage("Ogiltigt registeringsnummer!");
                        return;
                    }
                }

                // Check if registration number already parked in the garage
                if (garageHandler.IsVehicleWithRegNumInGarage(regNumber))
                {
                    PrintErrorMessage("Bil med samma reg nummer finns redan i garaget!");
                }

                // Ask for vehichle brand
                Console.WriteLine("Ange vilken tillverkare (Audi, BMW, Cupra, Ford, Volvo, etc.):");
                string brand = Console.ReadLine();

                // Ask for fuel type
                Console.WriteLine("Ange type av bränsle (1:Petrol, 2:Diesel, 3:Electric, 4:Hybrid):");
                string fuelType = Console.ReadLine();

                // Ask for vehichle color
                Console.WriteLine("Ange färg på fordonet:");
                string color = Console.ReadLine();

                // Ask for number of seats and number of wheels depending on vehichle type
                if (vehicleType.ToLower() == "bil" || vehicleType.ToLower() == "båt")
                {
                    Console.WriteLine("Ange antal säten på fordonet:");
                    int numOfSeats = int.Parse(Console.ReadLine());

                    garageHandler.AddVehicle(vehicleType, regNumber, brand, fuelType, color, numOfSeats);
                }
                else if (vehicleType.ToLower() == "buss" || vehicleType.ToLower() == "flygplan")
                {
                    Console.WriteLine("Ange antal säten på fordonet:");
                    int numOfSeats = int.Parse(Console.ReadLine());

                    Console.WriteLine("Ange antal hjul på fordonet:");
                    int numOfWheels = int.Parse(Console.ReadLine());

                    garageHandler.AddVehicle(vehicleType, regNumber, brand, fuelType, color, numOfSeats, numOfWheels);
                }
                else
                {
                    garageHandler.AddVehicle(vehicleType, regNumber, brand, fuelType, color);
                }
            }
            catch (IOException ex)
            {
                PrintErrorMessage($"IO error while getting vehicle: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                PrintErrorMessage($"Format error while getting vehicle: {ex.Message}");
            }
            catch (FormatException ex)
            {
                PrintErrorMessage($"Format error while getting vehicle: {ex.Message}");
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error getting vehicle: {ex.Message}");
            }
        }

        //**************************************************************
        // Method to get a vehicle from the garage
        //**************************************************************
        static public void GetVehicle(IHandler garageHandler)
        {
            try
            {
                if (garageHandler == null)
                {
                    PrintWarningMessage("Tyvärr finns inget garage tillgängligt nu!");
                    return;
                }
                Console.WriteLine("Ange registreringsnummer på fordonet som ska hämtas:");

                string regNumber = Console.ReadLine();
                garageHandler.GetVehicle(regNumber);
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error getting vehicle: {ex.Message}");
            }
        }

        //**************************************************************
        // List all vehicles parked in the garage
        //**************************************************************
        static public void ListAllVehicles(IHandler garageHandler)
        {
            try
            {
                if (garageHandler == null)
                {
                    PrintWarningMessage("Tyvärr finns inget garage tillgängligt nu!");
                    return;
                }

                garageHandler.ListAllVehicles();
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error getting vehicle: {ex.Message}");
            }
        }

        //**************************************************************
        // Show number of vehicles per type parked in the garage
        //**************************************************************
        static public void ShowVehiclesCount(IHandler garageHandler)
        {
            try
            {
                if (garageHandler == null)
                {
                    PrintWarningMessage("Tyvärr finns inget garage tillgängligt nu!");
                    return;
                }

                garageHandler.ShowVehiclesCount();
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error getting vehicle: {ex.Message}");
            }
        }
        
        //******************************************************************
        // List specific vehicles parked in the garage. The search criteria
        // is stored in the class SearchCriteria
        //******************************************************************
        static public void SearchVehicles(IHandler garageHandler)
        {
            if (garageHandler == null)
            {
                PrintWarningMessage("Tyvärr finns inget garage tillgängligt nu!");
                return;
            }

            // Ask for search criteria from the user   
            Console.WriteLine("*** Lista fordon i garaget efter fordons typ/ märke/ bränsle Typ/ färg/ antal säten/ antal hjul! ***");
            Console.WriteLine("1:Ange typ av fordon Car/Bus/Motorcycle/Boat/Airplane (enter to skip):");
            string vehicleType = Console.ReadLine()?.Trim();

            Console.WriteLine("2:Ange märke (enter to skip):");
            string brand = Console.ReadLine()?.Trim();

            Console.WriteLine("2:Ange typ av bränsle bensin/diesel/el/hybrid (enter to skip):");
            string fuelType = Console.ReadLine()?.Trim();

            Console.WriteLine("3:Ange färg (enter to skip):");
            string color = Console.ReadLine()?.Trim();

            Console.WriteLine("5:Ange antal säten (enter to skip):");
            int numOfSeats = int.TryParse(Console.ReadLine()?.Trim(), out int seats) ? seats : 0;

            Console.WriteLine("6:Ange antal hjul (enter to skip):");
            int numOfWheels = int.TryParse(Console.ReadLine()?.Trim(), out int wheels) ? wheels : 0;

            garageHandler.SearchVehicles(new SearchCriteria(vehicleType, brand, fuelType, color, numOfSeats, numOfWheels));
        }

        //**************************************************************
        // Add vehicles from a file to the garage
        //**************************************************************
        static public void AddVehiclesFromFile(IHandler garageHandler)
        {
            if (garageHandler == null)
            {
                PrintWarningMessage("Tyvärr finns inget garage tillgängligt nu!");
                return;
            }

            try
            {
                Console.WriteLine("Lägga till fordon från fil, Ange antal fordon (0 - 20):");
                if (int.TryParse(Console.ReadLine(), out int antal) && antal >= 0 && antal <= 20)
                { 
                    garageHandler.AddVehicleFromFile(antal);
                }
                else
                {
                    PrintWarningMessage("Ogiltigt antal, inga fordon tillagda");

                }
            }
            catch (IOException ex)
            {
                PrintErrorMessage($"IO error while parking vehicles: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                PrintErrorMessage($"Format error while parking vehicles: {ex.Message}");
            }
            catch (FormatException ex)
            {
                PrintErrorMessage($"Format error while parking vehicles: {ex.Message}");
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Exception error while parking vehicles: {ex.Message}");
            }
        }
    }


}
