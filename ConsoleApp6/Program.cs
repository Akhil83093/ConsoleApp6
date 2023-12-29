using System;

public class Vehicle
{
    public string VehicleNumber { get; set; }
    public int SlotNumber { get; set; }
    public DateTime InTime { get; set; }
    public DateTime OutTime { get; set; }
}

public class ParkingLot
{
    private int TwoWheelerCapacity;
    private int FourWheelerCapacity;
    private int HeavyVehicleCapacity;
    private List<Vehicle> parkedVehicles;

    public ParkingLot(int twoWheelerCapacity, int fourWheelerCapacity, int heavyVehicleCapacity)
    {
        TwoWheelerCapacity = twoWheelerCapacity;
        FourWheelerCapacity = fourWheelerCapacity;
        HeavyVehicleCapacity = heavyVehicleCapacity;
        parkedVehicles = new List<Vehicle>();
    }

    public void DisplayOccupancyDetails()
    {
        Console.WriteLine($"2 Wheeler Slots Occupied: {CountOccupiedSlots(2)} / {TwoWheelerCapacity}");
        Console.WriteLine($"4 Wheeler Slots Occupied: {CountOccupiedSlots(4)} / {FourWheelerCapacity}");
        Console.WriteLine($"Heavy Vehicle Slots Occupied: {CountOccupiedSlots(6)} / {HeavyVehicleCapacity}");
    }

    public void ParkVehicle()
    {
        Console.WriteLine("Enter vehicle number:");
        string vehicleNumber = Console.ReadLine();

        Console.WriteLine("Enter vehicle type (2 for 2 Wheeler, 4 for 4 Wheeler, 6 for Heavy Vehicle):");
        int vehicleType;
        while (!int.TryParse(Console.ReadLine(), out vehicleType) || (vehicleType != 2 && vehicleType != 4 && vehicleType != 6))
        {
            Console.WriteLine("Invalid input. Enter a valid vehicle type.");
        }

        if (!IsSlotAvailable(vehicleType))
        {
            Console.WriteLine("Sorry, no available slot for the given vehicle type.");
            return;
        }

        int slotNumber = GetNextAvailableSlot(vehicleType);
        Vehicle vehicle = new Vehicle
        {
            VehicleNumber = vehicleNumber,
            SlotNumber = slotNumber,
            InTime = DateTime.Now
        };

        parkedVehicles.Add(vehicle);
        Console.WriteLine($"Vehicle parked successfully. Ticket details:");
        DisplayTicketDetails(vehicle);
    }

    public void UnparkVehicle()
    {
        Console.WriteLine("Enter slot number to unpark:");
        int slotNumber;
        while (!int.TryParse(Console.ReadLine(), out slotNumber) || slotNumber < 1)
        {
            Console.WriteLine("Invalid input. Enter a valid slot number.");
        }

        Vehicle vehicle = parkedVehicles.Find(v => v.SlotNumber == slotNumber);

        if (vehicle != null)
        {
            vehicle.OutTime = DateTime.Now;
            Console.WriteLine("Vehicle unparked successfully. Ticket details:");
            DisplayTicketDetails(vehicle);
            parkedVehicles.Remove(vehicle);
        }
        else
        {
            Console.WriteLine("No vehicle found at the specified slot number.");
        }
    }

    private bool IsSlotAvailable(int vehicleType)
    {
        int occupiedSlots = CountOccupiedSlots(vehicleType);

        switch (vehicleType)
        {
            case 2:
                return occupiedSlots < TwoWheelerCapacity;
            case 4:
                return occupiedSlots < FourWheelerCapacity;
            case 6:
                return occupiedSlots < HeavyVehicleCapacity;
            default:
                return false;
        }
    }

    private int CountOccupiedSlots(int vehicleType)
    {
        return parkedVehicles.Count(v => GetVehicleType(v) == vehicleType);
    }

    private int GetNextAvailableSlot(int vehicleType)
    {
        for (int i = 1; i <= GetCapacity(vehicleType); i++)
        {
            if (!parkedVehicles.Any(v => v.SlotNumber == i && GetVehicleType(v) == vehicleType))
            {
                return i;
            }
        }
        return -1; // No available slot found
    }

    private int GetCapacity(int vehicleType)
    {
        switch (vehicleType)
        {
            case 2:
                return TwoWheelerCapacity;
            case 4:
                return FourWheelerCapacity;
            case 6:
                return HeavyVehicleCapacity;
            default:
                return 0;
        }
    }

    private int GetVehicleType(Vehicle vehicle)
    {
        return vehicle.VehicleNumber.StartsWith("2") ? 2 :
               vehicle.VehicleNumber.StartsWith("4") ? 4 :
               vehicle.VehicleNumber.StartsWith("6") ? 6 : 0;
    }

    private void DisplayTicketDetails(Vehicle vehicle)
    {
        Console.WriteLine($"Vehicle Number: {vehicle.VehicleNumber}");
        Console.WriteLine($"Slot Number: {vehicle.SlotNumber}");
        Console.WriteLine($"In Time: {vehicle.InTime}");
        Console.WriteLine($"Out Time: {vehicle.OutTime}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Parking Lot Simulation!");

        Console.WriteLine("Enter 2 Wheeler capacity:");
        int twoWheelerCapacity;
        while (!int.TryParse(Console.ReadLine(), out twoWheelerCapacity) || twoWheelerCapacity < 1)
        {
            Console.WriteLine("Invalid input. Enter a valid capacity.");
        }

        Console.WriteLine("Enter 4 Wheeler capacity:");
        int fourWheelerCapacity;
        while (!int.TryParse(Console.ReadLine(), out fourWheelerCapacity) || fourWheelerCapacity < 1)
        {
            Console.WriteLine("Invalid input. Enter a valid capacity.");
        }

        Console.WriteLine("Enter Heavy Vehicle capacity:");
        int heavyVehicleCapacity;
        while (!int.TryParse(Console.ReadLine(), out heavyVehicleCapacity) || heavyVehicleCapacity < 1)
        {
            Console.WriteLine("Invalid input. Enter a valid capacity.");
        }

        // Initialize parking lot with specified capacities
        ParkingLot parkingLot = new ParkingLot(twoWheelerCapacity, fourWheelerCapacity, heavyVehicleCapacity);

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Display Parking Lot Occupancy Details");
            Console.WriteLine("2. Park a Vehicle");
            Console.WriteLine("3. Unpark a Vehicle");
            Console.WriteLine("4. Exit");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 4))
            {
                Console.WriteLine("Invalid input. Enter a valid choice.");
            }

            switch (choice)
            {
                case 1:
                    parkingLot.DisplayOccupancyDetails();
                    break;
                case 2:
                    parkingLot.ParkVehicle();
                    break;
                case 3:
                    parkingLot.UnparkVehicle();
                    break;
                case 4:
                    exit = true;
                    break;
            }
        }

        Console.WriteLine("Exiting the Parking Lot Simulation. Thank you!");
    }
}

