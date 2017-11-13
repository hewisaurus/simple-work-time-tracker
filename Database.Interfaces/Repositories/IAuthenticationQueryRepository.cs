using Database.Interfaces.Models;
using Database.Interfaces.Repositories.Base;

namespace Database.Interfaces.Repositories
{
    public interface IAuthenticationQueryRepository : IQueryRepository<Authentication>
    {
    }
}