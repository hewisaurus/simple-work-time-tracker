using System;
using System.Threading.Tasks;
using Dapper;
using Database.Dapper;
using Database.Interfaces.Models;
using Database.Interfaces.Repositories;
using Database.Sql;

namespace Database.Repositories
{
    public class PersonQueryRepository : RepositoryBase, IPersonQueryRepository
    {
        public PersonQueryRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<Person> GetAsync(int id)
        {
            return await QueryAsync(q => q.QueryFirstOrDefaultAsync<Person>(PersonSql.GetById, new { id }));
        }

        public async Task<Person> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Person> GetByEmailAddressAsync(string emailAddress)
        {
            return await QueryAsync(q => q.QueryFirstOrDefaultAsync<Person>(PersonSql.GetByEmail, new { emailAddress }));
        }

        public async Task<Person> GetDetailsRequiredForClaimsAsync(string emailAddress)
        {
            return await QueryAsync(q => q.QueryFirstOrDefaultAsync<Person>(PersonSql.GetClaimsInformationByEmail, new { emailAddress }));
        }
    }
}
