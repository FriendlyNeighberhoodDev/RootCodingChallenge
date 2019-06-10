using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DriverInformationReport
{
    public static class BusinessLogic
    {
        private static List<DriverModel> Drivers { get; set; }
        public static void GetDriverInformationReport(string[] arguments)
        {
            try
            {
                var filePath = arguments[0];
                var fileStream = LoadDataFile(filePath);
            }
            catch
            {
                Console.WriteLine("No file found.  Please check your command line argument and try again.");
                Console.ReadLine();
                return;
            }
            DisplayReport();
        }

        private static FileStream LoadDataFile(string filePath)
        {
            return new FileStream(filePath, FileMode.Open);
        }

        private static void DisplayReport()
        {
            foreach(var driver in Drivers)
            {
                Console.WriteLine(GetDriverReportData(driver));
            }
        }

        private static string GetDriverReportData(DriverModel driver)
        {
            return driver.GetTotalMiles() > 0 ? $"{driver.DriverName}: {driver.GetTotalMiles()} miles @ {driver.GetAverageSpeed()} mph" : $"{driver.DriverName}: 0 miles";
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

                    }
                }
            }
        }
    }
}

