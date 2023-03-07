using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Resources;

namespace XYZ
{

    public struct CommonCoordinates   
    {
        public int xShaftLength;    //  length linear X bars in mm
        public int yShaftLength;    //  length linear Y bars in mm

        public float xTotalGround; // total x dimension of groundplane workspace in mm 
        public float yTotalGround; // total y dimension of groundplane workspace in mm   
    }


    public partial class XYZMainForm : Form
    {
       
        public static CommonCoordinates MyCommonCoordinates = new CommonCoordinates();
                        
        public static int GlobalMargeX;  // left  X marge in mm, mostly 0, eg 40mm when nozzle holder is fixed Z99PP
        public static int GlobalMargeY;  // top   Y marge in mm, mostly 0
        
        public static string CMPRT = "COM3",                            // should exist
              InstallationDir = Directory.GetCurrentDirectory();     

        public static int baudrate = 57600;
       
        public static int ResolutionStepperXY = 200;  //  maybe later also  400
        
        public static int TAAL = 3;    //  3 = English   
        public static int SignalColor=255;
      
        public static int selectedCommandAction = 1;  // 1=plot, 2=cut, 3=drill,4=disp and 5 =mill                                                 
        
        public static int plotXYTravelSpeed = 50;
        public static int plotXYWorkSpeed = 50;
        public static int plotZUpSpeed = 10;
        public static int plotZDownSpeed = 10;
        public static float plotZUpPosition = 5.0F;  //
        public static float plotZDownPosition = 12.0F; //

        public static int cutXYTravelSpeed = 50;
        public static int cutXYWorkSpeed = 50;
        public static int cutZUpSpeed = 50;        
        public static int cutZDownSpeed = 50; 
        public static float cutZUpPosition = 10.0F;
        public static float cutZDownPosition = 15.0F;

        public static int drillXYTravelSpeed = 50;
     //   public static int drillXYWorkSpeed = 50;   not possible with drill down
        public static int drillZUpSpeed = 50;
        public static int drillZDownSpeed = 50;
        public static float drillZUpPosition = 10.0F;
        public static float drillZDownPosition = 15.0F;

        public static int dispXYTravelSpeed = 50;
        public static int dispXYWorkSpeed = 50;
        public static int dispZUpSpeed = 50;
        public static int dispZDownSpeed = 50; 
        public static float dispZUpPosition = 10.0F;
        public static float dispZDownPosition = 15.0F;

        public static int millXYTravelSpeed = 50;
        public static int millXYWorkSpeed = 50;
        public static int millZUpSpeed = 50;
        public static int millZDownSpeed = 50;     
        public static float millZUpPosition = 10.0F;
        public static float millZDownPosition = 15.0F;      

        public static int IconColor = 14023167;    // colour for buttons
        public static int FondColor = 16777200; // 255 + 255 << 16 + 255 << 8; // background signal
        public static int FormColor = 16777200; //200 + 200 << 16 + 200 << 8; // 16777200;

        public static string SerialPortReturnS = "";
        public static string SerialPortSendS = "";
        // reading of analog value, not important for Z99
       public static bool FlagString = false;
       public static bool FlagPressure = false;        
       
        // global coordinates available in all modules:
        public static float globalActualMmPosZ = 0.0F;
        public static float globalActualMmPosX = 0.0F;
        public static float globalActualMmPosY = 0.0F;
            
        public static String StrFile = "File",
            StrOpenFile = "Open HPGL file",
            StrOpenGCODEFile="Open GCODE file",
            StrOpenDrillFile = "Open drill (*.drl) file",
            StrSaveFile = "Save HPGL file",
            StrLoadModel = "Load model image",
            StrClearModel = "Clear model image",
            StrClearListbox = "Clear Listbox",
            StrClearDrawing = "Clear drawing",
            StrClearAll = "Clear all",
            StrMirror = "Mirror",
            StrDrawing = "Drawing",
            StrUndo = "Undo",
            StrUndoSteps = "Undo steps",
            StrColor = "Color",
            StrThickness = "Thickness",

            StrDraw = "Draw",
            StrFoot = "Turn above checkbox 'Drawing' on.   Draw freehand while left mousebutton is pressed",
            StrCommand = "Command",
            StrMenu = "Menu",
            StrDelay = "delay",
            StrReset = "Reset",
            StrZSteps = "Z steps",
            StrUp = "Up",
            StrDown = "Down",
            StrCut = "Cutter",
            StrDrill="Drill",            

          StrScreen = "Screen",
          StrView = "View",
          StrPrint = "Print",
          StrDelete = "Delete",
          StrQuit = "Quit",
          StrHelp = "Help",
          StrSave = "Save",
          StrSaveNew = "Save as new",
          StrSetup = "Setup",

            StrStepperResolution = "Stepper motor resolution",
            StrColorButton = "Color button",
            StrColorForm = "Color form",
            StrColorFond = "Color fond",

          StrManual = "Manual",
          StrOnlineManual = "Online manual",
          StrContactUs = "Contact us",
          StrVersion = "Version 1.1  31/01/2018",
          StrAutor = "Auteur",
          StrStart = "Start",
          StrIconColor = "Icon color",       // = also for button color
          StrBackColor = "Backcolor",
          StrComport = "Comport",
          StrClear = "Clear",
          StrPrevious = "Previous",
          StrCancel = "Cancel",
          StrDefault = "Default",
          StrNoSerialPorts = "No serial port founds",
          StrNotFound = "not found",
          StrAttention = "Attention",
          StrSelection = "Selection",
          StrSaveAndQuit = "Save and quit",
          StrStartPlot = "Start plot",
          StrStartCut = "Start cutter",
          StrStartDrill = "Start drilling",
          StrStartDispense = "Start dispenser",
          StrStartMill="Start milling",
        StrReopen = "Reopen",
          StrPause = "Pause",
          StrDimension = "Dimension",
         StrLaunch = "Launch",

            StrLaunchFileViewer = "Launch file viewer",
            StrImport = "Import from",

            StrUSB = "Please, do not enter, type the comport here, but click on the downwards\n" +
                      "oriented  ^  sign and select the correct comport from the dropdown list.\n" +
                     "\n" +
               "If no comport is in the dropdownlist, the apparatus is not connected\n" +
               "or the driver is not yet installed.\n" +
                "\n" +
               "If the correct comportnumber is not known yet,\n" +
               "click twice on the downwards oriented  ^  sign :\n" +
               "- once without the apparatus connected to USB\n" +
               "- once with the apparatus connected to the USB port\n" +
               "the comport that appears when the apparatus is connected, is the correct choice\n",

        StrBT = "Selection of the Bluetooth outgoing comport\n" +
                "Do not enter, type the comport, but click on the downwards\n" +
                "oriented  ^  sign and select the correct outgoing comport from the dropdown list.\n" +
               "\n" +
              "If no comport is in the dropdownlist, verify that the apparatus is ON\n" +
              "and paired.\n" +
               "\n" +
              "Correct outgoing BT comportnumber is not known yet:\n" +
              "'Select show Bluetooth devices' -> 'More bluetooth options'\n" +
              " to see the correct outgoing comportnumber\n" +
              " select this COMx nr in the dropdown list.\n",
            StrHPGLInputProperties = "HPGL input properties",
            StrScreenCoordinates = "Screen coordinates, drawing made with this program",
            StrIndependent = "The plot dimensions are not dependent on the paper size",
            StrDependent = "The plot dimensions depend on the paper size",
            StrDoNotScale = "Do not scale to output (PCB)",
            StrDoScale = "Scale to output paper or vinyl",
            StrPrecision = "Precision",

            StrScrCoord = "Screen coordinates",
            StrSetOther = "Set input or output format",
            StrScaled = "Scaled output",                       // ---

            StrSelectPad = "Select a pad:",
            StrSetProperties= "Set properties:",
            StrDotDistance="Dot distance",
            StrDotExtrusion="Dot extrusion",
            StrSet="Set",
            StrFileContent="File content:",
            StrSettings="Settings",
            StrLengthShaftY = "Length Y shaft",
            StrLengthShaftX = "Length X shaft",
            StrMargeX = "Left marge X mm",
            StrMargeY = "Top marge Y mm";


     public static string GlobalHPGLFileName = "";  // loaded or saved filename in drawmodule will also ne used for plotter and cutter
        
     public static int TheScaledOutput = 4;       //  1=A1    2=A2    3=A3       4=A4     5=A5   6=Other
     public static int TheScaledCutterOutput = 4;       //  1=A1    2=A2    3=A3       4=A4     5=A5   6=Other
               
        public static float PaperDimX = 160.00F;  //  X dimension of plot in mm
        public static float PaperDimY = 100.00F;  //  Y dimension of plot  in mm

        public static float CustomDimX = 160.00F;  //  X when  TheScaledOutput = 6 = Other dimension
        public static float CustomDimY = 100.00F;  //  Y when TheScaledOutput = 6 = Other dimension       

        public static float CustomCutterDimX = 160.00F;  // X when  TheScaledOutput = 6 = Other dimension
        public static float CustomCutterDimY = 100.00F;  // Y when TheScaledOutput = 6 = Other dimension       
            
        public XYZMainForm()
        {
            InitializeComponent();                  
            this.Left = 0;
            this.Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Location = new System.Drawing.Point(5, 5);
        
            InstallationDir = Directory.GetCurrentDirectory();

            // TEMP
            if (Functions.XmlLoadSetup() == 0)                
                    MessageBox.Show(InstallationDir + @"\XYZConfig.xml" + " not found");


            XYZLanguage.Translate();  //  language settings

            Color ButtonColor = Functions.kleur(IconColor);            
            this.BackColor = Functions.kleur(FormColor);
            buttonCommand.BackColor = ButtonColor;
            buttonDraw.BackColor = ButtonColor;
            buttonCut.BackColor = ButtonColor;
            buttonPlot.BackColor = ButtonColor;
            buttonDrill.BackColor = ButtonColor;
            buttonDispense.BackColor = ButtonColor;
            buttonMill.BackColor = ButtonColor;
            buttonSetup.BackColor = ButtonColor;
            buttonHelp.BackColor = ButtonColor;
            buttonQuit.BackColor = ButtonColor;
            // text          
            buttonCommand.Text = StrCommand;
            buttonDraw.Text = StrDraw;
            buttonCut.Text = StrCut;
            buttonDrill.Text = StrDrill;
            buttonSetup.Text = StrSetup;
            buttonHelp.Text = StrHelp;
            buttonQuit.Text = StrQuit;
        }


        private void XYZMainForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonDispense_Click(object sender, EventArgs e)
        {
            Form dispensefrm = new DispenseForm();
            dispensefrm.Show();
        }


        private void buttonCommand_Click(object sender, EventArgs e)
        {
            XYZCommandForm cmdForm = new XYZCommandForm();
            cmdForm.Show();

            while (cmdForm.Visible == true) Application.DoEvents();
            SaveSettings(sender, e);
        }
        
        private void buttonDraw_Click(object sender, EventArgs e)
        {
            Form drfrm = new DrawForm();
            drfrm.Show();
        }
        
        private void buttonComponentPlacing_Click(object sender, EventArgs e) // plot
        {
            PlotForm pltfrm = new PlotForm();
            pltfrm.Show();
        }
        
        private void buttonCut_Click(object sender, EventArgs e)
        {
            Form ctrfrm = new CutForm(); // new CutterForm();
            ctrfrm.Show();
        }

        private void buttonDrill_Click(object sender, EventArgs e)
        {
            Form drillfrm = new DrillForm();
            drillfrm.Show();
        }

        private void buttonMill_Click(object sender, EventArgs e)
        {
            Form millfrm = new MillForm();
            millfrm.Show();
        }

        private void SetupButton_Click(object sender, EventArgs e)
        {
            XYZSetupForm frms = new XYZSetupForm();
            frms.Show();                      
            while (frms.Visible == true) Application.DoEvents();           

            Color buttonColor = Functions.kleur(IconColor);
            this.BackColor = Functions.kleur(FormColor);
            buttonCommand.BackColor = buttonColor;
            buttonDraw.BackColor = buttonColor;
            buttonCut.BackColor = buttonColor;
            buttonPlot.BackColor = buttonColor;
            buttonDrill.BackColor = buttonColor;
            buttonDispense.BackColor = buttonColor;
            buttonMill.BackColor = buttonColor;
            buttonSetup.BackColor = buttonColor;
            buttonHelp.BackColor = buttonColor;
            buttonQuit.BackColor = buttonColor;

            buttonCommand.Text = StrCommand;
            buttonDraw.Text = StrDraw;
            buttonCut.Text = StrCut;
            buttonDrill.Text = StrDrill;                     
            buttonSetup.Text = StrSetup;
            buttonHelp.Text = StrHelp;
            buttonQuit.Text = StrQuit;
            
            SaveSettings(sender, e);                        
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            XYZHelpForm hlpForm = new XYZHelpForm();
            hlpForm.Show();
        }                

        private void QuitButton_Click(object sender, EventArgs e)
        {
            SaveSettings(sender, e);                                     
            this.Close();
        }
        
        public void SaveSettings(object sender, EventArgs e)
        {    

            using (StreamWriter st = new StreamWriter(InstallationDir + @"\XYZConfig.xml"))
            {
                //     st.WriteLine("<?xml version={0}1.0{1} encoding={2}utf-8{3}?>",",",",");
                st.WriteLine("<!--COMMENT HERE-->");
                st.WriteLine("<Settings>");
                st.WriteLine("<Software>");
                st.WriteLine("<Language>{0}</Language>", TAAL);
                st.WriteLine("<InstallationDir>{0}</InstallationDir>", Directory.GetCurrentDirectory());

                      
                st.WriteLine("<GlobalMargeX>{0}</GlobalMargeX>", GlobalMargeX);
                st.WriteLine("<GlobalMargeY>{0}</GlobalMargeY>", GlobalMargeY);
               
                st.WriteLine("<IconColor>{0}</IconColor>", IconColor);
                st.WriteLine("<FormColor>{0}</FormColor>", FormColor);
                st.WriteLine("<FondColor>{0}</FondColor>", FondColor);
                           
                st.WriteLine("<Language>{0}</Language>", TAAL);

                //   plot cut drill disp or mill
                st.WriteLine("<SelectedCommandAction>{0}</SelectedCommandAction>", selectedCommandAction);
                
                st.WriteLine("</Software>");

               st.WriteLine("<Output>");   // A1 to A5, inch or mm, scaled or not
        
               st.WriteLine("<TheScaledOutput>{0}</TheScaledOutput>", TheScaledOutput);     //  A1..5 + other
               st.WriteLine("<CustomDimX>{0}</CustomDimX>", CustomDimX);
               st.WriteLine("<CustomDimY>{0}</CustomDimY>", CustomDimY);
             st.WriteLine("<TheScaledCutterOutput>{0}</TheScaledCutterOutput>", TheScaledCutterOutput);     //  A1..5 + other
                st.WriteLine("<CustomCutterDimX>{0}</CustomCutterDimX>", CustomCutterDimX);
                st.WriteLine("<CustomCutterDimY>{0}</CustomCutterDimY>", CustomCutterDimY);
              st.WriteLine("</Output>");                          

                st.WriteLine("<Hardware>");
                st.WriteLine("<Comport>{0}</Comport>", CMPRT);
                st.WriteLine("<Baudrate>{0}</Baudrate>", baudrate);

                st.WriteLine("<xShaftLength>{0}</xShaftLength>", MyCommonCoordinates.xShaftLength);
                st.WriteLine("<yShaftLength>{0}</yShaftLength>", MyCommonCoordinates.yShaftLength);

                st.WriteLine("<xTotalGround>{0}</xTotalGround>", MyCommonCoordinates.xTotalGround);
                st.WriteLine("<yTotalGround>{0}</yTotalGround>", MyCommonCoordinates.yTotalGround);
                 
                st.WriteLine("</Hardware>");
                
                st.WriteLine("<Plotter>");
                   st.WriteLine("<PlotXYTravelSpeed>{0}</PlotXYTravelSpeed>",plotXYTravelSpeed);
                   st.WriteLine("<PlotXYWorkSpeed>{0}</PlotXYWorkSpeed>", plotXYWorkSpeed);
                   st.WriteLine("<PlotZUpSpeed>{0}</PlotZUpSpeed>", plotZUpSpeed);
                   st.WriteLine("<PlotZDownSpeed>{0}</PlotZDownSpeed>", plotZDownSpeed);
                   st.WriteLine("<PlotZUpPosition>{0}</PlotZUpPosition>", plotZUpPosition);
                   st.WriteLine("<PlotZDownPosition>{0}</PlotZDownPosition>", plotZDownPosition);
                st.WriteLine("</Plotter>");

                st.WriteLine("<Cutter>");
                st.WriteLine("<CutXYTravelSpeed>{0}</CutXYTravelSpeed>", cutXYTravelSpeed);
                st.WriteLine("<CutXYWorkSpeed>{0}</CutXYWorkSpeed>", cutXYWorkSpeed);
                st.WriteLine("<CutZUpSpeed>{0}</CutZUpSpeed>", cutZUpSpeed);
                st.WriteLine("<CutZDownSpeed>{0}</CutZDownSpeed>", cutZDownSpeed);
                st.WriteLine("<CutZUpPosition>{0}</CutZUpPosition>", cutZUpPosition);
                st.WriteLine("<CutZDownPosition>{0}</CutZDownPosition>", cutZDownPosition);
                st.WriteLine("</Cutter>");

                st.WriteLine("<Drill>");
                st.WriteLine("<DrillXYTravelSpeed>{0}</DrillXYTravelSpeed>", drillXYTravelSpeed);
             //   st.WriteLine("<DrillXYWorkSpeed>{0}</DrillXYWorkSpeed>", drillXYWorkSpeed);
                st.WriteLine("<DrillZUpSpeed>{0}</DrillZUpSpeed>", drillZUpSpeed);
                st.WriteLine("<DrillZDownSpeed>{0}</DrillZDownSpeed>", drillZDownSpeed);
                st.WriteLine("<DrillZUpPosition>{0}</DrillZUpPosition>", drillZUpPosition);
                st.WriteLine("<DrillZDownPosition>{0}</DrillZDownPosition>", drillZDownPosition);
                st.WriteLine("</Drill>");

                st.WriteLine("<Dispenser>");
                st.WriteLine("<DispXYTravelSpeed>{0}</DispXYTravelSpeed>",dispXYTravelSpeed);
                st.WriteLine("<DispXYWorkSpeed>{0}</DispXYWorkSpeed>", dispXYWorkSpeed);
                st.WriteLine("<DispZUpSpeed>{0}</DispZUpSpeed>", dispZUpSpeed);
                st.WriteLine("<DispZDownSpeed>{0}</DispZDownSpeed>", dispZDownSpeed);
                st.WriteLine("<DispZUpPosition>{0}</DispZUpPosition>", dispZUpPosition);
                st.WriteLine("<DispZDownPosition>{0}</DispZDownPosition>", dispZDownPosition);
                st.WriteLine("</Dispenser>");

                st.WriteLine("<Mill>");
                st.WriteLine("<MillXYTravelSpeed>{0}</MillXYTravelSpeed>",millXYTravelSpeed);
                st.WriteLine("<MillXYWorkSpeed>{0}</MillXYWorkSpeed>", millXYWorkSpeed);
                st.WriteLine("<MillZUpSpeed>{0}</MillZUpSpeed>", millZUpSpeed);
                st.WriteLine("<MillZDownSpeed>{0}</MillZDownSpeed>", millZDownSpeed);
                st.WriteLine("<MillZUpPosition>{0}</MillZUpPosition>", millZUpPosition);
                st.WriteLine("<MillZDownPosition>{0}</MillZDownPosition>", millZDownPosition);
                st.WriteLine("</Mill>");                

                st.WriteLine("</Settings>");               
                st.Flush();
                st.Close();
            }
        }    

    }
}

