namespace XYZ
{
    partial class XYZMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XYZMainForm));
            this.buttonSetup = new System.Windows.Forms.Button();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.buttonCut = new System.Windows.Forms.Button();
            this.buttonPlot = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonCommand = new System.Windows.Forms.Button();
            this.buttonDraw = new System.Windows.Forms.Button();
            this.buttonDrill = new System.Windows.Forms.Button();
            this.buttonDispense = new System.Windows.Forms.Button();
            this.labelZ99 = new System.Windows.Forms.Label();
            this.buttonMill = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonSetup
            // 
            this.buttonSetup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetup.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetup.Image")));
            this.buttonSetup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSetup.Location = new System.Drawing.Point(274, 151);
            this.buttonSetup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSetup.Name = "buttonSetup";
            this.buttonSetup.Size = new System.Drawing.Size(433, 90);
            this.buttonSetup.TabIndex = 0;
            this.buttonSetup.Text = "Setup";
            this.buttonSetup.UseVisualStyleBackColor = false;
            this.buttonSetup.Click += new System.EventHandler(this.SetupButton_Click);
            // 
            // buttonQuit
            // 
            this.buttonQuit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonQuit.Image = ((System.Drawing.Image)(resources.GetObject("buttonQuit.Image")));
            this.buttonQuit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonQuit.Location = new System.Drawing.Point(274, 527);
            this.buttonQuit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(433, 90);
            this.buttonQuit.TabIndex = 1;
            this.buttonQuit.Text = "Quit";
            this.buttonQuit.UseVisualStyleBackColor = false;
            this.buttonQuit.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // buttonCut
            // 
            this.buttonCut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonCut.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCut.Image = ((System.Drawing.Image)(resources.GetObject("buttonCut.Image")));
            this.buttonCut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCut.Location = new System.Drawing.Point(713, 245);
            this.buttonCut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCut.Name = "buttonCut";
            this.buttonCut.Size = new System.Drawing.Size(433, 90);
            this.buttonCut.TabIndex = 49;
            this.buttonCut.Text = "Cut";
            this.buttonCut.UseVisualStyleBackColor = false;
            this.buttonCut.Click += new System.EventHandler(this.buttonCut_Click);
            // 
            // buttonPlot
            // 
            this.buttonPlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonPlot.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPlot.Image = ((System.Drawing.Image)(resources.GetObject("buttonPlot.Image")));
            this.buttonPlot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPlot.Location = new System.Drawing.Point(713, 151);
            this.buttonPlot.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPlot.Name = "buttonPlot";
            this.buttonPlot.Size = new System.Drawing.Size(433, 90);
            this.buttonPlot.TabIndex = 50;
            this.buttonPlot.Text = "Plotter";
            this.buttonPlot.UseVisualStyleBackColor = false;
            this.buttonPlot.Click += new System.EventHandler(this.buttonComponentPlacing_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonHelp
            // 
            this.buttonHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHelp.Image = ((System.Drawing.Image)(resources.GetObject("buttonHelp.Image")));
            this.buttonHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonHelp.Location = new System.Drawing.Point(274, 433);
            this.buttonHelp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(433, 90);
            this.buttonHelp.TabIndex = 53;
            this.buttonHelp.Text = "Help";
            this.buttonHelp.UseVisualStyleBackColor = false;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // buttonCommand
            // 
            this.buttonCommand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonCommand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCommand.Image = ((System.Drawing.Image)(resources.GetObject("buttonCommand.Image")));
            this.buttonCommand.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCommand.Location = new System.Drawing.Point(274, 245);
            this.buttonCommand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCommand.Name = "buttonCommand";
            this.buttonCommand.Size = new System.Drawing.Size(433, 90);
            this.buttonCommand.TabIndex = 54;
            this.buttonCommand.Text = "Command";
            this.buttonCommand.UseVisualStyleBackColor = false;
            this.buttonCommand.Click += new System.EventHandler(this.buttonCommand_Click);
            // 
            // buttonDraw
            // 
            this.buttonDraw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonDraw.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDraw.Image = ((System.Drawing.Image)(resources.GetObject("buttonDraw.Image")));
            this.buttonDraw.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDraw.Location = new System.Drawing.Point(274, 339);
            this.buttonDraw.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonDraw.Name = "buttonDraw";
            this.buttonDraw.Size = new System.Drawing.Size(433, 90);
            this.buttonDraw.TabIndex = 56;
            this.buttonDraw.Text = "Draw";
            this.buttonDraw.UseVisualStyleBackColor = false;
            this.buttonDraw.Click += new System.EventHandler(this.buttonDraw_Click);
            // 
            // buttonDrill
            // 
            this.buttonDrill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonDrill.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDrill.Image = ((System.Drawing.Image)(resources.GetObject("buttonDrill.Image")));
            this.buttonDrill.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDrill.Location = new System.Drawing.Point(713, 339);
            this.buttonDrill.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonDrill.Name = "buttonDrill";
            this.buttonDrill.Size = new System.Drawing.Size(433, 90);
            this.buttonDrill.TabIndex = 57;
            this.buttonDrill.Text = "Boren";
            this.buttonDrill.UseVisualStyleBackColor = false;
            this.buttonDrill.Click += new System.EventHandler(this.buttonDrill_Click);
            // 
            // buttonDispense
            // 
            this.buttonDispense.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonDispense.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDispense.Image = ((System.Drawing.Image)(resources.GetObject("buttonDispense.Image")));
            this.buttonDispense.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDispense.Location = new System.Drawing.Point(713, 433);
            this.buttonDispense.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonDispense.Name = "buttonDispense";
            this.buttonDispense.Size = new System.Drawing.Size(433, 90);
            this.buttonDispense.TabIndex = 58;
            this.buttonDispense.Text = "Dispense";
            this.buttonDispense.UseVisualStyleBackColor = false;
            this.buttonDispense.Click += new System.EventHandler(this.buttonDispense_Click);
            // 
            // labelZ99
            // 
            this.labelZ99.AutoSize = true;
            this.labelZ99.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZ99.ForeColor = System.Drawing.Color.Green;
            this.labelZ99.Location = new System.Drawing.Point(273, 37);
            this.labelZ99.Name = "labelZ99";
            this.labelZ99.Size = new System.Drawing.Size(179, 91);
            this.labelZ99.TabIndex = 59;
            this.labelZ99.Text = "Z99";
            // 
            // buttonMill
            // 
            this.buttonMill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonMill.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMill.Image = ((System.Drawing.Image)(resources.GetObject("buttonMill.Image")));
            this.buttonMill.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMill.Location = new System.Drawing.Point(713, 527);
            this.buttonMill.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonMill.Name = "buttonMill";
            this.buttonMill.Size = new System.Drawing.Size(433, 90);
            this.buttonMill.TabIndex = 60;
            this.buttonMill.Text = "Mill";
            this.buttonMill.UseVisualStyleBackColor = false;
            this.buttonMill.Click += new System.EventHandler(this.buttonMill_Click);
            // 
            // XYZMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1557, 932);
            this.Controls.Add(this.buttonMill);
            this.Controls.Add(this.labelZ99);
            this.Controls.Add(this.buttonDispense);
            this.Controls.Add(this.buttonDrill);
            this.Controls.Add(this.buttonDraw);
            this.Controls.Add(this.buttonCommand);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.buttonPlot);
            this.Controls.Add(this.buttonCut);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.buttonSetup);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "XYZMainForm";
            this.Text = "Draw - Plot - Cut -Drill";
            this.Load += new System.EventHandler(this.XYZMainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSetup;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.Button buttonCut;
        private System.Windows.Forms.Button buttonPlot;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Button buttonCommand;
        private System.Windows.Forms.Button buttonDraw;
        private System.Windows.Forms.Button buttonDrill;
        private System.Windows.Forms.Button buttonDispense;
        private System.Windows.Forms.Label labelZ99;
        private System.Windows.Forms.Button buttonMill;
    }
}

