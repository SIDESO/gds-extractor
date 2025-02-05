using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            label4 = new Label();
            labelUserDescription = new Label();
            panelGetEvents = new Panel();
            labelReposnseBtnGetEvents = new Label();
            dateTimeStartDate = new DateTimePicker();
            label3 = new Label();
            buttonGetEvents = new System.Windows.Forms.Button();
            label1 = new Label();
            dateTimeEndDate = new DateTimePicker();
            label2 = new Label();
            textBoxLimit = new System.Windows.Forms.TextBox();
            infoConection = new Label();
            userLabel = new Label();
            dataGridDeis = new DataGridView();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panelGetEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridDeis).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label4);
            panel1.Controls.Add(labelUserDescription);
            panel1.Controls.Add(panelGetEvents);
            panel1.Controls.Add(infoConection);
            panel1.Controls.Add(userLabel);
            panel1.Location = new Point(7, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1385, 143);
            panel1.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(18, 26);
            label4.Name = "label4";
            label4.Size = new Size(161, 20);
            label4.TabIndex = 12;
            label4.Text = "Estado conexión GDS:";
            // 
            // labelUserDescription
            // 
            labelUserDescription.AutoSize = true;
            labelUserDescription.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelUserDescription.Location = new Point(18, 6);
            labelUserDescription.Name = "labelUserDescription";
            labelUserDescription.Size = new Size(165, 20);
            labelUserDescription.TabIndex = 11;
            labelUserDescription.Text = "Usuario Transito APP :";
            // 
            // panelGetEvents
            // 
            panelGetEvents.BorderStyle = BorderStyle.FixedSingle;
            panelGetEvents.Controls.Add(labelReposnseBtnGetEvents);
            panelGetEvents.Controls.Add(dateTimeStartDate);
            panelGetEvents.Controls.Add(label3);
            panelGetEvents.Controls.Add(buttonGetEvents);
            panelGetEvents.Controls.Add(label1);
            panelGetEvents.Controls.Add(dateTimeEndDate);
            panelGetEvents.Controls.Add(label2);
            panelGetEvents.Controls.Add(textBoxLimit);
            panelGetEvents.Location = new Point(341, 53);
            panelGetEvents.Name = "panelGetEvents";
            panelGetEvents.Size = new Size(1041, 87);
            panelGetEvents.TabIndex = 10;
            // 
            // labelReposnseBtnGetEvents
            // 
            labelReposnseBtnGetEvents.ForeColor = Color.Red;
            labelReposnseBtnGetEvents.Location = new Point(781, 62);
            labelReposnseBtnGetEvents.Name = "labelReposnseBtnGetEvents";
            labelReposnseBtnGetEvents.Size = new Size(246, 23);
            labelReposnseBtnGetEvents.TabIndex = 10;
            labelReposnseBtnGetEvents.UseMnemonic = false;
            // 
            // dateTimeStartDate
            // 
            dateTimeStartDate.Location = new Point(3, 33);
            dateTimeStartDate.Name = "dateTimeStartDate";
            dateTimeStartDate.Size = new Size(311, 27);
            dateTimeStartDate.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(634, 10);
            label3.Name = "label3";
            label3.Size = new Size(119, 20);
            label3.TabIndex = 7;
            label3.Text = "Limite registros";
            label3.UseMnemonic = false;
            // 
            // buttonGetEvents
            // 
            buttonGetEvents.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonGetEvents.Location = new Point(781, 10);
            buttonGetEvents.Name = "buttonGetEvents";
            buttonGetEvents.Size = new Size(246, 50);
            buttonGetEvents.TabIndex = 9;
            buttonGetEvents.Text = "Consultar eventos en GDS";
            buttonGetEvents.UseVisualStyleBackColor = true;
            buttonGetEvents.Click += buttonGetEvents_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(317, 10);
            label1.Name = "label1";
            label1.Size = new Size(107, 20);
            label1.TabIndex = 6;
            label1.Text = "Fecha final (*)";
            label1.UseMnemonic = false;
            // 
            // dateTimeEndDate
            // 
            dateTimeEndDate.Location = new Point(317, 33);
            dateTimeEndDate.Name = "dateTimeEndDate";
            dateTimeEndDate.Size = new Size(311, 27);
            dateTimeEndDate.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(3, 10);
            label2.Name = "label2";
            label2.Size = new Size(112, 20);
            label2.TabIndex = 5;
            label2.Text = "Fecha inicial(*)";
            label2.UseMnemonic = false;
            // 
            // textBoxLimit
            // 
            textBoxLimit.Location = new Point(634, 33);
            textBoxLimit.Name = "textBoxLimit";
            textBoxLimit.Size = new Size(141, 27);
            textBoxLimit.TabIndex = 8;
            textBoxLimit.KeyPress += textBoxLimit_KeyPress;
            // 
            // infoConection
            // 
            infoConection.AccessibleDescription = "infoConection";
            infoConection.AccessibleName = "infoConection";
            infoConection.AutoSize = true;
            infoConection.Location = new Point(185, 26);
            infoConection.Name = "infoConection";
            infoConection.Size = new Size(0, 20);
            infoConection.TabIndex = 1;
            // 
            // userLabel
            // 
            userLabel.AccessibleDescription = "userLabel";
            userLabel.AccessibleName = "userLabel";
            userLabel.Location = new Point(184, 6);
            userLabel.Name = "userLabel";
            userLabel.Size = new Size(408, 20);
            userLabel.TabIndex = 0;
            // 
            // dataGridDeis
            // 
            dataGridDeis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridDeis.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridDeis.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridDeis.Location = new Point(3, 3);
            dataGridDeis.Name = "dataGridDeis";
            dataGridDeis.RowHeadersWidth = 51;
            dataGridDeis.ScrollBars = ScrollBars.Vertical;
            dataGridDeis.Size = new Size(1374, 462);
            dataGridDeis.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(dataGridDeis);
            panel2.Location = new Point(7, 152);
            panel2.Name = "panel2";
            panel2.Size = new Size(1385, 570);
            panel2.TabIndex = 1;
            // 
            // FormGds
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1404, 734);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormGds";
            RightToLeftLayout = true;
            Text = "Extraxtor Gds - TAAP";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelGetEvents.ResumeLayout(false);
            panelGetEvents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridDeis).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void textBoxLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }



        #endregion

        private Panel panel1;
        private Label userLabel;
        public Label infoConection;
        public DataGridView dataGridDeis;
        private Panel panel2;
        private DateTimePicker dateTimeStartDate;
        private DateTimePicker dateTimeEndDate;
        private Label line;
        private Label label2;
        private Label label3;
        private Label label1;
        private System.Windows.Forms.TextBox textBoxLimit;
        private System.Windows.Forms.Button buttonGetEvents;
        private Panel panelGetEvents;
        private Label labelReposnseBtnGetEvents;
        private Label labelUserDescription;
        private Label label4;
    }
}