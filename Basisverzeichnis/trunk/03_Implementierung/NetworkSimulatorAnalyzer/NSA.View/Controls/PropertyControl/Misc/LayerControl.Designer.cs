namespace NSA.View.Controls.PropertyControl.Misc {
    partial class LayerControl {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.textBoxTag = new System.Windows.Forms.TextBox();
            this.labelTag = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxTag
            // 
            this.textBoxTag.Location = new System.Drawing.Point(45, 25);
            this.textBoxTag.Name = "textBoxTag";
            this.textBoxTag.Size = new System.Drawing.Size(147, 20);
            this.textBoxTag.TabIndex = 1;
            this.textBoxTag.TextChanged += new System.EventHandler(this.textBoxTag_TextChanged);
            // 
            // labelTag
            // 
            this.labelTag.AutoSize = true;
            this.labelTag.Location = new System.Drawing.Point(13, 28);
            this.labelTag.Name = "labelTag";
            this.labelTag.Size = new System.Drawing.Size(26, 13);
            this.labelTag.TabIndex = 2;
            this.labelTag.Text = "Tag";
            // 
            // textBoxName
            // 
            this.textBoxName.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxName.Location = new System.Drawing.Point(3, 2);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(147, 15);
            this.textBoxName.TabIndex = 3;
            this.textBoxName.Text = "LayerName";
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            this.textBoxName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxName_KeyDown);
            // 
            // LayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelTag);
            this.Controls.Add(this.textBoxTag);
            this.Name = "LayerControl";
            this.Size = new System.Drawing.Size(195, 48);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LayerControl_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxTag;
        private System.Windows.Forms.Label labelTag;
        private System.Windows.Forms.TextBox textBoxName;
    }
}
