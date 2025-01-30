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
            panel1 = new Panel();
            infoConection = new Label();
            userLabel = new Label();
            dataGridDeis = new DataGridView();
            panel2 = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridDeis).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(infoConection);
            panel1.Controls.Add(userLabel);
            panel1.Location = new Point(7, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1172, 89);
            panel1.TabIndex = 0;
            // 
            // infoConection
            // 
            infoConection.AccessibleDescription = "infoConection";
            infoConection.AccessibleName = "infoConection";
            infoConection.AutoSize = true;
            infoConection.Location = new Point(18, 26);
            infoConection.Name = "infoConection";
            infoConection.Size = new Size(102, 20);
            infoConection.TabIndex = 1;
            infoConection.Text = "infoConection";
            // 
            // userLabel
            // 
            userLabel.AccessibleDescription = "userLabel";
            userLabel.AccessibleName = "userLabel";
            userLabel.AutoSize = true;
            userLabel.Location = new Point(18, 6);
            userLabel.Name = "userLabel";
            userLabel.Size = new Size(38, 20);
            userLabel.TabIndex = 0;
            userLabel.Text = "User";
            // 
            // dataGridDeis
            // 
            dataGridDeis.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridDeis.Location = new Point(3, 3);
            dataGridDeis.Name = "dataGridDeis";
            dataGridDeis.RowHeadersWidth = 51;
            dataGridDeis.Size = new Size(1161, 309);
            dataGridDeis.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(dataGridDeis);
            panel2.Location = new Point(12, 98);
            panel2.Name = "panel2";
            panel2.Size = new Size(1167, 315);
            panel2.TabIndex = 1;
            // 
            // FormGds
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1191, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FormGds";
            RightToLeftLayout = true;
            Text = "Extraxtor Gds - TAAP";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridDeis).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label userLabel;
        public Label infoConection;
        public DataGridView dataGridDeis;
        private Panel panel2;
    }
}