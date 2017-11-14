using System;
using System.Collections.Generic;
using System.Text;
using Database.Interfaces.Models.Base;

namespace Database.Interfaces.Models
{
    public class PersonStatus : ModelBase
    {
        public string Name { get; set; }
        public string AppearanceClass { get; set; }
        public bool IsDefault { get; set; }
    }
}
