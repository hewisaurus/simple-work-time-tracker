using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Sql
{
    public static class PersonStatusSql
    {
        public const string GetById = "SELECT * FROM PersonStatus WHERE Id = @id";
        public const string GetAll = "SELECT * FROM PersonStatus";
    }
}
