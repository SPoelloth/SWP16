using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSA.Model.NetworkComponents;
using NSA.Model.NetworkComponents.Helper_Classes;

namespace NSA.Model.BusinessLogic.TestscenarioRunnables
{
    class HasInternetTestscenarioRunnable: ITestscenarioRunnable
    {
        private Rule rule;
        private Hardwarenode startNode;
        private Network network;

        public HasInternetTestscenarioRunnable(Rule rule, Hardwarenode startNode, Network n)
        {
            this.rule = rule;
            this.startNode = startNode;
            this.network = n;
        }

        public Result Run()
        {
            //TODO: merge and uncomment the code. GetRouters should be there
            //Simulation sim = new Simulation(Testscenario.SimulationId++, rule.ExpectedResult);
            //List<Hardwarenode> routers = network.GetRouters();
            //foreach (var router in routers)
            //{
            //    Packet p = new Packet(startNode, router, 64, null);
            //    sim.AddPacketSend(p);
            //    if (sim.Execute() == false) return false;
            //}
            return new Result();
        }
    }
}
