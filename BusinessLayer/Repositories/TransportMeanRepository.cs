using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.PersistenceFolder;

namespace BusinessLayer.Repositories
{
    public class TransportMeanRepository : CrudRepository<TransportMean>, ITransportMeanRepository
    {
        private readonly IDatabaseContext _context;

        public TransportMeanRepository(IDatabaseContext context) : base(context)
        {
            _context = context;
        }


        public override IEnumerable<TransportMean> GetAll()
        {
            return _context.Set<TransportMean>().Include("Comments").AsNoTracking().ToList();
        }

        public override TransportMean GetById(Guid id)
        {
            //Lazy loading is missing from entity framework core
            //Explicit loading
            return _context.Set<TransportMean>().Include("Comments").AsNoTracking().FirstOrDefault(x => x.Id == id);
        }


        public TransportMean GetByIdentifyingCode(string identifyingCode) //dbset-ul implementeaza IQueryable
        {
            return _context.MeansOfTransport
                .SingleOrDefault(c =>
                    string.Equals(c.IdentifyingCode, identifyingCode, StringComparison.OrdinalIgnoreCase));
        }

        public List<TransportMean> GetMeansOTransportByLineNumber(int lineNumber)
        {
            return _context.Set<TransportMean>().Include("Comments").AsNoTracking()
                .Where(x => x.LineNumber == lineNumber).ToList();
        }

        public void Add(TransportMean transportMean)
        {
            var existingEntity = _context.MeansOfTransport.SingleOrDefault(c =>
                string.Equals(c.IdentifyingCode, transportMean.IdentifyingCode, StringComparison.OrdinalIgnoreCase));

            if (existingEntity == null)
            {
                _context.Set<TransportMean>().Add(transportMean);
                _context.SaveChanges();
            }
        }

        public void Update(TransportMean transportMean)
        {
            _context.Set<TransportMean>().Update(transportMean);
            _context.SaveChanges();
        }
    }
}