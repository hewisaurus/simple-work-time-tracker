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
        public AuthenticationQueryRepository(IConnectionFactory connectionFactory, string connectionString) : base(connectionFactory, connectionString)
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
    }
}
