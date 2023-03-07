using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;


namespace XYZ
{
    public partial class XYZCommandForm : Form
    {              
      
        private delegate void ObjectDelegate(object obj);          
        XYZR MyXYZR = new XYZR();        

        public XYZCommandForm()
        {                
            InitializeComponent();            
            this.Top = -1;
            this.Left = -1;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width - 20;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height - 50;
         
            Color buttonColor = Functions.kleur(XYZMainForm.IconColor);
            this.BackColor = Functions.kleur(XYZMainForm.FormColor);       
            buttonMenu.BackColor = buttonColor;
           
            buttonQuit.BackColor = buttonColor;
            checkBoxLaser.BackColor = buttonColor;
            
            int btW = this.Width / 16;  // button Width
            int btM = 4;                // marge
            int btH = this.Height / 10; // button Height 

            buttonMenu.Left = btM;
            buttonMenu.Top = btM;
            buttonMenu.Width = btW;
            buttonMenu.Height = btH;

            groupBoxAction.Left = buttonMenu.Left + buttonMenu.Width + btM;
            groupBoxAction.Top = btM;
            groupBoxAction.Width = btW * 3;
            groupBoxAction.Height = btH;

              radioButtonPlot.Left = btM;
              radioButtonPlot.Top = btH / 3+btM;
              radioButtonPlot.Width = groupBoxAction.Width / 5; // (btW * 3) / 5;
              radioButtonPlot.Height =( btH * 3) / 5-btM;

              radioButtonCut.Left = radioButtonPlot.Left+radioButtonPlot.Width;
              radioButtonCut.Top = radioButtonPlot.Top;
              radioButtonCut.Width = radioButtonPlot.Width;
              radioButtonCut.Height =radioButtonPlot.Height;

              radioButtonDrill.Left = radioButtonCut.Left + radioButtonPlot.Width;
              radioButtonDrill.Top = radioButtonPlot.Top;
              radioButtonDrill.Width = radioButtonPlot.Width;
              radioButtonDrill.Height = radioButtonPlot.Height;

              radioButtonDisp.Left = radioButtonDrill.Left + radioButtonPlot.Width;
              radioButtonDisp.Top = radioButtonPlot.Top;
              radioButtonDisp.Width = radioButtonPlot.Width;
              radioButtonDisp.Height = radioButtonPlot.Height;

              radioButtonMill.Left = radioButtonDisp.Left + radioButtonPlot.Width;
              radioButtonMill.Top = radioButtonPlot.Top;
              radioButtonMill.Width = radioButtonPlot.Width;
              radioButtonMill.Height = radioButtonPlot.Height;

            checkBoxLaser.Left = btM * 3 + btW * 4;
            checkBoxLaser.Top = btM;
            checkBoxLaser.Width = btW;
            checkBoxLaser.Height = btH;

            tableLayoutPanel1.Left = checkBoxLaser.Left + btW + btM;
            tableLayoutPanel1.Top = btM;
            tableLayoutPanel1.Width = btW * 7;
            tableLayoutPanel1.Height = btH;

            tableLayoutPanelPos.Left = tableLayoutPanel1.Left + btW * 7 + btM;
            tableLayoutPanelPos.Top = btM;
            tableLayoutPanelPos.Width = btW * 15/8;
            tableLayoutPanelPos.Height = btH;

            buttonQuit.Left = tableLayoutPanelPos.Left + tableLayoutPanelPos.Width + btM;
            buttonQuit.Top = btM;
            buttonQuit.Width = btW;
            buttonQuit.Height = btH;                                 

            if (XYZMainForm.selectedCommandAction == 1)      radioButtonPlot.Checked = true;   // plot                
            else if (XYZMainForm.selectedCommandAction == 2)  radioButtonCut.Checked = true;    // cut
            else if (XYZMainForm.selectedCommandAction == 3)  radioButtonDrill.Checked = true; // drill               
            else if (XYZMainForm.selectedCommandAction == 4)  radioButtonDisp.Checked = true;       // dispenser                  
            else if (XYZMainForm.selectedCommandAction == 5)  radioButtonMill.Checked = true;  // mill            
            
            labelXmm.Text = XYZMainForm.globalActualMmPosX.ToString("###.###");
            labelYmm.Text = XYZMainForm.globalActualMmPosY.ToString("###.###");       
            labelZmm.Text = XYZMainForm.globalActualMmPosZ.ToString("###.###");

            MyXYZR.InitSerialPort(); 

            if (labelXmm.Text == "") labelXmm.Text = "0.0"; // otherwise nothing is displayed on 0
            if (labelYmm.Text == "") labelYmm.Text = "0.0"; // otherwise nothing is displayed on 0
            if (labelZmm.Text == "") labelZmm.Text = "0.0"; // otherwise nothing is displayed on 0

            labelZUp.Text = "Z " + XYZMainForm.StrUp;
            labelZdown.Text = "Z " + XYZMainForm.StrDown;

            buttonQuit.Text = XYZMainForm.StrQuit;            
        }
              
        
        private void buttonL_Click(object sender, EventArgs e)
        {           
            float steps = float.Parse(textBoxLX.Text);
            serialPortCommandsXY(sender, e, 5, steps, 0, false);
        }

        private void buttonLL_Click(object sender, EventArgs e)
        {          
            float steps = float.Parse(textBoxLLX.Text);
            serialPortCommandsXY(sender, e, 5, steps, 0, false);
        }

        private void buttonLLL_Click(object sender, EventArgs e)
        {            
            float steps = float.Parse(textBoxLLLX.Text);
            serialPortCommandsXY(sender, e, 5, steps, 0, false);
        }

        private void buttonR_Click(object sender, EventArgs e)
        {
           
            float steps = float.Parse(textBoxRX.Text);
            serialPortCommandsXY(sender, e, 1, steps, 0, false);
           
            //TEMP
         //   MyXYZR.EscapeXY_Move(1, 600, 0);

        }

        private void buttonRR_Click(object sender, EventArgs e)
        {          
            float steps = float.Parse(textBoxRRX.Text);
            serialPortCommandsXY(sender, e, 1, steps, 0, false);
        }

        private void buttonRRR_Click(object sender, EventArgs e)
        {          
            float steps = float.Parse(textBoxRRRX.Text);
            serialPortCommandsXY(sender, e, 1, steps, 0, false);
        }

        private void buttonU_Click(object sender, EventArgs e)
        {           
            float steps = float.Parse(textBoxUY.Text);
            serialPortCommandsXY(sender, e, 5, 0,steps, false);
        }

        private void buttonUU_Click(object sender, EventArgs e)
        {            
            float steps = float.Parse(textBoxUUY.Text);
            serialPortCommandsXY(sender, e, 5, 0, steps, false);
        }


        private void buttonUUU_Click(object sender, EventArgs e)
        {           
            float steps = float.Parse(textBoxUUUY.Text);
            serialPortCommandsXY(sender, e, 5, 0, steps, false);
        }
        
        private void buttonD_Click(object sender, EventArgs e)
        {          
            float steps = float.Parse(textBoxDY.Text);
            serialPortCommandsXY(sender, e, 1, 0, steps, false);
        }

        private void buttonDD_Click(object sender, EventArgs e)
        {         
            float steps = float.Parse(textBoxDDY.Text);
            serialPortCommandsXY(sender, e, 1, 0, steps, false);
        }

        private void buttonDDD_Click(object sender, EventArgs e)
        {            
            float steps = float.Parse(textBoxDDDY.Text);
            serialPortCommandsXY(sender, e, 1, 0, steps, false);
        }
              
        private void buttonReset_Click(object sender, EventArgs e)
        {
            ObjectDelegate del1 = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delRX = new ObjectDelegate(UpdateTextbox);
            
            MyXYZR.Reset(sender, e);
           del1.Invoke("Reset "); // + XYZMainForm.SerialPortReturnS);
            while (MyXYZR.NextCommand == false) Application.DoEvents();

            if (XYZMainForm.SerialPortReturnS.Contains("43")) 
            delRX.Invoke("Reset ok " + XYZMainForm.SerialPortReturnS);
            else
            delRX.Invoke("Reset NOT ok " + XYZMainForm.SerialPortReturnS);
        }

        private void buttonMenu_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ContextMenuStrip ctx = new ContextMenuStrip();
                ctx.Font = new Font("Arial", 12);                                           
                   
                // Below disabled
              //      ctx.Items.Add("Detect hardware XY limits", null, buttonDetectXYLimits_Click);
             //       ctx.Items.Add("_________________________", null, null);
                             
                ctx.Items.Add("Reset PIC", null, buttonReset_Click);
                ctx.Items.Add("_________________________", null, null);

                ctx.Items.Add("Quit", null,buttonQuit_Click);
                ctx.Show(this, new Point(buttonMenu.Left, buttonMenu.Top + buttonMenu.Height + 10));

            }
        }
        
        
     private void buttonDetectXYLimits_Click(object sender, EventArgs e)
        {                  

            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextbox);

            XYZMainForm.SerialPortReturnS = "";
            XYZMainForm.SerialPortSendS = "";


            MyXYZR.leftFlagX = false;
            MyXYZR.rightFlagX = false;
            MyXYZR.backFlagY = false;
            MyXYZR.frontFlagY = false;           

       MyXYZR.SetXYSpeed(20); // some reasonable speed

            // Get dimension along X axis 
            // Leftwards

            MyXYZR.leftFlagX = false;
           while (MyXYZR.leftFlagX == false)
            {     
                MyXYZR.XYAction(sender, e, 5, 100.0F, 0, true);
                delListbox.Invoke(XYZMainForm.SerialPortSendS);
                while (MyXYZR.NextCommand == false) Application.DoEvents();
                delReceive.Invoke(XYZMainForm.SerialPortReturnS);
            }

           // 3 mm rightwards

      MyXYZR.EscapeXY_Move(1, 600, 0);

            while (MyXYZR.NextCommand == false) Application.DoEvents();
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
           


            // Rightwards

            XYZMainForm.globalActualMmPosX = 0.0F;
     

            MyXYZR.rightFlagX = false;
         while (MyXYZR.rightFlagX == false)
            {
                MyXYZR.XYAction(sender, e, 1, 100.0F, 0, true);
                delListbox.Invoke(XYZMainForm.SerialPortSendS);
                while (MyXYZR.NextCommand == false) Application.DoEvents();
                delReceive.Invoke(XYZMainForm.SerialPortReturnS);              
            }

            // 3 mm leftwards

            MyXYZR.EscapeXY_Move(5, 600, 0);  // Leftwards
            while (MyXYZR.NextCommand == false) Application.DoEvents();
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
         
            // Get dimension along Y axis
                        
            MyXYZR.backFlagY = false;
            while (MyXYZR.backFlagY == false)
            {
                MyXYZR.XYAction(sender, e, 5, 0.0F, 100.0F, true);
                delListbox.Invoke(XYZMainForm.SerialPortSendS);
                while (MyXYZR.NextCommand == false) Application.DoEvents();
                delReceive.Invoke(XYZMainForm.SerialPortReturnS);
             }

            // 3 mm frontwards 
                         
            MyXYZR.EscapeXY_Move(1, 0, 600);  // frontwards
            while (MyXYZR.NextCommand == false) Application.DoEvents();
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
          
            // frontwards

            XYZMainForm.globalActualMmPosY = 0.0F;
                
            MyXYZR.frontFlagY = false;
            while (MyXYZR.frontFlagY == false)
            {              
                MyXYZR.XYAction(sender, e, 1,0.0F , 100.0F, true);

                delListbox.Invoke(XYZMainForm.SerialPortSendS);
                while (MyXYZR.NextCommand == false) Application.DoEvents();
                delReceive.Invoke(XYZMainForm.SerialPortReturnS);

            }

            // 3 mm backwards  
            MyXYZR.EscapeXY_Move(5, 0, 600);  // backwards
            while (MyXYZR.NextCommand == false) Application.DoEvents();
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
            


            //    Thread.Sleep(250);

            XYZMainForm.MyCommonCoordinates.xTotalGround = XYZMainForm.globalActualMmPosX - 6.0F;   // 6.0F = 2*3 mm escape
            XYZMainForm.MyCommonCoordinates.yTotalGround = XYZMainForm.globalActualMmPosY - 6.0F;   // 6.0F = 2*3 mm escape 
            
            MessageBox.Show("X done xTotalGround " +
                 XYZMainForm.MyCommonCoordinates.xTotalGround.ToString("###.##")+
                 "  Y done xTotalGround " +
                 XYZMainForm.MyCommonCoordinates.yTotalGround.ToString("###.##")       );
            
            // home

            serialPortCommandsXY(sender, e, 5, XYZMainForm.MyCommonCoordinates.xTotalGround,
                XYZMainForm.MyCommonCoordinates.yTotalGround, false);



        }


        private void buttonUUUZ_Click(object sender, EventArgs e)
        {
            //   Acme screw pitch 2 mm                    
            //   800 1/32 steps advance 1 mm          
            float steps = 0.0F;
            try { steps = float.Parse(textBoxUUUZ.Text); }
            catch { steps = 1.0F; }
            serialPortCommandsZ(sender, e, steps, 0); // 0 = upwards 
        }     

        private void buttonUUZ_Click(object sender, EventArgs e)
        {          
            float steps = 0.0F;
            try { steps = float.Parse(textBoxUUZ.Text); }
            catch { steps = 1.0F; }
            serialPortCommandsZ(sender, e, steps, 0); // 0 = upwards 
        }

        private void buttonUZ_Click(object sender, EventArgs e)
        {           
            float steps = 0.0F;
            try { steps = float.Parse(textBoxUZ.Text); }
            catch { steps = 1.0F; }
            serialPortCommandsZ(sender, e, steps, 0); // 0 = upwards 
        }
        
        private void buttonDDDZ_Click(object sender, EventArgs e)
        {         
            float steps = 0.0F;
            try { steps = float.Parse(textBoxDDDZ.Text); }
            catch { steps = 1.0F; }
            serialPortCommandsZ(sender, e, steps, 1); // 1 = downwards 
        }


        private void buttonDDZ_Click(object sender, EventArgs e)
        {         
            float steps = 0.0F;
            try { steps = float.Parse(textBoxDDZ.Text); }
            catch { steps = 1.0F; }
            serialPortCommandsZ(sender, e, steps, 1); // 1 = downwards 
        }

        private void buttonDZ_Click(object sender, EventArgs e)
        {           
            float steps = 0.0F;
            try { steps = float.Parse(textBoxDZ.Text); }
            catch { steps = 1.0F; }
            serialPortCommandsZ(sender, e, steps, 1); // 1 = downwards 
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
            listBoxOutput.Items.Add(S + "\r\n");
        }
        
        private void UpdateTextbox(object obj)
        {
            if (InvokeRequired)
            {
                ObjectDelegate method = new ObjectDelegate(UpdateTextbox);
                Invoke(method, obj);
                return;
            }
            string S = (string)obj;

       //     MessageBox.Show(S);

            if (S.Length>0)
       textBoxReceive.AppendText(S + "\r\n");
        }           

        private void CommandForm_Load(object sender, EventArgs e)
        {
            // this was (bad) solution but works in +- 99 % of cases
            // and avoids working with ObjectDelegare for adding text
            // to listbox and textbox :
            //     CheckForIllegalCrossThreadCalls = false;  
        }
   
        
        private void buttonLeftUp_Click(object sender, EventArgs e)
        {            
            float xsteps = float.Parse(textBoxULX.Text);
            float ysteps = float.Parse(textBoxULY.Text);
            serialPortCommandsXY(sender, e, 5, xsteps, ysteps, false);
        }

        private void buttonRightUp_Click(object sender, EventArgs e)
        {           
            float xsteps = float.Parse(textBoxURX.Text);
            float ysteps = float.Parse(textBoxURY.Text);
            serialPortCommandsXY(sender, e, 3, xsteps, ysteps, false);
        }

        private void buttonRightDown_Click(object sender, EventArgs e)
        {
            float xsteps = float.Parse(textBoxDRX.Text);
            float ysteps = float.Parse(textBoxDRY.Text);
            serialPortCommandsXY(sender, e, 1, xsteps, ysteps, false);
        }

        private void buttonLeftDown_Click(object sender, EventArgs e)
        {
            float xsteps = float.Parse(textBoxDLX.Text);
            float ysteps = float.Parse(textBoxDLY.Text);
            serialPortCommandsXY(sender, e, 7, xsteps, ysteps, false);
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            float temp = 0.0F;
            MyXYZR.SetLaser(1); // OFF

            MyXYZR.CloseSerialPort();

            if (radioButtonPlot.Checked)
            {
             // XY speed
                try { XYZMainForm.plotXYTravelSpeed = int.Parse(comboBoxXYTravel.Text); }
                catch { XYZMainForm.plotXYTravelSpeed = 25;  }
                try { XYZMainForm.plotXYWorkSpeed = int.Parse(comboBoxXYWork.Text); }
                catch { XYZMainForm.plotXYWorkSpeed = 25; }

             // Z speed
                XYZMainForm.plotZUpSpeed = int.Parse(comboBoxZup.Text);
                XYZMainForm.plotZDownSpeed = int.Parse(comboBoxZdown.Text);
              
             // Position
                try { temp = float.Parse(textBoxZupPosition.Text); }
                catch { temp = 5.123F; }                             
                try { XYZMainForm.plotZUpPosition = temp; }
                catch { XYZMainForm.plotZUpPosition = 5.123F; }

                try { temp = float.Parse(textBoxZdownPosition.Text); }
                catch { temp = 10.123F; }
                try { XYZMainForm.plotZDownPosition = temp; }
                catch { XYZMainForm.plotZDownPosition = 10.123F; }
            }
           else if (radioButtonCut.Checked)
            {
                try { XYZMainForm.cutXYTravelSpeed = int.Parse(comboBoxXYTravel.Text); }
                catch { XYZMainForm.cutXYTravelSpeed = 25; }
                try { XYZMainForm.cutXYWorkSpeed = int.Parse(comboBoxXYWork.Text); }
                catch { XYZMainForm.cutXYWorkSpeed = 10; }
                
            // Z speed
                  XYZMainForm.cutZUpSpeed = int.Parse(comboBoxZup.Text);
                  XYZMainForm.cutZDownSpeed = int.Parse(comboBoxZdown.Text);

                XYZMainForm.cutZUpPosition = float.Parse(textBoxZupPosition.Text);
                XYZMainForm.cutZDownPosition = float.Parse(textBoxZdownPosition.Text);

            }
            else if (radioButtonDrill.Checked)
            {
                try { XYZMainForm.drillXYTravelSpeed = int.Parse(comboBoxXYTravel.Text); }
                catch { XYZMainForm.drillXYTravelSpeed = 25; }

                // NOT ! :
                /*
                try { XYZMainForm.drillXYWorkSpeed = int.Parse(comboBoxXYWork.Text); }
                catch { XYZMainForm.drillXYWorkSpeed = 50; }
                */
                
                try { XYZMainForm.drillZUpSpeed = int.Parse(comboBoxZup.Text); }
                catch { XYZMainForm.drillZUpSpeed = 50; }
                try { XYZMainForm.drillZDownSpeed = int.Parse(comboBoxZdown.Text); }
                catch { XYZMainForm.drillZDownSpeed = 5; }


                // Z speed

                XYZMainForm.drillZUpSpeed = int.Parse(comboBoxZup.Text);
                XYZMainForm.drillZDownSpeed = int.Parse(comboBoxZdown.Text);

                // position
               
                XYZMainForm.drillZUpPosition = float.Parse(textBoxZupPosition.Text);
                XYZMainForm.drillZDownPosition = float.Parse(textBoxZdownPosition.Text);
            }
            else if (radioButtonDisp.Checked)
            {
                try { XYZMainForm.dispXYTravelSpeed = int.Parse(comboBoxXYTravel.Text); }
                catch { XYZMainForm.dispXYTravelSpeed = 25; }
                try { XYZMainForm.dispXYWorkSpeed = int.Parse(comboBoxXYWork.Text); }
                catch { XYZMainForm.dispXYWorkSpeed = 5; }
              
             // Z speed
                XYZMainForm.dispZUpSpeed = int.Parse(comboBoxZup.Text);
                XYZMainForm.dispZDownSpeed = int.Parse(comboBoxZdown.Text);

              // Z position
                XYZMainForm.dispZUpPosition = float.Parse(textBoxZupPosition.Text);
                XYZMainForm.dispZDownPosition = float.Parse(textBoxZdownPosition.Text);
            }
            else if (radioButtonMill.Checked)
            {
                try { XYZMainForm.millXYTravelSpeed = int.Parse(comboBoxXYTravel.Text); }
                catch { XYZMainForm.millXYTravelSpeed = 25; }
                try { XYZMainForm.millXYWorkSpeed = int.Parse(comboBoxXYWork.Text); }
                catch { XYZMainForm.millXYWorkSpeed = 5; }
             
             // Z speed
                XYZMainForm.millZUpSpeed = int.Parse(comboBoxZup.Text);
                XYZMainForm.millZDownSpeed = int.Parse(comboBoxZdown.Text);

              // Z position
                XYZMainForm.millZUpPosition = float.Parse(textBoxZupPosition.Text);
                XYZMainForm.millZDownPosition = float.Parse(textBoxZdownPosition.Text);
            }
            this.Close();
        }
               
        private void buttonClearTX_Click(object sender, EventArgs e)
        {
            listBoxOutput.Items.Clear();
        }

        private void buttonClearRX_Click(object sender, EventArgs e)
        {
            textBoxReceive.Clear();
        }
        /*
        private void timer1_Tick(object sender, EventArgs e)
        {                                 
         
        }
        */

        private void buttonDispUp_Click_1(object sender, EventArgs e)
        {      
        }

        private void buttonDispDown_Click_1(object sender, EventArgs e)
        {
           
        }

        private void buttonMillUp_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonMillDown_Click(object sender, EventArgs e)
        {
           
        }

        private void radioButtonPlot_CheckedChanged(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = true;

            radioButtonPlot.ForeColor = Color.Black;
            radioButtonCut.ForeColor = Color.Black;
            radioButtonDrill.ForeColor = Color.Black;
            radioButtonDisp.ForeColor = Color.Black;
            radioButtonMill.ForeColor = Color.Black;

            radioButtonPlot.BackColor = Color.LightGray;
            radioButtonCut.BackColor = Color.LightGray;
            radioButtonDrill.BackColor = Color.LightGray;
            radioButtonDisp.BackColor = Color.LightGray;
            radioButtonMill.BackColor = Color.LightGray;

            labelXYWork.Visible = true;
            comboBoxXYWork.Visible = true;
            buttonSetXYWork.Visible = true;
            
         if (radioButtonPlot.Checked==true)
            {
                XYZMainForm.selectedCommandAction = 1;        
                radioButtonPlot.ForeColor = Color.Red;
                radioButtonPlot.BackColor = Color.Yellow;
                
                comboBoxXYTravel.Text = XYZMainForm.plotXYTravelSpeed.ToString();
                comboBoxXYWork.Text = XYZMainForm.plotXYWorkSpeed.ToString();                               

                comboBoxZup.Text = XYZMainForm.plotZUpSpeed.ToString();
                comboBoxZdown.Text = XYZMainForm.plotZDownSpeed.ToString();
                
    //     MessageBox.Show(comboBoxZup.Text + "      " + comboBoxZdown.Text);
                    
                textBoxZupPosition.Text = XYZMainForm.plotZUpPosition.ToString("##.###");
                textBoxZdownPosition.Text = XYZMainForm.plotZDownPosition.ToString("##.###");
            }
         else if (radioButtonCut.Checked==true)
            {
                XYZMainForm.selectedCommandAction = 2;
                radioButtonCut.ForeColor = Color.Red;
                radioButtonCut.BackColor = Color.Yellow;

                comboBoxXYTravel.Text = XYZMainForm.cutXYTravelSpeed.ToString();
                comboBoxXYWork.Text = XYZMainForm.cutXYWorkSpeed.ToString();
             
                comboBoxZup.Text = XYZMainForm.cutZUpSpeed.ToString();
                comboBoxZdown.Text = XYZMainForm.cutZDownSpeed.ToString("##.###");
                
                textBoxZupPosition.Text =  XYZMainForm.cutZUpPosition.ToString("##.###");
                textBoxZdownPosition.Text = XYZMainForm.cutZDownPosition.ToString("##.###");

            }
          else  if (radioButtonDrill.Checked==true)
            {

                labelXYWork.Visible = false;
                comboBoxXYWork.Visible =false;
                buttonSetXYWork.Visible =false;

                XYZMainForm.selectedCommandAction = 3;
                radioButtonDrill.ForeColor = Color.Red;
                radioButtonDrill.BackColor = Color.Yellow;
                comboBoxXYTravel.Text = XYZMainForm.drillXYTravelSpeed.ToString();
                
                comboBoxZup.Text = XYZMainForm.drillZUpSpeed.ToString();
                comboBoxZdown.Text = XYZMainForm.drillZDownSpeed.ToString();
                
                textBoxZupPosition.Text = XYZMainForm.drillZUpPosition.ToString("##.###");
                textBoxZdownPosition.Text = XYZMainForm.drillZDownPosition.ToString("##.###");
            }
           else if (radioButtonDisp.Checked==true)
            {
                XYZMainForm.selectedCommandAction = 4;
                radioButtonDisp.ForeColor = Color.Red;
                radioButtonDisp.BackColor = Color.Yellow;
                comboBoxXYTravel.Text = XYZMainForm.dispXYTravelSpeed.ToString();
                comboBoxXYWork.Text = XYZMainForm.dispXYWorkSpeed.ToString();
               
                comboBoxZup.Text = XYZMainForm.dispZUpSpeed.ToString();
                comboBoxZdown.Text = XYZMainForm.dispZDownSpeed.ToString();

                textBoxZupPosition.Text =  XYZMainForm.dispZUpPosition.ToString("##.###");
                textBoxZdownPosition.Text = XYZMainForm.dispZDownPosition.ToString("##.###");

            }
           else if (radioButtonMill.Checked==true)
            {
                XYZMainForm.selectedCommandAction = 5;
                radioButtonMill.ForeColor = Color.Red;
                radioButtonMill.BackColor = Color.Yellow;

                comboBoxXYTravel.Text = XYZMainForm.millXYTravelSpeed.ToString();
                comboBoxXYWork.Text = XYZMainForm.millXYWorkSpeed.ToString();
              
                comboBoxZup.Text = XYZMainForm.millZUpSpeed.ToString();
                comboBoxZdown.Text = XYZMainForm.millZDownSpeed.ToString();

                textBoxZupPosition.Text = XYZMainForm.millZUpPosition.ToString("##.###");
                textBoxZdownPosition.Text = XYZMainForm.millZDownPosition.ToString("##.###");
            }                  
        }

        private void buttonSetXYTravel_Click(object sender, EventArgs e)
        {
            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextbox);

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

            comboBoxXYTravel.ForeColor = Color.Red;
            comboBoxXYWork.ForeColor = Color.Black;

            if (tempdelay < 2) tempdelay = 2;   // maxmost speed is then 250mm /sec
            if (tempdelay > 150) tempdelay = 150; // 
            
            MyXYZR.SetXYSpeed(tempdelay);
            delListbox.Invoke(XYZMainForm.SerialPortSendS);
            
            while (MyXYZR.NextCommand == false) Application.DoEvents();
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);        
    }
     

        private void comboBoxXYTravel_Leave(object sender, EventArgs e)
        {
            int temp = 25;
            try { temp = int.Parse(comboBoxXYTravel.Text); }
            catch { temp = 25; }
            if (radioButtonPlot.Checked)  XYZMainForm.plotXYTravelSpeed = temp;
            else if (radioButtonCut.Checked) XYZMainForm.cutXYTravelSpeed = temp;
            else if (radioButtonDrill.Checked) XYZMainForm.drillXYTravelSpeed = temp;
            else if (radioButtonDisp.Checked) XYZMainForm.dispXYTravelSpeed = temp;
            else if (radioButtonMill.Checked) XYZMainForm.millXYTravelSpeed = temp;
        }

        private void comboBoxXYWork_Leave(object sender, EventArgs e)
        {
            int temp = 25;
            try { temp = int.Parse(comboBoxXYWork.Text); }
            catch { temp = 25; }
            if (radioButtonPlot.Checked) XYZMainForm.plotXYWorkSpeed = temp;
            else if (radioButtonCut.Checked) XYZMainForm.cutXYWorkSpeed = temp;
            // NOT:
         //   else if (radioButtonDrill.Checked) XYZMainForm.drillXYWorkSpeed = temp;
            else if (radioButtonDisp.Checked) XYZMainForm.dispXYWorkSpeed = temp;
            else if (radioButtonMill.Checked) XYZMainForm.millXYWorkSpeed = temp;
           
        }

        private void comboBoxZup_Leave(object sender, EventArgs e)
        {
            int temp = 5;
            try { temp = int.Parse(comboBoxZup.Text); }
            catch { temp = 5; }       
            if (radioButtonPlot.Checked) XYZMainForm.plotZUpSpeed = temp;       
            else if (radioButtonCut.Checked) XYZMainForm.cutZUpSpeed = temp;        
            else if (radioButtonDrill.Checked) XYZMainForm.drillZUpSpeed = temp;           
            else if (radioButtonDisp.Checked) XYZMainForm.dispZUpSpeed = temp;           
            else if (radioButtonMill.Checked) XYZMainForm.millZUpSpeed = temp;           
        }

        private void comboBoxZdown_Leave(object sender, EventArgs e)
        {
            int temp = 5;
            try    {  temp = int.Parse(comboBoxZdown.Text);  }
            catch  {  temp = 5;  }             
            if (radioButtonPlot.Checked) XYZMainForm.plotZDownSpeed = temp;          
            else if (radioButtonCut.Checked) XYZMainForm.cutZDownSpeed = temp;           
            else if (radioButtonDrill.Checked) XYZMainForm.drillZDownSpeed = temp;
            else if (radioButtonDisp.Checked) XYZMainForm.dispZDownSpeed = temp;            
            else if (radioButtonMill.Checked) XYZMainForm.millZDownSpeed = temp;          
        }

        private void textBoxZupPosition_Leave(object sender, EventArgs e)
        {
            float temp = 5.123F;
            try { temp = float.Parse(textBoxZupPosition.Text); }
            catch { temp = 5.123F; }     
           if (radioButtonPlot.Checked) XYZMainForm.plotZUpPosition = temp;
           else if (radioButtonCut.Checked) XYZMainForm.cutZUpPosition = temp;
           else if (radioButtonDrill.Checked) XYZMainForm.drillZUpPosition = temp;
           else if (radioButtonDisp.Checked) XYZMainForm.dispZUpPosition = temp;
           else if (radioButtonMill.Checked) XYZMainForm.millZUpPosition = temp;           
        }

        private void textBoxZdownPosition_Leave(object sender, EventArgs e)
        {
            float temp = 10.123F;
            try { temp = float.Parse(textBoxZdownPosition.Text); }
            catch { temp = 10.123F; }          
            if (radioButtonPlot.Checked) XYZMainForm.plotZDownPosition = temp;
            else if (radioButtonCut.Checked) XYZMainForm.cutZDownPosition = temp;
            else if (radioButtonDrill.Checked) XYZMainForm.drillZDownPosition = temp;
            else if (radioButtonDisp.Checked) XYZMainForm.dispZDownPosition = temp;
            else if (radioButtonMill.Checked) XYZMainForm.millZDownPosition = temp;
        }

        private void buttonSetXYWork_Click(object sender, EventArgs e)
        {          
            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextbox);
            XYZMainForm.SerialPortReturnS = "";
            XYZMainForm.SerialPortSendS = "";
            String S = comboBoxXYWork.Text;

            int tempDelay = 25;
            try
            {
              tempDelay = int.Parse(S);
            }
            catch
            {
               tempDelay = 25;
            }

            MyXYZR.SetXYSpeed(tempDelay);
            comboBoxXYTravel.ForeColor = Color.Black;
            comboBoxXYWork.ForeColor =   Color.Red;

            delListbox.Invoke(XYZMainForm.SerialPortSendS);
            while (MyXYZR.NextCommand == false) Application.DoEvents();
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
        }

        private void buttonSetZupSpeed_Click(object sender, EventArgs e)
        {          
            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextbox);

           XYZMainForm.SerialPortReturnS = "";
           XYZMainForm.SerialPortSendS = "";

            String S = comboBoxZup.Text;
            int delay = 25;
            try { delay = int.Parse(S); }
            catch { delay = 25; }
            MyXYZR.SetZSpeed(delay);   // calculateZDelay(comboBoxZup.Text));
           
            delListbox.Invoke(XYZMainForm.SerialPortSendS);
            while (MyXYZR.NextCommand == false) Application.DoEvents();
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
        }

        private void buttonSetZdownSpeed_Click(object sender, EventArgs e)
        {      
            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextbox);

            XYZMainForm.SerialPortReturnS = "";
            XYZMainForm.SerialPortSendS = "";

            String S = comboBoxZdown.Text;
            int delay = 25;
            try { delay = int.Parse(S); }
            catch { delay = 25; }
            MyXYZR.SetZSpeed(delay);   // calculateZDelay(comboBoxZup.Text));
            delListbox.Invoke(XYZMainForm.SerialPortSendS);
            while (MyXYZR.NextCommand == false) Application.DoEvents();
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
        }

        private void buttonSetZupPosition_Click(object sender, EventArgs e)
        { 
            float steps = 10.0F;
            try { steps = float.Parse(textBoxZupPosition.Text); }
            catch { steps = 10.0F; }
            //  steps is absolute position
            MyXYZR.ZAction(sender, e, steps);
            DisplayAbsoluteZCoordinates(sender, e);                         
        }

        private void buttonSetZdownPosition_Click(object sender, EventArgs e)
        {            
            float steps = 10.0F;
            try { steps = float.Parse(textBoxZdownPosition.Text); }
            catch { steps = 10.0F; }
            MyXYZR.ZAction(sender, e, steps);
            DisplayAbsoluteZCoordinates(sender, e);
        }
                  
        private void serialPortCommandsXY(object sender, EventArgs e, int direction,
         float stepsX, float stepsY, bool limit)
        {   
            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextbox);
            XYZMainForm.SerialPortReturnS = "";
            XYZMainForm.SerialPortSendS = "";

            if (checkBoxLaser.Checked==true) MyXYZR.SetLaser(0); // ON
                        
            MyXYZR.XYAction(sender, e, direction, stepsX, stepsY, limit);            
            DisplayXYCoordinates(sender, e);

            MyXYZR.SetLaser(1); // OFF
            delListbox.Invoke(XYZMainForm.SerialPortSendS);


            if (XYZMainForm.SerialPortReturnS.Contains("45")) MyXYZR.EscapeXY_Move(1, 600, 0); // left X closed
            else if (XYZMainForm.SerialPortReturnS.Contains("46")) MyXYZR.EscapeXY_Move(5, 600, 0); // right X closed

            else if (XYZMainForm.SerialPortReturnS.Contains("47")) MyXYZR.EscapeXY_Move(1,0, 600); // one of both back Y closed
            else if (XYZMainForm.SerialPortReturnS.Contains("48")) MyXYZR.EscapeXY_Move(1,0, 600); // one of both back Y closed


            else if (XYZMainForm.SerialPortReturnS.Contains("49")) MyXYZR.EscapeXY_Move(5,0, 600); // one of both Y closed

            delReceive.Invoke(XYZMainForm.SerialPortReturnS);

            //    while (MyXYZR.NextCommand == false) Application.DoEvents();





        }
        

        private void serialPortCommandsZ(object sender, EventArgs e, float stepsZ, int direction)
        {
            ObjectDelegate delListbox = new ObjectDelegate(UpdateListbox);
            ObjectDelegate delReceive = new ObjectDelegate(UpdateTextbox);
            XYZMainForm.SerialPortReturnS = "";
            XYZMainForm.SerialPortSendS = "";
            float steps = 0.0F;
            if (direction == 1)     // downwards
                steps = XYZMainForm.globalActualMmPosZ + stepsZ;
            else
                steps = XYZMainForm.globalActualMmPosZ - stepsZ;
            MyXYZR.ZAction(sender, e, steps);

            if (steps < 0) XYZMainForm.globalActualMmPosZ = 0; // after escape

            DisplayAbsoluteZCoordinates(sender, e);

            delListbox.Invoke(XYZMainForm.SerialPortSendS);
            while (MyXYZR.NextCommand == false) Application.DoEvents();
            delReceive.Invoke(XYZMainForm.SerialPortReturnS);
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

        private void buttonHome_Click(object sender, EventArgs e)
        {
            MyXYZR.SetLaser(1); // OFF
            // back to Origin at travel speed:                    
            MyXYZR.SetXYSpeed(int.Parse(comboBoxXYTravel.Text));
            MyXYZR.XYAction(sender, e, 5, XYZMainForm.globalActualMmPosX, XYZMainForm.globalActualMmPosY, false);
            DisplayXYCoordinates(sender, e);
        }
    }
}


    