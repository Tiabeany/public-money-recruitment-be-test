using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Core.Models;
using VacationRental.Core.Repositories;

namespace VacationRental.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDictionary<int, Rental> _rentals;
        private readonly IDictionary<int, Booking> _bookings;

        public BookingRepository(IDictionary<int, Rental> rentals, IDictionary<int, Booking> bookings)
        {
            _rentals = rentals;
            _bookings = bookings;
        }

        public Booking Get(int id)
        {
            if (!_bookings.ContainsKey(id))
                throw new ApplicationException("Booking not found");

            return _bookings[id];
        }

        public IEnumerable<Booking> GetAll()
        {
            return _bookings.Select(b => b.Value);
        }

        public List<Booking> GetByRentalId(int rentalId)
        {
            return _bookings.Select(b => b.Value).Where(b => b.RentalId == rentalId).ToList();
        }

        public int Insert(Booking entity)
        {
            var id = _bookings.Keys.Count + 1;
            entity.Id = id;

            _bookings.Add(id, entity);
            return id;
        }

        public int Update(Booking entity)
        {
            throw new NotImplementedException();
        }
    }
}
