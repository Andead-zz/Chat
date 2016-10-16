﻿using System;
using System.Linq;
using Andead.Chat.Server;
using Andead.Chat.Server.Wcf;
using NLog;

namespace ServiceHost
{
    internal class Program
    {
        private static System.ServiceModel.ServiceHost _host;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private static void Main()
        {
            Console.CancelKeyPress += (sender, args) => CloseHost();

            try
            {
                _host =
                    new System.ServiceModel.ServiceHost(
                        new Service(new ChatService(new CurrentOperationContextChatClientProvider(),
                            new NLogLogger(LogManager.GetCurrentClassLogger(typeof(ChatService))))));

                // Start
                _host.Open();

                Logger.Info(
                    $"The service is ready at {_host.BaseAddresses.Select(uri => uri.ToString()).Aggregate((s, s1) => s + ", " + s1)}.");
                Logger.Info("Press <Enter> to stop the service.\n");
                Console.ReadLine();
            }
            catch (Exception exception)
            {
                Logger.Error("Service can not be started \n\nError Message [" + exception.Message + "]");
            }
            finally
            {
                // Stop
                CloseHost();
            }
        }

        private static void CloseHost()
        {
            _host?.Close();
        }
    }
}