using System;
using VacationRental.Application.Services.Interfaces;
using VacationRental.Core.Models;
using VacationRental.Core.Repositories;

namespace VacationRental.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRentalRepository _rentalRepository;

        public BookingService(IBookingRepository bookingRepository, IRentalRepository rentalRepository)
        {
            _bookingRepository = bookingRepository;
            _rentalRepository = rentalRepository;
        }

        public int Add(Booking booking)
        {
            if (booking.Nights <= 0)
                throw new ApplicationException("Nights must be positive");

            booking.Rental = _rentalRepository.Get(booking.RentalId);            
            booking.Rental.AddBooking(booking);

            return _bookingRepository.Insert(booking);
        }

        public Booking Get(int id)
        {
            return _bookingRepository.Get(id);
        }

        public int Update(Booking booking)
        {
            return _bookingRepository.Update(booking);
        }
    }
}
