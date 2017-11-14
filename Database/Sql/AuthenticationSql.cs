using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Sql
{
    public static class AuthenticationSql
    {
        public const string GetById = "SELECT * FROM Authentication WHERE Id = @id";
        public const string GetByEmail = "SELECT * FROM Authentication WHERE Email = @emailAddress";
        public const string GetClaimsInformationByEmail = "SELECT Id, Firstname, Lastname, Email, '**********' AS Password, Created, Modified FROM Authentication WHERE Email = @emailAddress";
        public const string GetAll = "SELECT * FROM Authentication";
    }
}