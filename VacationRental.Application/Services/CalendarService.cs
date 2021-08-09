using System;
using VacationRental.Application.Services.Interfaces;
using VacationRental.Core.Factories;
using VacationRental.Core.Models;
using VacationRental.Core.Repositories;

namespace VacationRental.Application.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRentalRepository _rentalRepository;

        public CalendarService(IBookingRepository bookingRepository, IRentalRepository rentalRepository)
        {
            _bookingRepository = bookingRepository;
            _rentalRepository = rentalRepository;
        }

        public Calendar Get(int rentalId, DateTime start, int nights)
        {
            var allRentalBookings = _bookingRepository.GetByRentalId(rentalId);
            var rental = _rentalRepository.Get(rentalId);
            return CalendarFactory.CreateCalendar(nights, rental, start);
        }
    }
}
