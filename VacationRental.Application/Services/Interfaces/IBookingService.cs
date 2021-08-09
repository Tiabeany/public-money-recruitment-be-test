using VacationRental.Core.Models;

namespace VacationRental.Application.Services.Interfaces
{
    public interface IBookingService
    {
        Booking Get(int id);
        int Add(Booking booking);
        int Update(Booking booking);
    }
}
