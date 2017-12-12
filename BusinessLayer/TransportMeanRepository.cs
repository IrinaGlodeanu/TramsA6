using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.PersistenceFolder;

namespace BusinessLayer
{
    public class TransportMeanRepository: CrudRepository<TransportMean>, ITransportMeanRepository
    {
        private readonly IDatabaseContext _context;

        public TransportMeanRepository(IDatabaseContext context) : base(context)
        {
            _context = context;
        }

        public TransportMean GetByIdentifyingCode(string identifyingCode) //dbset-ul implementeaza IQueryable
        {
            return _context.MeansOfTransport
                .SingleOrDefault(c => String.Equals(c.IdentifyingCode,identifyingCode, StringComparison.OrdinalIgnoreCase));
        }

        public void Add(TransportMean transportMean)
        {
            var existingEntity = _context.MeansOfTransport.SingleOrDefault(c =>
                String.Equals(c.IdentifyingCode, transportMean.IdentifyingCode, StringComparison.OrdinalIgnoreCase));

            if(existingEntity == null) { 
                _context.Set<TransportMean>().Add(transportMean);
                _context.SaveChanges();
            }
        }

        public void Update(TransportMean transportMean)
        {
            var existingEntity = _context.MeansOfTransport.SingleOrDefault(c =>
                String.Equals(c.IdentifyingCode, transportMean.IdentifyingCode, StringComparison.OrdinalIgnoreCase));

            if (existingEntity == null)
            {
                _context.Set<TransportMean>().Update(transportMean);
                _context.SaveChanges();
            }
        }
    }
}
