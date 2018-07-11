using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using PizzaLocation.Library;
using PizzaLocation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Xml.Serialization;
using System.Security;

namespace PizzaLocation.UI
{
    class Program
    {



        public void SerializeDbData(XmlSerializer locations, LocationRepository locRepo)
        {
            var allLocations = locRepo.GetLocations().ToList();
            try
            {
                using (var stream = new FileStream("C:/Revature/oharak_Project1/PizzaLocation/SerializedData/data.xml", FileMode.Create))
                {
                    locations.Serialize(stream, allLocations);
                }
                Console.WriteLine("Success.");
            }
            catch (SecurityException ex)
            {
                Console.WriteLine($"Error while saving: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error while saving: {ex.Message}");
            }
        }





        private static void Main(string[] args)
        {
            // Get Connection String from appsettings.json
            string dir = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(dir)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration Configuration = builder.Build();

            string connectionString = Configuration.GetConnectionString("PizzaLocationDB");


            var optionsBuilder = new DbContextOptionsBuilder<PizzaLocationDBContext>();
            optionsBuilder.UseSqlServer(connectionString);


            //  CREATING REPOSITORY OBJECTS  /    SERIALIZER OBJECTS FOR EACH CLASS
            //
            // Initialize Repo Objects to be used for reading/writing data to the DB 
            //   based on the input from the user in the console
            var locationRepo = new LocationRepository(new PizzaLocationDBContext(optionsBuilder.Options));
            var LocationSerializer = new XmlSerializer(typeof(List<Location>));

            var userRepo = new UsersRepository(new PizzaLocationDBContext(optionsBuilder.Options));
            var UserSerializer = new XmlSerializer(typeof(List<Users>));

            var orderRepo = new OrderRepository(new PizzaLocationDBContext(optionsBuilder.Options));
            var OrderSerializer = new XmlSerializer(typeof(List<AOrder>));





            //  ---------------------------BEGINNING OF CONSOLE APP FUNCTIONALITY----------------------------------------
            //  User can...
            //     I.) Select if they are a new or returning user
            //       I.2) If they are a new user they can enter their info to create a new user in DB tbl_Users
            //
            //     II.) If they are a returning user, or one who just entered their info they can...
            //       II.2) Make a new order, choosing from the available locations and order items
            //       II.3) Look up info regarding their order history, like...
            //            i) Look up all of the details of their account(orders, cost @ locations, overall cost etc...)
            //            
            //     III.) Look up location specific info (number of orders per location, revenue for each location etc...)

            // Determine first and last Name via the Console
            bool correctInput = false;
            bool newOrNot = false;
            bool serializeData = false;
            string newOrExisting, firstNameInput = "", lastNameInput = "", userFirstName = "", userLastName = "";


            Console.WriteLine("WELCOME TO THE PIZZA ORDERING APP.\n\n\nIf you are a new user enter new" +
            "\n\nIf you are a returning user enter existing");
            newOrExisting = Console.ReadLine();


            while (correctInput == false)
            {
                if (newOrExisting.Contains("new"))
                {
                    Console.WriteLine("Thanks for starting an account with us!\n\nEnter you first name please:");
                    firstNameInput = Console.ReadLine();
                    Console.WriteLine("Enter your last name:");
                    lastNameInput = Console.ReadLine();
                    newOrNot = true;
                    correctInput = true;
                }
                else if (newOrExisting.Contains("existing"))
                {
                    Console.WriteLine("Welcome Back!\n\nEnter your first name please:");
                    userFirstName = Console.ReadLine();
                    Console.WriteLine("\nEnter your last name please:");
                    userLastName = Console.ReadLine();
                    newOrNot = false;
                    correctInput = true;
                }
                else
                {
                    Console.WriteLine("Incorrect Value entered, please enter 'new' for a new user, or 'existing' for" +
                    " an existing user.");
                    newOrExisting = Console.ReadLine();
                    newOrNot = false;
                }
            }



            // Create an instance of the ANewUser Class, passing the correct variables whether the user was new or not
            if (newOrNot == true)
            {
                userRepo.AddUser(firstNameInput, lastNameInput);

                var locations = locationRepo.GetLocations();

                //DataSet dataSet = new DataSet("location");
                //Console.WriteLine("Chose a Location to order from (user the keyword in parentheses next to the location info:");
                //using (DataTableReader reader = dataSet.CreateDataReader())
                //{
                //    do
                //    {
                //        if (!reader.HasRows)
                //        {
                //            Console.WriteLine("No Locations available at this time");
                //        }
                //        else
                //        {
                //            foreach (int i in reader)
                //            {
                //                Console.WriteLine(reader.GetString(i))
                //            }

                //        }
                //        Console.WriteLine("========================");
                //    } while (reader.NextResult());

                //}
            }
        }
    }
}
