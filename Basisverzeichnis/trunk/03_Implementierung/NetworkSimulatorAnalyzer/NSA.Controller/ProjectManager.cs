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

namespace NSA.Controller
{
    public class ProjectManager
    {
        public Project CurrentProject = new Project();
        private List<Testscenario> testscenarios = new List<Testscenario>();
        private const string TestscenarioDirectoryName = "Testscenarios";

        public static ProjectManager Instance = new ProjectManager();

        private void Initialize()
        {
            CreateNewProject();
        }

        /// <summary>
        /// Clears the Project.
        /// </summary>
        public void CreateNewProject()
        {
            foreach (var c in NetworkManager.Instance.GetAllConnections())
            {
                NetworkManager.Instance.RemoveConnection(c.Name);
            }

            foreach (var h in NetworkManager.Instance.GetAllHardwareNodes())
            {
                NetworkManager.Instance.RemoveHardwarenode(h.Name);
            }
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
                SaveToFile(CurrentProject.Path);
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
            SaveToFile(file);
            // create Directory
            Directory.CreateDirectory(file.Substring(0, file.LastIndexOf('\\')) + "\\" + TestscenarioDirectoryName);
        }

        private void SaveToFile(string file)
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("Project");

            XElement workstationsXML = new XElement("Workstations");

            foreach (var ws in NetworkManager.Instance.GetAllHardwareNodes().OfType<Workstation>())
            {
                var loc = NetworkViewController.Instance.GetLocationOfElementByName(ws.Name) ?? new Point();

                var interfaces = ws.GetInterfaces();

                XElement interfacesXML = new XElement("Interfaces");

                foreach (var i in interfaces)
                {
                    interfacesXML.Add(new XElement("Interface",
                                      new XAttribute("Name", i.Name),
                                      new XAttribute("IPAddress", i.IpAddress.ToString()),
                                      new XAttribute("SubnetMask", i.Subnetmask.ToString())


                        ));
                }

                var xmlnode = new XElement("Workstation",
                              new XAttribute("Name", ws.Name),
                              new XAttribute("LocationX", loc.X),
                              new XAttribute("LocationY", loc.Y)
                              // TODO
                              );
                xmlnode.Add(interfacesXML);

                workstationsXML.Add(xmlnode);
            }
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

            root.Add(workstationsXML);
            root.Add(connectionsXML);
            doc.Add(root);
            doc.Save(file);
        }

        private void LoadFromFile(string file)
        {
            try
            {
                XDocument document = XDocument.Load(file);
                XElement root = document.Root;
                if (root == null) throw new InvalidDataException();

                XElement hwNodesXML = root.Element("Workstations");
                if (hwNodesXML == null) throw new InvalidDataException();

                foreach (var node in hwNodesXML.Elements())
                {
                    var name = node.Attribute("Name").Value;
                    var x = int.Parse(node.Attribute("LocationX").Value);
                    var y = int.Parse(node.Attribute("LocationY").Value);

                    Workstation hwNode = (Workstation)NetworkManager.Instance.CreateHardwareNode(NetworkManager.HardwarenodeType.Workstation);
                    hwNode.Name = name;
                    NetworkViewController.Instance.MoveElementToLocation(name, new Point(x, y));

                    var xElement = node.Element("Interfaces");
                    if (xElement == null) throw new InvalidDataException();
                    foreach (var iface in xElement.Elements())
                    {
                        var iname = iface.Attribute("Name").Value;
                        var ip = iface.Attribute("IPAddress").Value;
                        var subnet = iface.Attribute("SubnetMask").Value;

                        hwNode.SetInterface(iname, IPAddress.Parse(ip), IPAddress.Parse(subnet));
                    }
                }

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
            }
            // ReSharper disable once UnusedVariable
            catch (Exception e)
            {
                CreateNewProject();
                MessageBox.Show("Laden des Projekts fehlgeschlagen");
            }
        }

        /// <summary>
        /// Loads a Project.
        /// </summary>
        public void LoadProject()
        {
            var openFileDialog = new OpenFileDialog { Filter = "XML|*.xml" };
            var result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            var file = openFileDialog.FileName;
            try
            {
                CreateNewProject();
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
            DirectoryInfo d = new DirectoryInfo(CurrentProject.Path + "/" + TestscenarioDirectoryName);

            foreach (var file in d.GetFiles("*.txt"))
            {
                try
                {
                    testscenarios.Add(new Testscenario(File.ReadAllText(file.FullName), CurrentProject.Network));
                }
                catch (IOException)
                {
                }
            }
        }

        /// <summary>
        /// Gets a Testscenario by its id.
        /// </summary>
        /// <param name="Id">The id of the Testscenario.</param>
        /// <returns>Returns the Testscenario.</returns>
        public Testscenario GetTestscenarioById(string Id)
        {
            return testscenarios?.FirstOrDefault(Testscenario => Testscenario.Id.Equals(Id));
        }

        /// <summary>
        /// Creates a Window.
        /// </summary>
        /// <returns>Returns a form.</returns>
        public Form CreateWindow()
        {
            var form = MainForm.Instance;
            form.Shown += Form_Shown;
            return form;
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