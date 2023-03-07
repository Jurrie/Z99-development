
    # include <built_in.h>



    #define EscapeStepsXY 600  // XY steps to do when corner switch becomes closed during initialisation, switch level has some hysteresis
                             // 600 microsteps =  3.0 mm  in reverse direction when corner switch becomes closed
    #define EscapeStepsZ  2000 // Z steps to do downwards when Z switch becomes closed.
                               // 2000 = 2.5 mm downwards in case of trapezoidal leadscrew that advances
                               // 8 mm on each turn
           // 2 X  STEPPERS
    #define DirX  LATD.B0       // LATD.F0   pin 19
    #define StepX LATD.B1       // LATD.F1   pin 20

         //  4 Y STEPPERS
    #define DirYR LATC.B0    // direction right 2 Y motors
    #define StepYR LATC.B3   // Step for 2 RIGHT Y motors
    #define DirYL LATC.B1    /// direction left 2 Y motors
    #define StepYL LATC.B2   //  Step for 2 LEFT Y motors

        //  Z STEPPER
     // if jumper 1 is placed = no Z motor = for laser Z carriage without Z motor
     // if jumper is NOT placed = Z motor for all other Z carriages is active

    #define JumperZ  PORTA.F1    // check if jumper is placed on pin 3 = port A1
    #define DirZ  LATE.B0        //  pin 8
    #define StepZ LATA.B5       //  pin 7

     // CORNER SWITCHES OPEN SWITCH = 1  CLOSED SWITCH = 0
        #define ZCORNERSWITCH    PORTB.F0    //  Z top
        #define YLCORNERSWITCH   PORTB.F1    //  Y left back
        #define YRCORNERSWITCH   PORTB.F2    //  Y  right back
        #define Y2CORNERSWITCH   PORTB.F3   //  for BOTH Y front switches
        #define XLCORNERSWITCH   PORTB.F4   //  X left
        #define XRCORNERSWITCH   PORTB.F5   // X right

        #define LASER LATD.F2   // LASER

    //  unipolar stepper is only used for ST28 dispenser stepper
      #define   S1F1  LATD.F7     // PIC18F45K22 pin 30  uln2803 pin 2
      #define   S1F2  LATD.F6    // PIC18F45K22 pin 29   uln2803 pin 3
      #define   S1F3  LATD.F5    // PIC18F45K22 pin 28   uln2803 pin 4
      #define   S1F4  LATD.F4    // PIC18F45K22 pin 27   uln2803 pin 5

     void MotorXY(unsigned int, unsigned int,unsigned short);
    // Escape mode in case of X or Y cornerswitch is closed
    // PC sofware will take care of direction
     void EscapeMotorXY(unsigned short,unsigned short,unsigned short,unsigned short);

     void Lift(unsigned int,unsigned int);
    // Escape mode in case of Z cornerswitch is closed
    // always downwards
     void EscapeMotorZ(unsigned int);

     void ST28(unsigned int,unsigned int); // dispenser motor

     void ZSpeed(unsigned int,unsigned int);
     
     void Initialisation(void);
        unsigned short UartByte;
        unsigned short Commands[12];    ///  10  is ok
        unsigned short CommandIndex=0;

        int XYMotorDelay =25;     // Speed of XY steps.
        int ZMotorDelay= 75;     // Speed of Z steps.

unsigned short countTimerX=0,countTimerY=0;

void interrupt()
        {
    if (TMR0IF_bit){
     countTimerX++;
     TMR0IF_bit = 0;         //  clear flag
        }
    if (TMR1IF_bit)
        {
     countTimerY++;
     TMR1IF_bit=0;           //  clear flag
        }
     }

void main()
       {
       int x=0;
       int y=0;
      unsigned int StepsX=0;
      unsigned int StepsY=0;
      unsigned int StepsZ=0;
      unsigned int StepsD=0;    // solder paste dispenser

       unsigned short CheckSum = 0 ;
       unsigned short TestCheckSum=0;
       int CheckSumIndex=0;
       int xDir=0,yDir=0;

        unsigned long xLong=0,yLong=0;
        unsigned int part1=0,part2=0,part3=0;

       //  BELOW ANSEL IS VERY IMPORTANT:ALL DIGITAL PORTS
       //  EVEN DIGITAL INPUT PORTS NEED TO BE 0 !!!!!!!!!!!
        ANSELA = 0b00001100;   //   Configure A2 and A3 pin as analog for MPX5010DP
                                //  PIN A1 is input for Z jumper
                                // no jumper = high = default operation with Z motor
                                // jumper placed = low = for operation without Z motor, laser module

        ANSELB = 0x00;        //   Configure PORTB pins as digital
        ANSELC = 0X00;        //   Configure PORTC pins as digital
        ANSELD=0X00;
        ANSELE=0X00;

    TRISA  = 0b00000010;      //  A1 = DIGITAL INPUT FOR Z JUMPER
                             // no jumper = high = default operation with Z motor
                             // jumper placed = low = for operation without Z motor, laser module

    TRISB = 0b11111111;      // All input for corner switches
    TRISC = 0b10000000;      //   C7 = RX van UART1
    TRISD = 0b00000000;     // = all output
    TRISE = 0b00000000;     // = all output

     LATA = 0b00000000;
     LATC = 0x00;
     LATD = 0b00000100;  // was 0x00  TURN LASER OFF
     LATE = 0x00;

    UART1_Init(57600);   // 57600
                        //  115200 was often OK for a couple of hours, but not for days !
    Delay_ms(200);          //  Wait for UART module to stabilize

     CommandIndex=0;

            // TIMER0  +-100 usec
           //  T0CON        = 0x88; // / no prescaler
             // 100 usec :
             TMR0H        = 0xF9; //+-100usec
             TMR0L        = 0xC0;  // +- 100 usec
             GIE_bit      = 1;
             countTimerY=0;
              TMR0IE_bit    = 1;   // enable timer 0 interrupt

            // TIMER1       +-100 usec
            T1CON         = 0x01; // no prescaler
            TMR1H         = 0xF9;  // +- 100 usec
            TMR1L         = 0xC0;  // +- 100 usec
       //     TMR1IE_bit    =1;      // enable timer 1 interrupt
            countTimerX   =0;
            INTCON=0XC0; // sety GIE, PEIE

     //  BELOW THE CODE FOR XYZzRrMOTION

          Initialisation();      // Will home Z Y and X axis in this order
        // enable timer 0 interrupt
           TMR1IE_bit    =1;      // enable timer 1 interrupt

         for(;;)
        {
             CommandIndex=0;
             while( CommandIndex < 6)      //    Go for this without interrupts
               {
              if (UART1_Data_Ready())
                      {
                            Commands[ CommandIndex ] = UART1_Read();
                            CommandIndex++;
                        }
               }
                // RESET ALSO THIS COULD HAPPEN :
                if (  ( Commands[0] == 65) && (Commands[1] == 0)    )
                               {
                           Uart1_Write(43);
                            CommandIndex=0;
                            continue;  // soft will continue on top
                                }
               CheckSum = Commands[0]+  Commands[1]+ Commands[2]+ Commands[3]+Commands[4];  //

               TestCheckSum = Commands[5];      // CheckSum;

               if ( TestCheckSum ==  CheckSum   )
                         {
                    switch(Commands[0])
                               {
                      case 68:   //   D   = MotorDelay, speed, steps /sec of XY steppers
                                 XYMotorDelay = (Commands[1] << 8 ) + Commands[2];
                                Uart1_Write(43);
                                CommandIndex=0;
                              break;
                             
                      case 75: // Laser
                               switch(Commands[1])
                                       {
                               case 0: LASER=0;          //= ON       Pin 21 of PIC D.F2 low
                                        break;
                               case 1: LASER=1;        // = OFF     Pin 21 of PIC D.F2 high
                                        break;
                              default: break;
                                        }
                                 Uart1_Write(43);
                                 CommandIndex=0;
                           break;

                      case 76:  // LIFT
                               StepsZ = ( Commands[2]  << 8) + Commands[3];
                               Lift ( (unsigned int)Commands[1]  ,(unsigned int) StepsZ );
                               Uart1_Write(43);
                               CommandIndex=0;
                               break;

                     // This function serves for unipolar ST28 dispensing stepper
                     case 84:
                            StepsD =  (Commands[2] << 8 ) + Commands[3];
                            ST28(Commands[1], StepsD);
                            Uart1_Write(43);
                           CommandIndex=0;
                             break;

                     case 86: // speed of Z motor;
                                ZMotorDelay = (Commands[1] << 8 ) + Commands[2];
                                Uart1_Write(43);
                                CommandIndex=0;
                              break;
                              
              //PART I  XY MOVEMENTS X and Y < 65536
              
                     case 87:         //  XY direction to left front corner
                            DirX=0;
                            DirYR = 0;
                            DirYL=DirYR;          // Direction is the same on both Y motors, 
                                                // one coil is crossed over on the PCB  to right Y motors !
                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY =  (unsigned int)  ( (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,1);  // 1 =  parts
                            Uart1_Write(43);
                            CommandIndex=0;
                             break;
                    case 89:                   // XY direction to left and back
                              DirX=0;
                              DirYR = 1;
                              DirYL=DirYR;
                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,1);   // 1 1 =slowstart slowstop
                            Uart1_Write(43);
                            CommandIndex=0;
                             break;
                    case 91:                   //      to right uppercorner
                              DirX=1;
                              DirYR = 1;
                              DirYL=DirYR;          // Direction is the same on both Y motors, one coil is crossed over on the PCB or connection to Y motor !
                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,1);    // 1 1 =slowstart slowstop
                            Uart1_Write(43);
                            CommandIndex=0;
                             break;
                   case 93:                   //   to right inferior corner
                              DirX=1;
                              DirYR = 0;
                              DirYL=DirYR;
                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,1);  // 1 =slowstart slowstop
                            Uart1_Write(43);
                            CommandIndex=0;
                             break;
             
           //   PART II   X or Y >655535  and <131070
                   case 94:         //  Dir 1    W  of  XY dir to right and inferior corner
                            DirX=0;
                            DirYR = 0;
                            DirYL=DirYR;          // Direction is the same on both Y motors, one coil is crossed over on the PCB or connection to Y motor !

                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,2);   // 2 = 2 parts
                            Uart1_Write(43);
                            CommandIndex=0;
                            break;
                    case 95:                   // DIR 3 dir to right and above
                            DirX=0;
                            DirYR = 1;
                            DirYL=DirYR;          //
                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,2);  // 2 = 2 parts

                           Uart1_Write(43);
                            CommandIndex=0;
                           break;
                    case 96:                   //   Dir 5    to upper and left
                             DirX=1;  //
                             DirYR = 1;
                             DirYL=DirYR;          // Direction is the same on both Y motors, one coil is crossed over on the PCB or connection to Y motor !
                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,2);  // 2 = 2 parts
                            Uart1_Write(43);
                            CommandIndex=0;
                            break;
                   case 97:                   //  Dir 7 to left and inferior
                              DirX=1;
                              DirYR = 0;
                              DirYL=DirYR;          //
                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,2);  // 2 = 2 parts
                             Uart1_Write(43);
                            CommandIndex=0;
                             break;
             
            //   PART III   X or Y >131070  and <196605

                   case 98:         //  Dir 1    W  of  XY dir to right and inferior corner
                            DirX=0;
                            DirYR = 0;
                            DirYL=DirYR;          // Direction is the same on both Y motors, one coil is crossed over on the PCB or connection to Y motor !
                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,3);  //3 = 3 parts
                            Uart1_Write(43);
                            CommandIndex=0;
                            break;
                    case 99:                   // DIR 3 dir to right and above
                            DirX=0;
                            DirYR = 1;
                            DirYL=DirYR;          //
                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,3);  //3 = 3 parts
                           Uart1_Write(43);
                            CommandIndex=0;
                           break;
                    case 100:                   //   Dir 5    to upper and left
                             DirX=1;  //
                             DirYR = 1;
                             DirYL=DirYR;          // Direction is the same on both Y motors, one coil is crossed over on the PCB or connection to Y motor !
                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,3);  //3 = 3 parts
                            Uart1_Write(43);
                            CommandIndex=0;
                            break;
                   case 101:                   //  Dir 7 to left and inferior
                              DirX=1;
                              DirYR = 0;
                              DirYL=DirYR;          //
                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,3);  //3 = 3 parts
                             Uart1_Write(43);
                            CommandIndex=0;
                             break;
             
            //   PART IV   X or Y > 196605
            // maybe will never be needed
            // only if X or Y distance is more as 65536*0.005mm= 327.68 * 4 = 1 meter 31 cm

                   case 102:         //  Dir 1    W  of  XY dir to right and inferior corner
                            DirX=0;
                            DirYR = 0;
                            DirYL=DirYR;          // Direction is the same on all 4 Y motors
                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,4);  //4 = 4 parts
                            Uart1_Write(43);
                            CommandIndex=0;
                            break;
                    case 103:                   // DIR 3 dir to right and above
                            DirX=0;
                            DirYR = 1;
                            DirYL=DirYR;          //
                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,4);  //4 = 4 parts
                           Uart1_Write(43);
                            CommandIndex=0;
                           break;
                    case 104:                   //   Dir 5    to upper and left
                             DirX=1;  //
                             DirYR = 1;
                             DirYL=DirYR;          // Direction is the same on both Y motors, one coil is crossed over on the PCB or connection to Y motor !
                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,4);  //4 = 4 parts
                            Uart1_Write(43);
                            CommandIndex=0;
                            break;
                   case 105:                   //  Dir 7 to left and inferior
                              DirX=1;
                              DirYR = 0;
                              DirYL=DirYR;          //
                            StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
                            StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
                            MotorXY(StepsX,StepsY,4);  //4 = 4 parts
                             Uart1_Write(43);
                            CommandIndex=0;
                             break;
                             
                // ESCAPE EXTENSIONS
                // START WITH Z
                       case 110:
                            EscapeMotorZ(2000); // 2.5 mm for now
                            Uart1_Write(43); // maybe not
                            CommandIndex=0;
                             break;
                             // X
                      case 111:
                            DirX=0;   // GO RIGHTWARDS OR DOWNWARDS WITHOUT CHECKING CORNERSWITCH
                            DirYR = 0;
                            DirYL=DirYR;
                            EscapeMotorXY(Commands[1],Commands[2],Commands[3],Commands[4]);
                            
                            Uart1_Write(43); // maybe not
                            CommandIndex=0;
                             break;

                       case 113:     // both back Y switches are closed
                            DirX=1;   // GO FRONTWARDS OR LEFTWARDS WITHOUT CHECKING CORNERSWITCH
                            DirYR = 1;
                            DirYL=DirYR;
                            EscapeMotorXY(Commands[1],Commands[2],Commands[3],Commands[4]);
                            Uart1_Write(43); // maybe not
                            CommandIndex=0;
                            break;

                     default:
                                Uart1_Write(63); //maybe aks for same instruction once more  when
                                                     // Commands[0] is not in the  switch list
                                CommandIndex=0;
                               break;
                            }
                         }  // checksum ok

                    else     // if checksum was not ok
                    {
                                    Uart1_Write(63); // aks for same instruction once more
                                    CommandIndex=0;
                    }
             }    // for
   }            // main


    void MotorXY(unsigned int xSteps,unsigned int ySteps, unsigned short parts)
              {
              unsigned int M=0;       // M = motor steps
              unsigned long stepperDelayX=0;     //  for X stepper
              unsigned int timerStepperDelayX=0;
              unsigned long stepperDelayY=0;     //  for Y stepper
               unsigned int timerStepperDelayY=0;
              unsigned int counterStepX=0;
              unsigned int counterStepY=0;
              unsigned int z=0; // used for timer delay
              
              unsigned int PartOf1600;
              unsigned int PartOf160;

            if (xSteps==ySteps)               //  ySteps <= xSteps
            {
                  stepperDelayX=XYMotorDelay*2*15;    // * 2 2*delay *15 from 14.7456MHZ clock count/sec
                  timerStepperDelayX = (unsigned int)(65536-(unsigned int)(stepperdelayX));  // was+1200 start slow

                            TMR0IE_bit=0;                              // disable timer0
                            TMR0H= (timerStepperDelayX & 0XFF00)>>8;
                            TMR0L=  timerStepperDelayX & 0X00FF;
                            TMR0IE_bit=1;                             // enable timer0

                   for (M=0;M<parts;M++)
                       {
                     counterStepX=0;
                 do
                     {
                       if (YLCORNERSWITCH==0) {  Uart1_Write(47); break; } //
                       if (YRCORNERSWITCH==0) {  Uart1_Write(48); break;  }
                       if (Y2CORNERSWITCH==0) {  Uart1_Write(49); break; }
                       if (XRCORNERSWITCH==0) {  Uart1_Write(46); break; }
                       if (XLCORNERSWITCH==0) {  Uart1_Write(45);break; }

                  // timer1 action  on 2  X motors:
                   if((countTimerX>0) && (counterStepX<xSteps))
                               {
                        StepX=1;
                        StepYR=1;
                        StepYL=1;
                        counterStepX++;

                  if (M==0)
                            {
                    if (counterStepX<1600)
                         for (z=counterStepX; z <(1600- counterStepX) ;z+=160)
                          delay_us(1);
                            }

                    if (M==(parts-1))
                             {
                   if (counterStepX> ( xSteps- 1600))
                      for (z= 0; z <  (  counterStepX -  (xSteps - 1600) ) ;  z+=160)
                                 delay_us(1);
                            }

                            TMR0IE_bit=0;                             // disable timer0
                            TMR0H= (timerStepperDelayX & 0XFF00)>>8;
                            TMR0L=  timerStepperDelayX & 0X00FF;
                            TMR0IE_bit=1; // enable timer1
                            countTimerX=0;
                            StepX=0;
                            StepYL=0;
                            StepYR=0;
                             }
                         }  while (counterStepX < xSteps );
                         } // end M
                    }   // xSteps = ySteps

             else if (xSteps==0)       // only y steps to do
                         {
                         // remark line below * 35 ipv * 3*15 (45) , otherwise to quick
                         
                    stepperDelayY=XYMotorDelay*35;    // * 2 2*delay *15 from 14.7456MHZ clock count/sec
                    timerStepperDelayY = (unsigned int)(65536-(unsigned int)(stepperdelayY));  // was+1200 start slow

                            TMR1IE_bit=0;   // disable timer0
                            TMR1H= (timerStepperDelayY & 0XFF00)>>8;
                            TMR1L=  timerStepperDelayY & 0X00FF;
                            TMR1IE_bit=1;                             // enable timer0

                   for (M=0;M<parts;M++)
                       {
                     counterStepY=0;
                 do
                     {
                       if (YLCORNERSWITCH==0) {  Uart1_Write(47); break; } //
                       if (YRCORNERSWITCH==0) {  Uart1_Write(48); break;  }
                       if (Y2CORNERSWITCH==0) {  Uart1_Write(49); break; }

                  // timer1 action  on 2  X motors:
                   if((countTimerY>0) && (counterStepY<ySteps))
                               {
                        StepYR=1;
                        StepYL=1;
                        counterStepY++;

                  if (M==0)
                            {
                    if (counterStepY<1600)
                         for (z=counterStepY; z <(1600- counterStepY) ;z+=160)
                          delay_us(1);
                            }

                    if (M==(parts-1))
                             {
                   if (counterStepY> ( ySteps- 1600))
                      for (z= 0; z <  (  counterStepY -  (ySteps - 1600) ) ;  z+=160)
                                 delay_us(1);
                            }
                            TMR1IE_bit=0;                             // disable timer0
                            TMR1H= (timerStepperDelayY & 0XFF00)>>8;
                            TMR1L=  timerStepperDelayY & 0X00FF;
                            TMR1IE_bit=1; // enable timer1
                            countTimerY=0;
                              StepYR=0;
                              StepYL=0;
                                  }
                         }  while (counterStepY < ySteps );
                         } // end M
                    }   //ySteps >  xSteps
                           
                else if (ySteps==0)       // only xSteps
                       {
                   // remark line below * 35 ipv * 3*15 (45) , otherwise to quick
                  stepperDelayX=XYMotorDelay*35;    // * 2 2*delay *15 from 14.7456MHZ clock count/sec
                    timerStepperDelayX = (unsigned int)(65536-(unsigned int)(stepperdelayX));  // was+1200 start slow

                            TMR0IE_bit=0;                              // disable timer0
                            TMR0H= (timerStepperDelayX & 0XFF00)>>8;
                            TMR0L=  timerStepperDelayX & 0X00FF;
                            TMR0IE_bit=1;                             // enable timer0


                   for (M=0;M<parts;M++)
                       {
                     counterStepX=0;
                     counterStepY=0;

                 do
                     {
                       if (XRCORNERSWITCH==0) {  Uart1_Write(46); break; }
                       if (XLCORNERSWITCH==0) {  Uart1_Write(45);break; }

                  // timer1 action  on 2  X motors:
                   if((countTimerX>0) && (counterStepX<xSteps))
                               {
                        StepX=1;
                        counterStepX++;

                  if (M==0)
                            {
                    if (counterStepX<1600)
                         for (z=counterStepX; z <(1600- counterStepX) ;z+=160)
                          delay_us(1);
                            }

                    if (M==(parts-1))
                             {
                   if (counterStepX> ( xSteps- 1600))
                      for (z= 0; z <  (  counterStepX -  (xSteps - 1600) ) ;  z+=160)
                                 delay_us(1);
                            }
                            TMR0IE_bit=0;                             // disable timer0
                            TMR0H= (timerStepperDelayX & 0XFF00)>>8;
                            TMR0L=  timerStepperDelayX & 0X00FF;
                           TMR0IE_bit=1; // enable timer1
                            countTimerX=0;
                            StepX=0;
                                  }
                                  
                         }  while (counterStepX < xSteps );
                         
                         } // end M
                    }   //ySteps >  xSteps


           // PART IV

       else if (xSteps>ySteps)
             {
             // NEW
              partof1600 = (unsigned int)(((long)1600*(long)ySteps)/(long)xSteps)  ;
              partOf160 = (unsigned int)(((long)partOf1600*(long)ySteps)/(long)xSteps)  ;
              if (partOf160==0) partOf160=1;

             stepperDelayX=XYMotorDelay*2*15;    // * 2 2*delay *15 from 14.7456MHZ clock count/sec

                  // After calculations of extra delay of 1600 first and last microsteps 280 was ok
                  // as extra delay for Y motors
                  
             stepperDelayY=(unsigned int)(((long)stepperDelayX*(long)xSteps)/(long)ySteps); // +80 nearly ok

                    timerStepperDelayX = (unsigned int)(65536-(unsigned int)(stepperdelayX));  //
                    timerStepperDelayY = (unsigned int)(65536-(unsigned int)(stepperdelayY) );

                            TMR0IE_bit=0;                              // disable timer0
                            TMR0H= (timerStepperDelayX & 0XFF00)>>8;
                            TMR0L=  timerStepperDelayX & 0X00FF;
                            TMR0IE_bit=1;                             // enable timer0

                            TMR1IE_bit=0;                             // disable timer1
                            TMR1H= (timerStepperDelayY & 0XFF00)>>8;
                            TMR1L=  timerStepperDelayY & 0X00FF;
                            TMR1IE_bit=1;                             // enable timer1

                   for (M=0;M<parts;M++)
                       {
                     counterStepX=0;
                     counterStepY=0;

                 do
                     {
                       if (YLCORNERSWITCH==0) {  Uart1_Write(47); break; } //
                       if (YRCORNERSWITCH==0) {  Uart1_Write(48); break;  }
                       if (Y2CORNERSWITCH==0) {  Uart1_Write(49); break; }
                       if (XRCORNERSWITCH==0) {  Uart1_Write(46); break; }
                       if (XLCORNERSWITCH==0) {  Uart1_Write(45);break; }

                  // timer1 action  on 2  X motors:
                   if((countTimerX>0) && (counterStepX<xSteps))
                               {
                        StepX=1;
                        counterStepX++;

                  if (M==0)
                            {
                 if (counterStepX<1600)
                         for (z=counterStepX; z <(1600- counterStepX) ;z+=160)
                          delay_us(1);
                            }

                    if (M==(parts-1))
                             {
                   if (counterStepX> ( xSteps- 1600))
                      for (z= 0; z <  (  counterStepX -  (xSteps - 1600) ) ;  z+=160)
                                 delay_us(1);
                            }

                            TMR0IE_bit=0;                             // disable timer0
                            TMR0H= (timerStepperDelayX & 0XFF00)>>8;
                            TMR0L=  timerStepperDelayX & 0X00FF;
                            TMR0IE_bit=1; // enable timer1
                            countTimerX=0;
                            StepX=0;
                                  }
                    // timer0 action  4 Y motors :

                    if ((countTimerY>0)&&(counterStepY<ySteps))
                                 {
                            StepYR=1;
                            StepYL=1;
                             counterStepY++;

                      //  MAKE SYNCHRONE

                                  if (M==0)
                            {
                 if (counterStepY< partOf1600)
                         for (z=counterStepY; z <(partOf1600- counterStepY) ;z+=partOf160)
                          delay_us(1);
                            }

                    if (M==(parts-1))
                             {
                   if (counterStepY> ( ySteps- partOf1600))
                      for (z= 0; z <  (  counterStepY -  (ySteps - partOf1600) ) ;z+=partOf160)
                                 delay_us(1);
                            }

                         // END SYNCHRONISATION

                            TMR1IE_bit=0;  // disable timer1
                            TMR1H= (timerStepperDelayY & 0XFF00)>>8;
                            TMR1L=  timerStepperDelayY & 0X00FF;
                            countTimerY=0;

                            StepYR=0;
                            StepYL=0;
                            TMR1IE_bit=1; // enable timer1 Y
                                  }

                         }  while (counterStepX < xSteps );

                         
                         } // end M
                    }   //ySteps >  xSteps

              // PART V
              
          else if (ySteps>xSteps)
                        {
                          // NEW

              partof1600 = (unsigned int)(((long)1600*(long)xSteps)/(long)ySteps)  ;
              partOf160 =  (unsigned int)(((long)partOf1600*(long)xSteps)/(long)ySteps)  ;
              if (partOf160==0) partOf160=1;

              // partOf1600/5;  // /// WAS 10  20 : slechter

                  stepperDelayY=XYMotorDelay*2*15;    // * 2 2*delay *15 from 14.7456MHZ clock count/sec

                  stepperDelayX=(unsigned int)(((long)stepperDelayY*(long)ySteps)/(long)xSteps);  // +80 nearly ok
                  timerStepperDelayX = (unsigned int)(65536-(unsigned int)(stepperdelayX));  //
                  timerStepperDelayY = (unsigned int)(65536-(unsigned int)(stepperdelayY) );

                            TMR0IE_bit=0;                              // disable timer0
                            TMR0H= (timerStepperDelayX & 0XFF00)>>8;
                            TMR0L=  timerStepperDelayX & 0X00FF;
                            TMR0IE_bit=1;                             // enable timer0

                            TMR1IE_bit=0;                             // disable timer1
                            TMR1H= (timerStepperDelayY & 0XFF00)>>8;
                            TMR1L=  timerStepperDelayY & 0X00FF;
                            TMR1IE_bit=1;                             // enable timer1

                   for (M=0;M<parts;M++)
                       {
                     counterStepX=0;
                     counterStepY=0;

                 do
                     {
                       if (YLCORNERSWITCH==0) {  Uart1_Write(47); break; } //
                       if (YRCORNERSWITCH==0) {  Uart1_Write(48); break;  }
                       if (Y2CORNERSWITCH==0) {  Uart1_Write(49); break; }
                       if (XRCORNERSWITCH==0) {  Uart1_Write(46); break; }
                       if (XLCORNERSWITCH==0) {  Uart1_Write(45);break; }

                  // timer0 action  on 4  Y motors:
                   if((countTimerY>0) && (counterStepY<ySteps))
                               {
                            StepYR=1;
                            StepYL=1;
                            counterStepY++;

                      if (M==0)
                          {
                    if (counterStepY<1600)
                         for (z=counterStepY; z <(1600- counterStepY) ;z+=160)
                          delay_us(1); // later less
                            }

                     if (M==(parts-1))
                             {
                   if (counterStepY> ( ySteps- 1600))
                      for (z= 0; z <  (  counterStepY -  (ySteps - 1600) ) ;  z+=160)
                                 delay_us(1);
                            }

                            TMR1IE_bit=0;                              // disable timer0
                            TMR1H= (timerStepperDelayY & 0XFF00)>>8;
                            TMR1L=  timerStepperDelayY & 0X00FF;
                            TMR1IE_bit=1; // enable timer1
                            countTimerY=0;
                            StepYR=0;
                            StepYL=0;
                                  }
                                  
                    // timer0 action  2 X motors :

                    if ((countTimerX>0)&&(counterStepX<xSteps))
                                 {
                            StepX=1;
                            counterStepX++;

                            // MAKE SYNCHRONE
                           if (M==0)
                          {
                    if (counterStepX<partOf1600)
                         for (z=counterStepX; z <(partOf1600- counterStepX) ;z+=partOf160)
                          delay_us(1);
                            }

                     if (M==(parts-1))
                             {
                   if (counterStepX> ( ySteps- partOf1600))
                      for (z= 0; z <  (  counterStepX -  (xSteps - partOf1600) ); z+=partOf160)
                                 delay_us(1);
                            }
                           // END SYNCHRONISATION
                            
                            TMR0IE_bit=0;  // disable timer1
                            TMR0H= (timerStepperDelayX & 0XFF00)>>8;
                            TMR0L=  timerStepperDelayX & 0X00FF;
                            countTimerX=0;

                            StepX=0;
                             TMR0IE_bit=1; // enable timer1
                                  }

                         }  while (counterStepY < ySteps );
                         } // end M
                    }   //ySteps >  xSteps
            }


  // Escape mode in case of X or Y cornerswitch is closed, this means moving without checking cornerswitch
     void EscapeMotorXY(unsigned short command1,unsigned short command2,unsigned short command3,
                        unsigned short command4)
     {
          unsigned int xSteps,ySteps;
           unsigned int L = 0;
           
                xSteps = (unsigned int)  ( (command1 << 8 ) + command2);
                ySteps = (unsigned int)  (  (command3 << 8 )+ command4);
           
              if (xSteps==0)
                         {
                  for (L=0;L<ySteps;L++)       // -100
                              {
                             StepYR=1;
                             StepYL=1;
                             Delay_us(200); // slow
                             StepYR=0;
                             StepYL=0;
                             Delay_us(200); // slow
                               }
                           }
              else if (ySteps==0)       //
                          {
                  for (L=0;L<xSteps;L++)       // -100
                              {
                            StepX=1;
                            Delay_us(200); // slow
                            StepX=0;
                             Delay_us(200); // slow
                              }
                           }
              }

                       // Z motor action
      void Lift(unsigned int Direction, unsigned int Steps)
       {
         unsigned int x;
         if (Direction==0) DirZ=0;       //  downwards
         else DirZ=1;                    //  upwards
          for (x=0;x<Steps;x++)
          {
          StepZ=0;
             ZSpeed(x,(Steps-x));
          StepZ=1;
            ZSpeed(x,(Steps-x));
          if (ZCORNERSWITCH==0)
          {
          Uart1_Write(44);  // if top is reached
           break;
           }
          }
        }

     void EscapeMotorZ(unsigned int Steps)
       {
         unsigned int x;
         DirZ=0;     //  always downwards !
          for (x=0;x<Steps;x++)
          {
          StepZ=0;
            Delay_us(200);
          StepZ=1;
              Delay_us(200);
          }
        }


   void  ST28( unsigned int Direction,unsigned int Steps) //ST 28 unipolar motor
       {
         int x;
         int stepsTune =0;
         
          for (x=0;x<Steps;x++)
          {
          if (Direction ==1 ) stepsTune = stepsTune + 1;
          else stepsTune =  stepsTune - 1;
           if (stepsTune>7)  stepsTune = 0;
           if (stepsTune<0)  stepsTune = 7;
          switch(stepsTune)
            {    //  all 4 coils need a full definition for both directions
             case 0:S1F1 = 1;   S1F2 = 0; S1F3 = 0;  S1F4=  0;   break;
             case 1: S1F1 = 1;   S1F2 = 1; S1F3 = 0;  S1F4=  0;   break;
             case 2: S1F1 = 0;   S1F2 = 1; S1F3 = 0;  S1F4=  0;   break;
             case 3: S1F1 = 0;   S1F2 = 1; S1F3 = 1;  S1F4=  0;   break;
             case 4: S1F1 = 0;   S1F2 = 0; S1F3 = 1;  S1F4=  0;   break;
             case 5: S1F1 = 0;   S1F2 = 0; S1F3 = 1;  S1F4=  1;   break;
             case 6: S1F1 = 0;   S1F2 = 0; S1F3 = 0;  S1F4=  1;   break;
             case 7: S1F1 = 1;   S1F2 = 0; S1F3 = 0;  S1F4=  1;   break;
            }
              Delay_ms(2);  //  500 halfsteps/sec     = ok !
            // Velocity(Speed);
          }        // for  Steps
        //   To lower power consumption (700 ma !! ) below is necessary :
             S1F1 = 0;   S1F2 = 0; S1F3 = 0;  S1F4=  0;
        }

       void ZSpeed(unsigned int ZStepsDone,unsigned int ZStepsToDo)
       {
         int x;
                 // SlowDown on start
                 // is maxmost 400+400+350+300+150 = 1.6 msec extra

                   if (ZStepsDone<10)    Delay_us(400);
                   if (ZStepsDone<20)    Delay_us(400);
                   if (ZStepsDone<40)    Delay_us(350);
                   if (ZStepsDone<80)    Delay_us(300);
                   if (ZStepsDone<160)    Delay_us(150);

              //  also some SlowDown on stop
                   if (ZStepsDone<10)    Delay_us(300);
                   if (ZStepsToDo<20)    Delay_us(300);
                   if (ZStepsToDo<40)    Delay_us(250);
                   if (ZStepsToDo<80)    Delay_us(200);
                   if (ZStepsToDo<160)    Delay_us(100);

           for (x=0;x<ZMotorDelay;x++) Delay_us(1);              // core
     }


       void Initialisation(void)
              {
              int x=0;
              
              // After power on, move always  Z  axis to top so it will not disturb XY movements!
              // only exception is when jumperZ is placed, for Z carriage with laser and without
              // Z motor
              //  Z AXIS HOME :
                        // No initialization,corner switch check if jumper is placed
                        
                if (JumperZ==1)   // jumper Z is not placed, Z motor is in use
                        {
                        DirZ=1;            //1 = upwards  0  downwards

                        while(1)
                             {
                        Delay_us(100);  // slow
                        StepZ=0;        //
                        Delay_us(100);       //  slow
                         StepZ=1;
                     if (ZCORNERSWITCH==0) break;
                              }
                              
                        DirZ=0;     // downwards
                     for(x=0;x<EscapeStepsZ;x++)   //2000=2.5 mm trap screw 8mm
                              {
                                   Delay_us(100);   //  slow  maxmost  2000 steps/sec - 2000 /16 = 125  steps /sec->250 full steps /sec
                                   StepZ=0;         //
                                   Delay_us(100);   // slow maxmost 2000 steps/sec
                                   StepZ=1;
                              }
                    //     }
                              
               //  Y AXIS HOME :
                          DirYR = 1;  // topwards
                          DirYL=DirYR;
                while(1)
                          {
                         Delay_us(100);  //  slow to synchronize Y bars !
                         StepYR=0;
                         StepYL=0;

                     Delay_us(100);  //  slow to synchronize Y bars !
                           
                    if (YRCORNERSWITCH==1) StepYR=1;
                    if (YLCORNERSWITCH==1) StepYL=1; // !!! was YR
                    if ((YRCORNERSWITCH==0) && (YLCORNERSWITCH==0)) break;
                           }
                           
                       DirYR = 0;      // frontwards    = to me
                       DirYL=DirYR;     // frontwards   = to me

               //  enough escape moves topwards so that BOTH Y switches are open again.

                  for(x=0;x<(EscapeStepsXY);x++)  //
                                {
                                   Delay_us(50);  //  slow  maxmost   5000 microsteps/sec
                                   StepYR=0;        //
                                   StepYL=0,
                                   Delay_us(50);       // slow
                                   StepYR=1;
                                   StepYL=1;
                                }

                  //  X AXIS HOME :
                      DirX=1;            // to left
                  while(1)
                     {
                        Delay_us(100);        // do it slowly
                        StepX=0;
                        Delay_us(100);       //  do it slowly
                
                    if (XLCORNERSWITCH==0) break;
                         StepX=1;
                       }
                     //  then enough escape steps to the right  so that X switch is open again
                     //  a full rotation = 200*16=3200 microsteps of pulley with GT2 and 20 teeths advances 40 mm.

                         DirX=0;     // to right

                      for(x=0;x<(EscapeStepsXY);x++)   // WAS 200
                                {
                                   Delay_us(50);   // slow
                                   StepX=0;         //
                                   Delay_us(50);   //  slow
                                   StepX=1;
                                }
                          }
                  return;
             }