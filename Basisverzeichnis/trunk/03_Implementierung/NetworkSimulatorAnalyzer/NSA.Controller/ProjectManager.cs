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
        private Project currentProject;
        private List<Testscenario> testscenarios;

        public static ProjectManager Instance = new ProjectManager();
        private ProjectManager()
        {
            CreateNewProject();
        }

        public void CreateNewProject()
        {
            currentProject = new Project();
            testscenarios = new List<Testscenario>();
        }

        public void Save()
        {
            if (currentProject.Path == null)
            {
                SaveAs();
            }
            else
            {
                WriteToXmlFile(currentProject.Path, currentProject);
            }
        }

        public void SaveAs()
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            var file = openFileDialog.FileName;
            WriteToXmlFile(file, currentProject);
        }

        public void LoadTestscenarios()
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            var file = openFileDialog.FileName;
            try
            {
                testscenarios.Add(ReadFromXmlFile<Testscenario>(file));
            }
            catch (IOException)
            {
            }
        }

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

        public Form CreateWindow()
        {
            var form = MainForm.Instance;
            form.Shown += Form_Shown;
            return form;
        }

        private static void Form_Shown(object Sender, System.EventArgs E)
        {
            ToolbarController.Instance.Init();
        }
    }
}