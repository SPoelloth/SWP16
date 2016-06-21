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
        private Workstation A;
        private Workstation B;
        private Workstation C;

        [TestInitialize]
        public void SetUp()
        {
            network = new Network();

            A = new Workstation("A");
            B = new Workstation("B");
            C = new Workstation("C");

            Router X = new Router("X");
            X.AddInterface(IPAddress.Parse("192.168.1.13"), IPAddress.Parse("255.255.255.0"));
            X.IsGateway = true;
            X.AddRoute(new Route(IPAddress.Parse("192.168.1.1"), IPAddress.Parse("255.255.255.0"), IPAddress.Parse("192.168.1.1"), new Interface(IPAddress.Parse("192.168.1.13"), IPAddress.Parse("255.255.255.0"), 0)));

            A.AddInterface(IPAddress.Parse("192.168.1.1"), IPAddress.Parse("255.255.255.0"));
            B.AddInterface(IPAddress.Parse("192.168.1.2"), IPAddress.Parse("255.255.255.0")); //mittlerer Rechner schnittstelle eins: eth0
            B.AddInterface(IPAddress.Parse("192.168.1.3"), IPAddress.Parse("255.255.255.0")); //mittlerer Rechner schnittstelle eins: eth1
            C.AddInterface(IPAddress.Parse("192.168.1.4"), IPAddress.Parse("255.255.255.0"));
            //Route von A nach C über B
            A.AddRoute(new Route(IPAddress.Parse("192.168.1.4"), IPAddress.Parse("255.255.255.0"), IPAddress.Parse("192.168.1.2"), new Interface(IPAddress.Parse("192.168.1.1"), IPAddress.Parse("255.255.255.0"), 0)));
            //Route von C nach A über B
            A.AddRoute(new Route(IPAddress.Parse("192.168.1.1"), IPAddress.Parse("255.255.255.0"), IPAddress.Parse("192.168.1.3"), new Interface(IPAddress.Parse("192.168.1.4"), IPAddress.Parse("255.255.255.0"), 0)));
            A.AddRoute(new Route(IPAddress.Parse("192.168.1.13"), IPAddress.Parse("255.255.255.0"), IPAddress.Parse("192.168.1.13"), new Interface(IPAddress.Parse("192.168.1.1"), IPAddress.Parse("255.255.255.0"), 0)));
            network.AddHardwarenode(A);
            network.AddHardwarenode(B);
            network.AddHardwarenode(C);
            network.AddHardwarenode(X);

            network.AddConnection("eth0", "eth0", new Connection(A, B));
            network.AddConnection("eth1", "eth0", new Connection(B, C));
            network.AddConnection("eth0", "eth9", new Connection(X, A));
        }

        [TestMethod]
        public void TestRuleSimulationEndNodes()
        {
            Rule rule = Rule.Parse("A|(B,C)|{TTL:64}|TRUE", network);
            Assert.IsTrue(rule.EndNodes.Contains(B));
            Assert.IsTrue(rule.EndNodes.Contains(C));
            Assert.IsTrue(rule.EndNodes.Count == 2);
        }

        [TestMethod]
        public void TestSimulationExpectedResultIsTrue()
        {
            Testscenario t = new Testscenario("A|(B,C)|{TTL:64}|TRUE", network, "r");

            List<ITestscenarioRunnable> runnables = t.GetTestscenarioRunnables();

            foreach (var runnable in runnables)
            {
                Assert.IsTrue(runnable.Run().Count == 0);
            }
        }

        [TestMethod]
        public void TestHasInternetIsTrue()
        {
            Testscenario t = new Testscenario("A|HAS_INTERNET|TRUE", network, "r");

            List<ITestscenarioRunnable> runnables = t.GetTestscenarioRunnables();

            foreach (var runnable in runnables)
            {
                Assert.IsTrue(runnable.Run().Count == 0);
            }
        }

        [TestMethod]
        public void TestSimulationShouldFail()
        {
            Testscenario t = new Testscenario("A|(D)|{TTL:64}|TRUE", network, "x");

            List<ITestscenarioRunnable> runnables = t.GetTestscenarioRunnables();

            foreach (var runnable in runnables)
            {
                Assert.IsFalse(runnable.Run().Count == 0);
            }
        }

        [TestMethod]
        public void TestTTLShouldFail() 
        {
            Testscenario t = new Testscenario("A|(B)|{TTL:0}|TRUE", network, "y");

            List<ITestscenarioRunnable> runnables = t.GetTestscenarioRunnables();

            foreach (var runnable in runnables)
            {
                Assert.IsTrue(runnable.Run().Count != 0);
            }
        }

        [TestMethod]
        public void TestAonlyReachesBShouldFail()
        {
            Testscenario t = new Testscenario("A|ONLY(B)|TRUE", network, "y");

            List<ITestscenarioRunnable> runnables = t.GetTestscenarioRunnables();

            foreach (var runnable in runnables)
            {
                Assert.IsFalse(runnable.Run().Count != 0);
            }
        }
    }
}
