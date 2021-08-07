using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Core.Models;
using VacationRental.Core.Repositories;

namespace VacationRental.Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly IDictionary<int, Rental> _rentals;
        public RentalRepository(IDictionary<int, Rental> rentals)
        
        {
            _rentals = rentals;
        }

        public Rental Get(int id)
        {
            if (!_rentals.ContainsKey(id))
                throw new ApplicationException("Rental not found");

            return _rentals[id];
        }

        public IEnumerable<Rental> GetAll()
        {
            return _rentals.Select(r => r.Value);
        }

        public int Insert(Rental entity)
        {
            var id = _rentals.Keys.Count + 1;
            entity.Id = id;

            _rentals.Add(id, entity);
            return id;
        }

        public int Update(Rental entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
