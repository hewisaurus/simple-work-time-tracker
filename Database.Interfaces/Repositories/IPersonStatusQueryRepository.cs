using System;
using System.Collections.Generic;
using System.Text;
using Database.Interfaces.Models;
using Database.Interfaces.Repositories.Base;

namespace Database.Interfaces.Repositories
{
    public interface IPersonStatusQueryRepository : IQueryRepository<PersonStatus>
    {
    }
}
