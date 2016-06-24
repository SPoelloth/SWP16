using System;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.Misc
{
    /// <summary>
    /// Control for separating several ConfigControls in <see cref="PropertyControl"/>.
    /// </summary>
    public partial class Separator : UserControl
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Separator()
        {
            InitializeComponent();
            if (Type.GetType("Mono.Runtime") != null)
            {
                Width = 220;
            }
        }
    }
}
