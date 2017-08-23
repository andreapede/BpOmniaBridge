using System;
using System.Windows.Forms;

namespace BpOmniaBridge
{
    partial class BpOmniaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BpOmniaForm));
            this.saveButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            this.weightLabel = new System.Windows.Forms.Label();
            this.weight = new System.Windows.Forms.Label();
            this.flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            this.heightLabel = new System.Windows.Forms.Label();
            this.height = new System.Windows.Forms.Label();
            this.titleVisitCard = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.ethinicityLabel = new System.Windows.Forms.Label();
            this.ethnicity = new System.Windows.Forms.Label();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.genderLabel = new System.Windows.Forms.Label();
            this.gender = new System.Windows.Forms.Label();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.dobLabel = new System.Windows.Forms.Label();
            this.dob = new System.Windows.Forms.Label();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.lastnameLabel = new System.Windows.Forms.Label();
            this.lastname = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.idLabel = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.Label();
            this.subjectTitle = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.nameLabel = new System.Windows.Forms.Label();
            this.firstname = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.middleNameLabel = new System.Windows.Forms.Label();
            this.middlename = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.statusBar = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel9.SuspendLayout();
            this.flowLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(367, 0);
            this.saveButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(151, 27);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save Tests";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(527, 321);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(521, 282);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel9, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel8, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.titleVisitCard, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(264, 5);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 9;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(252, 272);
            this.tableLayoutPanel5.TabIndex = 3;
            // 
            // flowLayoutPanel9
            // 
            this.flowLayoutPanel9.Controls.Add(this.weightLabel);
            this.flowLayoutPanel9.Controls.Add(this.weight);
            this.flowLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel9.Location = new System.Drawing.Point(3, 84);
            this.flowLayoutPanel9.Name = "flowLayoutPanel9";
            this.flowLayoutPanel9.Size = new System.Drawing.Size(246, 21);
            this.flowLayoutPanel9.TabIndex = 6;
            // 
            // weightLabel
            // 
            this.weightLabel.AutoSize = true;
            this.weightLabel.Cursor = System.Windows.Forms.Cursors.No;
            this.weightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weightLabel.Location = new System.Drawing.Point(3, 3);
            this.weightLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.weightLabel.Name = "weightLabel";
            this.weightLabel.Size = new System.Drawing.Size(77, 13);
            this.weightLabel.TabIndex = 3;
            this.weightLabel.Text = "Weight (kg):";
            this.weightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // weight
            // 
            this.weight.AutoSize = true;
            this.weight.Cursor = System.Windows.Forms.Cursors.No;
            this.weight.Location = new System.Drawing.Point(86, 3);
            this.weight.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.weight.Name = "weight";
            this.weight.Size = new System.Drawing.Size(0, 13);
            this.weight.TabIndex = 4;
            // 
            // flowLayoutPanel8
            // 
            this.flowLayoutPanel8.Controls.Add(this.heightLabel);
            this.flowLayoutPanel8.Controls.Add(this.height);
            this.flowLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel8.Location = new System.Drawing.Point(3, 57);
            this.flowLayoutPanel8.Name = "flowLayoutPanel8";
            this.flowLayoutPanel8.Size = new System.Drawing.Size(246, 21);
            this.flowLayoutPanel8.TabIndex = 5;
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Cursor = System.Windows.Forms.Cursors.No;
            this.heightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heightLabel.Location = new System.Drawing.Point(3, 3);
            this.heightLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(76, 13);
            this.heightLabel.TabIndex = 3;
            this.heightLabel.Text = "Height (cm):";
            this.heightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // height
            // 
            this.height.AutoSize = true;
            this.height.Cursor = System.Windows.Forms.Cursors.No;
            this.height.Location = new System.Drawing.Point(85, 3);
            this.height.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(0, 13);
            this.height.TabIndex = 4;
            // 
            // titleVisitCard
            // 
            this.titleVisitCard.AutoSize = true;
            this.titleVisitCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleVisitCard.Location = new System.Drawing.Point(3, 5);
            this.titleVisitCard.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.titleVisitCard.Name = "titleVisitCard";
            this.titleVisitCard.Size = new System.Drawing.Size(92, 20);
            this.titleVisitCard.TabIndex = 1;
            this.titleVisitCard.Text = "Visit Card:";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel7, 0, 7);
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel6, 0, 6);
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel5, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel4, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel3, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.subjectTitle, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel2, 0, 3);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 9;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(251, 272);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.Controls.Add(this.ethinicityLabel);
            this.flowLayoutPanel7.Controls.Add(this.ethnicity);
            this.flowLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel7.Location = new System.Drawing.Point(3, 219);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(245, 21);
            this.flowLayoutPanel7.TabIndex = 8;
            // 
            // ethinicityLabel
            // 
            this.ethinicityLabel.AutoSize = true;
            this.ethinicityLabel.Cursor = System.Windows.Forms.Cursors.No;
            this.ethinicityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ethinicityLabel.Location = new System.Drawing.Point(3, 3);
            this.ethinicityLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.ethinicityLabel.Name = "ethinicityLabel";
            this.ethinicityLabel.Size = new System.Drawing.Size(60, 13);
            this.ethinicityLabel.TabIndex = 5;
            this.ethinicityLabel.Text = "Ethnicity:";
            this.ethinicityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ethnicity
            // 
            this.ethnicity.AutoSize = true;
            this.ethnicity.Cursor = System.Windows.Forms.Cursors.No;
            this.ethnicity.Location = new System.Drawing.Point(69, 3);
            this.ethnicity.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.ethnicity.Name = "ethnicity";
            this.ethnicity.Size = new System.Drawing.Size(0, 13);
            this.ethnicity.TabIndex = 6;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.Controls.Add(this.genderLabel);
            this.flowLayoutPanel6.Controls.Add(this.gender);
            this.flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel6.Location = new System.Drawing.Point(3, 192);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(245, 21);
            this.flowLayoutPanel6.TabIndex = 7;
            // 
            // genderLabel
            // 
            this.genderLabel.AutoSize = true;
            this.genderLabel.Cursor = System.Windows.Forms.Cursors.No;
            this.genderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genderLabel.Location = new System.Drawing.Point(3, 3);
            this.genderLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.genderLabel.Name = "genderLabel";
            this.genderLabel.Size = new System.Drawing.Size(52, 13);
            this.genderLabel.TabIndex = 5;
            this.genderLabel.Text = "Gender:";
            this.genderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gender
            // 
            this.gender.AutoSize = true;
            this.gender.Cursor = System.Windows.Forms.Cursors.No;
            this.gender.Location = new System.Drawing.Point(61, 3);
            this.gender.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.gender.Name = "gender";
            this.gender.Size = new System.Drawing.Size(0, 13);
            this.gender.TabIndex = 6;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.dobLabel);
            this.flowLayoutPanel5.Controls.Add(this.dob);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 165);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(245, 21);
            this.flowLayoutPanel5.TabIndex = 6;
            // 
            // dobLabel
            // 
            this.dobLabel.AutoSize = true;
            this.dobLabel.Cursor = System.Windows.Forms.Cursors.No;
            this.dobLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dobLabel.Location = new System.Drawing.Point(3, 3);
            this.dobLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.dobLabel.Name = "dobLabel";
            this.dobLabel.Size = new System.Drawing.Size(83, 13);
            this.dobLabel.TabIndex = 5;
            this.dobLabel.Text = "Date of Birth:";
            this.dobLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dob
            // 
            this.dob.AutoSize = true;
            this.dob.Cursor = System.Windows.Forms.Cursors.No;
            this.dob.Location = new System.Drawing.Point(92, 3);
            this.dob.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.dob.Name = "dob";
            this.dob.Size = new System.Drawing.Size(0, 13);
            this.dob.TabIndex = 6;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.lastnameLabel);
            this.flowLayoutPanel4.Controls.Add(this.lastname);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 138);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(245, 21);
            this.flowLayoutPanel4.TabIndex = 5;
            // 
            // lastnameLabel
            // 
            this.lastnameLabel.AutoSize = true;
            this.lastnameLabel.Cursor = System.Windows.Forms.Cursors.No;
            this.lastnameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastnameLabel.Location = new System.Drawing.Point(3, 3);
            this.lastnameLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lastnameLabel.Name = "lastnameLabel";
            this.lastnameLabel.Size = new System.Drawing.Size(65, 13);
            this.lastnameLabel.TabIndex = 5;
            this.lastnameLabel.Text = "Lastname:";
            this.lastnameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lastname
            // 
            this.lastname.AutoSize = true;
            this.lastname.Cursor = System.Windows.Forms.Cursors.No;
            this.lastname.Location = new System.Drawing.Point(74, 3);
            this.lastname.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lastname.Name = "lastname";
            this.lastname.Size = new System.Drawing.Size(0, 13);
            this.lastname.TabIndex = 6;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.idLabel);
            this.flowLayoutPanel3.Controls.Add(this.id);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 57);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(245, 21);
            this.flowLayoutPanel3.TabIndex = 4;
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Cursor = System.Windows.Forms.Cursors.No;
            this.idLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idLabel.Location = new System.Drawing.Point(3, 3);
            this.idLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(24, 13);
            this.idLabel.TabIndex = 3;
            this.idLabel.Text = "ID:";
            this.idLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // id
            // 
            this.id.AutoSize = true;
            this.id.Cursor = System.Windows.Forms.Cursors.No;
            this.id.Location = new System.Drawing.Point(33, 3);
            this.id.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(0, 13);
            this.id.TabIndex = 4;
            // 
            // subjectTitle
            // 
            this.subjectTitle.AutoSize = true;
            this.subjectTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subjectTitle.Location = new System.Drawing.Point(3, 5);
            this.subjectTitle.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.subjectTitle.Name = "subjectTitle";
            this.subjectTitle.Size = new System.Drawing.Size(75, 20);
            this.subjectTitle.TabIndex = 1;
            this.subjectTitle.Text = "Subject:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.nameLabel);
            this.flowLayoutPanel1.Controls.Add(this.firstname);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 84);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(245, 21);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Cursor = System.Windows.Forms.Cursors.No;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(3, 3);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(65, 13);
            this.nameLabel.TabIndex = 3;
            this.nameLabel.Text = "Firstname:";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // firstname
            // 
            this.firstname.AutoSize = true;
            this.firstname.Cursor = System.Windows.Forms.Cursors.No;
            this.firstname.Location = new System.Drawing.Point(74, 3);
            this.firstname.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.firstname.Name = "firstname";
            this.firstname.Size = new System.Drawing.Size(0, 13);
            this.firstname.TabIndex = 4;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.middleNameLabel);
            this.flowLayoutPanel2.Controls.Add(this.middlename);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 111);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(245, 21);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // middleNameLabel
            // 
            this.middleNameLabel.AutoSize = true;
            this.middleNameLabel.Cursor = System.Windows.Forms.Cursors.No;
            this.middleNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.middleNameLabel.Location = new System.Drawing.Point(3, 3);
            this.middleNameLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.middleNameLabel.Name = "middleNameLabel";
            this.middleNameLabel.Size = new System.Drawing.Size(78, 13);
            this.middleNameLabel.TabIndex = 5;
            this.middleNameLabel.Text = "Middlename:";
            this.middleNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // middlename
            // 
            this.middlename.AutoSize = true;
            this.middlename.Cursor = System.Windows.Forms.Cursors.No;
            this.middlename.Location = new System.Drawing.Point(87, 3);
            this.middlename.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.middlename.Name = "middlename";
            this.middlename.Size = new System.Drawing.Size(0, 13);
            this.middlename.TabIndex = 6;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.Controls.Add(this.saveButton, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.statusBar, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 291);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(521, 27);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // statusBar
            // 
            this.statusBar.AutoSize = true;
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBar.Location = new System.Drawing.Point(3, 3);
            this.statusBar.Margin = new System.Windows.Forms.Padding(3);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(358, 21);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "Something";
            // 
            // BpOmniaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 321);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BpOmniaForm";
            this.Text = "BP/Omnia Bridge v" + Application.ProductVersion;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.flowLayoutPanel9.ResumeLayout(false);
            this.flowLayoutPanel9.PerformLayout();
            this.flowLayoutPanel8.ResumeLayout(false);
            this.flowLayoutPanel8.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.flowLayoutPanel7.ResumeLayout(false);
            this.flowLayoutPanel7.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button saveButton;
        private PaintEventHandler tableLayoutPanel2_Paint;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Label statusBar;
        private TableLayoutPanel tableLayoutPanel5;
        private Label titleVisitCard;
        private TableLayoutPanel tableLayoutPanel4;
        private Label subjectTitle;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label nameLabel;
        private Label firstname;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label middleNameLabel;
        private Label middlename;
        private FlowLayoutPanel flowLayoutPanel3;
        private Label idLabel;
        private Label id;
        private FlowLayoutPanel flowLayoutPanel7;
        private Label ethinicityLabel;
        private Label ethnicity;
        private FlowLayoutPanel flowLayoutPanel6;
        private Label genderLabel;
        private Label gender;
        private FlowLayoutPanel flowLayoutPanel5;
        private Label dobLabel;
        private Label dob;
        private FlowLayoutPanel flowLayoutPanel4;
        private Label lastnameLabel;
        private Label lastname;
        private FlowLayoutPanel flowLayoutPanel9;
        private Label weightLabel;
        private Label weight;
        private FlowLayoutPanel flowLayoutPanel8;
        private Label heightLabel;
        private Label height;
    }
}

