using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using EnsureThat;
using Persistence.PersistenceFolder;

namespace BusinessLayer
{
    public abstract class CrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IDatabaseContext _context;

        protected CrudRepository(IDatabaseContext context)
        {
            Ensure.That(context).IsNotNull();
            _context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity GetById(Guid id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                return false;
            }

            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();

            return true;
        }
    }
}