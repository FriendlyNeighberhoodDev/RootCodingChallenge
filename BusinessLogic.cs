using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DriverInformationReport
{
    public static class BusinessLogic
    {
        private static List<DriverModel> Drivers { get; set; }

        public static void GetDriverInformationReport(string[] arguments)
        {
            if (arguments.Length < 1 || string.IsNullOrWhiteSpace(arguments[0]) || !File.Exists(arguments[0]))
            {
                Console.WriteLine("No file found.  Please check your command line argument and try again.");
                Console.ReadLine();
                return;
            }
            Drivers = new List<DriverModel>();
            LoadDriverData(arguments[0]);
            DisplayReport();
        }

        private static void DisplayReport()
        {
            var sortedDrivers = Drivers.OrderByDescending(d => d.GetTotalMiles());
            foreach (var driver in sortedDrivers)
            {
                Console.WriteLine(driver.GetDriverReportData());
            }
            Console.ReadLine();
        }

        private static void LoadDriverData(string fileName)
        {
            using (var fileStream = File.OpenRead(fileName))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    var line = string.Empty;
                    while ((line = streamReader.ReadLine()) != null)
                    {

                        if (line.StartsWith("Driver", StringComparison.OrdinalIgnoreCase))
                        {
                            AddDriver(line);
                        }
                        else if (line.StartsWith("Trip", StringComparison.OrdinalIgnoreCase))
                        {
                            AddTrip(line);
                        }
                    }
                }
            }
        }

        private static void AddDriver(string driverText)
        {
            var driverName = string.Empty;
            try
            {
                var stringList = driverText.Split(' ').ToList();
                driverName = stringList[1];
            }
            catch
            {
                //Input string not fully formed.  Do not attempt to add to Driver List
                return;
            }
            // If driver already exists, do not attempt to add it again
            if (Drivers.Any(d => d.DriverName.Equals(driverName, StringComparison.OrdinalIgnoreCase))) return;
            Drivers.Add(new DriverModel { DriverName = driverName });
        }

        private static void AddTrip(string tripText)
        {
            var driverName = string.Empty;
            TimeSpan startTime;
            TimeSpan endTime;
            var miles = 0.0;
            try
            {
                var stringList = tripText.Split(' ').ToList();
                driverName = stringList[1];
                startTime = TimeSpan.Parse(stringList[2]);
                endTime = TimeSpan.Parse(stringList[3]);
                miles = Double.Parse(stringList[4]);
            }
            catch
            {
                //Input string not fully formed.  Do not attempt to add to Trip List
                return;
            }
            var milesPerHour = miles / (endTime - startTime).TotalHours;
            if (milesPerHour < 5 || milesPerHour > 100) return;
            // Check to see if the driver needs to be added
            if (!Drivers.Any(d => d.DriverName.Equals(driverName, StringComparison.OrdinalIgnoreCase)))
            {
                Drivers.Add(new DriverModel { DriverName = driverName });
            }
            // Find the driver by name
            var driver = Drivers.FirstOrDefault(d => d.DriverName.Equals(driverName, StringComparison.OrdinalIgnoreCase));
            // Add Trip to Driver
            driver.DriverTrips.Add(new TripModel
            {
                StartTime = startTime,
                EndTime = endTime,
                Miles = miles
            });
        }
    }
}

