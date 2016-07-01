using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;
using NSA.Controller.ViewControllers;
using NSA.Model.BusinessLogic;
using NSA.View.Forms;
using NSA.Model.NetworkComponents;
using NSA.Model.NetworkComponents.Layers;

namespace NSA.Controller
{
    public class ProjectManager
    {
        internal Project CurrentProject = new Project();
        private List<Testscenario> testscenarios = new List<Testscenario>();
        private const string SzenarioFolderPart = "Testscenarios";
        private string TestscenarioDirectoryName = "Testscenarios";

        public static ProjectManager Instance = new ProjectManager();

        private void Initialize()
        {
            ClearProject();
        }

        /// <summary>
        /// Clears the Project.
        /// </summary>
        public void CreateNewProject()
        {
            var result = AskSave();
            if (result == DialogResult.Cancel) return;
            ClearProject();
        }

        private void ClearProject()
        {
            foreach (var c in NetworkManager.Instance.GetAllConnections().ToList())
            {
                NetworkManager.Instance.RemoveConnection(c.Name);
            }

            foreach (var h in NetworkManager.Instance.GetAllHardwareNodes().ToList())
            {
                NetworkManager.Instance.RemoveHardwarenode(h.Name);
            }

            InfoController.Instance.ClearInfoControl();

            CurrentProject.Path = null;
        }

        /// <summary>
        /// Saves the Project without path selection if the project has already a path.
        /// Otherwise saveas is called.
        /// </summary>
        public void Save()
        {
            if (CurrentProject.Path == null)
            {
                SaveAs();
            }
            else
            {
                File.WriteAllText(CurrentProject.Path, SerializeProject());
            }
        }

        /// <summary>
        /// Saves the Project with path selection.
        /// </summary>
        public void SaveAs()
        {
            var saveFileDialog = new SaveFileDialog { Filter = "XML|*.xml" };
            var result = saveFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            var file = saveFileDialog.FileName;
            CurrentProject.Path = file;
            File.WriteAllText(CurrentProject.Path, SerializeProject());
            // create Directory
            TestscenarioDirectoryName = $"{Path.GetFileNameWithoutExtension(file)}_{SzenarioFolderPart}";
            Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(file) ?? "", TestscenarioDirectoryName));
        }

        private bool IsProjectEmpty()
        {
            return NetworkManager.Instance.GetAllConnections().Count + NetworkManager.Instance.GetAllHardwareNodes().Count < 1;
        }

        private DialogResult AskSave()
        {
            if (IsProjectEmpty()) return DialogResult.OK;
            if (!File.Exists(CurrentProject.Path)) return DialogResult.OK;
            if (SerializeProject() == File.ReadAllText(CurrentProject.Path)) return DialogResult.OK;

            var result = MessageBox.Show("Willst du das Projekt speichern?", "Projekt ist nicht leer", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes) Save();
            return result;
        }

        private string SerializeProject()
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("Project");

            #region Workstation

            XElement workstationsXML = new XElement("Workstations");

            foreach (var ws in NetworkManager.Instance.GetAllHardwareNodes().OfType<Workstation>())
            {
                var loc = NetworkViewController.Instance.GetLocationOfElementByName(ws.Name) ?? new Point();

                var interfaces = ws.Interfaces;
                XElement interfacesXML = new XElement("Interfaces");
                foreach (var i in interfaces)
                {
                    interfacesXML.Add(new XElement("Interface",
                                      new XAttribute("Name", i.Name),
                                      new XAttribute("IPAddress", i.IpAddress.ToString()),
                                      new XAttribute("SubnetMask", i.Subnetmask.ToString())));
                }

                var routen = ws.GetRoutes();
                XElement routesinterfacesXML = new XElement("Routes");
                foreach (var r in routen)
                {
                    routesinterfacesXML.Add(new XElement("Route",
                                      new XAttribute("Destination", r.Destination.ToString()),
                                      new XAttribute("Gateway", r.Gateway.ToString()),
                                      new XAttribute("SubnetMask", r.Subnetmask.ToString()),
                                      new XAttribute("Iface", r.Iface.Name)));
                }

                var layerstack = ws.Layerstack.GetAllLayers();
                XElement layerstackXML = new XElement("Layerstack");
                for (int i = 0; i < layerstack.Count; i++)
                {
                    var layer = layerstack[i];
                    if (!(layer is CustomLayer)) continue;
                    layerstackXML.Add(new XElement("Layer",
                                      new XAttribute("Index", layer.GetLayerIndex()),
                                      new XAttribute("Name", layer.GetLayerName())));
                }

                var xmlnode = new XElement("Workstation",
                              new XAttribute("Name", ws.Name),
                              new XAttribute("Type", ws.GetType().Name),
                              new XAttribute("LocationX", loc.X),
                              new XAttribute("LocationY", loc.Y));

                if (ws.StandardGateway != null)
                {
                    xmlnode.Add(new XAttribute("DefaultGW", ws.StandardGateway));
                    xmlnode.Add(new XAttribute("DefaultGWPort", ws.StandardGatewayPort.Name));
                }

                xmlnode.Add(interfacesXML);
                xmlnode.Add(routesinterfacesXML);
                xmlnode.Add(layerstackXML);

                workstationsXML.Add(xmlnode);
            }

            #endregion Workstation


            #region Switch

            XElement switchesXML = new XElement("Switches");

            foreach (var sw in NetworkManager.Instance.GetAllHardwareNodes().OfType<Switch>())
            {
                var loc = NetworkViewController.Instance.GetLocationOfElementByName(sw.Name) ?? new Point();

                var interfaces = sw.Interfaces;
                XElement interfacesXML = new XElement("Interfaces");
                foreach (var i in interfaces)
                {
                    interfacesXML.Add(new XElement("Interface", new XAttribute("Name", i)));
                }

                var xmlnode = new XElement("Switch",
                              new XAttribute("Name", sw.Name),
                              new XAttribute("LocationX", loc.X),
                              new XAttribute("LocationY", loc.Y));

                xmlnode.Add(interfacesXML);
                switchesXML.Add(xmlnode);
            }

            #endregion Switch


            #region Connections

            XElement connectionsXML = new XElement("Connections");

            foreach (var c in NetworkManager.Instance.GetAllConnections())
            {
                var xmlcon = new XElement("Connection",
                             new XAttribute("HWNode1", c.Start.Name),
                             new XAttribute("HWNode1Port", c.GetPortIndex(c.Start)),
                             new XAttribute("HWNode2", c.End.Name),
                             new XAttribute("HWNode2Port", c.GetPortIndex(c.End)));


                connectionsXML.Add(xmlcon);
            }

            #endregion Connections

            root.Add(workstationsXML);
            root.Add(switchesXML);
            root.Add(connectionsXML);
            doc.Add(root);
            return doc.ToString();
        }

        private void LoadFromFile(string file)
        {
            try
            {
                XDocument document = XDocument.Load(file);
                XElement root = document.Root;
                if (root == null) throw new InvalidDataException();

                #region Workstation

                XElement hwNodesXML = root.Element("Workstations");
                if (hwNodesXML == null) throw new InvalidDataException();

                foreach (var node in hwNodesXML.Elements())
                {
                    var name = node.Attribute("Name").Value;
                    var type = node.Attribute("Type").Value;
                    var x = int.Parse(node.Attribute("LocationX").Value);
                    var y = int.Parse(node.Attribute("LocationY").Value);
                    bool hasDefaultGW = node.Attribute("DefaultGW") != null;

                    Workstation hwNode;
                    if (type == typeof(Router).Name) hwNode = (Workstation)NetworkManager.Instance.CreateHardwareNode(NetworkManager.HardwarenodeType.Router, name);
                    else hwNode = (Workstation)NetworkManager.Instance.CreateHardwareNode(NetworkManager.HardwarenodeType.Workstation, name);

                    if (hasDefaultGW)
                    {
                        var defaultgw = IPAddress.Parse(node.Attribute("DefaultGW").Value);
                        hwNode.StandardGateway = defaultgw;
                    }
                    NetworkViewController.Instance.MoveElementToLocation(name, new Point(x, y));

                    foreach (var i in hwNode.Interfaces.ToList()) NetworkManager.Instance.RemoveInterface(hwNode.Name, i.Name);
                    var interfaceXML = node.Element("Interfaces");
                    if (interfaceXML == null) throw new InvalidDataException();
                    foreach (var xmliface in interfaceXML.Elements())
                    {
                        var iname = xmliface.Attribute("Name").Value;
                        var ip = xmliface.Attribute("IPAddress").Value;
                        var subnet = xmliface.Attribute("SubnetMask").Value;
                        NetworkManager.Instance.AddInterfaceToWorkstation(hwNode.Name, IPAddress.Parse(ip), IPAddress.Parse(subnet), int.Parse(iname.Replace(Interface.NamePrefix, "")));
                    }

                    var routenXML = node.Element("Routes");
                    if (routenXML == null) throw new InvalidDataException();
                    foreach (var route in routenXML.Elements())
                    {
                        var rDest = route.Attribute("Destination").Value;
                        var rgateway = route.Attribute("Gateway").Value;
                        var rsubnet = route.Attribute("SubnetMask").Value;
                        var rinterface = route.Attribute("Iface").Value;

                        hwNode.AddRoute(new Route(IPAddress.Parse(rDest), IPAddress.Parse(rsubnet), IPAddress.Parse(rgateway), hwNode.Interfaces.First(i => i.Name == rinterface)));
                    }

                    var layerstackXML = node.Element("Layerstack");
                    if (layerstackXML != null)
                    {
                        foreach (var layer in layerstackXML.Elements())
                        {
                            var index = int.Parse(layer.Attribute("Index").Value);
                            var layername = layer.Attribute("Name").Value;

                            hwNode.Layerstack.InsertAt(index, new CustomLayer(layername, index));
                        }
                    }

                    if (hasDefaultGW)
                    {
                        var defaultgwport = node.Attribute("DefaultGWPort").Value;
                        hwNode.StandardGatewayPort = hwNode.Interfaces.First(i => i.Name == defaultgwport);
                    }
                }

                #endregion Workstation

                #region Switch
                XElement switchesXML = root.Element("Switches");
                if (switchesXML == null) throw new InvalidDataException();

                foreach (var node in switchesXML.Elements())
                {
                    var name = node.Attribute("Name").Value;

                    var x = int.Parse(node.Attribute("LocationX").Value);
                    var y = int.Parse(node.Attribute("LocationY").Value);

                    Switch sw = (Switch)NetworkManager.Instance.CreateHardwareNode(NetworkManager.HardwarenodeType.Switch, name);
                    NetworkViewController.Instance.MoveElementToLocation(name, new Point(x, y));

                    foreach (var i in sw.Interfaces.ToList()) NetworkManager.Instance.RemoveInterface(sw.Name, i.Name);
                    var interfaceXML = node.Element("Interfaces");
                    if (interfaceXML == null) throw new InvalidDataException();
                    foreach (var iface in interfaceXML.Elements())
                    {
                        NetworkManager.Instance.AddInterfaceToSwitch(sw.Name);
                    }
                }

                #endregion Switch

                #region Connection

                XElement connectionXML = root.Element("Connections");
                if (connectionXML == null) throw new InvalidDataException();

                foreach (var node in connectionXML.Elements())
                {
                    var hwNode1 = node.Attribute("HWNode1").Value;
                    var hwNode1Port = int.Parse(node.Attribute("HWNode1Port").Value);
                    var hwNode2 = node.Attribute("HWNode2").Value;
                    var hwNode2Port = int.Parse(node.Attribute("HWNode2Port").Value);

                    NetworkManager.Instance.CreateConnection(hwNode1, "eth" + hwNode1Port, hwNode2, "eth" + hwNode2Port);
                }

                #endregion Connection

                CurrentProject.Path = file;
                TestscenarioDirectoryName = $"{Path.GetFileNameWithoutExtension(file)}_{SzenarioFolderPart}";
                LoadTestscenarios();
            }
            catch
            {
                ClearProject();
                MessageBox.Show("Laden des Projekts fehlgeschlagen");
            }
        }

        /// <summary>
        /// Loads a Project.
        /// </summary>
        public void LoadProject()
        {
            var result = AskSave();
            if (result == DialogResult.Cancel) return;
            var openFileDialog = new OpenFileDialog { Filter = "XML|*.xml" };
            result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            var file = openFileDialog.FileName;
            try
            {
                ClearProject();
                LoadFromFile(file);
            }
            catch (IOException)
            {
            }
        }

        /// <summary>
        /// Loads the Testscenarios.
        /// </summary>
        public void LoadTestscenarios()
        {
            testscenarios = new List<Testscenario>();
            DirectoryInfo d = new DirectoryInfo(Path.GetDirectoryName(CurrentProject.Path) + "/" + TestscenarioDirectoryName);

            if (!d.Exists) return;

            foreach (var file in d.GetFiles("*.txt"))
            {
                try
                {
                    Testscenario ts = new Testscenario(File.ReadAllText(file.FullName), CurrentProject.Network, file.FullName);
                    testscenarios.Add(ts);
                    InfoController.Instance.AddTestscenarioToScenarioTab(ts);
                }
                catch (IOException)
                {
                }
            }
        }


        /// <summary>
        /// Gets the testscenario by name.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns>Testscenario with given name</returns>
        public Testscenario GetTestscenarioByName(string Name)
        {
            return testscenarios?.FirstOrDefault(Testscenario => Testscenario.FileName.Equals(Name));
        }

        /// <summary>
        /// Creates a Window.
        /// </summary>
        /// <returns>Returns a form.</returns>
        public Form CreateWindow()
        {
            var form = MainForm.Instance;
            form.Shown += Form_Shown;
            form.FormClosing += Form_FormClosing;
            return form;
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = AskSave() == DialogResult.Cancel;
        }

        /// <summary>
        /// Inits the ToolbarController
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="E">The EventArgs.</param>
        private static void Form_Shown(object Sender, EventArgs E)
        {
            Instance.Initialize();
            ToolbarController.Instance.Init();
            NetworkViewController.Instance.Initialize();
            InfoController.Instance.Initialize();
        }
    }
}