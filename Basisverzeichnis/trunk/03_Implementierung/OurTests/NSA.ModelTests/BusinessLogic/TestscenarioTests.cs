using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSA.Model.BusinessLogic;
using System.Collections.Generic;
using System.Net;
using NSA.Model.NetworkComponents;


namespace NSA.ModelTests.BusinessLogic
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RunnableTest()
        {
            NSA.Model.NetworkComponents.Network network = new NSA.Model.NetworkComponents.Network();

            NSA.Model.NetworkComponents.Workstation A = new Model.NetworkComponents.Workstation("A");
            NSA.Model.NetworkComponents.Workstation B = new Model.NetworkComponents.Workstation("B");
            NSA.Model.NetworkComponents.Workstation C = new Model.NetworkComponents.Workstation("C");
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

            network.AddConnection("eth0", "eth0", new Model.NetworkComponents.Connection(A, B));
            network.AddConnection("eth1", "eth0", new Model.NetworkComponents.Connection(B, C));

            Testscenario t = new Testscenario("A|(B,C)|{TTL:64}|TRUE", network);

            List<NSA.Model.BusinessLogic.TestscenarioRunnables.ITestscenarioRunnable> runnables = t.GetRunnables();

            foreach (var runnable in runnables)
            {
                System.Console.WriteLine(runnable.Run().ToString());
            }
        }
    }
}
