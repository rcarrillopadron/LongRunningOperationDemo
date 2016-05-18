using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using Contracts;

namespace LongRunningContractImplementation
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class LongRunningImpl : ILongRunningService
    {
        private readonly List<int> _generatedNumbers = new List<int>();
        public void StartProcess()
        {
            var random = new Random(10000);

            bool requestCancel = false;
            const int NUMBER_OF_ITERATIONS = 10;
            for (int i = 0; i < NUMBER_OF_ITERATIONS; i++)
            {
                int n = random.Next();

                Console.WriteLine("New number: {0}", n);
                _generatedNumbers.Add(n);

                ILongRunningCallback callback = OperationContext.Current.GetCallbackChannel<ILongRunningCallback>();
                if (callback != null)
                {
                    try
                    {
                        requestCancel = callback.ReportPercentage(i/NUMBER_OF_ITERATIONS);
                    }
                    catch (CommunicationObjectAbortedException)
                    {
                        requestCancel = true;
                    }
                }

                if (requestCancel)
                    break;

                Thread.Sleep(1000);
            }
        }
    }
}
