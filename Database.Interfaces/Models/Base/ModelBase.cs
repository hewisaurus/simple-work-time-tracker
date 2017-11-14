using System;

namespace Database.Interfaces.Models.Base
{
    public class ModelBase : Table
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
