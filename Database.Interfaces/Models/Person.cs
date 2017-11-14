using System;
using Database.Interfaces.Models.Base;

namespace Database.Interfaces.Models
{
    public class Person : ModelBase
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
