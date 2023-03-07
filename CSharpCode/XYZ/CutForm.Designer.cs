﻿namespace XYZ
{
    partial class CutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CutForm));
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxXYTravel = new System.Windows.Forms.ComboBox();
            this.comboBoxXYWork = new System.Windows.Forms.ComboBox();
            this.comboBoxZdown = new System.Windows.Forms.ComboBox();
            this.textBoxZupPosition = new System.Windows.Forms.TextBox();
            this.textBoxZdownPosition = new System.Windows.Forms.TextBox();
            this.comboBoxZup = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelXmm = new System.Windows.Forms.Label();
            this.labelYmm = new System.Windows.Forms.Label();
            this.buttonSetZupPosition = new System.Windows.Forms.Button();
            this.buttonSetZdownPosition = new System.Windows.Forms.Button();
            this.buttonZupUp = new System.Windows.Forms.Button();
            this.buttonZupDown = new System.Windows.Forms.Button();
            this.buttonZdownUp = new System.Windows.Forms.Button();
            this.buttonZdownDown = new System.Windows.Forms.Button();
            this.checkBoxMirror = new System.Windows.Forms.CheckBox();
            this.checkBoxBorderCut = new System.Windows.Forms.CheckBox();
            this.textBoxEditY = new System.Windows.Forms.TextBox();
            this.textBoxEditX = new System.Windows.Forms.TextBox();
            this.labelZmm = new System.Windows.Forms.Label();
            this.comboBoxLaserPasses = new System.Windows.Forms.ComboBox();
            this.textBoxDepthZ = new System.Windows.Forms.TextBox();
            this.listBoxHidden = new System.Windows.Forms.ListBox();
            this.progressBarLoad = new System.Windows.Forms.ProgressBar();
            this.buttonMenu = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBoxTheDivider = new System.Windows.Forms.TextBox();
            this.checkBoxPause = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogImage = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.listBoxHPGL = new System.Windows.Forms.ListBox();
            this.textBoxReceive = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonOutput = new System.Windows.Forms.Button();
            this.groupBoxRGB = new System.Windows.Forms.GroupBox();
            this.radioButtonB = new System.Windows.Forms.RadioButton();
            this.radioButtonG = new System.Windows.Forms.RadioButton();
            this.radioButtonR = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSetXYTravel = new System.Windows.Forms.Button();
            this.buttonSetXYWork = new System.Windows.Forms.Button();
            this.buttonSetZupSpeed = new System.Windows.Forms.Button();
            this.buttonSetZdownSpeed = new System.Windows.Forms.Button();
            this.panelMaxLabels = new System.Windows.Forms.Panel();
            this.labelMaxYmm = new System.Windows.Forms.Label();
            this.labelMaxXmm = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxLaser = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanelLaser = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelDimXY = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxRGB.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelMaxLabels.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanelLaser.SuspendLayout();
            this.tableLayoutPanelDimXY.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxXYTravel
            // 
            this.comboBoxXYTravel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxXYTravel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxXYTravel.FormattingEnabled = true;
            this.comboBoxXYTravel.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
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
            this.comboBoxXYTravel.Location = new System.Drawing.Point(2, 24);
            this.comboBoxXYTravel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxXYTravel.Name = "comboBoxXYTravel";
            this.comboBoxXYTravel.Size = new System.Drawing.Size(51, 28);
            this.comboBoxXYTravel.TabIndex = 363;
            this.comboBoxXYTravel.Text = "50";
            this.toolTip1.SetToolTip(this.comboBoxXYTravel, "XY cutter travel speed");
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
            this.comboBoxXYWork.Location = new System.Drawing.Point(2, 52);
            this.comboBoxXYWork.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxXYWork.Name = "comboBoxXYWork";
            this.comboBoxXYWork.Size = new System.Drawing.Size(51, 28);
            this.comboBoxXYWork.TabIndex = 366;
            this.comboBoxXYWork.Text = "50";
            this.toolTip1.SetToolTip(this.comboBoxXYWork, "mm/sec");
            this.comboBoxXYWork.Leave += new System.EventHandler(this.comboBoxXYWork_Leave);
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
            this.comboBoxZdown.Location = new System.Drawing.Point(125, 52);
            this.comboBoxZdown.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxZdown.Name = "comboBoxZdown";
            this.comboBoxZdown.Size = new System.Drawing.Size(47, 28);
            this.comboBoxZdown.TabIndex = 371;
            this.toolTip1.SetToolTip(this.comboBoxZdown, "Z cutter downwards speed");
            this.comboBoxZdown.Leave += new System.EventHandler(this.comboBoxZdown_Leave);
            // 
            // textBoxZupPosition
            // 
            this.textBoxZupPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxZupPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxZupPosition.Location = new System.Drawing.Point(231, 24);
            this.textBoxZupPosition.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxZupPosition.Name = "textBoxZupPosition";
            this.textBoxZupPosition.Size = new System.Drawing.Size(70, 26);
            this.textBoxZupPosition.TabIndex = 376;
            this.toolTip1.SetToolTip(this.textBoxZupPosition, "Z cutter upmost position");
            this.textBoxZupPosition.Leave += new System.EventHandler(this.textBoxZupPosition_Leave);
            // 
            // textBoxZdownPosition
            // 
            this.textBoxZdownPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxZdownPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxZdownPosition.Location = new System.Drawing.Point(231, 52);
            this.textBoxZdownPosition.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxZdownPosition.Name = "textBoxZdownPosition";
            this.textBoxZdownPosition.Size = new System.Drawing.Size(70, 26);
            this.textBoxZdownPosition.TabIndex = 377;
            this.toolTip1.SetToolTip(this.textBoxZdownPosition, "Z cutter down position");
            this.textBoxZdownPosition.Leave += new System.EventHandler(this.textBoxZdownPosition_Leave);
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
            this.comboBoxZup.Location = new System.Drawing.Point(125, 24);
            this.comboBoxZup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxZup.Name = "comboBoxZup";
            this.comboBoxZup.Size = new System.Drawing.Size(47, 28);
            this.comboBoxZup.TabIndex = 370;
            this.toolTip1.SetToolTip(this.comboBoxZup, "Z cutter upwards speed");
            this.comboBoxZup.Leave += new System.EventHandler(this.comboBoxZup_Leave);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 22);
            this.label3.TabIndex = 380;
            this.label3.Text = "XYmm/sec";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label3, "mm/sec");
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(125, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 22);
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
            this.label7.Location = new System.Drawing.Point(231, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 22);
            this.label7.TabIndex = 382;
            this.label7.Text = "Zpos .mm";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.label7, "mm/sec");
            // 
            // labelXmm
            // 
            this.labelXmm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelXmm.AutoSize = true;
            this.labelXmm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXmm.Location = new System.Drawing.Point(2, 0);
            this.labelXmm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelXmm.Name = "labelXmm";
            this.labelXmm.Size = new System.Drawing.Size(66, 22);
            this.labelXmm.TabIndex = 382;
            this.labelXmm.Text = "0.0";
            this.labelXmm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.labelXmm, "Actual X coordinate");
            // 
            // labelYmm
            // 
            this.labelYmm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelYmm.AutoSize = true;
            this.labelYmm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelYmm.Location = new System.Drawing.Point(2, 22);
            this.labelYmm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelYmm.Name = "labelYmm";
            this.labelYmm.Size = new System.Drawing.Size(66, 22);
            this.labelYmm.TabIndex = 383;
            this.labelYmm.Text = "0.0";
            this.labelYmm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.labelYmm, "Actual Y coordinate");
            // 
            // buttonSetZupPosition
            // 
            this.buttonSetZupPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetZupPosition.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonSetZupPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetZupPosition.Location = new System.Drawing.Point(305, 24);
            this.buttonSetZupPosition.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSetZupPosition.Name = "buttonSetZupPosition";
            this.buttonSetZupPosition.Size = new System.Drawing.Size(44, 24);
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
            this.buttonSetZdownPosition.Location = new System.Drawing.Point(305, 52);
            this.buttonSetZdownPosition.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSetZdownPosition.Name = "buttonSetZdownPosition";
            this.buttonSetZdownPosition.Size = new System.Drawing.Size(44, 25);
            this.buttonSetZdownPosition.TabIndex = 379;
            this.buttonSetZdownPosition.Text = "Set";
            this.toolTip1.SetToolTip(this.buttonSetZdownPosition, "Set Z to pen down position");
            this.buttonSetZdownPosition.UseVisualStyleBackColor = false;
            this.buttonSetZdownPosition.Click += new System.EventHandler(this.buttonSetZdownPosition_Click);
            // 
            // buttonZupUp
            // 
            this.buttonZupUp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonZupUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonZupUp.BackgroundImage")));
            this.buttonZupUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZupUp.Location = new System.Drawing.Point(353, 24);
            this.buttonZupUp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonZupUp.Name = "buttonZupUp";
            this.buttonZupUp.Size = new System.Drawing.Size(27, 24);
            this.buttonZupUp.TabIndex = 383;
            this.toolTip1.SetToolTip(this.buttonZupUp, "Lift upper Z position 1 mm");
            this.buttonZupUp.UseVisualStyleBackColor = true;
            this.buttonZupUp.Click += new System.EventHandler(this.buttonZupUp_Click);
            // 
            // buttonZupDown
            // 
            this.buttonZupDown.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonZupDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonZupDown.BackgroundImage")));
            this.buttonZupDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZupDown.Location = new System.Drawing.Point(384, 24);
            this.buttonZupDown.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonZupDown.Name = "buttonZupDown";
            this.buttonZupDown.Size = new System.Drawing.Size(30, 24);
            this.buttonZupDown.TabIndex = 384;
            this.toolTip1.SetToolTip(this.buttonZupDown, "Upper Z position 1 mm down");
            this.buttonZupDown.UseVisualStyleBackColor = true;
            this.buttonZupDown.Click += new System.EventHandler(this.buttonZupDown_Click);
            // 
            // buttonZdownUp
            // 
            this.buttonZdownUp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonZdownUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonZdownUp.BackgroundImage")));
            this.buttonZdownUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZdownUp.Location = new System.Drawing.Point(353, 52);
            this.buttonZdownUp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonZdownUp.Name = "buttonZdownUp";
            this.buttonZdownUp.Size = new System.Drawing.Size(27, 25);
            this.buttonZdownUp.TabIndex = 385;
            this.toolTip1.SetToolTip(this.buttonZdownUp, "Cut Z position 0.05mm up");
            this.buttonZdownUp.UseVisualStyleBackColor = true;
            this.buttonZdownUp.Click += new System.EventHandler(this.buttonZdownUp_Click);
            // 
            // buttonZdownDown
            // 
            this.buttonZdownDown.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonZdownDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonZdownDown.BackgroundImage")));
            this.buttonZdownDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZdownDown.Location = new System.Drawing.Point(384, 52);
            this.buttonZdownDown.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonZdownDown.Name = "buttonZdownDown";
            this.buttonZdownDown.Size = new System.Drawing.Size(30, 25);
            this.buttonZdownDown.TabIndex = 386;
            this.toolTip1.SetToolTip(this.buttonZdownDown, "Cut Z position 0.05 mm down");
            this.buttonZdownDown.UseVisualStyleBackColor = true;
            this.buttonZdownDown.Click += new System.EventHandler(this.buttonZdownDown_Click);
            // 
            // checkBoxMirror
            // 
            this.checkBoxMirror.BackColor = System.Drawing.Color.LightCyan;
            this.checkBoxMirror.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.checkBoxMirror.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMirror.Location = new System.Drawing.Point(204, 8);
            this.checkBoxMirror.Name = "checkBoxMirror";
            this.checkBoxMirror.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.checkBoxMirror.Size = new System.Drawing.Size(86, 28);
            this.checkBoxMirror.TabIndex = 365;
            this.checkBoxMirror.Text = "MirrorY";
            this.toolTip1.SetToolTip(this.checkBoxMirror, "Mirror around Y axis");
            this.checkBoxMirror.UseVisualStyleBackColor = false;
            // 
            // checkBoxBorderCut
            // 
            this.checkBoxBorderCut.BackColor = System.Drawing.Color.LightCyan;
            this.checkBoxBorderCut.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.checkBoxBorderCut.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxBorderCut.Location = new System.Drawing.Point(204, 43);
            this.checkBoxBorderCut.Name = "checkBoxBorderCut";
            this.checkBoxBorderCut.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.checkBoxBorderCut.Size = new System.Drawing.Size(86, 28);
            this.checkBoxBorderCut.TabIndex = 639;
            this.checkBoxBorderCut.Text = "Border";
            this.toolTip1.SetToolTip(this.checkBoxBorderCut, "Only cut border when checked");
            this.checkBoxBorderCut.UseVisualStyleBackColor = false;
            // 
            // textBoxEditY
            // 
            this.textBoxEditY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEditY.Location = new System.Drawing.Point(35, 32);
            this.textBoxEditY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxEditY.Name = "textBoxEditY";
            this.textBoxEditY.Size = new System.Drawing.Size(81, 26);
            this.textBoxEditY.TabIndex = 614;
            this.textBoxEditY.Text = "80.00";
            this.textBoxEditY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.textBoxEditY, "Y Dimension of output in .mm");
            // 
            // textBoxEditX
            // 
            this.textBoxEditX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEditX.Location = new System.Drawing.Point(35, 2);
            this.textBoxEditX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxEditX.Name = "textBoxEditX";
            this.textBoxEditX.Size = new System.Drawing.Size(81, 26);
            this.textBoxEditX.TabIndex = 609;
            this.textBoxEditX.Text = "150.00";
            this.textBoxEditX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.textBoxEditX, "X Dimension of output in .mm");
            // 
            // labelZmm
            // 
            this.labelZmm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelZmm.AutoSize = true;
            this.labelZmm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZmm.Location = new System.Drawing.Point(2, 44);
            this.labelZmm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelZmm.Name = "labelZmm";
            this.labelZmm.Size = new System.Drawing.Size(66, 24);
            this.labelZmm.TabIndex = 384;
            this.labelZmm.Text = "0.0";
            this.labelZmm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.labelZmm, "Actual Z position");
            // 
            // comboBoxLaserPasses
            // 
            this.comboBoxLaserPasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLaserPasses.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxLaserPasses.ForeColor = System.Drawing.Color.Red;
            this.comboBoxLaserPasses.FormattingEnabled = true;
            this.comboBoxLaserPasses.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBoxLaserPasses.Location = new System.Drawing.Point(2, 42);
            this.comboBoxLaserPasses.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxLaserPasses.Name = "comboBoxLaserPasses";
            this.comboBoxLaserPasses.Size = new System.Drawing.Size(71, 28);
            this.comboBoxLaserPasses.TabIndex = 652;
            this.comboBoxLaserPasses.Text = "1";
            this.toolTip1.SetToolTip(this.comboBoxLaserPasses, "Passes for laser");
            // 
            // textBoxDepthZ
            // 
            this.textBoxDepthZ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDepthZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDepthZ.ForeColor = System.Drawing.Color.Red;
            this.textBoxDepthZ.Location = new System.Drawing.Point(77, 42);
            this.textBoxDepthZ.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxDepthZ.Name = "textBoxDepthZ";
            this.textBoxDepthZ.Size = new System.Drawing.Size(71, 26);
            this.textBoxDepthZ.TabIndex = 653;
            this.textBoxDepthZ.Text = "0.1";
            this.textBoxDepthZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.textBoxDepthZ, "Z dept after each pass");
            // 
            // listBoxHidden
            // 
            this.listBoxHidden.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxHidden.FormattingEnabled = true;
            this.listBoxHidden.ItemHeight = 20;
            this.listBoxHidden.Location = new System.Drawing.Point(9, 275);
            this.listBoxHidden.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBoxHidden.Name = "listBoxHidden";
            this.listBoxHidden.Size = new System.Drawing.Size(201, 124);
            this.listBoxHidden.TabIndex = 364;
            this.listBoxHidden.Visible = false;
            // 
            // progressBarLoad
            // 
            this.progressBarLoad.Location = new System.Drawing.Point(88, 11);
            this.progressBarLoad.Maximum = 54;
            this.progressBarLoad.Name = "progressBarLoad";
            this.progressBarLoad.Size = new System.Drawing.Size(110, 20);
            this.progressBarLoad.Step = 5;
            this.progressBarLoad.TabIndex = 363;
            this.progressBarLoad.Value = 1;
            // 
            // buttonMenu
            // 
            this.buttonMenu.BackColor = System.Drawing.Color.LightCyan;
            this.buttonMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMenu.Location = new System.Drawing.Point(12, 10);
            this.buttonMenu.Name = "buttonMenu";
            this.buttonMenu.Size = new System.Drawing.Size(70, 65);
            this.buttonMenu.TabIndex = 361;
            this.buttonMenu.Text = "Menu";
            this.buttonMenu.UseVisualStyleBackColor = false;
            this.buttonMenu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonMenu_MouseClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // textBoxTheDivider
            // 
            this.textBoxTheDivider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTheDivider.Location = new System.Drawing.Point(1480, 9);
            this.textBoxTheDivider.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxTheDivider.Name = "textBoxTheDivider";
            this.textBoxTheDivider.Size = new System.Drawing.Size(92, 22);
            this.textBoxTheDivider.TabIndex = 370;
            this.textBoxTheDivider.Text = "1";
            this.textBoxTheDivider.Visible = false;
            // 
            // checkBoxPause
            // 
            this.checkBoxPause.BackColor = System.Drawing.Color.LightCyan;
            this.checkBoxPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxPause.Location = new System.Drawing.Point(668, 15);
            this.checkBoxPause.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxPause.Name = "checkBoxPause";
            this.checkBoxPause.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.checkBoxPause.Size = new System.Drawing.Size(85, 37);
            this.checkBoxPause.TabIndex = 362;
            this.checkBoxPause.Text = "Pause";
            this.checkBoxPause.UseVisualStyleBackColor = false;
            this.checkBoxPause.CheckStateChanged += new System.EventHandler(this.checkBoxPause_CheckStateChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "plt";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "\"Hpgl files(*.hpgl) | *.hpgl |Plt files (*.plt)|*.plt|Pen files(*.pen) | *.pen\";";
            this.openFileDialog1.FilterIndex = 0;
            // 
            // openFileDialogImage
            // 
            this.openFileDialogImage.FileName = "openFileDialog1";
            this.openFileDialogImage.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*bmp\"";
            this.openFileDialogImage.FilterIndex = 0;
            this.openFileDialogImage.RestoreDirectory = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(342, 179);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(774, 475);
            this.pictureBox1.TabIndex = 352;
            this.pictureBox1.TabStop = false;
            // 
            // buttonQuit
            // 
            this.buttonQuit.BackColor = System.Drawing.Color.LightCyan;
            this.buttonQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonQuit.Image = ((System.Drawing.Image)(resources.GetObject("buttonQuit.Image")));
            this.buttonQuit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonQuit.Location = new System.Drawing.Point(1324, 4);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(70, 65);
            this.buttonQuit.TabIndex = 351;
            this.buttonQuit.Text = "Quit";
            this.buttonQuit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonQuit.UseVisualStyleBackColor = false;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // listBoxHPGL
            // 
            this.listBoxHPGL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxHPGL.FormattingEnabled = true;
            this.listBoxHPGL.ItemHeight = 20;
            this.listBoxHPGL.Location = new System.Drawing.Point(11, 86);
            this.listBoxHPGL.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBoxHPGL.Name = "listBoxHPGL";
            this.listBoxHPGL.Size = new System.Drawing.Size(299, 184);
            this.listBoxHPGL.TabIndex = 349;
            this.listBoxHPGL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBoxHPGL_MouseClick);
            // 
            // textBoxReceive
            // 
            this.textBoxReceive.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxReceive.Location = new System.Drawing.Point(11, 404);
            this.textBoxReceive.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxReceive.Multiline = true;
            this.textBoxReceive.Name = "textBoxReceive";
            this.textBoxReceive.Size = new System.Drawing.Size(299, 641);
            this.textBoxReceive.TabIndex = 348;
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.LightCyan;
            this.buttonStart.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.ForeColor = System.Drawing.Color.Blue;
            this.buttonStart.Location = new System.Drawing.Point(591, 13);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(72, 38);
            this.buttonStart.TabIndex = 381;
            this.buttonStart.Text = "Start cutter";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Visible = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonExecuteHPGL_Click);
            // 
            // buttonOutput
            // 
            this.buttonOutput.BackColor = System.Drawing.Color.LightCyan;
            this.buttonOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOutput.Location = new System.Drawing.Point(296, 8);
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.Size = new System.Drawing.Size(79, 64);
            this.buttonOutput.TabIndex = 607;
            this.buttonOutput.Text = "Output format";
            this.buttonOutput.UseVisualStyleBackColor = false;
            this.buttonOutput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonOutput_MouseClick);
            // 
            // groupBoxRGB
            // 
            this.groupBoxRGB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxRGB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.groupBoxRGB.Controls.Add(this.radioButtonB);
            this.groupBoxRGB.Controls.Add(this.radioButtonG);
            this.groupBoxRGB.Controls.Add(this.radioButtonR);
            this.groupBoxRGB.Location = new System.Drawing.Point(86, 34);
            this.groupBoxRGB.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxRGB.Name = "groupBoxRGB";
            this.groupBoxRGB.Padding = new System.Windows.Forms.Padding(0);
            this.groupBoxRGB.Size = new System.Drawing.Size(112, 34);
            this.groupBoxRGB.TabIndex = 611;
            this.groupBoxRGB.TabStop = false;
            // 
            // radioButtonB
            // 
            this.radioButtonB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonB.AutoSize = true;
            this.radioButtonB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonB.Location = new System.Drawing.Point(72, 10);
            this.radioButtonB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButtonB.Name = "radioButtonB";
            this.radioButtonB.Size = new System.Drawing.Size(35, 21);
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
            this.radioButtonG.Location = new System.Drawing.Point(35, 10);
            this.radioButtonG.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButtonG.Name = "radioButtonG";
            this.radioButtonG.Size = new System.Drawing.Size(37, 21);
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
            this.radioButtonR.Location = new System.Drawing.Point(6, 10);
            this.radioButtonR.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButtonR.Name = "radioButtonR";
            this.radioButtonR.Size = new System.Drawing.Size(36, 21);
            this.radioButtonR.TabIndex = 1;
            this.radioButtonR.TabStop = true;
            this.radioButtonR.Text = "R";
            this.radioButtonR.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.35156F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.34418F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.14025F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.35181F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.5108F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.000257F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.93991F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.58026F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.625375F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.155601F));
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
            this.tableLayoutPanel1.Controls.Add(this.buttonZupUp, 8, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonZupDown, 9, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonZdownUp, 8, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonZdownDown, 9, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(757, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.05755F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.97123F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.97123F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(416, 79);
            this.tableLayoutPanel1.TabIndex = 612;
            // 
            // buttonSetXYTravel
            // 
            this.buttonSetXYTravel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetXYTravel.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonSetXYTravel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetXYTravel.Location = new System.Drawing.Point(57, 24);
            this.buttonSetXYTravel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSetXYTravel.Name = "buttonSetXYTravel";
            this.buttonSetXYTravel.Size = new System.Drawing.Size(51, 24);
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
            this.buttonSetXYWork.Location = new System.Drawing.Point(57, 52);
            this.buttonSetXYWork.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSetXYWork.Name = "buttonSetXYWork";
            this.buttonSetXYWork.Size = new System.Drawing.Size(51, 25);
            this.buttonSetXYWork.TabIndex = 367;
            this.buttonSetXYWork.Text = "Set";
            this.buttonSetXYWork.UseVisualStyleBackColor = false;
            this.buttonSetXYWork.Click += new System.EventHandler(this.buttonSetXYWork_Click);
            // 
            // buttonSetZupSpeed
            // 
            this.buttonSetZupSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetZupSpeed.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonSetZupSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetZupSpeed.Location = new System.Drawing.Point(176, 24);
            this.buttonSetZupSpeed.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSetZupSpeed.Name = "buttonSetZupSpeed";
            this.buttonSetZupSpeed.Size = new System.Drawing.Size(39, 24);
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
            this.buttonSetZdownSpeed.Location = new System.Drawing.Point(176, 52);
            this.buttonSetZdownSpeed.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSetZdownSpeed.Name = "buttonSetZdownSpeed";
            this.buttonSetZdownSpeed.Size = new System.Drawing.Size(39, 25);
            this.buttonSetZdownSpeed.TabIndex = 373;
            this.buttonSetZdownSpeed.Text = "Set";
            this.buttonSetZdownSpeed.UseVisualStyleBackColor = false;
            this.buttonSetZdownSpeed.Click += new System.EventHandler(this.buttonSetZdownSpeed_Click);
            // 
            // panelMaxLabels
            // 
            this.panelMaxLabels.Controls.Add(this.labelMaxYmm);
            this.panelMaxLabels.Controls.Add(this.labelMaxXmm);
            this.panelMaxLabels.Location = new System.Drawing.Point(376, 80);
            this.panelMaxLabels.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelMaxLabels.Name = "panelMaxLabels";
            this.panelMaxLabels.Size = new System.Drawing.Size(134, 63);
            this.panelMaxLabels.TabIndex = 637;
            // 
            // labelMaxYmm
            // 
            this.labelMaxYmm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMaxYmm.AutoSize = true;
            this.labelMaxYmm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMaxYmm.Location = new System.Drawing.Point(3, 35);
            this.labelMaxYmm.Name = "labelMaxYmm";
            this.labelMaxYmm.Size = new System.Drawing.Size(64, 17);
            this.labelMaxYmm.TabIndex = 607;
            this.labelMaxYmm.Text = "MaxYmm";
            this.labelMaxYmm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMaxXmm
            // 
            this.labelMaxXmm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMaxXmm.AutoSize = true;
            this.labelMaxXmm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMaxXmm.Location = new System.Drawing.Point(3, 7);
            this.labelMaxXmm.Name = "labelMaxXmm";
            this.labelMaxXmm.Size = new System.Drawing.Size(64, 17);
            this.labelMaxXmm.TabIndex = 606;
            this.labelMaxXmm.Text = "MaxXmm";
            this.labelMaxXmm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel2.Controls.Add(this.labelXmm, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelYmm, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelZmm, 0, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1199, 6);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(70, 68);
            this.tableLayoutPanel2.TabIndex = 638;
            // 
            // checkBoxLaser
            // 
            this.checkBoxLaser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxLaser.AutoSize = true;
            this.tableLayoutPanelLaser.SetColumnSpan(this.checkBoxLaser, 2);
            this.checkBoxLaser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxLaser.ForeColor = System.Drawing.Color.Red;
            this.checkBoxLaser.Location = new System.Drawing.Point(2, 2);
            this.checkBoxLaser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxLaser.Name = "checkBoxLaser";
            this.checkBoxLaser.Size = new System.Drawing.Size(146, 36);
            this.checkBoxLaser.TabIndex = 646;
            this.checkBoxLaser.Text = "LASER";
            this.checkBoxLaser.UseVisualStyleBackColor = true;
            this.checkBoxLaser.CheckedChanged += new System.EventHandler(this.checkBoxLaser_CheckedChanged);
            // 
            // tableLayoutPanelLaser
            // 
            this.tableLayoutPanelLaser.ColumnCount = 2;
            this.tableLayoutPanelLaser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelLaser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelLaser.Controls.Add(this.checkBoxLaser, 0, 0);
            this.tableLayoutPanelLaser.Controls.Add(this.textBoxDepthZ, 1, 1);
            this.tableLayoutPanelLaser.Controls.Add(this.comboBoxLaserPasses, 0, 1);
            this.tableLayoutPanelLaser.Location = new System.Drawing.Point(612, 92);
            this.tableLayoutPanelLaser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanelLaser.Name = "tableLayoutPanelLaser";
            this.tableLayoutPanelLaser.RowCount = 2;
            this.tableLayoutPanelLaser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelLaser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelLaser.Size = new System.Drawing.Size(150, 81);
            this.tableLayoutPanelLaser.TabIndex = 652;
            // 
            // tableLayoutPanelDimXY
            // 
            this.tableLayoutPanelDimXY.ColumnCount = 2;
            this.tableLayoutPanelDimXY.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.5F));
            this.tableLayoutPanelDimXY.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.5F));
            this.tableLayoutPanelDimXY.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanelDimXY.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanelDimXY.Controls.Add(this.textBoxEditY, 1, 1);
            this.tableLayoutPanelDimXY.Controls.Add(this.textBoxEditX, 1, 0);
            this.tableLayoutPanelDimXY.Location = new System.Drawing.Point(382, 8);
            this.tableLayoutPanelDimXY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanelDimXY.Name = "tableLayoutPanelDimXY";
            this.tableLayoutPanelDimXY.RowCount = 2;
            this.tableLayoutPanelDimXY.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelDimXY.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelDimXY.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanelDimXY.Size = new System.Drawing.Size(118, 60);
            this.tableLayoutPanelDimXY.TabIndex = 653;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 30);
            this.label4.TabIndex = 614;
            this.label4.Text = "Y";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 30);
            this.label2.TabIndex = 613;
            this.label2.Text = "X";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1443, 812);
            this.Controls.Add(this.tableLayoutPanelDimXY);
            this.Controls.Add(this.tableLayoutPanelLaser);
            this.Controls.Add(this.checkBoxBorderCut);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.panelMaxLabels);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.groupBoxRGB);
            this.Controls.Add(this.buttonOutput);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.checkBoxMirror);
            this.Controls.Add(this.listBoxHidden);
            this.Controls.Add(this.progressBarLoad);
            this.Controls.Add(this.buttonMenu);
            this.Controls.Add(this.textBoxTheDivider);
            this.Controls.Add(this.checkBoxPause);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.listBoxHPGL);
            this.Controls.Add(this.textBoxReceive);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CutForm";
            this.Text = "Cutter";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxRGB.ResumeLayout(false);
            this.groupBoxRGB.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelMaxLabels.ResumeLayout(false);
            this.panelMaxLabels.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanelLaser.ResumeLayout(false);
            this.tableLayoutPanelLaser.PerformLayout();
            this.tableLayoutPanelDimXY.ResumeLayout(false);
            this.tableLayoutPanelDimXY.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBoxMirror;
        private System.Windows.Forms.ListBox listBoxHidden;
        private System.Windows.Forms.ProgressBar progressBarLoad;
        private System.Windows.Forms.Button buttonMenu;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBoxTheDivider;
        private System.Windows.Forms.CheckBox checkBoxPause;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialogImage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.ListBox listBoxHPGL;
        private System.Windows.Forms.TextBox textBoxReceive;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonOutput;
        private System.Windows.Forms.TextBox textBoxEditY;
        private System.Windows.Forms.TextBox textBoxEditX;
        private System.Windows.Forms.GroupBox groupBoxRGB;
        private System.Windows.Forms.RadioButton radioButtonB;
        private System.Windows.Forms.RadioButton radioButtonG;
        private System.Windows.Forms.RadioButton radioButtonR;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panelMaxLabels;
        private System.Windows.Forms.Label labelMaxYmm;
        private System.Windows.Forms.Label labelMaxXmm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelXmm;
        private System.Windows.Forms.Label labelYmm;
        private System.Windows.Forms.Button buttonZupUp;
        private System.Windows.Forms.Button buttonZupDown;
        private System.Windows.Forms.Button buttonZdownUp;
        private System.Windows.Forms.Button buttonZdownDown;
        private System.Windows.Forms.CheckBox checkBoxBorderCut;
        private System.Windows.Forms.CheckBox checkBoxLaser;
        private System.Windows.Forms.Label labelZmm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLaser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDimXY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDepthZ;
        private System.Windows.Forms.ComboBox comboBoxLaserPasses;
    }
}