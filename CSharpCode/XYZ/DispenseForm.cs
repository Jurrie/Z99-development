
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace XYZ
{
    public partial class DispenseForm : Form
    {       
        public struct DispensePads
        {
            public int Xpoint;
            public int Ypoint;
            public int dotForm; // DotCount;      //   1, 2H, 2V, 3H, 3V  or 4 dots for a pad
            public float dotDistance;   //  distance in 0.x millimeter
            public int DispenseColor; // = 255 + 255 << 16 + 255 << 8; // display color 
            public int dotExtrusion;  // length of paste extrusion
            public int IntPadName;    //   80,81, 104 ,  105 etc 
            public string PadName;    // D104*   D105*  etc    
            public bool dispensed;                              
        }
        
        // Types of different pads:

        public struct ListOfDispensePads
        {      
            public int dotForm;        //   1, 2H, 2V, 3H, 3V  or 4 dots for a pad
            public float dotDistance;  //  WAS INT distance in micromillimeter
            public int DispenseColor;  //  = 255 + 255 << 16 + 255 << 8; // display color 
            public int dotExtrusion;   //  length of past extrusion 1..10
            public int IntPadName;     //  104 ,  105 etc 
            public string PadName;     //  D104*   D105*  etc                      
        }
        
       public static int selectedDotCount = 1; // 1 = default, 2 = 2 horizontal dots, 3 = 2  vertical dot counts,
                                               // 4 = 3 horizontal dots, 5 = 3 vertical dot counts, 6 = 4 dots
                    
        public static int maxPads = 5000;  //  5000   is a huge pcb             
        public static int padCount = 0;    // count for pads in use        
        
        public static DispensePads[] myDispensePads = new DispensePads[maxPads];

        public static ListOfDispensePads[] myPadList = new ListOfDispensePads[100]; // up to 100 different pad types
              
        public static float screenDivider = 1.0F;      // factor is used for drawing on the screen
  
        private delegate void ObjectDelegate(object obj);

        public static Boolean dispenserUp = true;  
        public static int containsInch = 0;    // 1= input is in inch  0 input is in mm
        public static int decimalsPrecision;
        public static int oldX = 0, oldY = 0;    
        public static int globalMinimumX, globalMinimumY;
        public static bool mirrored=false;
        public static float xArrow = 0.0F; // secundary X displacement when arrow is used during pause
        public static float yArrow = 0.0F; // secundary Y displacement when arrow is used during pause

        XYZR MyXYZR = new XYZR();
                
        public DispenseForm()
        {
            InitializeComponent();            
            MyXYZR.InitSerialPort();                  
                        
            this.Top = -1;
            this.Left = -1;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;   
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;                        

            Color ButtonColor = Functions.kleur(XYZMainForm.IconColor);
            this.BackColor = Functions.kleur(XYZMainForm.FormColor);
            buttonMenu.BackColor = ButtonColor;

            checkBoxMirror.BackColor = ButtonColor;           
            buttonStartDispense.BackColor = ButtonColor;
            buttonQuit.BackColor = ButtonColor;
            checkBoxPause.BackColor = ButtonColor;

            pictureBox1.BackColor = Functions.kleur(XYZMainForm.FondColor);

            int btW = this.Width / 16; //15
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
            
            checkBoxMirror.Left = progressBarLoad.Left + progressBarLoad.Width + btM;
            checkBoxMirror.Top = btM;   //
            checkBoxMirror.Width = btW;
            checkBoxMirror.Height = btH;
           
            buttonStartDispense.Left = checkBoxMirror.Left + btW * 1 + btM;
            buttonStartDispense.Top = btM;
            buttonStartDispense.Width = btW;
            buttonStartDispense.Height = btH;

            checkBoxPause.Left = buttonStartDispense.Left + btW + btM;
            checkBoxPause.Top = btM;
            checkBoxPause.Width = btW;
            checkBoxPause.Height = btH;

            tableLayoutPanel1.Left = checkBoxPause.Left + btW + btM;
            tableLayoutPanel1.Top = 0;
            tableLayoutPanel1.Height = btH + btM;
            tableLayoutPanel1.Width = btW * 4;

            tableLayoutPanelPositions.Left = tableLayoutPanel1.Left + btW * 4 + btM;
            tableLayoutPanelPositions.Top = 0;
            tableLayoutPanelPositions.Height = btH + btM;
            tableLayoutPanelPositions.Width = btW * 12 / 8;
            
            tableLayoutPanelSetup.Left = tableLayoutPanelPositions.Left + tableLayoutPanelPositions.Width + btM*3;
            tableLayoutPanelSetup.Top = 0;
            tableLayoutPanelSetup.Height = btH + btM;
            tableLayoutPanelSetup.Width = btW * 3;
            
            buttonQuit.Left = tableLayoutPanelSetup.Left + btW*3 + btM*3 ;
            buttonQuit.Top = btM;
            buttonQuit.Width = btW;
            buttonQuit.Height = btH;                   
            
            labelPadList.Left = btM;
            labelPadList.Top = buttonMenu.Top + btH + btM;  // 100;

            listBoxPads.Left = labelPadList.Left;
            listBoxPads.Top = labelPadList.Top + labelPadList.Height + btM;
            listBoxPads.Width = this.Width / 5;
            listBoxPads.Height = this.Height / 5;

            labelPadProperties.Left = labelPadList.Left;
            labelPadProperties.Top = listBoxPads.Top + listBoxPads.Height + btM;

            panelDot.Left = labelPadList.Left;
            panelDot.Top = labelPadProperties.Top + labelPadProperties.Height + 4;
            panelDot.Width = listBoxPads.Width;

            labelFileContent.Left = labelPadList.Left;
            labelFileContent.Top = panelDot.Top + panelDot.Height + btM;

            listBoxFileContent.Left = labelPadList.Left;
            listBoxFileContent.Top = labelFileContent.Top + labelFileContent.Height + btM;
            listBoxFileContent.Width = listBoxPads.Width;
            listBoxFileContent.Height = (this.Height * 9) / 40;

            pictureBox1.Top = labelPadList.Top;
            pictureBox1.Left = listBoxFileContent.Left + listBoxFileContent.Width + btM;
            pictureBox1.Width = this.Width - (listBoxFileContent.Width + 36);
            pictureBox1.Height = this.Height - (pictureBox1.Top + 54);
            
            for (int x = 0; x < maxPads; x++)
            {
                myDispensePads[x].PadName = "";
                myDispensePads[x].IntPadName = 0;
                myDispensePads[x].Xpoint = 0;
                myDispensePads[x].Ypoint = 0;
                myDispensePads[x].DispenseColor = 0;
                myDispensePads[x].dotForm = 1; // default
                myDispensePads[x].dotDistance = 0;
                myDispensePads[x].dotExtrusion = 0;
                myDispensePads[x].dispensed = false;
            }
            
            buttonStartDispense.BackColor = ButtonColor;
            buttonQuit.BackColor = ButtonColor;
            checkBoxMirror.BackColor = ButtonColor;
            checkBoxPause.BackColor = ButtonColor;

            pictureBox1.BackColor = Functions.kleur(XYZMainForm.FondColor);
            
            comboBoxXYTravel.Text = XYZMainForm.dispXYTravelSpeed.ToString();
            comboBoxXYWork.Text = XYZMainForm.dispXYWorkSpeed.ToString();

            comboBoxZup.Text = XYZMainForm.dispZUpSpeed.ToString();
            comboBoxZdown.Text = XYZMainForm.dispZDownSpeed.ToString();

            textBoxZupPosition.Text = XYZMainForm.dispZUpPosition.ToString("##.####");
            textBoxZdownPosition.Text = XYZMainForm.dispZDownPosition.ToString("##.####");

            buttonMenu.Text = XYZMainForm.StrMenu;
            checkBoxMirror.Text = XYZMainForm.StrMirror;

            buttonStartDispense.Text = XYZMainForm.StrStartDispense;

            buttonQuit.Text = XYZMainForm.StrQuit;
            checkBoxPause.Text = XYZMainForm.StrPause;
          
            labelXmm.Text = XYZMainForm.globalActualMmPosX.ToString("###.###");
            labelYmm.Text = XYZMainForm.globalActualMmPosY.ToString("###.###");
            labelZmm.Text = XYZMainForm.globalActualMmPosZ.ToString("###.###");
                        
            if (labelXmm.Text == "") labelXmm.Text = "0.0"; // otherwise nothing is displayed on 0
            if (labelYmm.Text == "") labelYmm.Text = "0.0"; // otherwise nothing is displayed on 0
            if (labelZmm.Text == "") labelZmm.Text = "0.0"; // otherwise nothing is displayed on 0
                       
            labelPadList.Text = XYZMainForm.StrSelectPad;
            labelPadProperties.Text = XYZMainForm.StrSetProperties;

            labelDotDistance.Text = XYZMainForm.StrDotDistance;
            labelDotExtrusion.Text = XYZMainForm.StrDotExtrusion;

            buttonApply.Text = XYZMainForm.StrSet;
            labelFileContent.Text = XYZMainForm.StrFileContent;                        
        }
        
        private void buttonQuit_Click(object sender, EventArgs e)
        {            
          // XY speed
            try { XYZMainForm.dispXYTravelSpeed = int.Parse(comboBoxXYTravel.Text); }
            catch { XYZMainForm.dispXYTravelSpeed = 25; }
            try { XYZMainForm.dispXYWorkSpeed = int.Parse(comboBoxXYWork.Text); }
            catch { XYZMainForm.dispXYWorkSpeed = 10; }

            // Z speed           
            try { XYZMainForm.dispZUpSpeed = int.Parse(comboBoxZup.Text); }
            catch { XYZMainForm.dispZUpSpeed = 15; } 
                  
            try { XYZMainForm.dispZDownSpeed = int.Parse(comboBoxZdown.Text); }
            catch { XYZMainForm.dispZDownSpeed = 5; } 

            // Z position           
            try { XYZMainForm.dispZUpPosition = float.Parse(textBoxZupPosition.Text); }
            catch { XYZMainForm.dispZUpPosition = 5.0F; }
            
            try { XYZMainForm.dispZDownPosition = float.Parse(textBoxZdownPosition.Text); }
            catch { XYZMainForm.dispZDownPosition = 10.0F; }
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
                                
                ctx.Items.Add("Open gerber PASTE file", null, buttonOpenGerberPad_Click);
                // "Open *.gbr file "  file with D03instructions , = component pad file, paste file 
                ctx.Items.Add("_________________________", null, null);
                
                ctx.Items.Add("Open dispense (*.disp) file", null, buttonOpenDispenseFile_Click);
                ctx.Items.Add("Save as dispense (*.disp) file", null, buttonSaveDispenseFile_Click);
                
                // "Open *.gbr file "  file with D03instructions , = component pad file, paste file 
                ctx.Items.Add("_________________________", null, null);                
                ctx.Items.Add(XYZMainForm.StrReset + " dispenser", null, buttonReset_Click); // "Reset drill"           
                ctx.Items.Add(XYZMainForm.StrClearAll,null,buttonClearAll_Click);
                ctx.Items.Add("_________________________", null, null);
                ctx.Items.Add("Make array of 7 dots spaced 1.27mm", null, buttonMakeArrayOfDots);
                ctx.Items.Add("_________________________", null, null);
                
                if (listBoxFileContent.Items.Count > 2)
                {
                    ctx.Items.Add(XYZMainForm.StrStartDispense, null, buttonDispense_Click);  //  "Start dispenser"
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
            listBoxFileContent.Items.Clear();
            listBoxPads.Items.Clear();

            textBoxReceive.Clear();

            Graphics g = pictureBox1.CreateGraphics();
            Pen p = new Pen(Color.Green, 2);
          
            g.Clear(Functions.kleur(XYZMainForm.FondColor));
            
            for (x = 0; x < padCount; x++)          // 20000 ?
            {
                myDispensePads[x].PadName = ""; // D104* , D105* etc
                myDispensePads[x].IntPadName = 0;     //  104, 105 etc
                myDispensePads[x].Xpoint = 0;
                myDispensePads[x].Ypoint = 0;

                myDispensePads[x].dotForm = 1; // default
                myDispensePads[x].dotDistance = 0;
                myDispensePads[x].dotExtrusion = 0;
                myDispensePads[x].dispensed = false;                              
            }
            g.Dispose();
            buttonStartDispense.Visible = false;
        }


        private void buttonExecuteDispense_Click(object sender, EventArgs e)
        {          
            float outputDivider = 1.0F;   
        
            outputDivider = (float)(Math.Pow((double)10, (double)(decimalsPrecision)));        

            int x, direction = 1;
            float xVal = 0.0F, yVal = 0.0F, oldXval = 0.0F, oldYval = 0.0F;

            int Ydim = pictureBox1.Height - 2;           //     -2 for border

            Graphics g = pictureBox1.CreateGraphics();
    
            Pen p = new Pen(Color.Red,1);

            dispenserUp = true;            // starts with the pen up
                       
            float stepFactor = 1.0F;
            if (containsInch == 1) stepFactor = (stepFactor * 254) / 100;
            
           float margeX = XYZMainForm.GlobalMargeX;
           float margeY = XYZMainForm.GlobalMargeY;
              
      float checkValueX = XYZMainForm.MyCommonCoordinates.xTotalGround-margeX;  // is in .mm
      float checkValueY = XYZMainForm.MyCommonCoordinates.yTotalGround-margeY;
                                 
            if (dispenserUp == false)  // pen is down
            {               
                MyXYZR.SetZSpeed(XYZMainForm.dispZUpSpeed);  // upwards
                MyXYZR.ZAction(sender, e, XYZMainForm.dispZUpPosition);

                DisplayXYCoordinates(sender, e);
                DisplayAbsoluteZCoordinates(sender, e);

                dispenserUp = true;
            }

        // set XY travel speed:                
            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYTravel.Text));                                  

            Rectangle rect = new Rectangle(0,0,4,4);

            oldXval = XYZMainForm.globalActualMmPosX;
            oldYval = XYZMainForm.globalActualMmPosY;

            progressBarLoad.Maximum = 100;

            progressBarLoad.Value = 1;

         //  determine total padcount
         
            int totalPadCount = 1;
            for (x = 0; x <5000; x++)
            {
                totalPadCount++; ///  = myDispensePads[x].dotForm;                
                if ( (myDispensePads[x].Xpoint == 0) && (myDispensePads[x].Ypoint==0)) break;     
            }

            oldXval = XYZMainForm.globalActualMmPosX;
            oldYval = XYZMainForm.globalActualMmPosY;
            
            
            for (x = 0; x <totalPadCount-1; x++)          
            {
                
                if ((x % 10) == 0)
                {                   
                    progressBarLoad.Value = (progressBarLoad.Maximum * x) /totalPadCount;                   
                    progressBarLoad.Update();
                }                

                if (checkBoxPause.Checked == true)
                {
                    Application.DoEvents();
                    if(x>0) x = x - 1;                    
                    continue;
                }
                
           switch (myDispensePads[x].dotForm)
                {
                    case 1:   // one dot
                rect = new Rectangle((int)(myDispensePads[x].Xpoint / screenDivider)-2 ,
                                               (int)(myDispensePads[x].Ypoint / screenDivider)-2 , 4, 4);                       
                g.DrawRectangle(p, rect);
                        break;

                    case 2:   // 2 horizontal dots
                        rect = new Rectangle((int)(myDispensePads[x].Xpoint / screenDivider) - 4,
                              (int)(myDispensePads[x].Ypoint / screenDivider) - 2, 4, 4);
                        g.DrawRectangle(p, rect);
                        rect = new Rectangle((int)(myDispensePads[x].Xpoint / screenDivider),
                             (int)(myDispensePads[x].Ypoint / screenDivider)-2, 4, 4);
                        g.DrawRectangle(p, rect);
                        break;

                    case 3:   // 2 vertical dots
                        rect = new Rectangle((int)(myDispensePads[x].Xpoint / screenDivider) -2,
                              (int)(myDispensePads[x].Ypoint / screenDivider) - 4, 4, 4);
                        g.DrawRectangle(p, rect);
                        rect = new Rectangle((int)(myDispensePads[x].Xpoint / screenDivider)-2,
                             (int)(myDispensePads[x].Ypoint / screenDivider), 4, 4);
                        g.DrawRectangle(p, rect);
                        break;
                    case 4:   // 3 horizontal dots
                        rect = new Rectangle((int)(myDispensePads[x].Xpoint / screenDivider) - 6,
                              (int)(myDispensePads[x].Ypoint / screenDivider) - 2, 4, 4);
                        g.DrawRectangle(p, rect);
                        rect = new Rectangle((int)(myDispensePads[x].Xpoint / screenDivider-2),
                             (int)(myDispensePads[x].Ypoint / screenDivider)-2, 4, 4);
                        g.DrawRectangle(p, rect);
                        rect = new Rectangle((int)(myDispensePads[x].Xpoint / screenDivider) + 2,
                             (int)(myDispensePads[x].Ypoint / screenDivider) - 2, 4, 4);
                        g.DrawRectangle(p, rect);

                        break;
                    case 5:   // 3 vertical dots
                        rect = new Rectangle((int)(myDispensePads[x].Xpoint / screenDivider) - 2,
                              (int)(myDispensePads[x].Ypoint / screenDivider) - 6, 4, 4);
                        g.DrawRectangle(p, rect);
                        rect = new Rectangle((int)(myDispensePads[x].Xpoint / screenDivider) - 2,
                             (int)(myDispensePads[x].Ypoint / screenDivider), 4, 4);
                        g.DrawRectangle(p, rect);

                        rect = new Rectangle((int)(myDispensePads[x].Xpoint / screenDivider) + 2,
                            (int)(myDispensePads[x].Ypoint / screenDivider)+4, 4, 4);
                        g.DrawRectangle(p, rect);
                        break;
                    case 6:   // 4 dots
                        rect = new Rectangle((int)(myDispensePads[x].Xpoint / screenDivider) - 4,
                              (int)(myDispensePads[x].Ypoint / screenDivider) - 4, 4, 4);
                        g.DrawRectangle(p, rect);
                        rect = new Rectangle((int)(myDispensePads[x].Xpoint / screenDivider),
                             (int)(myDispensePads[x].Ypoint / screenDivider) - 4, 4, 4);
                        g.DrawRectangle(p, rect);
                        rect = new Rectangle((int)(myDispensePads[x].Xpoint / screenDivider-4),
                            (int)(myDispensePads[x].Ypoint / screenDivider), 4, 4);
                        g.DrawRectangle(p, rect);
                        rect = new Rectangle((int)(myDispensePads[x].Xpoint / screenDivider),
                         (int)(myDispensePads[x].Ypoint / screenDivider), 4, 4);
                        g.DrawRectangle(p, rect);
                        break;
                    default:break;
            }


              if (myDispensePads[x].dotExtrusion > 0)
                {
                    xVal = (float)myDispensePads[x].Xpoint * stepFactor / outputDivider + xArrow + margeX;
                    yVal = (float)myDispensePads[x].Ypoint * stepFactor / outputDivider + yArrow + margeY;
   
        //  PROTECTION AGAINS EXCESSIVE X AND Y movements:
        
                if (xVal > checkValueX) continue;
                 if (yVal > checkValueY) continue;

                    // ? below
                 if ((x > 10) && (xVal == margeX) && (yVal == margeY)) continue;

                    direction = 0;

                    if ((xVal >= oldXval) && (yVal > oldYval)) direction = 1;       //   
                    else if ((xVal > oldXval) && (yVal <= oldYval)) direction = 3;  //  
                    else if ((xVal <= oldXval) && (yVal < oldYval)) direction = 5;   //                        
                    else if ((xVal < oldXval) && (yVal >= oldYval)) direction = 7;   //                        

                    if ((Math.Abs(oldXval - xVal) + Math.Abs(oldYval - yVal)) > 0)    // 5
                    {
                        MyXYZR.XYAction(sender, e, direction, Math.Abs(xVal - oldXval), Math.Abs(yVal - oldYval), false);
                        DisplayXYCoordinates(sender, e);

                        oldXval = xVal;
                        oldYval = yVal;
                    }

                    //  Z dispenser down 

                    MyXYZR.SetZSpeed(XYZMainForm.dispZDownSpeed);  // downwards
                    MyXYZR.ZAction(sender, e, XYZMainForm.dispZDownPosition);
                    DisplayAbsoluteZCoordinates(sender, e);
                    dispenserUp = false;

                    // DISPENSION OF 1..4 DOTS HERE 

                    DispensDot(sender, e, myDispensePads[x].dotForm, myDispensePads[x].dotDistance,
                                            myDispensePads[x].dotExtrusion);
                    
                    myDispensePads[x].dispensed = true;

                    // END DISPENSING   
                    //   Dispenser upwards
                    MyXYZR.SetZSpeed(XYZMainForm.dispZUpSpeed);
                    MyXYZR.ZAction(sender, e, XYZMainForm.dispZUpPosition);
                    DisplayAbsoluteZCoordinates(sender, e);
                    dispenserUp = true;
                }  // if extrusion>0

            } // end for

            // back to Origin at travel speed:        
            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYTravel.Text));
            // controls visible
            tableLayoutPanel1.Visible = true;
            // back to Origin                     
            MyXYZR.XYAction(sender, e, 5, XYZMainForm.globalActualMmPosX, XYZMainForm.globalActualMmPosY, false);
            DisplayXYCoordinates(sender, e);
            g.Dispose();
        }
        

        private void DispensDot(object sender, EventArgs e, int dotForm, float dotdistance, int extrusion)
        {                                       
            int extrusionMultiplier = 100;
            try
            {
                extrusionMultiplier = int.Parse(textBoxExtractionPercent.Text);
            }
            catch
            {
                extrusionMultiplier = 100;
            }
            extrusion = (extrusion * extrusionMultiplier) / 100;

            int retraction = 400;
            try
            {  retraction = int.Parse(textBoxRetractSetup.Text); }
            catch
            { retraction = 400; }

            extrusion = extrusion + retraction;
                       

            switch (dotForm)
            {
                case 1: // single dot       *                       
                    MyXYZR.tMove(1,extrusion);
                    MyXYZR.tMove(0, retraction); // retract 

                    break;
               case 2:  // 2 horizontal dots   * *            
                 MyXYZR.XYAction(sender, e, 1, dotdistance,0,false);  // leftwards
                    DisplayXYCoordinates(sender, e);               
                    MyXYZR.tMove(1, extrusion);
                   MyXYZR.tMove(0, retraction);    // 'retract'
                    MyXYZR.XYAction(sender, e, 5, dotdistance, 0, false);  // rightwards
                    MyXYZR.tMove(1,extrusion);
                    MyXYZR.tMove(0,retraction);    // 'retract'            
                    DisplayXYCoordinates(sender, e);

                    break;
                case 3:  // 2 vertical dots  *
                         //                  *

                    MyXYZR.XYAction(sender, e, 5,0, dotdistance/2 , false);  // upwards
                    MyXYZR.tMove(1,extrusion);
                    MyXYZR.tMove(0,retraction);    // 'retract'

                    MyXYZR.XYAction(sender, e, 1,0, dotdistance , false);  // downwards
                    DisplayXYCoordinates(sender, e);
                    MyXYZR.tMove(1,extrusion);
                    MyXYZR.tMove(0,retraction);    // 'retract'

                  //  MyXYZR.XYAction(sender, e, 5, 0,dotdistance , false);  // downwards to original position
                    DisplayXYCoordinates(sender, e);
                    break;
                case 4:  // 3 horizontal dots  * * *

                    MyXYZR.XYAction(sender, e, 1, dotdistance , 0, false);  // leftwards
                    DisplayXYCoordinates(sender, e);
                    MyXYZR.tMove(1,extrusion);
                    MyXYZR.tMove(0,retraction);    // 'retract'

                    MyXYZR.XYAction(sender, e, 5, dotdistance , 0, false);  // rightwards
                    DisplayXYCoordinates(sender, e);
                    MyXYZR.tMove(1,extrusion);
                    MyXYZR.tMove(0,retraction);    // 'retract'
                    MyXYZR.XYAction(sender, e, 5, dotdistance , 0, false);  // rightwards
                    DisplayXYCoordinates(sender, e);
                    MyXYZR.tMove(1,extrusion);
                    MyXYZR.tMove(0,retraction);    // 'retract'                    

                //    MyXYZR.XYAction(sender, e, 7, dotdistance , 0, false);  // leftwards to original position
                    DisplayXYCoordinates(sender, e);
                    break;
                case 5:  // 3 vertical dots   *
                         //                   *   
                         //                  *
                      
                    MyXYZR.XYAction(sender, e, 5, 0, dotdistance , false);  // upwards
                    DisplayXYCoordinates(sender, e);
                    MyXYZR.tMove(1,extrusion);
                    MyXYZR.tMove(0,retraction);    // 'retract'

                    MyXYZR.XYAction(sender, e, 1, 0, dotdistance , false);  // downwards
                    DisplayXYCoordinates(sender, e);
                    MyXYZR.tMove(1,extrusion);
                    MyXYZR.tMove(0,retraction);    // 'retract'

                    MyXYZR.XYAction(sender, e, 1, 0, dotdistance , false);  // downwards
                    DisplayXYCoordinates(sender, e);
                    MyXYZR.tMove(1,extrusion);
                    MyXYZR.tMove(0,retraction);    // 'retract'

                    MyXYZR.XYAction(sender, e, 5, 0, dotdistance , false);  // upwards to original position
                    

                    break;
                case 6:   // 4 dots          * *
                          //                 * * 
                   float displacement = (dotdistance * 14) / 10; // Pythagoras sqrt(2)=1.4
                  
                    MyXYZR.XYAction(sender, e, 7, displacement,displacement, false);  //left and downwards
                    DisplayXYCoordinates(sender, e);
                    MyXYZR.tMove(1,extrusion);
                    MyXYZR.tMove(0,retraction);    // 'retract'
                                                
                    MyXYZR.XYAction(sender, e, 5, 0, dotdistance , false);  // upwards
                    DisplayXYCoordinates(sender, e);
                    MyXYZR.tMove(1,extrusion);
                    MyXYZR.tMove(0,retraction);    // 'retract'
                                                
                    MyXYZR.XYAction(sender, e, 1, dotdistance ,0, false);  // rightwards
                    DisplayXYCoordinates(sender, e);
                    MyXYZR.tMove(1,extrusion);
                    MyXYZR.tMove(0,retraction);    // 'retract'
                                                
                    MyXYZR.XYAction(sender, e, 1,0, dotdistance , false);  // downwards
                    MyXYZR.tMove(1,extrusion);
                    MyXYZR.tMove(0,retraction);    // 'retract'                    
            
                    DisplayXYCoordinates(sender, e);
                    break;

                default: break;
        }

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


        private void UpdatePadListBox(object obj)
        {
            if (InvokeRequired)
            {
                ObjectDelegate method = new ObjectDelegate(UpdatePadListBox);
                Invoke(method, obj);
                return;
            }
            string S = (string)obj;
            listBoxPads.Items.Add(S + "\r\n");
        }

        private void buttonSaveDispenseFile_Click(object sender, EventArgs e)
        {
            int x = 0;
            saveFileDialog1.Filter = "Dispense files(*.disp) |*.disp";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.FileName = "*.disp";
            saveFileDialog1.RestoreDirectory = true;

            int totalPadCount = 0;
            for (x = 0; x < 5000; x++)
            {
                totalPadCount++;
                if ((myDispensePads[x].Xpoint == 0) &&
                    (myDispensePads[x].Ypoint == 0) &&
                    (myDispensePads[x].PadName.Length<2) )  break;
            }
                  


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter wr = new StreamWriter(saveFileDialog1.FileName))
                {                                      
                    wr.WriteLine(totalPadCount.ToString());  
                    wr.WriteLine(decimalsPrecision.ToString());    // 3 4 5 6 ...

                    for (x = 0; x < totalPadCount; x++)
                    {
                        myDispensePads[x].PadName = myDispensePads[x].PadName.TrimEnd('\r');
                        myDispensePads[x].PadName = myDispensePads[x].PadName.TrimEnd('\n');

                        wr.WriteLine(myDispensePads[x].PadName);  
                        wr.WriteLine(myDispensePads[x].IntPadName.ToString());
                        wr.WriteLine(myDispensePads[x].Xpoint.ToString());
                        wr.WriteLine(myDispensePads[x].Ypoint.ToString());
                        wr.WriteLine(myDispensePads[x].DispenseColor.ToString());  // Not in use any more
                        wr.WriteLine(myDispensePads[x].dotForm.ToString());
                       wr.WriteLine(myDispensePads[x].dotDistance.ToString("#.##"));
                        wr.WriteLine(myDispensePads[x].dotExtrusion.ToString());  // dot thickness
                    }
                }              //      end of streamwriter
            }   // savedialog ok
        }

        private void buttonOpenDispenseFile_Click(object sender, EventArgs e)
        {
            String S = "";
            String sFile = "";
            String PreviousPadName = "";
            int x = 0;      
            int TheListIndex = -1;

        //  clear :

            for (x = 0; x < 5000; x++)
            {
                myDispensePads[x].PadName = "";
                myDispensePads[x].IntPadName = 0;
                myDispensePads[x].Xpoint = 0;
                myDispensePads[x].Ypoint = 0;
                myDispensePads[x].DispenseColor = 0;
                myDispensePads[x].dotForm = 1;      // default
                myDispensePads[x].dotDistance = 0.0F;
                myDispensePads[x].dotExtrusion = 0;  // dot thickness
                myDispensePads[x].dispensed = false;
            }            

            openFileDialog1.Filter = "Dispense files(*.disp)|*.disp";      // 
            openFileDialog1.FilterIndex = 0; // 1
            openFileDialog1.FileName = "*.disp";
            openFileDialog1.RestoreDirectory = true;
                        
            ObjectDelegate del = new ObjectDelegate(UpdateListbox);
            ObjectDelegate del1 = new ObjectDelegate(UpdatePadListBox);

            ///***
            listBoxFileContent.Items.Clear();
            listBoxPads.Items.Clear();

            progressBarLoad.Value = 0;
           padCount = 0;         

       if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sFile = openFileDialog1.FileName;

                if (File.Exists(sFile))
                {
                    this.Text = sFile;
               
                listBoxPads.Visible = true;
           
                using (StreamReader sr = new StreamReader(sFile))
                    {
                        while (sr.EndOfStream == false)
                        {
                            try
                            {
                                S = sr.ReadLine();
                                del.Invoke(S);

                            }
                            catch
                            {
                             //   MessageBox.Show(S);
                            };


                            try
                            {
                            padCount = int.Parse(S);                            
                            }
                            catch
                            {
                               padCount = 4999;
                            }


                            try
                            {
                                S = sr.ReadLine();
                                del.Invoke(S);

                            }
                            catch
                            {
                                S = "5";    //   decimalsPrecision
                            };

                            try
                            {
                              decimalsPrecision = int.Parse(S);  
                            }
                            catch
                            {
                              decimalsPrecision = 5;
                            }

                            for (x = 0; x < padCount; x++)  // L-10
                            {
                                try
                                {
                                    S = sr.ReadLine();
                                    del.Invoke(S);
                                    myDispensePads[x].PadName = S; 
                                }
                                catch
                                {
                                    myDispensePads[x].PadName = "XXXX";  
                                }
                                                                                            
                                try
                                {
                                    if ((S.Length > 2) && (S.Contains("D")) && (S != PreviousPadName))
                                    {  
                                        del1.Invoke(S);                                          
                                        TheListIndex++;
                                        myPadList[TheListIndex].PadName = S;
                                        PreviousPadName = S;                                        
                                    }
                                }
                                catch
                                {                                  
                                  //  break;
                                }
                                
                                    try
                                    {
                                        S = sr.ReadLine();
                                    del.Invoke(S);
                                    myDispensePads[x].IntPadName = int.Parse(S);
                                    myPadList[TheListIndex].IntPadName = int.Parse(S);
                                }
                                    catch
                                    {
                                        myDispensePads[x].IntPadName = 101;
                                    }
                                
                                    try
                                    {
                                    S = sr.ReadLine();
                                    del.Invoke(S);
                                    myDispensePads[x].Xpoint = int.Parse(S);
                                     }
                                   catch
                                     {
                                            myDispensePads[x].Xpoint = 0;
                                     }

                                   try
                                        {
                                            S = sr.ReadLine();
                                    del.Invoke(S);
                                    myDispensePads[x].Ypoint = int.Parse(S);
                                        }
                                   catch
                                        {
                                           myDispensePads[x].Ypoint = 0;
                                        }
                                
                                    try
                                    {
                                           S = sr.ReadLine();
                                    del.Invoke(S);
                                    myDispensePads[x].DispenseColor = int.Parse(S);
                                     }
                                    catch
                                     {
                                            myDispensePads[x].DispenseColor = 255;
                                      }
                                
                                     try
                                         {
                                                 S = sr.ReadLine();
                                    del.Invoke(S);
                                    myDispensePads[x].dotForm = int.Parse(S);
                                               myPadList[TheListIndex].dotForm = int.Parse(S);
                                         }
                                    catch
                                         {
                                                myDispensePads[x].dotForm = 1;
                                         }

                                   try
                                       {
                                          S = sr.ReadLine();
                                    del.Invoke(S);
                                    // NEWWAS INT
                                    myDispensePads[x].dotDistance =float.Parse(S);
                                         myPadList[TheListIndex].dotDistance = float.Parse(S);
                                        }
                                  catch
                                       {
                                         myDispensePads[x].dotDistance = 0.0F;
                                       }                                                                                     

                                try
                                {
                                    S = sr.ReadLine();
                                    del.Invoke(S);
                                    myDispensePads[x].dotExtrusion = int.Parse(S); // dot thickness
                                    myPadList[TheListIndex].dotExtrusion = int.Parse(S);
                                }
                                catch
                                {
                                    myDispensePads[x].dotExtrusion = 0;
                                }                                                          
                          }                            
                              //        del.Invoke(S);
                             //   UpdateListbox(S);  
                            
                        }    // EndOfStream
                        sr.Close();
                    }  // using StreamReader
                
                    labelPadList.Visible = true;
                    listBoxPads.Visible = true;             
                    labelFileContent.Visible = true;
                    listBoxFileContent.Visible = true;
                    
                drawPadsAndDots(sender, e,0);
                    
                    buttonStartDispense.Visible = true;
                }  // File.exists

            }  // Dialog ok
        }

        // BELOW IS INTENDED FOR USE OF GERBER COMPONENT PASTE OR SOLDERMASK FILE
        

        private void buttonOpenGerberPad_Click(object sender, EventArgs e)
        {
            String S = "";
            int beginX=0,beginY=0,stop = 0;
            String sFile = "";
            int x = 0;    
            int dispenseColor = 255;  //  red;  
            int padCount = 0;
            
            openFileDialog1.Filter = "Gerber files(*.gbr)|*.gbr";   // GERBER WITH D03 INSTRUCTIONS
            openFileDialog1.FilterIndex = 0; // 1
            openFileDialog1.FileName = "*.gbr";
            openFileDialog1.RestoreDirectory = true;
                       
            ObjectDelegate del = new ObjectDelegate(UpdateListbox);
            ObjectDelegate del1 = new ObjectDelegate(UpdatePadListBox);
            listBoxFileContent.Items.Clear();  // ?

            listBoxPads.Items.Clear();

            progressBarLoad.Value = 0;
            padCount = 0;

            containsInch = 1; // contains inch dimensions
                        
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sFile = openFileDialog1.FileName;

                if (File.Exists(sFile))
                {
                    this.Text = sFile;

                    using (StreamReader sr = new StreamReader(sFile))
                    {

                        // Pass 1 determine different pad shapes

                        while (sr.EndOfStream == false)
                        {
                            S = sr.ReadLine();
                            del.Invoke(S);
                            if (S.Contains("MOMM"))   // "METRIC" was for hpgl
                            {
                                containsInch = 0;  // ==mm                                                 
                            }
                            
                            if (S.Contains("%FSLA"))   // decimalsPrecision
                            {
                                beginX = S.IndexOf("X");
                                beginY = S.IndexOf("Y");                              
                               decimalsPrecision = int.Parse(S.Substring(beginX + 2, 1));
                            }


                            if ( S.Contains("D")
                                &&  S.Contains("*")  
                                &&  (!S.Contains("D01")) 
                                && (!S.Contains("D02"))
                                && (!S.Contains("D03"))
                                && (!S.Contains("%ADD"))
                                && (!S.Contains("G04"))
                                && (!S.Contains("%TF"))   // KiCad   
                                && (!S.Contains("%LPD"))  // KiCad                                
                                )                           
                            {
                                //  listBoxPads.Items.Add(S);
                                
                                del1.Invoke(S);      // listboxpads
                                myPadList[padCount].PadName = S;

                             //   MessageBox.Show("S " + S + " S.length "+S.Length );

                            stop = S.IndexOf("*");
                             myPadList[padCount].IntPadName = int.Parse(S.Substring(1, stop - 1));
                               
                                padCount++;

                            }
                            //    UpdateListbox(S);

                            del.Invoke(S);                            
                            mirrored = false;
                        }
                        sr.Close();
                    }
                    
                    // Pass 2

               for (x = 4; x < listBoxFileContent.Items.Count - 1; x++)
                    {
                        S = listBoxFileContent.Items[x].ToString();   //       sr1.ReadLine();
                                                                      //     del.Invoke(S);

                            if (S.Contains("D")
                               && S.Contains("*")
                               && (S.Contains("D01")==false)
                               && (S.Contains("D02")==false)
                               && (S.Contains("D03")==false)
                               && (S.Contains("%ADD")==false)
                               && (S.Contains("G04")==false)
                                && (!S.Contains("%TF"))   // KiCad   
                                && (!S.Contains("%LPD"))  // KiCad 
                               )

                        {
                            // ATT :

                            stop = S.IndexOf("*");
                            myDispensePads[padCount].PadName = S.Substring(0,stop); // without \r\n                                                                               
                           
                            myDispensePads[padCount].IntPadName = int.Parse(S.Substring(1, stop-1 ));
                                                                         
                        }

                        dispenseColor = 255;                                           
                        

                        if ((S.Contains("X")) && S.Contains("Y") && S.Contains("D03"))
                        {
                            myDispensePads[padCount].Xpoint = ExtractXXY(S);
                            myDispensePads[padCount].Ypoint = ExtractXYY(S);
                            myDispensePads[padCount].DispenseColor = dispenseColor;

                            if ((padCount > 0) && (myDispensePads[padCount].PadName == ""))
                            {
                                myDispensePads[padCount].PadName = myDispensePads[padCount-1].PadName;
                                myDispensePads[padCount].IntPadName = myDispensePads[padCount -1].IntPadName;                               
                            }                            
                            padCount++;

                        }

                        if ((S.Contains("X")) && (S.Contains("Y") == false) && S.Contains("D03"))
                        {
                            myDispensePads[padCount].Xpoint = ExtractX(S);
                            if (padCount > 0)
                                myDispensePads[padCount].Ypoint = myDispensePads[padCount - 1].Ypoint; // previous val
                            myDispensePads[padCount].DispenseColor = dispenseColor;
                          
                            if ((padCount > 0) && (myDispensePads[padCount].PadName == ""))
                            {
                                myDispensePads[padCount].PadName = myDispensePads[padCount - 1].PadName;
                                myDispensePads[padCount].IntPadName = myDispensePads[padCount - 1].IntPadName;
                            }
                            padCount++;
                        }

                        if ((S.Contains("X") == false) && (S.Contains("Y")) && S.Contains("D03"))
                        {
                            if (padCount > 0)
                                myDispensePads[padCount].Xpoint = myDispensePads[padCount- 1].Xpoint; // previous val
                            myDispensePads[padCount].Ypoint = ExtractY(S);
                            myDispensePads[padCount].DispenseColor = dispenseColor;
                           
                            if ((padCount > 0) && (myDispensePads[padCount].PadName == ""))
                            {
                                myDispensePads[padCount].PadName = myDispensePads[padCount - 1].PadName;
                                myDispensePads[padCount].IntPadName = myDispensePads[padCount - 1].IntPadName;
                            }
                           padCount++;
                        }                                            
                    }
               
                    listBoxFileContent.Visible = true;
                    listBoxPads.Visible = true;   
                }  // file exists
            }   // openfiledialog

            commonPart(sender, e);
            progressBarLoad.Value = progressBarLoad.Maximum;
            buttonStartDispense.Visible = false;  // !
            
            labelPadList.Visible = true;
            listBoxPads.Visible = true;      
            labelFileContent.Visible = true;
            listBoxFileContent.Visible = true;

            drawPadsAndDots(sender, e,0); //..
        }


        public static int ExtractXXY(String S)
        {
            int ValX = -1; 
            int PosX = -1, PosY = -1; 
            String Stemp = "", StempY = "";
            PosX = S.IndexOf("X");
            PosY = S.IndexOf("Y");

            if ((PosX > -1) && (PosY > -1))
            {
                Stemp = S.Substring(PosX + 1, PosY - (PosX + 1));
                StempY = S.Substring(PosY + 1);
            }
            ValX = int.Parse(Stemp);
           
            return (ValX);
        }

        public static int ExtractXYY(String S)
        {
            int ValY = -1;
            int PosX = -1, PosY = -1, EndY = -1;
            String Stemp = "", StempY = "";
            PosX = S.IndexOf("X");
            PosY = S.IndexOf("Y");

            if ((PosX > -1) && (PosY > -1))
            {
                Stemp = S.Substring(PosX + 1, PosY - (PosX + 1));
                StempY = S.Substring(PosY + 1);
            }
          
            if (StempY.Contains("D")) EndY = StempY.IndexOf("D"); // if D is present this has prevalence over * !
            ValY = int.Parse(StempY.Substring(0, EndY));
            return (ValY);
        }
        

        public static int ExtractX(String S)
        {
            int ValX = -1;
            int PosX = -1, EndX = -1;
            String Stemp = "";
            PosX = S.IndexOf("X");
            if (PosX > -1) Stemp = S.Substring(PosX + 1);
            //    if (Stemp.Contains("*")) EndX = Stemp.IndexOf("*");
            if (Stemp.Contains("D")) EndX = Stemp.IndexOf("D"); // if D is present this has prevalence over * !

            ValX = int.Parse(Stemp.Substring(0, EndX));

            return (ValX);
        }

        public static int ExtractY(String S)
        {
            int ValY = -1;
            int PosY = -1, EndY = -1;
            String Stemp = "";
            PosY = S.IndexOf("Y");
            if (PosY > -1) Stemp = S.Substring(PosY + 1);
            //   if (Stemp.Contains("*")) EndY = Stemp.IndexOf("*");
            if (Stemp.Contains("D")) EndY = Stemp.IndexOf("D"); // if D is present this has prevalence over * !
            ValY = int.Parse(Stemp.Substring(0, EndY));
            return (ValY);
        }
    
        
        private void commonPart(object sender, EventArgs e)
        {                     
            int t;
            int minimumX = 999999999, minimumY = 999999999;
            int maximumX = -999999999, maximumY = -999999999;

            for (t = 0; t < padCount; t++)    // 20000
            {
                if (minimumX > myDispensePads[t].Xpoint) minimumX = myDispensePads[t].Xpoint;
                if (minimumY > myDispensePads[t].Ypoint) minimumY = myDispensePads[t].Ypoint;
            }

            for (t = 0; t < padCount; t++)
            {
                myDispensePads[t].Xpoint = myDispensePads[t].Xpoint - minimumX;
                myDispensePads[t].Ypoint = myDispensePads[t].Ypoint - minimumY;
            }

            for (t = 0; t < padCount; t++)    // 20000
            {
                if (maximumX < myDispensePads[t].Xpoint) maximumX = myDispensePads[t].Xpoint;
                if (maximumY < myDispensePads[t].Ypoint) maximumY = myDispensePads[t].Ypoint;
            }

            if ((checkBoxMirror.Checked) && (mirrored == false))
                {
                for (t = 0; t < padCount; t++)
                    myDispensePads[t].Ypoint = maximumY - myDispensePads[t].Ypoint+1; // +1 is very important
                mirrored = true;
            }
            // Values below are necessary on mouseclick in listbox
    
            globalMinimumX = minimumX;
            globalMinimumY = minimumY;

            float dividerX = 1, dividerY = 1;
            try { if (maximumX < 1000) maximumX = 1001; }
            catch { maximumX = 1; }
            try { if (maximumY < 1000) maximumY = 1000; }
            catch { maximumY = 1; }
            try { if (minimumX > 0) minimumX = 0; }
            catch { minimumX = 1; }
            try { if (minimumY > 0) minimumY = 0; }
            catch { minimumY = 1; }

            dividerX = ((float)(maximumX - minimumX)) / pictureBox1.Width;  
            dividerY = ((float)(maximumY - minimumY)) / pictureBox1.Height;
            screenDivider = dividerX;
            if (dividerY > dividerX) screenDivider = dividerY;
            if (screenDivider == 0) screenDivider = 1;

            progressBarLoad.Value = progressBarLoad.Maximum;
        }
        
        private void drawPadsAndDots(object sender, EventArgs e,int ThePadNumber)
        {
            int x=0;
            int Ydim = pictureBox1.Height + 1;  // for mirrored images, without +1 top border is not drawn               
            int Xloc=0, Yloc = 0;
            int Xell = 4, Yell = 4;
         
            int SelectedPadIndex = listBoxPads.SelectedIndex;
            if (SelectedPadIndex < 0) SelectedPadIndex = 0;
           
            commonPart(sender, e);  // Adjust and calculate TheScreenDivider            
            
            Graphics g = pictureBox1.CreateGraphics();         
            g.Clear(Functions.kleur(XYZMainForm.FondColor));

            Color DispenseColor = new Color();

            int dotDistance = 4; // used if pad has more then one dot.
            int PenThickness = 2;

            padCount = 0;
                        
            for (x = 0; x < 5000; x++)
            {
                padCount++;
                    if ((myDispensePads[x].Xpoint == 0) &&
                        (myDispensePads[x].Ypoint == 0) &&
                        (myDispensePads[x].PadName.Length<2)) break;
            }

            if (padCount < 10) padCount = 4999; // if dispense pads were not assigned yet


                for (x = 0; x < padCount; x++)
            {                                
                Xloc = (int)(myDispensePads[x].Xpoint / screenDivider);             
                Yloc = (int)(myDispensePads[x].Ypoint / screenDivider);                   
           
               if     ( ((ThePadNumber > (-1)) && (myDispensePads[x].IntPadName==ThePadNumber) )
                        || (myDispensePads[x].dispensed==true)  )
                {                   
                    myDispensePads[x].dotForm= myPadList[SelectedPadIndex].dotForm;
                    myDispensePads[x].dotDistance = myPadList[SelectedPadIndex].dotDistance;
                    myDispensePads[x].dotExtrusion =myPadList[SelectedPadIndex].dotExtrusion;

                    DispenseColor = Color.Red;   // ParFuncties.kleur(myDispensePads[x].DispenseColor);
                    Xell = 4;
                    Yell = 4;
                    PenThickness = 1;
                }
                else
                {         
                        DispenseColor = Color.Blue;                        
                        Xell = 4;
                        Yell = 4;
                    PenThickness = 1;
                }             
               
                Pen p = new Pen(DispenseColor, PenThickness);


                if (myDispensePads[x].dotExtrusion == 0.0F)   //   0 or 1 , 0 is Gerber case
                {
                    Rectangle rect = new Rectangle(Xloc -1, Yloc - 1,2,2);
                    g.DrawEllipse(p, rect);
                }


              else if (myDispensePads[x].dotForm < 2)   //   0 or 1 , 0 is Gerber case
                    {
                        Rectangle rect = new Rectangle(Xloc - dotDistance/2, Yloc - dotDistance/2, Xell, Yell );
                        g.DrawEllipse(p, rect);
                    }

              else if (myDispensePads[x].dotForm == 2)   // pads have 2 horizontal dots
                    {
                        Rectangle rect1 = new Rectangle(Xloc - dotDistance,Yloc-dotDistance/2, Xell, Yell);
                    g.DrawEllipse(p, rect1);
                        Rectangle rect2 = new Rectangle(Xloc, Yloc-dotDistance/2, Xell, Yell);
                    g.DrawEllipse(p, rect2); 
                   }

              else if (myDispensePads[x].dotForm == 3)   // pads have 2 vertical dots
                    {
                        Rectangle rect1 = new Rectangle(Xloc-dotDistance/2 ,Yloc - dotDistance, Xell, Yell);
                    g.DrawEllipse(p, rect1);
                        Rectangle rect2 = new Rectangle(Xloc-dotDistance/2 ,Yloc, Xell, Yell);
                    g.DrawEllipse(p, rect2);
                    }
                    
             else if (myDispensePads[x].dotForm == 4)   // pads have 3 horizontal dots
                    {
                        Rectangle rect1 = new Rectangle(Xloc - dotDistance*3/2,Yloc-dotDistance/2, Xell, Yell);
                    g.DrawEllipse(p, rect1);
                        Rectangle rect2 = new Rectangle(Xloc - dotDistance / 2, Yloc-dotDistance/2, Xell, Yell);
                    g.DrawEllipse(p, rect2);
                        Rectangle rect3 = new Rectangle(Xloc + dotDistance/2,Yloc-dotDistance/2, Xell, Yell);
                    g.DrawEllipse(p, rect3);
                    }
                    
              else if (myDispensePads[x].dotForm == 5)   // pads have 3 vertical dots
                    {
                        Rectangle rect1 = new Rectangle(Xloc-dotDistance/2, Yloc - dotDistance*3/2, Xell, Yell);
                    g.DrawEllipse(p, rect1);
                        Rectangle rect2 = new Rectangle(Xloc-dotDistance/2, Yloc - dotDistance/2 , Xell, Yell);
                    g.DrawEllipse(p, rect2);
                        Rectangle rect3 = new Rectangle(Xloc-dotDistance/2, Yloc + dotDistance/2, Xell, Yell);
                    g.DrawEllipse(p, rect3);
                    }

            else if (myDispensePads[x].dotForm == 6)   // pads have 4 dots              
                    {
                        Rectangle rect1 = new Rectangle(Xloc - dotDistance, Yloc- dotDistance, Xell, Yell);
                    g.DrawEllipse(p, rect1);
                        Rectangle rect2 = new Rectangle(Xloc - dotDistance, Yloc, Xell, Yell);
                    g.DrawEllipse(p, rect2);
                        Rectangle rect3 = new Rectangle(Xloc , Yloc - dotDistance, Xell, Yell);
                    g.DrawEllipse(p, rect3);
                        Rectangle rect4 = new Rectangle(Xloc , Yloc, Xell, Yell);
                    g.DrawEllipse(p, rect4);
                    }

                    p.Dispose();
                }    // for
            g.Dispose();
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

            listBoxFileContent.Items.Add( S.Trim() + "\r\n");
        }
       
        private void buttonDispense_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = false;
            buttonExecuteDispense_Click(sender, e);
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
                timer1.Enabled = true; // will execute drawPadsAnddots, refresh screen
            }
        }   

       

        private void radioButtonOne_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOne.Checked == true)
            {
                groupBoxDots.Text = "One dot";
                selectedDotCount = 1;
            }
            if (radioButtonTwoH.Checked == true)
            {
                groupBoxDots.Text = "Two horizontal dots";
                selectedDotCount = 2;
            }
            if (radioButtonTwoV.Checked == true)
            {
                groupBoxDots.Text = "Two vertical dots";
                selectedDotCount = 3;
            }
            if (radioButtonThreeH.Checked == true)
            {
                groupBoxDots.Text = "Three horizontal dots";
                selectedDotCount = 4;
            }
            if (radioButtonThreeV.Checked == true)
            {
                groupBoxDots.Text = "Three vertical dots";
                selectedDotCount = 5;
            }
            if (radioButtonFour.Checked == true)
            {
                groupBoxDots.Text = "Four dots";
                selectedDotCount = 6;
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            int x = 0;

            if (listBoxPads.SelectedIndex == -1) return;

            int i = listBoxPads.SelectedIndices[0]; 
            if (i < 0) i = 0;

            int CheckNumber = myPadList[i].IntPadName;

            selectedDotCount = 1;
            if (radioButtonOne.Checked == true) selectedDotCount = 1;
            if (radioButtonTwoH.Checked == true) selectedDotCount = 2;
            if (radioButtonTwoV.Checked == true) selectedDotCount = 3;
            if (radioButtonThreeH.Checked == true) selectedDotCount = 4;
            if (radioButtonThreeV.Checked == true) selectedDotCount = 5;
            if (radioButtonFour.Checked == true) selectedDotCount = 6;
         
            myPadList[i].dotForm = selectedDotCount;

            if (comboBoxDotDistance.Text == "") comboBoxDotDistance.Text = "0.0";

            myPadList[i].dotDistance = float.Parse(comboBoxDotDistance.Text);
            myPadList[i].dotExtrusion = int.Parse(comboBoxDotExtrusion.Text);
            
            for (x = 0; x < 5000; x++)
            {
              if (myDispensePads[x].IntPadName == CheckNumber)
                {                    
                    myDispensePads[x].dotForm = selectedDotCount;
                    myDispensePads[x].dotDistance = float.Parse(comboBoxDotDistance.Text);
                    myDispensePads[x].dotExtrusion = int.Parse(comboBoxDotExtrusion.Text);
                }
                // etc
            }
            drawPadsAndDots(sender, e,myPadList[i].IntPadName);  //
        }

        private void listBoxPads_SelectedIndexChanged(object sender, EventArgs e)
        {         
            int PadIndex = listBoxPads.SelectedIndex ;
                        
            if (PadIndex < 0)
            {
                MessageBox.Show("Please select one of the pads in above list.\nClick on it.");
                return;
            }
            else
            {          
                switch (myPadList[PadIndex].dotForm)
                {

                    case 1:
                        radioButtonOne.Checked = true;
                        break;
                    case 2:
                        radioButtonTwoH.Checked = true;
                        break;
                    case 3:
                        radioButtonTwoV.Checked = true;
                        break;
                    case 4:
                        radioButtonThreeH.Checked = true;
                        break;
                    case 5:
                        radioButtonThreeV.Checked = true;
                        break;
                    case 6:
                        radioButtonFour.Checked = true;
                        break;
                    default:
                        radioButtonOne.Checked = true;
                        break;
                }    // end switch


                if (myPadList[PadIndex].dotDistance!=0.0F)
                comboBoxDotDistance.Text = myPadList[PadIndex].dotDistance.ToString("#.##");
                else
                comboBoxDotDistance.Text = "0.0";
                
                comboBoxDotExtrusion.Text = myPadList[PadIndex].dotExtrusion.ToString();
                
                String S = listBoxPads.Items[PadIndex].ToString();
                                
                labelPadProperties.Visible = true;
                panelDot.Visible = true;

                drawPadsAndDots(sender, e, myPadList[PadIndex].IntPadName);  //                
            }
        }
               

        private void listBoxFileContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = 0, y = 0;
            int i = listBoxFileContent.SelectedIndex;
            String S = listBoxFileContent.Items[i].ToString();

            if ((S.Contains("X")) && S.Contains("Y") && S.Contains("D03"))
            {
                x = ExtractXXY(S);
                y = ExtractXYY(S);
                oldX = x;
                oldY = y;
            }

            if ((S.Contains("X")) && (S.Contains("Y") == false) && S.Contains("D03"))
            {
                x = ExtractX(S);
                y = oldY;
            }

            if ((S.Contains("X") == false) && (S.Contains("Y")) && S.Contains("D03"))
            {
                x = oldX;
                y = ExtractY(S);
            }

            x = (int)((float)(x - globalMinimumX) / screenDivider); 
            y = (int)((float)(y - globalMinimumY) / screenDivider); 
            
            Graphics g = pictureBox1.CreateGraphics();
            Pen p = new Pen(Color.Blue, 2);
            Rectangle rt = new Rectangle(x + 10, y + 10, 8, 6);
            g.DrawRectangle(p, rt);
            g.Dispose();     
        }
               
    
        private void buttonSetXYTravel_Click(object sender, EventArgs e)
        {         
            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYTravel.Text));
        }
                

        private void buttonSetZupSpeed_Click(object sender, EventArgs e)
        {  
            MyXYZR.SetZSpeed(int.Parse(comboBoxZup.Text));
        }

        private void buttonSetZdownSpeed_Click(object sender, EventArgs e)
        {          
            MyXYZR.SetZSpeed(int.Parse(comboBoxZdown.Text));
        }

        private void buttonSetZupPosition_Click(object sender, EventArgs e)
        {           
            MyXYZR.ZAction(sender,e,float.Parse(textBoxZupPosition.Text));
            dispenserUp = true;
        }

        private void buttonSetZdownPosition_Click(object sender, EventArgs e)
        {
            MyXYZR.ZAction(sender, e, float.Parse(textBoxZdownPosition.Text));
            dispenserUp = false;
        }

        private void comboBoxXYTravel_Leave(object sender, EventArgs e)
        {
            int temp = 25;
            try { temp = int.Parse(comboBoxXYTravel.Text); }
            catch { temp = 25; }
            XYZMainForm.dispXYTravelSpeed = temp;
        }
        private void comboBoxXYWork_Leave(object sender, EventArgs e)
        {
            int temp = 10;
            try { temp = int.Parse(comboBoxXYTravel.Text); }
            catch { temp = 10; }
            XYZMainForm.dispXYTravelSpeed = temp;
        }

        private void comboBoxZup_Leave(object sender, EventArgs e)
        {        
            XYZMainForm.dispZUpSpeed = int.Parse(comboBoxZup.Text);
        }

        private void comboBoxZdown_Leave(object sender, EventArgs e)
        {           
            XYZMainForm.dispZDownSpeed = int.Parse(comboBoxZdown.Text);
        }

        private void textBoxZupPosition_Leave(object sender, EventArgs e)
        {
            try
            {
                XYZMainForm.dispZUpPosition = float.Parse(textBoxZupPosition.Text);
            }
            catch
            {
                XYZMainForm.dispZUpPosition = 0.0F;
            }
        }

        private void textBoxZdownPosition_Leave(object sender, EventArgs e)
        {           
            XYZMainForm.dispZDownPosition = float.Parse(textBoxZdownPosition.Text);
        }
        
        private void DisplayXYCoordinates(object sender, EventArgs e)
        {
            labelXmm.Text = XYZMainForm.globalActualMmPosX.ToString("###.####");
            labelYmm.Text = XYZMainForm.globalActualMmPosY.ToString("###.####");

            if (labelXmm.Text == "") labelXmm.Text = "0.0";    //  otherwise nothing is displayed on 0
            if (labelYmm.Text == "") labelYmm.Text = "0.0";    //  otherwise nothing is displayed on 0        
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
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextboxReceive); // UpdateTextbox);
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

        private void buttonU_Click(object sender, EventArgs e) // U = up
        {
            float steps = float.Parse(textBoxXY.Text);
            serialPortCommandsXY(sender, e, 5, 0, steps, false);
            yArrow = yArrow - steps;
        }

      
        private void buttonExtract_Click(object sender, EventArgs e)
        {            
            int extractionSteps=1024;
            try
            {
                extractionSteps = int.Parse(textBoxExtract.Text);
            }
            catch
            {
                extractionSteps = 1024;
            }
            MyXYZR.tMove(1,extractionSteps);          
        }

        private void buttonRetract_Click(object sender, EventArgs e)
        {
            int retractionSteps = 512;
            try
            {
                retractionSteps = int.Parse(textBoxRetract.Text);
            }
            catch
            {
                retractionSteps = 512;
            }
            MyXYZR.tMove(0, retractionSteps);
        }


        private void buttonMakeArrayOfDots(object sender, EventArgs e)
        {            
            int extractionSteps = 1024;
            try
            {
                extractionSteps = int.Parse(textBoxExtract.Text);
            }
            catch
            {
                extractionSteps = 1024;
            }

            int retractionSteps = 400; // will extract this amount on start and retract on stop.
            try
            {
                retractionSteps = int.Parse(textBoxRetract.Text);
            }
            catch
            {
                retractionSteps = 400;
            }
    
            if (textBoxZupPosition.Text.Length < 1) textBoxZupPosition.Text = "0.0";

            for (int x = 0; x <7; x++)
                        {
                            MyXYZR.ZAction(sender, e, float.Parse(textBoxZdownPosition.Text));
                            MyXYZR.tMove(1, extractionSteps);                          
                            MyXYZR.tMove(0, retractionSteps);    // retract   
                            MyXYZR.ZAction(sender, e, float.Parse(textBoxZupPosition.Text));                         
                            serialPortCommandsXY(sender, e, 1, 1.27F, 0, false); // rightwards
                        }        
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            drawPadsAndDots(sender, e,0);  // ?? 0
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
