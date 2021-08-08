namespace VacationRental.Core.Models
{
    public class BaseEntity
    {
        public int Id { get; private set; }

        public BaseEntity(int id)
        {
            Id = id;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
