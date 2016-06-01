using NSA.Controller.ViewControllers;
using NSA.Model.NetworkComponents;

namespace NSA.Controller
{
    internal class NetworkManager
    {
        #region Singleton

        private static NetworkManager instance = null;
        private static readonly object padlock = new object();

        public static NetworkManager Instance {
            get {
                lock (padlock) {
                    return instance ?? (instance = new NetworkManager());
                }
            }
        }

        #endregion Singleton

        public Network Network { get; }

        // Default constructor:
        NetworkManager()
        {
            createConfigControls();
        }

        // Constructor:
        public NetworkManager(Network network)
        {
            Network = network;
            createConfigControls();
        }

        private void createConfigControls()
        {

        }

        public void HardwarenodeSelected()
        {

        }

        public void InterfaceChanged()
        {

        }

        public void RouteChanged()
        {

        }

        public void GatewayChanged()
        {

        }

        public void CreateWorkstation()
        {
            // name kommt nicht vom UI
            // es muss ein eindeutiger defaultname hier erzeugt werden

            // workstation zum model hinzufügen
            // dem UI sagen das es eine neue hardwarenode gibt
            //NetworkViewController.Instance.AddHardwarenode(...);
        }

        public void CreateSwitch()
        {
            // name kommt nicht vom UI
            // es muss ein eindeutiger defaultname hier erzeugt werden

            // switch zum model hinzufügen
            // dem UI sagen das es eine neue hardwarenode gibt
            //NetworkViewController.Instance.AddHardwarenode(...);
        }

        public void CreateConnection(string start, string end)
        {
            Hardwarenode A = GetHardwarenodeByName(start);
            Hardwarenode B = GetHardwarenodeByName(end);
            Connection newConnection = new Connection(A, B);

            // todo model

            NetworkViewController.Instance.AddConnection(newConnection);
        }

        public void RemoveHardwarenode(string name)
        {

        }

        public void RemoveConnection(string name)
        {

        }

        public Hardwarenode GetHardwarenodeByName(string name)
        {
            return Network?.GetHardwarenodeByName(name);
        }
    }
}
