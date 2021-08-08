using System.Collections.Generic;
using VacationRental.Core.Models;

namespace VacationRental.Core.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
        IEnumerable<Booking> GetByRentalId(int rentalId);
    }
}
