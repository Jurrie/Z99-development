 
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace XYZ
{
    public partial class CutForm : Form
    {
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool Ellipse(IntPtr hdc, int x1, int y1, int x2, int y2);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool Rectangle(IntPtr hdc, int X1, int Y1, int X2, int Y2);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern IntPtr MoveToEx(IntPtr hdc, int x, int y, IntPtr lpPoint);
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool LineTo(IntPtr hdc, int x, int y);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern IntPtr CreatePen(PenStyles enPenStyle, int nWidth, int crColor);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern IntPtr CreateSolidBrush(BrushStyles enBrushStyle, int crColor);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern int SetTextColor(IntPtr hDC, int crColor);

        [DllImport("gdi32.dll")]
        public static extern bool TextOut(IntPtr hdc, int nXStart, int nYStart, string lpString,
        int cbString);

      
        public struct PlotPoints
        {
            public int Xpoint;
            public int Ypoint;
            public int Mode;  // 1= draw line, PD pen down, 0 = PU pen up while moving to this point            
            public int Thickness; // = width
            public int PenNr;  // = color
        }
             
        public static int maxVectors = 100000;  //       is necessary for large pcb's eg feeder pcb top has 49500 lines
        public static int vectorIndex = 0;  // count for vectors in use        

        public static PlotPoints[] MyCutPoints = new PlotPoints[maxVectors];

        // used for drawing on the screen :
        public static float outputScreenDivider = 1.0F;      

       // depends  of  paper or vinyl output format :
        public static float outputCutDivider = 1.0F;     
        
        private delegate void ObjectDelegate(object obj);

        XYZR MyXYZR = new XYZR();

        public static Boolean cutterUp = true;  // start default with pen up.

        public static int fileInUse=0;     //0 = no previous HPGL file loaded in draw module                
        public static int penThickness = 1;
        public static int selectedPen = 1;
        
        public static int globalMaximumX, globalMaximumY;
        public static int globalMinimumX, globalMinimumY;

        public static bool started = false;
      
        public CutForm()
        {
            InitializeComponent();
                       
            MyXYZR.InitSerialPort();

            this.Top = -1;
            this.Left = -1;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;    //  - 20;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;  //  - 50;           

            pictureBox1.Top = listBoxHPGL.Top;
            pictureBox1.Left = listBoxHPGL.Left + listBoxHPGL.Width + 20;
            pictureBox1.Width = this.Width - (listBoxHPGL.Width + 50);
            pictureBox1.Height = this.Height - (pictureBox1.Top + 75);
            
            clearCutPoints();
            
            Color ButtonColor = Functions.kleur(XYZMainForm.IconColor);
            this.BackColor = Functions.kleur(XYZMainForm.FormColor);
            buttonMenu.BackColor = ButtonColor;

            int btW = this.Width / 17; //18
            int btM = 4;
            int btH = this.Height / 11;//12                    

            buttonMenu.Left = btM;
            buttonMenu.Top = btM;
            buttonMenu.Width = btW;
            buttonMenu.Height = btH;

            progressBarLoad.Left = btM + btW + btM;
            progressBarLoad.Top = btM;
            progressBarLoad.Width = btW * 5 / 4;
            progressBarLoad.Height = btH / 2 - 4;

            groupBoxRGB.Left = btM + btW + btM;
            groupBoxRGB.Top = btM + buttonMenu.Height / 2 - 2;
            groupBoxRGB.Width = btW * 5 / 4;
            groupBoxRGB.Height = btH / 2 + 2;

            groupBoxRGB.BackColor = ButtonColor;

            checkBoxMirror.Left = progressBarLoad.Left + progressBarLoad.Width + btM;
            checkBoxMirror.Top = btM;   //
            checkBoxMirror.Width = btW;
            checkBoxMirror.Height = btH / 2;

            checkBoxBorderCut.Left = checkBoxMirror.Left;
            checkBoxBorderCut.Top = btM + btH / 2;
            checkBoxBorderCut.Width = btW;
            checkBoxBorderCut.Height = btH / 2;
           
            buttonOutput.Left = checkBoxMirror.Left + btW + btM;
            buttonOutput.Top = btM;
            buttonOutput.Width = btW;
            buttonOutput.Height = btH;
                        
            tableLayoutPanelDimXY.Left = buttonOutput.Left + btW + btM;
            tableLayoutPanelDimXY.Top = btM * 3;
            tableLayoutPanelDimXY.Width = btW + btM * 3;
            tableLayoutPanelDimXY.Height = btH - btM;
            
            panelMaxLabels.Left = buttonOutput.Left + btW + btM;
            panelMaxLabels.Top = btM;
            panelMaxLabels.Width = btW + btM * 3;
            panelMaxLabels.Height = btH;
            
            tableLayoutPanelLaser.Left = tableLayoutPanelDimXY.Left + tableLayoutPanelDimXY.Width + btM;  // panelMaxEdit.Left + panelMaxEdit.Width + btM;
            tableLayoutPanelLaser.Width = btW*5/4;
            tableLayoutPanelLaser.Height = btH;
            tableLayoutPanelLaser.Top = btM;
            
            buttonStart.Left = tableLayoutPanelLaser.Left + tableLayoutPanelLaser.Width; // panelMaxEdit.Left + btW *3 / 2 + btM;
            buttonStart.Top = btM;
            buttonStart.Width = btW;
            buttonStart.Height = btH;

            checkBoxPause.Left = buttonStart.Left + btW + btM;
            checkBoxPause.Top = btM;
            checkBoxPause.Width = btW;
            checkBoxPause.Height = btH;

            tableLayoutPanel1.Left = checkBoxPause.Left + btW + btM;
            tableLayoutPanel1.Top = 0;
            tableLayoutPanel1.Height = btH + btM;
            tableLayoutPanel1.Width = (btW * 22) / 4;

            tableLayoutPanel2.Left = tableLayoutPanel1.Left + (btW * 22) / 4 + btM;
            tableLayoutPanel2.Top = 0;
            tableLayoutPanel2.Height = btH + btM;
            tableLayoutPanel2.Width = btW * 21 / 16; //21 /16
                      
            buttonQuit.Left =tableLayoutPanel2.Left + tableLayoutPanel2.Width + btM;
            buttonQuit.Top = btM;
            buttonQuit.Width = btW;
            buttonQuit.Height = btH;

            listBoxHPGL.Left = btM;
            listBoxHPGL.Top = btM*3 + btH;
            listBoxHPGL.Width = btW * 3;
            listBoxHPGL.Height = this.Height / 2;

            textBoxReceive.Left = btM;
            textBoxReceive.Top = listBoxHPGL.Top + this.Height / 2 + btM;
            textBoxReceive.Width = listBoxHPGL.Width;
            textBoxReceive.Height = this.Height / 3;

            pictureBox1.Top = listBoxHPGL.Top;
            pictureBox1.Left = listBoxHPGL.Left + listBoxHPGL.Width + btM;
            pictureBox1.Height = this.Height - (btH + btM * 12);
            pictureBox1.Width = this.Width - (listBoxHPGL.Width + btM * 8);           

            buttonOutput.BackColor = ButtonColor;
           
            buttonStart.BackColor = ButtonColor;
            buttonQuit.BackColor = ButtonColor;
            checkBoxMirror.BackColor = ButtonColor;
            checkBoxBorderCut.BackColor = ButtonColor;
            checkBoxPause.BackColor = ButtonColor;
            
            pictureBox1.BackColor = Functions.kleur(XYZMainForm.FondColor);
            
            comboBoxXYTravel.Text = XYZMainForm.cutXYTravelSpeed.ToString();
            comboBoxXYWork.Text = XYZMainForm.cutXYWorkSpeed.ToString();

            comboBoxZup.Text = XYZMainForm.cutZUpSpeed.ToString();
            comboBoxZdown.Text = XYZMainForm.cutZDownSpeed.ToString();
           
            textBoxZupPosition.Text = XYZMainForm.cutZUpPosition.ToString("##.####");
            textBoxZdownPosition.Text = XYZMainForm.cutZDownPosition.ToString("##.####");
            
            buttonMenu.Text = XYZMainForm.StrMenu;         
            buttonQuit.Text = XYZMainForm.StrQuit;            
            checkBoxPause.Text = XYZMainForm.StrPause;
            
           textBoxEditX.Text = XYZMainForm.CustomDimX.ToString("###.###");
           textBoxEditY.Text = XYZMainForm.CustomDimY.ToString("###.###");                       

          if (XYZMainForm.TheScaledOutput < 11)
            {               
                tableLayoutPanelDimXY.Visible = false;
                panelMaxLabels.Visible = true;

                switch (XYZMainForm.TheScaledOutput)
                {                   
                    case 1:
                        labelMaxXmm.Text = "A1 landscape";
                        labelMaxYmm.Text = "840 * 594 mm";
                        break;
                    case 2:
                        labelMaxXmm.Text = "A1 portrait";
                        labelMaxYmm.Text = "594 * 840 mm";
                        break;
                    case 3:
                        labelMaxXmm.Text = "A2 landscape";
                        labelMaxYmm.Text = "594 * 420 mm";
                        break;
                    case 4:
                        labelMaxXmm.Text = "A2 landscape";
                        labelMaxYmm.Text = "420 * 594 mm";
                        break;
                    case 5:
                        labelMaxXmm.Text = "A3 landscape";
                        labelMaxYmm.Text = "420 * 297 mm";
                        break;
                    case 6:
                        labelMaxXmm.Text = "A3 portrait";
                        labelMaxYmm.Text = "297 * 420 mm";
                        break;
                    case 7:
                        labelMaxXmm.Text = "A4 landscape";
                        labelMaxYmm.Text = "297 * 210 mm";
                        break;
                    case 8:
                        labelMaxXmm.Text = "A4 portrait";
                        labelMaxYmm.Text = "210 * 297 mm";
                        break;
                    case 9:
                        labelMaxXmm.Text = "A5 landscape";
                        labelMaxYmm.Text = "210 * 148 mm";
                        break;
                    case 10:
                        labelMaxXmm.Text = "A5 portrait";
                        labelMaxYmm.Text = "148 * 210 mm";
                        break;
                        
                    default: break;
                }
            }
            if (XYZMainForm.TheScaledOutput == 11)
              {
                panelMaxLabels.Visible = false;                       
                tableLayoutPanelDimXY.Visible = true;
               }
          
            labelXmm.Text = "X" + XYZMainForm.globalActualMmPosX.ToString("###.##") + "mm";  // ((float)XYZMainForm.globalActualMicrostepsX / 200).ToString("###.###");
            labelYmm.Text = "Y" + XYZMainForm.globalActualMmPosY.ToString("###.##") + "mm"; //  ((float)XYZMainForm.globalActualMicrostepsY / 200).ToString("###.###");
            labelZmm.Text = "Z" + XYZMainForm.globalActualMmPosZ.ToString("##.##") + "mm"; // ((float)XYZMainForm.globalActualMicrostepsZ / 200).ToString("###.###");

        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {          
            MyXYZR.SetLaser(1);  // 1 = off !!
            
            XYZMainForm.CustomDimX = float.Parse(textBoxEditX.Text);
            XYZMainForm.CustomDimY = float.Parse(textBoxEditY.Text);

            // XY speed
            try { XYZMainForm.cutXYTravelSpeed = int.Parse(comboBoxXYTravel.Text); }
            catch { XYZMainForm.cutXYTravelSpeed = 25; }
            try { XYZMainForm.cutXYWorkSpeed = int.Parse(comboBoxXYWork.Text); }
            catch { XYZMainForm.cutXYWorkSpeed = 10; }

            // Z speed           
            XYZMainForm.cutZUpSpeed = int.Parse(comboBoxZup.Text);
            XYZMainForm.cutZDownSpeed = int.Parse(comboBoxZdown.Text);

            // Z position
            XYZMainForm.cutZUpPosition = float.Parse(textBoxZupPosition.Text);
            XYZMainForm.cutZDownPosition = float.Parse(textBoxZdownPosition.Text);

            MyXYZR.CloseSerialPort();
                       
            this.Close();
        }

        private void buttonMenu_MouseClick(object sender, MouseEventArgs e)
        {            
            if (e.Button == MouseButtons.Left)
            {
                progressBarLoad.Value = 0; // is always ok then                
                ContextMenuStrip ctx = new ContextMenuStrip();
                ctx.Font = new Font("Arial", 13);

                if (XYZMainForm.GlobalHPGLFileName.Length > 4)
                {                   
                    ctx.Items.Add(XYZMainForm.StrReopen+"  " + XYZMainForm.GlobalHPGLFileName, null, buttonOpenPrevious_Click);        // "Reopen"    
                    ctx.Items.Add("_________________________", null, null);
                }
                
                ctx.Items.Add(XYZMainForm.StrOpenFile, null, buttonOpenHpgl_Click); // "Open HPGL"              
                ctx.Items.Add("Open *.plt file", null, buttonOpenPlt_Click); // "PCB"
                ctx.Items.Add("_________________________", null, null);
                
                ctx.Items.Add(XYZMainForm.StrClearAll, null, buttonClearAll_Click);  // "Clear all"
                ctx.Items.Add("_________________________", null, null);

                ctx.Items.Add(XYZMainForm.StrReset+ " cutter", null, buttonReset_Click); // "Reset cutter"
                ctx.Items.Add("_________________________", null, null);

                if (listBoxHPGL.Items.Count > 2)
                {
                    if (checkBoxLaser.Checked==false)
                    ctx.Items.Add(XYZMainForm.StrStartPlot, null, buttonPlot_Click);  //  Start cutter"
                    else
                     ctx.Items.Add("Start laser", null, buttonPlot_Click);

                    ctx.Items.Add("_________________________", null, null);
                }

                ctx.Items.Add( XYZMainForm.StrQuit , null, buttonQuit_Click);
                ctx.Show(this, new Point(buttonMenu.Left, buttonMenu.Top + buttonMenu.Height + 10));
            }
        }
        
     
        private void buttonReset_Click(object sender, EventArgs e)
        {                    
            MyXYZR.NextCommand = true;                    //  anyway !!             
            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextboxReceive);
            XYZMainForm.SerialPortReturnS = "";
            XYZMainForm.SerialPortSendS = "";
            MyXYZR.Reset(sender, e);
            delListbox.Invoke(XYZMainForm.SerialPortSendS);
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
        }


        private void buttonClearAll_Click(object sender, EventArgs e)
        {         
            pictureBox1.Image = null;
            listBoxHPGL.Items.Clear();
            textBoxReceive.Clear();
            Graphics g = pictureBox1.CreateGraphics();
            Pen p = new Pen(Color.Green, 2);           
            g.Clear(Functions.kleur(XYZMainForm.FondColor));          
            clearCutPoints();           
            g.Dispose();
            buttonStart.Visible = false;
        }


        private void clearCutPoints()
        {
            for (int x = 0; x < vectorIndex; x++)          //
            {
                MyCutPoints[x].Xpoint = 0;
                MyCutPoints[x].Ypoint = 0;
                MyCutPoints[x].Mode = 0;
            }
        }


        private void buttonExecuteHPGL_Click(object sender, EventArgs e)
        {
            ObjectDelegate deltr = new ObjectDelegate(UpdateTextboxReceive);
            String S = "";
            int x = 0, direction = 0; ;
            float xVal = 0.0F, yVal = 0.0F, oldXval = 0.0F, oldYval = 0.0F;

            float checkValueX = 0, checkValueY = 0;
            int kleur = 0, thickness = 2;
            int Ydim = pictureBox1.Height;                

      //    Calculation of output, paper, vinyl dimensions:
            CalculateOutputCutDivider(sender, e); //  is once more necessary: after loading the file, the user may scale the output

            progressBarLoad.Value = 0;

            Graphics g = pictureBox1.CreateGraphics();            
            Pen p = new Pen(Color.Blue, 4);  //
            IntPtr hdc = g.GetHdc();

            MyXYZR.SetZSpeed(XYZMainForm.cutZUpSpeed);  // upwards

            MyXYZR.ZAction(sender, e, XYZMainForm.cutZUpPosition);                 

            DisplayAbsoluteZCoordinates(sender, e);
            cutterUp = true;
            
            // set XY travel speed:                    
            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYTravel.Text));
                    
            started = true; 
            
            float margeX = (float)XYZMainForm.GlobalMargeX;
            float margeY = (float)XYZMainForm.GlobalMargeY;

            checkValueX = XYZMainForm.MyCommonCoordinates.xTotalGround;
            checkValueY = XYZMainForm.MyCommonCoordinates.yTotalGround;


       // MessageBox.Show("checkValueX " + checkValueX.ToString() + " checkValueY " + checkValueY.ToString()  );




            int total = listBoxHPGL.Items.Count - 1;

            // USED FOR OPTION TO NOT CUT BORDERS :
                      
            float minimumX = 1000000.0F;
            float minimumY = 1000000.0F;
            float maximumX = -1000000.0F;
            float maximumY = -1000000.0F;
            
            int passes = 1;
            float depthZ = 0.0F;

            if (checkBoxLaser.Checked) passes = int.Parse(comboBoxLaserPasses.Text);


            int progressBarActualVal = 0;
            progressBarLoad.Value = 0;
            
            oldXval = XYZMainForm.globalActualMmPosX;
            oldYval = XYZMainForm.globalActualMmPosY;

             int vectorCount = 0;

            x = 1;
            while (x < maxVectors)
            {
                if ((MyCutPoints[x].Xpoint == 0) && (MyCutPoints[x].Ypoint == 0)) break;
                x++;
            }

            vectorCount = x - 1;


            for (x = 1; x < vectorCount; x++)
            {
                if (minimumX > MyCutPoints[x].Xpoint) minimumX = MyCutPoints[x].Xpoint;
                if (minimumY > MyCutPoints[x].Ypoint) minimumY = MyCutPoints[x].Ypoint;

                if (maximumX < MyCutPoints[x].Xpoint) maximumX = MyCutPoints[x].Xpoint;
                if (maximumY < MyCutPoints[x].Ypoint) maximumY = MyCutPoints[x].Ypoint;
            }

            minimumX = minimumX / outputCutDivider;
            minimumY = minimumY / outputCutDivider;   
            maximumX = maximumX / outputCutDivider;
            maximumY = maximumY / outputCutDivider;

            float percentX = (maximumX - minimumX) / 50;  // 2 % from border will not be cut
            float percentY = (maximumY - minimumY) / 50;  // 2 % from border will not be cut
                        

            for (int y = 0; y < passes; y++)    // maxmost 10 passes
            {
                if ((y % 3) == 0) radioButtonR.Checked = true;
                else if ((y % 3) == 1) radioButtonG.Checked = true;
                else radioButtonB.Checked = true;           

                try
                { depthZ = float.Parse(textBoxDepthZ.Text); }
                catch
                { depthZ = 0.0F; }
                MyXYZR.SetZSpeed(XYZMainForm.cutZDownSpeed); // down speed

                XYZMainForm.cutZDownPosition = float.Parse(textBoxZdownPosition.Text) + depthZ * y;
                MyXYZR.ZAction(sender, e, XYZMainForm.cutZDownPosition);

                DisplayAbsoluteZCoordinates(sender, e);

           for (x = 0; x < vectorCount; x++)   
          
                {
                    // from  time to time upgrade progressbar
                    if ((x % 50) == 0)
                    {
                        progressBarActualVal = (y * progressBarLoad.Maximum) / passes
                                             + (x * progressBarLoad.Maximum) / (vectorIndex * passes);
                        if (progressBarActualVal < progressBarLoad.Maximum)
                            progressBarLoad.Value = progressBarActualVal;
                    }

                if (x<(listBoxHPGL.Items.Count-1)) listBoxHPGL.SelectedIndex = x;

                 
                    if (checkBoxPause.Checked == true)
                    {
                        Application.DoEvents();
                        x = x - 1;
                        continue;
                    }

                    if (radioButtonR.Checked) kleur = 255;            // red
                    else if (radioButtonG.Checked) kleur = 255 << 8;    // green
                    else if (radioButtonB.Checked) kleur = 255 << 16;   // blue

                    thickness = MyCutPoints[x].Thickness + 1; // thicker  

                    xVal = ((float)MyCutPoints[x].Xpoint / outputCutDivider) + margeX;
                    yVal = ((float)MyCutPoints[x].Ypoint / outputCutDivider) + margeY;

                    //    BELOW PROTECTION AGAINS EXCESSIVE X AND Y hardware movements:   
                    // MAKE SURE THAT BORDERS ARE NOT CUT!

                    // BUG BELOW !!!

                    /****************************************************/

                //    MessageBox.Show("xVal " + xVal.ToString() + "  yVal   "+ yVal.ToString()  );


                   /*
                    if (xVal > checkValueX) continue;
                    if (yVal > checkValueY) continue;
                    if (xVal < margeX) continue;
                    if (yVal < margeY) continue;
                   */
                    //   MAKE SURE THAT BORDERS ARE NOT CUT! 
                    if (checkBoxBorderCut.Checked == false)
                    {
                        if (xVal <= (margeX + minimumX + percentX)) continue; 
                        if (yVal <= (margeY + minimumY + percentY)) continue;
                        if (xVal >= (margeX + maximumX - percentX)) continue;
                        if (yVal >= (margeY + maximumY - percentY)) continue;
                    }

                    /***************************************************/

                    S = "x " + xVal.ToString("") + " y " + yVal.ToString("")+" mode"+ MyCutPoints[x].Mode.ToString();
                    deltr.Invoke(S);
                    
                    if (MyCutPoints[x].Mode == 1)
                    {
                        IntPtr hObject = CreatePen(PenStyles.PS_SOLID, thickness, kleur);   
                        SelectObject(hdc, hObject);
                        MoveToEx(hdc, (int)(MyCutPoints[x - 1].Xpoint * 1 / outputScreenDivider),
                                  (int)(MyCutPoints[x - 1].Ypoint * 1 / outputScreenDivider), IntPtr.Zero);
                        LineTo(hdc, (int)(MyCutPoints[x].Xpoint * 1 / outputScreenDivider),
                                  (int)(MyCutPoints[x].Ypoint * 1 / outputScreenDivider));
                        DeleteObject(hObject);
                    }


            if (MyCutPoints[x].Mode == 0)    // Z  up
                    {  
                            // LASER
                            if (checkBoxLaser.Checked == false)
                            {
                                MyXYZR.SetZSpeed(XYZMainForm.cutZUpSpeed); // upwards Z speed                                                 

                            XYZMainForm.cutZUpPosition = float.Parse(textBoxZupPosition.Text);
                                MyXYZR.ZAction(sender, e, XYZMainForm.cutZUpPosition);

                            DisplayAbsoluteZCoordinates(sender, e);
                            }
                            else   // Turn laser off
                            {
                                MyXYZR.SetLaser(1);  // 1 = off !!                 
                        }

                            cutterUp = true;
                            // set XY travel speed:                                       
                            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYTravel.Text));
                     
                    }
                    else if (MyCutPoints[x].Mode == 1)    // cutter should become down
                    {
                        if (cutterUp == true)   // cutter is up
                        {                          
                            // LASER
                            if (checkBoxLaser.Checked == false)
                            {
                                MyXYZR.SetZSpeed(XYZMainForm.cutZDownSpeed); // Z down speed
                                                          
                                XYZMainForm.cutZDownPosition = float.Parse(textBoxZdownPosition.Text);
                                MyXYZR.ZAction(sender, e, XYZMainForm.cutZDownPosition);
                                                         
                                DisplayAbsoluteZCoordinates(sender, e);
                            }
                            else   // Turn laser ON
                            {
                                MyXYZR.SetLaser(0); // 0 = on       
                            }

                            cutterUp = false; // cutter is down

                            // set XY work speed:                     
                            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYWork.Text));    
                        }
                    }

                   direction = 0;
                    if ((xVal >= oldXval) && (yVal > oldYval)) direction = 1;       //   
                    else if ((xVal > oldXval) && (yVal <= oldYval)) direction = 3;  //  
                    else if ((xVal <= oldXval) && (yVal < oldYval)) direction = 5;   //                        
                    else if ((xVal < oldXval) && (yVal >= oldYval)) direction = 7;   //  


                    if (x < 2) MyXYZR.SetLaser(1); // set laser of in begin, otherwise diagonal line to first coordinate !


                    if (direction > 0)
                        MyXYZR.XYAction(sender, e, direction, Math.Abs(xVal - oldXval), Math.Abs(yVal - oldYval), false);


                    DisplayXYCoordinates(sender, e);

                    oldXval = xVal;
                    oldYval = yVal;                                                    
                }           //  end for x


            }               // end for y number of passes
            g.ReleaseHdc(hdc);
            g.Dispose();

            //  Cutter to upmost Z position:

                MyXYZR.SetZSpeed(XYZMainForm.cutZUpSpeed);

                MyXYZR.ZAction(sender, e, XYZMainForm.cutZUpPosition);
              
                DisplayAbsoluteZCoordinates(sender, e);
        
            if (checkBoxLaser.Checked)
                {
                    MyXYZR.SetLaser(1);     // Turn laser off   
            }

            cutterUp = true;            
            progressBarLoad.Value = progressBarLoad.Maximum;

            // back to Origin at travel speed:                    

            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYTravel.Text));

            MyXYZR.XYAction(sender, e, 5, XYZMainForm.globalActualMmPosX, XYZMainForm.globalActualMmPosY, false);

            DisplayXYCoordinates(sender, e);
            // controls visible
            tableLayoutPanel1.Visible = true;

        }

        private void UpdateTextboxReceive(object obj)
        {
            if (InvokeRequired)
            {
                ObjectDelegate method = new ObjectDelegate(UpdateTextboxReceive);
                Invoke(method, obj);
                return;
            }
            string S = (string)obj;
            textBoxReceive.AppendText(S + "\r\n");
        }
        
        private void buttonOpenHpgl_Click(object sender, EventArgs e)
        {
            String S = "", S1 = "", S2 = "", Stemp = "";
            String sFile = "";
            int TheIndex = 0;      
            int SplitIndex = 0;
    
            openFileDialog1.Filter =
                "Hpgl files(*.hpgl)|*.hpgl|Plt files (*.plt)|*.plt|Pen files(*.pen) | *.pen";
                     
            openFileDialog1.FilterIndex = 0; // 2
            openFileDialog1.FileName = "*.hpgl";  // *.hpgl";
            openFileDialog1.RestoreDirectory = true;

            ObjectDelegate del = new ObjectDelegate(UpdateListbox);
            ObjectDelegate del1 = new ObjectDelegate(UpdateHiddenListbox);

            listBoxHPGL.Items.Clear();  // ?
            listBoxHidden.Items.Clear();

            progressBarLoad.Value = 0;
          
            vectorIndex = 0;
            penThickness = 1;            // = important!
            
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                sFile = openFileDialog1.FileName;
                    if (File.Exists(sFile))
                    {
                    this.Text = sFile;
                        using (StreamReader sr = new StreamReader(sFile))
                        {
                            while (sr.EndOfStream == false)
                            {
                                S = sr.ReadLine();
                                Stemp = S;
                                TheIndex = 0;
                              
                                //       if comment with // is allowed, strip it:
                                if (Stemp.Contains("//"))
                                {
                                    TheIndex = S.IndexOf("//");
                                    if (TheIndex > 0) Stemp = Stemp.Remove(TheIndex, Stemp.Length - TheIndex);
                                }
                                Stemp.Trim();
                               
                                //      if S contains various ';' signs, splitting is necessary
                                //      so that each line in the listbox contains one statement 
                                do
                                {
                                    SplitIndex = 0;
                                    SplitIndex = Stemp.IndexOf(';');
                                    S1 = Stemp.Substring(0, SplitIndex + 1);
                                   
                                    //   We put all statements,ending with ; in the hidden listbox
                                    del1.Invoke(S1);  // del1 =  to hidden listbox
                                    S2 = Stemp.Substring(SplitIndex + 1);  // rest
                                    S2.Trim();
                                    if (S2.Length < 4) break;
                                    Stemp = S2;
                                }
                                while (true);
                            }    //    EOF
                        }        //    end of stream
            XYZMainForm.GlobalHPGLFileName = sFile;       
            buttonCommonPart_Click(sender, e);
            progressBarLoad.Value = progressBarLoad.Maximum;
            buttonStart.Visible = true;
            DrawArrayXY(sender, e);                    
        }         // file exists
    }   // openfiledialog ok                
}


        private void buttonOpenPlt_Click(object sender, EventArgs e)   
        {
            String S = "", S1 = "", S2 = "", Stemp = "";
            String sFile = "";
            int TheIndex = 0;         
            int SplitIndex = 0;
          
            openFileDialog1.Filter =  "Plt files (*.plt)|*.plt";
            openFileDialog1.FileName = "*.plt";
         
            openFileDialog1.FilterIndex = 0; 
   
            openFileDialog1.RestoreDirectory = true;
          
            ObjectDelegate del = new ObjectDelegate(UpdateListbox);
            ObjectDelegate del1 = new ObjectDelegate(UpdateHiddenListbox);

            listBoxHPGL.Items.Clear();  // ?
            listBoxHidden.Items.Clear();

            progressBarLoad.Value = 0;
            
            vectorIndex = 0;
            penThickness = 1;            

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sFile = openFileDialog1.FileName ;
                if (File.Exists(sFile))
                {
                    this.Text = sFile;
                    using (StreamReader sr = new StreamReader(sFile))
                    {
                        while (sr.EndOfStream == false)
                        {
                            S = sr.ReadLine();
                            Stemp = S;
                            TheIndex = 0;
                           
                            //       if comment with // is allowed, strip it:
                            if (Stemp.Contains("//"))
                            {
                                TheIndex = S.IndexOf("//");
                                if (TheIndex > 0) Stemp = Stemp.Remove(TheIndex, Stemp.Length - TheIndex);
                            }
                            Stemp.Trim();
                          
                            //      if S contains various ';' signs, splitting is necessary
                            //      so that each line in the listbox contains one statement 
                            do
                            {
                                SplitIndex = 0;
                                SplitIndex = Stemp.IndexOf(';');
                                S1 = Stemp.Substring(0, SplitIndex + 1);                                
                                //   We put all statements,ending with ; in the hidden listbox
                                del1.Invoke(S1);  // del1 =  to hidden listbox
                                S2 = Stemp.Substring(SplitIndex + 1);  // rest
                                S2.Trim();
                                if (S2.Length < 4) break;
                                Stemp = S2;
                            }
                            while (true);
                        }    //    EOF
                    }        //    end of stream

                    XYZMainForm.GlobalHPGLFileName = sFile;

                    buttonCommonPart_Click(sender, e);
                    progressBarLoad.Value = progressBarLoad.Maximum;
                    buttonStart.Visible = true;
                    DrawArrayXY(sender, e);

                }         // file exists
            }   // openfiledialog ok
        }
        

        private void buttonOpenPrevious_Click(object sender, EventArgs e)
        {
            String S = "", S1 = "", S2 = "", Stemp = "";          
            int TheIndex = 0;
            int SplitIndex = 0;
     
            ObjectDelegate del = new ObjectDelegate(UpdateListbox);
            ObjectDelegate del1 = new ObjectDelegate(UpdateHiddenListbox);

            listBoxHPGL.Items.Clear();  // ?
            listBoxHidden.Items.Clear();

            progressBarLoad.Value = 0;
            //  arrayIndex = 0; // 
            vectorIndex = 0;
            penThickness = 1; // = important

                if (File.Exists(XYZMainForm.GlobalHPGLFileName))
                {
                this.Text = XYZMainForm.GlobalHPGLFileName;

                using (StreamReader sr = new StreamReader(XYZMainForm.GlobalHPGLFileName))
                    {
                        while (sr.EndOfStream == false)
                        {
                            S = sr.ReadLine();
                            Stemp = S;
                            TheIndex = 0;
                           
                            //       if comment with // is allowed, strip it:
                            if (Stemp.Contains("//"))
                            {
                                TheIndex = S.IndexOf("//");
                                if (TheIndex > 0) Stemp = Stemp.Remove(TheIndex, Stemp.Length - TheIndex);
                            }
                            Stemp.Trim();
                            
                            //      if S contains various ';' signs, splitting is necessary
                            //      so that each line in the listbox contains one statement 
                            do
                            {
                                SplitIndex = 0;
                                SplitIndex = Stemp.IndexOf(';');
                                S1 = Stemp.Substring(0, SplitIndex + 1);
                              
                                //   We put all statements,ending with ; in the hidden listbox
                                del1.Invoke(S1);  // del1 =  to hidden listbox
                                S2 = Stemp.Substring(SplitIndex + 1);  // rest
                                S2.Trim();
                                if (S2.Length < 4) break;
                                Stemp = S2;
                            }
                            while (true);
                        }   // end stream  
                    }   // using                  
                }  // File exists
                
            buttonCommonPart_Click(sender, e);            
            progressBarLoad.Value = progressBarLoad.Maximum;
            buttonStart.Visible = true;
            DrawArrayXY(sender, e);
        }


        private void buttonCommonPart_Click(object sender, EventArgs e)
        {
            String S = "", S1 = "";           
            int cnt = 0;
            int position = 0;
            int totaal = 0;

            bool HPGLmodePD = false;
            bool HPGLmodePU = true;

            int[] CoordinatesX = new int[1000];  // maxmost 1000
            int[] CoordinatesY = new int[1000];
            int[] CoordinatesComma = new int[2000];

            ObjectDelegate del = new ObjectDelegate(UpdateListbox);
            ObjectDelegate del1 = new ObjectDelegate(UpdateHiddenListbox);
            
            totaal = listBoxHidden.Items.Count - 1;
            if (totaal == 0) totaal = 1;
            totaal = (int)(totaal / 48);
            if (totaal == 0) totaal = 1;

                    
            //  Now all the strings in the hidden listbox
            // like  eg     "PD1,2,3,4,5,6;"         (= bad programming style!!! )
            // will be splitted in
            //     "PD1,2;"
            //     "PD3,4;"
            //     "PD5,6;"
            // and added to HPGL listbox

            for (int j = 0; j < (listBoxHidden.Items.Count - 1); j++)
            {
                S1 = listBoxHidden.Items[j].ToString();
                string Snew = S1.Trim();

                if ((j % totaal) == 0) progressBarLoad.Value++;
                if (progressBarLoad.Value > 24) progressBarLoad.Value = 24;

                //  if various coordinates are in one line, split !
                //  (number of commas + 1 ) /2 = number of statements  
                //  take then into account PA or PR or PU or PD  
                //  PREPARE splitting of eg PD123,45,6,7,8,9,100,145;  
                           
                for (int i = 0; i < 1000; i++) { CoordinatesX[i] = 0; CoordinatesY[i] = 0; }
                for (int i = 0; i < 2000; i++) CoordinatesComma[i] = 0;
              
                cnt = 0;
                position = 0;
                try
                {
                    foreach (char c in S1)
                    {
                        position++;
                        if (c == ',') { CoordinatesComma[cnt] = position; cnt++; }
                        if (c == ';') { CoordinatesComma[cnt] = position; break; }    // the last CommaIndex comes from ';'
                    }
                }
                catch
                {          //  MessageBox.Show(S);      
                }
               
                if (S1.Contains("PU"))    // Pen up = moveto
                {
                    HPGLmodePU = true;
                    HPGLmodePD = false;
                }
                if (S1.Contains("PD"))    // Pen down = drawline
                {
                    HPGLmodePU = false;
                    HPGLmodePD = true;
                }
                
          // first part:
                if (CoordinatesComma[1] == 0)    del.Invoke(S1);  //  + "\r\n" will be added by delegate there as it is
              
                    //     REMARK when no commas then the complete  S1    is added to listbox
                
                else if (CoordinatesComma[1] > 0)
                {
                    S1 = Snew.Substring(0, CoordinatesComma[1] - 1).Trim();       // PU or PD is already in it       
                    del.Invoke(S1 + ";");    // add the first and maybe the only statement to listbox                        
                }

                // Add all the other parts that are in Snew: 

                for (int i = 3; i < 2000; i += 2)
                {
                    if (CoordinatesComma[i] == 0) break;
                    S1 = Snew.Substring(CoordinatesComma[i - 2], CoordinatesComma[i] - (CoordinatesComma[i - 2] + 1)).Trim();

                    if (HPGLmodePD == true) del.Invoke("PD" + S1 + ";");
                    if (HPGLmodePU == true) del.Invoke("PU" + S1 + ";");
                }
            }    // end for loop that indexes all items in the hidden listbox     
            

         // Finally all X,Y commands are now properly in the listboxHPGL:

            int t = 0;          
            vectorIndex = 0;

            for (t = 0; t < maxVectors; t++)
            {
                MyCutPoints[t].Xpoint = 0;
                MyCutPoints[t].Ypoint = 0;
                MyCutPoints[t].Mode = 0; // makes sure the previous file is not drawn.
                MyCutPoints[t].PenNr = 0;
                MyCutPoints[t].Thickness = 0;
            }
                        
       
            totaal = listBoxHPGL.Items.Count - 1;
            if (totaal == 0) totaal = 1;
            totaal = (int)(totaal / 48);
            if (totaal == 0) totaal = 1;

            bool PenUp1 = false;

            int rectangleX = 0, rectangleY = 0;
            int circleOriginX = 0, circleOriginY = 0, radiusX = 0;
            int centerArcX = 0, centerArcY = 0, radiusArc = 0;
            int arcX = 0, arcY = 0;

            t = 0;
            while (t < listBoxHPGL.Items.Count)
            {
                listBoxHPGL.SelectedIndex = t;
                S = listBoxHPGL.Items[t].ToString();

                if ((t % totaal) == 0) progressBarLoad.Value++;
                if (progressBarLoad.Value > 45) progressBarLoad.Value = 45;

                // important for KiCad:

                if (S.Contains("SP"))      selectedPen = GetPenNummer(S);    
                if (S.Contains("PW"))      penThickness = GetPenWidth(S);
                               
                if (S.Contains("PU")) PenUp1 = true;
                if (S.Contains("PD")) PenUp1 = false;
                
                if ((S.Length > 5) && (S.Contains("AA")) && (vectorIndex > 2))
                {
                    arcX = GetArrayValues(S, 1);
                    arcY = GetArrayValues(S, 2);

                    radiusArc = arcY - MyCutPoints[vectorIndex - 1].Ypoint;

                    centerArcX = arcX;
                    centerArcY = MyCutPoints[vectorIndex - 1].Ypoint + radiusArc;

                    for (double i = -90.0F; i < 91.0F; i += 9.0F)
                    {
                        double angle = i * System.Math.PI / 180;
                        int xCoord = (int)(radiusArc * System.Math.Cos(angle));
                        int yCoord = (int)(radiusArc * System.Math.Sin(angle));
                        MyCutPoints[vectorIndex].Xpoint = centerArcX + xCoord;
                        MyCutPoints[vectorIndex].Ypoint = centerArcY + yCoord; // // ???
                        MyCutPoints[vectorIndex].Mode = 1;
                        vectorIndex++;
                    }
                }


                else if ((S.Length > 5) && (S.Contains("CI")) && (vectorIndex > 1))
                {
                    radiusX = GetCircleRadius(S);
                    
                    circleOriginX = MyCutPoints[vectorIndex - 1].Xpoint;
                    circleOriginY = MyCutPoints[vectorIndex - 1].Ypoint;

                    MyCutPoints[vectorIndex].Xpoint = circleOriginX - radiusX;
                    MyCutPoints[vectorIndex].Ypoint = circleOriginY;  // -radiusX;
                    MyCutPoints[vectorIndex].Mode = 0;
                    vectorIndex++;

                    for (double i = 0.0F; i < 360.0F; i += 15.0F)
                    {
                        double angle = i * System.Math.PI / 180;
                        int xCoord = (int)(radiusX * System.Math.Cos(angle));
                        int yCoord = (int)(radiusX * System.Math.Sin(angle));
                        MyCutPoints[vectorIndex].Xpoint = circleOriginX + xCoord;
                        MyCutPoints[vectorIndex].Ypoint = circleOriginY + yCoord;
                        MyCutPoints[vectorIndex].Mode = 1;
                        vectorIndex++;
                    }
                }

                else if ((S.Length > 5) && (S.Contains(",")) && (S.Contains("EA")) && (vectorIndex > 1))
                {
                    rectangleX = GetArrayValues(S, 1);
                    rectangleY = GetArrayValues(S, 2);

                    MyCutPoints[vectorIndex].Xpoint = MyCutPoints[vectorIndex - 1].Xpoint;
                    MyCutPoints[vectorIndex].Ypoint = MyCutPoints[vectorIndex - 1].Ypoint;
                    MyCutPoints[vectorIndex].Mode = 0;
                    vectorIndex++;

                    MyCutPoints[vectorIndex].Xpoint = rectangleX;
                    MyCutPoints[vectorIndex].Ypoint = MyCutPoints[vectorIndex - 1].Ypoint;
                    MyCutPoints[vectorIndex].Mode = 1;
                    vectorIndex++;

                    MyCutPoints[vectorIndex].Xpoint = rectangleX;
                    MyCutPoints[vectorIndex].Ypoint = rectangleY;
                    MyCutPoints[vectorIndex].Mode = 1;
                    vectorIndex++;

                    MyCutPoints[vectorIndex].Xpoint = MyCutPoints[vectorIndex - 3].Xpoint; // MyCutPoints[vectorIndex - 1].Xpoint-rectangleX;
                    MyCutPoints[vectorIndex].Ypoint = rectangleY;
                    MyCutPoints[vectorIndex].Mode = 1;
                    vectorIndex++;

                    MyCutPoints[vectorIndex].Xpoint = MyCutPoints[vectorIndex - 4].Xpoint;
                    MyCutPoints[vectorIndex].Ypoint = MyCutPoints[vectorIndex - 4].Ypoint;
                    MyCutPoints[vectorIndex].Mode = 1;
                    vectorIndex++;
                }

                else if ((S.Length > 5) && (S.Contains(",")))
                {

                    MyCutPoints[vectorIndex].Xpoint = GetArrayValues(S, 1); // 1 = X
                    MyCutPoints[vectorIndex].Ypoint = GetArrayValues(S, 2); // 2 = Y ->Y +1 is necessary to draw top border??? !
                    MyCutPoints[vectorIndex].Mode = 1;      // original only 1 = pd and 0 = pu are supported

                    //   if (S.Contains("PD")) MyPlotPoints[arrayIndex].Mode = 1;
                    //   if (S.Contains("PU")) MyPlotPoints[arrayIndex].Mode = 0;
                    // NEW from 2 8 2020
                    if (PenUp1 == true) MyCutPoints[vectorIndex].Mode = 0;
                    else MyCutPoints[vectorIndex].Mode = 1;
                    
                    MyCutPoints[vectorIndex].Thickness = penThickness;
                    MyCutPoints[vectorIndex].PenNr = selectedPen;
                    
                   vectorIndex++;                   
                }
                t++;
            }
            
// start new :

            int minimumX = 999999999, minimumY = 999999999;           

            for (t = 0; t <vectorIndex; t++)    // 20000
            {
                if (minimumX >MyCutPoints[t].Xpoint) minimumX = MyCutPoints[t].Xpoint;
                if (minimumY > MyCutPoints[t].Ypoint) minimumY = MyCutPoints[t].Ypoint;
            }
            for (t = 0; t <vectorIndex; t++)
            {
             MyCutPoints[t].Xpoint = MyCutPoints[t].Xpoint - minimumX;
             MyCutPoints[t].Ypoint = MyCutPoints[t].Ypoint - minimumY;
            }

            int maximumX = -999999999, maximumY = -999999999;
            for (t = 0; t < vectorIndex; t++)    // 20000
            {
                if (maximumX < MyCutPoints[t].Xpoint) maximumX = MyCutPoints[t].Xpoint;
                if (maximumY < MyCutPoints[t].Ypoint) maximumY = MyCutPoints[t].Ypoint;
            }
                        

        if (checkBoxMirror.Checked)
                for (t = 0; t <  vectorIndex; t++)
                        MyCutPoints[t].Ypoint = maximumY - MyCutPoints[t].Ypoint+1;// +1 is very important
        
           
            minimumX = 999999999; ; minimumY = 999999999;
            maximumX = -999999999; maximumY = -999999999;

            for (t = 0; t < vectorIndex; t++)    // 20000
            {
                if (minimumX > MyCutPoints[t].Xpoint) minimumX = MyCutPoints[t].Xpoint;
                if (minimumY > MyCutPoints[t].Ypoint) minimumY = MyCutPoints[t].Ypoint;
            }


            for (t = 0; t < vectorIndex; t++)    // 20000
            {
                if (maximumX < MyCutPoints[t].Xpoint) maximumX = MyCutPoints[t].Xpoint;
                if (maximumY < MyCutPoints[t].Ypoint) maximumY = MyCutPoints[t].Ypoint;
            }
                      
           
            globalMaximumX = maximumX;
            globalMaximumY = maximumY;
            globalMinimumX = minimumX;
            globalMinimumY = minimumY;
           

            float DividerX = 1, DividerY = 1;
            try { if (maximumX < 1000) maximumX = 1001; }
            catch { maximumX = 1; }
            try { if (maximumY < 1000) maximumY = 1000; }
            catch { maximumY = 1; }
            try { if (minimumX > 0) minimumX = 0; }
            catch { minimumX = 1; }
            try { if (minimumY > 0) minimumY = 0; }
            catch { minimumY = 1; }

            DividerX = ((float)(maximumX - minimumX)) / pictureBox1.Width;  //  (int)
            DividerY = ((float)(maximumY - minimumY)) / pictureBox1.Height;
            outputScreenDivider = DividerX;
            if (DividerY > DividerX) outputScreenDivider = DividerY;
            if (outputScreenDivider == 0) outputScreenDivider = 1;
             
            CalculateOutputCutDivider(sender, e);
            
          progressBarLoad.Value = progressBarLoad.Maximum;
          DrawArrayXY(sender, e);
        }
        
            
      private void CalculateOutputCutDivider(object sender, EventArgs e)   // Calculates output scale for display on screen and for plotting on paper or vinyl
        {
            float DividerX = 1, DividerY = 1;     
       
            if (XYZMainForm.TheScaledOutput == 11)
            {               
                XYZMainForm.PaperDimX = float.Parse(textBoxEditX.Text);
                XYZMainForm.PaperDimY = float.Parse(textBoxEditY.Text);
            }
                                  
            DividerX =  ( (float)(globalMaximumX - globalMinimumX) *1) / (float)XYZMainForm.PaperDimX;   // 
            DividerY =  ( (float)(globalMaximumY - globalMinimumY)*1) / (float)XYZMainForm.PaperDimY;   //
            
            outputCutDivider = DividerX;
            if (DividerY > DividerX) outputCutDivider = DividerY;
            if (outputCutDivider == 0) outputCutDivider = 1;

       }


        private void DrawArrayXY(object sender, EventArgs e)
        {
            int x;
            int Ydim = pictureBox1.Height + 1;    //     for mirrored images, without +1 top border is not drawn
            int kleur = 0;                       //      black
            int thickness = 2;                   //      pen thickness

            Graphics g = pictureBox1.CreateGraphics();            
            IntPtr hdc = g.GetHdc();
            
            for (x =1; x < vectorIndex; x++)  // 0 or 1 resulted in fake diagonal line
            {
                switch (MyCutPoints[x].PenNr)
                {   
                    case 1: kleur = 0; break;
                    case 2: kleur = 128; break;
                    case 3: kleur = 255; break;
                    case 4: kleur = 128 + 128<<8 ; break;
                    case 5: kleur = 255 + 255<<8; break;
                    case 6: kleur = 255<<8; break;
                    case 7: kleur = 255<<16; break;
                    case 8: kleur = 128+ 255<<8; break;
                    case 9: kleur = 255+ 255<<8 + 255<<16; break;
                    default: kleur = 0; break;  // black
                }
                          
                thickness = MyCutPoints[x].Thickness;                
                IntPtr hObject = CreatePen(PenStyles.PS_SOLID, thickness, kleur);   //20 Color.FromArgb(Form1.PrintSignalColor));
                SelectObject(hdc, hObject);
                            
                if (MyCutPoints[x].Mode == 1)
                {                               
                        MoveToEx(hdc, (int)((float)MyCutPoints[x-1].Xpoint / outputScreenDivider),
                            (int)((float)MyCutPoints[x-1].Ypoint / outputScreenDivider) , IntPtr.Zero);

                        LineTo(hdc, (int)((float)MyCutPoints[x].Xpoint / outputScreenDivider) ,
                            (int)((float)MyCutPoints[x].Ypoint / outputScreenDivider) );                

                }   // mode == 1

                DeleteObject(hObject);
            } // for loop
            
            g.ReleaseHdc(hdc);
            g.Dispose();
        }

        private int GetCircleRadius(string S)
        {
            int x;
            string S1 = "";
            int radiusX = 0;
          
            if (S.Contains(".")) x = S.IndexOf('.');
            else x = S.Length;

            try { S1 = S.Substring(2, x - 2); }
            catch { return (0); }

            try { radiusX = int.Parse(S1); }
            catch { return (0); }
            return (radiusX);
        }

        private int GetArrayValues(string S, int mode)
        {
            int x, y;
            string S1 = "", S2 = "";
            int CoordX = 0, CoordY = 0;

            x = S.IndexOf(',');

            try { S1 = S.Substring(2, x - 2); }
            catch { return (0); }

            y = S.IndexOf(';');
            try { S2 = S.Substring(x + 1, y - (x + 1)); }
            catch { return (0); }

            try { CoordX = int.Parse(S1); }
            catch { return (0); }

            try { CoordY = int.Parse(S2); }
            catch { return (0); }

            if (mode == 1) return (CoordX);
            if (mode == 2) return (CoordY);
            return (0);
        }

        private int GetPenNummer(string S)
        {
            int x;
            string S1 = "";
            int SP = 0;  // 0 = black
            x = S.IndexOf(';');
            try { S1 = S.Substring(2, x - 2); }
            catch { return (0); }
            try { SP = int.Parse(S1); }
            catch { return (0); }
            return (SP);
        }

        private int GetPenWidth(string S)
        {
            int x;
            string S1 = "";
            int PW = 0;        //  0 = black
            x = S.IndexOf(';');

            try { S1 = S.Substring(2, x - 2); }
            catch { return (0); }

            try { PW = int.Parse(S1); }
            catch { return (0); }

            return (PW);
        }
                
        private void UpdateHiddenListbox(object obj)
        {
            if (InvokeRequired)
            {
                ObjectDelegate method = new ObjectDelegate(UpdateHiddenListbox);
                Invoke(method, obj);
                return;
            }
            string S = (string)obj;
            listBoxHidden.Items.Add(S + "\r\n");
        }

        private void UpdateListbox(object obj)
        {
            if (InvokeRequired)
            {
                ObjectDelegate method = new ObjectDelegate(UpdateListbox);
                Invoke(method, obj);
                return;
            }
            string S = (string)obj;
            listBoxHPGL.Items.Add(S + "\r\n");
        }        
        
        private void buttonPlot_Click(object sender, EventArgs e)
        {           
            tableLayoutPanel1.Visible = false;
            buttonExecuteHPGL_Click(sender, e);            
        }
        
        private void checkBoxPause_CheckStateChanged(object sender, EventArgs e)
        {           
            tableLayoutPanel1.Visible = false;
            if (checkBoxPause.Checked)
            {
                // Add here this can not happen when start button was not pressed !!
                if (started == true) MyXYZR.SetLaser(1);  // 1 = off !!             
                tableLayoutPanel1.Visible = true;                
            }
        }
     
        private void listBoxHPGL_MouseClick(object sender, MouseEventArgs e)
        {        
            int x = listBoxHPGL.SelectedIndices[0];
            if (x == -1) return;
            Graphics g = pictureBox1.CreateGraphics();
            IntPtr hdc = g.GetHdc();
            
            int kleur = 255; // red            
            if (radioButtonR.Checked) kleur = 255;
            if (radioButtonG.Checked) kleur = 255<<8;
            if (radioButtonB.Checked) kleur = 255<<16;
            
            IntPtr hObject = CreatePen(PenStyles.PS_SOLID, 2,kleur);   //20 Color.FromArgb(Form1.PrintSignalColor));
            SelectObject(hdc, hObject);

            int xval = (int)(MyCutPoints[x].Xpoint / outputScreenDivider) ;
            int yval =  (int)(MyCutPoints[x].Ypoint / outputScreenDivider);
       
            MoveToEx(hdc, xval - 5, yval - 5, IntPtr.Zero);
            LineTo(hdc, xval + 5, yval + 5);

            MoveToEx(hdc, xval - 5, yval + 5, IntPtr.Zero);
            LineTo(hdc, xval + 5, yval - 5);
          
            DeleteObject(hObject);
            g.ReleaseHdc(hdc);
            g.Dispose();
        }               
    

        private void buttonOutput_MouseClick(object sender, MouseEventArgs e)
        {            
            XYZMainForm.CustomDimX = float.Parse(textBoxEditX.Text);
            XYZMainForm.CustomDimY = float.Parse(textBoxEditY.Text);

            if (e.Button == MouseButtons.Left)
            {
                ContextMenuStrip ctx1 = new ContextMenuStrip();
                ctx1.Font = new Font("Arial", 12);
                ctx1.Items.Add("A1 landscape 840 * 594 mm", null,SetA1_Landscape_Click);
                ctx1.Items.Add("A1 portrait  594 * 840 mm", null, SetA1_Portrait_Click);
                ctx1.Items.Add("A2 landscape 594 * 420 mm", null, SetA2_Landscape_Click);
                ctx1.Items.Add("A2 portrait  420 * 594 mm", null, SetA2_Portrait_Click);
                ctx1.Items.Add("A3 landscape 420 * 297 mm", null, SetA3_Landscape_Click);
                ctx1.Items.Add("A3 portrait  297 * 420 mm", null, SetA3_Portrait_Click);
                ctx1.Items.Add("A4 landscape 297 * 210 mm", null, SetA4_Landscape_Click);
                ctx1.Items.Add("A4 portrait  210 * 297 mm", null, SetA4_Portrait_Click);
                ctx1.Items.Add("A5 landscape 210 * 148 mm", null, SetA5_Landscape_Click);
                ctx1.Items.Add("A5 portrait  148 * 210 mm", null, SetA5_Portrait_Click);
                ctx1.Items.Add("Custom dimensions", null, SetCustom_Click);
                
                switch (XYZMainForm.TheScaledOutput)
                {
                    case 1: ((ToolStripMenuItem)ctx1.Items[0]).Checked = true;  break;
                    case 2: ((ToolStripMenuItem)ctx1.Items[1]).Checked = true; break;
                    case 3: ((ToolStripMenuItem)ctx1.Items[2]).Checked = true; break;
                    case 4: ((ToolStripMenuItem)ctx1.Items[3]).Checked = true; break;
                    case 5: ((ToolStripMenuItem)ctx1.Items[4]).Checked = true; break;
                    case 6: ((ToolStripMenuItem)ctx1.Items[5]).Checked = true; break;
                    case 7: ((ToolStripMenuItem)ctx1.Items[6]).Checked = true; break;
                    case 8: ((ToolStripMenuItem)ctx1.Items[7]).Checked = true; break;
                    case 9: ((ToolStripMenuItem)ctx1.Items[8]).Checked = true; break;
                    case 10: ((ToolStripMenuItem)ctx1.Items[9]).Checked = true; break;
                    case 11: ((ToolStripMenuItem)ctx1.Items[10]).Checked = true; break;
                    default: break;
                }                
                ctx1.Show(this, new Point(buttonOutput.Left, buttonOutput.Top + buttonOutput.Height + 10));
            }
        }

        private void SetLabels(object sender,EventArgs e)
        {
            XYZMainForm.CustomDimX = float.Parse(textBoxEditX.Text);
            XYZMainForm.CustomDimY = float.Parse(textBoxEditY.Text);

            if (XYZMainForm.TheScaledOutput < 11)
            {
                //  panelMaxEdit.Visible = false;
                tableLayoutPanelDimXY.Visible = false;
                panelMaxLabels.Visible = true;                
           
                switch (XYZMainForm.TheScaledOutput)
                {                   
                    case 1:
                        labelMaxXmm.Text = "A1 landscape";
                        labelMaxYmm.Text = "840 * 594 mm";
                        break;
                    case 2:
                        labelMaxXmm.Text = "A1 portrait";
                        labelMaxYmm.Text = "594 * 840 mm";
                        break;
                    case 3:
                        labelMaxXmm.Text = "A2 landscape";
                        labelMaxYmm.Text = "594 * 420 mm";
                        break;
                    case 4:
                        labelMaxXmm.Text = "A2 landscape";
                        labelMaxYmm.Text = "420 * 594 mm";
                        break;
                    case 5:
                        labelMaxXmm.Text = "A3 landscape";
                        labelMaxYmm.Text = "420 * 297 mm";
                        break;
                    case 6:
                        labelMaxXmm.Text = "A3 portrait";
                        labelMaxYmm.Text = "297 * 420 mm";
                        break;
                    case 7:
                        labelMaxXmm.Text = "A4 landscape";
                        labelMaxYmm.Text = "297 * 210 mm";
                        break;
                    case 8:
                        labelMaxXmm.Text = "A4 portrait";
                        labelMaxYmm.Text = "210 * 297 mm";
                        break;
                    case 9:
                        labelMaxXmm.Text = "A5 landscape";
                        labelMaxYmm.Text = "210 * 148 mm";
                        break;
                    case 10:
                        labelMaxXmm.Text = "A5 portrait";
                        labelMaxYmm.Text = "148 * 210 mm";
                        break;
                    default: break;
                }
            }
          else if (XYZMainForm.TheScaledOutput == 11)
            {
                panelMaxLabels.Visible = false;
                //    panelMaxEdit.Visible = true;               
                tableLayoutPanelDimXY.Visible = true;
            }
        }
        
        private void SetA1_Landscape_Click(object sender,EventArgs e)
        {
           XYZMainForm.PaperDimX = 840;
           XYZMainForm.PaperDimY = 594;                   
           XYZMainForm.TheScaledOutput = 1;
           SetLabels(sender, e);
        }

        private void SetA1_Portrait_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 594;
            XYZMainForm.PaperDimY = 840;
            XYZMainForm.TheScaledOutput = 2;
            SetLabels(sender, e);
        }

        private void SetA2_Landscape_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 594;
            XYZMainForm.PaperDimY = 420;
            XYZMainForm.TheScaledOutput = 3;
            SetLabels(sender, e);
        }
        private void SetA2_Portrait_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 420;
            XYZMainForm.PaperDimY = 594;
            XYZMainForm.TheScaledOutput = 4;
            SetLabels(sender, e);
        }

        private void SetA3_Landscape_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 420;
            XYZMainForm.PaperDimY = 297;
            XYZMainForm.TheScaledOutput = 5;
            SetLabels(sender, e);
        }
        private void SetA3_Portrait_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 297;
            XYZMainForm.PaperDimY = 420;
            XYZMainForm.TheScaledOutput = 6;
            SetLabels(sender, e);
        }
        private void SetA4_Landscape_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 297;
            XYZMainForm.PaperDimY = 210;
            XYZMainForm.TheScaledOutput = 7;
            SetLabels(sender, e);
        }
        private void SetA4_Portrait_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 210;
            XYZMainForm.PaperDimY = 297;
            XYZMainForm.TheScaledOutput = 8;
            SetLabels(sender, e);
        }
        private void SetA5_Landscape_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 210;
            XYZMainForm.PaperDimY = 148;
            XYZMainForm.TheScaledOutput = 9;
            SetLabels(sender, e);
        }
        private void SetA5_Portrait_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 148;
            XYZMainForm.PaperDimY = 210;
            XYZMainForm.TheScaledOutput = 10;
            SetLabels(sender, e);
        }

        private void SetCustom_Click(object sender, EventArgs e)
        {          
            XYZMainForm.CustomDimX = float.Parse(textBoxEditX.Text);
            XYZMainForm.CustomDimY = float.Parse(textBoxEditY.Text);
            XYZMainForm.PaperDimX = XYZMainForm.CustomDimX;
            XYZMainForm.PaperDimY = XYZMainForm.CustomDimY;
            XYZMainForm.TheScaledOutput = 11;            
            SetLabels(sender, e);
        }
       

        private void buttonSetXYTravel_Click(object sender, EventArgs e)
        {        
            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextboxReceive);
            XYZMainForm.SerialPortReturnS = "";
            XYZMainForm.SerialPortSendS = "";
           
            int tempdelay = 25;
            try
            {
                tempdelay = int.Parse(comboBoxXYTravel.Text);
            }
            catch
            {
                tempdelay = 25;
            }
            if (tempdelay < 2) tempdelay = 2;
            if (tempdelay > 150) tempdelay = 150;

            MyXYZR.SetXYSpeed(tempdelay);

            delListbox.Invoke(XYZMainForm.SerialPortSendS);
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
        }
        
        private void buttonSetXYWork_Click(object sender, EventArgs e)
        {
            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextboxReceive);

            XYZMainForm.SerialPortReturnS = "";
            XYZMainForm.SerialPortSendS = "";

            int tempdelay = 25;
            try
            {
                tempdelay = int.Parse(comboBoxXYWork.Text);
            }
            catch
            {
                tempdelay = 25;
            }
            if (tempdelay < 2) tempdelay = 2;   
            if (tempdelay > 150) tempdelay = 150; 

            MyXYZR.SetXYSpeed(tempdelay);
            delListbox.Invoke(XYZMainForm.SerialPortSendS);
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
        }

        private void buttonSetZupSpeed_Click(object sender, EventArgs e)
        {
            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextboxReceive);

            XYZMainForm.SerialPortReturnS = "";
            XYZMainForm.SerialPortSendS = "";

            int tempdelay = 25;
            try
            {
                tempdelay = int.Parse(comboBoxZup.Text);
            }
            catch
            {
                tempdelay = 25;
            }

            if (tempdelay < 2) tempdelay = 2;   // maxmost speed is then 250mm /sec
            if (tempdelay > 150) tempdelay = 150; // 0.05 mm.sec

            MyXYZR.SetZSpeed(tempdelay);
            delListbox.Invoke(XYZMainForm.SerialPortSendS);
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
        }

        private void buttonSetZdownSpeed_Click(object sender, EventArgs e)
        {
            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextboxReceive);

            XYZMainForm.SerialPortReturnS = "";
            XYZMainForm.SerialPortSendS = "";

            int tempdelay = 25;
            try
            {
                tempdelay = int.Parse(comboBoxZdown.Text);
            }
            catch
            {
                tempdelay = 25;
            }

            if (tempdelay < 2) tempdelay = 2;   // maxmost speed is then 250mm /sec
            if (tempdelay > 150) tempdelay = 150; // 0.05 mm.sec
            MyXYZR.SetZSpeed(tempdelay);
            delListbox.Invoke(XYZMainForm.SerialPortSendS);
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
        }

        private void buttonSetZupPosition_Click(object sender, EventArgs e)
        {
            float positionZ = 10.0F;
            try { positionZ = float.Parse(textBoxZupPosition.Text); }
            catch { positionZ = 10.0F; }
            MyXYZR.ZAction(sender, e,  positionZ);
            cutterUp = true;                             
        }

        private void buttonSetZdownPosition_Click(object sender, EventArgs e)
        {
            float positionZ = 10.0F;
            try { positionZ = float.Parse(textBoxZdownPosition.Text); }
            catch { positionZ = 10.0F; }
            MyXYZR.ZAction(sender, e, positionZ);           
            cutterUp = false;
        }

        private void textBoxZdownPosition_Leave(object sender, EventArgs e)
        {
            float temp = 10.123F;
            try { temp = float.Parse(textBoxZdownPosition.Text); }
            catch { temp = 10.123F; }           
            XYZMainForm.cutZDownPosition = temp;
        }

        private void textBoxZupPosition_Leave(object sender, EventArgs e)
        {
            float temp = 10.123F;
            try { temp = float.Parse(textBoxZupPosition.Text); }
            catch { temp = 10.123F; }           
            XYZMainForm.cutZUpPosition = temp;
        }

        private void comboBoxZup_Leave(object sender, EventArgs e)
        {
            int temp = 25;
            try { temp = int.Parse(comboBoxZup.Text); }
            catch { temp = 25; }         
            XYZMainForm.cutZUpSpeed = temp;
        }

        private void comboBoxZdown_Leave(object sender, EventArgs e)
        {
            int temp = 25;
            try { temp = int.Parse(comboBoxZdown.Text); }
            catch { temp = 25; }            
            XYZMainForm.cutZDownSpeed = temp;           
        }

        private void comboBoxXYWork_Leave(object sender, EventArgs e)
        {
            int temp = 50;
            try { temp = int.Parse(comboBoxXYWork.Text); }
            catch { temp = 50; }
            XYZMainForm.cutXYWorkSpeed = temp;
        }

        private void comboBoxXYTravel_Leave(object sender, EventArgs e)
        {
            int temp = 50;
            try { temp = int.Parse(comboBoxXYTravel.Text); }
            catch { temp = 50; }
            XYZMainForm.cutXYTravelSpeed = temp;
        }

        private void DisplayXYCoordinates(object sender, EventArgs e)
        {         
            labelXmm.Text = "X" + XYZMainForm.globalActualMmPosX.ToString("###.##") + "mm";  // ((float)XYZMainForm.globalActualMicrostepsX / 200).ToString("###.###");
            labelYmm.Text = "Y" + XYZMainForm.globalActualMmPosY.ToString("###.##") + "mm"; //  ((float)XYZMainForm.globalActualMicrostepsY / 200).ToString("###.###");
            labelZmm.Text = "Z" + XYZMainForm.globalActualMmPosZ.ToString("##.##") + "mm"; // ((float)XYZMainForm.globalActualMicrostepsZ / 200).ToString("###.###");

        }

        private void buttonZupUp_Click(object sender, EventArgs e)
        {
            changeZposition(sender, e, true, -1.0F);            
            cutterUp = true;
        }              

        private void buttonZupDown_Click(object sender, EventArgs e)
        {
            changeZposition(sender, e, true, 1.0F);
            cutterUp = true;
        }

        private void buttonZdownUp_Click(object sender, EventArgs e)
        {
            changeZposition(sender, e, false, -0.05F);
            cutterUp = false;
        }

        private void buttonZdownDown_Click(object sender, EventArgs e)
        {
            changeZposition(sender, e, false, 0.05F);
            cutterUp = false;
        }

        private void changeZposition(object sender, EventArgs e, bool zUp, float Zchange)
        {
            float positionZ = 10.0F;
            if (zUp == true)  // up position
            {
                try { positionZ = float.Parse(textBoxZupPosition.Text); }
                catch { positionZ = 10.0F; }
                positionZ = positionZ + Zchange;
                textBoxZupPosition.Text = positionZ.ToString("##.###");
            }
            else if (zUp == false) // donw position
            {
                try { positionZ = float.Parse(textBoxZdownPosition.Text); }
                catch { positionZ = 10.0F; }
                positionZ = positionZ + Zchange;
                textBoxZdownPosition.Text = positionZ.ToString("##.###");
            }
            MyXYZR.ZAction(sender, e, positionZ);
        }

        private void checkBoxLaser_CheckedChanged(object sender, EventArgs e)
        {
            textBoxZupPosition.Visible = true;
            buttonSetZupPosition.Visible = true;

            buttonZupUp.Visible = true;
            buttonZupDown.Visible = true;

            labelZmm.Visible = true;
            label6.Visible = true;
            comboBoxZup.Visible = true;
            comboBoxZdown.Visible = true;

            buttonSetZupSpeed.Visible = true;
            buttonSetZdownSpeed.Visible = true;
            buttonStart.ForeColor = Color.Blue;
            buttonStart.Text = "Start Cut";

            if (checkBoxLaser.Checked)
            {               
                buttonZupUp.Visible = false;
                buttonZupDown.Visible = false;
                textBoxZupPosition.Visible = false;
                buttonSetZupPosition.Visible = false;            
                label6.Visible = false;             
                comboBoxZup.Visible = false;
                comboBoxZdown.Visible = false;
                buttonSetZupSpeed.Visible = false;
                buttonSetZdownSpeed.Visible = false;
                buttonStart.ForeColor = Color.Red;
                buttonStart.Text = "Start Laser";
            }
        } 

    // zSteps below are absolute

    private void DisplayAbsoluteZCoordinates(object sender, EventArgs e)   // in zSteps
        {
            labelZmm.Text = XYZMainForm.globalActualMmPosZ.ToString("###.###");
            if (labelZmm.Text == "") labelZmm.Text = "0.0";     // otherwise nothing is displayed on 0 
        }


    // prevents clearing screen on 'Alt' press, keep it !

        protected override void WndProc(ref Message m)
        {
            // Suppress the WM_UPDATEUISTATE message
            if (m.Msg == 0x128) return;
            base.WndProc(ref m);
        }


    }
}
