using System;
using System.Xml;
using System.IO;
using System.Drawing;

namespace XYZ
{
    class Functions
    {     

        public static Color kleur(int BasisColor)
        {
            int rood = Color.FromArgb(BasisColor).R;
            int groen = Color.FromArgb(BasisColor).G;
            int blauw = Color.FromArgb(BasisColor).B;
            Color kleur = Color.FromArgb(255, rood, groen, blauw);
            return (kleur);
        }

        public static int intkleur(int BasisColor)
        {
            int rood = Color.FromArgb(BasisColor).R;
            int groen = Color.FromArgb(BasisColor).G;
            int blauw = Color.FromArgb(BasisColor).B;
            return (rood + (groen << 8) + (blauw << 16));
        }


        public static int XmlLoadSetup()
        {
            FileInfo fi = new FileInfo(XYZMainForm.InstallationDir + @"\XYZConfig.xml");   // 

            if (File.Exists(XYZMainForm.InstallationDir + @"\XYZConfig.xml") && (fi.Length > 10))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(XYZMainForm.InstallationDir + @"\XYZConfig.xml"); //
                XmlReaderSettings Xset = new XmlReaderSettings();
                Xset.ConformanceLevel = ConformanceLevel.Auto;
                Xset.IgnoreWhitespace = true;
                Xset.IgnoreComments = true;
                XmlNodeReader noderd = new XmlNodeReader(doc);
                XmlReader reader = XmlReader.Create(noderd, Xset);

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "Language")
                        {
                            reader.Read();
                            XYZMainForm.TAAL = int.Parse(reader.Value);
                        }

                        if (reader.Name == "GlobalMargeX")
                        {
                            reader.Read();
                            XYZMainForm.GlobalMargeX = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "GlobalMargeY")
                        {
                            reader.Read();
                            XYZMainForm.GlobalMargeY = Int32.Parse(reader.Value); // 20
                        }

                        if (reader.Name == "Comport")
                        {
                            reader.Read();
                            XYZMainForm.CMPRT = reader.Value;
                        }
                        if (reader.Name == "Baudrate")
                        {
                            reader.Read();
                            XYZMainForm.baudrate = int.Parse(reader.Value);
                        }


                        if (reader.Name == "IconColor")    // colour buttons
                        {
                            reader.Read();
                            XYZMainForm.IconColor = Int32.Parse(reader.Value);
                        }

                        if (reader.Name == "FondColor")      // colour display
                        {
                            reader.Read();
                            XYZMainForm.FondColor = Int32.Parse(reader.Value);
                        }

                        if (reader.Name == "FormColor")     // colour form
                        {
                            reader.Read();
                            XYZMainForm.FormColor = Int32.Parse(reader.Value);
                        }

                        // total dimensions groundplane :

                        if (reader.Name == "xShaftLength")
                        {
                            reader.Read();
                            XYZMainForm.MyCommonCoordinates.xShaftLength = int.Parse(reader.Value); // length of 2 X shafts
                        }
                        if (reader.Name == "yShaftLength")
                        {
                            reader.Read();
                            XYZMainForm.MyCommonCoordinates.yShaftLength = int.Parse(reader.Value); // length of 2 Y shafts
                        }

                        if (reader.Name == "xTotalGround")
                        {
                            reader.Read();
                           XYZMainForm.MyCommonCoordinates.xTotalGround = float.Parse(reader.Value); // X of total groundplane
                        }
                        if (reader.Name == "yTotalGround")
                        {
                            reader.Read();
                           XYZMainForm.MyCommonCoordinates.yTotalGround = float.Parse(reader.Value); // X of total groundplane
                        }
                        
                        if (reader.Name == "TheScaledOutput")    // A1..A5 + other
                        {
                            reader.Read();
                            XYZMainForm.TheScaledOutput = Int32.Parse(reader.Value); // 
                        }
                        if (reader.Name == "TheScaledCutterOutput")    // A1..A5 + other
                        {
                            reader.Read();
                            XYZMainForm.TheScaledCutterOutput = Int32.Parse(reader.Value); // 
                        }

                        if (reader.Name == "CustomDimX")    // A1..A5 + other
                        {
                            reader.Read();
                            XYZMainForm.CustomDimX = float.Parse(reader.Value); // 
                        }
                        if (reader.Name == "CustomDimY")    // A1..A5 + other
                        {
                            reader.Read();
                            XYZMainForm.CustomDimY = float.Parse(reader.Value); // 
                        }
                        if (reader.Name == "CustomCutterDimX")    // A1..A5 + other
                        {
                            reader.Read();
                            XYZMainForm.CustomCutterDimX = float.Parse(reader.Value); // 
                        }
                        if (reader.Name == "CustomCutterDimY")    // A1..A5 + other
                        {
                            reader.Read();
                            XYZMainForm.CustomCutterDimY = float.Parse(reader.Value); // 
                        }


                        if (reader.Name == "SelectedCommandAction")    // plot cut drill disp or mill
                        {
                            reader.Read();
                            XYZMainForm.selectedCommandAction = Int32.Parse(reader.Value); // 
                        }

                        // Plotter
                        if (reader.Name == "PlotXYTravelSpeed")
                        {
                            reader.Read();
                            XYZMainForm.plotXYTravelSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "PlotXYWorkSpeed")
                        {
                            reader.Read();
                            XYZMainForm.plotXYWorkSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "PlotZUpSpeed")
                        {
                            reader.Read();
                            XYZMainForm.plotZUpSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "PlotZDownSpeed")
                        {
                            reader.Read();
                            XYZMainForm.plotZDownSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "PlotZUpPosition")
                        {
                            reader.Read();
                            XYZMainForm.plotZUpPosition = float.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "PlotZDownPosition")
                        {
                            reader.Read();
                            XYZMainForm.plotZDownPosition = float.Parse(reader.Value); // 20
                        }
                        // Cutter
                        if (reader.Name == "CutXYTravelSpeed")
                        {
                            reader.Read();
                            XYZMainForm.cutXYTravelSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "CutXYWorkSpeed")
                        {
                            reader.Read();
                            XYZMainForm.cutXYWorkSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "CutZUpSpeed")
                        {
                            reader.Read();
                            XYZMainForm.cutZUpSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "CutZDownSpeed")
                        {
                            reader.Read();
                            XYZMainForm.cutZDownSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "CutZUpPosition")
                        {
                            reader.Read();
                            XYZMainForm.cutZUpPosition = float.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "CutZDownPosition")
                        {
                            reader.Read();
                            XYZMainForm.cutZDownPosition = float.Parse(reader.Value); // 20
                        }

                        // Drill
                        if (reader.Name == "DrillXYTravelSpeed")
                        {
                            reader.Read();
                            XYZMainForm.drillXYTravelSpeed = Int32.Parse(reader.Value); // 20
                        }
                        /*
                        if (reader.Name == "DrillXYWorkSpeed")
                        {
                            reader.Read();
                            XYZMainForm.drillXYWorkSpeed = Int32.Parse(reader.Value); // 20
                        }
                        */
                        if (reader.Name == "DrillZUpSpeed")
                        {
                            reader.Read();
                            XYZMainForm.drillZUpSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "DrillZDownSpeed")
                        {
                            reader.Read();
                            XYZMainForm.drillZDownSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "DrillZUpPosition")
                        {
                            reader.Read();
                            XYZMainForm.drillZUpPosition = float.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "DrillZDownPosition")
                        {
                            reader.Read();
                            XYZMainForm.drillZDownPosition = float.Parse(reader.Value); // 20
                        }

                        // Dispenser
                        if (reader.Name == "DispXYTravelSpeed")
                        {
                            reader.Read();
                            XYZMainForm.dispXYTravelSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "DispXYWorkSpeed")
                        {
                            reader.Read();
                            XYZMainForm.dispXYWorkSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "DispZUpSpeed")
                        {
                            reader.Read();
                            XYZMainForm.dispZUpSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "DispZDownSpeed")
                        {
                            reader.Read();
                            XYZMainForm.dispZDownSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "DispZUpPosition")
                        {
                            reader.Read();
                            XYZMainForm.dispZUpPosition = float.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "DispZDownPosition")
                        {
                            reader.Read();
                            XYZMainForm.dispZDownPosition = float.Parse(reader.Value); // 20
                        }

                        // Mill
                        if (reader.Name == "MillXYTravelSpeed")
                        {
                            reader.Read();
                            XYZMainForm.millXYTravelSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "MillXYWorkSpeed")
                        {
                            reader.Read();
                            XYZMainForm.millXYWorkSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "MillZUpSpeed")
                        {
                            reader.Read();
                            XYZMainForm.millZUpSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "MillZDownSpeed")
                        {
                            reader.Read();
                            XYZMainForm.millZDownSpeed = Int32.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "MillZUpPosition")
                        {
                            reader.Read();
                            XYZMainForm.millZUpPosition = float.Parse(reader.Value); // 20
                        }
                        if (reader.Name == "MillZDownPosition")
                        {
                            reader.Read();
                            XYZMainForm.millZDownPosition = float.Parse(reader.Value); // 20
                        }
                    }
                }
                reader.Close();
                return (1);
            }   // File exists
            else
            {
                //  MessageBox.Show( @"\SMT_Plotter_Config.xml" + " not found");    

                XYZMainForm.TAAL = 3;     // English
                XYZMainForm.CMPRT = "COM3";    // should exist  
                XYZMainForm.baudrate = 57600;
                                            
                XYZMainForm.GlobalMargeX = 0;  
                XYZMainForm.GlobalMargeY = 0; 

                XYZMainForm.IconColor= 14023167;
                XYZMainForm.FondColor = 16777200;
                XYZMainForm.FormColor = 16777200;


                    XYZMainForm.MyCommonCoordinates.xShaftLength = 500; // length of 2 X shafts in mm
               
                    XYZMainForm.MyCommonCoordinates.yShaftLength = 500; // length of 4 Y shafts in  mm
               
                    XYZMainForm.MyCommonCoordinates.xTotalGround = 500.0F ; // X of total groundplane
              
                    XYZMainForm.MyCommonCoordinates.yTotalGround = 500.0F; // X of total groundplane
              

                XYZMainForm.TheScaledOutput = 7;         //  A4
                XYZMainForm.TheScaledCutterOutput = 7;   //  A4

                XYZMainForm.CustomDimX = 210; // A4
                XYZMainForm.CustomDimY = 297; // A4
                XYZMainForm.CustomCutterDimX = 210; // A4
                XYZMainForm.CustomCutterDimY = 297; // A4



                XYZMainForm.plotXYTravelSpeed = 25;
                XYZMainForm.plotXYWorkSpeed = 15;
                XYZMainForm.plotZUpSpeed = 50;
                XYZMainForm.plotZDownSpeed = 50;
                XYZMainForm.plotZUpPosition = 5.123F;
                XYZMainForm.plotZDownPosition = 10.123F;

                XYZMainForm.cutXYTravelSpeed = 25;
                XYZMainForm.cutXYWorkSpeed = 15;
                XYZMainForm.cutZUpSpeed = 25;
                XYZMainForm.cutZDownSpeed = 25;
                XYZMainForm.cutZUpPosition = 5.123F;
                XYZMainForm.cutZDownPosition = 10.123F;

                XYZMainForm.dispXYTravelSpeed = 25;
                XYZMainForm.dispXYWorkSpeed = 15;
                XYZMainForm.dispZUpSpeed = 25;
                XYZMainForm.dispZDownSpeed = 5;
                XYZMainForm.dispZUpPosition = 5.123F;
                XYZMainForm.dispZDownPosition = 10.123F;

                XYZMainForm.drillXYTravelSpeed = 25;
                //XYZMainForm.drillXYWorkSpeed = 15;
                XYZMainForm.drillZUpSpeed = 25;
                XYZMainForm.drillZDownSpeed = 5;
                XYZMainForm.drillZUpPosition = 5.123F;
                XYZMainForm.drillZDownPosition = 10.123F;

                XYZMainForm.millXYTravelSpeed = 25;
                XYZMainForm.millXYWorkSpeed = 15;
                XYZMainForm.millZUpSpeed = 25;
                XYZMainForm.millZDownSpeed = 5;
                XYZMainForm.millZUpPosition = 5.123F;
                XYZMainForm.millZDownPosition = 10.123F;

                XYZMainForm.SignalColor = 255;
                XYZMainForm.ResolutionStepperXY = 200;
          
            }
            return (0);
        }        

    }   // Class Functions
    
}   
