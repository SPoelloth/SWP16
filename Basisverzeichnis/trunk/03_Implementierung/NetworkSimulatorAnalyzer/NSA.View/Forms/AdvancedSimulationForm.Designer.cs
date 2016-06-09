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
            this.sourceLabel = new System.Windows.Forms.Label();
            this.destinationLabel = new System.Windows.Forms.Label();
            this.ttlLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sourceComboBox = new System.Windows.Forms.ComboBox();
            this.targetComboBox = new System.Windows.Forms.ComboBox();
            this.hopCountTextBox = new System.Windows.Forms.TextBox();
            this.animationTimeTextBox = new System.Windows.Forms.TextBox();
            this.resultCombo = new System.Windows.Forms.ComboBox();
            this.startButton = new System.Windows.Forms.Button();
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
            // ttlLabel
            // 
            this.ttlLabel.AutoSize = true;
            this.ttlLabel.Location = new System.Drawing.Point(12, 70);
            this.ttlLabel.Name = "ttlLabel";
            this.ttlLabel.Size = new System.Drawing.Size(132, 13);
            this.ttlLabel.TabIndex = 2;
            this.ttlLabel.Text = "Maximale Anzahl an Hops:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Zeit für Animation pro Hop:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Erwartetes Ergebnis:";
            // 
            // sourceComboBox
            // 
            this.sourceComboBox.FormattingEnabled = true;
            this.sourceComboBox.Location = new System.Drawing.Point(166, 13);
            this.sourceComboBox.Name = "sourceComboBox";
            this.sourceComboBox.Size = new System.Drawing.Size(121, 21);
            this.sourceComboBox.TabIndex = 5;
            this.sourceComboBox.SelectedIndexChanged += new System.EventHandler(this.sourceComboBox_SelectedIndexChanged);
            // 
            // targetComboBox
            // 
            this.targetComboBox.FormattingEnabled = true;
            this.targetComboBox.Location = new System.Drawing.Point(166, 40);
            this.targetComboBox.Name = "targetComboBox";
            this.targetComboBox.Size = new System.Drawing.Size(121, 21);
            this.targetComboBox.TabIndex = 6;
            this.targetComboBox.SelectedIndexChanged += new System.EventHandler(this.targetComboBox_SelectedIndexChanged);
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
            // 
            // animationTimeTextBox
            // 
            this.animationTimeTextBox.Location = new System.Drawing.Point(166, 93);
            this.animationTimeTextBox.Name = "animationTimeTextBox";
            this.animationTimeTextBox.Size = new System.Drawing.Size(121, 20);
            this.animationTimeTextBox.TabIndex = 8;
            this.animationTimeTextBox.Text = "1000 ms";
            this.animationTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.animationTimeTextBox.TextChanged += new System.EventHandler(this.animationTimeTextBox_TextChanged);
            // 
            // resultCombo
            // 
            this.resultCombo.FormattingEnabled = true;
            this.resultCombo.Items.AddRange(new object[] {
            "Bitte Auswählen",
            "Erfolgreich",
            "Timeout",
            "Ziel nicht erreichbar",
            "Hier Ergebnisse aus Sim eintragen"});
            this.resultCombo.Location = new System.Drawing.Point(166, 119);
            this.resultCombo.Name = "resultCombo";
            this.resultCombo.Size = new System.Drawing.Size(121, 21);
            this.resultCombo.TabIndex = 9;
            this.resultCombo.SelectedIndexChanged += new System.EventHandler(this.resultCombo_SelectedIndexChanged);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(216, 150);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 10;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // AdvancedSimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 185);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.resultCombo);
            this.Controls.Add(this.animationTimeTextBox);
            this.Controls.Add(this.hopCountTextBox);
            this.Controls.Add(this.targetComboBox);
            this.Controls.Add(this.sourceComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ttlLabel);
            this.Controls.Add(this.destinationLabel);
            this.Controls.Add(this.sourceLabel);
            this.Name = "AdvancedSimulationForm";
            this.Text = "NSA - Erweiterte Simulation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.Label destinationLabel;
        private System.Windows.Forms.Label ttlLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox sourceComboBox;
        private System.Windows.Forms.ComboBox targetComboBox;
        private System.Windows.Forms.TextBox hopCountTextBox;
        private System.Windows.Forms.TextBox animationTimeTextBox;
        private System.Windows.Forms.ComboBox resultCombo;
        private System.Windows.Forms.Button startButton;
    }
}