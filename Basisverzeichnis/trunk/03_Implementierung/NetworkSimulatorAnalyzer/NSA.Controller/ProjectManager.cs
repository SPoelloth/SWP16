using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NSA.Model.BusinessLogic;

namespace NSA.Controller
{
    internal class ProjectManager
    {
        private Project currentProject;
        private List<Testscenario> testscenarios;

        // Default constructor:
        public ProjectManager()
        {
            currentProject = null;
        }

        // Constructor:
        public ProjectManager(Project CurrentProject, List<Testscenario> Testscenarios)
        {
            currentProject = CurrentProject;
            testscenarios = Testscenarios;
        }

        public void CreateNewProject()
        {
            currentProject = new Project();
            testscenarios = new List<Testscenario>();
        }

        public void CloseProject()
        {
            currentProject = null;
            testscenarios.Clear();
        }

        public void SaveAs(string Path)
        {
            WriteToBinaryFile(Path, currentProject);
        }

        public void LoadTestscenarios()
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            var file = openFileDialog.FileName;
            try
            {
                testscenarios.Add(ReadFromBinaryFile<Testscenario>(file));
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
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the XML file.</typeparam>
        /// <param name="FilePath">The file path to write the object instance to.</param>
        /// <param name="ObjectToWrite">The object instance to write to the XML file.</param>
        /// <param name="Append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToBinaryFile<T>(string FilePath, T ObjectToWrite, bool Append = false)
        {
            using (Stream stream = File.Open(FilePath, Append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, ObjectToWrite);
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the XML.</typeparam>
        /// <param name="FilePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public static T ReadFromBinaryFile<T>(string FilePath)
        {
            using (Stream stream = File.Open(FilePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}