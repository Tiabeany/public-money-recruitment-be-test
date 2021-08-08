using System.Collections.Generic;
using VacationRental.Core.Models;

namespace VacationRental.Core.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
        List<Booking> GetByRentalId(int rentalId);
    }
}
