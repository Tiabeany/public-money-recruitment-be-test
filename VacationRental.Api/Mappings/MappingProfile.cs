using AutoMapper;
using VacationRental.Api.Models;
using VacationRental.Core.Models;

namespace VacationRental.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Rental, RentalViewModel>();
            CreateMap<Booking, BookingViewModel > ();
            CreateMap<CalendarDate, CalendarDateViewModel>();
            CreateMap<CalendarBooking, CalendarBookingViewModel>();
            CreateMap<CalendarPreparationTime, CalendarPreparationTimeViewModel>();
            CreateMap<Calendar, CalendarViewModel>();
        }
    }
}
