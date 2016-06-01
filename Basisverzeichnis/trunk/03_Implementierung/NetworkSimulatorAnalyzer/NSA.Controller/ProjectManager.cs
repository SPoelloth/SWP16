using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NSA.Controller.ViewControllers;
using NSA.Model.BusinessLogic;
using NSA.View.Forms;

namespace NSA.Controller
{
    public class ProjectManager
    {
        public static ProjectManager Instance = new ProjectManager();

        private Project _currentProject;
        private List<Testscenario> _testscenarios;
        
        private ProjectManager()
        {
            _currentProject = new Project();
        }

        

        public void CreateNewProject()
        {
            _currentProject = new Project();
            _testscenarios = new List<Testscenario>();
        }

        public void CloseProject()
        {
            _currentProject = null;
            _testscenarios.Clear();
        }

        public void SaveAs(string path)
        {
            WriteToBinaryFile(path, _currentProject);
        }

        public void LoadTestscenarios()
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            var file = openFileDialog.FileName;
            try
            {
                _testscenarios.Add(ReadFromBinaryFile<Testscenario>(file));
            }
            catch (IOException)
            {
            }
        }

        public Testscenario GetTestscenarioById(string id)
        {
            return _testscenarios?.FirstOrDefault(testscenario => testscenario.Id.Equals(id));
        }

        /// <summary>
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the XML file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the XML file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the XML.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        public Form CreateWindow()
        {
            var form = MainForm.Instance;
            form.Shown += Form_Shown;
            return form;
        }

        private void Form_Shown(object sender, System.EventArgs e)
        {
            ToolbarController.Instance.Init();
        }
    }
}