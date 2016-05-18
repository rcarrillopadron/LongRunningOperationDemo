using System.ServiceModel;
using Contracts;

namespace TheClientProxy
{
    public class LongRunningDuplexClientProxy : DuplexClientBase<ILongRunningService>, ILongRunningService
    {
        public LongRunningDuplexClientProxy(InstanceContext callbackInstance) : base(callbackInstance)
        {
        }

        public void Connect()
        {
            Channel.Connect();
        }

        public void Disconnect()
        {
            Channel.Disconnect();
        }

        public void StartProcess()
        {
            Channel.StartProcess();
        }
    }
}
