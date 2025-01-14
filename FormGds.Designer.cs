namespace GDSExtractor
{
    partial class FormGds
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGds));
            panel1 = new Panel();
            userLabel = new Label();
            panel2 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(userLabel);
            panel1.Location = new Point(7, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(910, 89);
            panel1.TabIndex = 0;
            // 
            // userLabel
            // 
            userLabel.AccessibleDescription = "userLabel";
            userLabel.AccessibleName = "userLabel";
            userLabel.AutoSize = true;
            userLabel.Location = new Point(18, 24);
            userLabel.Name = "userLabel";
            userLabel.Size = new Size(38, 20);
            userLabel.TabIndex = 0;
            userLabel.Text = "User";
            // 
            // panel2
            // 
            panel2.Location = new Point(7, 98);
            panel2.Name = "panel2";
            panel2.Size = new Size(910, 344);
            panel2.TabIndex = 1;
            // 
            // FormGds
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(925, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormGds";
            RightToLeftLayout = true;
            Text = "Extraxtor Gds - TAAP";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label userLabel;
        private Panel panel2;
    }
}