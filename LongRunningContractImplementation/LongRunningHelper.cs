using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using Contracts;

namespace LongRunningContractImplementation
{
    public static class LongRunningHelper
    {
        public static bool IsRunning { get; set; }

        static readonly List<int> _generatedNumbers = new List<int>();
        public static void StartProcess()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var random = new Random(10000);

                const int NUMBER_OF_ITERATIONS = 100;
                for (int i = 0; i < NUMBER_OF_ITERATIONS; i++)
                {
                    int n = random.Next();

                    Console.WriteLine("New number: {0}", n);
                    _generatedNumbers.Add(n);
                    lock (LongRunningManager.ConnectedClients)
                    {
                        ReportStateToClients(new State
                        {
                            Percentage = i/(decimal) NUMBER_OF_ITERATIONS,
                            GeneratedNumbers = _generatedNumbers.Count
                        });
                    }

                    Thread.Sleep(1000);
                }

                IsRunning = false;
            }
        }

        private static void ReportStateToClients(State state)
        {
            foreach (ILongRunningCallback callbackClient in LongRunningManager.ConnectedClients)
            {
                try
                {
                    callbackClient.ReportState(state);
                }
                catch (CommunicationException)
                {
                }
            }
        }
    }
}