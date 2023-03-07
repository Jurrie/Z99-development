

namespace XYZ
{
    partial class DrillForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrillForm));
            this.labelDecimals = new System.Windows.Forms.Label();
            this.buttonDrill = new System.Windows.Forms.Button();
            this.textBoxThePlotDivider = new System.Windows.Forms.TextBox();
            this.progressBarLoad = new System.Windows.Forms.ProgressBar();
            this.buttonMenu = new System.Windows.Forms.Button();
            this.checkBoxPause = new System.Windows.Forms.CheckBox();
            this.listBoxDrillFile = new System.Windows.Forms.ListBox();
            this.openFileDialogImage = new System.Windows.Forms.OpenFileDialog();
            this.textBoxReceive = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxXYTravel = new System.Windows.Forms.ComboBox();
            this.comboBoxXYWork = new System.Windows.Forms.ComboBox();
            this.comboBoxZdown = new System.Windows.Forms.ComboBox();
            this.textBoxZupPosition = new System.Windows.Forms.TextBox();
            this.textBoxZdownPosition = new System.Windows.Forms.TextBox();
            this.buttonSetZupPosition = new System.Windows.Forms.Button();
            this.buttonSetZdownPosition = new System.Windows.Forms.Button();
            this.comboBoxZup = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelZmm = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelXmm = new System.Windows.Forms.Label();
            this.labelYmm = new System.Windows.Forms.Label();
            this.textBoxXY = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.checkBoxMirror = new System.Windows.Forms.CheckBox();
            this.comboBoxDecimals = new System.Windows.Forms.ComboBox();
            this.listBoxOutput = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSetXYTravel = new System.Windows.Forms.Button();
            this.buttonSetXYWork = new System.Windows.Forms.Button();
            this.buttonSetZupSpeed = new System.Windows.Forms.Button();
            this.buttonSetZdownSpeed = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxRGB = new System.Windows.Forms.GroupBox();
            this.radioButtonB = new System.Windows.Forms.RadioButton();
            this.radioButtonG = new System.Windows.Forms.RadioButton();
            this.radioButtonR = new System.Windows.Forms.RadioButton();
            this.panelXY = new System.Windows.Forms.Panel();
            this.buttonD = new System.Windows.Forms.Button();
            this.buttonU = new System.Windows.Forms.Button();
            this.buttonR = new System.Windows.Forms.Button();
            this.buttonL = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBoxRGB.SuspendLayout();
            this.panelXY.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelDecimals
            // 
            this.labelDecimals.AutoSize = true;
            this.labelDecimals.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDecimals.Location = new System.Drawing.Point(1686, 3);
            this.labelDecimals.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDecimals.Name = "labelDecimals";
            this.labelDecimals.Size = new System.Drawing.Size(92, 25);
            this.labelDecimals.TabIndex = 410;
            this.labelDecimals.Text = "Decimals";
            // 
            // buttonDrill
            // 
            this.buttonDrill.BackColor = System.Drawing.Color.LightCyan;
            this.buttonDrill.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDrill.ForeColor = System.Drawing.Color.Blue;
            this.buttonDrill.Location = new System.Drawing.Point(552, 6);
            this.buttonDrill.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonDrill.Name = "buttonDrill";
            this.buttonDrill.Size = new System.Drawing.Size(120, 80);
            this.buttonDrill.TabIndex = 408;
            this.buttonDrill.Text = "Start drill";
            this.toolTip1.SetToolTip(this.buttonDrill, "Start drilling");
            this.buttonDrill.UseVisualStyleBackColor = false;
            this.buttonDrill.Visible = false;
            this.buttonDrill.Click += new System.EventHandler(this.buttonDrill_Click);
            // 
            // textBoxThePlotDivider
            // 
            this.textBoxThePlotDivider.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxThePlotDivider.Location = new System.Drawing.Point(2125, 42);
            this.textBoxThePlotDivider.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxThePlotDivider.Name = "textBoxThePlotDivider";
            this.textBoxThePlotDivider.Size = new System.Drawing.Size(229, 30);
            this.textBoxThePlotDivider.TabIndex = 402;
            this.textBoxThePlotDivider.Text = "1";
            // 
            // progressBarLoad
            // 
            this.progressBarLoad.Location = new System.Drawing.Point(224, 13);
            this.progressBarLoad.Margin = new System.Windows.Forms.Padding(4);
            this.progressBarLoad.Maximum = 54;
            this.progressBarLoad.Name = "progressBarLoad";
            this.progressBarLoad.Size = new System.Drawing.Size(167, 28);
            this.progressBarLoad.Step = 5;
            this.progressBarLoad.TabIndex = 396;
            this.progressBarLoad.Value = 1;
            // 
            // buttonMenu
            // 
            this.buttonMenu.BackColor = System.Drawing.Color.LightCyan;
            this.buttonMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMenu.Location = new System.Drawing.Point(16, 6);
            this.buttonMenu.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMenu.Name = "buttonMenu";
            this.buttonMenu.Size = new System.Drawing.Size(200, 79);
            this.buttonMenu.TabIndex = 394;
            this.buttonMenu.Text = "Menu";
            this.buttonMenu.UseVisualStyleBackColor = false;
            this.buttonMenu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonMenu_MouseClick);
            // 
            // checkBoxPause
            // 
            this.checkBoxPause.BackColor = System.Drawing.Color.LightCyan;
            this.checkBoxPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxPause.Location = new System.Drawing.Point(774, 9);
            this.checkBoxPause.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxPause.Name = "checkBoxPause";
            this.checkBoxPause.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.checkBoxPause.Size = new System.Drawing.Size(106, 76);
            this.checkBoxPause.TabIndex = 395;
            this.checkBoxPause.Text = "Pause";
            this.toolTip1.SetToolTip(this.checkBoxPause, "Pause drilling, allows adjustment of XYZ speed and drill depth");
            this.checkBoxPause.UseVisualStyleBackColor = false;
            this.checkBoxPause.CheckedChanged += new System.EventHandler(this.checkBoxPause_CheckStateChanged);
            // 
            // listBoxDrillFile
            // 
            this.listBoxDrillFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxDrillFile.FormattingEnabled = true;
            this.listBoxDrillFile.ItemHeight = 25;
            this.listBoxDrillFile.Location = new System.Drawing.Point(15, 86);
            this.listBoxDrillFile.Margin = new System.Windows.Forms.Padding(5);
            this.listBoxDrillFile.Name = "listBoxDrillFile";
            this.listBoxDrillFile.Size = new System.Drawing.Size(380, 329);
            this.listBoxDrillFile.TabIndex = 389;
            this.listBoxDrillFile.SelectedIndexChanged += new System.EventHandler(this.listBoxDrillFile_SelectedIndexChanged);
            // 
            // openFileDialogImage
            // 
            this.openFileDialogImage.FileName = "openFileDialog1";
            this.openFileDialogImage.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*bmp\"";
            this.openFileDialogImage.FilterIndex = 0;
            this.openFileDialogImage.RestoreDirectory = true;
            // 
            // textBoxReceive
            // 
            this.textBoxReceive.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxReceive.Location = new System.Drawing.Point(15, 369);
            this.textBoxReceive.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxReceive.Multiline = true;
            this.textBoxReceive.Name = "textBoxReceive";
            this.textBoxReceive.Size = new System.Drawing.Size(421, 155);
            this.textBoxReceive.TabIndex = 388;
            this.textBoxReceive.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "plt";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Plt files (*.plt)|*.plt|Pen files (*.pen)|*.pen|Hpgl files(*.hpgl)|*.hpgl\"";
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // comboBoxXYTravel
            // 
            this.comboBoxXYTravel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxXYTravel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxXYTravel.FormattingEnabled = true;
            this.comboBoxXYTravel.Items.AddRange(new object[] {
            "10",
            "15",
            "20",
            "25",
            "50",
            "75",
            "100",
            "125",
            "150",
            "175",
            "200",
            "250"});
            this.comboBoxXYTravel.Location = new System.Drawing.Point(3, 30);
            this.comboBoxXYTravel.Name = "comboBoxXYTravel";
            this.comboBoxXYTravel.Size = new System.Drawing.Size(86, 33);
            this.comboBoxXYTravel.TabIndex = 363;
            this.comboBoxXYTravel.Text = "50";
            this.toolTip1.SetToolTip(this.comboBoxXYTravel, "XY travel speed");
            this.comboBoxXYTravel.Leave += new System.EventHandler(this.comboBoxXYTravel_Leave);
            // 
            // comboBoxXYWork
            // 
            this.comboBoxXYWork.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxXYWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxXYWork.FormattingEnabled = true;
            this.comboBoxXYWork.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "15",
            "20",
            "25",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100",
            "125",
            "150",
            "175",
            "200",
            "250"});
            this.comboBoxXYWork.Location = new System.Drawing.Point(3, 64);
            this.comboBoxXYWork.Name = "comboBoxXYWork";
            this.comboBoxXYWork.Size = new System.Drawing.Size(86, 33);
            this.comboBoxXYWork.TabIndex = 366;
            this.comboBoxXYWork.Text = "50";
            this.toolTip1.SetToolTip(this.comboBoxXYWork, "mm/sec");
            this.comboBoxXYWork.Visible = false;
            // 
            // comboBoxZdown
            // 
            this.comboBoxZdown.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxZdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxZdown.FormattingEnabled = true;
            this.comboBoxZdown.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "35",
            "40",
            "45",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100"});
            this.comboBoxZdown.Location = new System.Drawing.Point(175, 64);
            this.comboBoxZdown.Name = "comboBoxZdown";
            this.comboBoxZdown.Size = new System.Drawing.Size(86, 33);
            this.comboBoxZdown.TabIndex = 371;
            this.toolTip1.SetToolTip(this.comboBoxZdown, "Z speed downwards");
            this.comboBoxZdown.Leave += new System.EventHandler(this.comboBoxZdown_Leave);
            // 
            // textBoxZupPosition
            // 
            this.textBoxZupPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxZupPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxZupPosition.Location = new System.Drawing.Point(347, 29);
            this.textBoxZupPosition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxZupPosition.Name = "textBoxZupPosition";
            this.textBoxZupPosition.Size = new System.Drawing.Size(88, 30);
            this.textBoxZupPosition.TabIndex = 376;
            this.toolTip1.SetToolTip(this.textBoxZupPosition, "Z drill up position");
            this.textBoxZupPosition.Leave += new System.EventHandler(this.textBoxZupPosition_Leave);
            // 
            // textBoxZdownPosition
            // 
            this.textBoxZdownPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxZdownPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxZdownPosition.Location = new System.Drawing.Point(347, 63);
            this.textBoxZdownPosition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxZdownPosition.Name = "textBoxZdownPosition";
            this.textBoxZdownPosition.Size = new System.Drawing.Size(88, 30);
            this.textBoxZdownPosition.TabIndex = 377;
            this.toolTip1.SetToolTip(this.textBoxZdownPosition, "Z drill down position");
            this.textBoxZdownPosition.Leave += new System.EventHandler(this.textBoxZdownPosition_Leave);
            // 
            // buttonSetZupPosition
            // 
            this.buttonSetZupPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetZupPosition.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonSetZupPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetZupPosition.Location = new System.Drawing.Point(441, 29);
            this.buttonSetZupPosition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSetZupPosition.Name = "buttonSetZupPosition";
            this.buttonSetZupPosition.Size = new System.Drawing.Size(58, 30);
            this.buttonSetZupPosition.TabIndex = 378;
            this.buttonSetZupPosition.Text = "Set";
            this.toolTip1.SetToolTip(this.buttonSetZupPosition, "Z to upper Z position");
            this.buttonSetZupPosition.UseVisualStyleBackColor = false;
            this.buttonSetZupPosition.Click += new System.EventHandler(this.buttonSetZupPosition_Click);
            // 
            // buttonSetZdownPosition
            // 
            this.buttonSetZdownPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetZdownPosition.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonSetZdownPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetZdownPosition.Location = new System.Drawing.Point(441, 63);
            this.buttonSetZdownPosition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSetZdownPosition.Name = "buttonSetZdownPosition";
            this.buttonSetZdownPosition.Size = new System.Drawing.Size(58, 32);
            this.buttonSetZdownPosition.TabIndex = 379;
            this.buttonSetZdownPosition.Text = "Set";
            this.toolTip1.SetToolTip(this.buttonSetZdownPosition, "set Z to pen down position");
            this.buttonSetZdownPosition.UseVisualStyleBackColor = false;
            this.buttonSetZdownPosition.Click += new System.EventHandler(this.buttonSetZdownPosition_Click);
            // 
            // comboBoxZup
            // 
            this.comboBoxZup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxZup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxZup.FormattingEnabled = true;
            this.comboBoxZup.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "35",
            "40",
            "45",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100"});
            this.comboBoxZup.Location = new System.Drawing.Point(175, 30);
            this.comboBoxZup.Name = "comboBoxZup";
            this.comboBoxZup.Size = new System.Drawing.Size(86, 33);
            this.comboBoxZup.TabIndex = 370;
            this.toolTip1.SetToolTip(this.comboBoxZup, "Z speed upwards");
            this.comboBoxZup.Leave += new System.EventHandler(this.comboBoxZup_Leave);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(175, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 27);
            this.label6.TabIndex = 381;
            this.label6.Text = "Zmm/sec";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label6, "mm/sec");
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(347, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 27);
            this.label7.TabIndex = 382;
            this.label7.Text = "Z . mm";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label7, "mm/sec");
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 27);
            this.label3.TabIndex = 380;
            this.label3.Text = "XYmm/sec";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label3, "mm/sec");
            // 
            // labelZmm
            // 
            this.labelZmm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelZmm.AutoSize = true;
            this.labelZmm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZmm.Location = new System.Drawing.Point(31, 42);
            this.labelZmm.Name = "labelZmm";
            this.labelZmm.Size = new System.Drawing.Size(97, 21);
            this.labelZmm.TabIndex = 384;
            this.labelZmm.Text = "0.0";
            this.labelZmm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.labelZmm, "Actual Z position .mm");
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 21);
            this.label8.TabIndex = 376;
            this.label8.Text = "Z";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label8, "Actual Z level");
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 21);
            this.label9.TabIndex = 377;
            this.label9.Text = "X";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label9, "Actual X coordinate");
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 21);
            this.label10.TabIndex = 378;
            this.label10.Text = "Y";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label10, "Actual Y coordinate");
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(134, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 21);
            this.label11.TabIndex = 379;
            this.label11.Text = ".mm";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label11, ".mm");
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(134, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 21);
            this.label12.TabIndex = 380;
            this.label12.Text = "mm";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label12, ".mm");
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(134, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 21);
            this.label13.TabIndex = 381;
            this.label13.Text = "mm";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label13, ".mm");
            // 
            // labelXmm
            // 
            this.labelXmm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelXmm.AutoSize = true;
            this.labelXmm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXmm.Location = new System.Drawing.Point(31, 0);
            this.labelXmm.Name = "labelXmm";
            this.labelXmm.Size = new System.Drawing.Size(97, 21);
            this.labelXmm.TabIndex = 382;
            this.labelXmm.Text = "0.0";
            this.labelXmm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.labelXmm, "Actual X position .mm");
            // 
            // labelYmm
            // 
            this.labelYmm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelYmm.AutoSize = true;
            this.labelYmm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelYmm.Location = new System.Drawing.Point(31, 21);
            this.labelYmm.Name = "labelYmm";
            this.labelYmm.Size = new System.Drawing.Size(97, 21);
            this.labelYmm.TabIndex = 383;
            this.labelYmm.Text = "0.0";
            this.labelYmm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.labelYmm, "Actual Y position .mm");
            // 
            // textBoxXY
            // 
            this.textBoxXY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxXY.Location = new System.Drawing.Point(139, 147);
            this.textBoxXY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxXY.Name = "textBoxXY";
            this.textBoxXY.Size = new System.Drawing.Size(94, 30);
            this.textBoxXY.TabIndex = 212;
            this.textBoxXY.Text = "0.1";
            this.textBoxXY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.textBoxXY, "X mm");
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(1091, 108);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(971, 815);
            this.pictureBox1.TabIndex = 391;
            this.pictureBox1.TabStop = false;
            // 
            // buttonQuit
            // 
            this.buttonQuit.BackColor = System.Drawing.Color.LightCyan;
            this.buttonQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonQuit.Image = ((System.Drawing.Image)(resources.GetObject("buttonQuit.Image")));
            this.buttonQuit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonQuit.Location = new System.Drawing.Point(1720, 35);
            this.buttonQuit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(112, 51);
            this.buttonQuit.TabIndex = 415;
            this.buttonQuit.Text = "Quit";
            this.buttonQuit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonQuit.UseVisualStyleBackColor = false;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // checkBoxMirror
            // 
            this.checkBoxMirror.BackColor = System.Drawing.Color.LightCyan;
            this.checkBoxMirror.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMirror.Location = new System.Drawing.Point(399, 4);
            this.checkBoxMirror.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxMirror.Name = "checkBoxMirror";
            this.checkBoxMirror.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.checkBoxMirror.Size = new System.Drawing.Size(126, 76);
            this.checkBoxMirror.TabIndex = 416;
            this.checkBoxMirror.Text = "Mirror Y";
            this.checkBoxMirror.UseVisualStyleBackColor = false;
            // 
            // comboBoxDecimals
            // 
            this.comboBoxDecimals.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDecimals.FormattingEnabled = true;
            this.comboBoxDecimals.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboBoxDecimals.Location = new System.Drawing.Point(1805, -1);
            this.comboBoxDecimals.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxDecimals.Name = "comboBoxDecimals";
            this.comboBoxDecimals.Size = new System.Drawing.Size(52, 37);
            this.comboBoxDecimals.TabIndex = 417;
            this.comboBoxDecimals.Text = "5";
            this.comboBoxDecimals.SelectedIndexChanged += new System.EventHandler(this.comboBoxDecimals_SelectedIndexChanged);
            // 
            // listBoxOutput
            // 
            this.listBoxOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxOutput.FormattingEnabled = true;
            this.listBoxOutput.ItemHeight = 25;
            this.listBoxOutput.Location = new System.Drawing.Point(14, 436);
            this.listBoxOutput.Margin = new System.Windows.Forms.Padding(5);
            this.listBoxOutput.Name = "listBoxOutput";
            this.listBoxOutput.Size = new System.Drawing.Size(380, 279);
            this.listBoxOutput.TabIndex = 418;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.32749F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.94286F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.260127F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.35864F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.02718F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.113257F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.82128F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.14918F));
            this.tableLayoutPanel1.Controls.Add(this.comboBoxXYTravel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonSetXYTravel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxXYWork, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonSetXYWork, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxZdown, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonSetZupSpeed, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonSetZdownSpeed, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxZupPosition, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxZdownPosition, 6, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonSetZupPosition, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonSetZdownPosition, 7, 2);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxZup, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(958, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.05755F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.97123F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.97123F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(502, 97);
            this.tableLayoutPanel1.TabIndex = 622;
            // 
            // buttonSetXYTravel
            // 
            this.buttonSetXYTravel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetXYTravel.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonSetXYTravel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetXYTravel.Location = new System.Drawing.Point(95, 29);
            this.buttonSetXYTravel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSetXYTravel.Name = "buttonSetXYTravel";
            this.buttonSetXYTravel.Size = new System.Drawing.Size(48, 30);
            this.buttonSetXYTravel.TabIndex = 364;
            this.buttonSetXYTravel.Text = "Set";
            this.buttonSetXYTravel.UseVisualStyleBackColor = false;
            this.buttonSetXYTravel.Click += new System.EventHandler(this.buttonSetXYTravel_Click);
            // 
            // buttonSetXYWork
            // 
            this.buttonSetXYWork.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetXYWork.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonSetXYWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetXYWork.Location = new System.Drawing.Point(95, 63);
            this.buttonSetXYWork.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSetXYWork.Name = "buttonSetXYWork";
            this.buttonSetXYWork.Size = new System.Drawing.Size(48, 32);
            this.buttonSetXYWork.TabIndex = 367;
            this.buttonSetXYWork.Text = "Set";
            this.buttonSetXYWork.UseVisualStyleBackColor = false;
            this.buttonSetXYWork.Visible = false;
            this.buttonSetXYWork.Click += new System.EventHandler(this.buttonSetXYWork_Click);
            // 
            // buttonSetZupSpeed
            // 
            this.buttonSetZupSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetZupSpeed.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonSetZupSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetZupSpeed.Location = new System.Drawing.Point(267, 29);
            this.buttonSetZupSpeed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSetZupSpeed.Name = "buttonSetZupSpeed";
            this.buttonSetZupSpeed.Size = new System.Drawing.Size(49, 30);
            this.buttonSetZupSpeed.TabIndex = 372;
            this.buttonSetZupSpeed.Text = "Set";
            this.buttonSetZupSpeed.UseVisualStyleBackColor = false;
            this.buttonSetZupSpeed.Click += new System.EventHandler(this.buttonSetZupSpeed_Click);
            // 
            // buttonSetZdownSpeed
            // 
            this.buttonSetZdownSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetZdownSpeed.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonSetZdownSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetZdownSpeed.Location = new System.Drawing.Point(267, 63);
            this.buttonSetZdownSpeed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSetZdownSpeed.Name = "buttonSetZdownSpeed";
            this.buttonSetZdownSpeed.Size = new System.Drawing.Size(49, 32);
            this.buttonSetZdownSpeed.TabIndex = 373;
            this.buttonSetZdownSpeed.Text = "Set";
            this.buttonSetZdownSpeed.UseVisualStyleBackColor = false;
            this.buttonSetZdownSpeed.Click += new System.EventHandler(this.buttonSetZdownSpeed_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.93407F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.14286F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.47253F));
            this.tableLayoutPanel2.Controls.Add(this.labelZmm, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label11, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label12, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label13, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelXmm, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelYmm, 1, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1482, 6);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(182, 84);
            this.tableLayoutPanel2.TabIndex = 639;
            // 
            // groupBoxRGB
            // 
            this.groupBoxRGB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxRGB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.groupBoxRGB.Controls.Add(this.radioButtonB);
            this.groupBoxRGB.Controls.Add(this.radioButtonG);
            this.groupBoxRGB.Controls.Add(this.radioButtonR);
            this.groupBoxRGB.Location = new System.Drawing.Point(224, 48);
            this.groupBoxRGB.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxRGB.Name = "groupBoxRGB";
            this.groupBoxRGB.Padding = new System.Windows.Forms.Padding(0);
            this.groupBoxRGB.Size = new System.Drawing.Size(167, 37);
            this.groupBoxRGB.TabIndex = 612;
            this.groupBoxRGB.TabStop = false;
            // 
            // radioButtonB
            // 
            this.radioButtonB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonB.AutoSize = true;
            this.radioButtonB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonB.Location = new System.Drawing.Point(96, 12);
            this.radioButtonB.Name = "radioButtonB";
            this.radioButtonB.Size = new System.Drawing.Size(42, 24);
            this.radioButtonB.TabIndex = 3;
            this.radioButtonB.Text = "B";
            this.radioButtonB.UseVisualStyleBackColor = true;
            // 
            // radioButtonG
            // 
            this.radioButtonG.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonG.AutoSize = true;
            this.radioButtonG.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonG.Location = new System.Drawing.Point(47, 12);
            this.radioButtonG.Name = "radioButtonG";
            this.radioButtonG.Size = new System.Drawing.Size(43, 24);
            this.radioButtonG.TabIndex = 2;
            this.radioButtonG.Text = "G";
            this.radioButtonG.UseVisualStyleBackColor = true;
            // 
            // radioButtonR
            // 
            this.radioButtonR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonR.AutoSize = true;
            this.radioButtonR.Checked = true;
            this.radioButtonR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonR.Location = new System.Drawing.Point(8, 12);
            this.radioButtonR.Name = "radioButtonR";
            this.radioButtonR.Size = new System.Drawing.Size(42, 24);
            this.radioButtonR.TabIndex = 1;
            this.radioButtonR.TabStop = true;
            this.radioButtonR.Text = "R";
            this.radioButtonR.UseVisualStyleBackColor = true;
            // 
            // panelXY
            // 
            this.panelXY.BackColor = System.Drawing.SystemColors.Info;
            this.panelXY.Controls.Add(this.textBoxXY);
            this.panelXY.Controls.Add(this.buttonD);
            this.panelXY.Controls.Add(this.buttonU);
            this.panelXY.Controls.Add(this.buttonR);
            this.panelXY.Controls.Add(this.buttonL);
            this.panelXY.Location = new System.Drawing.Point(552, 143);
            this.panelXY.Name = "panelXY";
            this.panelXY.Size = new System.Drawing.Size(369, 326);
            this.panelXY.TabIndex = 640;
            this.panelXY.Visible = false;
            // 
            // buttonD
            // 
            this.buttonD.AccessibleDescription = "";
            this.buttonD.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonD.Image = ((System.Drawing.Image)(resources.GetObject("buttonD.Image")));
            this.buttonD.Location = new System.Drawing.Point(139, 216);
            this.buttonD.Margin = new System.Windows.Forms.Padding(0);
            this.buttonD.Name = "buttonD";
            this.buttonD.Size = new System.Drawing.Size(99, 80);
            this.buttonD.TabIndex = 211;
            this.buttonD.UseVisualStyleBackColor = true;
            this.buttonD.Click += new System.EventHandler(this.buttonD_Click);
            // 
            // buttonU
            // 
            this.buttonU.AccessibleDescription = "";
            this.buttonU.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonU.Image = ((System.Drawing.Image)(resources.GetObject("buttonU.Image")));
            this.buttonU.Location = new System.Drawing.Point(139, 23);
            this.buttonU.Margin = new System.Windows.Forms.Padding(0);
            this.buttonU.Name = "buttonU";
            this.buttonU.Size = new System.Drawing.Size(99, 85);
            this.buttonU.TabIndex = 210;
            this.buttonU.UseVisualStyleBackColor = true;
            this.buttonU.Click += new System.EventHandler(this.buttonU_Click);
            // 
            // buttonR
            // 
            this.buttonR.AccessibleDescription = "";
            this.buttonR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonR.AutoSize = true;
            this.buttonR.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonR.Image = ((System.Drawing.Image)(resources.GetObject("buttonR.Image")));
            this.buttonR.Location = new System.Drawing.Point(236, 116);
            this.buttonR.Margin = new System.Windows.Forms.Padding(0);
            this.buttonR.Name = "buttonR";
            this.buttonR.Size = new System.Drawing.Size(103, 99);
            this.buttonR.TabIndex = 209;
            this.buttonR.UseVisualStyleBackColor = true;
            this.buttonR.Click += new System.EventHandler(this.buttonR_Click);
            // 
            // buttonL
            // 
            this.buttonL.AutoSize = true;
            this.buttonL.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonL.Image = ((System.Drawing.Image)(resources.GetObject("buttonL.Image")));
            this.buttonL.Location = new System.Drawing.Point(24, 114);
            this.buttonL.Margin = new System.Windows.Forms.Padding(0);
            this.buttonL.Name = "buttonL";
            this.buttonL.Size = new System.Drawing.Size(109, 101);
            this.buttonL.TabIndex = 208;
            this.buttonL.UseVisualStyleBackColor = true;
            this.buttonL.Click += new System.EventHandler(this.buttonL_Click);
            // 
            // DrillForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 976);
            this.Controls.Add(this.panelXY);
            this.Controls.Add(this.groupBoxRGB);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.listBoxOutput);
            this.Controls.Add(this.comboBoxDecimals);
            this.Controls.Add(this.checkBoxMirror);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.labelDecimals);
            this.Controls.Add(this.buttonDrill);
            this.Controls.Add(this.textBoxThePlotDivider);
            this.Controls.Add(this.progressBarLoad);
            this.Controls.Add(this.buttonMenu);
            this.Controls.Add(this.checkBoxPause);
            this.Controls.Add(this.listBoxDrillFile);
            this.Controls.Add(this.textBoxReceive);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DrillForm";
            this.Text = "DrillForm";
            this.Load += new System.EventHandler(this.DrillForm_Load);
            this.Resize += new System.EventHandler(this.DrillForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBoxRGB.ResumeLayout(false);
            this.groupBoxRGB.PerformLayout();
            this.panelXY.ResumeLayout(false);
            this.panelXY.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelDecimals;
        private System.Windows.Forms.Button buttonDrill;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox textBoxThePlotDivider;
        private System.Windows.Forms.ProgressBar progressBarLoad;
        private System.Windows.Forms.Button buttonMenu;
        private System.Windows.Forms.CheckBox checkBoxPause;
        private System.Windows.Forms.ListBox listBoxDrillFile;
        private System.Windows.Forms.OpenFileDialog openFileDialogImage;
        private System.Windows.Forms.TextBox textBoxReceive;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.CheckBox checkBoxMirror;
        private System.Windows.Forms.ComboBox comboBoxDecimals;
        private System.Windows.Forms.ListBox listBoxOutput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBoxXYTravel;
        private System.Windows.Forms.Button buttonSetXYTravel;
        private System.Windows.Forms.ComboBox comboBoxXYWork;
        private System.Windows.Forms.Button buttonSetXYWork;
        private System.Windows.Forms.ComboBox comboBoxZdown;
        private System.Windows.Forms.Button buttonSetZupSpeed;
        private System.Windows.Forms.Button buttonSetZdownSpeed;
        private System.Windows.Forms.TextBox textBoxZupPosition;
        private System.Windows.Forms.TextBox textBoxZdownPosition;
        private System.Windows.Forms.Button buttonSetZupPosition;
        private System.Windows.Forms.Button buttonSetZdownPosition;
        private System.Windows.Forms.ComboBox comboBoxZup;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelZmm;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelXmm;
        private System.Windows.Forms.Label labelYmm;
        private System.Windows.Forms.GroupBox groupBoxRGB;
        private System.Windows.Forms.RadioButton radioButtonB;
        private System.Windows.Forms.RadioButton radioButtonG;
        private System.Windows.Forms.RadioButton radioButtonR;
        private System.Windows.Forms.Panel panelXY;
        private System.Windows.Forms.TextBox textBoxXY;
        private System.Windows.Forms.Button buttonD;
        private System.Windows.Forms.Button buttonU;
        private System.Windows.Forms.Button buttonR;
        private System.Windows.Forms.Button buttonL;
    }
}