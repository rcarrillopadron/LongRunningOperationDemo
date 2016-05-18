using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Contracts;

namespace LongRunningContractImplementation
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class LongRunningManager : ILongRunningService
    {
        public static List<ILongRunningCallback> ConnectedClients { get; }

        static LongRunningManager()
        {
            ConnectedClients = new List<ILongRunningCallback>();
        }

        public void Connect()
        {
            ILongRunningCallback callbackClient = OperationContext.Current.GetCallbackChannel<ILongRunningCallback>();
            if (callbackClient != null)
            {
                lock (ConnectedClients)
                    if (!ConnectedClients.Contains(callbackClient))
                        ConnectedClients.Add(callbackClient);
            }
        }

        public void Disconnect()
        {
            ILongRunningCallback callbackClient = OperationContext.Current.GetCallbackChannel<ILongRunningCallback>();
            if (callbackClient != null)
            {
                lock (ConnectedClients)
                    if (ConnectedClients.Contains(callbackClient))
                        ConnectedClients.Remove(callbackClient);
            }
        }

        public void StartProcess()
        {
            Task.Run(() =>
            {
                LongRunningHelper.StartProcess();
            });
        }
    }

    public static class LongRunningHelper
    {
        static readonly List<int> _generatedNumbers = new List<int>();
        public static void StartProcess()
        {
            var random = new Random(10000);

            bool requestCancel = false;
            const int NUMBER_OF_ITERATIONS = 100;
            for (int i = 0; i < NUMBER_OF_ITERATIONS; i++)
            {
                int n = random.Next();

                Console.WriteLine("New number: {0}", n);
                _generatedNumbers.Add(n);
                lock (LongRunningManager.ConnectedClients)
                {
                    foreach (ILongRunningCallback callbackClient in LongRunningManager.ConnectedClients)
                    {
                        try
                        {
                            callbackClient.ReportState(new State
                            {
                                Percentage = i/(decimal) NUMBER_OF_ITERATIONS,
                                GeneratedNumbers = _generatedNumbers.Count
                            });
                        }
                        catch (CommunicationException)
                        {
                        }
                    }
                }

//                ILongRunningCallback callback = OperationContext.Current.GetCallbackChannel<ILongRunningCallback>();
//                if (callback != null)
//                {
//                    try
//                    {
//                        requestCancel = callback.ReportPercentage(i / NUMBER_OF_ITERATIONS);
//                    }
//                    catch (CommunicationObjectAbortedException)
//                    {
//                        requestCancel = true;
//                    }
//                }
//
//                if (requestCancel)
//                    break;

                Thread.Sleep(1000);
            }
        }
    }
}
