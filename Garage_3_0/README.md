
Garage 3.0: 
Garage_3_0 is a console application representing a Garage where it is possible to park and fetch different types of vehicles. 

Its possible to:
- Create a Garage with specified number of parking lots, how many of them has loaing possibility for ev's and a parking fee based on hourly rate.
- At cretion of the Garage it is possible to park a defined number of vehicles (from file Fordon.txt) in the garage.
- Recreate a Garage from a file with the Garage information and the vehicles parked in the garage. Only one garage withId 1 is supported at the moment.
- Add a vehicle to the Garage with specified Type, RegNum, Brand, FuelType, Color, NumSeats and NumWheels.
- Get a vehicle from the Garage based on RegNumber.
- List all vehicles in the Garage.
- List vehicles in the Garage of specified vehicles search criterias (Type, Brand, FuelType, Color, NumberOfSeats, NumberOfWheels).
- Exit the program, the garage information is stored to a file named Garage-{GarageId}.txt.


Code structure:
  - The Program class starts the application by calling Run() in the ConsoleUI class which handles the user interaction
  - The GarageHandler class handles the interaction between the ConsoleUI and the garage, including adding and getting vehicles, listing all vehicles or a search for specific vehicles
	- The Garage class handles the garage specific data, like number of parking lots, occupied spots, and the list of vehicles parked in the garage
	- The Vehicle class and its subclasses (car, mc, bus, boat, airplane) handles vehicle-specific data
  - The Utilities provides common functions, like:
	- IsValidVehicleType()
	- IsValidRegistrationNumber()
	- IsValidGarageId()
	- IsValidFuelType()
	- CalculateParkingFee()
	- PrintSuccessful-/Warning-/ErrorMessage()
