﻿using NSA.View.Controls.NetworkView;

namespace NSA.View.Forms
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
            this.propertyControl = new NSA.View.Controls.PropertyControl.PropertyControl();
            this.toolbarControl = new NSA.View.Controls.Toolbar.ToolbarControl();
            this.infoControl = new NSA.View.Controls.InfoControl.InfoControl();
            this.networkViewControl = new NSA.View.Controls.NetworkView.NetworkViewControl();
            this.SuspendLayout();
            // 
            // propertyControl
            // 
            this.propertyControl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.propertyControl.Location = new System.Drawing.Point(764, 64);
            this.propertyControl.Name = "propertyControl";
            this.propertyControl.Size = new System.Drawing.Size(150, 439);
            this.propertyControl.TabIndex = 3;
            // 
            // toolbarControl
            // 
            this.toolbarControl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.toolbarControl.AutoSize = true;
            this.toolbarControl.Location = new System.Drawing.Point(12, 12);
            this.toolbarControl.Name = "toolbarControl";
            this.toolbarControl.Size = new System.Drawing.Size(902, 46);
            this.toolbarControl.TabIndex = 2;
            // 
            // infoControl
            // 
            this.infoControl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.infoControl.Location = new System.Drawing.Point(12, 351);
            this.infoControl.Name = "infoControl";
            this.infoControl.Size = new System.Drawing.Size(746, 173);
            this.infoControl.TabIndex = 1;
            // 
            // networkViewControl
            // 
            this.networkViewControl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.networkViewControl.Location = new System.Drawing.Point(12, 64);
            this.networkViewControl.Name = "networkViewControl";
            this.networkViewControl.Size = new System.Drawing.Size(746, 281);
            this.networkViewControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 527);
            this.Controls.Add(this.propertyControl);
            this.Controls.Add(this.toolbarControl);
            this.Controls.Add(this.infoControl);
            this.Controls.Add(this.networkViewControl);
            this.MinimumSize = new System.Drawing.Size(750, 500);
            this.Name = "MainForm";
            this.Text = "Network Simulator and Analyzer";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private NetworkViewControl networkViewControl;
    private Controls.InfoControl.InfoControl infoControl;
    private Controls.Toolbar.ToolbarControl toolbarControl;
    private Controls.PropertyControl.PropertyControl propertyControl;
    }
}

