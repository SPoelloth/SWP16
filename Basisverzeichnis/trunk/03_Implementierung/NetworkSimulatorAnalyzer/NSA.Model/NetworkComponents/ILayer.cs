using System.Security.Cryptography.X509Certificates;

namespace NSA.Model.NetworkComponents
{
    public interface ILayer
    {
        /// <summary>
        /// Validates the layer instance.
        /// </summary>
        void Validate();
    }
}
