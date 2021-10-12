using AirlineProject.Data;
using System;

namespace AirlineProject.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            PassengerDAO dao = new PassengerDAO();

            Passenger passenger1 = new Passenger("Fake Man", "1/05/1984", "FakeDude@fakeemail.com", "Ghost");

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

            int id = 2;
            Console.WriteLine(dao.GetPassenger(id));



        }
    }
}
