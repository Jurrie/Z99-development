#line 1 "C:/Z99/MikrocontrollerCode/SourceCode/PIC18F45K40/PIC18F45K40.c"
#line 1 "c:/users/public/documents/mikroelektronika/mikroc pro for pic/include/built_in.h"
#line 44 "C:/Z99/MikrocontrollerCode/SourceCode/PIC18F45K40/PIC18F45K40.c"
 void MotorXY(unsigned int, unsigned int,unsigned short);


 void EscapeMotorXY(unsigned short,unsigned short,unsigned short,unsigned short);

 void Lift(unsigned int,unsigned int);


 void EscapeMotorZ(unsigned int);

 void ST28(unsigned int,unsigned int);

 void ZSpeed(unsigned int,unsigned int);

 void Initialisation(void);
 unsigned short UartByte;
 unsigned short Commands[12];
 unsigned short CommandIndex=0;

 int XYMotorDelay =25;
 int ZMotorDelay= 75;

unsigned short countTimerX=0,countTimerY=0;

void interrupt()
 {
 if (TMR0IF_bit){
 countTimerX++;
 TMR0IF_bit = 0;
 }
 if (TMR1IF_bit)
 {
 countTimerY++;
 TMR1IF_bit=0;
 }
 }

void main()
 {
 int x=0;
 int y=0;
 unsigned int StepsX=0;
 unsigned int StepsY=0;
 unsigned int StepsZ=0;
 unsigned int StepsD=0;

 unsigned short CheckSum = 0 ;
 unsigned short TestCheckSum=0;
 int CheckSumIndex=0;
 int xDir=0,yDir=0;

 unsigned long xLong=0,yLong=0;
 unsigned int part1=0,part2=0,part3=0;



 ANSELA = 0b00001100;




 ANSELB = 0x00;
 ANSELC = 0X00;
 ANSELD=0X00;
 ANSELE=0X00;

 TRISA = 0b00000010;



 TRISB = 0b11111111;
 TRISC = 0b10000000;
 TRISD = 0b00000000;
 TRISE = 0b00000000;

 LATA = 0b00000000;
 LATC = 0x00;
 LATD = 0b00000100;
 LATE = 0x00;

 UART1_Init(57600);

 Delay_ms(200);

 CommandIndex=0;




 TMR0H = 0xF9;
 TMR0L = 0xC0;
 GIE_bit = 1;
 countTimerY=0;
 TMR0IE_bit = 1;


 T1CON = 0x01;
 TMR1H = 0xF9;
 TMR1L = 0xC0;

 countTimerX =0;
 INTCON=0XC0;



 Initialisation();

 TMR1IE_bit =1;

 for(;;)
 {
 CommandIndex=0;
 while( CommandIndex < 6)
 {
 if (UART1_Data_Ready())
 {
 Commands[ CommandIndex ] = UART1_Read();
 CommandIndex++;
 }
 }

 if ( ( Commands[0] == 65) && (Commands[1] == 0) )
 {
 Uart1_Write(43);
 CommandIndex=0;
 continue;
 }
 CheckSum = Commands[0]+ Commands[1]+ Commands[2]+ Commands[3]+Commands[4];

 TestCheckSum = Commands[5];

 if ( TestCheckSum == CheckSum )
 {
 switch(Commands[0])
 {
 case 68:
 XYMotorDelay = (Commands[1] << 8 ) + Commands[2];
 Uart1_Write(43);
 CommandIndex=0;
 break;

 case 75:
 switch(Commands[1])
 {
 case 0:  LATD.F2 =0;
 break;
 case 1:  LATD.F2 =1;
 break;
 default: break;
 }
 Uart1_Write(43);
 CommandIndex=0;
 break;

 case 76:
 StepsZ = ( Commands[2] << 8) + Commands[3];
 Lift ( (unsigned int)Commands[1] ,(unsigned int) StepsZ );
 Uart1_Write(43);
 CommandIndex=0;
 break;


 case 84:
 StepsD = (Commands[2] << 8 ) + Commands[3];
 ST28(Commands[1], StepsD);
 Uart1_Write(43);
 CommandIndex=0;
 break;

 case 86:
 ZMotorDelay = (Commands[1] << 8 ) + Commands[2];
 Uart1_Write(43);
 CommandIndex=0;
 break;



 case 87:
  LATD.B0 =0;
  LATC.B0  = 0;
  LATC.B1 = LATC.B0 ;

 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,1);
 Uart1_Write(43);
 CommandIndex=0;
 break;
 case 89:
  LATD.B0 =0;
  LATC.B0  = 1;
  LATC.B1 = LATC.B0 ;
 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,1);
 Uart1_Write(43);
 CommandIndex=0;
 break;
 case 91:
  LATD.B0 =1;
  LATC.B0  = 1;
  LATC.B1 = LATC.B0 ;
 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,1);
 Uart1_Write(43);
 CommandIndex=0;
 break;
 case 93:
  LATD.B0 =1;
  LATC.B0  = 0;
  LATC.B1 = LATC.B0 ;
 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,1);
 Uart1_Write(43);
 CommandIndex=0;
 break;


 case 94:
  LATD.B0 =0;
  LATC.B0  = 0;
  LATC.B1 = LATC.B0 ;

 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,2);
 Uart1_Write(43);
 CommandIndex=0;
 break;
 case 95:
  LATD.B0 =0;
  LATC.B0  = 1;
  LATC.B1 = LATC.B0 ;
 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,2);

 Uart1_Write(43);
 CommandIndex=0;
 break;
 case 96:
  LATD.B0 =1;
  LATC.B0  = 1;
  LATC.B1 = LATC.B0 ;
 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,2);
 Uart1_Write(43);
 CommandIndex=0;
 break;
 case 97:
  LATD.B0 =1;
  LATC.B0  = 0;
  LATC.B1 = LATC.B0 ;
 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,2);
 Uart1_Write(43);
 CommandIndex=0;
 break;



 case 98:
  LATD.B0 =0;
  LATC.B0  = 0;
  LATC.B1 = LATC.B0 ;
 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,3);
 Uart1_Write(43);
 CommandIndex=0;
 break;
 case 99:
  LATD.B0 =0;
  LATC.B0  = 1;
  LATC.B1 = LATC.B0 ;
 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,3);
 Uart1_Write(43);
 CommandIndex=0;
 break;
 case 100:
  LATD.B0 =1;
  LATC.B0  = 1;
  LATC.B1 = LATC.B0 ;
 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,3);
 Uart1_Write(43);
 CommandIndex=0;
 break;
 case 101:
  LATD.B0 =1;
  LATC.B0  = 0;
  LATC.B1 = LATC.B0 ;
 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,3);
 Uart1_Write(43);
 CommandIndex=0;
 break;





 case 102:
  LATD.B0 =0;
  LATC.B0  = 0;
  LATC.B1 = LATC.B0 ;
 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,4);
 Uart1_Write(43);
 CommandIndex=0;
 break;
 case 103:
  LATD.B0 =0;
  LATC.B0  = 1;
  LATC.B1 = LATC.B0 ;
 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,4);
 Uart1_Write(43);
 CommandIndex=0;
 break;
 case 104:
  LATD.B0 =1;
  LATC.B0  = 1;
  LATC.B1 = LATC.B0 ;
 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,4);
 Uart1_Write(43);
 CommandIndex=0;
 break;
 case 105:
  LATD.B0 =1;
  LATC.B0  = 0;
  LATC.B1 = LATC.B0 ;
 StepsX = (unsigned int) ( (Commands[1] << 8 ) + Commands[2]);
 StepsY = (unsigned int) ( (Commands[3] << 8 )+Commands[4]);
 MotorXY(StepsX,StepsY,4);
 Uart1_Write(43);
 CommandIndex=0;
 break;



 case 110:
 EscapeMotorZ(2000);
 Uart1_Write(43);
 CommandIndex=0;
 break;

 case 111:
  LATD.B0 =0;
  LATC.B0  = 0;
  LATC.B1 = LATC.B0 ;
 EscapeMotorXY(Commands[1],Commands[2],Commands[3],Commands[4]);

 Uart1_Write(43);
 CommandIndex=0;
 break;

 case 113:
  LATD.B0 =1;
  LATC.B0  = 1;
  LATC.B1 = LATC.B0 ;
 EscapeMotorXY(Commands[1],Commands[2],Commands[3],Commands[4]);
 Uart1_Write(43);
 CommandIndex=0;
 break;

 default:
 Uart1_Write(63);

 CommandIndex=0;
 break;
 }
 }

 else
 {
 Uart1_Write(63);
 CommandIndex=0;
 }
 }
 }


 void MotorXY(unsigned int xSteps,unsigned int ySteps, unsigned short parts)
 {
 unsigned int M=0;
 unsigned long stepperDelayX=0;
 unsigned int timerStepperDelayX=0;
 unsigned long stepperDelayY=0;
 unsigned int timerStepperDelayY=0;
 unsigned int counterStepX=0;
 unsigned int counterStepY=0;
 unsigned int z=0;

 unsigned int PartOf1600;
 unsigned int PartOf160;

 if (xSteps==ySteps)
 {
 stepperDelayX=XYMotorDelay*2*15;
 timerStepperDelayX = (unsigned int)(65536-(unsigned int)(stepperdelayX));

 TMR0IE_bit=0;
 TMR0H= (timerStepperDelayX & 0XFF00)>>8;
 TMR0L= timerStepperDelayX & 0X00FF;
 TMR0IE_bit=1;

 for (M=0;M<parts;M++)
 {
 counterStepX=0;
 do
 {
 if ( PORTB.F1 ==0) { Uart1_Write(47); break; }
 if ( PORTB.F2 ==0) { Uart1_Write(48); break; }
 if ( PORTB.F3 ==0) { Uart1_Write(49); break; }
 if ( PORTB.F5 ==0) { Uart1_Write(46); break; }
 if ( PORTB.F4 ==0) { Uart1_Write(45);break; }


 if((countTimerX>0) && (counterStepX<xSteps))
 {
  LATD.B1 =1;
  LATC.B3 =1;
  LATC.B2 =1;
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
 for (z= 0; z < ( counterStepX - (xSteps - 1600) ) ; z+=160)
 delay_us(1);
 }

 TMR0IE_bit=0;
 TMR0H= (timerStepperDelayX & 0XFF00)>>8;
 TMR0L= timerStepperDelayX & 0X00FF;
 TMR0IE_bit=1;
 countTimerX=0;
  LATD.B1 =0;
  LATC.B2 =0;
  LATC.B3 =0;
 }
 } while (counterStepX < xSteps );
 }
 }

 else if (xSteps==0)
 {


 stepperDelayY=XYMotorDelay*35;
 timerStepperDelayY = (unsigned int)(65536-(unsigned int)(stepperdelayY));

 TMR1IE_bit=0;
 TMR1H= (timerStepperDelayY & 0XFF00)>>8;
 TMR1L= timerStepperDelayY & 0X00FF;
 TMR1IE_bit=1;

 for (M=0;M<parts;M++)
 {
 counterStepY=0;
 do
 {
 if ( PORTB.F1 ==0) { Uart1_Write(47); break; }
 if ( PORTB.F2 ==0) { Uart1_Write(48); break; }
 if ( PORTB.F3 ==0) { Uart1_Write(49); break; }


 if((countTimerY>0) && (counterStepY<ySteps))
 {
  LATC.B3 =1;
  LATC.B2 =1;
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
 for (z= 0; z < ( counterStepY - (ySteps - 1600) ) ; z+=160)
 delay_us(1);
 }
 TMR1IE_bit=0;
 TMR1H= (timerStepperDelayY & 0XFF00)>>8;
 TMR1L= timerStepperDelayY & 0X00FF;
 TMR1IE_bit=1;
 countTimerY=0;
  LATC.B3 =0;
  LATC.B2 =0;
 }
 } while (counterStepY < ySteps );
 }
 }

 else if (ySteps==0)
 {

 stepperDelayX=XYMotorDelay*35;
 timerStepperDelayX = (unsigned int)(65536-(unsigned int)(stepperdelayX));

 TMR0IE_bit=0;
 TMR0H= (timerStepperDelayX & 0XFF00)>>8;
 TMR0L= timerStepperDelayX & 0X00FF;
 TMR0IE_bit=1;


 for (M=0;M<parts;M++)
 {
 counterStepX=0;
 counterStepY=0;

 do
 {
 if ( PORTB.F5 ==0) { Uart1_Write(46); break; }
 if ( PORTB.F4 ==0) { Uart1_Write(45);break; }


 if((countTimerX>0) && (counterStepX<xSteps))
 {
  LATD.B1 =1;
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
 for (z= 0; z < ( counterStepX - (xSteps - 1600) ) ; z+=160)
 delay_us(1);
 }
 TMR0IE_bit=0;
 TMR0H= (timerStepperDelayX & 0XFF00)>>8;
 TMR0L= timerStepperDelayX & 0X00FF;
 TMR0IE_bit=1;
 countTimerX=0;
  LATD.B1 =0;
 }

 } while (counterStepX < xSteps );

 }
 }




 else if (xSteps>ySteps)
 {

 partof1600 = (unsigned int)(((long)1600*(long)ySteps)/(long)xSteps) ;
 partOf160 = (unsigned int)(((long)partOf1600*(long)ySteps)/(long)xSteps) ;
 if (partOf160==0) partOf160=1;

 stepperDelayX=XYMotorDelay*2*15;




 stepperDelayY=(unsigned int)(((long)stepperDelayX*(long)xSteps)/(long)ySteps);

 timerStepperDelayX = (unsigned int)(65536-(unsigned int)(stepperdelayX));
 timerStepperDelayY = (unsigned int)(65536-(unsigned int)(stepperdelayY) );

 TMR0IE_bit=0;
 TMR0H= (timerStepperDelayX & 0XFF00)>>8;
 TMR0L= timerStepperDelayX & 0X00FF;
 TMR0IE_bit=1;

 TMR1IE_bit=0;
 TMR1H= (timerStepperDelayY & 0XFF00)>>8;
 TMR1L= timerStepperDelayY & 0X00FF;
 TMR1IE_bit=1;

 for (M=0;M<parts;M++)
 {
 counterStepX=0;
 counterStepY=0;

 do
 {
 if ( PORTB.F1 ==0) { Uart1_Write(47); break; }
 if ( PORTB.F2 ==0) { Uart1_Write(48); break; }
 if ( PORTB.F3 ==0) { Uart1_Write(49); break; }
 if ( PORTB.F5 ==0) { Uart1_Write(46); break; }
 if ( PORTB.F4 ==0) { Uart1_Write(45);break; }


 if((countTimerX>0) && (counterStepX<xSteps))
 {
  LATD.B1 =1;
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
 for (z= 0; z < ( counterStepX - (xSteps - 1600) ) ; z+=160)
 delay_us(1);
 }

 TMR0IE_bit=0;
 TMR0H= (timerStepperDelayX & 0XFF00)>>8;
 TMR0L= timerStepperDelayX & 0X00FF;
 TMR0IE_bit=1;
 countTimerX=0;
  LATD.B1 =0;
 }


 if ((countTimerY>0)&&(counterStepY<ySteps))
 {
  LATC.B3 =1;
  LATC.B2 =1;
 counterStepY++;



 if (M==0)
 {
 if (counterStepY< partOf1600)
 for (z=counterStepY; z <(partOf1600- counterStepY) ;z+=partOf160)
 delay_us(1);
 }

 if (M==(parts-1))
 {
 if (counterStepY> ( ySteps- partOf1600))
 for (z= 0; z < ( counterStepY - (ySteps - partOf1600) ) ;z+=partOf160)
 delay_us(1);
 }



 TMR1IE_bit=0;
 TMR1H= (timerStepperDelayY & 0XFF00)>>8;
 TMR1L= timerStepperDelayY & 0X00FF;
 countTimerY=0;

  LATC.B3 =0;
  LATC.B2 =0;
 TMR1IE_bit=1;
 }

 } while (counterStepX < xSteps );


 }
 }



 else if (ySteps>xSteps)
 {


 partof1600 = (unsigned int)(((long)1600*(long)xSteps)/(long)ySteps) ;
 partOf160 = (unsigned int)(((long)partOf1600*(long)xSteps)/(long)ySteps) ;
 if (partOf160==0) partOf160=1;



 stepperDelayY=XYMotorDelay*2*15;

 stepperDelayX=(unsigned int)(((long)stepperDelayY*(long)ySteps)/(long)xSteps);
 timerStepperDelayX = (unsigned int)(65536-(unsigned int)(stepperdelayX));
 timerStepperDelayY = (unsigned int)(65536-(unsigned int)(stepperdelayY) );

 TMR0IE_bit=0;
 TMR0H= (timerStepperDelayX & 0XFF00)>>8;
 TMR0L= timerStepperDelayX & 0X00FF;
 TMR0IE_bit=1;

 TMR1IE_bit=0;
 TMR1H= (timerStepperDelayY & 0XFF00)>>8;
 TMR1L= timerStepperDelayY & 0X00FF;
 TMR1IE_bit=1;

 for (M=0;M<parts;M++)
 {
 counterStepX=0;
 counterStepY=0;

 do
 {
 if ( PORTB.F1 ==0) { Uart1_Write(47); break; }
 if ( PORTB.F2 ==0) { Uart1_Write(48); break; }
 if ( PORTB.F3 ==0) { Uart1_Write(49); break; }
 if ( PORTB.F5 ==0) { Uart1_Write(46); break; }
 if ( PORTB.F4 ==0) { Uart1_Write(45);break; }


 if((countTimerY>0) && (counterStepY<ySteps))
 {
  LATC.B3 =1;
  LATC.B2 =1;
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
 for (z= 0; z < ( counterStepY - (ySteps - 1600) ) ; z+=160)
 delay_us(1);
 }

 TMR1IE_bit=0;
 TMR1H= (timerStepperDelayY & 0XFF00)>>8;
 TMR1L= timerStepperDelayY & 0X00FF;
 TMR1IE_bit=1;
 countTimerY=0;
  LATC.B3 =0;
  LATC.B2 =0;
 }



 if ((countTimerX>0)&&(counterStepX<xSteps))
 {
  LATD.B1 =1;
 counterStepX++;


 if (M==0)
 {
 if (counterStepX<partOf1600)
 for (z=counterStepX; z <(partOf1600- counterStepX) ;z+=partOf160)
 delay_us(1);
 }

 if (M==(parts-1))
 {
 if (counterStepX> ( ySteps- partOf1600))
 for (z= 0; z < ( counterStepX - (xSteps - partOf1600) ); z+=partOf160)
 delay_us(1);
 }


 TMR0IE_bit=0;
 TMR0H= (timerStepperDelayX & 0XFF00)>>8;
 TMR0L= timerStepperDelayX & 0X00FF;
 countTimerX=0;

  LATD.B1 =0;
 TMR0IE_bit=1;
 }

 } while (counterStepY < ySteps );
 }
 }
 }



 void EscapeMotorXY(unsigned short command1,unsigned short command2,unsigned short command3,
 unsigned short command4)
 {
 unsigned int xSteps,ySteps;
 unsigned int L = 0;

 xSteps = (unsigned int) ( (command1 << 8 ) + command2);
 ySteps = (unsigned int) ( (command3 << 8 )+ command4);

 if (xSteps==0)
 {
 for (L=0;L<ySteps;L++)
 {
  LATC.B3 =1;
  LATC.B2 =1;
 Delay_us(200);
  LATC.B3 =0;
  LATC.B2 =0;
 Delay_us(200);
 }
 }
 else if (ySteps==0)
 {
 for (L=0;L<xSteps;L++)
 {
  LATD.B1 =1;
 Delay_us(200);
  LATD.B1 =0;
 Delay_us(200);
 }
 }
 }


 void Lift(unsigned int Direction, unsigned int Steps)
 {
 unsigned int x;
 if (Direction==0)  LATE.B0 =0;
 else  LATE.B0 =1;
 for (x=0;x<Steps;x++)
 {
  LATA.B5 =0;
 ZSpeed(x,(Steps-x));
  LATA.B5 =1;
 ZSpeed(x,(Steps-x));
 if ( PORTB.F0 ==0)
 {
 Uart1_Write(44);
 break;
 }
 }
 }

 void EscapeMotorZ(unsigned int Steps)
 {
 unsigned int x;
  LATE.B0 =0;
 for (x=0;x<Steps;x++)
 {
  LATA.B5 =0;
 Delay_us(200);
  LATA.B5 =1;
 Delay_us(200);
 }
 }


 void ST28( unsigned int Direction,unsigned int Steps)
 {
 int x;
 int stepsTune =0;

 for (x=0;x<Steps;x++)
 {
 if (Direction ==1 ) stepsTune = stepsTune + 1;
 else stepsTune = stepsTune - 1;
 if (stepsTune>7) stepsTune = 0;
 if (stepsTune<0) stepsTune = 7;
 switch(stepsTune)
 {
 case 0: LATD.F7  = 1;  LATD.F6  = 0;  LATD.F5  = 0;  LATD.F4 = 0; break;
 case 1:  LATD.F7  = 1;  LATD.F6  = 1;  LATD.F5  = 0;  LATD.F4 = 0; break;
 case 2:  LATD.F7  = 0;  LATD.F6  = 1;  LATD.F5  = 0;  LATD.F4 = 0; break;
 case 3:  LATD.F7  = 0;  LATD.F6  = 1;  LATD.F5  = 1;  LATD.F4 = 0; break;
 case 4:  LATD.F7  = 0;  LATD.F6  = 0;  LATD.F5  = 1;  LATD.F4 = 0; break;
 case 5:  LATD.F7  = 0;  LATD.F6  = 0;  LATD.F5  = 1;  LATD.F4 = 1; break;
 case 6:  LATD.F7  = 0;  LATD.F6  = 0;  LATD.F5  = 0;  LATD.F4 = 1; break;
 case 7:  LATD.F7  = 1;  LATD.F6  = 0;  LATD.F5  = 0;  LATD.F4 = 1; break;
 }
 Delay_ms(2);

 }

  LATD.F7  = 0;  LATD.F6  = 0;  LATD.F5  = 0;  LATD.F4 = 0;
 }

 void ZSpeed(unsigned int ZStepsDone,unsigned int ZStepsToDo)
 {
 int x;



 if (ZStepsDone<10) Delay_us(400);
 if (ZStepsDone<20) Delay_us(400);
 if (ZStepsDone<40) Delay_us(350);
 if (ZStepsDone<80) Delay_us(300);
 if (ZStepsDone<160) Delay_us(150);


 if (ZStepsDone<10) Delay_us(300);
 if (ZStepsToDo<20) Delay_us(300);
 if (ZStepsToDo<40) Delay_us(250);
 if (ZStepsToDo<80) Delay_us(200);
 if (ZStepsToDo<160) Delay_us(100);

 for (x=0;x<ZMotorDelay;x++) Delay_us(1);
 }


 void Initialisation(void)
 {
 int x=0;







 if ( PORTA.F1 ==1)
 {
  LATE.B0 =1;

 while(1)
 {
 Delay_us(100);
  LATA.B5 =0;
 Delay_us(100);
  LATA.B5 =1;
 if ( PORTB.F0 ==0) break;
 }

  LATE.B0 =0;
 for(x=0;x< 2000 ;x++)
 {
 Delay_us(100);
  LATA.B5 =0;
 Delay_us(100);
  LATA.B5 =1;
 }



  LATC.B0  = 1;
  LATC.B1 = LATC.B0 ;
 while(1)
 {
 Delay_us(100);
  LATC.B3 =0;
  LATC.B2 =0;

 Delay_us(100);

 if ( PORTB.F2 ==1)  LATC.B3 =1;
 if ( PORTB.F1 ==1)  LATC.B2 =1;
 if (( PORTB.F2 ==0) && ( PORTB.F1 ==0)) break;
 }

  LATC.B0  = 0;
  LATC.B1 = LATC.B0 ;



 for(x=0;x<( 600 );x++)
 {
 Delay_us(50);
  LATC.B3 =0;
  LATC.B2 =0,
 Delay_us(50);
  LATC.B3 =1;
  LATC.B2 =1;
 }


  LATD.B0 =1;
 while(1)
 {
 Delay_us(100);
  LATD.B1 =0;
 Delay_us(100);

 if ( PORTB.F4 ==0) break;
  LATD.B1 =1;
 }



  LATD.B0 =0;

 for(x=0;x<( 600 );x++)
 {
 Delay_us(50);
  LATD.B1 =0;
 Delay_us(50);
  LATD.B1 =1;
 }
 }
 return;
 }
