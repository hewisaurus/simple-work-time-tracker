using System;
using System.Threading.Tasks;
using Dapper;
using Database.Dapper;
using Database.Interfaces.Models;
using Database.Interfaces.Repositories;
using Database.Sql;

namespace Database.Repositories
{
    public class AuthenticationQueryRepository : RepositoryBase, IAuthenticationQueryRepository
    {
        public AuthenticationQueryRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<Authentication> GetAsync(int id)
        {
            return await QueryAsync(q => q.QueryFirstOrDefaultAsync<Authentication>(AuthenticationSql.GetById, new { id }));
        }

        public async Task<Authentication> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Authentication> GetByEmailAddressAsync(string emailAddress)
        {
            return await QueryAsync(q => q.QueryFirstOrDefaultAsync<Authentication>(AuthenticationSql.GetByEmail, new { emailAddress }));
        }

        public async Task<Authentication> GetDetailsRequiredForClaimsAsync(string emailAddress)
        {
            return await QueryAsync(q => q.QueryFirstOrDefaultAsync<Authentication>(AuthenticationSql.GetClaimsInformationByEmail, new { emailAddress }));
        }
    }
}
