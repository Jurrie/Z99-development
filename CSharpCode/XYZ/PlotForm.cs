 
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace XYZ
{
    public partial class PlotForm : Form
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
        public static PlotPoints[] MyPlotPoints = new PlotPoints[maxVectors];        
             
        public static float outputScreenDivider = 1.0F;      // factor used for drawing on the screen
        public static float outputPlotDivider = 1.0F;        //  factor used fro plotting
        
        private delegate void ObjectDelegate(object obj);

        public static Boolean penUp = true;  // start default with pen up.
        public static int penThickness = 1;
        public static int selectedPen = 1;

        public static int globalMaximumX, globalMaximumY;
        public static int globalMinimumX, globalMinimumY;
        
        public static float drawMultiplier = 1.0F;
        
        XYZR MyXYZR = new XYZR();

        public static bool started = false; // button Start has not been selected

        
        public PlotForm()
        {
            InitializeComponent();

            this.Top = -1;
            this.Left = -1;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;    //  - 20;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;  //  - 50;           
                     
            clearPlotPoints();
                      
            this.BackColor = Functions.kleur(XYZMainForm.FormColor);
            Color buttonColor = Functions.kleur(XYZMainForm.IconColor);
            buttonMenu.BackColor = buttonColor;
            groupBoxScreenDimension.BackColor = buttonColor;
            groupBoxRGB.BackColor = buttonColor;

            buttonOutput.BackColor = buttonColor;
            buttonStart.BackColor = buttonColor;
            buttonQuit.BackColor = buttonColor;
            checkBoxMirror.BackColor = buttonColor;
            checkBoxBorderPlot.BackColor = buttonColor;
            checkBoxPause.BackColor = buttonColor;


            int btW = this.Width / 17; //16
            int btM = 4;
            int btH = this.Height / 11;//12
            
            buttonMenu.Left = btM;
            buttonMenu.Top = btM;
            buttonMenu.Width = btW;
            buttonMenu.Height = btH;

            progressBarLoad.Left = btM + btW + btM;
            progressBarLoad.Top = btM;
            progressBarLoad.Width = btW*5/4;
            progressBarLoad.Height = btH / 2 - 4;
            groupBoxRGB.Left =  btM + btW + btM;
            groupBoxRGB.Top = btM + buttonMenu.Height / 2-2;
            groupBoxRGB.Width = btW*5/4;
            groupBoxRGB.Height = btH / 2 + 2;
                      
            checkBoxMirror.Left = progressBarLoad.Left + progressBarLoad.Width + btM;
            checkBoxMirror.Top = btM;   //
            checkBoxMirror.Width = btW;
            checkBoxMirror.Height = btH/2;

            checkBoxBorderPlot.Left = checkBoxMirror.Left;
            checkBoxBorderPlot.Top = btM+btH/2;
            checkBoxBorderPlot.Width = btW;
            checkBoxBorderPlot.Height = btH / 2;          
            
            buttonOutput.Left = checkBoxMirror.Left +btW+ btM;
            buttonOutput.Top = btM;
            buttonOutput.Width = btW;
            buttonOutput.Height = btH/2;

            groupBoxScreenDimension.Left = buttonOutput.Left;
            groupBoxScreenDimension.Top = buttonOutput.Top + btH/2;
            groupBoxScreenDimension.Width = btW;
            groupBoxScreenDimension.Height = btH / 2;

            radioButton10.Left = 2;
            radioButton10.Top = 5;
            radioButton10.Height = btH / 3 + 4;
            radioButton10.Width = btW / 3 + 4;

            radioButton20.Left = btW / 3;
            radioButton20.Top = 5;
            radioButton20.Height = btH / 3 + 4;
            radioButton20.Width = btW / 3 + 4;

            radioButton40.Left = btW * 2 / 3;
            radioButton40.Top = 5;
            radioButton40.Height = btH / 3 + 4;
            radioButton40.Width = btW / 3 + 4;

                       
            tableLayoutPanelDimXY.Left = buttonOutput.Left + btW + btM;
            tableLayoutPanelDimXY.Top = btM*3;
            tableLayoutPanelDimXY.Width = btW+btM*3;
            tableLayoutPanelDimXY.Height = btH-btM;
            
            panelMaxLabels.Left = buttonOutput.Left + btW + btM;
            panelMaxLabels.Top = btM;
            panelMaxLabels.Width = btW +btM*3;
            panelMaxLabels.Height = btH;
            
            tableLayoutPanelLaser.Left = tableLayoutPanelDimXY.Left + tableLayoutPanelDimXY.Width+btM;  // panelMaxEdit.Left + panelMaxEdit.Width + btM;
            tableLayoutPanelLaser.Width = btW*5/4;
            tableLayoutPanelLaser.Height = btH;
            tableLayoutPanelLaser.Top = btM;
                     
            buttonStart.Left =tableLayoutPanelLaser.Left + tableLayoutPanelLaser.Width; // panelMaxEdit.Left + btW *3 / 2 + btM;
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

            tableLayoutPanelXYZPos.Left = tableLayoutPanel1.Left + (btW * 22) / 4 + btM;
            tableLayoutPanelXYZPos.Top = 0;
            tableLayoutPanelXYZPos.Height = btH + btM;
            tableLayoutPanelXYZPos.Width = btW; //21 /16
           
            buttonQuit.Left = tableLayoutPanelXYZPos.Left + btW + btM;
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

            panel1.Top = listBoxHPGL.Top;
            panel1.Left = listBoxHPGL.Left + listBoxHPGL.Width + 20;
            panel1.Width = this.Width - (listBoxHPGL.Width + 50);
            panel1.Height = this.Height- (listBoxHPGL.Top+50); // - (pictureBox1.Top + 75);

            panel1.AutoSize =true;
            panel1.AutoScroll = true;
                      
            panel1.BackColor = Functions.kleur(XYZMainForm.FondColor);
            
            comboBoxXYTravel.Text = XYZMainForm.plotXYTravelSpeed.ToString();
            comboBoxXYWork.Text = XYZMainForm.plotXYWorkSpeed.ToString();

            comboBoxZup.Text = XYZMainForm.plotZUpSpeed.ToString();
            comboBoxZdown.Text = XYZMainForm.plotZDownSpeed.ToString();
                   
            textBoxZupPosition.Text = XYZMainForm.plotZUpPosition.ToString("##.####");
            textBoxZdownPosition.Text = XYZMainForm.plotZDownPosition.ToString("##.####");           
            
          buttonMenu.Text = XYZMainForm.StrMenu;
          //  checkBoxMirror.Text = XYZMainForm.StrMirror;          
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
          else if (XYZMainForm.TheScaledOutput == 11)
              {
                panelMaxLabels.Visible = false;          
                tableLayoutPanelDimXY.Visible = true;                     
               }            

            labelXmm.Text = "X"+XYZMainForm.globalActualMmPosX.ToString("###.##")+"mm";  // ((float)XYZMainForm.globalActualMicrostepsX / 200).ToString("###.###");
            labelYmm.Text = "Y"+XYZMainForm.globalActualMmPosY.ToString("###.##")+"mm"; //  ((float)XYZMainForm.globalActualMicrostepsY / 200).ToString("###.###");
            labelZmm.Text = "Z"+XYZMainForm.globalActualMmPosZ.ToString("##.##")+"mm"; // ((float)XYZMainForm.globalActualMicrostepsZ / 200).ToString("###.###");

            MyXYZR.InitSerialPort();

        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            MyXYZR.SetLaser(1);  // 1 = off !!

            XYZMainForm.CustomDimX = float.Parse(textBoxEditX.Text);
            XYZMainForm.CustomDimY = float.Parse(textBoxEditY.Text);

            // XY speed
            try { XYZMainForm.plotXYTravelSpeed = int.Parse(comboBoxXYTravel.Text); }
            catch { XYZMainForm.plotXYTravelSpeed = 25; }
            try { XYZMainForm.plotXYWorkSpeed = int.Parse(comboBoxXYWork.Text); }
            catch { XYZMainForm.plotXYWorkSpeed = 10; }

            // Z speed           
            XYZMainForm.plotZUpSpeed = int.Parse(comboBoxZup.Text);
            XYZMainForm.plotZDownSpeed = int.Parse(comboBoxZdown.Text);

            // Z position
            XYZMainForm.plotZUpPosition = float.Parse(textBoxZupPosition.Text);            
            XYZMainForm.plotZDownPosition = float.Parse(textBoxZdownPosition.Text);

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
                ctx.Items.Add("Open *.plt file (for PCB)", null, buttonOpenPlt_Click); // "PCB"
                ctx.Items.Add("_________________________", null, null);
                
                ctx.Items.Add(XYZMainForm.StrClearAll, null, buttonClearAll_Click);  // "Clear all"
                ctx.Items.Add("_________________________", null, null);

                ctx.Items.Add(XYZMainForm.StrReset+ " plotter", null, buttonReset_Click); // "Reset plotter"
                ctx.Items.Add("_________________________", null, null);

                if (listBoxHPGL.Items.Count > 2)
                {
                    if (checkBoxLaser.Checked==false)
                  ctx.Items.Add(XYZMainForm.StrStartPlot, null, buttonPlot_Click); 
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
        
            while (MyXYZR.NextCommand == false) Application.DoEvents();

            if (XYZMainForm.SerialPortReturnS.Contains("43"))
              delReceive.Invoke("Reset ok " + XYZMainForm.SerialPortReturnS);
            else
              delReceive.Invoke("Reset NOT ok " + XYZMainForm.SerialPortReturnS);

        }
        
        private void buttonClearAll_Click(object sender, EventArgs e)
        {                    
            listBoxHPGL.Items.Clear();
            textBoxReceive.Clear();

            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Green, 2);
            // g.Clear(panel1.BackColor);
            g.Clear(Functions.kleur(XYZMainForm.FondColor));
            clearPlotPoints();                   
            g.Dispose();
            buttonStart.Visible = false;
        }

        private void clearPlotPoints()
        {
            for (int x = 0; x < maxVectors; x++)
            {
                MyPlotPoints[x].Xpoint = 0;
                MyPlotPoints[x].Ypoint = 0;
                MyPlotPoints[x].Mode = 0;
                MyPlotPoints[x].Thickness = 0;
                MyPlotPoints[x].PenNr = 0;                
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
            int Ydim = panel1.Height;        //  -2 for border  ?
                       

            //    Calculation of output, paper, vinyl, PCB dimensions:
            CalculateOutputPlotDivider(sender, e);  //  after loading the file, the user may have scaled the output

            progressBarLoad.Value = 0;

            Graphics g = panel1.CreateGraphics();
       
            Pen p = new Pen(Color.Blue, 4);  
            IntPtr hdc = g.GetHdc();
            
            if (checkBoxLaser.Checked == false)
            {
                MyXYZR.SetZSpeed(XYZMainForm.plotZUpSpeed);  // upwards
                MyXYZR.ZAction(sender, e, XYZMainForm.plotZUpPosition);
                DisplayAbsoluteZCoordinates(sender, e);
            }
            penUp = true;

            started = true;

          // set XY travel speed:                    
            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYTravel.Text));

            float margeX = (float)XYZMainForm.GlobalMargeX; 
            float margeY = (float)XYZMainForm.GlobalMargeY; 
                       
            checkValueX = XYZMainForm.MyCommonCoordinates.xTotalGround; 
            checkValueY = XYZMainForm.MyCommonCoordinates.yTotalGround;



      //      MessageBox.Show("margeX " + margeX.ToString() + " margeY " + margeY.ToString() + " checkValueX "+ 
        //        checkValueX.ToString() + " checkValueY  " + checkValueY.ToString());  


            int passes = 1;            
            if (checkBoxLaser.Checked) passes = int.Parse(comboBoxLaserPasses.Text);
                    
            int progressBarActualVal = 0;
            progressBarLoad.Value = 0;
            
            //  OPTION DO NOT CUT BORDER

            float minimumX = 1000000.0F;
            float minimumY = 1000000.0F;
            float maximumX = -1000000.0F;
            float maximumY = -1000000.0F;
            
            if (checkBoxLaser.Checked) passes = int.Parse(comboBoxLaserPasses.Text);
            
            progressBarLoad.Value = 0;

            oldXval = XYZMainForm.globalActualMmPosX;
            oldYval = XYZMainForm.globalActualMmPosY;

            int vectorCount = 0;


            x = 0;
            while (x < maxVectors)
            {
                if ((MyPlotPoints[x].Xpoint == 0) && (MyPlotPoints[x].Ypoint == 0)) break;
                x++;
            }

            vectorCount = x - 1;


            //  MessageBox.Show(vectorCount.ToString());

            MyXYZR.SetLaser(1);  // 1 = off !!

            for (x = 1; x < vectorCount;x++)  // vectorIndex; x++)     
            {
                if (minimumX > MyPlotPoints[x].Xpoint) minimumX = MyPlotPoints[x].Xpoint;
                if (minimumY > MyPlotPoints[x].Ypoint) minimumY = MyPlotPoints[x].Ypoint;

                if (maximumX < MyPlotPoints[x].Xpoint) maximumX = MyPlotPoints[x].Xpoint;
                if (maximumY < MyPlotPoints[x].Ypoint) maximumY = MyPlotPoints[x].Ypoint;
            }

            minimumX = minimumX / outputPlotDivider;
            minimumY = minimumY / outputPlotDivider;   // margeY;
            maximumX = maximumX / outputPlotDivider;
            maximumY = maximumY / outputPlotDivider;

            float percentX = (maximumX - minimumX) / 50;  // 2 % from border will not be cut
            float percentY = (maximumY - minimumY) / 50;  // 2 % from border will not be cut

            MyXYZR.SetLaser(1);  // 1 = off !!

            for (int y = 0; y < passes; y++)    // maxmost 10 passes
            {
                if ((y % 3) == 0) radioButtonR.Checked = true;
                else if ((y % 3) == 1) radioButtonG.Checked = true;
                else radioButtonB.Checked = true;


                MyXYZR.SetLaser(1);  // 1 = off !!


               

                for (x = 1; x < vectorCount;x++)  //vectorIndex; x++) // 
                {
                    // from  time to time upgrade progressbar

                    if ((x % 50) == 0)
                    {
                        progressBarActualVal = (y * progressBarLoad.Maximum) / passes
                                             + (x * progressBarLoad.Maximum) /( vectorIndex*passes);
                        if (progressBarActualVal < progressBarLoad.Maximum)
                            progressBarLoad.Value = progressBarActualVal;
                    }

                    if (x<listBoxHPGL.Items.Count - 1) listBoxHPGL.SelectedIndex = x;

                    if (checkBoxPause.Checked == true)
                    {
                        Application.DoEvents();
                        x--;
                        continue;
                    }

                    if (radioButtonR.Checked) kleur = 255;            // red
                    else if (radioButtonG.Checked) kleur = 255 << 8;    // green
                    else if (radioButtonB.Checked) kleur = 255 << 16;   // blue


                    try
                    {
                        thickness = MyPlotPoints[x].Thickness + 1; // thicker  
                    }
                    catch
                    {
                        thickness = 1;
                    }

                    xVal = ((float)MyPlotPoints[x].Xpoint / outputPlotDivider) + margeX;
                    yVal = ((float)MyPlotPoints[x].Ypoint / outputPlotDivider) + margeY;

              //    BELOW WAS PROTECTION AGAINS EXCESSIVE X AND Y hardware movements:
                  /*
                    if (xVal > checkValueX) continue;
                    if (yVal > checkValueY) continue;

                    if (xVal < margeX) continue;
                    if (yVal < margeY) continue;
                    */


                    //   MAKE SURE THAT BORDERS ARE NOT PLOTTED
                    
                    if (checkBoxBorderPlot.Checked == false)
                    {
                        if (xVal <= (margeX + minimumX + percentX))
                        {
                            MyXYZR.SetLaser(1);  // 1 = off !!
                            continue;
                        }
                        if (yVal <= (margeY + minimumY + percentY))
                        {
                            MyXYZR.SetLaser(1);  // 1 = off !!
                            continue;
                        }
                        if (xVal >= (margeX + maximumX - percentX))
                        {
                            MyXYZR.SetLaser(1);  // 1 = off !!
                            continue;
                        }
                        if (yVal >= (margeY + maximumY - percentY))
                        {
                            MyXYZR.SetLaser(1);  // 1 = off !!
                            continue;
                        }
                    }
                    
                    S =x.ToString()+ " x " + xVal.ToString("##.##") + " y " + yVal.ToString("##.##") + " mm";


                 //   Debug.WriteLine(S);

                 deltr.Invoke(S);



                    if (MyPlotPoints[x].Mode == 1)
                    {
                        IntPtr hObject = CreatePen(PenStyles.PS_SOLID, thickness, kleur);   //20 Color.FromArgb(Form1.PrintSignalColor));
                        SelectObject(hdc, hObject);
                        MoveToEx(hdc, (int)(MyPlotPoints[x - 1].Xpoint * 1 / outputScreenDivider),
                                  (int)(MyPlotPoints[x - 1].Ypoint * 1 / outputScreenDivider), IntPtr.Zero);
                        LineTo(hdc, (int)(MyPlotPoints[x].Xpoint * 1 / outputScreenDivider),
                                  (int)(MyPlotPoints[x].Ypoint * 1 / outputScreenDivider));
                        DeleteObject(hObject);
                    }
               
                if (MyPlotPoints[x].Mode == 0)    // Z  up
                    {
                        // Turn laser off
                        MyXYZR.SetLaser(1);  // 1 = off !!


                        if ((penUp == false)  && (x>10))          // Z is down
                        {
                            // LASER
                         if (checkBoxLaser.Checked == false)
                            {
                          MyXYZR.SetZSpeed(XYZMainForm.plotZUpSpeed); // upwards Z speed
                          XYZMainForm.plotZUpPosition = float.Parse(textBoxZupPosition.Text);

                            MyXYZR.ZAction(sender, e, XYZMainForm.plotZUpPosition);
                                DisplayAbsoluteZCoordinates(sender, e);
                            }
                         /*
                            else   // Turn laser off
                            {
                                MyXYZR.SetLaser(1);  // 1 = off !!
                            }
                         */
                            penUp = true;
                            // set XY travel speed:                                       
                            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYTravel.Text));
                        }
                        // penUp true : no action
                    }
             else if (MyPlotPoints[x].Mode == 1)    // pen should become down
                    {
                        MyXYZR.SetLaser(0);     // 0 = on

                        if (penUp == true)   // pen was up
                        {
                            // LASER
                            if (checkBoxLaser.Checked == false)
                            {
                               MyXYZR.SetZSpeed(XYZMainForm.plotZDownSpeed); // Z down speed
                                XYZMainForm.plotZDownPosition = float.Parse(textBoxZdownPosition.Text);
                               MyXYZR.ZAction(sender, e, XYZMainForm.plotZDownPosition);
                                DisplayAbsoluteZCoordinates(sender, e);
                            }
                       //     else   // Turn laser ON
                         //   {
                                MyXYZR.SetLaser(0);     // 0 = on
                        //    }

                            penUp = false; // pen is down
                            // set XY work speed:                     
                           MyXYZR.SetXYSpeed(int.Parse(comboBoxXYWork.Text));
                        }
                    }



                   
                    direction = 0;

                    if ((xVal >= oldXval) && (yVal > oldYval)) direction = 1;       
                    else if ((xVal > oldXval) && (yVal <= oldYval)) direction = 3;    
                    else if ((xVal <= oldXval) && (yVal < oldYval)) direction = 5;                           
                    else if ((xVal < oldXval) && (yVal >= oldYval)) direction = 7;


                    if (x < 2) MyXYZR.SetLaser(1); // set laser of in begin, otherwise diagonal line to first coordinate !


                if (direction > 0)
                    MyXYZR.XYAction(sender, e, direction, Math.Abs(xVal - oldXval), Math.Abs(yVal - oldYval), false);
                 
               DisplayXYCoordinates(sender, e);
                                     
                    



                    oldXval = xVal;
                    oldYval = yVal;

                }           //  end for x
            }             // end for y (laser) passes

            g.ReleaseHdc(hdc);
            g.Dispose();
                     
            //  plotter to upmost Z position:
           
            if (checkBoxLaser.Checked == false)
            {
                MyXYZR.SetZSpeed(XYZMainForm.plotZUpSpeed);
                MyXYZR.ZAction(sender, e, XYZMainForm.plotZUpPosition);

                DisplayAbsoluteZCoordinates(sender, e);
            }
            else   // Turn laser off
            {
                MyXYZR.SetLaser(1);              
            }

            penUp = true;
            progressBarLoad.Value = progressBarLoad.Maximum;

       //   back to Origin at travel speed:                    

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
                     
          //  openFileDialog1.FilterIndex = 0; // 2
           openFileDialog1.FileName =   "*.*";
               
            openFileDialog1.RestoreDirectory = true;
            
            ObjectDelegate del = new ObjectDelegate(UpdateListbox);
            ObjectDelegate del1 = new ObjectDelegate(UpdateHiddenListbox);

            listBoxHPGL.Items.Clear();  // ?
            listBoxHidden.Items.Clear();

            progressBarLoad.Value = 0;

            penThickness = 1;            // = important!
            
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                sFile = openFileDialog1.FileName;
                    if (File.Exists(sFile))
                    {
                    this.Text = sFile;
                        using (StreamReader sr = new StreamReader(sFile))
                        {

                        progressBarLoad.Value = 4;
                        
                            while (sr.EndOfStream == false)
                            {
                                S = sr.ReadLine();
                                Stemp = S;
                                TheIndex = 0;
                              
                                //     comment with // is allowed, strip it:
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
                    drawPlotFileOnScreen(sender, e);  //,1.0F);                    
        }         // file exists
    }   // openfiledialog ok                
}


        private void buttonOpenPlt_Click(object sender, EventArgs e)   // ,int FileType)
        {
            String S = "", S1 = "", S2 = "", Stemp = "";
            String sFile = "";
            int TheIndex = 0;         
            int SplitIndex = 0;
          
            openFileDialog1.Filter =  "Plt files (*.plt)|*.plt";
            openFileDialog1.FileName = "*.plt";         
            openFileDialog1.FilterIndex = 0; // 2   
            openFileDialog1.RestoreDirectory = true;

            ObjectDelegate del = new ObjectDelegate(UpdateListbox);
            ObjectDelegate del1 = new ObjectDelegate(UpdateHiddenListbox);

            listBoxHPGL.Items.Clear();  // ?
            listBoxHidden.Items.Clear();

            progressBarLoad.Value = 0;
            
            penThickness = 1;            // = important

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
                    drawPlotFileOnScreen(sender, e); //,1.0F);

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
            penThickness = 1; // important

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
            drawPlotFileOnScreen(sender, e); 
        }


        private void buttonCommonPart_Click(object sender, EventArgs e)
        {
            bool HPGLmodePD = false;
            bool HPGLmodePU = true; 
             String S = "", S1 = "";           
            int cnt = 0;
            int position = 0;
            int totaal = 0;

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
                //  take then into account PA or PR or PU or PD  ????
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
            clearPlotPoints();

            vectorIndex = 0;
           
            totaal = listBoxHPGL.Items.Count - 1;
            if (totaal == 0) totaal = 1;
            totaal = (int)(totaal / 48);
            if (totaal == 0) totaal = 1;

            bool penUp1 = false;
            
            int rectangleX = 0, rectangleY = 0;
            int circleOriginX = 0, circleOriginY = 0, radiusX = 0;
            int centerArcX = 0, centerArcY = 0, radiusArc = 0;
            int arcX = 0, arcY = 0;

            selectedPen = 1;
            penThickness = 1;
        
            while (t < listBoxHPGL.Items.Count)
            {
                listBoxHPGL.SelectedIndex = t;
                S = listBoxHPGL.Items[t].ToString();

                if ((t % totaal) == 0) progressBarLoad.Value++;
                if (progressBarLoad.Value > 45) progressBarLoad.Value = 45;
                             
                if (S.Contains("SP"))      selectedPen = GetPenNummer(S);    
                if (S.Contains("PW"))      penThickness = GetPenWidth(S);
                
                
                 if (S.Contains("PU")) penUp1 = true;
                 if (S.Contains("PD")) penUp1 = false;
                
                if ((S.Length > 5) && (S.Contains("AA")) && (vectorIndex > 2))
                {
                   arcX = GetArrayValues(S, 1);
                   arcY = GetArrayValues(S, 2);

                  radiusArc = arcY - MyPlotPoints[vectorIndex - 1].Ypoint;
                                       
                centerArcX = arcX;       
                centerArcY =MyPlotPoints[vectorIndex - 1].Ypoint + radiusArc;

                        for (double i = -90.0F; i < 91.0F; i += 9.0F)
                        {
                            double angle = i * System.Math.PI / 180;
                            int xCoord = (int)(radiusArc * System.Math.Cos(angle));
                            int yCoord = (int)(radiusArc * System.Math.Sin(angle));
                            MyPlotPoints[vectorIndex].Xpoint = centerArcX + xCoord;
                            MyPlotPoints[vectorIndex].Ypoint = centerArcY + yCoord; // // ???
                            MyPlotPoints[vectorIndex].Mode = 1;
                            vectorIndex++;
                        }                     
                }

                else if ((S.Length > 5) &&  (S.Contains("CI")) && (vectorIndex > 1))
                {
                    radiusX = GetCircleRadius(S);

                    //         MessageBox.Show(radiusX.ToString());

                    circleOriginX = MyPlotPoints[vectorIndex - 1].Xpoint;
                    circleOriginY = MyPlotPoints[vectorIndex - 1].Ypoint;
                    
                    MyPlotPoints[vectorIndex].Xpoint = circleOriginX - radiusX;
                    MyPlotPoints[vectorIndex].Ypoint = circleOriginY;  // -radiusX;
                    MyPlotPoints[vectorIndex].Mode = 0;
                    vectorIndex++;

                    for (double i = 0.0F; i < 360.0F; i += 15.0F)
                    {
                        double angle = i * System.Math.PI / 180;
                        int xCoord = (int)(radiusX * System.Math.Cos(angle));
                        int yCoord = (int)(radiusX * System.Math.Sin(angle));
                        MyPlotPoints[vectorIndex].Xpoint = circleOriginX + xCoord;
                        MyPlotPoints[vectorIndex].Ypoint = circleOriginY + yCoord;
                        MyPlotPoints[vectorIndex].Mode = 1;
                        vectorIndex++;
                    }                                                       
                }
                
                else if ((S.Length > 5) && (S.Contains(","))&& (S.Contains("EA")) && (vectorIndex>1)  )
                {
                    rectangleX = GetArrayValues(S, 1);
                    rectangleY = GetArrayValues(S, 2);

                    MyPlotPoints[vectorIndex].Xpoint = MyPlotPoints[vectorIndex-1].Xpoint;  
                    MyPlotPoints[vectorIndex].Ypoint = MyPlotPoints[vectorIndex-1].Ypoint; 
                    MyPlotPoints[vectorIndex].Mode =0;
                    vectorIndex++;

                    MyPlotPoints[vectorIndex].Xpoint = rectangleX; 
                    MyPlotPoints[vectorIndex].Ypoint = MyPlotPoints[vectorIndex - 1].Ypoint;
                    MyPlotPoints[vectorIndex].Mode = 1;
                    vectorIndex++;

                    MyPlotPoints[vectorIndex].Xpoint = rectangleX;
                    MyPlotPoints[vectorIndex].Ypoint = rectangleY; 
                    MyPlotPoints[vectorIndex].Mode = 1;
                    vectorIndex++;

                    MyPlotPoints[vectorIndex].Xpoint = MyPlotPoints[vectorIndex - 3].Xpoint; // MyPlotPoints[vectorIndex - 1].Xpoint-rectangleX;
                    MyPlotPoints[vectorIndex].Ypoint = rectangleY; 
                    MyPlotPoints[vectorIndex].Mode = 1;
                    vectorIndex++;

                    MyPlotPoints[vectorIndex].Xpoint = MyPlotPoints[vectorIndex - 4].Xpoint;
                    MyPlotPoints[vectorIndex].Ypoint = MyPlotPoints[vectorIndex - 4].Ypoint;
                    MyPlotPoints[vectorIndex].Mode = 1;
                    vectorIndex++;                    
                }

              else if ((S.Length > 5) && (S.Contains(",")))
                {
                    MyPlotPoints[vectorIndex].Xpoint = GetArrayValues(S, 1); // 1 = X
                    MyPlotPoints[vectorIndex].Ypoint = GetArrayValues(S, 2); // 2 = Y ->Y +1 is necessary to draw top border??? !
                                                       
                    if (penUp1==true) MyPlotPoints[vectorIndex].Mode = 0;
                    else MyPlotPoints[vectorIndex].Mode = 1;
                    
                    MyPlotPoints[vectorIndex].Thickness = penThickness;
                    MyPlotPoints[vectorIndex].PenNr = selectedPen;                    
                   vectorIndex++;                   
                }
                t++;
            }
                      
            // start new :

            int minimumX = 999999999, minimumY = 999999999;           

            for (t = 0; t < vectorIndex; t++)    // 20000
            {
                if (minimumX >MyPlotPoints[t].Xpoint) minimumX = MyPlotPoints[t].Xpoint;
                if (minimumY > MyPlotPoints[t].Ypoint) minimumY = MyPlotPoints[t].Ypoint;
            }
            for (t = 0; t < vectorIndex; t++)
            {
             MyPlotPoints[t].Xpoint = MyPlotPoints[t].Xpoint - minimumX;
             MyPlotPoints[t].Ypoint = MyPlotPoints[t].Ypoint - minimumY;
            }

            int maximumX = -999999999, maximumY = -999999999;
            for (t = 0; t < vectorIndex; t++)    // 20000
            {
                if (maximumX < MyPlotPoints[t].Xpoint) maximumX = MyPlotPoints[t].Xpoint;
                if (maximumY < MyPlotPoints[t].Ypoint) maximumY = MyPlotPoints[t].Ypoint;
            }

        if (checkBoxMirror.Checked)
                for (t = 0; t < vectorIndex; t++)
                        MyPlotPoints[t].Ypoint = maximumY - MyPlotPoints[t].Ypoint+1; // +1 is important!!
        
           
            minimumX = 999999999; ; minimumY = 999999999;
            maximumX = -999999999; maximumY = -999999999;

            for (t = 0; t < vectorIndex; t++)    // 
            {
                if (minimumX > MyPlotPoints[t].Xpoint) minimumX = MyPlotPoints[t].Xpoint;
                if (minimumY > MyPlotPoints[t].Ypoint) minimumY = MyPlotPoints[t].Ypoint;
            }

            for (t = 0; t < vectorIndex; t++)    // 
            {
                if (maximumX < MyPlotPoints[t].Xpoint) maximumX = MyPlotPoints[t].Xpoint;
                if (maximumY < MyPlotPoints[t].Ypoint) maximumY = MyPlotPoints[t].Ypoint;
            }

            // Values below are necessary
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

            DividerX = ((float)(maximumX - minimumX)) / panel1.Width;  //  (int)
            DividerY = ((float)(maximumY - minimumY)) / panel1.Height;
           outputScreenDivider = DividerX;
            if (DividerY > DividerX) outputScreenDivider = DividerY;
            if (outputScreenDivider == 0) outputScreenDivider = 1;
                                    
          CalculateOutputPlotDivider(sender, e);
            
          progressBarLoad.Value = progressBarLoad.Maximum;  
        }
        
                    
      private void CalculateOutputPlotDivider(object sender, EventArgs e)   // Calculates output scale for display on screen and for plotting on paper or vinyl
        {
            float dividerX = 1.0F, dividerY = 1.0F;
     
      // EXCEPT FOR CUSTOM DIMENSIONS WE HAVE ALREADY DIMENSIONS IN MM :

            if (XYZMainForm.TheScaledOutput == 11)
            {             
                XYZMainForm.PaperDimX = float.Parse(textBoxEditX.Text);
                XYZMainForm.PaperDimY = float.Parse(textBoxEditY.Text);
            }                                  
           
            dividerX =  (float)(globalMaximumX - globalMinimumX)  / (float)XYZMainForm.PaperDimX;   // 
            dividerY =  (float)(globalMaximumY - globalMinimumY) / (float)XYZMainForm.PaperDimY;   //
            
            outputPlotDivider = dividerX;
            if (dividerY > dividerX) outputPlotDivider = dividerY;
            if (outputPlotDivider == 0) outputPlotDivider = 1;
       }


        private void drawPlotFileOnScreen(object sender, EventArgs e) 
        {
            int x;
            int Ydim = panel1.Height + 1;    //     for mirrored images, without +1 top border is not drawn

            int kleur = 0;                       //      black
            int thickness = 2;                   //      pen thickness

            Graphics g = panel1.CreateGraphics();
            g.Clear(Functions.kleur(XYZMainForm.FondColor));                
                    
            IntPtr hdc = g.GetHdc();            

            for (x =2; x < maxVectors - 1; x++)  // 0 or 1 resulted in fake diagonal line
            {               
                switch (MyPlotPoints[x].PenNr)
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
                          
                thickness = MyPlotPoints[x].Thickness;

                IntPtr hObject = CreatePen(PenStyles.PS_SOLID, thickness, kleur);   //20 Color.FromArgb(Form1.PrintSignalColor));
                SelectObject(hdc, hObject);

           if (MyPlotPoints[x].Mode == 1)
             {                               
                        MoveToEx(hdc, (int)((float)(MyPlotPoints[x-1].Xpoint*drawMultiplier) / outputScreenDivider),
                            (int)((float)(MyPlotPoints[x-1].Ypoint* drawMultiplier) / outputScreenDivider) , IntPtr.Zero);

                        LineTo(hdc, (int)((float)(MyPlotPoints[x].Xpoint* drawMultiplier) / outputScreenDivider) ,
                            (int)((float)(MyPlotPoints[x].Ypoint*drawMultiplier) / outputScreenDivider) );                

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
          if (checkBoxPause.Checked)
            {                         
                // Add here this can not happen when start button was not pressed !!
                if ((checkBoxLaser.Checked) && (started==true))  MyXYZR.SetLaser(1);  // 1 = off !!             
                tableLayoutPanel1.Visible = true;                
            }
         else if (checkBoxPause.Checked==false)
            {
                // Add here this can not happen when start button was not pressed !!
                if ((checkBoxLaser.Checked) && (started == true)) MyXYZR.SetLaser(0);  // 0 = on !!             
                tableLayoutPanel1.Visible = false;
            }         
        }
     
        private void listBoxHPGL_MouseClick(object sender, MouseEventArgs e)
        {        
            int x = listBoxHPGL.SelectedIndices[0];

           // MessageBox.Show(x.ToString());


            if (x == -1) return;
            Graphics g = panel1.CreateGraphics();
            IntPtr hdc = g.GetHdc();
            
            int kleur = 255; // red            
            if (radioButtonR.Checked) kleur = 255;
            if (radioButtonG.Checked) kleur = 255<<8;
            if (radioButtonB.Checked) kleur = 255<<16;
            
            IntPtr hObject = CreatePen(PenStyles.PS_SOLID, 2,kleur);   //20 Color.FromArgb(Form1.PrintSignalColor));
            SelectObject(hdc, hObject);

            int xval = (int)(MyPlotPoints[x].Xpoint / outputScreenDivider) ;
            int yval =  (int)(MyPlotPoints[x].Ypoint / outputScreenDivider);
       
            MoveToEx(hdc, (int)(xval*drawMultiplier) - 5, (int)(yval*drawMultiplier) - 5, IntPtr.Zero);
            LineTo(hdc, (int)(xval*drawMultiplier) + 5, (int)(yval*drawMultiplier) + 5);

            MoveToEx(hdc, (int)(xval * drawMultiplier) - 5, (int)(yval * drawMultiplier) + 5, IntPtr.Zero);
            LineTo(hdc, (int)(xval * drawMultiplier) + 5, (int)(yval * drawMultiplier) - 5);
                   
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
                //  panelMaxEdit.Visible = true;               
                tableLayoutPanelDimXY.Visible = true;
            }
        }
        
        private void SetA1_Landscape_Click(object sender,EventArgs e)
        {
           XYZMainForm.PaperDimX = 840.0F;
           XYZMainForm.PaperDimY = 594.0F;                   
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
          
            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYTravel.Text));

            delListbox.Invoke(XYZMainForm.SerialPortSendS);
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
        }
              
        private void buttonSetXYWork_Click(object sender, EventArgs e)
        {
            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextboxReceive);
            XYZMainForm.SerialPortReturnS = "";
            XYZMainForm.SerialPortSendS = "";
           
            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYWork.Text));

            delListbox.Invoke(XYZMainForm.SerialPortSendS);
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
        }

        private void buttonSetZupSpeed_Click(object sender, EventArgs e)
        {
            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextboxReceive);

            XYZMainForm.SerialPortReturnS = "";
            XYZMainForm.SerialPortSendS = "";

            MyXYZR.SetZSpeed(int.Parse(comboBoxZup.Text));

            delListbox.Invoke(XYZMainForm.SerialPortSendS);
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
        }

        private void buttonSetZdownSpeed_Click(object sender, EventArgs e)
        {
            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextboxReceive);

            XYZMainForm.SerialPortReturnS = "";
            XYZMainForm.SerialPortSendS = "";
          
            MyXYZR.SetZSpeed(int.Parse(comboBoxZdown.Text));
            delListbox.Invoke(XYZMainForm.SerialPortSendS);
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
        }

        private void buttonSetZupPosition_Click(object sender, EventArgs e)
        {           
            MyXYZR.ZAction(sender, e, float.Parse(textBoxZupPosition.Text));
            DisplayAbsoluteZCoordinates(sender, e);
            penUp = true;                              
        }

        private void buttonSetZdownPosition_Click(object sender, EventArgs e)
        {       
            MyXYZR.ZAction(sender, e, float.Parse(textBoxZdownPosition.Text));
            DisplayAbsoluteZCoordinates(sender, e);
            penUp = false;
        }

        private void textBoxZdownPosition_Leave(object sender, EventArgs e)
        {           
            XYZMainForm.plotZDownPosition = float.Parse(textBoxZdownPosition.Text);
        }

        private void comboBoxZdown_Leave(object sender, EventArgs e)
        {
            int temp = 10;
            try { temp = int.Parse(comboBoxZdown.Text); }
            catch { temp = 10; }           
            XYZMainForm.plotZDownSpeed = temp;
        }

        private void comboBoxZup_Leave(object sender, EventArgs e)
        {
            int temp = 10;
            try { temp = int.Parse(comboBoxZup.Text); }
            catch { temp = 10; }            
            XYZMainForm.plotZDownSpeed = temp;
        }

        private void comboBoxXYTravel_Leave(object sender, EventArgs e)
        {
            int temp = 25;
            try { temp = int.Parse(comboBoxXYTravel.Text); }
            catch { temp = 25; }
            XYZMainForm.plotXYTravelSpeed = temp;           
        }

        private void comboBoxXYWork_Leave(object sender, EventArgs e)
        {
            int temp = 25;
            try { temp = int.Parse(comboBoxXYWork.Text); }
            catch { temp = 25; }
            XYZMainForm.plotXYWorkSpeed = temp;      
    }
         

        private void buttonZupUp_Click(object sender, EventArgs e)
        {
            changeZposition(sender, e, true, -1.0F);
             penUp = true;
        }

        private void buttonZupDown_Click(object sender, EventArgs e)
        {
            changeZposition(sender, e, true, 1.0F);
            penUp = true;
        }

        private void buttonZdownUp_Click(object sender, EventArgs e)
        {
            changeZposition(sender, e, false, -0.1F);
            penUp = false;
        }

        private void buttonZdownDown_Click(object sender, EventArgs e)
        {
            changeZposition(sender, e, false, 0.1F);
            penUp = false;
        }

        private void changeZposition(object sender, EventArgs e, bool zUp, float Zchange)
        {
            float positionZ = 10.0F;
            if (zUp == true)  // up position
            {
                try { positionZ = float.Parse(textBoxZupPosition.Text); }
                catch { positionZ = 5.0F; }
                positionZ = positionZ + Zchange;
                textBoxZupPosition.Text = positionZ.ToString("##.###");
            }
            else if (zUp == false) // down position
            {
                try { positionZ = float.Parse(textBoxZdownPosition.Text); }
                catch { positionZ = 10.0F; }
                positionZ = positionZ + Zchange;
                textBoxZdownPosition.Text = positionZ.ToString("##.###");
            }
            MyXYZR.ZAction(sender, e, positionZ);
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked) drawMultiplier=1.0F;
            else if (radioButton20.Checked) drawMultiplier = 2.0F;
            else if (radioButton40.Checked) drawMultiplier = 4.0F;                       
            drawPlotFileOnScreen(sender, e);
        }

        private void DisplayXYCoordinates(object sender, EventArgs e)
        {        
            labelXmm.Text = "X" + XYZMainForm.globalActualMmPosX.ToString("###.##") + "mm";  
            labelYmm.Text = "Y" + XYZMainForm.globalActualMmPosY.ToString("###.##") + "mm"; 
            labelZmm.Text = "Z" + XYZMainForm.globalActualMmPosZ.ToString("##.##") + "mm";              
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

            buttonStart.Text = "Start plot";

            if (checkBoxLaser.Checked)
            {
                buttonZupUp.Visible = false;
                buttonZupDown.Visible = false;

                textBoxZupPosition.Visible = false;
                buttonSetZupPosition.Visible = false;
                labelZmm.Visible = false;
                label6.Visible = false;
               comboBoxZup.Visible = false;
                comboBoxZdown.Visible = false;
                buttonSetZupSpeed.Visible = false;
                buttonSetZdownSpeed.Visible = false;
                buttonStart.ForeColor = Color.Red;
                buttonStart.Text = "Start Laser";
            }
            else
            {
                MyXYZR.SetLaser(1);  // set laser off
            }

        }

        private void textBoxZupPositionNew_Leave(object sender, EventArgs e)
        {    
            try
            {
                XYZMainForm.plotZUpPosition = float.Parse(textBoxZupPosition.Text);
            }
            catch
            {
                XYZMainForm.plotZUpPosition = 5.123F;
            }
        }
      

        // zSteps below is absolute

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
