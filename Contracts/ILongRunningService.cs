using System.Collections.Generic;
using System.ServiceModel;

namespace Contracts
{
    [ServiceContract (CallbackContract = typeof(ILongRunningCallback))]
    public interface ILongRunningService
    {
        [OperationContract]
        void Connect();

        [OperationContract]
        void Disconnect();

        [OperationContract]
        void StartProcess();
    }

    [ServiceContract]
    public interface ILongRunningCallback
    {
        [OperationContract]
        bool ReportState(State state);
    }

    public class State
    {
        public decimal Percentage { get; set; }
        public int GeneratedNumbers { get; set; }
    }
}
