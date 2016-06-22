using System;
using System.Windows.Forms;

namespace NSA.View.Controls.PropertyControl.Misc
{
    public partial class Separator : UserControl
    {
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
