
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace XYZ
{
    public partial class MillForm : Form
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


        public struct MillPoints
        {
            public int Xpoint;
            public int Ypoint;
            public int Mode;  // 1= draw line, Mill down, 0 =  Mill up while moving to this point             
            public bool milled; // true when drilled
        }

        public static int maxVectors = 100000;  //  is necessary for large pcb's eg feeder pcb top has 49500 lines
        public static int vectorIndex = 0;  // vectors in use
        
        public static MillPoints[] MyMillPoints = new MillPoints[maxVectors];
      
        public static float screenDivider = 1.0F;      // factor is used for drawing on the screen               
        public static float millOutputDivider = 1.0F;  // output factor is used for milling, depends  on PCB format

        private delegate void ObjectDelegate(object obj);
        public static Boolean millUp = true;
     
        public static int globalMaximumX, globalMaximumY,globalMinimumX, globalMinimumY;

        public static float xArrow = 0.0F; // secundary X displacement when arrow is used during pause
        public static float yArrow = 0.0F; // secundary Y displacement when arrow is used during pause

        XYZR MyXYZR = new XYZR();

        public static bool started = false;
        
        public MillForm()
        {
            InitializeComponent();
            MyXYZR.InitSerialPort();
                      
            this.Top = -1;
            this.Left = -1;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;    
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;          
                        
            for (int x = 0; x < maxVectors; x++)
            {
                MyMillPoints[x].Xpoint = 0;
                MyMillPoints[x].Ypoint = 0;
                MyMillPoints[x].Mode = 0;
                MyMillPoints[x].milled = false;         
            }
            
            Color buttonColor = Functions.kleur(XYZMainForm.IconColor);
            this.BackColor = Functions.kleur(XYZMainForm.FormColor);
            buttonMenu.BackColor = buttonColor;
            groupBoxRGB.BackColor = buttonColor;
            checkBoxBorderMill.BackColor = buttonColor;
            buttonOutput.BackColor = buttonColor;
            buttonStart.BackColor = buttonColor;
            buttonQuit.BackColor = buttonColor;
            checkBoxMirror.BackColor = buttonColor;
            checkBoxPause.BackColor = buttonColor;

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
                      
            checkBoxMirror.Left = progressBarLoad.Left + progressBarLoad.Width + btM;
            checkBoxMirror.Top = btM;   //
            checkBoxMirror.Width = btW;
            checkBoxMirror.Height = btH/2;

            checkBoxBorderMill.Left = checkBoxMirror.Left;
            checkBoxBorderMill.Top = btM + btH / 2;
            checkBoxBorderMill.Width = btW;
            checkBoxBorderMill.Height = btH / 2;
                       
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
            tableLayoutPanelLaser.Width = btW;
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

            tableLayoutPanelXYZPos.Left = tableLayoutPanel1.Left + (btW * 22) / 4 + btM;
            tableLayoutPanelXYZPos.Top = 0;
            tableLayoutPanelXYZPos.Height = btH + btM;
            tableLayoutPanelXYZPos.Width = btW; //21 /16

            buttonQuit.Left = tableLayoutPanelXYZPos.Left + btW + btM;
            buttonQuit.Top = btM;
            buttonQuit.Width = btW;
            buttonQuit.Height = btH;
                      
            listBoxGCODE.Left = btM;
            listBoxGCODE.Top = btM * 2 + btH;
            listBoxGCODE.Width = btW * 3;
            listBoxGCODE.Height = this.Height / 2;

            textBoxReceive.Left = btM;
            textBoxReceive.Top = listBoxGCODE.Top + this.Height / 2 + btM;
            textBoxReceive.Width = listBoxGCODE.Width;
            textBoxReceive.Height = this.Height / 3;
           
            pictureBox1.Top = listBoxGCODE.Top;
            pictureBox1.Left = listBoxGCODE.Left + listBoxGCODE.Width +btM*2;                   
            pictureBox1.Height = this.Height - (btH + btM * 12);
            pictureBox1.Width = this.Width - (listBoxGCODE.Width + btM * 8);

     
            pictureBox1.BackColor = Functions.kleur(XYZMainForm.FondColor);
            
            comboBoxXYTravel.Text = XYZMainForm.millXYTravelSpeed.ToString();
            comboBoxXYWork.Text = XYZMainForm.millXYWorkSpeed.ToString();

            comboBoxZup.Text = XYZMainForm.millZUpSpeed.ToString();
            comboBoxZdown.Text = XYZMainForm.millZDownSpeed.ToString();

            textBoxZupPosition.Text = XYZMainForm.millZUpPosition.ToString("##.####");
            textBoxZdownPosition.Text = XYZMainForm.millZDownPosition.ToString("##.####");

            buttonMenu.Text = XYZMainForm.StrMenu;
       //   checkBoxMirror.Text = XYZMainForm.StrMirror;

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
                //  panelMaxEdit.Visible = true;
                tableLayoutPanelDimXY.Visible = true;
            }
          
            labelXmm.Text = "X" + XYZMainForm.globalActualMmPosX.ToString("###.##") + "mm";  // ((float)XYZMainForm.globalActualMicrostepsX / 200).ToString("###.###");
            labelYmm.Text = "Y" + XYZMainForm.globalActualMmPosY.ToString("###.##") + "mm"; //  ((float)XYZMainForm.globalActualMicrostepsY / 200).ToString("###.###");
            labelZmm.Text = "Z" + XYZMainForm.globalActualMmPosZ.ToString("##.##") + "mm"; // ((float)XYZMainForm.globalActualMicrostepsZ / 200).ToString("###.###");
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            MyXYZR.SetLaser(1); // MAKE SURE LASER IS OFF

            XYZMainForm.CustomDimX = float.Parse(textBoxEditX.Text);
            XYZMainForm.CustomDimY = float.Parse(textBoxEditY.Text);

            // XY speed
            try { XYZMainForm.millXYTravelSpeed = int.Parse(comboBoxXYTravel.Text); }
            catch { XYZMainForm.millXYTravelSpeed = 25; }
            try { XYZMainForm.millXYWorkSpeed = int.Parse(comboBoxXYWork.Text); }
            catch { XYZMainForm.millXYWorkSpeed = 10; }

            // Z speed           
            XYZMainForm.millZUpSpeed = int.Parse(comboBoxZup.Text);
            XYZMainForm.millZDownSpeed = int.Parse(comboBoxZdown.Text);

            // Z position
            XYZMainForm.millZUpPosition = float.Parse(textBoxZupPosition.Text);
            XYZMainForm.millZDownPosition = float.Parse(textBoxZdownPosition.Text);
            
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
                
                ctx.Items.Add(XYZMainForm.StrOpenGCODEFile+" + ( wait somewhat! )", null, buttonOpenGCODE_Click); // "Open HPGL"              
                ctx.Items.Add("_________________________", null, null);

                ctx.Items.Add(XYZMainForm.StrClearAll, null, buttonClearAll_Click);  // "Clear all"
                ctx.Items.Add("_________________________", null, null);

                ctx.Items.Add(XYZMainForm.StrReset + " mill", null, buttonReset_Click); // "Reset plotter"
                ctx.Items.Add("_________________________", null, null);

                if (listBoxGCODE.Items.Count > 2)
                {
                    if (checkBoxLaser.Checked==false)
                    ctx.Items.Add(XYZMainForm.StrStartMill, null, buttonMill_Click);  // start mill
                    else
                    ctx.Items.Add("Start laser", null, buttonMill_Click);  // start mill

                    ctx.Items.Add("_________________________", null, null);
                }

                ctx.Items.Add(XYZMainForm.StrQuit, null, buttonQuit_Click);
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
            if (XYZMainForm.SerialPortReturnS.Contains("63"))
            delReceive.Invoke("Reset NOT ok "+XYZMainForm.SerialPortReturnS);
            else if (XYZMainForm.SerialPortReturnS.Contains("43"))
                delReceive.Invoke("Reset ok " + XYZMainForm.SerialPortReturnS);
        }
        
        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            int x;
            pictureBox1.Image = null;
            listBoxGCODE.Items.Clear();
            textBoxReceive.Clear();

            Graphics g = pictureBox1.CreateGraphics();           
            g.Clear(Functions.kleur(XYZMainForm.FondColor));

            for (x = 0; x < vectorIndex; x++)         
            {
                MyMillPoints[x].Xpoint = 0;
                MyMillPoints[x].Ypoint = 0;
                MyMillPoints[x].Mode = 0;
                MyMillPoints[x].milled = false;
            }
            g.Dispose();
            buttonStart.Visible = false;
        }
        
        private void buttonExecuteGCODE_Click(object sender, EventArgs e)
        {          
            int tempdelay = 10;         
            ObjectDelegate deltr = new ObjectDelegate(UpdateTextboxReceive);
            String S = "";

         // Calculation of output, paper, vinyl, wood, or PCB dimensions:

            CalculateOutputPlotDivider(sender, e); //  is once more necessary: after loading the file, the user may scale the output

            int x=0, direction = 0;
            float xVal = 0.0F, yVal = 0.0F, oldXval = 0.0F, oldYval = 0.0F;
            float checkValueX = 0, checkValueY = 0;

            int kleur = 255, thickness = 2;
            int Ydim = pictureBox1.Height;        //  -2 for border  ?                

            Graphics g = pictureBox1.CreateGraphics();
            //    g.Clear(Color.White);
            Pen p = new Pen(Color.Blue, 4);  //
            IntPtr hdc = g.GetHdc();
             
            if (millUp == false)  // pen is down
            {               
                MyXYZR.SetZSpeed(XYZMainForm.millZUpSpeed);
                MyXYZR.ZAction(sender, e, float.Parse(textBoxZupPosition.Text));
                DisplayAbsoluteZCoordinates(sender, e);
                millUp = true;
            }

            // set XY travel speed:                
            try { tempdelay = int.Parse(comboBoxXYTravel.Text); }
            catch { tempdelay = 25; }
            if (tempdelay < 2) tempdelay = 2;   // maxmost speed is then 250mm /sec
            if (tempdelay > 150) tempdelay = 150; // 0.05 mm.sec
            MyXYZR.SetXYSpeed(tempdelay);

            started = true;
            
            float millMargeX = XYZMainForm.GlobalMargeX;
            float millMargeY = XYZMainForm.GlobalMargeY;
            xVal = (float)MyMillPoints[1].Xpoint / millOutputDivider + xArrow+ millMargeX;
            yVal = (float)MyMillPoints[1].Ypoint / millOutputDivider + yArrow + millMargeY;

            checkValueX = XYZMainForm.MyCommonCoordinates.xTotalGround-millMargeX; ;
            checkValueY = XYZMainForm.MyCommonCoordinates.yTotalGround-millMargeY;
            
            //   OPTION DO NOT BORDER

            float minimumX = 1000000.0F;
            float minimumY = 1000000.0F;
            float maximumX = -1000000.0F;
            float maximumY = -1000000.0F;

            for (x = 0; x < vectorIndex; x++)       // WAS 1      0 resulted often in fake diagonal line           
            {
                if (minimumX > MyMillPoints[x].Xpoint) minimumX = MyMillPoints[x].Xpoint;
                if (minimumY > MyMillPoints[x].Ypoint) minimumY = MyMillPoints[x].Ypoint;

                if (maximumX < MyMillPoints[x].Xpoint) maximumX = MyMillPoints[x].Xpoint;
                if (maximumY < MyMillPoints[x].Ypoint) maximumY = MyMillPoints[x].Ypoint;
            }

            minimumX = minimumX / millOutputDivider;
            minimumY = minimumY / millOutputDivider;   
            maximumX = maximumX / millOutputDivider;
            maximumY = maximumY / millOutputDivider;

            float percentX = (maximumX - minimumX) / 100;  // 1 % from border will not be milled
            float percentY = (maximumY - minimumY) / 100;  // 1 % from border will not be milled

            int progressBarActualVal = 0;
            progressBarLoad.Value = 0;
            oldXval = XYZMainForm.globalActualMmPosX;
            oldYval = XYZMainForm.globalActualMmPosY;
            int passes = 1;
            float depthZ = 0.0F;
            if (checkBoxLaser.Checked) passes = int.Parse(comboBoxLaserPasses.Text);
            

         for (int y = 0; y < passes; y++)    // maxmost 10 passes
            {
                if ((y%3)==0) radioButtonR.Checked = true;
                else if ((y % 3) == 1) radioButtonG.Checked = true;
                else radioButtonB.Checked = true;                

                try
                { depthZ = float.Parse(textBoxDepthZ.Text); }
                catch
                { depthZ = 0.0F; }
                MyXYZR.SetZSpeed(XYZMainForm.millZDownSpeed); // down speed
                XYZMainForm.millZDownPosition = float.Parse(textBoxZdownPosition.Text)+depthZ*y;
                MyXYZR.ZAction(sender, e, XYZMainForm.millZDownPosition);
                DisplayAbsoluteZCoordinates(sender, e);
                                
                for (x = 1; x < vectorIndex; x++)       //   0 not ok x-1 is used           
                {
                    if ((x % 50) == 0)
                    {
                        progressBarActualVal = (y * progressBarLoad.Maximum) / passes
                                             + (x * progressBarLoad.Maximum) / (vectorIndex * passes);
                        if (progressBarActualVal < progressBarLoad.Maximum)
                            progressBarLoad.Value = progressBarActualVal;
                    }

                    if (x < (listBoxGCODE.Items.Count - 1)) listBoxGCODE.SelectedIndex = x;
                   
                    if (radioButtonR.Checked) kleur = 255;
                    if (radioButtonG.Checked) kleur = 255 << 8;
                    if (radioButtonB.Checked) kleur = 255 << 16;
                    
                    if (checkBoxPause.Checked == true)
                    {
                        Application.DoEvents();
                        x = x - 1;
                        continue;
                    }
                    xVal = (float)MyMillPoints[x].Xpoint / millOutputDivider + xArrow + millMargeX;
                    yVal = (float)MyMillPoints[x].Ypoint / millOutputDivider + yArrow + millMargeY;
                    
              //    BELOW IS PROTECTION AGAINS EXCESSIVE X AND Y movements:               

                    if (xVal > checkValueX) continue;
                    if (yVal > checkValueY) continue;
                    if (xVal < millMargeX) continue;
                    if (yVal < millMargeY) continue;

                    //   MAKE SURE THAT BORDERS ARE NOT MILLED WHEN CHECKBOX BORDER IS NOT CHECKED ! 


                    if (checkBoxBorderMill.Checked == false)
                    {
                        if (xVal <= (millMargeX + minimumX + percentX)) continue;
                        if (yVal <= (millMargeY + minimumY + percentY)) continue;
                        if (xVal >= (millMargeX + maximumX - percentX)) continue;
                        if (yVal >= (millMargeY + maximumY - percentY)) continue;
                    }

                 


                    S = "x " + xVal.ToString("") + " y " + yVal.ToString("");
                    deltr.Invoke(S);

                 if (MyMillPoints[x].Mode == 1)
                    {
                        IntPtr hObject = CreatePen(PenStyles.PS_SOLID, thickness, kleur);   
                        SelectObject(hdc, hObject);

                        MoveToEx(hdc, (int)(MyMillPoints[x - 1].Xpoint / screenDivider),
                               (int)(MyMillPoints[x - 1].Ypoint / screenDivider), IntPtr.Zero);

                        LineTo(hdc, (int)(MyMillPoints[x].Xpoint / screenDivider),
                              (int)(MyMillPoints[x].Ypoint / screenDivider));

                        DeleteObject(hObject);
                    }

                 //    1  =  up
                 //    0  =  down

                    if (MyMillPoints[x].Mode == 0)    // Z  up
                    {                   
                            if (checkBoxLaser.Checked == false)
                            {
                                MyXYZR.SetZSpeed(XYZMainForm.millZUpSpeed); // upwards

                                XYZMainForm.millZUpPosition = float.Parse(textBoxZupPosition.Text);
                                MyXYZR.ZAction(sender, e, XYZMainForm.millZUpPosition);
                                DisplayAbsoluteZCoordinates(sender, e);
                            }
                            else   // Turn laser off
                            {
                                MyXYZR.SetLaser(1);  // 1 = off !!
                            }

                            MyMillPoints[x].milled = false;
                            MyXYZR.SetLaser(1);  // 1 = off !!

                            // set XY travel speed:                                       
                            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYTravel.Text));                        
                            millUp = true;                    
                    }
               else if (MyMillPoints[x].Mode == 1)    // mill should become down
                    {  
                            if (checkBoxLaser.Checked == false)
                            {
                                MyXYZR.SetZSpeed(XYZMainForm.millZDownSpeed);    // down speed
                                XYZMainForm.millZDownPosition = float.Parse(textBoxZdownPosition.Text);
                                MyXYZR.ZAction(sender, e, XYZMainForm.millZDownPosition);
                                DisplayAbsoluteZCoordinates(sender, e);
                            }
                            else
                            {                           
                            if (x > 1) MyXYZR.SetLaser(0); // Turn laser ON
                            MyMillPoints[x].milled = true;
                        }

                        millUp = false;     // mill is down 
                        MyXYZR.SetXYSpeed(int.Parse(comboBoxXYWork.Text));    // set XY work speed:   
                     
                    }

                    direction = 0;

                    if ((xVal >= oldXval) && (yVal > oldYval)) direction = 1;       //   
                    else if ((xVal > oldXval) && (yVal <= oldYval)) direction = 3;  //  
                    else if ((xVal <= oldXval) && (yVal < oldYval)) direction = 5;   //                        
                    else if ((xVal < oldXval) && (yVal >= oldYval)) direction = 7;   //  

                 if ((MyMillPoints[x].Mode == 0) || (MyMillPoints[x].Mode == 1))
                    {
                        MyXYZR.XYAction(sender, e, direction, Math.Abs(xVal - oldXval), Math.Abs(yVal - oldYval), false);
                        DisplayXYCoordinates(sender, e);
                        oldXval = xVal;
                        oldYval = yVal;
                    }
                }           //  end for x                          
            }              // end for y laserpasses
         
            g.ReleaseHdc(hdc);
            g.Dispose();
            
            if (checkBoxLaser.Checked == false)
            {            
                MyXYZR.SetZSpeed(XYZMainForm.millZUpSpeed);               
                MyXYZR.ZAction(sender, e, XYZMainForm.millZUpPosition);
                DisplayAbsoluteZCoordinates(sender, e);
            }
            else   
            {          
                MyXYZR.SetLaser(1); // Turn laser off         
            }

            MyXYZR.SetLaser(1);  // 1 = off !!

            millUp = true;
            
            // back to Origin at travel speed:        
            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYTravel.Text));
            // controls visible
            tableLayoutPanel1.Visible = true;
            // back to Origin                     
            MyXYZR.XYAction(sender, e, 5, XYZMainForm.globalActualMmPosX, XYZMainForm.globalActualMmPosY, false);
            DisplayXYCoordinates(sender, e);      
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
            try
            {
                textBoxReceive.AppendText(S + "\r\n");
            }
            catch
            {

            }
        }
                
   private void buttonOpenGCODE_Click(object sender, EventArgs e)
        {
            String S = "";
            String sFile = "";
                  
            openFileDialog1.Filter = "Gcode files(*.gcode)|*.gcode|GCODE files (*.GCODE)|*.GCODE";
            openFileDialog1.FilterIndex = 0;         // 
            openFileDialog1.FileName = "*.gcode";    
            openFileDialog1.RestoreDirectory = true;

            ObjectDelegate del = new ObjectDelegate(UpdateListbox);
         
            listBoxGCODE.Items.Clear();          
            progressBarLoad.Value = 0;
            vectorIndex = 0;
       
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
                            listBoxGCODE.Items.Add(S); 
                        }    //    EOF
                    }        //    end of stream
           
                    buttonCommonPart_Click(sender, e);
                    progressBarLoad.Value = progressBarLoad.Maximum;
                    buttonStart.Visible = true;                 
                }         // file exists
            }           // openfiledialog ok                
        }
          

        private void buttonCommonPart_Click(object sender, EventArgs e)
        {
            String S = "";       
            int totaal = 0;
            ObjectDelegate del = new ObjectDelegate(UpdateListbox);
     
            totaal =listBoxGCODE.Items.Count - 1;
            if (totaal == 0) totaal = 1;
            totaal = (int)(totaal / 48);
            if (totaal == 0) totaal = 1;
            
            // All X,Y commands are now properly in the listboxHPGL:

            int t = 0;
            vectorIndex = 0;

            for (t = 0; t < maxVectors; t++)
            {
                MyMillPoints[t].Xpoint = 0;
                MyMillPoints[t].Ypoint = 0;
                MyMillPoints[t].Mode = 0; // makes sure the previous file is not drawn.  
                MyMillPoints[t].milled = false;               
            }

            t = 0;
            totaal = listBoxGCODE.Items.Count - 1;
            if (totaal == 0) totaal = 1;
            totaal = (int)(totaal / 48);
            if (totaal == 0) totaal = 1;

            while (t < listBoxGCODE.Items.Count)
            {
                listBoxGCODE.SelectedIndex = t;
                S = listBoxGCODE.Items[t].ToString();

                if (S.Contains("Z")) { t++; continue; }
                               
                if ((t % totaal) == 0) progressBarLoad.Value++;
                if (progressBarLoad.Value > 45) progressBarLoad.Value = 45;
                             
                if ((S.Length > 5) && (S.Contains("G0")))
                {
                    MyMillPoints[vectorIndex].Xpoint = GetArrayValues(S, 1); // 1 = X
                    MyMillPoints[vectorIndex].Ypoint = GetArrayValues(S, 2); // 2 = Y ->Y +1 is necessary to draw top border??? !
                    MyMillPoints[vectorIndex].Mode = 1;      // original only 1 = pd and 0 = pu are supported

                     if (S.Contains("G01")) MyMillPoints[vectorIndex].Mode = 1;                                        
                    if (S.Contains("G00")) MyMillPoints[vectorIndex].Mode = 0;                                       
                
                    vectorIndex++;
                }
                t++;
            }
                       
            int minimumX = 999999999, minimumY = 999999999;

            for (t = 0; t < vectorIndex-1; t++)    // -1 is necessary for KiCad !
            {
                if (minimumX > MyMillPoints[t].Xpoint) minimumX = MyMillPoints[t].Xpoint;
                if (minimumY > MyMillPoints[t].Ypoint) minimumY = MyMillPoints[t].Ypoint;
            }
            for (t = 0; t < vectorIndex-1; t++)
            {
                MyMillPoints[t].Xpoint = MyMillPoints[t].Xpoint - minimumX;
                MyMillPoints[t].Ypoint = MyMillPoints[t].Ypoint - minimumY;
            }

            int maximumX = -999999999, maximumY = -999999999;
            for (t = 0; t < vectorIndex-1; t++)    // 20000
            {
                if (maximumX < MyMillPoints[t].Xpoint) maximumX = MyMillPoints[t].Xpoint;
                if (maximumY < MyMillPoints[t].Ypoint) maximumY = MyMillPoints[t].Ypoint;
            }
            
            if (checkBoxMirror.Checked)
                for (t = 0; t < vectorIndex; t++)
                    MyMillPoints[t].Ypoint = maximumY - MyMillPoints[t].Ypoint+1; // +1 is very important            

            // 8 9 2020 is once more necessary for kicad:
                        
            minimumX = 999999999; ; minimumY = 999999999;
            maximumX = -999999999; maximumY = -999999999;

            for (t = 0; t < vectorIndex-1; t++)    // 20000
            {
                if (minimumX > MyMillPoints[t].Xpoint) minimumX = MyMillPoints[t].Xpoint;
                if (minimumY > MyMillPoints[t].Ypoint) minimumY = MyMillPoints[t].Ypoint;
            }

            for (t = 0; t < vectorIndex-1; t++)    // 20000
            {
                if (maximumX < MyMillPoints[t].Xpoint) maximumX = MyMillPoints[t].Xpoint;
                if (maximumY < MyMillPoints[t].Ypoint) maximumY = MyMillPoints[t].Ypoint;
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

            DividerX = ((float)(maximumX - minimumX)) / pictureBox1.Width;  //  (int)
            DividerY = ((float)(maximumY - minimumY)) / pictureBox1.Height;
            screenDivider = DividerX;
            if (DividerY > DividerX) screenDivider = DividerY;
            if (screenDivider == 0) screenDivider = 1;

            progressBarLoad.Value = progressBarLoad.Maximum;                       

            CalculateOutputPlotDivider(sender, e);
            progressBarLoad.Value = progressBarLoad.Maximum;
            DrawArrayXY(sender, e);
       }
        

        private void CalculateOutputPlotDivider(object sender, EventArgs e)   // Calculates output scale for display on screen and for plotting on paper or vinyl
        {
            float dividerX = 1, dividerY = 1;
            
            if (XYZMainForm.TheScaledOutput == 11)
            {              
                XYZMainForm.PaperDimX = float.Parse(textBoxEditX.Text);
                XYZMainForm.PaperDimY = float.Parse(textBoxEditY.Text);
            }
            dividerX = ((float)(globalMaximumX - globalMinimumX) * 1) / (float)XYZMainForm.PaperDimX;   // 
            dividerY = ((float)(globalMaximumY - globalMinimumY) * 1) / (float)XYZMainForm.PaperDimY;   //
            millOutputDivider = dividerX;
            if (dividerY > dividerX) millOutputDivider = dividerY;
            if (millOutputDivider == 0)  millOutputDivider = 1;
            //  MessageBox.Show(TheOutputPlotDivider.ToString("###.###"));   
        }


        private void DrawArrayXY(object sender, EventArgs e)
        {
            int x;
            int Ydim = pictureBox1.Height + 1;    //     for mirrored images, without +1 top border is not drawn

            int kleur = 0;                       //      black
            int kleurDone = 0;  // black
            int thickness = 2;                   //      pen thickness

            Graphics g = pictureBox1.CreateGraphics();
            IntPtr hdc = g.GetHdc();

            IntPtr hObject = CreatePen(PenStyles.PS_SOLID, thickness, kleur);   //20 Color.FromArgb(Form1.PrintSignalColor));
            
            if (radioButtonR.Checked) kleurDone = 255;
            else if (radioButtonG.Checked) kleurDone = 255<<8;
            else if (radioButtonB.Checked) kleurDone = 255 << 16;
            IntPtr hObjectDone = CreatePen(PenStyles.PS_SOLID, thickness, kleurDone);   //20 Color.FromArgb(Form1.PrintSignalColor));
           
            for (x = 1; x <vectorIndex; x++)  //  1 resulted in fake diagonal line
            {            
                kleur = 255<<16; // red?                

                if (MyMillPoints[x].Mode == 1)
                {
                    if (MyMillPoints[x].milled == false)
                    {
                        SelectObject(hdc, hObject);
                        MoveToEx(hdc, (int)((float)MyMillPoints[x - 1].Xpoint / screenDivider),
                            (int)((float)MyMillPoints[x - 1].Ypoint / screenDivider), IntPtr.Zero);

                        LineTo(hdc, (int)((float)MyMillPoints[x].Xpoint / screenDivider),
                            (int)((float)MyMillPoints[x].Ypoint / screenDivider));
                        DeleteObject(hObject);
                    }   // mode == 1

                    else if (MyMillPoints[x].milled == true)
                    {
                        SelectObject(hdc, hObjectDone);
                        MoveToEx(hdc, (int)((float)MyMillPoints[x - 1].Xpoint / screenDivider),
                            (int)((float)MyMillPoints[x - 1].Ypoint / screenDivider), IntPtr.Zero);

                        LineTo(hdc, (int)((float)MyMillPoints[x].Xpoint / screenDivider),
                            (int)((float)MyMillPoints[x].Ypoint / screenDivider));
                        DeleteObject(hObjectDone);
                    }   // mode == 1
                }
                //   DeleteObject(hObject);
            } // for loop
            g.ReleaseHdc(hdc);
            g.Dispose();
        }

        private int GetArrayValues(string S, int mode)
        {            
            string sX= "", sY = "";
            int CoordX = 0, CoordY = 0;
            float fCoordX = 0.0F, fCoordY = 0.0F;

            int indexX = 0, indexY = 0;
            indexX = S.IndexOf("X");
            indexY = S.IndexOf("Y");
            
        //    MessageBox.Show(indexX.ToString() + " " + indexY.ToString());
                    
            try { sX = S.Substring(indexX+1,indexY -(indexX+1) );      }
            catch { return (0); }

            try { sY = S.Substring(indexY+1);      }
            catch { return (0); }
      
            try {fCoordX =  float.Parse(sX); }
            catch { return (0); }

            try { fCoordY =float.Parse(sY); }
            catch { return (0); }

            //    MessageBox.Show(fCoordX.ToString("###.####")+ "  "+ fCoordY.ToString("###.####"));

            fCoordX = fCoordX * 10000;
            fCoordY = fCoordY * 10000;
            CoordX = (int)fCoordX;
            CoordY = (int)fCoordY;

            if (mode == 1) return (CoordX);
            if (mode == 2) return (CoordY);
            return (0);
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
            listBoxGCODE.Items.Add(S + "\r\n");
        }
          

        private void buttonMill_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = false;
            buttonExecuteGCODE_Click(sender, e);
        }

        private void checkBoxPause_CheckStateChanged(object sender, EventArgs e)
        {           
            if (checkBoxPause.Checked)
            {
                tableLayoutPanel1.Visible = true;
                panelXY.Visible = true;
            }
            else
            {
                tableLayoutPanel1.Visible = false;
                panelXY.Visible = false;
                timer1.Enabled = true; // will execute drawArrayXY, refresh screen
            }
        }
        

        private void listBoxGCODE_MouseClick(object sender, MouseEventArgs e)
        {
            int x = listBoxGCODE.SelectedIndices[0];
            if (x == -1) return;
            Graphics g = pictureBox1.CreateGraphics();
            IntPtr hdc = g.GetHdc();

            int kleur = 255; // red            
            if (radioButtonR.Checked) kleur = 255;
            if (radioButtonG.Checked) kleur = 255 << 8;
            if (radioButtonB.Checked) kleur = 255 << 16;

            IntPtr hObject = CreatePen(PenStyles.PS_SOLID, 2, kleur);   //20 Color.FromArgb(Form1.PrintSignalColor));
            SelectObject(hdc, hObject);

            if (x > 2) x = x - 2; // 2 instruction on top are G21 and G90

            int xval = (int)(MyMillPoints[x].Xpoint / screenDivider);
            int yval = (int)(MyMillPoints[x].Ypoint / screenDivider);

            MoveToEx(hdc, xval - 4, yval - 4, IntPtr.Zero);
            LineTo(hdc, xval + 4, yval + 4);

            MoveToEx(hdc, xval - 4, yval + 4, IntPtr.Zero);
            LineTo(hdc, xval + 4, yval - 4);

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
            XYZMainForm.CustomDimX = float.Parse(textBoxEditX.Text);
            XYZMainForm.CustomDimY = float.Parse(textBoxEditY.Text);
            
            if (XYZMainForm.TheScaledOutput < 11)
            {
                // panelMaxEdit.Visible = false;
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

        private void SetA1_Landscape_Click(object sender, EventArgs e)
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

            if (tempdelay < 2) tempdelay = 2;   // maxmost speed is then 250mm /sec
            if (tempdelay > 150) tempdelay = 150; // 0.05 mm.sec
                       
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
                tempdelay = 100;
            }

            if (tempdelay < 2) tempdelay = 2;   // maxmost speed is then 250mm /sec
            if (tempdelay > 150) tempdelay = 150; // 0.05 mm.sec
            
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
                tempdelay =  int.Parse(comboBoxZup.Text);
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
                tempdelay = 100;
            }

            if (tempdelay < 2) tempdelay = 2;   // maxmost speed is then 250mm /sec
            if (tempdelay > 6000) tempdelay = 6000; // 0.05 mm.sec

            MyXYZR.SetZSpeed(tempdelay);
            delListbox.Invoke(XYZMainForm.SerialPortSendS);
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
        }

        private void buttonSetZupPosition_Click(object sender, EventArgs e)
        {            
            MyXYZR.ZAction(sender, e, float.Parse(textBoxZupPosition.Text));
            millUp = true;
        }

        private void buttonSetZdownPosition_Click(object sender, EventArgs e)
        {          
            MyXYZR.ZAction(sender, e, float.Parse(textBoxZdownPosition.Text));
            millUp = false;
        }

        private void textBoxZdownPosition_Leave(object sender, EventArgs e)
        {
            float temp = 10.123F;
            try { temp = float.Parse(textBoxZdownPosition.Text); }
            catch { temp = 10.123F; }         
            XYZMainForm.millZDownPosition = temp;
        }

        private void textBoxZupPosition_Leave(object sender, EventArgs e)
        {
            float temp = 2.123F;
            try { temp = float.Parse(textBoxZupPosition.Text); }
            catch { temp = 2.123F; }         
            XYZMainForm.millZUpPosition = temp;
        }

        private void comboBoxZdown_Leave(object sender, EventArgs e)
        {
            int temp = 10;
            try { temp = int.Parse(comboBoxZdown.Text); }
            catch { temp = 10; }
            XYZMainForm.millZDownSpeed = temp;
        }

        private void comboBoxZup_Leave(object sender, EventArgs e)
        {            
            int temp = 10;
            try { temp = int.Parse(comboBoxZup.Text); }
            catch { temp = 10; }           
            XYZMainForm.millZUpSpeed = temp;
        }

        private void comboBoxXYTravel_Leave(object sender, EventArgs e)
        {
            int temp = 50;
            try { temp = int.Parse(comboBoxXYTravel.Text); }
            catch { temp = 50; }
            XYZMainForm.millXYTravelSpeed = temp;
        }
             

        private void checkBoxLaser_CheckedChanged(object sender, EventArgs e)
        {
            textBoxZupPosition.Visible = true;
            buttonSetZupPosition.Visible = true;

            buttonPlusZ.Visible = false;
            buttonMinZ.Visible = false;

            labelZmm.Visible = true;
            label6.Visible = true;
            comboBoxZup.Visible = true;
            comboBoxZdown.Visible = true;
          
      
            buttonSetZupSpeed.Visible = true;
            buttonSetZdownSpeed.Visible = true;
            buttonStart.ForeColor = Color.Blue;
            buttonStart.Text = "Start Mill";
            
            if (checkBoxLaser.Checked)
            {
                buttonPlusZ.Visible = true;
                buttonMinZ.Visible = true;
                textBoxZupPosition.Visible = false;
                buttonSetZupPosition.Visible =false;
                labelZmm.Visible = false;
                label6.Visible = false;
               comboBoxZup.Visible = false;
                comboBoxZdown.Visible = false;
                buttonSetZupSpeed.Visible = false;
                buttonSetZdownSpeed.Visible = false;

                buttonStart.ForeColor = Color.Red;
                buttonStart.Text = "Start Laser";
            }
        }

        private void buttonPlusZ_Click(object sender, EventArgs e)
        {
            float temp = 0.0F;

            try
            {
                temp = float.Parse(textBoxZdownPosition.Text);
            }
            catch
            {
                temp = 0.0F;
            }
         //   temp = temp + 0.010F;
            textBoxZdownPosition.Text = temp.ToString("##.###");         

        }

        private void buttonMinZ_Click(object sender, EventArgs e)
        {
            float temp = 0.0F;
            try
            {
                temp = float.Parse(textBoxZdownPosition.Text);
            }
            catch
            {
                temp = 0.0F;
            }
        //    temp = temp - 0.005F; // remark is 0.005 mm different
            textBoxZdownPosition.Text = temp.ToString("##.###");
        }

        private void comboBoxXYWork_Leave(object sender, EventArgs e)
        {
            int temp = 50;
            try { temp = int.Parse(comboBoxXYWork.Text); }
            catch { temp = 50; }
            XYZMainForm.millXYWorkSpeed = temp;
        }

        private void DisplayXYCoordinates(object sender, EventArgs e)
        {           
            labelXmm.Text = "X" + XYZMainForm.globalActualMmPosX.ToString("###.##") + "mm";
            labelYmm.Text = "Y" + XYZMainForm.globalActualMmPosY.ToString("###.##") + "mm";
            labelZmm.Text = "Z" + XYZMainForm.globalActualMmPosZ.ToString("##.##") + "mm"; 
            
        }

        // zSteps below is absolute

        private void DisplayAbsoluteZCoordinates(object sender, EventArgs e)   // in zSteps
        {
            labelZmm.Text = XYZMainForm.globalActualMmPosZ.ToString("###.###");
            if (labelZmm.Text == "") labelZmm.Text = "0.0";     // otherwise nothing is displayed on 0 
        }
        
        private void buttonR_Click(object sender, EventArgs e) // R = right
        {
            float steps = float.Parse(textBoxXY.Text);
            serialPortCommandsXY(sender, e, 1, steps, 0, false);
            xArrow = xArrow + steps;
        }

       private void serialPortCommandsXY(object sender, EventArgs e, int direction,
         float stepsX, float stepsY, bool limit)
        {
            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextboxReceive);
            XYZMainForm.SerialPortReturnS = "";
            XYZMainForm.SerialPortSendS = "";

            MyXYZR.XYAction(sender, e, direction, stepsX, stepsY, limit);
            DisplayXYCoordinates(sender, e);

            delListbox.Invoke(XYZMainForm.SerialPortSendS);

            while (MyXYZR.NextCommand == false) Application.DoEvents();
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);

        }

        private void buttonD_Click(object sender, EventArgs e) // D=down
        {
            float steps = float.Parse(textBoxXY.Text);
            serialPortCommandsXY(sender, e, 1, 0, steps, false);
            yArrow = yArrow + steps;
        }

        private void buttonL_Click(object sender, EventArgs e) // L = left
        {
            float steps = float.Parse(textBoxXY.Text);
            serialPortCommandsXY(sender, e, 5, steps, 0, false);
            xArrow = xArrow - steps;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            DrawArrayXY(sender, e);
            timer1.Enabled = false;
        }

        private void buttonU_Click(object sender, EventArgs e) // U = up
        {
            float steps = float.Parse(textBoxXY.Text);
            serialPortCommandsXY(sender, e, 5, 0, steps, false);
            yArrow = yArrow - steps;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawArrayXY(sender, e);
            timer1.Enabled = false;
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
