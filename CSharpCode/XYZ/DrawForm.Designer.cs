namespace XYZ
{
    partial class DrawForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawForm));
            this.progressBarLoad = new System.Windows.Forms.ProgressBar();
            this.buttonMenu = new System.Windows.Forms.Button();
            this.textBoxUndo = new System.Windows.Forms.TextBox();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.listBoxHPGL = new System.Windows.Forms.ListBox();
            this.checkBoxMirror = new System.Windows.Forms.CheckBox();
            this.openFileDialogImage = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBoxScreenDivider = new System.Windows.Forms.TextBox();
            this.listBoxHidden = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonWhite = new System.Windows.Forms.RadioButton();
            this.radioButtonViolet = new System.Windows.Forms.RadioButton();
            this.radioButtonBlue = new System.Windows.Forms.RadioButton();
            this.radioButtonGreen = new System.Windows.Forms.RadioButton();
            this.radioButtonYellow = new System.Windows.Forms.RadioButton();
            this.radioButtonOrange = new System.Windows.Forms.RadioButton();
            this.radioButtonRed = new System.Windows.Forms.RadioButton();
            this.radioButtonBrown = new System.Windows.Forms.RadioButton();
            this.radioButtonBlack = new System.Windows.Forms.RadioButton();
            this.panelBoxLines = new System.Windows.Forms.Panel();
            this.panelDraw = new System.Windows.Forms.Panel();
            this.labelThickness = new System.Windows.Forms.Label();
            this.labelUndoSteps = new System.Windows.Forms.Label();
            this.checkBoxDrawing = new System.Windows.Forms.CheckBox();
            this.labelFoot = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBarLoad
            // 
            this.progressBarLoad.Location = new System.Drawing.Point(231, 20);
            this.progressBarLoad.Margin = new System.Windows.Forms.Padding(4);
            this.progressBarLoad.Maximum = 54;
            this.progressBarLoad.Name = "progressBarLoad";
            this.progressBarLoad.Size = new System.Drawing.Size(133, 28);
            this.progressBarLoad.Step = 5;
            this.progressBarLoad.TabIndex = 337;
            this.progressBarLoad.Value = 1;
            // 
            // buttonMenu
            // 
            this.buttonMenu.BackColor = System.Drawing.Color.LightCyan;
            this.buttonMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMenu.Location = new System.Drawing.Point(16, 15);
            this.buttonMenu.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMenu.Name = "buttonMenu";
            this.buttonMenu.Size = new System.Drawing.Size(207, 73);
            this.buttonMenu.TabIndex = 336;
            this.buttonMenu.Text = "Menu";
            this.buttonMenu.UseVisualStyleBackColor = false;
            this.buttonMenu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonMenu_MouseClick);
            // 
            // textBoxUndo
            // 
            this.textBoxUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUndo.Location = new System.Drawing.Point(1053, 41);
            this.textBoxUndo.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxUndo.Name = "textBoxUndo";
            this.textBoxUndo.Size = new System.Drawing.Size(128, 34);
            this.textBoxUndo.TabIndex = 335;
            this.textBoxUndo.Text = "10";
            // 
            // buttonUndo
            // 
            this.buttonUndo.BackColor = System.Drawing.Color.LightCyan;
            this.buttonUndo.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUndo.Location = new System.Drawing.Point(835, 9);
            this.buttonUndo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.Size = new System.Drawing.Size(212, 71);
            this.buttonUndo.TabIndex = 334;
            this.buttonUndo.Text = "Undo";
            this.buttonUndo.UseVisualStyleBackColor = false;
            this.buttonUndo.Click += new System.EventHandler(this.buttonUndo_Click);
            // 
            // buttonQuit
            // 
            this.buttonQuit.BackColor = System.Drawing.Color.LightCyan;
            this.buttonQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonQuit.Image = ((System.Drawing.Image)(resources.GetObject("buttonQuit.Image")));
            this.buttonQuit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonQuit.Location = new System.Drawing.Point(1508, 2);
            this.buttonQuit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(167, 80);
            this.buttonQuit.TabIndex = 332;
            this.buttonQuit.Text = "Quit";
            this.buttonQuit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonQuit.UseVisualStyleBackColor = false;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // listBoxHPGL
            // 
            this.listBoxHPGL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxHPGL.FormattingEnabled = true;
            this.listBoxHPGL.ItemHeight = 25;
            this.listBoxHPGL.Location = new System.Drawing.Point(16, 641);
            this.listBoxHPGL.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxHPGL.Name = "listBoxHPGL";
            this.listBoxHPGL.Size = new System.Drawing.Size(367, 254);
            this.listBoxHPGL.TabIndex = 331;
            this.listBoxHPGL.SelectedIndexChanged += new System.EventHandler(this.listBoxHPGL_SelectedIndexChanged);
            // 
            // checkBoxMirror
            // 
            this.checkBoxMirror.BackColor = System.Drawing.Color.LightCyan;
            this.checkBoxMirror.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMirror.Location = new System.Drawing.Point(372, 9);
            this.checkBoxMirror.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxMirror.Name = "checkBoxMirror";
            this.checkBoxMirror.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.checkBoxMirror.Size = new System.Drawing.Size(215, 74);
            this.checkBoxMirror.TabIndex = 340;
            this.checkBoxMirror.Text = "Mirror Y";
            this.checkBoxMirror.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxMirror.UseVisualStyleBackColor = false;
            // 
            // openFileDialogImage
            // 
            this.openFileDialogImage.FileName = "openFileDialog1";
            this.openFileDialogImage.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*bmp\"";
            this.openFileDialogImage.FilterIndex = 0;
            this.openFileDialogImage.RestoreDirectory = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "plt";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Plt files (*.plt)|*.plt|Pen files (*.pen)|*.pen|Hpgl files(*.hpgl)|*.hpgl\"";
            // 
            // textBoxScreenDivider
            // 
            this.textBoxScreenDivider.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxScreenDivider.Location = new System.Drawing.Point(1239, 31);
            this.textBoxScreenDivider.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxScreenDivider.Name = "textBoxScreenDivider";
            this.textBoxScreenDivider.Size = new System.Drawing.Size(109, 30);
            this.textBoxScreenDivider.TabIndex = 342;
            this.textBoxScreenDivider.Text = "1";
            this.textBoxScreenDivider.Visible = false;
            // 
            // listBoxHidden
            // 
            this.listBoxHidden.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxHidden.FormattingEnabled = true;
            this.listBoxHidden.ItemHeight = 25;
            this.listBoxHidden.Location = new System.Drawing.Point(1561, 182);
            this.listBoxHidden.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxHidden.Name = "listBoxHidden";
            this.listBoxHidden.Size = new System.Drawing.Size(380, 404);
            this.listBoxHidden.TabIndex = 346;
            this.listBoxHidden.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonWhite);
            this.groupBox1.Controls.Add(this.radioButtonViolet);
            this.groupBox1.Controls.Add(this.radioButtonBlue);
            this.groupBox1.Controls.Add(this.radioButtonGreen);
            this.groupBox1.Controls.Add(this.radioButtonYellow);
            this.groupBox1.Controls.Add(this.radioButtonOrange);
            this.groupBox1.Controls.Add(this.radioButtonRed);
            this.groupBox1.Controls.Add(this.radioButtonBrown);
            this.groupBox1.Controls.Add(this.radioButtonBlack);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 124);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(212, 497);
            this.groupBox1.TabIndex = 347;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "          Color";
            // 
            // radioButtonWhite
            // 
            this.radioButtonWhite.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonWhite.AutoSize = true;
            this.radioButtonWhite.BackColor = System.Drawing.Color.White;
            this.radioButtonWhite.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonWhite.ForeColor = System.Drawing.Color.White;
            this.radioButtonWhite.Location = new System.Drawing.Point(24, 438);
            this.radioButtonWhite.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonWhite.Name = "radioButtonWhite";
            this.radioButtonWhite.Size = new System.Drawing.Size(157, 39);
            this.radioButtonWhite.TabIndex = 16;
            this.radioButtonWhite.Text = "radiobutton2";
            this.radioButtonWhite.UseVisualStyleBackColor = false;
            this.radioButtonWhite.Click += new System.EventHandler(this.radioButtonBlack_CheckedChanged);
            // 
            // radioButtonViolet
            // 
            this.radioButtonViolet.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonViolet.AutoSize = true;
            this.radioButtonViolet.BackColor = System.Drawing.Color.Violet;
            this.radioButtonViolet.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonViolet.ForeColor = System.Drawing.Color.Violet;
            this.radioButtonViolet.Location = new System.Drawing.Point(24, 388);
            this.radioButtonViolet.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonViolet.Name = "radioButtonViolet";
            this.radioButtonViolet.Size = new System.Drawing.Size(157, 39);
            this.radioButtonViolet.TabIndex = 15;
            this.radioButtonViolet.Text = "radiobutton2";
            this.radioButtonViolet.UseVisualStyleBackColor = false;
            this.radioButtonViolet.Click += new System.EventHandler(this.radioButtonBlack_CheckedChanged);
            // 
            // radioButtonBlue
            // 
            this.radioButtonBlue.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonBlue.AutoSize = true;
            this.radioButtonBlue.BackColor = System.Drawing.Color.Blue;
            this.radioButtonBlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonBlue.ForeColor = System.Drawing.Color.Blue;
            this.radioButtonBlue.Location = new System.Drawing.Point(24, 338);
            this.radioButtonBlue.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonBlue.Name = "radioButtonBlue";
            this.radioButtonBlue.Size = new System.Drawing.Size(157, 39);
            this.radioButtonBlue.TabIndex = 14;
            this.radioButtonBlue.Text = "radiobutton2";
            this.radioButtonBlue.UseVisualStyleBackColor = false;
            this.radioButtonBlue.Click += new System.EventHandler(this.radioButtonBlack_CheckedChanged);
            // 
            // radioButtonGreen
            // 
            this.radioButtonGreen.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonGreen.AutoSize = true;
            this.radioButtonGreen.BackColor = System.Drawing.Color.LimeGreen;
            this.radioButtonGreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonGreen.ForeColor = System.Drawing.Color.LimeGreen;
            this.radioButtonGreen.Location = new System.Drawing.Point(24, 289);
            this.radioButtonGreen.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonGreen.Name = "radioButtonGreen";
            this.radioButtonGreen.Size = new System.Drawing.Size(157, 39);
            this.radioButtonGreen.TabIndex = 13;
            this.radioButtonGreen.Text = "radiobutton2";
            this.radioButtonGreen.UseVisualStyleBackColor = false;
            this.radioButtonGreen.Click += new System.EventHandler(this.radioButtonBlack_CheckedChanged);
            // 
            // radioButtonYellow
            // 
            this.radioButtonYellow.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonYellow.AutoSize = true;
            this.radioButtonYellow.BackColor = System.Drawing.Color.Yellow;
            this.radioButtonYellow.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonYellow.ForeColor = System.Drawing.Color.Yellow;
            this.radioButtonYellow.Location = new System.Drawing.Point(24, 240);
            this.radioButtonYellow.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonYellow.Name = "radioButtonYellow";
            this.radioButtonYellow.Size = new System.Drawing.Size(157, 39);
            this.radioButtonYellow.TabIndex = 12;
            this.radioButtonYellow.Text = "radiobutton2";
            this.radioButtonYellow.UseVisualStyleBackColor = false;
            this.radioButtonYellow.Click += new System.EventHandler(this.radioButtonBlack_CheckedChanged);
            // 
            // radioButtonOrange
            // 
            this.radioButtonOrange.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonOrange.AutoSize = true;
            this.radioButtonOrange.BackColor = System.Drawing.Color.DarkOrange;
            this.radioButtonOrange.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonOrange.ForeColor = System.Drawing.Color.DarkOrange;
            this.radioButtonOrange.Location = new System.Drawing.Point(25, 191);
            this.radioButtonOrange.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonOrange.Name = "radioButtonOrange";
            this.radioButtonOrange.Size = new System.Drawing.Size(157, 39);
            this.radioButtonOrange.TabIndex = 11;
            this.radioButtonOrange.Text = "radiobutton2";
            this.radioButtonOrange.UseVisualStyleBackColor = false;
            this.radioButtonOrange.Click += new System.EventHandler(this.radioButtonBlack_CheckedChanged);
            // 
            // radioButtonRed
            // 
            this.radioButtonRed.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonRed.AutoSize = true;
            this.radioButtonRed.BackColor = System.Drawing.Color.Red;
            this.radioButtonRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonRed.ForeColor = System.Drawing.Color.Red;
            this.radioButtonRed.Location = new System.Drawing.Point(24, 142);
            this.radioButtonRed.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonRed.Name = "radioButtonRed";
            this.radioButtonRed.Size = new System.Drawing.Size(157, 39);
            this.radioButtonRed.TabIndex = 10;
            this.radioButtonRed.Text = "radiobutton2";
            this.radioButtonRed.UseVisualStyleBackColor = false;
            this.radioButtonRed.Click += new System.EventHandler(this.radioButtonBlack_CheckedChanged);
            // 
            // radioButtonBrown
            // 
            this.radioButtonBrown.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonBrown.AutoSize = true;
            this.radioButtonBrown.BackColor = System.Drawing.Color.Brown;
            this.radioButtonBrown.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonBrown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonBrown.ForeColor = System.Drawing.Color.Brown;
            this.radioButtonBrown.Location = new System.Drawing.Point(24, 92);
            this.radioButtonBrown.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonBrown.Name = "radioButtonBrown";
            this.radioButtonBrown.Size = new System.Drawing.Size(157, 39);
            this.radioButtonBrown.TabIndex = 9;
            this.radioButtonBrown.Text = "radiobutton2";
            this.radioButtonBrown.UseVisualStyleBackColor = false;
            this.radioButtonBrown.Click += new System.EventHandler(this.radioButtonBlack_CheckedChanged);
            // 
            // radioButtonBlack
            // 
            this.radioButtonBlack.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonBlack.AutoSize = true;
            this.radioButtonBlack.BackColor = System.Drawing.Color.Black;
            this.radioButtonBlack.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonBlack.Location = new System.Drawing.Point(24, 43);
            this.radioButtonBlack.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonBlack.Name = "radioButtonBlack";
            this.radioButtonBlack.Size = new System.Drawing.Size(159, 39);
            this.radioButtonBlack.TabIndex = 1;
            this.radioButtonBlack.Text = "radioButton2";
            this.radioButtonBlack.UseVisualStyleBackColor = false;
            this.radioButtonBlack.Click += new System.EventHandler(this.radioButtonBlack_CheckedChanged);
            // 
            // panelBoxLines
            // 
            this.panelBoxLines.Location = new System.Drawing.Point(215, 167);
            this.panelBoxLines.Margin = new System.Windows.Forms.Padding(4);
            this.panelBoxLines.Name = "panelBoxLines";
            this.panelBoxLines.Size = new System.Drawing.Size(168, 409);
            this.panelBoxLines.TabIndex = 349;
            this.panelBoxLines.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBoxLines_Paint);
            this.panelBoxLines.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelBoxLines_MouseClick);
            // 
            // panelDraw
            // 
            this.panelDraw.BackColor = System.Drawing.Color.White;
            this.panelDraw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelDraw.Location = new System.Drawing.Point(439, 134);
            this.panelDraw.Margin = new System.Windows.Forms.Padding(4);
            this.panelDraw.Name = "panelDraw";
            this.panelDraw.Size = new System.Drawing.Size(865, 625);
            this.panelDraw.TabIndex = 350;
            this.panelDraw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelDraw_MouseDown);
            this.panelDraw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelDraw_MouseMove);
            this.panelDraw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDraw_MouseUp);
            // 
            // labelThickness
            // 
            this.labelThickness.AutoSize = true;
            this.labelThickness.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelThickness.Location = new System.Drawing.Point(236, 134);
            this.labelThickness.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelThickness.Name = "labelThickness";
            this.labelThickness.Size = new System.Drawing.Size(123, 29);
            this.labelThickness.TabIndex = 351;
            this.labelThickness.Text = "Thickness";
            // 
            // labelUndoSteps
            // 
            this.labelUndoSteps.AutoSize = true;
            this.labelUndoSteps.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUndoSteps.Location = new System.Drawing.Point(1053, 12);
            this.labelUndoSteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUndoSteps.Name = "labelUndoSteps";
            this.labelUndoSteps.Size = new System.Drawing.Size(117, 25);
            this.labelUndoSteps.TabIndex = 352;
            this.labelUndoSteps.Text = "Undo steps:";
            this.labelUndoSteps.Click += new System.EventHandler(this.buttonUndo_Click);
            // 
            // checkBoxDrawing
            // 
            this.checkBoxDrawing.BackColor = System.Drawing.Color.LightCyan;
            this.checkBoxDrawing.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxDrawing.Location = new System.Drawing.Point(595, 10);
            this.checkBoxDrawing.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxDrawing.Name = "checkBoxDrawing";
            this.checkBoxDrawing.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.checkBoxDrawing.Size = new System.Drawing.Size(233, 71);
            this.checkBoxDrawing.TabIndex = 354;
            this.checkBoxDrawing.Text = "Drawing";
            this.checkBoxDrawing.UseVisualStyleBackColor = false;
            // 
            // labelFoot
            // 
            this.labelFoot.AutoSize = true;
            this.labelFoot.BackColor = System.Drawing.SystemColors.Info;
            this.labelFoot.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFoot.Location = new System.Drawing.Point(445, 825);
            this.labelFoot.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFoot.Name = "labelFoot";
            this.labelFoot.Size = new System.Drawing.Size(756, 25);
            this.labelFoot.TabIndex = 355;
            this.labelFoot.Text = "Turn above checkbox \"Drawing\" on.   Draw freehand while left mousebutton is press" +
    "ed";
            // 
            // DrawForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 860);
            this.ControlBox = false;
            this.Controls.Add(this.labelFoot);
            this.Controls.Add(this.checkBoxDrawing);
            this.Controls.Add(this.labelUndoSteps);
            this.Controls.Add(this.labelThickness);
            this.Controls.Add(this.panelDraw);
            this.Controls.Add(this.panelBoxLines);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listBoxHidden);
            this.Controls.Add(this.textBoxScreenDivider);
            this.Controls.Add(this.checkBoxMirror);
            this.Controls.Add(this.progressBarLoad);
            this.Controls.Add(this.buttonMenu);
            this.Controls.Add(this.textBoxUndo);
            this.Controls.Add(this.buttonUndo);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.listBoxHPGL);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DrawForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "DrawForm";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBarLoad;
        private System.Windows.Forms.Button buttonMenu;
        private System.Windows.Forms.TextBox textBoxUndo;
        private System.Windows.Forms.Button buttonUndo;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.ListBox listBoxHPGL;
        private System.Windows.Forms.CheckBox checkBoxMirror;
        private System.Windows.Forms.OpenFileDialog openFileDialogImage;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBoxScreenDivider;
        private System.Windows.Forms.ListBox listBoxHidden;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonBlack;
        private System.Windows.Forms.RadioButton radioButtonBrown;
        private System.Windows.Forms.RadioButton radioButtonWhite;
        private System.Windows.Forms.RadioButton radioButtonViolet;
        private System.Windows.Forms.RadioButton radioButtonBlue;
        private System.Windows.Forms.RadioButton radioButtonGreen;
        private System.Windows.Forms.RadioButton radioButtonYellow;
        private System.Windows.Forms.RadioButton radioButtonOrange;
        private System.Windows.Forms.RadioButton radioButtonRed;
        private System.Windows.Forms.Panel panelBoxLines;
        private System.Windows.Forms.Panel panelDraw;
        private System.Windows.Forms.Label labelThickness;
        private System.Windows.Forms.Label labelUndoSteps;
        private System.Windows.Forms.CheckBox checkBoxDrawing;
        private System.Windows.Forms.Label labelFoot;
    }
}