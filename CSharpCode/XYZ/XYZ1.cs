using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;  // makes MessageBox possble in class


namespace XYZ
{      
          public partial class XYZR
    {
        public System.IO.Ports.SerialPort serialPort1 = new System.IO.Ports.SerialPort(); // 2
        public static byte[] bytesToPIC = new byte[6];          // commands are maxmost 6 bytes
        public static byte[] previousBytesToPIC = new byte[6];  // bytes previously send

        public Boolean NextCommand;  
              
        
        public bool leftFlagX = false;   // true when  left X cornerswitch becomes closed
        public bool rightFlagX = false;  // true when right X cornerswitch becomes closed 
        public bool backFlagY = false;   // true when one of both back Y cornerswitch becomes closed
        public bool frontFlagY = false;  // true when one of both Y cornerswitch becomes closed

        public void InitSerialPort()
        {                        
            serialPort1.PortName = XYZMainForm.CMPRT;      // Com3..Com128
            serialPort1.BaudRate = XYZMainForm.baudrate;   // default 57600;                  
            NextCommand = true;
          // Event handler when PC receives incoming command  
           this.serialPort1.DataReceived += 
                new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived); //2              
        }        
        // is used in Z99PP for measuring vacuum pressure
        
        private int Pressure=0;
        public int ThePressure
        {
            get
            {
                return this.Pressure;
            }            
        
        }

        public int OpenSerialPort()
        {

            if (serialPort1.IsOpen == true)
            {
              //  MessageBox.Show("open ok return 1 ");
                return 1;
            }
            else if (serialPort1.IsOpen == false)
            {
                try
                {

                 //   MessageBox.Show("open false, try to open and return 1 ");
                    //if (serialPort1.IsOpen == false) 
                    
                    serialPort1.Open();
                    Thread.Sleep(1000);


                    return 1;
                }
                catch (Exception Exc)
                {

                    MessageBox.Show("Wrong or non existing comportnumber is selected in setup.\n\n" + Exc.ToString());
                    return 0;

                }
            }

        //    MessageBox.Show("Open serialport returns 0");
            return 0; //?? 0 ??

        }


        public void CloseSerialPort()
        {
            if (serialPort1.IsOpen==true)
            { 
                try
                    {
                    serialPort1.ReadExisting();
                }
                catch (Exception Exc)
                {
                    MessageBox.Show(Exc.ToString());
                    return;
                }                               
                serialPort1.Close();                        
            }
        }


        public void Reset(object sender,EventArgs e)
        {
            if (OpenSerialPort() == 0) return;           
            bytesToPIC[0] = 65;               //  65 = A = alpha = reset
            bytesToPIC[1] = 0;                    
            bytesToPIC[2] = 65;
            bytesToPIC[3] = 0;
            bytesToPIC[4] = 65;                               
            NextCommand = true;
            sendBytesToPIC(5);
        }
      
     

        public void SetLaser(int Action)
        {
            if (OpenSerialPort() == 0) return;
            bytesToPIC[0] = 75;            //75 = LASER 
            // below 0 = laser OFF
            // below 1 = laser ON                               
            bytesToPIC[1] = (byte)(Action);    //  0 or 1      
            bytesToPIC[2] = 0; bytesToPIC[3] = 0; bytesToPIC[4] = 0; 
            sendBytesToPIC(5);
        }
                    
        public void ZDelay(int delay)  // speed of z motor(s)
        {
            if (OpenSerialPort() == 0) return;
            bytesToPIC[0] = 86;  // = V,  velocity of lift motor
            bytesToPIC[1] = (byte)((delay & 0xFF00) >> 8);  //  MSB first
            bytesToPIC[2] = (byte)(delay & 0X00FF);         //  LSB 
            bytesToPIC[3] = 0; bytesToPIC[4] = 0;
            sendBytesToPIC(5);
        }

       
        public void SetXYMotorsDelay(int delay)
        {
            if (OpenSerialPort() == 0) return;
            bytesToPIC[0] = 68;                        // =   D          = delay,XY steppers speed                                                   
            bytesToPIC[1] = (byte)((delay & 0xFF00) >> 8);  //  MSB first
            bytesToPIC[2] = (byte)(delay & 0X00FF);         //  LSB 
            bytesToPIC[3] = 0; bytesToPIC[4] = 0;   //               
           sendBytesToPIC(5);
        }              

     public void XY_Move(int xyDir, int xSteps, int ySteps)
        {
            if (OpenSerialPort() == 0) return;
         // MOST COMMON CASE 2 bytes precision in X and Y :

            if ((xSteps < 65535) && (ySteps < 65535))
            {         
                switch (xyDir)
                {
                    case 1:
                        bytesToPIC[0] = 87;    // 88 W  
                        break;
                    case 3:
                        bytesToPIC[0] = 89;    // 90 Y
                        break;
                    case 5:
                        bytesToPIC[0] = 91;    // 92 
                        break;
                    case 7:
                        bytesToPIC[0] = 93;    //  94
                        break;
                    default:
                        bytesToPIC[0] = 87;  // 88
                        break;
                }
            }

       // >2 bytes <4 bytes precision

        else if (((xSteps > 65535) || (ySteps > 65535)) && (xSteps < 131071) && (ySteps < 131071))
            {
                xSteps = xSteps / 2;      // ok we lose some precision.. still 0.01 mm precision, 10 times better then visible  
                ySteps = ySteps / 2;

                switch (xyDir)
                {
                    case 1:
                        bytesToPIC[0] = 94;    // 88 W  
                        break;
                    case 3:
                        bytesToPIC[0] = 95;    // 90 Y
                        break;
                    case 5:
                        bytesToPIC[0] = 96;    // 92 
                        break;
                    case 7:
                        bytesToPIC[0] = 97;    //  94
                        break;
                    default:
                        bytesToPIC[0] = 87;  // 88
                        break;
                }
            }
            // > 4 bytes <6 bytes precision , is very exceptional procedure
            else if (((xSteps > 131072) || (ySteps > 131072)) && (xSteps < 196605) && (ySteps < 196605))
            {
                xSteps = xSteps / 3; // ok we lose some precision.. still 8 times better then visible
                ySteps = ySteps / 3;

                switch (xyDir)
                {
                    case 1:
                        bytesToPIC[0] = 98;    // 88 W  
                        break;
                    case 3:
                        bytesToPIC[0] = 99;    // 90 Y
                        break;
                    case 5:
                        bytesToPIC[0] = 100;    // 92 
                        break;
                    case 7:
                        bytesToPIC[0] = 101;    //  94
                        break;
                    default:
                        bytesToPIC[0] = 87;  // 88
                        break;
                }
            }
            // > 6 bytes precision only for A0 with pulley T16

            else if ((xSteps > 196605) || (ySteps > 196605))
            {
                xSteps = xSteps / 4;  // ok we lose some precision.. still 0.02 mm precision, 5 times better then visible
                ySteps = ySteps / 4;

                switch (xyDir)
                {
                    case 1:
                        bytesToPIC[0] = 102;    // 88 W  
                        break;
                    case 3:
                        bytesToPIC[0] = 103;    // 90 Y
                        break;
                    case 5:
                        bytesToPIC[0] = 104;    // 92 
                        break;
                    case 7:
                        bytesToPIC[0] = 105;    //  94
                        break;
                    default:
                        bytesToPIC[0] = 87;  // 88
                        break;
                }
            }

            // always this :
            bytesToPIC[1] = (byte)((xSteps & 0xFF00) >> 8);  //  MSB first
            bytesToPIC[2] = (byte)(xSteps & 0X00FF);
            bytesToPIC[3] = (byte)((ySteps & 0xFF00) >> 8);  //  MSB first
            bytesToPIC[4] = (byte)(ySteps & 0X00FF);

          //  MessageBox.Show("XY "+bytesToPIC[1].ToString() + " " + bytesToPIC[2].ToString() + " " + bytesToPIC[3].ToString());
            
          //Debug.WriteLine("XY " + bytesToPIC[1].ToString() + " " + bytesToPIC[2].ToString() + " " + bytesToPIC[3].ToString());


            sendBytesToPIC(5);
        }
        
                
      public void EscapeXY_Move(int xyDir, int xSteps, int ySteps)
        {

       //     MessageBox.Show("In escapeXY " ); ok here


            if (OpenSerialPort() == 0) return;

            switch (xyDir)
                {
                    case 1:
                        bytesToPIC[0] =111;    // 111 is code re and down without checking corner switch !
                        break;             
                    case 5:
                        bytesToPIC[0] = 113;    //113 is code without checking corner switch upwards AND LEFTWARDS
                        break;              
                    default:
                        bytesToPIC[0] = 0;  // no move ?
                        break;
                }                       
            bytesToPIC[1] = (byte)((xSteps & 0xFF00) >> 8);  //  MSB first
            bytesToPIC[2] = (byte)(xSteps & 0X00FF);
            bytesToPIC[3] = (byte)((ySteps & 0xFF00) >> 8);  //  MSB first
            bytesToPIC[4] = (byte)(ySteps & 0X00FF);

            sendBytesToPIC(5);


      //      MessageBox.Show("In escapeXY " + bytesToPIC[0].ToString() );



        }
             

        public void zMove(int dir, int zSteps)   // Z up down
        {
            int temp = zSteps;
            if (OpenSerialPort() == 0) return;

            if (zSteps > 30000) zSteps = 30000;  // 3000 / 800 = 37.5 mm is ok
            bytesToPIC[0] = 76;                   //  =  76  =  L(IFT) up or down              
            bytesToPIC[1] = (byte)dir;  // z direction
            bytesToPIC[2] = (byte)((temp & 0x0000FF00) >> 8);  //   MSB first  
            bytesToPIC[3] = (byte)(temp & 0x000000FF);
            bytesToPIC[4] = 0;
            sendBytesToPIC(5);   
        }

        // 21 7 2020
       
        public void EscapeZMove()
        {
            if (OpenSerialPort() == 0) return;
            bytesToPIC[0] = 110;                   //  =  Z escape, Z down              
            bytesToPIC[1] = 0;      //   (byte)dir;  // z direction
            bytesToPIC[2] = 17;      // 7   2*256+208=2000 =2.5mm  (byte)((temp & 0x0000FF00) >> 8);  //   MSB first  
            bytesToPIC[3] = 208;    //    (byte)(zSteps & 0x000000FF);
            bytesToPIC[4] = 0;
            sendBytesToPIC(5);   //
        }
        
    
        
        public void tMove(int dir, int tSteps) // t = tune bottom microscope with unipolar ST28 stepper
                                               // later used for solder paste dispenser
        {
            if (OpenSerialPort() == 0) return;
            if (tSteps > 40096) tSteps = 40096; // =10 mm retraction or extrusion
            bytesToPIC[0] = 84;                  // 84=t 
            
            int temp = tSteps;
            bytesToPIC[1] = (byte)dir;                           //  direction
            bytesToPIC[2] = (byte)((temp & 0x0000FF00) >> 8);    //   MSB first  
            bytesToPIC[3] = (byte)(tSteps & 0x000000FF);         // LSB
            bytesToPIC[4] = 0;
            sendBytesToPIC(5);   //
        }
        
      
      public void sendBytesToPIC(int CountBytes)       //
        {         
            // calculate checksumbyte:
         bytesToPIC[5] = (byte)(bytesToPIC[0] + bytesToPIC[1] + bytesToPIC[2] + bytesToPIC[3] + bytesToPIC[4]);  // = checksum

                              
         while (NextCommand == false) Application.DoEvents(); // wait until previous command is ready                        
                    
           NextCommand = false;
                       

            serialPort1.Write(bytesToPIC, 0, CountBytes+1); // + 1);     //   Ready, send to PIC ! 

                XYZMainForm.SerialPortSendS = bytesToPIC[0].ToString() + " " + bytesToPIC[1].ToString() + " " +
                    bytesToPIC[2].ToString() + " " + bytesToPIC[3].ToString() +" "+
                    bytesToPIC[4].ToString() + " " + bytesToPIC[5].ToString();
          
        }


        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {                 
            int br;            
            while (serialPort1.BytesToRead > 0)
            {
                br = serialPort1.ReadByte();
                             

                if (br == 43)
                {       
                    XYZMainForm.SerialPortReturnS = br.ToString();
                    NextCommand = true;
                }

                //  Z switch closed :
                else if (br == 44)
                {              
                    XYZMainForm.SerialPortReturnS = br.ToString();
                    NextCommand = true;
                    EscapeZMove();
                    XYZMainForm.globalActualMmPosZ = 0;
                    //  return;

                }
                //  X left switch is closed :
                else if (br == 45)
                {    
                    XYZMainForm.SerialPortReturnS = br.ToString();
                    NextCommand= true;
                    leftFlagX = true;
                   
                }
                // right  X switch is closed :
                else if (br == 46)
                {         
                    XYZMainForm.SerialPortReturnS = br.ToString();
                    NextCommand = true;
                    rightFlagX = true;
                }

                //   back Y switch is closed :
                else if ((br == 47) || (br == 48))
                {             
                    XYZMainForm.SerialPortReturnS = br.ToString();
                    NextCommand = true;
                    backFlagY = true;   
                }

                //  if one of both front Y switches is closed :
                else if (br == 49)
                {             
                    XYZMainForm.SerialPortReturnS = br.ToString();
                    NextCommand = true;
                    frontFlagY = true;                  

                }
                else if (br == 63)
                {            
                    XYZMainForm.SerialPortReturnS = br.ToString();
                    NextCommand = true;               
                }

                serialPort1.ReadExisting();                

    }
}
        
      
        
    }
}
