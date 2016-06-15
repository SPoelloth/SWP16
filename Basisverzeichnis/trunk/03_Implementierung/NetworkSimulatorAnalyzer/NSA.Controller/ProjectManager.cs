using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NSA.Controller.ViewControllers;
using NSA.Model.BusinessLogic;
using NSA.View.Forms;
using System.Xml.Serialization;

namespace NSA.Controller
{
    public class ProjectManager
    {
        public Project CurrentProject;
        private List<Testscenario> testscenarios;
        private const string TestscenarioDirectoryName = "Testscenarios";

        public static ProjectManager Instance = new ProjectManager();
        private bool instanceIsFullyCreated;
        /// <summary>
        /// Default Constructor.
        /// </summary>
        private ProjectManager()
        {
            CreateNewProject();
        }

        /// <summary>
        /// Creates a new Project.
        /// </summary>
        public void CreateNewProject()
        {
            CurrentProject = new Project();
            testscenarios = new List<Testscenario>();

            if (instanceIsFullyCreated)
            {
                // Do not call Networkmanager if the instance not fully created yet.
                // (Because Networkmanager would try to access ProjectManager´s Properties)
                NetworkManager.Instance.Reset();
                NetworkViewController.Instance.ClearNodes();
            }

            instanceIsFullyCreated = true;
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
                SavingProcess(CurrentProject.Path);
            }
        }

        /// <summary>
        /// Saves the Project with path selection.
        /// </summary>
        public void SaveAs()
        {
            var saveFileDialog = new SaveFileDialog {Filter = "XML|*.xml"};
            var result = saveFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            var file = saveFileDialog.FileName;
            CurrentProject.Path = file;
            SavingProcess(file);
            // create Directory
            Directory.CreateDirectory(file.Substring(0 ,file.LastIndexOf('\\')) + "\\" + TestscenarioDirectoryName);
        }

        /// <summary>
        /// Processes the saving
        /// </summary>
        private void SavingProcess(string Path)
        {
            /*********************** View ***********************/
            // Locations of Víew Elements
            CurrentProject.NodeLocations = NetworkViewController.Instance.GetAllLocationsWithName();
            /* Alle Verbindungen zwischen Hardwareknoten */
            // Verbindungen aus dem Model holn!

            /*********************** Model ***********************/
            /* Alle Verbindungen zwischen Hardwareknoten */
            //- Alle Eigenschaften der einzelnen Hardwareknoten (sprich Informationen des Models) */
            // --> sind im network im Project
            // network wird vom Networkmanager verarbeitet
            // CurrentProject.Network = aktuelles Network

            WriteToXmlFile(Path, CurrentProject);
        }

        /// <summary>
        /// Loads a Project.
        /// </summary>
        public void LoadProject()
        {
            var openFileDialog = new OpenFileDialog {Filter = "XML|*.xml"};
            var result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            var file = openFileDialog.FileName;
            try
            {
                CurrentProject = ReadFromXmlFile<Project>(file);
                NetworkManager.Instance.Reset();
                CurrentProject.parseProjectViewDataToViewControlls();
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
                    testscenarios.Add(ReadTestscenarioFromTxtFile(file.FullName));
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
        /// Writes the given object instance to an XML file.
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [XmlIgnore] attribute.</para>
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="FilePath">The file path to write the object instance to.</param>
        /// <param name="ObjectToWrite">The object instance to write to the file.</param>
        /// <param name="Append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToXmlFile<T>(string FilePath, T ObjectToWrite, bool Append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(FilePath, Append);
                serializer.Serialize(writer, ObjectToWrite);
            }
            finally
            {
                writer?.Close();
            }
        }

        /// <summary>
        /// Reads an object instance from an XML file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="FilePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the XML file.</returns>
        public static T ReadFromXmlFile<T>(string FilePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(FilePath);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                reader?.Close();
            }
        }

        /// <summary>
        /// Reads a Testscenario instance from a txt file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <param name="FilePath">The file path to read the object instance from.</param>
        /// <returns>Returns a Testscenario  from the txt file.</returns>
        public Testscenario ReadTestscenarioFromTxtFile(string FilePath)
        {
            string text = File.ReadAllText(FilePath);
            var testscenario = new Testscenario(text, CurrentProject.Network);
            return testscenario;
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
            ToolbarController.Instance.Init();
            NetworkViewController.Instance.Initialize();
            InfoController.Instance.Initialize();
        }
    }
}