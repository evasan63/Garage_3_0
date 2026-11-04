using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Garage_3_0.Services
{
    public class Utilities
    {
        //***********************************************************************************
        // This enum defines the different types of fuel that a vehicle can use.
        //***********************************************************************************
        public enum FuelType
        {
            NotUsed,
            Bensin,
            Diesel,
            El,
            Hybrid
        }


        //*******************************************************************************************
        // This method verifies that the vehicleTipe is valif for this garage
        // ******************************************************************************************
        public static bool IsValidVehicleType(string vehicleType)
        {

            if ( vehicleType == "bil" ||
                 vehicleType == "buss" ||
                 vehicleType == "mc" ||
                 vehicleType == "båt" ||
                 vehicleType == "flygplan")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //*******************************************************************************************
        // This method is used to validate a registration number. It takes a string parameter
        // representing the registration number and returns a boolean indicating whether the    
        // registration number is valid or not. The method uses a regular expression to check if the
        // registration number matches the expected format of three letters followed by two digits
        // and an optional letter or digit at the end. The method also handles exceptions that may
        // occur during the validation process and returns false if an exception is caught.
        //*******************************************************************************************
        public static bool IsValidRegistrationNumber(string regNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(regNumber))
                    throw new ArgumentException("Registration number cannot be null or whitespace.");

                regNumber = regNumber.Replace(" ", "").ToUpperInvariant();

                return Regex.IsMatch(regNumber, @"^[A-Z]{3}[0-9]{2}[0-9A-Z]$");
            }
            catch (ArgumentException ex)
            {
                PrintErrorMessage($"Error validating registration number: {ex.Message}");
                return false;

            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error validating registration number: {ex.Message}");
                return false;

            }
        }


        //*******************************************************************************************
        // This method verifies that the GarageId is valid for this garage.   
        // ******************************************************************************************      
        public static bool IsValidGarageId(string filePath, int garageId)
        {
            if (!File.Exists(filePath))
            {
                PrintErrorMessage($"Garage med ID {garageId} finns inte sparad!");
                return false;
            }
            else
            {
                return true;
            }

            //if (garage != null && garage.GarageId == garageId)
            //{
            //    PrintWarningMessage($"Garage med ID {garageId} är redan öppnat!");
            //    return garage;
            //}

        }

        //*******************************************************************************************
        // This method calculates the parking fee for a vehicle based on the number of hours parked.
        // The method returns a decimal value representing the total parking fee.
        //**//***************************************************************************************
        public static decimal CalculateParkingFee(int hoursParked, decimal hourlyRate)
        {
            decimal baseRate = 0.00m; // Base rate for the first hour

            if (hoursParked <= 2)
            {
                return baseRate;
            }
            else if (hoursParked <= 12)
            {
                return baseRate + (hoursParked - 2) * hourlyRate;
            }
            else
            {
                return baseRate + 12 * hourlyRate;
            }
        }

        //***********************************************************************************
        // This method prints the heading when listing vehicles
        //***********************************************************************************
        public static void PrintHeading()
        {
            Console.WriteLine();
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            Console.WriteLine($"{"Typ",-9} {"RegNummer",-10} {"Märke",-8} {"Bränsle",-10} {"Färg",-8} {"Säten",-6} {"Hjul",-6}");
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");
        }

        //***********************************************************************************
        // This method prints a given successful message to the console
        //***********************************************************************************
        public static void PrintSuccessfulMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        //***********************************************************************************
        // This method prints a given warning message to the console
        //***********************************************************************************
        public static void PrintWarningMessage( string msg )
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        //***********************************************************************************
        // This method prints a given error message to the console
        //***********************************************************************************
        public static void PrintErrorMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
