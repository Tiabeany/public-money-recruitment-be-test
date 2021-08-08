using System.Collections.Generic;
using VacationRental.Core.Models;

namespace VacationRental.Core.Repositories
{
    public interface IPreparationTimeRepository : IRepository<PreparationTime>
    {
        List<PreparationTime> GetByRentalId(int rentalId);
    }
}
