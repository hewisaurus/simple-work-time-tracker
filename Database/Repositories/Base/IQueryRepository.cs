using System.Threading.Tasks;
using Database.Interfaces.Models.Base;

namespace Database.Interfaces.Repositories.Base
{
    public interface IQueryRepository<T> where T : Table
    {
        Task<T> GetAsync(int id);
        Task<T> GetAllAsync();
    }
}
