/*
            // Stepper with 200 fullsteps/rotation:
            //  200 full steps = 1 rotation of T2 pulley 20 teeth = 20 * 2 = 40 mm displacement 
            // 3200 1/16 steps = 40 mm
            //  1  1/16 step = 40 / 3200 =  0.0125  mm             
            // So 1 mm X or Y displacement needs   80   1/16 steps 
  
 */
 
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace XYZ
{
    public partial class CutterForm : Form
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
            // added 11 1 2018:
            public int Thickness; // = width
            public int PenNr;  // = color
        }

        //////////////////////
        public static int MaxVectors = 60000;  //       is necessary for large pcb's eg feeder pcb top has 49500 lines
        ///////////////////

        public static PlotPoints[] MyPlotPoints = new PlotPoints[MaxVectors];     
        public static int TheIndex = 0;
        public static string GlobalImageFileName = "";       
        public static bool HPGLmodePD = false;
        public static bool HPGLmodePU = true;   // Pen up is default to start with
        public static Point OldPoint = new Point();
        public static int GlobalArrayIndex = 0;
        public static float OutputCoefficient = 1.0F;
        public static float TheScreenDivider = 1.0F;      // factor is used for drawing on the screen

        // input factor is used for plotting,  depends on .precision and of inch / mm
        public static float TheInputPlotDivider = 1.0F;   // depends on inch or mm, dot precision and type stepper, 200 or 400 steps / resolution

        // output factor is used for plotting on the paper, depends  of screen to paper format ratio

        public static float TheOutputPlotDivider = 1.0F;  // 
        private delegate void ObjectDelegate(object obj);
        public static Boolean PenUp = true;
        public static int FileInUse = 0;     //0 = no previous HPGL file loaded in draw module
        public static int GlobalPenThickness = 1;
        public static int GlobalSelectedPen = 1;

        // debug below temp
        public static int TempGlobalMaximumX, TempGlobalMaximumY;
        public static int TempGlobalMinimumX, TempGlobalMinimumY;



        Move MyMove = new Move();
        

        public CutterForm()
        {
            InitializeComponent();
            MyMove.InitSerialPort();

            this.Top = -1;
            this.Left = -1;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;    //  - 20;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;  //  - 50;           
                       
            Color ButtonColor = ParFuncties.kleur(XYZMainForm.IconColor);
            this.BackColor = ParFuncties.kleur(XYZMainForm.FormColor);
            
            int BTW = this.Width / 15;
            int BTM = 4;
            int BTH = this.Height / 12;

            buttonMenu.Left = BTM;
            buttonMenu.Top = BTM;
            buttonMenu.Width = BTW;
            buttonMenu.Height = BTH;

            progressBarLoad.Left = BTM + BTW + BTM;
            progressBarLoad.Top = BTM;
            progressBarLoad.Width = BTW * 5 / 4;
            progressBarLoad.Height = BTH / 2 - 4;

            groupBoxRGB.Left = BTM + BTW + BTM;
            groupBoxRGB.Top = BTM + buttonMenu.Height / 2 - 2;
            groupBoxRGB.Width = BTW * 5 / 4;
            groupBoxRGB.Height = BTH / 2 + 2;

            groupBoxRGB.BackColor = ButtonColor;

            checkBoxMirror.Left = progressBarLoad.Left + progressBarLoad.Width + BTM;
            checkBoxMirror.Top = BTM;   //
            checkBoxMirror.Width = BTW;
            checkBoxMirror.Height = BTH;

            buttonOutput.Left = checkBoxMirror.Left + BTW + BTM;
            buttonOutput.Top = BTM;
            buttonOutput.Width = BTW;
            buttonOutput.Height = BTH;

            panelMaxEdit.Left = buttonOutput.Left + BTW + BTM;
            panelMaxEdit.Top = BTM;
            panelMaxEdit.Width = BTW * 2;
            panelMaxEdit.Height = BTH;
            panelMaxLabels.Left = buttonOutput.Left + BTW + BTM;
            panelMaxLabels.Top = BTM;
            panelMaxLabels.Width = BTW * 2;
            panelMaxLabels.Height = BTH;

            buttonPlot.Left = panelMaxEdit.Left + BTW * 2 + BTM;
            buttonPlot.Top = BTM;
            buttonPlot.Width = BTW;
            buttonPlot.Height = BTH;

            buttonReset.Left = buttonPlot.Left + BTW + BTM;
            buttonReset.Top = BTM;
            buttonReset.Width = BTW;
            buttonReset.Height = BTH;

            checkBoxPause.Left = buttonReset.Left + BTW + BTM;
            checkBoxPause.Top = BTM;
            checkBoxPause.Width = BTW;
            checkBoxPause.Height = BTH;

            buttonMotorDelay.Left = checkBoxPause.Left + BTW + BTM;
            buttonMotorDelay.Top = BTM;
            buttonMotorDelay.Width = BTW;
            buttonMotorDelay.Height = BTH / 2;

            textBoxXYMotorDelay.Left = checkBoxPause.Left + BTW + BTM;
            textBoxXYMotorDelay.Top = BTM + BTH / 2;
            textBoxXYMotorDelay.Width = BTW;
            textBoxXYMotorDelay.Height = BTH / 2;

            buttonZmotorDelay.Left = buttonMotorDelay.Left + BTW + BTM;
            buttonZmotorDelay.Top = BTM;
            buttonZmotorDelay.Width = BTW;
            buttonZmotorDelay.Height = BTH / 2;

            textBoxZmotorDelay.Left = buttonMotorDelay.Left + BTW + BTM;
            textBoxZmotorDelay.Top = BTM + BTH / 2;
            textBoxZmotorDelay.Width = BTW;
            textBoxZmotorDelay.Height = BTH / 2;

            buttonPenUp.Left = buttonZmotorDelay.Left + BTW + BTM;
            buttonPenUp.Top = BTM;
            buttonPenUp.Width = BTW;
            buttonPenUp.Height = BTH / 2;

            textBoxPenUp.Left = buttonPenUp.Left;
            textBoxPenUp.Top = BTM + BTH / 2;
            textBoxPenUp.Width = BTW;
            textBoxPenUp.Height = BTH / 2;

            buttonPenDown.Left = buttonPenUp.Left + BTW + BTM;
            buttonPenDown.Top = BTM;
            buttonPenDown.Width = BTW;
            buttonPenDown.Height = BTH / 2;

            textBoxPenDown.Left = buttonPenDown.Left;
            textBoxPenDown.Top = BTM + BTH / 2;
            textBoxPenDown.Width = BTW;
            textBoxPenDown.Height = BTH / 2;

            buttonQuit.Left = buttonPenDown.Left + BTW + BTM;
            buttonQuit.Top = BTM;
            buttonQuit.Width = BTW;
            buttonQuit.Height = BTH;

            listBoxHPGL.Left = BTM;
            listBoxHPGL.Top = BTM * 3 + BTH;
            listBoxHPGL.Width = BTW * 3;
            listBoxHPGL.Height = this.Height / 2;

            textBoxReceive.Left = BTM;
            textBoxReceive.Top = listBoxHPGL.Top + this.Height / 2 + BTM;
            textBoxReceive.Width = listBoxHPGL.Width;
            textBoxReceive.Height = this.Height / 3;

            pictureBox1.Top = listBoxHPGL.Top;
            pictureBox1.Left = listBoxHPGL.Left + listBoxHPGL.Width + BTM;
            pictureBox1.Height = this.Height - (BTH + BTM * 12);
            pictureBox1.Width = this.Width - (listBoxHPGL.Width + BTM * 8);

            buttonOutput.BackColor = ButtonColor;
            buttonReset.BackColor = ButtonColor;
            buttonMotorDelay.BackColor = ButtonColor;
            buttonZmotorDelay.BackColor = ButtonColor;
            buttonPenUp.BackColor = ButtonColor;
            buttonPenDown.BackColor = ButtonColor;
            buttonPlot.BackColor = ButtonColor;
            buttonQuit.BackColor = ButtonColor;
            checkBoxMirror.BackColor = ButtonColor;
            checkBoxPause.BackColor = ButtonColor;           

            pictureBox1.Top = listBoxHPGL.Top;
            pictureBox1.Left = listBoxHPGL.Left + listBoxHPGL.Width + 20;
            pictureBox1.Width = this.Width - (listBoxHPGL.Width + 50);
            pictureBox1.Height = this.Height - (pictureBox1.Top + 75);

            for (int x = 0; x < MaxVectors; x++)
            {
                MyPlotPoints[x].Xpoint = 0;
                MyPlotPoints[x].Ypoint = 0;
                MyPlotPoints[x].Mode = 0;
                MyPlotPoints[x].Thickness = 0;
                MyPlotPoints[x].PenNr = 0;
            }
            
            buttonMenu.BackColor = ButtonColor;
            buttonReset.BackColor = ButtonColor;
            buttonMotorDelay.BackColor = ButtonColor;
            buttonZmotorDelay.BackColor = ButtonColor;
            buttonPenUp.BackColor = ButtonColor;
            buttonPenDown.BackColor = ButtonColor;
            buttonPlot.BackColor = ButtonColor;
            buttonQuit.BackColor = ButtonColor;
            checkBoxMirror.BackColor = ButtonColor;
            checkBoxPause.BackColor = ButtonColor;          
            pictureBox1.BackColor = ParFuncties.kleur(XYZMainForm.FondColor);

            buttonMenu.Text = XYZMainForm.StrMenu;
            checkBoxMirror.Text = XYZMainForm.StrMirror;
            buttonReset.Text = XYZMainForm.StrReset;
            buttonMotorDelay.Text = "XY " + XYZMainForm.StrDelay;
            buttonZmotorDelay.Text = "Z " + XYZMainForm.StrDelay;
            
            buttonQuit.Text = XYZMainForm.StrQuit;        
            checkBoxPause.Text = XYZMainForm.StrPause;
            
            textBoxEditX.Text = (XYZMainForm.CustomCutterDimX / 10000).ToString();
            textBoxEditDotX.Text = (XYZMainForm.CustomCutterDimX - ((XYZMainForm.CustomCutterDimX / 10000) * 10000)).ToString();

            textBoxEditY.Text = (XYZMainForm.CustomCutterDimY / 10000).ToString();
            textBoxEditDotY.Text = (XYZMainForm.CustomCutterDimY - ((XYZMainForm.CustomCutterDimY / 10000) * 10000)).ToString();
            
            if (XYZMainForm.TheScaledOutput < 11)
            {
                panelMaxEdit.Visible = false;
                panelMaxLabels.Visible = true;

                switch (XYZMainForm.TheScaledOutput)
                {
                    case 1:
                        labelMaxXmm.Text = "A1 landscape X 840 mm";
                        labelMaxYmm.Text = "A1 landscape Y 594 mm";
                        break;
                    case 2:
                        labelMaxXmm.Text = "A1 portrait X 594 mm";
                        labelMaxYmm.Text = "A1 portrait Y 840 mm";
                        break;
                    case 3:
                        labelMaxXmm.Text = "A2 landscape X 594 mm";
                        labelMaxYmm.Text = "A2 landscape Y 420 mm";
                        break;
                    case 4:
                        labelMaxXmm.Text = "A2 landscape X 420 mm";
                        labelMaxYmm.Text = "A2 landscape Y 594 mm";
                        break;
                    case 5:
                        labelMaxXmm.Text = "A3 landscape X 420 mm";
                        labelMaxYmm.Text = "A3 landscape Y 297 mm";
                        break;
                    case 6:
                        labelMaxXmm.Text = "A3 portrait X 297 mm";
                        labelMaxYmm.Text = "A3 portrait Y 420 mm";
                        break;
                    case 7:
                        labelMaxXmm.Text = "A4 landscape X 297 mm";
                        labelMaxYmm.Text = "A4 landscape Y 210 mm";
                        break;
                    case 8:
                        labelMaxXmm.Text = "A4 landscape X 210 mm";
                        labelMaxYmm.Text = "A4 landscape Y 297 mm";
                        break;
                    case 9:
                        labelMaxXmm.Text = "A5 landscape X 210 mm";
                        labelMaxYmm.Text = "A5 landscape Y 148 mm";
                        break;
                    case 10:
                        labelMaxXmm.Text = "A5 portrait X 148 mm";
                        labelMaxYmm.Text = "A5 portrait Y 210 mm";
                        break;
                    default: break;
                }
            }
            if (XYZMainForm.TheScaledOutput == 11)
            {
                panelMaxLabels.Visible = false;
                panelMaxEdit.Visible = true;
            }                      
        }
        
        private void buttonQuit_Click(object sender, EventArgs e)
        {
            int DotPart = 0;
            // X calculation
            try { DotPart = int.Parse(textBoxEditDotX.Text); }
            catch { DotPart = 0; }
            try { XYZMainForm.CustomCutterDimX = int.Parse(textBoxEditX.Text) * 10000 + DotPart; }
            catch { XYZMainForm.CustomCutterDimX = 1600000; }    // =  PCB eurocard format              
                                                           // Y calculation
            try { DotPart = int.Parse(textBoxEditDotY.Text); }
            catch { DotPart = 0; }
            try { XYZMainForm.CustomCutterDimY = int.Parse(textBoxEditY.Text) * 10000 + DotPart; }
            catch { XYZMainForm.CustomCutterDimY = 1000000; }    // =  PCB eurocard format                                            

            MyMove.CloseSerialPort();           
            this.Close();
        }

        private void buttonMenu_MouseClick(object sender, MouseEventArgs e)
        {
            FileInUse = 0;

            if (e.Button == MouseButtons.Left)
            {
                progressBarLoad.Value = 0; // is always ok then                
                ContextMenuStrip ctx = new ContextMenuStrip();
                ctx.Font = new Font("Arial", 13);

                if (XYZMainForm.GlobalHPGLFileName.Length > 4)
                {
                    FileInUse = 1;
                    ctx.Items.Add(XYZMainForm.StrReopen + "  " + XYZMainForm.GlobalHPGLFileName, null, buttonOpenPrevious_Click);        // "Reopen"    
                    ctx.Items.Add("_________________________", null, null);
                }
               
                ctx.Items.Add(XYZMainForm.StrOpenFile, null, buttonOpenHpgl_Click); // "Open HPGL"
                ctx.Items.Add("_________________________", null, null);

                ctx.Items.Add(XYZMainForm.StrClearAll, null, buttonClearAll_Click);  // "Clear all"
                ctx.Items.Add("_________________________", null, null);

                ctx.Items.Add(XYZMainForm.StrReset + " plotter", null, buttonReset_Click); // "Reset plotter"
                ctx.Items.Add("_________________________", null, null);

                if (listBoxHPGL.Items.Count > 2)
                {
                    ctx.Items.Add(XYZMainForm.StrStartPlot, null, buttonPlot_Click);  // buttonExecuteHPGL_Click);  // "Send to plotter"
                    ctx.Items.Add("_________________________", null, null);
                }

                ctx.Items.Add(XYZMainForm.StrQuit, null, buttonQuit_Click);
                ctx.Show(this, new Point(buttonMenu.Left, buttonMenu.Top + buttonMenu.Height + 10));
            }
        }


        private void buttonReset_Click(object sender, EventArgs e)
        {            
     MyMove.NextCommand = true;                    //  anyway !!
                                                   //  TheOutput(sender, e, 5);    // 
            MyMove.Reset();
        }
        
        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            int x;
            pictureBox1.Image = null;
            listBoxHPGL.Items.Clear();

            textBoxReceive.Clear();

            Graphics g = pictureBox1.CreateGraphics();
            Pen p = new Pen(Color.Green, 2);

            g.Clear(pictureBox1.BackColor);

            for (x = 0; x < GlobalArrayIndex; x++)          // 20000 ?
            {
                MyPlotPoints[x].Xpoint = 0;
                MyPlotPoints[x].Ypoint = 0;
                MyPlotPoints[x].Mode = 0;
            }

            g.Dispose();
            buttonPlot.Visible = false;
        }


        private void buttonExecuteHPGL_Click(object sender, EventArgs e)
        {
            ObjectDelegate deltr = new ObjectDelegate(UpdateTextboxReceive);
            String S = "";

            // Calculation of output, paper, vinyl dimensions:

            CalculateOutputPlotDivider(sender, e); //  is once more necessary: after loading the file, the user may scale the output

            int x, Xval = 0, Yval = 0, Direction = 0, oldXval = 0, oldYval = 0;
            int checkValueX = 0, checkValueY = 0;

            int kleur = 255, thickness = 2;
            int Ydim = pictureBox1.Height;        //  -2 for border  ?                

            Graphics g = pictureBox1.CreateGraphics();
            //    g.Clear(Color.White);
            Pen p = new Pen(Color.Blue, 4);  //
            IntPtr hdc = g.GetHdc();
            PenUp = true;  // starts with the pen up

            MyPlotPoints[0].Mode = 0;     // otherwise resulted in fake diagonal line            
            MyPlotPoints[1].Mode = 0;     // otherwise this resulted in fake diagonal line on startup
            MyPlotPoints[2].Mode = 0;     // otherwise this resulted in fake diagonal line on startup
            MyPlotPoints[3].Mode = 0;     // otherwise this resulted in fake diagonal line on startup                                   

            int stepFactor = 160;  // normal stepper 200steps/revolution 1/32 microsteps and Pulley T20
            if (XYZMainForm.ResolutionStepperXY == 400) stepFactor = stepFactor * 2;  // stepper with 400 full steps per revolution           
            if (XYZMainForm.TeethPulleyXY == 16) stepFactor = (stepFactor * 125) / 100;   //  Pulley T16 !

            if (PenUp == false)
            {
                while (MyMove.NextCommand == false) Application.DoEvents();
                zAction(sender, e, 1, XYZMainForm.cutZUpSpeed);    //  ZPenSteps);   // 1 = upwards
                PenUp = true;
            }

            int PlotMargeX = (int)((float)(XYZMainForm.GlobalMargeX * stepFactor));
            int PlotMargeY = (int)((float)(XYZMainForm.GlobalMargeY * stepFactor));

            Xval = (int)(((float)MyPlotPoints[1].Xpoint * stepFactor) / TheOutputPlotDivider) + PlotMargeX;
            Yval = (int)(((float)MyPlotPoints[1].Ypoint * stepFactor) / TheOutputPlotDivider) + PlotMargeY;

            checkValueX = (int)((float)XYZMainForm.GlobalMaxDimensionX * stepFactor); // * TheOutputPlotDivider);
            checkValueY = (int)((float)XYZMainForm.GlobalMaxDimensionY * stepFactor); // * TheOutputPlotDivider);

            S = "MargeX " + PlotMargeX.ToString("") + " MargeY " + PlotMargeY.ToString("");
            deltr.Invoke(S);

            S = "checkValueX " + checkValueX.ToString("") + " checkValueY " + checkValueY.ToString("");
            deltr.Invoke(S);

            for (x = 2; x < MaxVectors - 1; x++)       // WAS 1      0 resulted often in fake diagonal line           
            {
                if (x < (listBoxHPGL.Items.Count - 1)) listBoxHPGL.SelectedIndex = x;
                try
                {
                    S = listBoxHPGL.Items[x].ToString();
                }
                catch
                {
                    S = "";
                }

                if ((x > 1000) && (S.Contains(";") == false)) break;

                switch (MyPlotPoints[x + 1].PenNr)   // MyPens[x].PenNummer)
                {
                    case 1: kleur = 0; break;  //black
                    case 2: kleur = 128; break;
                    case 3: kleur = 255; break;
                    case 4: kleur = 128 + 128 << 8; break;
                    case 5: kleur = 255 + 255 << 8; break;
                    case 6: kleur = 255 << 8; break;
                    case 7: kleur = 255 << 16; break;
                    case 8: kleur = 128 + 255 << 8; break;
                    case 9: kleur = 255 + 255 << 8 + 255 << 16; break;
                    default: kleur = 0; break;  // black
                }

                thickness = MyPlotPoints[x].Thickness;
                if (checkBoxPause.Checked == true)
                {
                    Application.DoEvents();
                    x = x - 1;
                    continue;
                }
                Xval = (int)(((float)MyPlotPoints[x].Xpoint * stepFactor) / TheOutputPlotDivider) + PlotMargeX;
                Yval = (int)(((float)MyPlotPoints[x].Ypoint * stepFactor) / TheOutputPlotDivider) + PlotMargeY;

                // BELOW PROTECTION AGAINS EXCESSIVE X AND Y movements:

                if (Xval > checkValueX) continue;
                if (Yval > checkValueY) continue;

                if ((x > 10) && (Xval == PlotMargeX) && (Yval == PlotMargeY)) continue;

                S = "Xval " + Xval.ToString("") + " Yval " + Yval.ToString("");
                deltr.Invoke(S);

                if (MyPlotPoints[x].Mode == 1)
                {
                    IntPtr hObject = CreatePen(PenStyles.PS_SOLID, thickness, kleur);   //20 Color.FromArgb(Form1.PrintSignalColor));
                    SelectObject(hdc, hObject);

                    MoveToEx(hdc, (int)(MyPlotPoints[x - 1].Xpoint / TheScreenDivider),
                           (int)(MyPlotPoints[x - 1].Ypoint / TheScreenDivider), IntPtr.Zero);

                    LineTo(hdc, (int)(MyPlotPoints[x].Xpoint / TheScreenDivider),
                          (int)(MyPlotPoints[x].Ypoint / TheScreenDivider));

                    DeleteObject(hObject);
                }

                //  1  =  up
                //  0  =  down

                if (MyPlotPoints[x].Mode == 0)    // pen should become up
                {
                    if (PenUp == false)            // pen is down
                    {
                        while (MyMove.NextCommand == false) Application.DoEvents();
                        zAction(sender, e, 1, XYZMainForm.cutZUpSpeed);  //.ZPenSteps);   // pen becomes up
                        PenUp = true;
                    }
                    // Penup true : no action
                }
                else if (MyPlotPoints[x].Mode == 1)    // pen should become down
                {
                    if (PenUp == true)   //  pen is up
                    {
                        while (MyMove.NextCommand == false) Application.DoEvents();
                        zAction(sender, e, 0, XYZMainForm.cutZDownSpeed); // .ZPenSteps);   // pen becomes down
                        PenUp = false;
                    }
                    // Penup false : no action
                }

                Direction = 0;

                /*                           5 
                 *                           
                 *                    5            3
                 *                      
                 *                 7                  3
                 * 
                 *                    7            1
                 * 
                 *                          1 
                 */

                if ((Xval >= oldXval) && (Yval > oldYval)) Direction = 1;       //   
                else if ((Xval > oldXval) && (Yval <= oldYval)) Direction = 3;  //  
                else if ((Xval <= oldXval) && (Yval < oldYval)) Direction = 5;   //                        
                else if ((Xval < oldXval) && (Yval >= oldYval)) Direction = 7;   //                        

                /*
                if ((Xval >= oldXval) && (Yval > oldYval)) Direction = 1;       //  3 
                else if ((Xval >= oldXval) && (Yval < oldYval)) Direction = 3;  //  1
                else if ((Xval < oldXval) && (Yval >= oldYval)) Direction = 7;  //  5
                else if ((Xval < oldXval) && (Yval < oldYval)) Direction = 5;   //  7                       
                */
                if ((MyPlotPoints[x].Mode == 0) || (MyPlotPoints[x].Mode == 1))
                {
                    while (MyMove.NextCommand == false) Application.DoEvents();
                    XYAction(sender, e, Direction, Math.Abs(Xval - oldXval), Math.Abs(Yval - oldYval));

                    oldXval = Xval;
                    oldYval = Yval;
                }
            }           //  end for
            g.ReleaseHdc(hdc);
            g.Dispose();

            // back to Origin 
            while (MyMove.NextCommand == false) Application.DoEvents();
            XYAction(sender, e, 5, Math.Abs(oldXval), Math.Abs(oldYval));       // 5 = left
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
            int x = 0;
            int SplitIndex = 0;
            int[] CoordinatesX = new int[1000];  // maxmost 1000
            int[] CoordinatesY = new int[1000];
            int[] CoordinatesComma = new int[2000];

            openFileDialog1.Filter =                
            "Hpgl files(*.hpgl) | *.hpgl |Plt files (*.plt)|*.plt|Pen files(*.pen) | *.pen";
            
            // "Plt files(*.plt)|*.plt|Hpgl files(*.hpgl)|*.hpgl";

            openFileDialog1.FilterIndex = 0; // 2
            openFileDialog1.FileName = "*.hpgl";  // *.hpgl";
            openFileDialog1.RestoreDirectory = true;

            // MGB = 0;
            ObjectDelegate del = new ObjectDelegate(UpdateListbox);
            ObjectDelegate del1 = new ObjectDelegate(UpdateHiddenListbox);

            listBoxHPGL.Items.Clear();  // ?
            listBoxHidden.Items.Clear();

            progressBarLoad.Value = 0;

            GlobalArrayIndex = 0;
            GlobalPenThickness = 1;            // = important!

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
                            //     A
                            //       if comment with // is allowed, strip it:
                            if (Stemp.Contains("//"))
                            {
                                TheIndex = S.IndexOf("//");
                                if (TheIndex > 0) Stemp = Stemp.Remove(TheIndex, Stemp.Length - TheIndex);
                            }
                            Stemp.Trim();
                            //    B
                            //      if S contains various ';' signs, splitting is necessary
                            //      so that each line in the listbox contains one statement 
                            do
                            {
                                SplitIndex = 0;
                                SplitIndex = Stemp.IndexOf(';');
                                S1 = Stemp.Substring(0, SplitIndex + 1);
                                //////********  
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

                    buttonPlot.Visible = true;
                    DrawArrayXY(sender, e);

                }         // file exists
            }   // openfiledialog ok
        }


        private void buttonOpenPrevious_Click(object sender, EventArgs e)
        {
            String S = "", S1 = "", S2 = "", Stemp = "";            
          
            int SplitIndex = 0;

            int[] CoordinatesX = new int[1000];  // maxmost 1000
            int[] CoordinatesY = new int[1000];
            int[] CoordinatesComma = new int[2000];

            ObjectDelegate del = new ObjectDelegate(UpdateListbox);
            ObjectDelegate del1 = new ObjectDelegate(UpdateHiddenListbox);

            listBoxHPGL.Items.Clear();  // ?
            listBoxHidden.Items.Clear();

            progressBarLoad.Value = 0;

            GlobalArrayIndex = 0; // 

            GlobalPenThickness = 1; // = important

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
                        //     A
                        //       if comment with // is allowed, strip it:
                        if (Stemp.Contains("//"))
                        {
                            TheIndex = S.IndexOf("//");
                            if (TheIndex > 0) Stemp = Stemp.Remove(TheIndex, Stemp.Length - TheIndex);
                        }
                        Stemp.Trim();
                        //    B
                        //      if S contains various ';' signs, splitting is necessary
                        //      so that each line in the listbox contains one statement 
                        do
                        {
                            SplitIndex = 0;
                            SplitIndex = Stemp.IndexOf(';');
                            S1 = Stemp.Substring(0, SplitIndex + 1);
                            //////********  
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
            buttonPlot.Visible = true;
            DrawArrayXY(sender, e);
        }
            
        private void buttonCommonPart_Click(object sender, EventArgs e)
        {
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
                if (CoordinatesComma[1] == 0) del.Invoke(S1);  //  + "\r\n" will be added by delegate there as it is

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
            GlobalArrayIndex = 0;

            for (t = 0; t < MaxVectors; t++)
            {
                MyPlotPoints[t].Xpoint = 0;
                MyPlotPoints[t].Ypoint = 0;
                MyPlotPoints[t].Mode = 0; // makes sure the previous file is not drawn.
                MyPlotPoints[t].PenNr = 0;
                MyPlotPoints[t].Thickness = 0;
            }

            t = 0;
            totaal = listBoxHPGL.Items.Count - 1;
            if (totaal == 0) totaal = 1;
            totaal = (int)(totaal / 48);
            if (totaal == 0) totaal = 1;

            while (t < listBoxHPGL.Items.Count)
            {
                listBoxHPGL.SelectedIndex = t;
                S = listBoxHPGL.Items[t].ToString();

                if ((t % totaal) == 0) progressBarLoad.Value++;
                if (progressBarLoad.Value > 45) progressBarLoad.Value = 45;

                // important:

                if (S.Contains("SP")) GlobalSelectedPen = GetPenNummer(S);
                if (S.Contains("PW")) GlobalPenThickness = GetPenWidth(S);

                if ((S.Length > 5) && (S.Contains(",")))
                {
                    MyPlotPoints[GlobalArrayIndex].Xpoint = GetArrayValues(S, 1); // 1 = X
                    MyPlotPoints[GlobalArrayIndex].Ypoint = GetArrayValues(S, 2); // 2 = Y ->Y +1 is necessary to draw top border??? !
                    MyPlotPoints[GlobalArrayIndex].Mode = 1;      // original only 1 = pd and 0 = pu are supported

                    if (S.Contains("PD")) MyPlotPoints[GlobalArrayIndex].Mode = 1;

                    if (S.Contains("PU")) MyPlotPoints[GlobalArrayIndex].Mode = 0;

                    MyPlotPoints[GlobalArrayIndex].Thickness = GlobalPenThickness;
                    MyPlotPoints[GlobalArrayIndex].PenNr = GlobalSelectedPen;

                    GlobalArrayIndex++;
                }
                t++;
            }

            // start new :

            int minimumX = 999999999, minimumY = 999999999;

            for (t = 0; t < GlobalArrayIndex; t++)    // 20000
            {
                if (minimumX > MyPlotPoints[t].Xpoint) minimumX = MyPlotPoints[t].Xpoint;
                if (minimumY > MyPlotPoints[t].Ypoint) minimumY = MyPlotPoints[t].Ypoint;
            }
            for (t = 0; t < GlobalArrayIndex; t++)
            {
                MyPlotPoints[t].Xpoint = MyPlotPoints[t].Xpoint - minimumX;
                MyPlotPoints[t].Ypoint = MyPlotPoints[t].Ypoint - minimumY;
            }
            int maximumX = -999999999, maximumY = -999999999;
            for (t = 0; t < GlobalArrayIndex; t++)    // 20000
            {
                if (maximumX < MyPlotPoints[t].Xpoint) maximumX = MyPlotPoints[t].Xpoint;
                if (maximumY < MyPlotPoints[t].Ypoint) maximumY = MyPlotPoints[t].Ypoint;
            }

            // HERE :

            if (checkBoxMirror.Checked)
                for (t = 0; t < GlobalArrayIndex; t++)
                    MyPlotPoints[t].Ypoint = maximumY - MyPlotPoints[t].Ypoint;



            // Values below are necessary
            TempGlobalMaximumX = maximumX;
            TempGlobalMaximumY = maximumY;
            TempGlobalMinimumX = minimumX;
            TempGlobalMinimumY = minimumY;




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
            TheScreenDivider = DividerX;
            if (DividerY > DividerX) TheScreenDivider = DividerY;
            if (TheScreenDivider == 0) TheScreenDivider = 1;

        //  new
            CalculateOutputPlotDivider(sender, e);
            progressBarLoad.Value = progressBarLoad.Maximum;
            DrawArrayXY(sender, e);
        }        

        private void CalculateOutputPlotDivider(object sender, EventArgs e)   // Calculates output scale for display on screen and for plotting on paper or vinyl
        {
            float DividerX = 1, DividerY = 1;
            // EXCEPT FOR CUSTOM DIMENSIONS WE HAVE ALREADY DIMENSIONS IN MM *10000:

            if (XYZMainForm.TheScaledOutput == 11)
            {
                int DotPart = 0;
                // X calculation
                try { DotPart = int.Parse(textBoxEditDotX.Text); }
                catch { DotPart = 0; }
                try { XYZMainForm.PaperDimX = int.Parse(textBoxEditX.Text) * 10000 + DotPart; }
                catch { XYZMainForm.PaperDimX = 1600000; }    // =  PCB eurocard format              
                                                              // Y calculation
                try { DotPart = int.Parse(textBoxEditDotY.Text); }
                catch { DotPart = 0; }
                try { XYZMainForm.PaperDimY = int.Parse(textBoxEditY.Text) * 10000 + DotPart; }
                catch { XYZMainForm.PaperDimY = 1000000; }    // =  PCB eurocard format                                            
            }

            DividerX = ((float)(TempGlobalMaximumX - TempGlobalMinimumX) * 10000) / (float)XYZMainForm.PaperDimX;   // 
            DividerY = ((float)(TempGlobalMaximumY - TempGlobalMinimumY) * 10000) / (float)XYZMainForm.PaperDimY;   //

            TheOutputPlotDivider = DividerX;
            if (DividerY > DividerX) TheOutputPlotDivider = DividerY;
            if (TheOutputPlotDivider == 0) TheOutputPlotDivider = 1;
            //  MessageBox.Show(TheOutputPlotDivider.ToString("###.###"));   
        }
        
        private void DrawArrayXY(object sender, EventArgs e)
        {
            int x;
            int kleur = 0;                       //      black
            int thickness = 2;                   //      pen thickness

            Graphics g = pictureBox1.CreateGraphics();
            IntPtr hdc = g.GetHdc();

            for (x = 2; x < MaxVectors - 1; x++)  // 0 or 1 resulted in fake diagonal line
            {
                switch (MyPlotPoints[x].PenNr)
                {
                    case 1: kleur = 0; break;
                    case 2: kleur = 128; break;
                    case 3: kleur = 255; break;
                    case 4: kleur = 128 + 128 << 8; break;
                    case 5: kleur = 255 + 255 << 8; break;
                    case 6: kleur = 255 << 8; break;
                    case 7: kleur = 255 << 16; break;
                    case 8: kleur = 128 + 255 << 8; break;
                    case 9: kleur = 255 + 255 << 8 + 255 << 16; break;
                    default: kleur = 0; break;  // black
                }

                thickness = MyPlotPoints[x].Thickness;

                IntPtr hObject = CreatePen(PenStyles.PS_SOLID, thickness, kleur);   //20 Color.FromArgb(Form1.PrintSignalColor));
                SelectObject(hdc, hObject);

                int PlotYval = (int)(MyPlotPoints[x - 1].Ypoint / TheScreenDivider);

                if (MyPlotPoints[x].Mode == 1)
                {
                    MoveToEx(hdc, (int)(MyPlotPoints[x - 1].Xpoint / TheScreenDivider),
                        (int)(MyPlotPoints[x - 1].Ypoint / TheScreenDivider), IntPtr.Zero);

                    LineTo(hdc, (int)(MyPlotPoints[x].Xpoint / TheScreenDivider),
                        (int)(MyPlotPoints[x].Ypoint / TheScreenDivider));

                }   // mode == 1

                DeleteObject(hObject);
            } // for loop

            g.ReleaseHdc(hdc);
            g.Dispose();           
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

        private void buttonMotorDelay_Click(object sender, EventArgs e)
        {         
            int tempdelay = 75;
            try
            {
                tempdelay = int.Parse(textBoxXYMotorDelay.Text.ToString());
            }
            catch
            {
                tempdelay = 50;
            }
            if (tempdelay < 1) tempdelay = 1;
            if (tempdelay > 255) tempdelay = 255;
            MyMove.SetXYMotorsDelay(tempdelay);
        }

        private void buttonPenUp_Click(object sender, EventArgs e)
        {
            int StepsUp = 100;
            try
            {
                StepsUp = int.Parse(textBoxPenUp.Text);
            }
            catch
            {
                StepsUp = 100;
                MessageBox.Show(textBoxPenUp.Text.ToString() + " not ok");
                return;
            }
            zAction(sender, e, 0, XYZMainForm.cutZDownPosition);  //.ZCutterSteps);
            PenUp = true;
        }

        private void buttonPenDown_Click(object sender, EventArgs e)
        {
            int StepsDown = 100;
            try
            {
                StepsDown = int.Parse(textBoxPenDown.Text);
            }
            catch
            {
                StepsDown = 100;
                MessageBox.Show(textBoxPenDown.Text.ToString() + " not ok");
                return;
            }
            zAction(sender, e, 0, XYZMainForm.cutZDownSpeed); // .ZCutterSteps);
            PenUp = false;
        }


        private void buttonPlot_Click(object sender, EventArgs e)
        {
            buttonMotorDelay.Visible = false;
            buttonZmotorDelay.Visible = false;
            buttonPenUp.Visible = false;
            buttonPenDown.Visible = false;
            textBoxXYMotorDelay.Visible = false;
            textBoxZmotorDelay.Visible = false;
            textBoxPenUp.Visible = false;
            textBoxPenDown.Visible = false;
            buttonExecuteHPGL_Click(sender, e);
        }

        private void checkBoxPause_CheckStateChanged(object sender, EventArgs e)
        {
            buttonMotorDelay.Visible = false;
            buttonZmotorDelay.Visible = false;
            buttonPenUp.Visible = false;
            buttonPenDown.Visible = false;
            textBoxXYMotorDelay.Visible = false;
            textBoxZmotorDelay.Visible = false;
            textBoxPenUp.Visible = false;
            textBoxPenDown.Visible = false;

            if (checkBoxPause.Checked)
            {
                buttonMotorDelay.Visible = true;
                buttonZmotorDelay.Visible = true;
                buttonPenUp.Visible = true;
                buttonPenDown.Visible = true;
                textBoxXYMotorDelay.Visible = true;
                textBoxZmotorDelay.Visible = true;
                textBoxPenUp.Visible = true;
                textBoxPenDown.Visible = true;
            }
        }

        private void buttonZmotorDelay_Click(object sender, EventArgs e)
        {        
            int tempdelay = 75;
            try
            {
                tempdelay = int.Parse(textBoxZmotorDelay.Text.ToString());
            }
            catch
            {
                tempdelay = 75;
            }
            if (tempdelay < 1) tempdelay = 1;  //  lower then 25 the motors do not turn ?
            if (tempdelay > 30000) tempdelay = 30000;
            MyMove.LiftRotDelay(tempdelay);
        }

        private void listBoxHPGL_MouseClick(object sender, MouseEventArgs e)
        {     
            int x = listBoxHPGL.SelectedIndices[0];
            if (x == -1) return;
            Graphics g = pictureBox1.CreateGraphics();
            IntPtr hdc = g.GetHdc();

            int kleur = 255; // red            
            if (radioButtonR.Checked) kleur = 255;
            if (radioButtonG.Checked) kleur = 255 << 8;
            if (radioButtonB.Checked) kleur = 255 << 16;

            IntPtr hObject = CreatePen(PenStyles.PS_SOLID, 1, kleur);   //20 Color.FromArgb(Form1.PrintSignalColor));
            SelectObject(hdc, hObject);

            int xval = (int)(MyPlotPoints[x].Xpoint / TheScreenDivider);
            int yval = (int)(MyPlotPoints[x].Ypoint / TheScreenDivider);

            MoveToEx(hdc, xval - 3, yval - 3, IntPtr.Zero);
            LineTo(hdc, xval + 3, yval + 3);

            MoveToEx(hdc, xval - 3, yval + 3, IntPtr.Zero);
            LineTo(hdc, xval + 3, yval - 3);

            DeleteObject(hObject);
            g.ReleaseHdc(hdc);
            g.Dispose();

        }


        private void buttonOutput_MouseClick(object sender, MouseEventArgs e)
        {
            int DotPart = 0;
         // X calculation
            try { DotPart = int.Parse(textBoxEditDotX.Text); }
            catch { DotPart = 0; }
            try { XYZMainForm.CustomCutterDimX = int.Parse(textBoxEditX.Text) * 10000 + DotPart; }
            catch { XYZMainForm.CustomCutterDimX = 1600000; }    // =  PCB eurocard format              
         // Y calculation
            try { DotPart = int.Parse(textBoxEditDotY.Text); }
            catch { DotPart = 0; }
            try { XYZMainForm.CustomCutterDimY = int.Parse(textBoxEditY.Text) * 10000 + DotPart; }
            catch { XYZMainForm.CustomCutterDimY = 1000000; }    // =  PCB eurocard format     


            if (e.Button == MouseButtons.Left)
            {
                ContextMenuStrip ctx1 = new ContextMenuStrip();
                ctx1.Font = new Font("Arial", 12);
                ctx1.Items.Add("A1 landscape 840 * 594 mm", null, SetA1_Landscape_Click);
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
                    case 1: ((ToolStripMenuItem)ctx1.Items[0]).Checked = true; break;
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

        private void SetLabels(object sender, EventArgs e)
        {
            int DotPart = 0;
            // X calculation
            try { DotPart = int.Parse(textBoxEditDotX.Text); }
            catch { DotPart = 0; }
            try { XYZMainForm.CustomCutterDimX = int.Parse(textBoxEditX.Text) * 10000 + DotPart; }
            catch { XYZMainForm.CustomCutterDimX = 1600000; }    // =  PCB eurocard format              
                                                           // Y calculation
            try { DotPart = int.Parse(textBoxEditDotY.Text); }
            catch { DotPart = 0; }
            try { XYZMainForm.CustomCutterDimY = int.Parse(textBoxEditY.Text) * 10000 + DotPart; }
            catch { XYZMainForm.CustomCutterDimY = 1000000; }    // =  PCB eurocard format                                               


            if (XYZMainForm.TheScaledOutput < 11)
            {
                panelMaxEdit.Visible = false;
                panelMaxLabels.Visible = true;

                switch (XYZMainForm.TheScaledOutput)
                {
                    case 1:
                        labelMaxXmm.Text = "A1 landscape X 840 mm";
                        labelMaxYmm.Text = "A1 landscape Y 594 mm";
                        break;
                    case 2:
                        labelMaxXmm.Text = "A1 portrait X 594 mm";
                        labelMaxYmm.Text = "A1 portrait Y 840 mm";
                        break;
                    case 3:
                        labelMaxXmm.Text = "A2 landscape X 594 mm";
                        labelMaxYmm.Text = "A2 landscape Y 420 mm";
                        break;
                    case 4:
                        labelMaxXmm.Text = "A2 landscape X 420 mm";
                        labelMaxYmm.Text = "A2 landscape Y 594 mm";
                        break;
                    case 5:
                        labelMaxXmm.Text = "A3 landscape X 420 mm";
                        labelMaxYmm.Text = "A3 landscape Y 297 mm";
                        break;
                    case 6:
                        labelMaxXmm.Text = "A3 portrait X 297 mm";
                        labelMaxYmm.Text = "A3 portrait Y 420 mm";
                        break;
                    case 7:
                        labelMaxXmm.Text = "A4 landscape X 297 mm";
                        labelMaxYmm.Text = "A4 landscape Y 210 mm";
                        break;
                    case 8:
                        labelMaxXmm.Text = "A4 landscape X 210 mm";
                        labelMaxYmm.Text = "A4 landscape Y 297 mm";
                        break;
                    case 9:
                        labelMaxXmm.Text = "A5 landscape X 210 mm";
                        labelMaxYmm.Text = "A5 landscape Y 148 mm";
                        break;
                    case 10:
                        labelMaxXmm.Text = "A5 portrait X 148 mm";
                        labelMaxYmm.Text = "A5 portrait Y 210 mm";
                        break;
                    default: break;
                }
            }
            else if (XYZMainForm.TheScaledOutput == 11)
            {
                panelMaxLabels.Visible = false;
                panelMaxEdit.Visible = true;
            }
        }


        private void SetA1_Landscape_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 8400000;
            XYZMainForm.PaperDimY = 5940000;
            XYZMainForm.TheScaledOutput = 1;
            SetLabels(sender, e);
        }

        private void SetA1_Portrait_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 5940000;
            XYZMainForm.PaperDimY = 8400000;
            XYZMainForm.TheScaledOutput = 2;
            SetLabels(sender, e);
        }

        private void SetA2_Landscape_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 5940000;
            XYZMainForm.PaperDimY = 4200000;
            XYZMainForm.TheScaledOutput = 3;
            SetLabels(sender, e);
        }
        private void SetA2_Portrait_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 4200000;
            XYZMainForm.PaperDimY = 5940000;
            XYZMainForm.TheScaledOutput = 4;
            SetLabels(sender, e);
        }

        private void SetA3_Landscape_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 4200000;
            XYZMainForm.PaperDimY = 2970000;
            XYZMainForm.TheScaledOutput = 5;
            SetLabels(sender, e);
        }
        private void SetA3_Portrait_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 2970000;
            XYZMainForm.PaperDimY = 4200000;
            XYZMainForm.TheScaledOutput = 6;
            SetLabels(sender, e);
        }
        private void SetA4_Landscape_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 2970000;
            XYZMainForm.PaperDimY = 2100000;
            XYZMainForm.TheScaledOutput = 7;
            SetLabels(sender, e);
        }
        private void SetA4_Portrait_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 2100000;
            XYZMainForm.PaperDimY = 2970000;
            XYZMainForm.TheScaledOutput = 8;
            SetLabels(sender, e);
        }
        private void SetA5_Landscape_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 2100000;
            XYZMainForm.PaperDimY = 1480000;
            XYZMainForm.TheScaledOutput = 9;
            SetLabels(sender, e);
        }
        private void SetA5_Portrait_Click(object sender, EventArgs e)
        {
            XYZMainForm.PaperDimX = 1480000;
            XYZMainForm.PaperDimY = 2100000;
            XYZMainForm.TheScaledOutput = 10;
            SetLabels(sender, e);
        }

        private void SetCustom_Click(object sender, EventArgs e)
        {
            int DotPart = 0;
            // X calculation
            try { DotPart = int.Parse(textBoxEditDotX.Text); }
            catch { DotPart = 0; }
            try { XYZMainForm.CustomCutterDimX = int.Parse(textBoxEditX.Text) * 10000 + DotPart; }
            catch { XYZMainForm.CustomCutterDimX = 1600000; }    // =  PCB eurocard format              
                                                           // Y calculation
            try { DotPart = int.Parse(textBoxEditDotY.Text); }
            catch { DotPart = 0; }
            try { XYZMainForm.CustomCutterDimY = int.Parse(textBoxEditY.Text) * 10000 + DotPart; }
            catch { XYZMainForm.CustomCutterDimY = 1000000; }    // =  PCB eurocard format                                            

            XYZMainForm.PaperDimX = XYZMainForm.CustomCutterDimX;
            XYZMainForm.PaperDimY = XYZMainForm.CustomCutterDimY;
            XYZMainForm.TheScaledOutput = 11;
            SetLabels(sender, e);
        }



        private void XYAction(object sender, EventArgs e, int Direction, int StepsX, int StepsY) //
        {          
            int NewStepsX = 0, NewStepsY = 0;
            NewStepsX = StepsX;
            NewStepsY = StepsY;
            MyMove.NextCommand = true;
            MyMove.XY_Move(Direction, NewStepsX, NewStepsY);     //      
            while (MyMove.NextCommand == false) Application.DoEvents();
        }
           

        private void zAction(object sender, EventArgs e, int dir, int zSteps) //
        {         
            int NewzSteps = zSteps;
            XYZMainForm.FlagString = false;
            MyMove.zMove(dir, NewzSteps);
            while (XYZMainForm.FlagString == false) Application.DoEvents();

            if (dir == 0)  // downwards
                XYZMainForm.OpticZCoordinate = XYZMainForm.OpticZCoordinate + zSteps;
            else if (dir == 1) // upwards
                XYZMainForm.OpticZCoordinate = XYZMainForm.OpticZCoordinate - zSteps;

            XYZMainForm.SerialPortReturnS = "";
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
