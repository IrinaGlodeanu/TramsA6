using System;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITransportMeanRepository : ICrudRepository<TransportMean>
    {
        TransportMean GetByIdentifyingCode(String identifyingCode);
    }
}