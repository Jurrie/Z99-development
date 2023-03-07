using System;
using System.Drawing;
using System.Windows.Forms;

namespace XYZ
{
    public partial class XYZSetupForm : Form

    {
        public XYZSetupForm()
        {           
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(0, 0);
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(0, 0);
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            try
            {
                comboBoxLanguage.SelectedIndex = XYZMainForm.TAAL - 1;
            }
            catch
            {
                comboBoxLanguage.SelectedIndex = 2; // English
            }
            
            if (XYZMainForm.ResolutionStepperXY == 400)
            {
                radioButton400.ForeColor = Color.Red;
                radioButton400.Checked = true;
            }
            else
            {
                radioButton200.ForeColor = Color.Red;
               radioButton200.Checked = true;
            }
            
            comboBoxBaudrate.Text = XYZMainForm.baudrate.ToString();
            
            textBoxLengthShaftX.Text = XYZMainForm.MyCommonCoordinates.xShaftLength.ToString();
            textBoxLengthShaftY.Text = XYZMainForm.MyCommonCoordinates.yShaftLength.ToString();

            textBoxMargeX.Text = XYZMainForm.GlobalMargeX.ToString();
            textBoxMargeY.Text = XYZMainForm.GlobalMargeY.ToString();
                               
            Color buttonColor = Functions.kleur(XYZMainForm.IconColor);

            buttonButtonColor.BackColor = buttonColor; // ParFuncties.kleur(MainForm.IconColor);
            buttonQuit.BackColor = buttonColor; //  ParFuncties.kleur(MainForm.IconColor);
            buttonCancel.BackColor = buttonColor; //  ParFuncties.kleur(MainForm.IconColor);
            buttonFormColor.BackColor = Functions.kleur(XYZMainForm.FormColor);
            buttonFondColor.BackColor = Functions.kleur(XYZMainForm.FondColor); //  ParFuncties.kleur(MainForm.IconColor);
          
            this.BackColor = Functions.kleur(XYZMainForm.FormColor);
          
            groupBoxSteppersXY.Text = XYZMainForm.StrStepperResolution;
           
            int btM = 10;
            int btH = this.Height / 20;
            int btW = this.Width / 16;

            labelSetup.Left = this.Width / 4;
            labelSetup.Top = btH;

            tableLayoutPanelScreen.Left = btM;
            tableLayoutPanelScreen.Top = btH * 2 + btM * 2;
            tableLayoutPanelScreen.Width = btW * 15 / 4;
            tableLayoutPanelScreen.Height = btH * 9;

            tableLayoutPanelHardware.Left = tableLayoutPanelScreen.Left + tableLayoutPanelScreen.Width + btM;
            tableLayoutPanelHardware.Top = tableLayoutPanelScreen.Top;
            tableLayoutPanelHardware.Width = btW * 4;
            tableLayoutPanelHardware.Height = tableLayoutPanelScreen.Height;

            buttonCancel.Top = tableLayoutPanelScreen.Top + tableLayoutPanelScreen.Height + btM * 4;
            buttonCancel.Left = btM;
            buttonCancel.Width = btW *2;
            buttonCancel.Height = (btH *19)/10 - btM;

            buttonQuit.Top = buttonCancel.Top;
            buttonQuit.Left = buttonCancel.Left + buttonCancel.Width + 2;
            buttonQuit.Width = buttonCancel.Width;
            buttonQuit.Height = buttonCancel.Height;
        }

        private void SetupForm_Activated(object sender, EventArgs e)
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
          comboBoxComport.Items.Clear();
            foreach (string port in ports)
             comboBoxComport.Items.Add(port);
         comboBoxComport.Text = XYZMainForm.CMPRT;
            
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            try
            {
                XYZMainForm.CMPRT = comboBoxComport.Text;
            }
            catch
            {
                XYZMainForm.CMPRT = "Com3";
            }
            try
            {
                XYZMainForm.baudrate = int.Parse(comboBoxBaudrate.Text);
            }
            catch
            {
                XYZMainForm.baudrate  = 57600;
            }


            if (radioButton200.Checked == true) XYZMainForm.ResolutionStepperXY = 200;
            if (radioButton400.Checked == true) XYZMainForm.ResolutionStepperXY = 400;
            try
            {
                XYZMainForm.TAAL = comboBoxLanguage.SelectedIndex + 1;
            }
            catch
            {
                XYZMainForm.TAAL = 3;
            }


            if ((textBoxLengthShaftX.Text.Contains(".")) || (textBoxLengthShaftY.Text.Contains(".")))
            {
                MessageBox.Show("No decimal points are allowed in length of shafts. Enter in mm eg 500");
                return;
            }
            
            try
            {
                XYZMainForm.MyCommonCoordinates.xShaftLength = int.Parse(textBoxLengthShaftX.Text);
            }
            catch
            {
                XYZMainForm.MyCommonCoordinates.xShaftLength = 500;
            }
            try
            {
                XYZMainForm.MyCommonCoordinates.yShaftLength = int.Parse(textBoxLengthShaftY.Text);
            }
            catch
            {
                XYZMainForm.MyCommonCoordinates.yShaftLength = 500;
            }
                        
            XYZMainForm.GlobalMargeX = int.Parse(textBoxMargeX.Text);
            XYZMainForm.GlobalMargeY = int.Parse(textBoxMargeY.Text);
            this.Close();
        }

       
        
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
      
        private void PortComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
          comboBoxComport.Items.Clear();
            foreach (string port in ports)
            {
                if (port.Length > 4)
                {
                    if (Char.IsNumber(port, port.Length - 1) == true) comboBoxComport.Items.Add(port);
                    else comboBoxComport.Items.Add(port.Substring(0, port.Length - 1));          //         
                    //  MessageBox.Show(splits);
                }
                else comboBoxComport.Items.Add(port);
            }

        }

        private void buttonIconcolor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Color.FromArgb(XYZMainForm.IconColor);
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                int Rood = colorDialog1.Color.R;
                int Groen = colorDialog1.Color.G;
                int Blauw = colorDialog1.Color.B;
                XYZMainForm.IconColor = (Rood << 16) + (Groen << 8) + (Blauw << 0);
            }
            //    Done = 0;
            buttonButtonColor.BackColor = Functions.kleur(XYZMainForm.IconColor);
            SetColors(sender, e);
            this.Invalidate();
        }
        
        private void SetColors(object sender, EventArgs e)
        {
            buttonButtonColor.BackColor = Functions.kleur(XYZMainForm.IconColor);
            buttonQuit.BackColor = Functions.kleur(XYZMainForm.IconColor);
            buttonCancel.BackColor = Functions.kleur(XYZMainForm.IconColor);

            buttonFormColor.BackColor = Functions.kleur(XYZMainForm.FormColor);
            buttonFondColor.BackColor = Functions.kleur(XYZMainForm.FondColor);

           this.BackColor = Functions.kleur(XYZMainForm.FormColor);  
            
        }

        private void buttonFormColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Color.FromArgb(XYZMainForm.FormColor);
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                int Rood = colorDialog1.Color.R;
                int Groen = colorDialog1.Color.G;
                int Blauw = colorDialog1.Color.B;
                XYZMainForm.FormColor = (Rood << 16) + (Groen << 8) + (Blauw << 0);
            }          
            SetColors(sender, e);
            this.Invalidate();
        }

        private void buttonFondColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Color.FromArgb(XYZMainForm.FondColor);
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                int Rood = colorDialog1.Color.R;
                int Groen = colorDialog1.Color.G;
                int Blauw = colorDialog1.Color.B;
                XYZMainForm.FondColor = (Rood << 16) + (Groen << 8) + (Blauw << 0);
            }           
            SetColors(sender, e);
            this.Invalidate();
        }

    
        private void labelUSB_Click(object sender, EventArgs e)
        {
            MessageBox.Show(XYZMainForm.StrUSB);
        }

        private void radioButton200_CheckedChanged(object sender, EventArgs e)
        {
            radioButton200.ForeColor = Color.Black;
            radioButton400.ForeColor = Color.Black;

            if (radioButton400.Checked==true)
            {
                radioButton400.ForeColor = Color.Red;
                XYZMainForm.ResolutionStepperXY = 400;             
            }
            else
            {
                radioButton200.ForeColor = Color.Red;
                XYZMainForm.ResolutionStepperXY = 200;            
            }
        }


     
        private void labelHelpMargeX_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Enter left X marge in mm. This makes possible to use the Z99PP as Z99.In most cases 0 is ok.\n" +
                         "When nozzle holder of Z99PP is fixed on groundplane, enter eg 40 mm");
        }

        private void labelHelpMargeY_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Enter top Y marge in mm. This makes possible to use the Z99PP as Z99.In most cases 0 is ok.");
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {           

            try
            {
                XYZMainForm.TAAL = comboBoxLanguage.SelectedIndex + 1;
            }
            catch
            {
                XYZMainForm.TAAL = 3; // English
            }
            

            XYZLanguage.Translate();

            labelComport.Text = XYZMainForm.StrComport;
            
            buttonQuit.Text = XYZMainForm.StrSaveAndQuit;
            buttonCancel.Text = XYZMainForm.StrCancel;

            labelSetup.Text=   XYZMainForm.StrSettings;
            labelLengthShaftX.Text = XYZMainForm.StrLengthShaftX;
            labelLengthShaftY.Text = XYZMainForm.StrLengthShaftY;

            labelMargeX.Text = XYZMainForm.StrMargeX;
            labelMargeY.Text = XYZMainForm.StrMargeY;

            labelScreen.Text = XYZMainForm.StrScreen;
            labelComport.Text = XYZMainForm.StrComport;


            labelButtonColor.Text= XYZMainForm.StrColorButton;
            labelFormColor.Text = XYZMainForm.StrColorForm;
            labelFondColor.Text= XYZMainForm.StrColorFond;
                
            //  labelStepper.Text = MainForm.StrStepperResolution;
            groupBoxSteppersXY.Text = XYZMainForm.StrStepperResolution;
           
        }

        private void labelBaudrateInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Default is 57600\n"+"Use the same baudrate as programmed in PIC of X2Y4ZR PCB\n"+
                "Selecting wrong baudrate: no operation of hardware !");
        }
    }
    }
