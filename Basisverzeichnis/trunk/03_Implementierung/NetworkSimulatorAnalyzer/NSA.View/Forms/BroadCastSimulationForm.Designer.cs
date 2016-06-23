namespace NSA.View.Forms {
    partial class BroadcastSimulationForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BroadcastSimulationForm));
            this.sourceLabel = new System.Windows.Forms.Label();
            this.subnetLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sourceComboBox = new System.Windows.Forms.ComboBox();
            this.resultCombo = new System.Windows.Forms.ComboBox();
            this.startButton = new System.Windows.Forms.Button();
            this.textBoxSubnet = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(12, 16);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(92, 13);
            this.sourceLabel.TabIndex = 0;
            this.sourceLabel.Text = "Start-Workstation:";
            // 
            // subnetLabel
            // 
            this.subnetLabel.AutoSize = true;
            this.subnetLabel.Location = new System.Drawing.Point(12, 43);
            this.subnetLabel.Name = "subnetLabel";
            this.subnetLabel.Size = new System.Drawing.Size(69, 13);
            this.subnetLabel.TabIndex = 1;
            this.subnetLabel.Text = "Ziel-Subnetz:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Erwartetes Ergebnis:";
            // 
            // sourceComboBox
            // 
            this.sourceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sourceComboBox.FormattingEnabled = true;
            this.sourceComboBox.Location = new System.Drawing.Point(166, 13);
            this.sourceComboBox.Name = "sourceComboBox";
            this.sourceComboBox.Size = new System.Drawing.Size(121, 21);
            this.sourceComboBox.TabIndex = 0;
            this.sourceComboBox.SelectedIndexChanged += new System.EventHandler(this.sourceComboBox_SelectedIndexChanged);
            // 
            // resultCombo
            // 
            this.resultCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resultCombo.FormattingEnabled = true;
            this.resultCombo.Items.AddRange(new object[] {
            "Erreichbar",
            "Nicht erreichbar"});
            this.resultCombo.Location = new System.Drawing.Point(166, 67);
            this.resultCombo.Name = "resultCombo";
            this.resultCombo.Size = new System.Drawing.Size(121, 21);
            this.resultCombo.TabIndex = 2;
            this.resultCombo.SelectedIndexChanged += new System.EventHandler(this.resultCombo_SelectedIndexChanged);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(212, 94);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // textBoxSubnet
            // 
            this.textBoxSubnet.Location = new System.Drawing.Point(166, 41);
            this.textBoxSubnet.Name = "textBoxSubnet";
            this.textBoxSubnet.Size = new System.Drawing.Size(121, 20);
            this.textBoxSubnet.TabIndex = 1;
            this.textBoxSubnet.TextChanged += new System.EventHandler(this.textBoxSubnet_TextChanged);
            this.textBoxSubnet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSubnet_KeyPress);
            // 
            // BroadcastSimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 124);
            this.Controls.Add(this.textBoxSubnet);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.resultCombo);
            this.Controls.Add(this.sourceComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.subnetLabel);
            this.Controls.Add(this.sourceLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BroadcastSimulationForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "NSA - Erweiterte Simulation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.Label subnetLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox sourceComboBox;
        private System.Windows.Forms.ComboBox resultCombo;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TextBox textBoxSubnet;
    }
}