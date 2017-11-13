using System.Data.Common;

namespace Database.Dapper
{
    public interface IConnectionFactory
    {
        DbConnection Create();
        string GetConnectionString();
    }
}
