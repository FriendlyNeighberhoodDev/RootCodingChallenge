using System;
using System.Collections.Generic;
using System.Text;

namespace DriverInformationReport
{
    public class DriverModel
    {
        public string DriverName { get; set; }
        public List<TripModel> DriverTrips { get; set; }

        public DriverModel()
        {
            DriverTrips = new List<TripModel>();
        }

        public double GetTotalMiles()
        {
            var totalMiles = 0.0;
            if (DriverTrips.Count == 0) return totalMiles;

            foreach (var trip in DriverTrips)
            {
                totalMiles += trip.Miles;
            }
            return totalMiles;
        }

        public double GetAverageSpeed()
        {
            var totalMiles = GetTotalMiles();
            var totalTime = GetTotalTime();
            if (totalMiles == 0 ||totalTime ==0)
            return 0.0;
            return totalMiles / totalTime;
        }

        private double GetTotalTime()
        {
            var totalTime = 0.0;
            if (DriverTrips.Count == 0) return totalTime;
            foreach(var trip in DriverTrips)
            {
                var time = trip.EndTime - trip.StartTime;
                totalTime += time.TotalHours;
            }
            return totalTime;
        }
    }

    public class TripModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Miles { get; set; }
    }
}
