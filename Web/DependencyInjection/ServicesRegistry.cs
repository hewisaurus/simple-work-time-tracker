using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleWorkTimeTracker.Services;
using StructureMap;

namespace SimpleWorkTimeTracker.DependencyInjection
{
    public class ServicesRegistry : Registry
    {
        public ServicesRegistry()
        {
            For<IAuthentication>().Use<Authentication>();
        }
    }
}
