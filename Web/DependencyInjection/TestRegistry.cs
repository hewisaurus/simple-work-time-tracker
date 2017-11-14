using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleWorkTimeTracker.DITest;
using StructureMap;
using StructureMap.Pipeline;

namespace SimpleWorkTimeTracker.DependencyInjection
{
    public class TestRegistry : Registry
    {
        public TestRegistry()
        {
            For<IMessagingService>().LifecycleIs(Lifecycles.Singleton).Use<StructuremapMessagingService>();
        }
    }
}
