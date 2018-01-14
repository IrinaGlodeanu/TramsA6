using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITransportMeanRepository : ICrudRepository<TransportMean>
    {
        TransportMean GetByIdentifyingCode(string identifyingCode);

        List<TransportMean> GetMeansOTransportByLineNumber(int lineNumber);
    }
}