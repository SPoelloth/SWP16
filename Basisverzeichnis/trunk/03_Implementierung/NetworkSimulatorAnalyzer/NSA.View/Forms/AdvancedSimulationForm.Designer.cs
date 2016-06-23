namespace NSA.View.Forms
{
    partial class AdvancedSimulationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedSimulationForm));
            this.sourceLabel = new System.Windows.Forms.Label();
            this.destinationLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sourceComboBox = new System.Windows.Forms.ComboBox();
            this.resultCombo = new System.Windows.Forms.ComboBox();
            this.startButton = new System.Windows.Forms.Button();
            this.targetComboBox = new System.Windows.Forms.ComboBox();
            this.ttlLabel = new System.Windows.Forms.Label();
            this.hopCountTextBox = new System.Windows.Forms.TextBox();
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
            // destinationLabel
            // 
            this.destinationLabel.AutoSize = true;
            this.destinationLabel.Location = new System.Drawing.Point(12, 43);
            this.destinationLabel.Name = "destinationLabel";
            this.destinationLabel.Size = new System.Drawing.Size(87, 13);
            this.destinationLabel.TabIndex = 1;
            this.destinationLabel.Text = "Ziel-Workstation:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 97);
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
            this.sourceComboBox.TabIndex = 5;
            this.sourceComboBox.SelectedIndexChanged += new System.EventHandler(this.sourceComboBox_SelectedIndexChanged);
            // 
            // resultCombo
            // 
            this.resultCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resultCombo.FormattingEnabled = true;
            this.resultCombo.Items.AddRange(new object[] {
            "Erreichbar",
            "Nicht erreichbar"});
            this.resultCombo.Location = new System.Drawing.Point(166, 94);
            this.resultCombo.Name = "resultCombo";
            this.resultCombo.Size = new System.Drawing.Size(121, 21);
            this.resultCombo.TabIndex = 9;
            this.resultCombo.SelectedIndexChanged += new System.EventHandler(this.resultCombo_SelectedIndexChanged);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(212, 125);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 10;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // targetComboBox
            // 
            this.targetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetComboBox.FormattingEnabled = true;
            this.targetComboBox.Location = new System.Drawing.Point(166, 40);
            this.targetComboBox.Name = "targetComboBox";
            this.targetComboBox.Size = new System.Drawing.Size(121, 21);
            this.targetComboBox.TabIndex = 6;
            this.targetComboBox.SelectedIndexChanged += new System.EventHandler(this.targetComboBox_SelectedIndexChanged);
            // 
            // ttlLabel
            // 
            this.ttlLabel.AutoSize = true;
            this.ttlLabel.Location = new System.Drawing.Point(12, 70);
            this.ttlLabel.Name = "ttlLabel";
            this.ttlLabel.Size = new System.Drawing.Size(132, 13);
            this.ttlLabel.TabIndex = 2;
            this.ttlLabel.Text = "Maximale Anzahl an Hops:";
            // 
            // hopCountTextBox
            // 
            this.hopCountTextBox.Location = new System.Drawing.Point(166, 67);
            this.hopCountTextBox.Name = "hopCountTextBox";
            this.hopCountTextBox.Size = new System.Drawing.Size(121, 20);
            this.hopCountTextBox.TabIndex = 7;
            this.hopCountTextBox.Text = "255";
            this.hopCountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.hopCountTextBox.TextChanged += new System.EventHandler(this.hopCountTextBox_TextChanged);
            this.hopCountTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IntOnlyInput_TextboxKeyPress);
            // 
            // AdvancedSimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 156);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.resultCombo);
            this.Controls.Add(this.hopCountTextBox);
            this.Controls.Add(this.targetComboBox);
            this.Controls.Add(this.sourceComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ttlLabel);
            this.Controls.Add(this.destinationLabel);
            this.Controls.Add(this.sourceLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedSimulationForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "NSA - Erweiterte Simulation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.Label destinationLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox sourceComboBox;
        private System.Windows.Forms.ComboBox resultCombo;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ComboBox targetComboBox;
        private System.Windows.Forms.Label ttlLabel;
        private System.Windows.Forms.TextBox hopCountTextBox;
    }
}