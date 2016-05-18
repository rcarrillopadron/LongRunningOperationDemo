using System.Collections.Generic;
using System.ServiceModel;
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

        public bool Connect()
        {
            ILongRunningCallback callbackClient = OperationContext.Current.GetCallbackChannel<ILongRunningCallback>();
            if (callbackClient != null)
            {
                lock (ConnectedClients)
                    if (!ConnectedClients.Contains(callbackClient))
                        ConnectedClients.Add(callbackClient);
            }

            return LongRunningHelper.IsRunning;
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
}
