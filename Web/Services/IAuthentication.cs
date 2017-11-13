using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace SimpleWorkTimeTracker.Services
{
    public interface IAuthentication
    {
        Task<ReturnValue> AuthenticateAsync(string emailAddress, string password);
    }
}
