using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace XYZ
{
    public partial class DrawForm : Form
    {
                       
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport("gdi32.dll")]
        static extern int SetPixel(IntPtr hDC, int x, int y, int color);

        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

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

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool Ellipse(IntPtr hdc, int x1, int y1, int x2, int y2);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool Rectangle(IntPtr hdc, int X1, int Y1, int X2, int Y2);

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
            public int Thickness; // = pen width
            public int PenNr;     // = pen color on screen
        }
                      
        public static int MaxVectors = 100000;  // will be enough        
                
        public static PlotPoints[] MyPlotPoints = new PlotPoints[MaxVectors];
   
        public static string imageFileName = "";
        public static bool drawTheLine = false;
       
        public static bool HPGLmodePD = false;
        public static bool HPGLmodePU = true;   // Pen up is default to start with
        
        public static Point latestPoint = new Point();
  
        public static int arrayIndex = 0;        
        public static float screenDivider = 1.0F;
      
        private delegate void ObjectDelegate(object obj);
        public static Boolean PenUp = true;
        public static int penThickness = 1;
        public static int selectedPen = 1; 

        public DrawForm()
        {
            InitializeComponent();
            this.Top = -80;
            this.Left = -1;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;    //  - 20;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;  //  - 50;           
                                                                    
            panelDraw.Top = groupBox1.Top;
            panelDraw.Left = listBoxHPGL.Left + listBoxHPGL.Width + 20;  //  pictureBox1.Left + pictureBox1.Width;
            panelDraw.Width = this.Width - (listBoxHPGL.Width + 50);
            panelDraw.Height = this.Height - (panelDraw.Top + 50+labelFoot.Height);

            labelFoot.Left = panelDraw.Left;
            labelFoot.Top = panelDraw.Top + panelDraw.Height + 2;

            for (int x = 0; x < MaxVectors; x++)
            {
                MyPlotPoints[x].Xpoint = 0;
                MyPlotPoints[x].Ypoint = 0;
                MyPlotPoints[x].Mode = 0;
                MyPlotPoints[x].Thickness = 0;
                MyPlotPoints[x].PenNr = 0;
            }
            
            Color ButtonColor = Functions.kleur(XYZMainForm.IconColor);
            this.BackColor = Functions.kleur(XYZMainForm.FormColor);
            buttonMenu.BackColor = ButtonColor;
            buttonUndo.BackColor = ButtonColor;           
            buttonQuit.BackColor = ButtonColor;            
            checkBoxMirror.BackColor = ButtonColor;
            checkBoxDrawing.BackColor = ButtonColor;
            panelDraw.BackColor = Functions.kleur(XYZMainForm.FondColor);

            buttonMenu.Text = XYZMainForm.StrMenu;
            checkBoxMirror.Text = XYZMainForm.StrMirror;
            checkBoxDrawing.Text = XYZMainForm.StrDrawing;
            buttonUndo.Text = XYZMainForm.StrUndo;
            labelUndoSteps.Text = XYZMainForm.StrUndoSteps;
            buttonQuit.Text = XYZMainForm.StrQuit;

            groupBox1.Text = XYZMainForm.StrColor;
            labelThickness.Text = XYZMainForm.StrThickness;

            labelFoot.Text = XYZMainForm.StrFoot;

        }
        
        private void panelDraw_MouseDown(object sender, MouseEventArgs e)
        {
            if (!checkBoxDrawing.Checked) return;

            String S = "";
            ObjectDelegate del = new ObjectDelegate(UpdateListbox);

            drawTheLine = true;
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // Remember the location where the button was pressed
                latestPoint = e.Location;                     
                    S = "PU" + ((int)(e.X*screenDivider)).ToString() + "," + ((int)(e.Y*screenDivider)).ToString() + ";";    
                    del.Invoke(S); 
            }
        }
        
        private void panelDraw_MouseMove(object sender, MouseEventArgs e)
        {        
            if (!checkBoxDrawing.Checked) return;

            String S = "";
            ObjectDelegate del = new ObjectDelegate(UpdateListbox);

            Pen p = new Pen(Color.Black, penThickness);  // Sets PW Pen Width

            if (radioButtonBlack.Checked == true) p.Color = Color.Black;
            if (radioButtonBrown.Checked == true) p.Color = Color.Brown;
            if (radioButtonRed.Checked == true) p.Color = Color.Red;
            if (radioButtonOrange.Checked) p.Color = Color.Orange;
            if (radioButtonYellow.Checked) p.Color = Color.Yellow;
            if (radioButtonGreen.Checked) p.Color = Color.Green;
            if (radioButtonBlue.Checked) p.Color = Color.Blue;
            if (radioButtonViolet.Checked) p.Color = Color.Violet;
            if (radioButtonWhite.Checked) p.Color = Color.White;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                using (Graphics g = panelDraw.CreateGraphics() )               
                {
                    // Draw next line 
                    g.DrawLine(p, latestPoint, e.Location);

                    int tempDir = 1;
                    if (e.Y > latestPoint.Y) tempDir = 2;
                    if (e.X > latestPoint.X) tempDir = tempDir + 2;
                  
                    arrayIndex++;
                    MyPlotPoints[arrayIndex].Xpoint = e.X; //  NewPoint.X;
                    MyPlotPoints[arrayIndex].Ypoint = e.Y;  //  NewPoint.Y;
                    MyPlotPoints[arrayIndex].Mode = 1;

                    // ADD :
                    MyPlotPoints[arrayIndex].PenNr = selectedPen;
                    MyPlotPoints[arrayIndex].Thickness = penThickness;

                    buttonUndo.Text = XYZMainForm.StrUndo + "\n" + arrayIndex.ToString();  // "Undo\n" + GlobalArrayIndex.ToString();
                         
                    if (drawTheLine == true)
                    {
                        S = "PD" + ((int)(e.X*screenDivider)).ToString() + "," + ((int)(e.Y*screenDivider)).ToString() + ";";
                        
                        del.Invoke(S);
                        latestPoint = e.Location;
                    }              
                          
                }
            }
        }

        private void panelDraw_MouseUp(object sender, MouseEventArgs e)
        {
            if (!checkBoxDrawing.Checked) return;             
            drawTheLine = false;           
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

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


      private void buttonUndo_Click(object sender, EventArgs e)
        {          
            if (arrayIndex == 0) return;

            int x = 0;
            int dec = 1;
            try
            { dec = int.Parse(textBoxUndo.Text); }
            catch
            { dec = 1; }

            for (x = 0; x < dec ; x++)   
            {
                MyPlotPoints[arrayIndex].Xpoint = 0;
                MyPlotPoints[arrayIndex].Ypoint = 0;
                MyPlotPoints[arrayIndex].Mode = 0;
                MyPlotPoints[arrayIndex].PenNr = 0;
                MyPlotPoints[arrayIndex].Thickness = 0;

                arrayIndex--;
                if (arrayIndex == 0) break;
            }
            
            x = 0;
            
            while (x < ((dec * 1)))     // was *2 !
            {
                if (listBoxHPGL.Items.Count > 1)
                {
                    listBoxHPGL.BeginUpdate(); //
                    listBoxHPGL.SelectedIndex = listBoxHPGL.Items.Count - 1;  // remove of latest item
                    listBoxHPGL.Items[listBoxHPGL.SelectedIndex] = "";
                    listBoxHPGL.Items.RemoveAt(listBoxHPGL.SelectedIndex);
                    listBoxHPGL.EndUpdate(); // ?      
                }
                x++;
            }
            
            buttonUndo.Text = XYZMainForm.StrUndo + "\n" + arrayIndex.ToString();  // "\n"+listBoxHPGL.Items.Count.ToString();
                                                                 
           DrawArrayXY(sender, e);

            try { panelDraw.BackgroundImage = Image.FromFile(imageFileName); }
            catch { panelDraw.BackgroundImage = null; }

        }

        private void DrawArrayXY(object sender, EventArgs e)
        {
            int x;            
            int Ydim = panelDraw.Height + 1;  // for mirrored images, without +1 top border is not drawn

            Graphics g = panelDraw.CreateGraphics();             
            Pen p = new Pen(Color.Black, 2);

               for (x = 2; x < MaxVectors-1; x++)
            {
                switch ( MyPlotPoints[x].PenNr)
                {
                    case 1: p.Color = Color.Black; break;
                    case 2: p.Color = Color.Brown; break;
                    case 3: p.Color = Color.Red; break;
                    case 4: p.Color = Color.Orange; break;
                    case 5: p.Color = Color.Yellow; break;
                    case 6: p.Color = Color.Green; break;
                    case 7: p.Color = Color.Blue; break;
                    case 8: p.Color = Color.Violet; break;
                    case 9: p.Color = Color.White; break;
                    default: p.Color = Color.Black; break;
                }
                p.Width = MyPlotPoints[x].Thickness;                

            if (MyPlotPoints[x + 1].Mode == 1)
                {
                    if (checkBoxMirror.Checked == false)
                       g.DrawLine(p, MyPlotPoints[x].Xpoint / screenDivider, MyPlotPoints[x].Ypoint / screenDivider,
                       MyPlotPoints[x + 1].Xpoint / screenDivider, MyPlotPoints[x + 1].Ypoint / screenDivider);
                    else
                      g.DrawLine(p, MyPlotPoints[x].Xpoint / screenDivider,Ydim - MyPlotPoints[x].Ypoint / screenDivider,
                      MyPlotPoints[x + 1].Xpoint / screenDivider, Ydim - MyPlotPoints[x + 1].Ypoint / screenDivider);
                }                        
            }
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

        private void buttonMenu_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                progressBarLoad.Value = 0; // is always ok then                
                ContextMenuStrip ctx = new ContextMenuStrip();
                ctx.Font = new Font("Arial", 13);

                ctx.Items.Add(XYZMainForm.StrOpenFile, null, buttonOpenHpgl_Click);
                ctx.Items.Add(XYZMainForm.StrSaveFile, null, buttonSaveHpgl_Click);

                ctx.Items.Add("_________________________", null, null);

                ctx.Items.Add(XYZMainForm.StrLoadModel, null, buttonLoadImage_Click);

                ctx.Items.Add(XYZMainForm.StrClearModel, null, buttonClearImage_Click);
                ctx.Items.Add("_________________________", null, null);

                ctx.Items.Add(XYZMainForm.StrClearListbox, null, buttonClearListBox_Click);
                ctx.Items.Add(XYZMainForm.StrClearDrawing, null, buttonClearDrawing_Click);
                ctx.Items.Add(XYZMainForm.StrClearAll, null, buttonClearAll_Click);
                            
                ctx.Items.Add("_________________________", null, null);
                ctx.Items.Add(XYZMainForm.StrQuit, null, buttonQuit_Click);
                ctx.Show(this, new Point(buttonMenu.Left, buttonMenu.Top + buttonMenu.Height + 10));
            }
        }
        
        private void buttonLoadImage_Click(object sender, EventArgs e)
        {
           openFileDialogImage.Filter =
                "Jpg files(*.jpg)|*.jpg|jpeg files(*.jpeg)|*.jpeg|png files(*.png)|*.png|bmp files(*.bmp)|*.bmp";

            openFileDialogImage.FilterIndex = 0; // 1
            openFileDialogImage.FileName = "*.jpg";
            openFileDialogImage.RestoreDirectory = true;

            if (openFileDialogImage.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialogImage.FileName))
                {
                    imageFileName = openFileDialogImage.FileName;                    
                    try { panelDraw.BackgroundImage = Image.FromFile(imageFileName);  }
                    catch { panelDraw.BackgroundImage = null; }       // pictureBox1.Image = null; }                                
                }               
               DrawArrayXY(sender, e);
            }           
        }
        
        private void buttonOpenHpgl_Click(object sender, EventArgs e)
        {
            String S = "", S1 = "", S2 = "", Stemp = "";
           // int x = 0;
            int index = 0;
            int SplitIndex = 0;
            int cnt = 0;
            int position = 0;
            int totaal = 0;

            int[] CoordinatesX = new int[1000];  // maxmost 1000
            int[] CoordinatesY = new int[1000];
            int[] CoordinatesComma = new int[2000];

            openFileDialog1.Filter =
                  "Hpgl files(*.hpgl)|*.hpgl|Pen files(*.pen)|*.pen|Plt files(*.plt)|*.plt";
           
            openFileDialog1.FilterIndex = 0; // 1
            openFileDialog1.FileName = "*.hpgl";
            openFileDialog1.DefaultExt = "hpgl";  // ?
            openFileDialog1.RestoreDirectory = true;
                                   
            ObjectDelegate del = new ObjectDelegate(UpdateListbox);
            ObjectDelegate del1 = new ObjectDelegate(UpdateHiddenListbox);
                        
            listBoxHPGL.Items.Clear();  // ?
            listBoxHidden.Items.Clear();
            
            progressBarLoad.Value = 0;

            arrayIndex = 0;
            penThickness = 1;            // = important

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog1.FileName))
                {
                    this.Text = openFileDialog1.FileName;

                   listBoxHidden.Items.Clear();
                    progressBarLoad.Value = 0;
                    
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {                    
                        while (sr.EndOfStream == false)
                        {
                            S = sr.ReadLine();
                            Stemp = S;
                            index = 0;
                            
                            //       if comment with // is allowed, strip it:

                            if (Stemp.Contains("//"))
                            {
                               index = S.IndexOf("//");
                                if (index > 0) Stemp = Stemp.Remove(index, Stemp.Length - index);
                            }
                            Stemp.Trim();
                                                       
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

                        XYZMainForm.GlobalHPGLFileName = openFileDialog1.FileName;

                    }  // file exists

                }


                totaal = listBoxHidden.Items.Count - 1;
                if (totaal == 0) totaal = 1;
                totaal = (int)(totaal / 48);
                if (totaal == 0) totaal = 1;

                //    C                 A and B were easier then C..
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

                    if (CoordinatesComma[1] == 0) del.Invoke(S1);                    
                      
                        //     REMARK when no commas then the complete  S1    is added to listbox
                       //  + "\r\n" will be added by delegate there as it is
                  
                    else if (CoordinatesComma[1] > 0)
                    {
                        S1 = Snew.Substring(0, CoordinatesComma[1] - 1).Trim(); // PU or PD is in it                           
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
            }           // Openfiledialog = ok   

            else return;

            // Now that the listbox contains single pu x,y and pd x,y commands we will extract them:

            arrayIndex = 0;
            penThickness = 1;            // = important!
            int t = 0;

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

            selectedPen = 1;
            penThickness = 1;   // important !

            bool Up = true;

            while (t < listBoxHPGL.Items.Count)
            {
                listBoxHPGL.SelectedIndex = t;
                S = listBoxHPGL.Items[t].ToString();

                if ((t % totaal) == 0) progressBarLoad.Value++;
                if (progressBarLoad.Value > 45) progressBarLoad.Value = 45;
                
                if (S.Contains("SP"))    selectedPen = GetPenNummer(S);  //   
                if (S.Contains("PW"))    penThickness = GetPenWidth(S);

                if (S.Contains("PU")) Up = true;
                if (S.Contains("PD")) Up = false;
                
                if ((S.Length > 5) && (S.Contains(",")))
                {
                    MyPlotPoints[arrayIndex].Xpoint = GetArrayValues(S, 1); // 1 = X
                    MyPlotPoints[arrayIndex].Ypoint = GetArrayValues(S, 2); // 2 = Y
                                                                                 
                    if (Up == false) MyPlotPoints[arrayIndex].Mode = 1;
                    else MyPlotPoints[arrayIndex].Mode = 0;
                    
                    MyPlotPoints[arrayIndex].PenNr = selectedPen;
                    MyPlotPoints[arrayIndex].Thickness = penThickness;                    
                    arrayIndex++;           
                }
                t++;
            }

            int minimumX = 65000, minimumY = 65000;
            int maximumX = -65000, maximumY = -65000;

            for (t = 0; t < arrayIndex; t++)    // 20000
            {
                if (minimumX > MyPlotPoints[t].Xpoint) minimumX = MyPlotPoints[t].Xpoint;
                if (minimumY > MyPlotPoints[t].Ypoint) minimumY = MyPlotPoints[t].Ypoint;
            }

            int GetalX = 0;
            int GetalY = 0;

            if (minimumX < 0)          // case of negative coordinates !
            {
                GetalX = Math.Abs(minimumX);
                for (t = 0; t < arrayIndex; t++) MyPlotPoints[t].Xpoint = MyPlotPoints[t].Xpoint + GetalX;  // 20000
            }
            if (minimumY < 0)
            {
                GetalY = Math.Abs(minimumY);
                for (t = 0; t < arrayIndex; t++) MyPlotPoints[t].Ypoint = MyPlotPoints[t].Ypoint + GetalY;      // 20000
            }            

            // KEEP CODE BELOW FOR EXTERNAL HUGE FILES :
            
            float dividerX = 1, dividerY = 1;

            for (t = 0; t < arrayIndex; t++)       
            {
                if (maximumX < MyPlotPoints[t].Xpoint) maximumX = MyPlotPoints[t].Xpoint;
                if (maximumY < MyPlotPoints[t].Ypoint) maximumY = MyPlotPoints[t].Ypoint;
            }
            
            try { if (maximumX < 1000) maximumX = 1001; }
            catch { maximumX = 1; }

            try { if (maximumY < 1000) maximumY = 1000; }
            catch { maximumY = 1; }

            dividerX = ((float)maximumX / panelDraw.Width);  //* (int)
            dividerY = ((float)maximumY / panelDraw.Height);

            screenDivider = dividerX;
            if (dividerY > dividerX) screenDivider = dividerY;
    
            if (screenDivider == 0) screenDivider = 1;
            
            textBoxScreenDivider.Text = screenDivider.ToString("####.##");
                                 
            progressBarLoad.Value = progressBarLoad.Maximum;                        
            DrawArrayXY(sender, e);                        
            buttonUndo.Text = XYZMainForm.StrUndo+ "\n" + arrayIndex.ToString();
        }
        

        private void buttonSaveHpgl_Click(object sender, EventArgs e)
        {
            String S = "";
            int x = 0;

            saveFileDialog1.Filter =  "Hpgl files(*.hpgl) |*.hpgl|Plt files(*.plt)|*.plt|Pen files(*.pen)|*.pen";        
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.FileName = "*.hpgl";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter wr = new StreamWriter(saveFileDialog1.FileName))
                {
                    //  Write initialisation HPGL file :                         

                    wr.WriteLine("IN;");      // Begin
                    wr.WriteLine("IW0,0;");   //   Window from X = 0 and Y = 0
                    
                    wr.WriteLine("PU{0},{1};",panelDraw.Width,panelDraw.Height);  // to X = 6300 and Y = 4104 ????
                    
                    wr.WriteLine("PA;");      // when not specified, PA (plot absolute) mode is the default
                                              // so this line may be omitted
                                              // PR Plot Relative mode is probably less used.
                    wr.WriteLine("VS1;");     // Velocity select = speed
                    wr.WriteLine("SP1;");     //  Select pen 1    ??
                    wr.WriteLine("PW1;");    // to start with ???
                    

                    for (x = 0; x < listBoxHPGL.Items.Count - 1; x++)
                    {
                        S = listBoxHPGL.Items[x].ToString();
                        wr.Write(S);
                    }

           //      write the end, is not mandatory:
           //      wr.WriteLine("SP;");  // "SP0;"   is also ok   

                }   // end of streamwrite                               

                XYZMainForm.GlobalHPGLFileName = saveFileDialog1.FileName;  // will be used in plotter or cutter module
                
            }   // savedialog ok
        }
        
        private void buttonClearImage_Click(object sender, EventArgs e)      
        {
            Graphics g = panelDraw.CreateGraphics();
            g.Clear(Functions.kleur(XYZMainForm.FondColor));
            g.Dispose();
            imageFileName = "";    
            DrawArrayXY(sender, e);
        }


        private void buttonClearListBox_Click(object sender, EventArgs e)
        {
            listBoxHPGL.Items.Clear();
        }

        private void buttonClearDrawing_Click(object sender, EventArgs e)
        {
            int x = 0;
            for (x = 1; x < arrayIndex; x++)          // 20000 ?
            {
                MyPlotPoints[x].Xpoint = 0;
                MyPlotPoints[x].Ypoint = 0;
                MyPlotPoints[x].Mode = 0;                                
                MyPlotPoints[x].PenNr = 0;
                MyPlotPoints[x].Thickness = 0;                
            }

            penThickness = 1;
            selectedPen = 1;

            Graphics g = panelDraw.CreateGraphics();                    
            g.Clear(Functions.kleur(XYZMainForm.FondColor));

            g.Dispose();
        }
                
              private void buttonClearAll_Click(object sender, EventArgs e)
        {
            int x;  
            listBoxHPGL.Items.Clear();
            
           penThickness = 1;
           selectedPen = 1;

            Graphics g = panelDraw.CreateGraphics();
            Pen p = new Pen(Color.Green, 2);

            //    g.Clear(panelDraw.BackColor);
            g.Clear(Functions.kleur(XYZMainForm.FondColor));
            
            for (x = 1; x < arrayIndex; x++)          // 20000 ?
            {
                MyPlotPoints[x].Xpoint = 0;
                MyPlotPoints[x].Ypoint = 0;
                MyPlotPoints[x].Mode = 0;
                // ADD :
                MyPlotPoints[x].PenNr = selectedPen;
                MyPlotPoints[x].Thickness = penThickness;
            }
           
            g.Dispose();
            arrayIndex = 0;
            buttonUndo.Text = XYZMainForm.StrUndo; //  "Undo";
        }


        private void radioButtonBlack_CheckedChanged(object sender, EventArgs e)
        {
            String S = "SP";
            ObjectDelegate del = new ObjectDelegate(UpdateListbox);

            if (radioButtonBlack.Checked == true) selectedPen = 1;  // S = S + "1;";                      
            if (radioButtonBrown.Checked == true) selectedPen = 2;  //    S = S + "2;";
            if (radioButtonRed.Checked == true) selectedPen = 3;  //S = S + "3;"; 
            if (radioButtonOrange.Checked)   selectedPen = 4;  //     S = S + "4;";
            if (radioButtonYellow.Checked)  selectedPen = 5;  //        S = S + "5;"; 
            if (radioButtonGreen.Checked)  selectedPen = 6;  //        S = S + "6;";
            if (radioButtonBlue.Checked)   selectedPen = 7;  //         S = S + "7;";
            if (radioButtonViolet.Checked)  selectedPen = 8;  //       S = S + "8;";
            if (radioButtonWhite.Checked) selectedPen = 9;  //        S = S + "9;";
            
            S = S + selectedPen.ToString()+";";           
            del.Invoke(S);

            // Code below for pen width is also necessary  !!!!!!!!!!!!!

            S = "PW";
            S = S + penThickness.ToString() + ";";
            del.Invoke(S);            
        }        
    

        private void panelBoxLines_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panelBoxLines.CreateGraphics();
            Pen p = new Pen(Color.Black, 1);
            for (int x = 1; x < 10; x++)
            {
                p.Dispose();
                p = new Pen(Color.Black, x);
                g.DrawLine(p, 0, panelBoxLines.Height * x / 10, panelBoxLines.Width, panelBoxLines.Height * x / 10);               
            }                   
            p.Dispose();
            Pen pp = new Pen(Color.Red, 2);
            int Ystart = ( penThickness  * (panelBoxLines.Height / 10 ) ) ;                               
            Rectangle rect = new Rectangle(5, Ystart - panelBoxLines.Height / 40, panelBoxLines.Width - 10, panelBoxLines.Height / 20);
            g.DrawRectangle(pp, rect);
            pp.Dispose();
            g.Dispose();
        }

        private void panelBoxLines_MouseClick(object sender, MouseEventArgs e)
        {
            String S = "PW"; // Pen Width            
                        
            Graphics g = panelBoxLines.CreateGraphics();
            g.Clear(panelBoxLines.BackColor);
            Pen p = new Pen(Color.Black, 1);

            for (int x = 1; x < 10; x++)
            {
                p.Dispose();
                p = new Pen(Color.Black, x);
                g.DrawLine(p, 0, panelBoxLines.Height * x / 10, panelBoxLines.Width, panelBoxLines.Height * x / 10);
            }
            p.Dispose();                               
            Pen pp = new Pen(Color.Red,2);

          int Ystart =   ((e.Y*9) / panelBoxLines.Height)*(panelBoxLines.Height/10)+ panelBoxLines.Height / 10;           

            penThickness = (e.Y * 9) / panelBoxLines.Height+1;                       
            Rectangle rect = new Rectangle(5, Ystart-panelBoxLines.Height/40  , panelBoxLines.Width - 10, panelBoxLines.Height / 20);
            g.DrawRectangle(pp, rect);            
            pp.Dispose();
            g.Dispose();
            
            // Below is also necessary, if penthickness changes, write also SP number !

            S = "SP";
            ObjectDelegate del = new ObjectDelegate(UpdateListbox);

            if (radioButtonBlack.Checked == true) S = S + "1;";
            if (radioButtonBrown.Checked == true) S = S + "2;";
            if (radioButtonRed.Checked == true) S = S + "3;";
            if (radioButtonOrange.Checked) S = S + "4;";
            if (radioButtonYellow.Checked) S = S + "5;";
            if (radioButtonGreen.Checked) S = S + "6;";
            if (radioButtonBlue.Checked) S = S + "7;";
            if (radioButtonViolet.Checked) S = S + "8;";
            if (radioButtonWhite.Checked) S = S + "9;";

            del.Invoke(S);

            // Code below for pen width is also necessary  !
            S = "PW";
            S = S + penThickness.ToString() + ";";
            del.Invoke(S);
        }

        private void listBoxHPGL_SelectedIndexChanged(object sender, EventArgs e)
        {

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
