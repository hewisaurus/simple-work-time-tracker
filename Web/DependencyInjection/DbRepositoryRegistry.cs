using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Dapper;
using Database.Interfaces.Repositories;
using Database.Repositories;
using StructureMap;
using StructureMap.Pipeline;

namespace SimpleWorkTimeTracker.DependencyInjection
{
    public class DbRepositoryRegistry : Registry
    {
        public DbRepositoryRegistry(string connectionString)
        {
            For<IConnectionFactory>().Use<MysqlConnectionFactory>().Ctor<string>("connectionString").Is(connectionString);
            For<IPersonQueryRepository>().Use<PersonQueryRepository>();
        }
    }
}
