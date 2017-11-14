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
            // DB Connection
            For<IConnectionFactory>().Use<MysqlConnectionFactory>().Ctor<string>("connectionString").Is(connectionString);
            // Tables
            For<IPersonQueryRepository>().Use<PersonQueryRepository>();
            For<IPersonStatusQueryRepository>().Use<PersonStatusQueryRepository>();
        }
    }
}
