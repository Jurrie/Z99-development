using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace XYZ
{
    public partial class XYZHelpForm : Form
    {
        public XYZHelpForm()
        {
            InitializeComponent();
            this.Top = 100;
            this.Left = 100;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width*2/5;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height*2/3;

            tableLayoutPanel1.Left = 10; // this.Left;
            tableLayoutPanel1.Top = 10;  // this.Top;
            tableLayoutPanel1.Width=this.Width - 20;
            tableLayoutPanel1.Height=this.Height - 60;
            

            this.BackColor = Functions.kleur(XYZMainForm.FormColor);
            Color tempcolor = Functions.kleur(XYZMainForm.IconColor);

            QuitButton.BackColor = tempcolor;
            ManualButton.BackColor = tempcolor;
            OnlineButton.BackColor = tempcolor;
            ContactButton.BackColor = tempcolor;

            if (XYZMainForm.TAAL == 1)
            {
                labelXYZversion.Text = "Versie 10.1 voor Z99.";
                labelRelease.Text = "Datum 24 01 2023";
                labelDocumentation.Text = "Handleiding en volledige documentatie in:";
                labelPrintedBy.Text = "De handleiding is gedrukt en wordt uitgegeven door:";
            }

            if (XYZMainForm.TAAL == 2)
            {
                labelXYZversion.Text = "Version 10.1 fur Z99.";
                labelRelease.Text = "Ausgabe Datum 24 01 2023";
                labelDocumentation.Text = "Handbuch und vollständige Dokumentation in:";
                labelPrintedBy.Text = "Das Handbuch wird gedruckt und verteilt von:";
            }

            if (XYZMainForm.TAAL == 3)
            {
                labelXYZversion.Text = "Version 10.1 for Z99.";
                labelRelease.Text = "Release Jan 24 2023";
                labelDocumentation.Text = "Full documentation and manual in:";
                labelPrintedBy.Text = "Manual is printed and sold by:";             
            }

            if (XYZMainForm.TAAL == 4)
            {
                labelXYZversion.Text = "Version 10.1 pour Z99.";
                labelRelease.Text = "Date 24 01 2023";
                labelDocumentation.Text = "Manuel et documentation complète dans:";
                labelPrintedBy.Text = "Le manuel est imprimé et distribué par:";
            }

            if (XYZMainForm.TAAL == 5)
            {
                labelXYZversion.Text = "Version 10.1 Z99.";
                labelRelease.Text = "Data 24 01 2023";
                labelDocumentation.Text = "Manual y documentación completa en:";
                labelPrintedBy.Text = "El manual está impreso y distribuido por:";
            }
            /*
            ManualButton.Text = MainForm.S847;
            OnlineButton.Text = MainForm.S848;
            ContactButton.Text = MainForm.S849;
            */
            QuitButton.Text = XYZMainForm.StrQuit;
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ManualButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"C:\Z99\Manual\Z99Manual.pdf")) Help.ShowHelp(XYZ.XYZMainForm.ActiveForm,
                @"C:\Z99\Manual\Z99Manual.pdf");             // provide the corect path !

            else
                MessageBox.Show(@"C:\Z99\Manual\Z99Manual.pdf"+" not found");
            /*
            switch (XYZMainForm.TAAL)
            {
                case 1:
                    try
                    {
                        if (File.Exists("AudioTestNL.pdf")) Help.ShowHelp(XYZ.XYZMainForm.ActiveForm,
                       "AudioTestNl.pdf");             // provide the corect path !

                        else
                            MessageBox.Show("AudioTestNL.pdf not found");
                    }
                    catch
                    {
                        MessageBox.Show("AudioTestNl.pdf not found");
                    }
                    break;
        
                    // impliment later
                    // 
                    // case 2 ..5 


                default:

                    try
                    {
                        if (File.Exists("AudioTestEn.pdf"))

                            Help.ShowHelp(XYZ.XYZMainForm.ActiveForm,
                                  "AudioEn.pdf");             // provide the corect path !

                        else
                            MessageBox.Show("AudioTestEn.pdf not found");
                    }
                    catch
                    {
                        MessageBox.Show("AudioTestEn.pdf not found");
                    }

                    break;                    
            }

            */

        }

       

        private void ContactButton_Click(object sender, EventArgs e)
        {
            /*
            Help.ShowHelp(XYZ.XYZMainForm.ActiveForm,
                           "https://www.ganseman.com/Contact.aspx");                        
            */
            MessageBox.Show("mailto: info@ganseman.com");
        }
              

        private void OnlineButton_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(XYZ.XYZMainForm.ActiveForm,
               "https://www.ganseman.com/Manuals/Z99Manual.pdf");

            /*
            switch (XYZMainForm.TAAL)
            {
                case 1:
                    Help.ShowHelp(XYZ.XYZMainForm.ActiveForm,
                 "https://www.ganseman.com/Manuals/AudioTestNl.pdf");
                    break;
                case 2:
                    Help.ShowHelp(XYZ.XYZMainForm.ActiveForm,
                 "https://www.ganseman.com/Manuals/AudioTestEn.pdf");
                    break;
                case 3:
                    Help.ShowHelp(XYZ.XYZMainForm.ActiveForm,
                 "https://www.ganseman.com/Manuals/AudioTestEn.pdf");
                    break;
                case 4:
                    Help.ShowHelp(XYZ.XYZMainForm.ActiveForm,
                  "https://www.ganseman.com/Manuals/AudioTestFr.pdf");

                    break;
                default: break;
            }
            */

        }


       




    }
}


