using System.Threading.Tasks;
using Database.Interfaces.Models;
using Database.Interfaces.Repositories.Base;

namespace Database.Interfaces.Repositories
{
    public interface IAuthenticationQueryRepository : IQueryRepository<Authentication>
    {
        Task<Authentication> GetByEmailAddressAsync(string emailAddress);
        Task<Authentication> GetDetailsRequiredForClaimsAsync(string emailAddress);
    }
}