using System.Collections.Generic;
using VacationRental.Core.Models;

namespace VacationRental.Core.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        int Insert(T entity);
        int Update(T entity);
    }
}
