
using System;
using System.Windows.Forms;   // allows MessageBox in class
using System.Runtime.InteropServices;


namespace XYZ
{
    // X Y Z and R movements  used by all modules with serialport, comport communication
    // The 2 parts in the partial class are in
    // XYZ1.cs
    // XYZ2.cs
      
    public partial class XYZR
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
               
                
      public void XYAction(object sender, EventArgs e, int direction, float xMm,float yMm,bool origin)
        {                                
         XYZMainForm.SerialPortReturnS = "";
         XYZMainForm.SerialPortSendS = "";

          if (origin == false)  // default
            {
                switch (direction)
                {
                    case 1:            // to right and downwards 
                        if ((XYZMainForm.globalActualMmPosX + Math.Abs(xMm)) <= XYZMainForm.MyCommonCoordinates.xTotalGround)
                            XYZMainForm.globalActualMmPosX = XYZMainForm.globalActualMmPosX + Math.Abs(xMm);
                        else
                        {
                       //     MessageBox.Show("X axis limit was exceeded !");
                            XYZMainForm.globalActualMmPosX = XYZMainForm.MyCommonCoordinates.xTotalGround;
                        }
                        if ((XYZMainForm.globalActualMmPosY + Math.Abs(yMm)) <= XYZMainForm.MyCommonCoordinates.yTotalGround)
                            XYZMainForm.globalActualMmPosY = XYZMainForm.globalActualMmPosY + Math.Abs(yMm);
                        else
                        {
                     //       MessageBox.Show("Y axis limit was exceeded !");
                            XYZMainForm.globalActualMmPosY = XYZMainForm.MyCommonCoordinates.yTotalGround;
                        }
                        break;
                    case 3:
                        if ((XYZMainForm.globalActualMmPosX + Math.Abs(xMm)) <= XYZMainForm.MyCommonCoordinates.xTotalGround)
                            XYZMainForm.globalActualMmPosX = XYZMainForm.globalActualMmPosX + Math.Abs(xMm);
                        else
                        {
                       //     MessageBox.Show("X axis limit was exceeded !");
                            XYZMainForm.globalActualMmPosX = XYZMainForm.MyCommonCoordinates.xTotalGround;
                        }
                        if ((XYZMainForm.globalActualMmPosY - Math.Abs(yMm)) < 0)
                        {
                        //    MessageBox.Show("Y axis limit was exceeded !");
                            XYZMainForm.globalActualMmPosY = 0;
                        }
                        else
                        {                           
                            XYZMainForm.globalActualMmPosY = XYZMainForm.globalActualMmPosY - Math.Abs(yMm);
                        }
                        break;
                    case 5:
                        if ((XYZMainForm.globalActualMmPosX - Math.Abs(xMm)) < 0)
                        {
                      //      MessageBox.Show("X axis limit was exceeded !");
                            XYZMainForm.globalActualMmPosX = 0;
                        }
                        else
                        {                          
                            XYZMainForm.globalActualMmPosX = XYZMainForm.globalActualMmPosX - Math.Abs(xMm);
                        }

                        if ((XYZMainForm.globalActualMmPosY - Math.Abs(yMm)) < 0)
                        {
                      //      MessageBox.Show("Y axis limit was exceeded !");
                            XYZMainForm.globalActualMmPosY = 0;
                        }
                        else
                        {
                            XYZMainForm.globalActualMmPosY = XYZMainForm.globalActualMmPosY - Math.Abs(yMm);                       
                        }                       
                        break;
                    case 7:
                        if ((XYZMainForm.globalActualMmPosX - Math.Abs(xMm)) < 0)
                        {
                     //       MessageBox.Show("X axis limit was exceeded !");
                            XYZMainForm.globalActualMmPosX = 0;
                        }
                        else
                        {
                            XYZMainForm.globalActualMmPosX = XYZMainForm.globalActualMmPosX - Math.Abs(xMm);
                        }

                        if ((XYZMainForm.globalActualMmPosY + Math.Abs(yMm)) <= XYZMainForm.MyCommonCoordinates.yTotalGround)
                            XYZMainForm.globalActualMmPosY = XYZMainForm.globalActualMmPosY + Math.Abs(yMm);
                        else
                        {
                    //        MessageBox.Show("Y axis limit was exceeded !");
                            XYZMainForm.globalActualMmPosY = XYZMainForm.MyCommonCoordinates.yTotalGround;
                        }
                        break;
                    default: break;
                }
            }
            else if (origin==true)   // for origin setting is  overflow, negative coordinates  allowed
            {
                switch (direction)
                {
                    case 1:            // to right and downwards 
                         XYZMainForm.globalActualMmPosX  = XYZMainForm.globalActualMmPosX + Math.Abs(xMm);
                         XYZMainForm.globalActualMmPosY  = XYZMainForm.globalActualMmPosY + Math.Abs(yMm);                        
                        break;
                    case 3:                 
                        XYZMainForm.globalActualMmPosX = XYZMainForm.globalActualMmPosX + Math.Abs(xMm);
                        XYZMainForm.globalActualMmPosY = XYZMainForm.globalActualMmPosY - Math.Abs(yMm);
                        break;
                    case 5:                                  
                        XYZMainForm.globalActualMmPosX = XYZMainForm.globalActualMmPosX - Math.Abs(xMm);
                        XYZMainForm.globalActualMmPosY = XYZMainForm.globalActualMmPosY - Math.Abs(yMm);
                        break;
                    case 7:           
                        XYZMainForm.globalActualMmPosX = XYZMainForm.globalActualMmPosX - Math.Abs(xMm);
                        XYZMainForm.globalActualMmPosY = XYZMainForm.globalActualMmPosY + Math.Abs(yMm);
                        break;
                    default: break;
                }
            }

          /*
            MessageBox.Show("dir " + direction.ToString() + "xMm " + xMm.ToString("##.##") +
                 " yMm " + yMm.ToString("##.##") + " limit " + origin.ToString());
*/
                                               
            XY_Move(direction,(int)Math.Abs(xMm*200), (int)Math.Abs(yMm*200)  );  
        }
        
      
        public void ZAction(object sender, EventArgs e, float zSteps)  // zSteps is absolute value from top on in mm
        {
          int direction = 0; // downwards                       
          if (zSteps < XYZMainForm.globalActualMmPosZ) direction = 1; // upwards 
          float zStepsToDo = Math.Abs(XYZMainForm.globalActualMmPosZ - zSteps)*800;
              
          zMove(direction,(int)zStepsToDo);
           XYZMainForm.globalActualMmPosZ = zSteps;                    
        }
           
        // for Z99PP
        /*
        public void Rotate(int direction,int steps)
        {                        
           rMove(direction, Math.Abs((int)steps));
        }
        */
        // for Z99PP
        /*
        public int GetAnalogValue(object sender, EventArgs e)
        {
            int val = 0;                              
            XYZMainForm.FlagPressure = false;  
           MeasureAnalogValue();
           while (XYZMainForm.FlagPressure == false) Application.DoEvents();            
            val = ThePressure;
            return (val);
            }
          */

        public void SetXYSpeed(int speed)
        {         
        if (speed < 5) speed = 5;
        if (speed > 1304) speed = 1304;
        int XYDelay = 1300 / (speed-4);  //  was 150 until 16 2 2020
        SetXYMotorsDelay(XYDelay);
        }


        public void SetZSpeed(int delay)
        {
               ZDelay( (int)(1500 / delay));  //  was 150 until 16 2 2020
       }

        // Z99PP
        /*
        public void SetRSpeed(int delay)
        {     
           RDelay(1500 / delay);  //  was 150 until 16 2 2020            
        }       
   */

    }
}
