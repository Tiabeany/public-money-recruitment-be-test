using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Core.Models;
using VacationRental.Core.Repositories;

namespace VacationRental.Infrastructure.Repositories
{
    public class PreparationTimeRepository : IPreparationTimeRepository
    {
        private readonly IDictionary<int, PreparationTime> _preparationTimes;

        public PreparationTimeRepository(IDictionary<int, PreparationTime> preparationTimes)
        {
            _preparationTimes = preparationTimes;
        }

        public PreparationTime Get(int id)
        {
            if (!_preparationTimes.ContainsKey(id))
                throw new ApplicationException("PreparationTime not found");

            return _preparationTimes[id];
        }

        public IEnumerable<PreparationTime> GetAll()
        {
            return _preparationTimes.Select(p => p.Value);
        }

        public List<PreparationTime> GetByRentalId(int rentalId)
        {
            return _preparationTimes.Select(p => p.Value).Where(p => p.RentalId == rentalId).ToList();
        }

        public int Insert(PreparationTime entity)
        {
            var id = _preparationTimes.Keys.Count + 1;
            entity.Id = id;

            _preparationTimes.Add(id, entity);
            return id;
        }

        public int Update(PreparationTime entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
