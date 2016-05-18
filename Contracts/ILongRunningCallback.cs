using System.ServiceModel;

namespace Contracts
{
    [ServiceContract]
    public interface ILongRunningCallback
    {
        [OperationContract]
        bool ReportState(State state);
    }
}