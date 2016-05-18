using System;
using System.ServiceModel;
using LongRunningContractImplementation;

namespace TheHostAsAConsoleApp
{
    class Program
    {
        static void Main()
        {
            var host = new ServiceHost(typeof(LongRunningManager));
            host.Open();

            Console.WriteLine("Service started. Press ENTER to exit");
            Console.ReadLine();
        }
    }
}
