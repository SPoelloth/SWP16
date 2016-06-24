using System;

namespace NSA.Model.NetworkComponents
{
    /// <summary>
    /// Class for a connection between two hardwarenodes.
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Gets the start hardwarenode.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public Hardwarenode Start { get; }
        /// <summary>
        /// Gets the end hardwarenode.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public Hardwarenode End { get; }
        /// <summary>
        /// Gets the name (id) of the connection.
        /// Every connection has a unique id.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection" /> class.
        /// </summary>
        /// <param name="Source">The sourcenode.</param>
        /// <param name="Target">The targetnode.</param>
        public Connection(Hardwarenode Source, Hardwarenode Target)
        {
            Start = Source;
            End = Target;
            Name = Guid.NewGuid().ToString("N");
        }

        #region Equality
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="Obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object Obj)
        {
            // If parameter cannot be cast to Connection return false.
            Connection c = Obj as Connection;
            if (c == null) return false;

            // Return true if the fields match (same hardwarenodes):
            return (Start == c.Start && End == c.End) || (Start == c.End && End == c.Start);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="Other">The other.</param>
        /// <returns></returns>
        public bool Equals(Connection Other)
        {
            if (Other == null) return false;
            return (Start.Equals(Other.Start) && End.Equals(Other.End)) || (Start.Equals(Other.End) && End.Equals(Other.Start));
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            // auto-generated method
            unchecked
            {
                return ((Start?.GetHashCode() ?? 0) * 397) ^ (End?.GetHashCode() ?? 0);
            }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="A">a.</param>
        /// <param name="B">The b.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator == (Connection A, Connection B)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(A, B)) return true;

            // If not casted to object, the Connection == Operator gets used => endless loop => Stackoverflow
            if ((object) A == null) return false;
            return A.Equals(B);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="A">a.</param>
        /// <param name="B">The b.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Connection A, Connection B)
        {
            // auto-generated method
            return !(A == B);
        }
        #endregion

        /// <summary>
        /// Gets the index of the port.
        /// </summary>
        /// <param name="Node">The node.</param>
        /// <returns>Portindex</returns>
        public int GetPortIndex(Hardwarenode Node)
        {
            return Node.GetPortIndexOfConnection(this);
        }
    }
}
