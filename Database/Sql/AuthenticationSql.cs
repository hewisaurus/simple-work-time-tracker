using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Sql
{
    public class AuthenticationSql
    {
        public const string GetById = "SELECT * FROM Authentication WHERE Id = @id";
        public const string GetAll = "SELECT * FROM Authentication";
    }
}