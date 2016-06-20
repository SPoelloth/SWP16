using NSA.Model.NetworkComponents.Helper_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA.Model.BusinessLogic.TestscenarioRunnables
{
    public interface ITestscenarioRunnable
    {
        int SimulationCount { get; }
        List<Simulation> Run(); // returns list of failed Simulations
    }
}
