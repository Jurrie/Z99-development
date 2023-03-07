namespace XYZ
{
    partial class XYZSetupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XYZSetupForm));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxSteppersXY = new System.Windows.Forms.GroupBox();
            this.radioButton400 = new System.Windows.Forms.RadioButton();
            this.radioButton200 = new System.Windows.Forms.RadioButton();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tableLayoutPanelHardware = new System.Windows.Forms.TableLayoutPanel();
            this.labelMargeY = new System.Windows.Forms.Label();
            this.labelMargeX = new System.Windows.Forms.Label();
            this.labelLengthShaftY = new System.Windows.Forms.Label();
            this.labelLengthShaftX = new System.Windows.Forms.Label();
            this.textBoxMargeY = new System.Windows.Forms.TextBox();
            this.textBoxMargeX = new System.Windows.Forms.TextBox();
            this.textBoxLengthShaftY = new System.Windows.Forms.TextBox();
            this.textBoxLengthShaftX = new System.Windows.Forms.TextBox();
            this.labelHelpMargeY = new System.Windows.Forms.Label();
            this.labelHelpMargeX = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelComport = new System.Windows.Forms.Label();
            this.comboBoxComport = new System.Windows.Forms.ComboBox();
            this.labelUSB = new System.Windows.Forms.Label();
            this.comboBoxBaudrate = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelBaudrateInfo = new System.Windows.Forms.Label();
            this.tableLayoutPanelScreen = new System.Windows.Forms.TableLayoutPanel();
            this.labelFondColor = new System.Windows.Forms.Label();
            this.labelFormColor = new System.Windows.Forms.Label();
            this.labelButtonColor = new System.Windows.Forms.Label();
            this.labelLanguage = new System.Windows.Forms.Label();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.buttonButtonColor = new System.Windows.Forms.Button();
            this.buttonFormColor = new System.Windows.Forms.Button();
            this.buttonFondColor = new System.Windows.Forms.Button();
            this.labelScreen = new System.Windows.Forms.Label();
            this.labelSetup = new System.Windows.Forms.Label();
            this.groupBoxSteppersXY.SuspendLayout();
            this.tableLayoutPanelHardware.SuspendLayout();
            this.tableLayoutPanelScreen.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(59, 493);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(197, 70);
            this.buttonCancel.TabIndex = 30;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // groupBoxSteppersXY
            // 
            this.groupBoxSteppersXY.Controls.Add(this.radioButton400);
            this.groupBoxSteppersXY.Controls.Add(this.radioButton200);
            this.groupBoxSteppersXY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxSteppersXY.Location = new System.Drawing.Point(880, 42);
            this.groupBoxSteppersXY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxSteppersXY.Name = "groupBoxSteppersXY";
            this.groupBoxSteppersXY.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxSteppersXY.Size = new System.Drawing.Size(239, 70);
            this.groupBoxSteppersXY.TabIndex = 41;
            this.groupBoxSteppersXY.TabStop = false;
            this.groupBoxSteppersXY.Text = "XY stepper motor resolution";
            this.groupBoxSteppersXY.Visible = false;
            // 
            // radioButton400
            // 
            this.radioButton400.AutoSize = true;
            this.radioButton400.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton400.Location = new System.Drawing.Point(168, 27);
            this.radioButton400.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton400.Name = "radioButton400";
            this.radioButton400.Size = new System.Drawing.Size(69, 29);
            this.radioButton400.TabIndex = 2;
            this.radioButton400.Text = "400";
            this.radioButton400.UseVisualStyleBackColor = true;
            this.radioButton400.CheckedChanged += new System.EventHandler(this.radioButton200_CheckedChanged);
            // 
            // radioButton200
            // 
            this.radioButton200.AutoSize = true;
            this.radioButton200.Checked = true;
            this.radioButton200.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton200.Location = new System.Drawing.Point(17, 27);
            this.radioButton200.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton200.Name = "radioButton200";
            this.radioButton200.Size = new System.Drawing.Size(69, 29);
            this.radioButton200.TabIndex = 1;
            this.radioButton200.TabStop = true;
            this.radioButton200.Text = "200";
            this.radioButton200.UseVisualStyleBackColor = true;
            this.radioButton200.CheckedChanged += new System.EventHandler(this.radioButton200_CheckedChanged);
            // 
            // buttonQuit
            // 
            this.buttonQuit.BackColor = System.Drawing.Color.LightCyan;
            this.buttonQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonQuit.Image = ((System.Drawing.Image)(resources.GetObject("buttonQuit.Image")));
            this.buttonQuit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonQuit.Location = new System.Drawing.Point(311, 493);
            this.buttonQuit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(217, 70);
            this.buttonQuit.TabIndex = 334;
            this.buttonQuit.Text = "Save and quit";
            this.buttonQuit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonQuit.UseVisualStyleBackColor = false;
            this.buttonQuit.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // tableLayoutPanelHardware
            // 
            this.tableLayoutPanelHardware.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelHardware.ColumnCount = 3;
            this.tableLayoutPanelHardware.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.06934F));
            this.tableLayoutPanelHardware.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.57655F));
            this.tableLayoutPanelHardware.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.3541F));
            this.tableLayoutPanelHardware.Controls.Add(this.labelMargeY, 0, 6);
            this.tableLayoutPanelHardware.Controls.Add(this.labelMargeX, 0, 5);
            this.tableLayoutPanelHardware.Controls.Add(this.labelLengthShaftY, 0, 4);
            this.tableLayoutPanelHardware.Controls.Add(this.labelLengthShaftX, 0, 3);
            this.tableLayoutPanelHardware.Controls.Add(this.textBoxMargeY, 1, 6);
            this.tableLayoutPanelHardware.Controls.Add(this.textBoxMargeX, 1, 5);
            this.tableLayoutPanelHardware.Controls.Add(this.textBoxLengthShaftY, 1, 4);
            this.tableLayoutPanelHardware.Controls.Add(this.textBoxLengthShaftX, 1, 3);
            this.tableLayoutPanelHardware.Controls.Add(this.labelHelpMargeY, 2, 6);
            this.tableLayoutPanelHardware.Controls.Add(this.labelHelpMargeX, 2, 5);
            this.tableLayoutPanelHardware.Controls.Add(this.label4, 2, 4);
            this.tableLayoutPanelHardware.Controls.Add(this.label3, 2, 3);
            this.tableLayoutPanelHardware.Controls.Add(this.labelComport, 0, 1);
            this.tableLayoutPanelHardware.Controls.Add(this.comboBoxComport, 1, 1);
            this.tableLayoutPanelHardware.Controls.Add(this.labelUSB, 2, 1);
            this.tableLayoutPanelHardware.Controls.Add(this.comboBoxBaudrate, 1, 2);
            this.tableLayoutPanelHardware.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanelHardware.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanelHardware.Controls.Add(this.labelBaudrateInfo, 2, 2);
            this.tableLayoutPanelHardware.Location = new System.Drawing.Point(551, 160);
            this.tableLayoutPanelHardware.Name = "tableLayoutPanelHardware";
            this.tableLayoutPanelHardware.RowCount = 8;
            this.tableLayoutPanelHardware.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.13149F));
            this.tableLayoutPanelHardware.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.20991F));
            this.tableLayoutPanelHardware.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.20991F));
            this.tableLayoutPanelHardware.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.20991F));
            this.tableLayoutPanelHardware.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.20991F));
            this.tableLayoutPanelHardware.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.47372F));
            this.tableLayoutPanelHardware.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.51524F));
            this.tableLayoutPanelHardware.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.0399F));
            this.tableLayoutPanelHardware.Size = new System.Drawing.Size(480, 316);
            this.tableLayoutPanelHardware.TabIndex = 348;
            // 
            // labelMargeY
            // 
            this.labelMargeY.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMargeY.AutoSize = true;
            this.labelMargeY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMargeY.Location = new System.Drawing.Point(3, 238);
            this.labelMargeY.Name = "labelMargeY";
            this.labelMargeY.Size = new System.Drawing.Size(306, 36);
            this.labelMargeY.TabIndex = 354;
            this.labelMargeY.Text = "Top marge Y mm:";
            this.labelMargeY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMargeX
            // 
            this.labelMargeX.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMargeX.AutoSize = true;
            this.labelMargeX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMargeX.Location = new System.Drawing.Point(3, 202);
            this.labelMargeX.Name = "labelMargeX";
            this.labelMargeX.Size = new System.Drawing.Size(306, 36);
            this.labelMargeX.TabIndex = 353;
            this.labelMargeX.Text = "Left marge X mm:";
            this.labelMargeX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelLengthShaftY
            // 
            this.labelLengthShaftY.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLengthShaftY.AutoSize = true;
            this.labelLengthShaftY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLengthShaftY.Location = new System.Drawing.Point(82, 170);
            this.labelLengthShaftY.Name = "labelLengthShaftY";
            this.labelLengthShaftY.Size = new System.Drawing.Size(147, 25);
            this.labelLengthShaftY.TabIndex = 360;
            this.labelLengthShaftY.Text = "Length Y shafts";
            this.labelLengthShaftY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelLengthShaftX
            // 
            this.labelLengthShaftX.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLengthShaftX.AutoSize = true;
            this.labelLengthShaftX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLengthShaftX.Location = new System.Drawing.Point(82, 132);
            this.labelLengthShaftX.Name = "labelLengthShaftX";
            this.labelLengthShaftX.Size = new System.Drawing.Size(148, 25);
            this.labelLengthShaftX.TabIndex = 359;
            this.labelLengthShaftX.Text = "Length X shafts";
            this.labelLengthShaftX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxMargeY
            // 
            this.textBoxMargeY.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMargeY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMargeY.Location = new System.Drawing.Point(315, 241);
            this.textBoxMargeY.Name = "textBoxMargeY";
            this.textBoxMargeY.Size = new System.Drawing.Size(111, 30);
            this.textBoxMargeY.TabIndex = 356;
            this.textBoxMargeY.Text = "0";
            this.textBoxMargeY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxMargeX
            // 
            this.textBoxMargeX.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMargeX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMargeX.Location = new System.Drawing.Point(315, 205);
            this.textBoxMargeX.Name = "textBoxMargeX";
            this.textBoxMargeX.Size = new System.Drawing.Size(111, 30);
            this.textBoxMargeX.TabIndex = 355;
            this.textBoxMargeX.Text = "0";
            this.textBoxMargeX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxLengthShaftY
            // 
            this.textBoxLengthShaftY.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLengthShaftY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLengthShaftY.Location = new System.Drawing.Point(315, 167);
            this.textBoxLengthShaftY.Name = "textBoxLengthShaftY";
            this.textBoxLengthShaftY.Size = new System.Drawing.Size(111, 30);
            this.textBoxLengthShaftY.TabIndex = 364;
            this.textBoxLengthShaftY.Text = "500";
            this.textBoxLengthShaftY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxLengthShaftX
            // 
            this.textBoxLengthShaftX.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLengthShaftX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLengthShaftX.Location = new System.Drawing.Point(315, 129);
            this.textBoxLengthShaftX.Name = "textBoxLengthShaftX";
            this.textBoxLengthShaftX.Size = new System.Drawing.Size(111, 30);
            this.textBoxLengthShaftX.TabIndex = 363;
            this.textBoxLengthShaftX.Text = "500";
            this.textBoxLengthShaftX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelHelpMargeY
            // 
            this.labelHelpMargeY.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHelpMargeY.AutoSize = true;
            this.labelHelpMargeY.BackColor = System.Drawing.Color.Yellow;
            this.labelHelpMargeY.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHelpMargeY.Location = new System.Drawing.Point(432, 238);
            this.labelHelpMargeY.Name = "labelHelpMargeY";
            this.labelHelpMargeY.Size = new System.Drawing.Size(45, 36);
            this.labelHelpMargeY.TabIndex = 358;
            this.labelHelpMargeY.Text = "?";
            this.labelHelpMargeY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelHelpMargeY.Click += new System.EventHandler(this.labelHelpMargeY_Click);
            // 
            // labelHelpMargeX
            // 
            this.labelHelpMargeX.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHelpMargeX.AutoSize = true;
            this.labelHelpMargeX.BackColor = System.Drawing.Color.Yellow;
            this.labelHelpMargeX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHelpMargeX.Location = new System.Drawing.Point(432, 202);
            this.labelHelpMargeX.Name = "labelHelpMargeX";
            this.labelHelpMargeX.Size = new System.Drawing.Size(45, 36);
            this.labelHelpMargeX.TabIndex = 357;
            this.labelHelpMargeX.Text = "?";
            this.labelHelpMargeX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelHelpMargeX.Click += new System.EventHandler(this.labelHelpMargeX_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(432, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 25);
            this.label4.TabIndex = 362;
            this.label4.Text = "mm";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(432, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 25);
            this.label3.TabIndex = 361;
            this.label3.Text = "mm";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelComport
            // 
            this.labelComport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelComport.AutoSize = true;
            this.labelComport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComport.Location = new System.Drawing.Point(3, 50);
            this.labelComport.Name = "labelComport";
            this.labelComport.Size = new System.Drawing.Size(306, 38);
            this.labelComport.TabIndex = 365;
            this.labelComport.Text = "Comport";
            this.labelComport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxComport
            // 
            this.comboBoxComport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxComport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxComport.FormattingEnabled = true;
            this.comboBoxComport.Location = new System.Drawing.Point(315, 52);
            this.comboBoxComport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxComport.Name = "comboBoxComport";
            this.comboBoxComport.Size = new System.Drawing.Size(111, 33);
            this.comboBoxComport.TabIndex = 366;
            this.comboBoxComport.Text = "Com3";
            // 
            // labelUSB
            // 
            this.labelUSB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUSB.BackColor = System.Drawing.Color.Yellow;
            this.labelUSB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelUSB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUSB.ForeColor = System.Drawing.Color.Black;
            this.labelUSB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUSB.Location = new System.Drawing.Point(429, 50);
            this.labelUSB.Margin = new System.Windows.Forms.Padding(0);
            this.labelUSB.Name = "labelUSB";
            this.labelUSB.Size = new System.Drawing.Size(51, 38);
            this.labelUSB.TabIndex = 367;
            this.labelUSB.Text = "?";
            this.labelUSB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelUSB.Click += new System.EventHandler(this.labelUSB_Click);
            // 
            // comboBoxBaudrate
            // 
            this.comboBoxBaudrate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBaudrate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxBaudrate.FormattingEnabled = true;
            this.comboBoxBaudrate.Items.AddRange(new object[] {
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "76800",
            "115200"});
            this.comboBoxBaudrate.Location = new System.Drawing.Point(314, 90);
            this.comboBoxBaudrate.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxBaudrate.Name = "comboBoxBaudrate";
            this.comboBoxBaudrate.Size = new System.Drawing.Size(113, 33);
            this.comboBoxBaudrate.TabIndex = 379;
            this.comboBoxBaudrate.Text = "57600";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(306, 38);
            this.label5.TabIndex = 380;
            this.label5.Text = "Baudrate";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.tableLayoutPanelHardware.SetColumnSpan(this.label7, 2);
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(423, 50);
            this.label7.TabIndex = 381;
            this.label7.Text = "Hardware";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelBaudrateInfo
            // 
            this.labelBaudrateInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBaudrateInfo.BackColor = System.Drawing.Color.Yellow;
            this.labelBaudrateInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelBaudrateInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBaudrateInfo.ForeColor = System.Drawing.Color.Black;
            this.labelBaudrateInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelBaudrateInfo.Location = new System.Drawing.Point(429, 88);
            this.labelBaudrateInfo.Margin = new System.Windows.Forms.Padding(0);
            this.labelBaudrateInfo.Name = "labelBaudrateInfo";
            this.labelBaudrateInfo.Size = new System.Drawing.Size(51, 38);
            this.labelBaudrateInfo.TabIndex = 382;
            this.labelBaudrateInfo.Text = "?";
            this.labelBaudrateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelBaudrateInfo.Click += new System.EventHandler(this.labelBaudrateInfo_Click);
            // 
            // tableLayoutPanelScreen
            // 
            this.tableLayoutPanelScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelScreen.ColumnCount = 2;
            this.tableLayoutPanelScreen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.45705F));
            this.tableLayoutPanelScreen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.54295F));
            this.tableLayoutPanelScreen.Controls.Add(this.labelFondColor, 0, 5);
            this.tableLayoutPanelScreen.Controls.Add(this.labelFormColor, 0, 4);
            this.tableLayoutPanelScreen.Controls.Add(this.labelButtonColor, 0, 3);
            this.tableLayoutPanelScreen.Controls.Add(this.labelLanguage, 0, 1);
            this.tableLayoutPanelScreen.Controls.Add(this.comboBoxLanguage, 1, 1);
            this.tableLayoutPanelScreen.Controls.Add(this.buttonButtonColor, 1, 3);
            this.tableLayoutPanelScreen.Controls.Add(this.buttonFormColor, 1, 4);
            this.tableLayoutPanelScreen.Controls.Add(this.buttonFondColor, 1, 5);
            this.tableLayoutPanelScreen.Controls.Add(this.labelScreen, 0, 0);
            this.tableLayoutPanelScreen.Location = new System.Drawing.Point(59, 148);
            this.tableLayoutPanelScreen.Name = "tableLayoutPanelScreen";
            this.tableLayoutPanelScreen.RowCount = 8;
            this.tableLayoutPanelScreen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.13149F));
            this.tableLayoutPanelScreen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.20991F));
            this.tableLayoutPanelScreen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.20991F));
            this.tableLayoutPanelScreen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.20991F));
            this.tableLayoutPanelScreen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.20991F));
            this.tableLayoutPanelScreen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.47372F));
            this.tableLayoutPanelScreen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.51524F));
            this.tableLayoutPanelScreen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.0399F));
            this.tableLayoutPanelScreen.Size = new System.Drawing.Size(443, 316);
            this.tableLayoutPanelScreen.TabIndex = 349;
            // 
            // labelFondColor
            // 
            this.labelFondColor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFondColor.AutoSize = true;
            this.labelFondColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFondColor.Location = new System.Drawing.Point(3, 202);
            this.labelFondColor.Name = "labelFondColor";
            this.labelFondColor.Size = new System.Drawing.Size(306, 36);
            this.labelFondColor.TabIndex = 353;
            this.labelFondColor.Text = "Fond color";
            this.labelFondColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelFormColor
            // 
            this.labelFormColor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelFormColor.AutoSize = true;
            this.labelFormColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFormColor.Location = new System.Drawing.Point(104, 170);
            this.labelFormColor.Name = "labelFormColor";
            this.labelFormColor.Size = new System.Drawing.Size(104, 25);
            this.labelFormColor.TabIndex = 360;
            this.labelFormColor.Text = "Form color";
            this.labelFormColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelButtonColor
            // 
            this.labelButtonColor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelButtonColor.AutoSize = true;
            this.labelButtonColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelButtonColor.Location = new System.Drawing.Point(98, 132);
            this.labelButtonColor.Name = "labelButtonColor";
            this.labelButtonColor.Size = new System.Drawing.Size(115, 25);
            this.labelButtonColor.TabIndex = 359;
            this.labelButtonColor.Text = "Button color";
            this.labelButtonColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelLanguage
            // 
            this.labelLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLanguage.Location = new System.Drawing.Point(3, 50);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(306, 38);
            this.labelLanguage.TabIndex = 365;
            this.labelLanguage.Text = "Taal-Sprache-Language-Idioma";
            this.labelLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Items.AddRange(new object[] {
            "Nederlands",
            "Deutsch",
            "English",
            "Français",
            "Espagnol"});
            this.comboBoxLanguage.Location = new System.Drawing.Point(315, 52);
            this.comboBoxLanguage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(125, 33);
            this.comboBoxLanguage.TabIndex = 366;
            this.comboBoxLanguage.Text = "English";
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
            // 
            // buttonButtonColor
            // 
            this.buttonButtonColor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonButtonColor.BackColor = System.Drawing.Color.LightCyan;
            this.buttonButtonColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonButtonColor.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonButtonColor.Location = new System.Drawing.Point(316, 130);
            this.buttonButtonColor.Margin = new System.Windows.Forms.Padding(4);
            this.buttonButtonColor.Name = "buttonButtonColor";
            this.buttonButtonColor.Size = new System.Drawing.Size(123, 30);
            this.buttonButtonColor.TabIndex = 368;
            this.buttonButtonColor.UseVisualStyleBackColor = false;
            this.buttonButtonColor.Click += new System.EventHandler(this.buttonIconcolor_Click);
            // 
            // buttonFormColor
            // 
            this.buttonFormColor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFormColor.BackColor = System.Drawing.Color.LightCyan;
            this.buttonFormColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFormColor.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonFormColor.Location = new System.Drawing.Point(316, 168);
            this.buttonFormColor.Margin = new System.Windows.Forms.Padding(4);
            this.buttonFormColor.Name = "buttonFormColor";
            this.buttonFormColor.Size = new System.Drawing.Size(123, 30);
            this.buttonFormColor.TabIndex = 369;
            this.buttonFormColor.UseVisualStyleBackColor = false;
            this.buttonFormColor.Click += new System.EventHandler(this.buttonFormColor_Click);
            // 
            // buttonFondColor
            // 
            this.buttonFondColor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFondColor.BackColor = System.Drawing.Color.LightCyan;
            this.buttonFondColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFondColor.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonFondColor.Location = new System.Drawing.Point(316, 206);
            this.buttonFondColor.Margin = new System.Windows.Forms.Padding(4);
            this.buttonFondColor.Name = "buttonFondColor";
            this.buttonFondColor.Size = new System.Drawing.Size(123, 28);
            this.buttonFondColor.TabIndex = 370;
            this.buttonFondColor.UseVisualStyleBackColor = false;
            this.buttonFondColor.Click += new System.EventHandler(this.buttonFondColor_Click);
            // 
            // labelScreen
            // 
            this.labelScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelScreen.AutoSize = true;
            this.tableLayoutPanelScreen.SetColumnSpan(this.labelScreen, 2);
            this.labelScreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScreen.Location = new System.Drawing.Point(3, 0);
            this.labelScreen.Name = "labelScreen";
            this.labelScreen.Size = new System.Drawing.Size(437, 50);
            this.labelScreen.TabIndex = 371;
            this.labelScreen.Text = "Screen";
            this.labelScreen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSetup
            // 
            this.labelSetup.AutoSize = true;
            this.labelSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSetup.Location = new System.Drawing.Point(349, 65);
            this.labelSetup.Name = "labelSetup";
            this.labelSetup.Size = new System.Drawing.Size(126, 32);
            this.labelSetup.TabIndex = 350;
            this.labelSetup.Text = "Settings";
            // 
            // XYZSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.ClientSize = new System.Drawing.Size(1187, 673);
            this.ControlBox = false;
            this.Controls.Add(this.labelSetup);
            this.Controls.Add(this.tableLayoutPanelScreen);
            this.Controls.Add(this.tableLayoutPanelHardware);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.groupBoxSteppersXY);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "XYZSetupForm";
            this.Text = "Setup";
            this.Activated += new System.EventHandler(this.SetupForm_Activated);
            this.groupBoxSteppersXY.ResumeLayout(false);
            this.groupBoxSteppersXY.PerformLayout();
            this.tableLayoutPanelHardware.ResumeLayout(false);
            this.tableLayoutPanelHardware.PerformLayout();
            this.tableLayoutPanelScreen.ResumeLayout(false);
            this.tableLayoutPanelScreen.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBoxSteppersXY;
        private System.Windows.Forms.RadioButton radioButton400;
        private System.Windows.Forms.RadioButton radioButton200;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelHardware;
        private System.Windows.Forms.Label labelMargeX;
        private System.Windows.Forms.TextBox textBoxMargeX;
        private System.Windows.Forms.Label labelHelpMargeX;
        private System.Windows.Forms.Label labelHelpMargeY;
        private System.Windows.Forms.TextBox textBoxMargeY;
        private System.Windows.Forms.Label labelMargeY;
        private System.Windows.Forms.Label labelLengthShaftX;
        private System.Windows.Forms.Label labelLengthShaftY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxLengthShaftX;
        private System.Windows.Forms.TextBox textBoxLengthShaftY;
        private System.Windows.Forms.Label labelComport;
        private System.Windows.Forms.ComboBox comboBoxComport;
        private System.Windows.Forms.Label labelUSB;
        private System.Windows.Forms.ComboBox comboBoxBaudrate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelScreen;
        private System.Windows.Forms.Label labelFondColor;
        private System.Windows.Forms.Label labelFormColor;
        private System.Windows.Forms.Label labelButtonColor;
        private System.Windows.Forms.Label labelLanguage;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.Button buttonButtonColor;
        private System.Windows.Forms.Button buttonFormColor;
        private System.Windows.Forms.Button buttonFondColor;
        private System.Windows.Forms.Label labelSetup;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelScreen;
        private System.Windows.Forms.Label labelBaudrateInfo;
    }
}