using System.Threading.Tasks;
using Database.Interfaces.Models;
using Database.Interfaces.Repositories.Base;

namespace Database.Interfaces.Repositories
{
    public interface IPersonQueryRepository : IQueryRepository<Person>
    {
        Task<Person> GetByEmailAddressAsync(string emailAddress);
        Task<Person> GetDetailsRequiredForClaimsAsync(string emailAddress);
    }
}