
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace XYZ
{
    public partial class DrillForm : Form
    {
        public struct DrillPoints
        {
            public int Xpoint;
            public int Ypoint;
            public float drillDiameter;          
            public bool drilled; // true when drilled
        }
  
        public static int maxVectors = 5000;  // 5000 would be a huge PCB
        public static int vectorIndex = 0;
        
        public static DrillPoints[] MyDrillPoints = new DrillPoints[maxVectors];

        public static float[] Drills = new float[99];   // diameter of drills
        
        public static Point OldPoint = new Point();
        
        public static float screenDivider = 1.0F;      // factor is used for drawing on the screen

        private delegate void ObjectDelegate(object obj);

        public static Boolean drillUp = true;
        public static int fileInUse = 0;     //0 = no previous HPGL file loaded in draw module

        public static int containsInch = 0;    // 1= input is in inch  0 input is in mm

        public static int decimalsPrecision;
        public static int oldX=0, oldY=0;

        public static float xArrow = 0.0F; // secundary X displacement when arrow is used during pause
        public static float yArrow = 0.0F; // secundary Y displacement when arrow is used during pause
               
        public static int globalMaximumX, globalMaximumY;
        public static int globalMinimumX, globalMinimumY;

        XYZR MyXYZR = new XYZR();
                
      public DrillForm()
        {
            InitializeComponent();

            MyXYZR.InitSerialPort();

            this.Top = -1;
            this.Left = -1;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;    //  - 20;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;  //  - 50;           
            
            listBoxDrillFile.Height = this.Height / 2;
            listBoxOutput.Top = listBoxDrillFile.Top + listBoxDrillFile.Height + 2;
            
            pictureBox1.Top = listBoxDrillFile.Top;
            pictureBox1.Left = listBoxDrillFile.Left + listBoxDrillFile.Width + 20;
            pictureBox1.Width = this.Width - (listBoxDrillFile.Width + 50);
            pictureBox1.Height = this.Height - (pictureBox1.Top + 50);

            for (int x = 0; x < maxVectors; x++)
            {
                MyDrillPoints[x].Xpoint = 0;
                MyDrillPoints[x].Ypoint = 0;
                MyDrillPoints[x].drilled = false;                       
            }
      
            Color ButtonColor = Functions.kleur(XYZMainForm.IconColor);
            this.BackColor = Functions.kleur(XYZMainForm.FormColor);
            buttonMenu.BackColor = ButtonColor;

            checkBoxMirror.BackColor = ButtonColor;
 
            buttonDrill.BackColor = ButtonColor;
            buttonQuit.BackColor = ButtonColor;        
            checkBoxPause.BackColor = ButtonColor;

            pictureBox1.BackColor = Functions.kleur(XYZMainForm.FondColor);
                       
            comboBoxDecimals.Text = "5";
            
            int btW = this.Width / 16; //15
            int btM = 2;
            int btH = this.Height / 11;//12

            buttonMenu.Left = btM;
            buttonMenu.Top = btM;
            buttonMenu.Width = btW;
            buttonMenu.Height = btH;

            progressBarLoad.Left = btM + btW + btM;
            progressBarLoad.Top = btM;
            progressBarLoad.Width = btW * 5 / 4;
            progressBarLoad.Height = btH / 2 - 4;

            groupBoxRGB.Left = progressBarLoad.Left;
            groupBoxRGB.Top = btH / 2;
            groupBoxRGB.Width = progressBarLoad.Width;
            groupBoxRGB.Height = btH / 2;
                                  
            checkBoxMirror.Left = progressBarLoad.Left + progressBarLoad.Width + btM;
            checkBoxMirror.Top = btM;   //
            checkBoxMirror.Width = btW;
            checkBoxMirror.Height = btH;
           
            buttonDrill.Left =checkBoxMirror.Left + btW * 1 + btM;
            buttonDrill.Top = btM;
            buttonDrill.Width = btW;
            buttonDrill.Height = btH;

            checkBoxPause.Left = buttonDrill.Left + btW + btM;
            checkBoxPause.Top = btM;
            checkBoxPause.Width = btW;
            checkBoxPause.Height = btH;

            tableLayoutPanel1.Left = checkBoxPause.Left + btW + btM;
            tableLayoutPanel1.Top = 0;
            tableLayoutPanel1.Height = btH + btM;
            tableLayoutPanel1.Width = btW * 5;

            tableLayoutPanel2.Left = tableLayoutPanel1.Left + btW * 5 + btM;
            tableLayoutPanel2.Top = 0;
            tableLayoutPanel2.Height = btH + btM;
            tableLayoutPanel2.Width = btW * 15 / 8;
            
            labelDecimals.Left = tableLayoutPanel2.Left + btW * 15 / 8 + btM*2;
            labelDecimals.Top = btM*4;
            labelDecimals.Width = btW;

            comboBoxDecimals.Left = labelDecimals.Left+btM*2;
            comboBoxDecimals.Top =  btH / 2;
            comboBoxDecimals.Width = (btW*2)/3;
            comboBoxDecimals.Height = btH / 4;
            
            buttonQuit.Left = comboBoxDecimals.Left + btW + btM*3;
            buttonQuit.Top = btM;
            buttonQuit.Width = btW;
            buttonQuit.Height = btH;

            listBoxDrillFile.Left = btM;
            listBoxDrillFile.Top = btM * 3 + btH;
            listBoxDrillFile.Width = btW * 3;
            listBoxDrillFile.Height = this.Height / 2;

            listBoxOutput.Left = btM;
            listBoxOutput.Top = listBoxDrillFile.Top + listBoxDrillFile.Height + btM;
            listBoxOutput.Width = listBoxDrillFile.Width;
            listBoxOutput.Height = this.Height / 2;
            
            pictureBox1.Top = listBoxDrillFile.Top;
            pictureBox1.Left = listBoxDrillFile.Left + listBoxDrillFile.Width + btM;
            pictureBox1.Height = this.Height - (btH + btM * 12);
            pictureBox1.Width = this.Width - (listBoxDrillFile.Width + btM * 8);
                       
            buttonDrill.BackColor = ButtonColor;
            buttonQuit.BackColor = ButtonColor;
            checkBoxMirror.BackColor = ButtonColor;
            checkBoxPause.BackColor = ButtonColor;
            pictureBox1.BackColor = Functions.kleur(XYZMainForm.FondColor);
            
            comboBoxXYTravel.Text = XYZMainForm.drillXYTravelSpeed.ToString();
      
            comboBoxZup.Text = XYZMainForm.drillZUpSpeed.ToString();
            comboBoxZdown.Text = XYZMainForm.drillZDownSpeed.ToString();

            textBoxZupPosition.Text = XYZMainForm.drillZUpPosition.ToString("##.####");    // was 1000
            textBoxZdownPosition.Text = XYZMainForm.drillZDownPosition.ToString("##.####"); // was 1000

            buttonMenu.Text = XYZMainForm.StrMenu;
            checkBoxMirror.Text = XYZMainForm.StrMirror;
         
            buttonDrill.Text = XYZMainForm.StrStartDrill;
                 buttonQuit.Text = XYZMainForm.StrQuit;      
            checkBoxPause.Text = XYZMainForm.StrPause;
          
            labelXmm.Text = XYZMainForm.globalActualMmPosX.ToString("###.###");
            labelYmm.Text = XYZMainForm.globalActualMmPosY.ToString("###.###");
            labelZmm.Text = XYZMainForm.globalActualMmPosZ.ToString("###.###");
            
            if (labelXmm.Text == "") labelXmm.Text = "0.0"; // otherwise nothing is displayed on 0
            if (labelYmm.Text == "") labelYmm.Text = "0.0"; // otherwise nothing is displayed on 0
            if (labelZmm.Text == "") labelZmm.Text = "0.0"; // otherwise nothing is displayed on 0
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {           
            MyXYZR.CloseSerialPort();

     // XY speed

            try { XYZMainForm.drillXYTravelSpeed = int.Parse(comboBoxXYTravel.Text); }
            catch { XYZMainForm.drillXYTravelSpeed = 25; }
       // NO WORK XY SPEED, XY DISPLACEMENTS WHEN DRILL IS DOWN ARE NOT ALLOWED !
    
         // Z speed                      
            try { XYZMainForm.drillZUpSpeed = int.Parse(comboBoxZup.Text); }
            catch { XYZMainForm.drillZUpSpeed = 5123; } // 5
                     
            try { XYZMainForm.drillZDownSpeed = int.Parse(comboBoxZdown.Text); }
            catch { XYZMainForm.drillZDownSpeed = 5456; } // 5

         // Z position        
            try { XYZMainForm.drillZUpPosition = float.Parse(textBoxZupPosition.Text); }
            catch { XYZMainForm.drillZUpPosition = 2.5F; }
                      
            try { XYZMainForm.drillZDownPosition = float.Parse(textBoxZdownPosition.Text); }
            catch { XYZMainForm.drillZDownPosition = 5.1F; }

            this.Close();           
        }

        private void buttonMenu_MouseClick(object sender, MouseEventArgs e)
        {
            fileInUse = 0;

            if (e.Button == MouseButtons.Left)
            {
                progressBarLoad.Value = 0; // is always ok then                
                ContextMenuStrip ctx = new ContextMenuStrip();
                ctx.Font = new Font("Arial", 13);
        
                ctx.Items.Add(XYZMainForm.StrOpenDrillFile, null, buttonOpenDrill_Click); // "Open *.drl file "
                ctx.Items.Add("_________________________", null, null);
        
                ctx.Items.Add(XYZMainForm.StrClearAll, null, buttonClearAll_Click);  // "Clear all"
                ctx.Items.Add("_________________________", null, null);
                               

                ctx.Items.Add(XYZMainForm.StrReset + " drill", null, buttonReset_Click); // "Reset drillotter"
                ctx.Items.Add("_________________________", null, null);

                if (listBoxDrillFile.Items.Count > 2)
                {
                    ctx.Items.Add(XYZMainForm.StrStartDrill, null, buttonDrill_Click);  //  buttonExecute_Click);  // "Send to plotter"
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
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
        }
        
        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            int x;
            pictureBox1.Image = null;
            listBoxDrillFile.Items.Clear();
    
            Graphics g = pictureBox1.CreateGraphics();
            Pen p = new Pen(Color.Green, 2);
         
            g.Clear(Functions.kleur(XYZMainForm.FondColor));
            
            for (x = 0; x < vectorIndex; x++)          
            {
                MyDrillPoints[x].Xpoint = 0;
                MyDrillPoints[x].Ypoint = 0;
                MyDrillPoints[x].drilled = false;         
            }
            g.Dispose();
            buttonDrill.Visible = false;
        }

            
        private void buttonExecute_Click(object sender, EventArgs e)
        {          
            ObjectDelegate del1 = new ObjectDelegate(UpdateListBoxOutput);
            float outputDivider = 1.0F;
           
            int x, direction = 0;
            float xVal = 0.0F, yVal = 0.0F, oldXval = 0.0F, oldYval = 0.0F;
            
            int yDim = (int)(globalMaximumY / screenDivider);
            
            Graphics g = pictureBox1.CreateGraphics();    

            Pen p = new Pen(Color.Green, 2);
            if (radioButtonR.Checked) p = new Pen(Color.Red, 2);
            else if (radioButtonG.Checked) p = new Pen(Color.Green, 2);
            else if (radioButtonB.Checked) p = new Pen(Color.Blue, 2);

            outputDivider = (float)(Math.Pow((double)10, (double)(decimalsPrecision) ) );
            
            if (containsInch == 1) outputDivider = (outputDivider * 254) / 100;
                   
     float  checkValueX = XYZMainForm.MyCommonCoordinates.xTotalGround + XYZMainForm.GlobalMargeX ;
     float  checkValueY = XYZMainForm.MyCommonCoordinates.yTotalGround + XYZMainForm.GlobalMargeY;
                   
            if (drillUp == false)  // pen is down
            {      
                MyXYZR.SetXYSpeed(XYZMainForm.drillZUpSpeed);  // upwards
                MyXYZR.ZAction(sender, e, XYZMainForm.drillZUpPosition);
                drillUp = true;
            }

            // set XY travel speed:              
            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYTravel.Text));  // upwards

            oldXval = XYZMainForm.globalActualMmPosX;
            oldYval = XYZMainForm.globalActualMmPosY;
            
            for (x = 1; x < vectorIndex; x++)

            {                                  
                if (checkBoxPause.Checked == true)
                {
                    Application.DoEvents();
                    x = x - 1;

                    if (x < 1) x = 1;

                    continue;
                }

                if (radioButtonR.Checked) p = new Pen(Color.Red, 2);
                else if (radioButtonG.Checked) p = new Pen(Color.Green, 2);
                else if (radioButtonB.Checked) p = new Pen(Color.Blue, 2);
                
                xVal = MyDrillPoints[x].Xpoint/outputDivider+ xArrow+ XYZMainForm.GlobalMargeX;    
                yVal = MyDrillPoints[x].Ypoint/outputDivider + yArrow+ XYZMainForm.GlobalMargeY;
                               
       //       BELOW PROTECTION AGAINS EXCESSIVE X AND Y movements:

                if ( xVal > checkValueX ) continue;                
                if ( yVal  > checkValueY) continue;                                                                      

                if ((MyDrillPoints[x+1].Xpoint == 0) && (MyDrillPoints[x + 1].Ypoint == 0)) continue;
                                           
                    Rectangle rect = new Rectangle((int)(MyDrillPoints[x].Xpoint / screenDivider) - 3,
                                               (int)(MyDrillPoints[x].Ypoint / screenDivider) - 3, 6, 6);
                    g.DrawRectangle(p, rect);
                                                                       
               direction = 0;

                if ((xVal >= oldXval) && (yVal > oldYval)) direction = 1;       //   
                else if ((xVal > oldXval) && (yVal <= oldYval)) direction = 3;  //  
                else if ((xVal <= oldXval) && (yVal < oldYval)) direction = 5;   //                        
                else if ((xVal < oldXval) && (yVal >= oldYval)) direction = 7;   //           

                if ((Math.Abs(xVal - oldXval) + Math.Abs(yVal -oldYval)) > 0)                     
                {                       
                    MyXYZR.XYAction(sender, e, direction, Math.Abs(xVal - oldXval) ,
                                                   Math.Abs(yVal - oldYval) , false);
                    DisplayXYCoordinates(sender, e); 
                    oldXval = xVal;
                    oldYval = yVal;                   
                    String S = "D " + direction.ToString() + " X " + xVal.ToString() + " Y " + yVal.ToString();
                    del1.Invoke(S);
                }

                // Drill down :
                     MyXYZR.SetZSpeed(XYZMainForm.drillZDownSpeed);  // very slow downwards
                     MyXYZR.ZAction(sender, e, XYZMainForm.drillZDownPosition);
                
                MyDrillPoints[x].drilled = true;
                DisplayAbsoluteZCoordinates(sender, e);

                drillUp = false;

                // Drill up               
                MyXYZR.SetZSpeed(XYZMainForm.drillZUpSpeed);  // very slow downwards
                MyXYZR.ZAction(sender, e, XYZMainForm.drillZUpPosition);

                DisplayAbsoluteZCoordinates(sender, e);
                drillUp = true;
            }
                       
            // back to Origin at travel speed:        
            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYTravel.Text));
            // controls visible
            tableLayoutPanel1.Visible = true;
            // back to Origin                     
            MyXYZR.XYAction(sender, e, 5, XYZMainForm.globalActualMmPosX, XYZMainForm.globalActualMmPosY, false);
            DisplayXYCoordinates(sender, e);                        
            g.Dispose();
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
        
        private void UpdateListBoxOutput(object obj)
        {
            if (InvokeRequired)
            {
                ObjectDelegate method = new ObjectDelegate(UpdateListBoxOutput);  // (UpdateTextboxReceive);
                Invoke(method, obj);
                return;
            }
            string S = (string)obj;
            listBoxOutput.Items.Add(S);           
        }

        private void buttonOpenDrill_Click(object sender, EventArgs e)
        {
            String S = "";    
            openFileDialog1.Filter =        "Drill files(*.drl)|*.drl";
            openFileDialog1.FilterIndex = 0; // 1
            openFileDialog1.FileName = "*.drl";
            openFileDialog1.RestoreDirectory = true;

            ObjectDelegate del = new ObjectDelegate(UpdateListbox);
       
            listBoxDrillFile.Items.Clear();  // 
            progressBarLoad.Value = 0;
            vectorIndex = 0;
             
            buttonClearAll_Click(sender, e);
                   
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog1.FileName))
                {                    
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        while (sr.EndOfStream == false)
                        {
                            S = sr.ReadLine();
                            del.Invoke(S);
                            if (S.Contains("METRIC"))
                            {
                                containsInch = 0;  // ==mm
                                break;
                            }
                            if (S.Contains("INCH"))
                            {
                                containsInch = 1;  // ==Inch
                                break;
                            }
                        }
                    }
                    
                    string search = "";
                    string subs="";
                    float fDiameter = 0.0F;

              using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {

                    while (sr.EndOfStream == false)
                        {
                            S = sr.ReadLine(); // no duplicates in listbox  = METRIC

                            if (S.Contains(";")) continue; // sometimes comments with ; in a file

                            if ( S.Contains("%") || S.Contains("M95") || S.Contains("M30"))  break;
                            
                            for (int i = 1; i < 99; i++)
                            {
                                search = "T" + i.ToString() + "C";

                                if (S.Contains(search))
                                {
                                    if (i<10)
                                    subs = S.Substring(3);
                                    else
                                    subs = S.Substring(4);
                                    Drills[i] = float.Parse(subs);                                                               
                                }
                            }
                        }                                                                                      
                            
                    while (sr.EndOfStream == false)
                        {
                           S = sr.ReadLine();

                            if (S.Contains(";")) continue; // sometimes comments with ; in a file

                            if (S.Contains("M30")) break;

                            del.Invoke(S);

                            if (S.Contains("T"))
                            {
                                subs = S.Substring(1);
                                fDiameter = float.Parse(subs);
                            }
                            
                            if (S.Contains("X")  && S.Contains("Y")  )
                               {
                                MyDrillPoints[vectorIndex].Xpoint = GetArrayValues(S, 1);     // 1 = X
                                MyDrillPoints[vectorIndex].Ypoint = GetArrayValues(S, 2) + 1; // 2 = Y ->+1 is necessary to draw top border !
                                MyDrillPoints[vectorIndex].drillDiameter = fDiameter;
                                vectorIndex++;
                            }
                        }   // end stream
                    }
                }   // file.exists
            }   // openfiledialog

            buttonCommonPart_Click(sender, e);
            progressBarLoad.Value = progressBarLoad.Maximum;
            buttonDrill.Visible = true; 
            DrawArrayXY(sender, e);
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
            else if (StempY.Contains("*")) EndY = StempY.IndexOf("*"); //             
            else EndY = StempY.Length;
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
            if (Stemp.Contains("D")) EndX = Stemp.IndexOf("D"); // if D is present this has prevalence over * !
            else if (Stemp.Contains("*")) EndX = Stemp.IndexOf("*"); //             
            else EndX = Stemp.Length;
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
            if (Stemp.Contains("D")) EndY = Stemp.IndexOf("D"); // if D is present this has prevalence over * !
            else if (Stemp.Contains("*")) EndY = Stemp.IndexOf("*"); //                         
            else EndY = Stemp.Length;
            ValY = int.Parse(Stemp.Substring(0, EndY));           
            return (ValY);
        }
                    
        private void buttonCommonPart_Click(object sender, EventArgs e)
        {     
            int t;
            int minimumX = 999999999, minimumY = 999999999;
            int maximumX = -999999999, maximumY = -999999999;

            for (t = 0; t < vectorIndex; t++)    // 20000
            {
                if (minimumX > MyDrillPoints[t].Xpoint) minimumX = MyDrillPoints[t].Xpoint;
                if (minimumY > MyDrillPoints[t].Ypoint) minimumY = MyDrillPoints[t].Ypoint;          
            }

            for (t = 0; t < vectorIndex; t++)
            {
                MyDrillPoints[t].Xpoint = MyDrillPoints[t].Xpoint - minimumX;  
                MyDrillPoints[t].Ypoint = MyDrillPoints[t].Ypoint - minimumY;
            }         

            for (t = 0; t < vectorIndex; t++)    // 20000
            {   
                 if (maximumX < MyDrillPoints[t].Xpoint) maximumX = MyDrillPoints[t].Xpoint;
                 if (maximumY < MyDrillPoints[t].Ypoint) maximumY = MyDrillPoints[t].Ypoint;                
            }

            if (checkBoxMirror.Checked)
                for (t = 0; t < vectorIndex; t++)
                    MyDrillPoints[t].Ypoint = maximumY - MyDrillPoints[t].Ypoint+1; // +1 is very important
                        
            minimumX = 999999999; ; minimumY = 999999999;
            maximumX = -999999999; maximumY = -999999999;

            for (t = 0; t < vectorIndex; t++)    // 20000
            {
                if (minimumX > MyDrillPoints[t].Xpoint) minimumX = MyDrillPoints[t].Xpoint;
                if (minimumY > MyDrillPoints[t].Ypoint) minimumY = MyDrillPoints[t].Ypoint;
            }
                       
            for (t = 0; t < vectorIndex; t++)    // 20000
            {
                if (maximumX < MyDrillPoints[t].Xpoint) maximumX = MyDrillPoints[t].Xpoint;
                if (maximumY < MyDrillPoints[t].Ypoint) maximumY = MyDrillPoints[t].Ypoint;
            }

            // Values below are necessary on mouseclick in listbox
            
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
        }
              

        private void DrawArrayXY(object sender, EventArgs e)
        {
            int iDiameter = 0;                                         
            Graphics g = pictureBox1.CreateGraphics();
            Pen p = new Pen(Color.Black, 2);

            Pen pDone = new Pen(Color.Red, 2); // colour for drilled holes

            if (radioButtonR.Checked) pDone = new Pen(Color.Red, 2);
            else if (radioButtonG.Checked) pDone = new Pen(Color.Green, 2);
            else if (radioButtonB.Checked) pDone = new Pen(Color.Blue, 2);
            
         for (int x=0;x<vectorIndex;x++)
            {
                iDiameter = (int)(MyDrillPoints[x].drillDiameter*2);
                    Rectangle rect = new Rectangle((int)(MyDrillPoints[x].Xpoint / screenDivider-iDiameter/2)  ,
                        (int)(MyDrillPoints[x].Ypoint / screenDivider-iDiameter/2) ,iDiameter,iDiameter);
                    g.DrawEllipse(p, rect);

                if (MyDrillPoints[x].drilled == true)
                {
                    Rectangle rect1 = new Rectangle((int)(MyDrillPoints[x].Xpoint / screenDivider) - 3,
                                               (int)(MyDrillPoints[x].Ypoint / screenDivider) - 3, 6, 6);
                    g.DrawRectangle(pDone, rect1);
                }
            }

            p.Dispose();
            pDone.Dispose();
            g.Dispose();
        }        
        
      private int GetArrayValues(string S, int mode)
        {
            int x1,x2,y1;
            string S1 = "", S2 = "";
            int CoordX = 0, CoordY = 0;        
            x1 = S.IndexOf('X');
            x2 = S.IndexOf('Y');
            try {
                S1 = S.Substring(x1+1, x2 - (x1+1));   
               }
            catch { return (0); }

            y1 = S.IndexOf('Y');
            try { S2 = S.Substring(y1+1); }       //     y - (x + 1)); }
            catch { return (0); }
            try { CoordX = int.Parse(S1); }
            catch { return (0); }
            try { CoordY = int.Parse(S2); }
            catch { return (0); }    
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

            listBoxDrillFile.Items.Add(S + "\r\n");
        }
       

        private void buttonDrill_Click(object sender, EventArgs e)
        {         
            tableLayoutPanel1.Visible = false;
            buttonExecute_Click(sender, e);
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
       

        private void comboBoxDecimals_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                decimalsPrecision = int.Parse(comboBoxDecimals.Text);
            }
            catch
            {
                decimalsPrecision =5;
            }
        }

        private void DrillForm_Load(object sender, EventArgs e)
        {
            try
            {
                decimalsPrecision = int.Parse(comboBoxDecimals.Text);
            }
            catch
            {
                decimalsPrecision = 5;
            }
        }
        

        private void DrillForm_Resize(object sender, EventArgs e)
        {
            DrawArrayXY(sender, e);
        }

       
        private void buttonSetXYTravel_Click(object sender, EventArgs e)
        {       

            int tempdelay = 25;
            try
            {
                tempdelay = int.Parse(comboBoxXYTravel.Text);
            }
            catch
            {
                tempdelay = 100;
            }

            if (tempdelay < 2) tempdelay = 2;   
            if (tempdelay > 150) tempdelay = 150;            
            MyXYZR.SetXYSpeed(tempdelay);
        }
             
        private void buttonSetXYWork_Click(object sender, EventArgs e)
        {        
            // NOT :
        }


        private void buttonSetZupSpeed_Click(object sender, EventArgs e)
        {      
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
            if (tempdelay > 6000) tempdelay = 6000; // 0.05 mm.sec

           MyXYZR.SetZSpeed(tempdelay);
        }

        private void buttonSetZdownSpeed_Click(object sender, EventArgs e)
        {       

            int tempdelay = 5;
            try
            {
                tempdelay = int.Parse(comboBoxZdown.Text.ToString());
            }
            catch
            {
                tempdelay = 5;
            }

            if (tempdelay < 2) tempdelay = 2;   // maxmost speed is then 250mm /sec
            if (tempdelay > 150) tempdelay = 150; // 0.05 mm.sec

           MyXYZR.SetZSpeed(tempdelay);       
        }

        private void buttonSetZupPosition_Click(object sender, EventArgs e)
        {
            float steps = 5.0F;
            try { steps = float.Parse(textBoxZupPosition.Text); }
            catch { steps = 50.0F; }
           
            MyXYZR.ZAction(sender, e, steps);
            drillUp = true;
        }

        private void buttonSetZdownPosition_Click(object sender, EventArgs e)
        {
        //  We have to take into account actual Z position
            float steps = 10.0F;
            try { steps = float.Parse(textBoxZdownPosition.Text); }
            catch { steps = 10.0F; }       
            MyXYZR.ZAction(sender, e, steps);
            drillUp = false;
        }

        private void comboBoxXYTravel_Leave(object sender, EventArgs e)
        {
            int temp = 20;
            try { temp = int.Parse(comboBoxXYTravel.Text); }
            catch { temp = 0; }
            XYZMainForm.drillXYTravelSpeed = temp;
        }

        private void comboBoxZup_Leave(object sender, EventArgs e)
        {
            int temp = 5;
            try { temp = int.Parse(comboBoxZup.Text); }
            catch { temp = 5; }          
             XYZMainForm.drillZUpSpeed = temp;
        }

        private void comboBoxZdown_Leave(object sender, EventArgs e)
        {   
            int temp = 5;
            try { temp = int.Parse(comboBoxZdown.Text); }
            catch { temp = 5; }
            XYZMainForm.drillZDownSpeed = temp;
        }

        private void textBoxZupPosition_Leave(object sender, EventArgs e)
        {     
           float temp = 5.0F;
            try { temp = float.Parse(textBoxZdownPosition.Text); }
            catch { temp = 5.0F; }            
            XYZMainForm.drillZUpPosition = temp;
        }

        private void textBoxZdownPosition_Leave(object sender, EventArgs e)
        {           
            XYZMainForm.drillZDownPosition = float.Parse(textBoxZdownPosition.Text);
        }
                

        private void listBoxDrillFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = listBoxDrillFile.SelectedIndex;
            String S = listBoxDrillFile.Items[i].ToString();
            int x = 0, y = 0;

            if ((S.Contains("X")) && S.Contains("Y")) // D02  D03  
            {
                x = ExtractXXY(S);
                y = ExtractXYY(S);
                oldX = x;
                oldY = y;
            }

            if ((S.Contains("X")) && (S.Contains("Y") == false)) // && S.Contains("D02"))  // D03
            {
                x = ExtractX(S);
                y = oldY;
            }

            if ((S.Contains("X") == false) && (S.Contains("Y")))  // && S.Contains("D02"))  // D03
            {
                x = oldX;
                y = ExtractY(S);
            }

            x = (int)((float)(x - globalMinimumX) / screenDivider); // 1059;
            y = (int)((float)(y - globalMinimumY) / screenDivider); //  1059;

            Graphics g = pictureBox1.CreateGraphics();
            Pen p = new Pen(Color.Green, 1);

            if (radioButtonR.Checked) p = new Pen(Color.Red, 2);
            if (radioButtonG.Checked) p = new Pen(Color.Green, 2);
            if (radioButtonB.Checked) p = new Pen(Color.Blue, 2);

            if (checkBoxMirror.Checked == false)
            {
                Rectangle rt = new Rectangle(x - 5, y - 5, 10, 10);
                g.DrawRectangle(p, rt);
            }
            if (checkBoxMirror.Checked == true)
            {
                int yDim = (int)(globalMaximumY / screenDivider);

                Rectangle rt1 = new Rectangle(x - 5, (yDim - y) - 5, 10, 10);
                g.DrawRectangle(p, rt1);
            }
            p.Dispose();
            g.Dispose();
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
            ObjectDelegate delReceive = new ObjectDelegate(UpdateListBoxOutput); // UpdateTextbox);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawArrayXY(sender, e);
            timer1.Enabled = false;
        }

        private void DisplayXYCoordinates(object sender, EventArgs e)
        {
            labelXmm.Text = XYZMainForm.globalActualMmPosX.ToString("###.####");
            labelYmm.Text = XYZMainForm.globalActualMmPosY.ToString("###.####");

            if (labelXmm.Text == "") labelXmm.Text = "0.0";    //  otherwise nothing is displayed on 0
            if (labelYmm.Text == "") labelYmm.Text = "0.0";    //  otherwise nothing is displayed on 0        
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
