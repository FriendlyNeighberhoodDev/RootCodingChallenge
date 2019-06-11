using System;

namespace DriverInformationReport
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] array = { @"C:\temp\TestFile.txt" };
            BusinessLogic.GetDriverInformationReport(array);
        }
    }
}
