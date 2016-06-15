using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSA.Model.BusinessLogic;
using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents;
using NSA.Model.BusinessLogic.TestscenarioRunnables;


namespace NSA.ModelTests.BusinessLogic
{
    [TestClass]
    public class TestscenarioTests
    {
        private Network network;
        [TestInitialize]
        public void SetUp()
        {
            network = new Network();

            Workstation A = new Workstation("A");
            Workstation B = new Workstation("B");
            Workstation C = new Workstation("C");
            A.AddInterface(IPAddress.Parse("192.168.1.1"), IPAddress.Parse("255.255.255.0"));
            B.AddInterface(IPAddress.Parse("192.168.1.2"), IPAddress.Parse("255.255.255.0")); //mittlerer Rechner schnittstelle eins: eth0
            B.AddInterface(IPAddress.Parse("192.168.1.3"), IPAddress.Parse("255.255.255.0")); //mittlerer Rechner schnittstelle eins: eth1
            C.AddInterface(IPAddress.Parse("192.168.1.4"), IPAddress.Parse("255.255.255.0"));
            //Route von A nach C über B
            A.AddRoute(new Route(IPAddress.Parse("192.168.1.4"), IPAddress.Parse("255.255.255.0"), IPAddress.Parse("192.168.1.2"), new Interface(IPAddress.Parse("192.168.1.1"), IPAddress.Parse("255.255.255.0"), 0)));
            //Route von C nach A über B
            A.AddRoute(new Route(IPAddress.Parse("192.168.1.1"), IPAddress.Parse("255.255.255.0"), IPAddress.Parse("192.168.1.3"), new Interface(IPAddress.Parse("192.168.1.4"), IPAddress.Parse("255.255.255.0"), 0)));
            network.AddHardwarenode(A);
            network.AddHardwarenode(B);
            network.AddHardwarenode(C);

            network.AddConnection("eth0", "eth0", new Connection(A, B));
            network.AddConnection("eth1", "eth0", new Connection(B, C));
        }

        [TestMethod]
        public void TestSimulationExpectedResultIsTrue()
        {
            Testscenario t = new Testscenario("A|(B,C)|{TTL:64}|TRUE", network);

            List<ITestscenarioRunnable> runnables = t.GetRunnables();

            foreach (var runnable in runnables)
            {
                Assert.IsTrue(runnable.Run().ErrorID == 0);
            }
        }

        [TestMethod]
        public void TestSimulationShouldFail()
        {
            Testscenario t = new Testscenario("A|(D)|{TTL:64}|TRUE", network);

            List<ITestscenarioRunnable> runnables = t.GetRunnables();

            foreach (var runnable in runnables)
            {
                Assert.IsFalse(runnable.Run().ErrorID == 0);
            }
        }
    }
}
