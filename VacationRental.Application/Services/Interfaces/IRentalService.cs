using VacationRental.Core.Models;

namespace VacationRental.Application.Services.Interfaces
{
    public interface IRentalService
    {
        Rental Get(int id);
        int Add(Rental rental);
        int Update(Rental rental);
    }
}
