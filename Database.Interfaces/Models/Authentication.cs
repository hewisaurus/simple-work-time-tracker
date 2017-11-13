using System;
using Database.Interfaces.Models.Base;

namespace Database.Interfaces.Models
{
    public class Authentication : ModelBase
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
