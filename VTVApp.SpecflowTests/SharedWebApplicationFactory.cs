using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTVApp.Api;

namespace VTVApp.SpecflowTests
{
    [Binding]
    public static class SharedWebApplicationFactory
    {
        private static readonly CustomWebApplicationFactory<Program> factory = new CustomWebApplicationFactory<Program>();

        public static CustomWebApplicationFactory<Program> Factory => factory;

        [AfterTestRun]
        public static void GlobalTeardown()
        {
            factory.Dispose();
        }
    }

}
