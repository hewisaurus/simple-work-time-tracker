using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWorkTimeTracker.DITest
{
    public interface IMessagingService
    {
        string GetMessage();
    }

    public class BuiltInDiMessagingService : IMessagingService
    {
        public string GetMessage()
        {
            return "Hello from built-in dependency injection!";
        }
    }

    public class StructuremapMessagingService : IMessagingService
    {
        public string GetMessage()
        {
            return "Hello from Structuremap!";
        }
    }
}
