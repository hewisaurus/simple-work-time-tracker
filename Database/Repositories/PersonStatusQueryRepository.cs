using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Database.Dapper;
using Database.Interfaces.Models;
using Database.Interfaces.Repositories;
using Database.Sql;

namespace Database.Repositories
{
    public class PersonStatusQueryRepository : RepositoryBase, IPersonStatusQueryRepository
    {
        public PersonStatusQueryRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<PersonStatus> GetAsync(int id)
        {
            return await QueryAsync(q => q.QueryFirstOrDefaultAsync<PersonStatus>(PersonStatusSql.GetById, new { id }));
        }

        public async Task<PersonStatus> GetAllAsync()
        {
            return await QueryAsync(q => q.QueryFirstOrDefaultAsync<PersonStatus>(PersonStatusSql.GetAll));
        }
    }
}
