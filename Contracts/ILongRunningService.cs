using System.ServiceModel;

namespace Contracts
{
    [ServiceContract (CallbackContract = typeof(ILongRunningCallback))]
    public interface ILongRunningService
    {
        [OperationContract(IsOneWay = true)]
        void StartProcess();
    }

    [ServiceContract]
    public interface ILongRunningCallback
    {
        [OperationContract]
        bool ReportPercentage(decimal percentage);
    }
}
