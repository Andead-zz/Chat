using System;
using System.Linq;
using Andead.Chat.Server.Wcf;

namespace ServiceHost
{
    internal class Program
    {
        private static void Main()
        {
            System.ServiceModel.ServiceHost host = null;
            try
            {
                host = new System.ServiceModel.ServiceHost(typeof(Service));

                // Start
                host.Open();

                Console.WriteLine(
                    $"The service is ready at {host.BaseAddresses.Select(uri => uri.ToString()).Aggregate((s, s1) => s + ", " + s1)}.");
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();
            }
            catch (Exception exception)
            {
                host = null;
                Console.WriteLine("Service can not be started \n\nError Message [" + exception.Message + "]");
            }
            finally
            {
                // Stop
                host?.Close();
            }
        }
    }
}