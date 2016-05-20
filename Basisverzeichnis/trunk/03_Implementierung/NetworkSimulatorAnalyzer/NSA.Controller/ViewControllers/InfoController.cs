using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA.Controller.ViewControllers
{
    public class InfoController
    {
        #region Singleton

        private static InfoController instance = null;
        private static readonly object padlock = new object();

        public static InfoController Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new InfoController();
                    }
                    return instance;
                }
            }
        }

        #endregion Singleton

        InfoController() {
        }
    }
}