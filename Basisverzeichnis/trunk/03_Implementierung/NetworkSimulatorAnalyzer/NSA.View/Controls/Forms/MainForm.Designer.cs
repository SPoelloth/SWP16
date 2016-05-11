using NSA.View.Controls.NetworkView;

namespace NSA.View.Controls.Forms
{
  partial class MainForm
  {
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      this.propertyControl1 = new NSA.View.Controls.PropertyControl.PropertyControl();
      this.toolbarControl1 = new NSA.View.Controls.Toolbar.ToolbarControl();
      this.infoControl1 = new NSA.View.Controls.InfoControl.InfoControl();
      this.networkViewControl1 = new NSA.View.Controls.NetworkView.NetworkViewControl();
      this.SuspendLayout();
      // 
      // propertyControl1
      // 
      this.propertyControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.propertyControl1.Location = new System.Drawing.Point(768, 12);
      this.propertyControl1.Name = "propertyControl1";
      this.propertyControl1.Size = new System.Drawing.Size(150, 506);
      this.propertyControl1.TabIndex = 3;
      // 
      // toolbarControl1
      // 
      this.toolbarControl1.AutoSize = true;
      this.toolbarControl1.Location = new System.Drawing.Point(1, 1);
      this.toolbarControl1.Name = "toolbarControl1";
      this.toolbarControl1.Size = new System.Drawing.Size(752, 46);
      this.toolbarControl1.TabIndex = 2;
      // 
      // infoControl1
      // 
      this.infoControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.infoControl1.Location = new System.Drawing.Point(2, 365);
      this.infoControl1.Name = "infoControl1";
      this.infoControl1.Size = new System.Drawing.Size(751, 173);
      this.infoControl1.TabIndex = 1;
      // 
      // networkViewControl1
      // 
      this.networkViewControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.networkViewControl1.Location = new System.Drawing.Point(1, 47);
      this.networkViewControl1.Name = "networkViewControl1";
      this.networkViewControl1.Size = new System.Drawing.Size(752, 312);
      this.networkViewControl1.TabIndex = 0;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(930, 541);
      this.Controls.Add(this.propertyControl1);
      this.Controls.Add(this.toolbarControl1);
      this.Controls.Add(this.infoControl1);
      this.Controls.Add(this.networkViewControl1);
      this.Name = "MainForm";
      this.Text = "Network Simulator and Analyzer";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private NetworkViewControl networkViewControl1;
    private InfoControl.InfoControl infoControl1;
    private Toolbar.ToolbarControl toolbarControl1;
    private PropertyControl.PropertyControl propertyControl1;
  }
}

