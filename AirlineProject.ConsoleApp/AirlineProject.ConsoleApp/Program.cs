using AirlineProject.Data;
using System;

namespace AirlineProject.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            PassengerDAO dao = new PassengerDAO();
            PilotDAO pilotDAO = new PilotDAO();
            PlaneDAO planeDAO = new PlaneDAO();
            FlightDAO fdao = new FlightDAO();

            Plane plane = planeDAO.GetPlane(1);
            Pilot pilot = pilotDAO.GetPilot(1);

            Flight tempFlight = fdao.GetFlight(1);

            Passenger passenger1 = new Passenger("Fake Man", "1/05/1984", "FakeDude@fakeemail.com", "Ghost", tempFlight);



            //
            //Pilot pilot = new Pilot("Pilot Man", "pilotman@email.com");

            //pilotDAO.AddPilot(pilot);

            int capacity = passenger1.CheckCapacity(passenger1.flight.id);
            int howFull = passenger1.HowFull(passenger1.flight.id);
            Console.WriteLine(howFull);
            Console.WriteLine(capacity);


            //dao.AddPassenger(passenger1);
            //int confirmationNumber = passenger1.GenerateConfirmationNumber();
            //Console.WriteLine(confirmationNumber);
            //Passenger newPassenger = dao.GetPassengerByName(passenger1.name);
            //newPassenger.FinalizeConfirmation(confirmationNumber, newPassenger.id);
            //Console.WriteLine("Success");









            //Console.WriteLine("Enter a Passenger Id: ");
            //string input = Console.ReadLine();
            //int id = int.Parse(input);

            //Console.WriteLine(dao.GetPassenger(id));

            //dao.AddPassenger(passenger1);

            //Console.WriteLine("See all Passengers?: ");
            //string input = Console.ReadLine();
            //if(input.Equals("Y"))
            //{
            //    foreach (var v in dao.GetPassengers())
            //    {
            //        Console.WriteLine(v);
            //    }
            //}

            //int id = 2;
            //Console.WriteLine(dao.GetPassenger(id));



        }
    }
}
