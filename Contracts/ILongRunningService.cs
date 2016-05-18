using System.ServiceModel;

namespace Contracts
{
    [ServiceContract (CallbackContract = typeof(ILongRunningCallback))]
    public interface ILongRunningService
    {
        [OperationContract]
        bool Connect();

        [OperationContract]
        void Disconnect();

        [OperationContract]
        void StartProcess();
    }
}
