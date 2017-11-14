using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Sql
{
    public static class PersonSql
    {
        public const string GetById = "SELECT * FROM Person WHERE Id = @id";
        public const string GetByEmail = "SELECT * FROM Person WHERE Email = @emailAddress";
        public const string GetClaimsInformationByEmail = "SELECT Id, Firstname, Lastname, Email, '**********' AS Password, Created, Modified FROM Person WHERE Email = @emailAddress";
        public const string GetAll = "SELECT * FROM Person";
    }
}