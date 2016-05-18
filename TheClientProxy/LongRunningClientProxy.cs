using System.ServiceModel;
using Contracts;

namespace TheClientProxy
{
    public class LongRunningDuplexClientProxy : DuplexClientBase<ILongRunningService>, ILongRunningService
    {
        public LongRunningDuplexClientProxy(InstanceContext callbackInstance) : base(callbackInstance)
        {
        }

        public void StartProcess()
        {
            Channel.StartProcess();
        }
    }
}
