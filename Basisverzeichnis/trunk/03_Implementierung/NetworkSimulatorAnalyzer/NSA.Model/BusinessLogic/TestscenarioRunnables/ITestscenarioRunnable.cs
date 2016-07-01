using System.Collections.Generic;

namespace NSA.Model.BusinessLogic.TestscenarioRunnables
{
    public interface ITestscenarioRunnable
    {
        /// <summary>
        /// runs all simulations for a given rule
        /// </summary>
        /// <returns>simulations that failed</returns>
        List<Simulation> Run();
    }
}
