using System.Collections.Generic;

namespace NSA.Model.NetworkComponents
{
    class Switch : Hardwarenode
    {
        private List<string> interfaces;

        public Switch(string name) : base(name)
        {
        }
    }
}
