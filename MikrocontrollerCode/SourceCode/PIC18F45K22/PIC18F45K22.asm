
_interrupt:

;PIC18F45K22.c,67 :: 		void interrupt()
;PIC18F45K22.c,69 :: 		if (TMR0IF_bit){
	BTFSS       TMR0IF_bit+0, BitPos(TMR0IF_bit+0) 
	GOTO        L_interrupt0
;PIC18F45K22.c,70 :: 		countTimerX++;
	INCF        _countTimerX+0, 1 
;PIC18F45K22.c,71 :: 		TMR0IF_bit = 0;         //  clear flag
	BCF         TMR0IF_bit+0, BitPos(TMR0IF_bit+0) 
;PIC18F45K22.c,72 :: 		}
L_interrupt0:
;PIC18F45K22.c,73 :: 		if (TMR1IF_bit)
	BTFSS       TMR1IF_bit+0, BitPos(TMR1IF_bit+0) 
	GOTO        L_interrupt1
;PIC18F45K22.c,75 :: 		countTimerY++;
	INCF        _countTimerY+0, 1 
;PIC18F45K22.c,76 :: 		TMR1IF_bit=0;           //  clear flag
	BCF         TMR1IF_bit+0, BitPos(TMR1IF_bit+0) 
;PIC18F45K22.c,77 :: 		}
L_interrupt1:
;PIC18F45K22.c,78 :: 		}
L_end_interrupt:
L__interrupt323:
	RETFIE      1
; end of _interrupt

_main:

;PIC18F45K22.c,80 :: 		void main()
;PIC18F45K22.c,82 :: 		int x=0;
;PIC18F45K22.c,83 :: 		int y=0;
;PIC18F45K22.c,84 :: 		unsigned int StepsX=0;
;PIC18F45K22.c,85 :: 		unsigned int StepsY=0;
;PIC18F45K22.c,86 :: 		unsigned int StepsZ=0;
;PIC18F45K22.c,87 :: 		unsigned int StepsD=0;    // solder paste dispenser
;PIC18F45K22.c,89 :: 		unsigned short CheckSum = 0 ;
;PIC18F45K22.c,90 :: 		unsigned short TestCheckSum=0;
;PIC18F45K22.c,91 :: 		int CheckSumIndex=0;
;PIC18F45K22.c,92 :: 		int xDir=0,yDir=0;
;PIC18F45K22.c,94 :: 		unsigned long xLong=0,yLong=0;
;PIC18F45K22.c,95 :: 		unsigned int part1=0,part2=0,part3=0;
;PIC18F45K22.c,99 :: 		ANSELA = 0b00001100;   //   Configure A2 and A3 pin as analog for MPX5010DP
	MOVLW       12
	MOVWF       ANSELA+0 
;PIC18F45K22.c,104 :: 		ANSELB = 0x00;        //   Configure PORTB pins as digital
	CLRF        ANSELB+0 
;PIC18F45K22.c,105 :: 		ANSELC = 0X00;        //   Configure PORTC pins as digital
	CLRF        ANSELC+0 
;PIC18F45K22.c,106 :: 		ANSELD=0X00;
	CLRF        ANSELD+0 
;PIC18F45K22.c,107 :: 		ANSELE=0X00;
	CLRF        ANSELE+0 
;PIC18F45K22.c,109 :: 		TRISA  = 0b00000010;      //  A1 = DIGITAL INPUT FOR Z JUMPER
	MOVLW       2
	MOVWF       TRISA+0 
;PIC18F45K22.c,113 :: 		TRISB = 0b11111111;      // All input for corner switches
	MOVLW       255
	MOVWF       TRISB+0 
;PIC18F45K22.c,114 :: 		TRISC = 0b10000000;      //   C7 = RX van UART1
	MOVLW       128
	MOVWF       TRISC+0 
;PIC18F45K22.c,115 :: 		TRISD = 0b00000000;     // = all output
	CLRF        TRISD+0 
;PIC18F45K22.c,116 :: 		TRISE = 0b00000000;     // = all output
	CLRF        TRISE+0 
;PIC18F45K22.c,118 :: 		LATA = 0b00000000;
	CLRF        LATA+0 
;PIC18F45K22.c,119 :: 		LATC = 0x00;
	CLRF        LATC+0 
;PIC18F45K22.c,120 :: 		LATD = 0b00000100;  // was 0x00  TURN LASER OFF
	MOVLW       4
	MOVWF       LATD+0 
;PIC18F45K22.c,121 :: 		LATE = 0x00;
	CLRF        LATE+0 
;PIC18F45K22.c,123 :: 		UART1_Init(57600);   // 57600
	BSF         BAUDCON+0, 3, 0
	CLRF        SPBRGH+0 
	MOVLW       255
	MOVWF       SPBRG+0 
	BSF         TXSTA+0, 2, 0
	CALL        _UART1_Init+0, 0
;PIC18F45K22.c,125 :: 		Delay_ms(200);          //  Wait for UART module to stabilize
	MOVLW       15
	MOVWF       R11, 0
	MOVLW       246
	MOVWF       R12, 0
	MOVLW       251
	MOVWF       R13, 0
L_main2:
	DECFSZ      R13, 1, 1
	BRA         L_main2
	DECFSZ      R12, 1, 1
	BRA         L_main2
	DECFSZ      R11, 1, 1
	BRA         L_main2
;PIC18F45K22.c,127 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,131 :: 		T0CON        = 0x88; // / no prescaler
	MOVLW       136
	MOVWF       T0CON+0 
;PIC18F45K22.c,133 :: 		TMR0H        = 0xF9; //+-100usec
	MOVLW       249
	MOVWF       TMR0H+0 
;PIC18F45K22.c,134 :: 		TMR0L        = 0xC0;  // +- 100 usec
	MOVLW       192
	MOVWF       TMR0L+0 
;PIC18F45K22.c,135 :: 		GIE_bit      = 1;
	BSF         GIE_bit+0, BitPos(GIE_bit+0) 
;PIC18F45K22.c,136 :: 		countTimerY=0;
	CLRF        _countTimerY+0 
;PIC18F45K22.c,137 :: 		TMR0IE_bit    = 1;   // enable timer 0 interrupt
	BSF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,140 :: 		T1CON         = 0x01; // no prescaler
	MOVLW       1
	MOVWF       T1CON+0 
;PIC18F45K22.c,141 :: 		TMR1H         = 0xF9;  // +- 100 usec
	MOVLW       249
	MOVWF       TMR1H+0 
;PIC18F45K22.c,142 :: 		TMR1L         = 0xC0;  // +- 100 usec
	MOVLW       192
	MOVWF       TMR1L+0 
;PIC18F45K22.c,144 :: 		countTimerX   =0;
	CLRF        _countTimerX+0 
;PIC18F45K22.c,145 :: 		INTCON=0XC0; // sety GIE, PEIE
	MOVLW       192
	MOVWF       INTCON+0 
;PIC18F45K22.c,151 :: 		Initialisation();      // Will home Z Y and X axis in this order
	CALL        _Initialisation+0, 0
;PIC18F45K22.c,158 :: 		TMR1IE_bit    =1;      // enable timer 1 interrupt
	BSF         TMR1IE_bit+0, BitPos(TMR1IE_bit+0) 
;PIC18F45K22.c,160 :: 		for(;;)
L_main3:
;PIC18F45K22.c,162 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,163 :: 		while( CommandIndex < 6)      //    Go for this without interrupts
L_main6:
	MOVLW       6
	SUBWF       _CommandIndex+0, 0 
	BTFSC       STATUS+0, 0 
	GOTO        L_main7
;PIC18F45K22.c,165 :: 		if (UART1_Data_Ready())
	CALL        _UART1_Data_Ready+0, 0
	MOVF        R0, 1 
	BTFSC       STATUS+0, 2 
	GOTO        L_main8
;PIC18F45K22.c,167 :: 		Commands[ CommandIndex ] = UART1_Read();
	MOVLW       _Commands+0
	MOVWF       FLOC__main+0 
	MOVLW       hi_addr(_Commands+0)
	MOVWF       FLOC__main+1 
	MOVF        _CommandIndex+0, 0 
	ADDWF       FLOC__main+0, 1 
	BTFSC       STATUS+0, 0 
	INCF        FLOC__main+1, 1 
	CALL        _UART1_Read+0, 0
	MOVFF       FLOC__main+0, FSR1L+0
	MOVFF       FLOC__main+1, FSR1H+0
	MOVF        R0, 0 
	MOVWF       POSTINC1+0 
;PIC18F45K22.c,168 :: 		CommandIndex++;
	INCF        _CommandIndex+0, 1 
;PIC18F45K22.c,169 :: 		}
L_main8:
;PIC18F45K22.c,170 :: 		}
	GOTO        L_main6
L_main7:
;PIC18F45K22.c,172 :: 		if (  ( Commands[0] == 65) && (Commands[1] == 0)    )
	MOVF        _Commands+0, 0 
	XORLW       65
	BTFSS       STATUS+0, 2 
	GOTO        L_main11
	MOVF        _Commands+1, 0 
	XORLW       0
	BTFSS       STATUS+0, 2 
	GOTO        L_main11
L__main313:
;PIC18F45K22.c,174 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,175 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,176 :: 		continue;  // soft will continue on top
	GOTO        L_main5
;PIC18F45K22.c,177 :: 		}
L_main11:
;PIC18F45K22.c,178 :: 		CheckSum = Commands[0]+  Commands[1]+ Commands[2]+ Commands[3]+Commands[4];  //
	MOVF        _Commands+1, 0 
	ADDWF       _Commands+0, 0 
	MOVWF       R0 
	MOVF        _Commands+2, 0 
	ADDWF       R0, 1 
	MOVF        _Commands+3, 0 
	ADDWF       R0, 1 
	MOVF        _Commands+4, 0 
	ADDWF       R0, 0 
	MOVWF       R1 
;PIC18F45K22.c,182 :: 		if ( TestCheckSum ==  CheckSum   )
	MOVF        _Commands+5, 0 
	XORWF       R1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L_main12
;PIC18F45K22.c,184 :: 		switch(Commands[0])
	GOTO        L_main13
;PIC18F45K22.c,186 :: 		case 68:   //   D   = MotorDelay, speed, steps /sec of XY steppers
L_main15:
;PIC18F45K22.c,187 :: 		XYMotorDelay = (Commands[1] << 8 ) + Commands[2];
	MOVF        _Commands+1, 0 
	MOVWF       _XYMotorDelay+1 
	CLRF        _XYMotorDelay+0 
	MOVF        _Commands+2, 0 
	ADDWF       _XYMotorDelay+0, 1 
	MOVLW       0
	ADDWFC      _XYMotorDelay+1, 1 
;PIC18F45K22.c,188 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,189 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,190 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,192 :: 		case 75: // Laser
L_main16:
;PIC18F45K22.c,193 :: 		switch(Commands[1])
	GOTO        L_main17
;PIC18F45K22.c,195 :: 		case 0: LASER=0;          //= ON       Pin 21 of PIC D.F2 low
L_main19:
	BCF         LATD+0, 2 
;PIC18F45K22.c,196 :: 		break;
	GOTO        L_main18
;PIC18F45K22.c,197 :: 		case 1: LASER=1;        // = OFF     Pin 21 of PIC D.F2 high
L_main20:
	BSF         LATD+0, 2 
;PIC18F45K22.c,198 :: 		break;
	GOTO        L_main18
;PIC18F45K22.c,199 :: 		default: break;
L_main21:
	GOTO        L_main18
;PIC18F45K22.c,200 :: 		}
L_main17:
	MOVF        _Commands+1, 0 
	XORLW       0
	BTFSC       STATUS+0, 2 
	GOTO        L_main19
	MOVF        _Commands+1, 0 
	XORLW       1
	BTFSC       STATUS+0, 2 
	GOTO        L_main20
	GOTO        L_main21
L_main18:
;PIC18F45K22.c,201 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,202 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,203 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,205 :: 		case 76:  // LIFT
L_main22:
;PIC18F45K22.c,206 :: 		StepsZ = ( Commands[2]  << 8) + Commands[3];
	MOVF        _Commands+2, 0 
	MOVWF       FARG_Lift+1 
	CLRF        FARG_Lift+0 
	MOVF        _Commands+3, 0 
	ADDWF       FARG_Lift+0, 1 
	MOVLW       0
	ADDWFC      FARG_Lift+1, 1 
;PIC18F45K22.c,207 :: 		Lift ( (unsigned int)Commands[1]  ,(unsigned int) StepsZ );
	MOVF        _Commands+1, 0 
	MOVWF       FARG_Lift+0 
	MOVLW       0
	MOVWF       FARG_Lift+1 
	CALL        _Lift+0, 0
;PIC18F45K22.c,208 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,209 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,210 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,213 :: 		case 84:
L_main23:
;PIC18F45K22.c,214 :: 		StepsD =  (Commands[2] << 8 ) + Commands[3];
	MOVF        _Commands+2, 0 
	MOVWF       FARG_ST28+1 
	CLRF        FARG_ST28+0 
	MOVF        _Commands+3, 0 
	ADDWF       FARG_ST28+0, 1 
	MOVLW       0
	ADDWFC      FARG_ST28+1, 1 
;PIC18F45K22.c,215 :: 		ST28(Commands[1], StepsD);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_ST28+0 
	MOVLW       0
	MOVWF       FARG_ST28+1 
	CALL        _ST28+0, 0
;PIC18F45K22.c,216 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,217 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,218 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,220 :: 		case 86: // speed of Z motor;
L_main24:
;PIC18F45K22.c,221 :: 		ZMotorDelay = (Commands[1] << 8 ) + Commands[2];
	MOVF        _Commands+1, 0 
	MOVWF       _ZMotorDelay+1 
	CLRF        _ZMotorDelay+0 
	MOVF        _Commands+2, 0 
	ADDWF       _ZMotorDelay+0, 1 
	MOVLW       0
	ADDWFC      _ZMotorDelay+1, 1 
;PIC18F45K22.c,222 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,223 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,224 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,228 :: 		case 87:         //  XY direction to left front corner
L_main25:
;PIC18F45K22.c,229 :: 		DirX=0;
	BCF         LATD+0, 0 
;PIC18F45K22.c,230 :: 		DirYR = 0;
	BCF         LATC+0, 0 
;PIC18F45K22.c,231 :: 		DirYL=DirYR;          // Direction is the same on both Y motors,
	BTFSC       LATC+0, 0 
	GOTO        L__main325
	BCF         LATC+0, 1 
	GOTO        L__main326
L__main325:
	BSF         LATC+0, 1 
L__main326:
;PIC18F45K22.c,233 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,234 :: 		StepsY =  (unsigned int)  ( (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,235 :: 		MotorXY(StepsX,StepsY,1);  // 1 =  parts
	MOVLW       1
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,236 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,237 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,238 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,239 :: 		case 89:                   // XY direction to left and back
L_main26:
;PIC18F45K22.c,240 :: 		DirX=0;
	BCF         LATD+0, 0 
;PIC18F45K22.c,241 :: 		DirYR = 1;
	BSF         LATC+0, 0 
;PIC18F45K22.c,242 :: 		DirYL=DirYR;
	BTFSC       LATC+0, 0 
	GOTO        L__main327
	BCF         LATC+0, 1 
	GOTO        L__main328
L__main327:
	BSF         LATC+0, 1 
L__main328:
;PIC18F45K22.c,243 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,244 :: 		StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,245 :: 		MotorXY(StepsX,StepsY,1);   // 1 1 =slowstart slowstop
	MOVLW       1
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,246 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,247 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,248 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,249 :: 		case 91:                   //      to right uppercorner
L_main27:
;PIC18F45K22.c,250 :: 		DirX=1;
	BSF         LATD+0, 0 
;PIC18F45K22.c,251 :: 		DirYR = 1;
	BSF         LATC+0, 0 
;PIC18F45K22.c,252 :: 		DirYL=DirYR;          // Direction is the same on both Y motors, one coil is crossed over on the PCB or connection to Y motor !
	BTFSC       LATC+0, 0 
	GOTO        L__main329
	BCF         LATC+0, 1 
	GOTO        L__main330
L__main329:
	BSF         LATC+0, 1 
L__main330:
;PIC18F45K22.c,253 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,254 :: 		StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,255 :: 		MotorXY(StepsX,StepsY,1);    // 1 1 =slowstart slowstop
	MOVLW       1
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,256 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,257 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,258 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,259 :: 		case 93:                   //   to right inferior corner
L_main28:
;PIC18F45K22.c,260 :: 		DirX=1;
	BSF         LATD+0, 0 
;PIC18F45K22.c,261 :: 		DirYR = 0;
	BCF         LATC+0, 0 
;PIC18F45K22.c,262 :: 		DirYL=DirYR;
	BTFSC       LATC+0, 0 
	GOTO        L__main331
	BCF         LATC+0, 1 
	GOTO        L__main332
L__main331:
	BSF         LATC+0, 1 
L__main332:
;PIC18F45K22.c,263 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,264 :: 		StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,265 :: 		MotorXY(StepsX,StepsY,1);  // 1 =slowstart slowstop
	MOVLW       1
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,266 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,267 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,268 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,271 :: 		case 94:         //  Dir 1    W  of  XY dir to right and inferior corner
L_main29:
;PIC18F45K22.c,272 :: 		DirX=0;
	BCF         LATD+0, 0 
;PIC18F45K22.c,273 :: 		DirYR = 0;
	BCF         LATC+0, 0 
;PIC18F45K22.c,274 :: 		DirYL=DirYR;          // Direction is the same on both Y motors, one coil is crossed over on the PCB or connection to Y motor !
	BTFSC       LATC+0, 0 
	GOTO        L__main333
	BCF         LATC+0, 1 
	GOTO        L__main334
L__main333:
	BSF         LATC+0, 1 
L__main334:
;PIC18F45K22.c,276 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,277 :: 		StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,278 :: 		MotorXY(StepsX,StepsY,2);   // 2 = 2 parts
	MOVLW       2
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,279 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,280 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,281 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,282 :: 		case 95:                   // DIR 3 dir to right and above
L_main30:
;PIC18F45K22.c,283 :: 		DirX=0;
	BCF         LATD+0, 0 
;PIC18F45K22.c,284 :: 		DirYR = 1;
	BSF         LATC+0, 0 
;PIC18F45K22.c,285 :: 		DirYL=DirYR;          //
	BTFSC       LATC+0, 0 
	GOTO        L__main335
	BCF         LATC+0, 1 
	GOTO        L__main336
L__main335:
	BSF         LATC+0, 1 
L__main336:
;PIC18F45K22.c,286 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,287 :: 		StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,288 :: 		MotorXY(StepsX,StepsY,2);  // 2 = 2 parts
	MOVLW       2
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,290 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,291 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,292 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,293 :: 		case 96:                   //   Dir 5    to upper and left
L_main31:
;PIC18F45K22.c,294 :: 		DirX=1;  //
	BSF         LATD+0, 0 
;PIC18F45K22.c,295 :: 		DirYR = 1;
	BSF         LATC+0, 0 
;PIC18F45K22.c,296 :: 		DirYL=DirYR;          // Direction is the same on both Y motors, one coil is crossed over on the PCB or connection to Y motor !
	BTFSC       LATC+0, 0 
	GOTO        L__main337
	BCF         LATC+0, 1 
	GOTO        L__main338
L__main337:
	BSF         LATC+0, 1 
L__main338:
;PIC18F45K22.c,297 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,298 :: 		StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,299 :: 		MotorXY(StepsX,StepsY,2);  // 2 = 2 parts
	MOVLW       2
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,300 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,301 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,302 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,303 :: 		case 97:                   //  Dir 7 to left and inferior
L_main32:
;PIC18F45K22.c,304 :: 		DirX=1;
	BSF         LATD+0, 0 
;PIC18F45K22.c,305 :: 		DirYR = 0;
	BCF         LATC+0, 0 
;PIC18F45K22.c,306 :: 		DirYL=DirYR;          //
	BTFSC       LATC+0, 0 
	GOTO        L__main339
	BCF         LATC+0, 1 
	GOTO        L__main340
L__main339:
	BSF         LATC+0, 1 
L__main340:
;PIC18F45K22.c,307 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,308 :: 		StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,309 :: 		MotorXY(StepsX,StepsY,2);  // 2 = 2 parts
	MOVLW       2
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,310 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,311 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,312 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,316 :: 		case 98:         //  Dir 1    W  of  XY dir to right and inferior corner
L_main33:
;PIC18F45K22.c,317 :: 		DirX=0;
	BCF         LATD+0, 0 
;PIC18F45K22.c,318 :: 		DirYR = 0;
	BCF         LATC+0, 0 
;PIC18F45K22.c,319 :: 		DirYL=DirYR;          // Direction is the same on both Y motors, one coil is crossed over on the PCB or connection to Y motor !
	BTFSC       LATC+0, 0 
	GOTO        L__main341
	BCF         LATC+0, 1 
	GOTO        L__main342
L__main341:
	BSF         LATC+0, 1 
L__main342:
;PIC18F45K22.c,320 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,321 :: 		StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,322 :: 		MotorXY(StepsX,StepsY,3);  //3 = 3 parts
	MOVLW       3
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,323 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,324 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,325 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,326 :: 		case 99:                   // DIR 3 dir to right and above
L_main34:
;PIC18F45K22.c,327 :: 		DirX=0;
	BCF         LATD+0, 0 
;PIC18F45K22.c,328 :: 		DirYR = 1;
	BSF         LATC+0, 0 
;PIC18F45K22.c,329 :: 		DirYL=DirYR;          //
	BTFSC       LATC+0, 0 
	GOTO        L__main343
	BCF         LATC+0, 1 
	GOTO        L__main344
L__main343:
	BSF         LATC+0, 1 
L__main344:
;PIC18F45K22.c,330 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,331 :: 		StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,332 :: 		MotorXY(StepsX,StepsY,3);  //3 = 3 parts
	MOVLW       3
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,333 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,334 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,335 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,336 :: 		case 100:                   //   Dir 5    to upper and left
L_main35:
;PIC18F45K22.c,337 :: 		DirX=1;  //
	BSF         LATD+0, 0 
;PIC18F45K22.c,338 :: 		DirYR = 1;
	BSF         LATC+0, 0 
;PIC18F45K22.c,339 :: 		DirYL=DirYR;          // Direction is the same on both Y motors, one coil is crossed over on the PCB or connection to Y motor !
	BTFSC       LATC+0, 0 
	GOTO        L__main345
	BCF         LATC+0, 1 
	GOTO        L__main346
L__main345:
	BSF         LATC+0, 1 
L__main346:
;PIC18F45K22.c,340 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,341 :: 		StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,342 :: 		MotorXY(StepsX,StepsY,3);  //3 = 3 parts
	MOVLW       3
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,343 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,344 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,345 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,346 :: 		case 101:                   //  Dir 7 to left and inferior
L_main36:
;PIC18F45K22.c,347 :: 		DirX=1;
	BSF         LATD+0, 0 
;PIC18F45K22.c,348 :: 		DirYR = 0;
	BCF         LATC+0, 0 
;PIC18F45K22.c,349 :: 		DirYL=DirYR;          //
	BTFSC       LATC+0, 0 
	GOTO        L__main347
	BCF         LATC+0, 1 
	GOTO        L__main348
L__main347:
	BSF         LATC+0, 1 
L__main348:
;PIC18F45K22.c,350 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,351 :: 		StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,352 :: 		MotorXY(StepsX,StepsY,3);  //3 = 3 parts
	MOVLW       3
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,353 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,354 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,355 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,361 :: 		case 102:         //  Dir 1    W  of  XY dir to right and inferior corner
L_main37:
;PIC18F45K22.c,362 :: 		DirX=0;
	BCF         LATD+0, 0 
;PIC18F45K22.c,363 :: 		DirYR = 0;
	BCF         LATC+0, 0 
;PIC18F45K22.c,364 :: 		DirYL=DirYR;          // Direction is the same on all 4 Y motors
	BTFSC       LATC+0, 0 
	GOTO        L__main349
	BCF         LATC+0, 1 
	GOTO        L__main350
L__main349:
	BSF         LATC+0, 1 
L__main350:
;PIC18F45K22.c,365 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,366 :: 		StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,367 :: 		MotorXY(StepsX,StepsY,4);  //4 = 4 parts
	MOVLW       4
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,368 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,369 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,370 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,371 :: 		case 103:                   // DIR 3 dir to right and above
L_main38:
;PIC18F45K22.c,372 :: 		DirX=0;
	BCF         LATD+0, 0 
;PIC18F45K22.c,373 :: 		DirYR = 1;
	BSF         LATC+0, 0 
;PIC18F45K22.c,374 :: 		DirYL=DirYR;          //
	BTFSC       LATC+0, 0 
	GOTO        L__main351
	BCF         LATC+0, 1 
	GOTO        L__main352
L__main351:
	BSF         LATC+0, 1 
L__main352:
;PIC18F45K22.c,375 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,376 :: 		StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,377 :: 		MotorXY(StepsX,StepsY,4);  //4 = 4 parts
	MOVLW       4
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,378 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,379 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,380 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,381 :: 		case 104:                   //   Dir 5    to upper and left
L_main39:
;PIC18F45K22.c,382 :: 		DirX=1;  //
	BSF         LATD+0, 0 
;PIC18F45K22.c,383 :: 		DirYR = 1;
	BSF         LATC+0, 0 
;PIC18F45K22.c,384 :: 		DirYL=DirYR;          // Direction is the same on both Y motors, one coil is crossed over on the PCB or connection to Y motor !
	BTFSC       LATC+0, 0 
	GOTO        L__main353
	BCF         LATC+0, 1 
	GOTO        L__main354
L__main353:
	BSF         LATC+0, 1 
L__main354:
;PIC18F45K22.c,385 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,386 :: 		StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,387 :: 		MotorXY(StepsX,StepsY,4);  //4 = 4 parts
	MOVLW       4
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,388 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,389 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,390 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,391 :: 		case 105:                   //  Dir 7 to left and inferior
L_main40:
;PIC18F45K22.c,392 :: 		DirX=1;
	BSF         LATD+0, 0 
;PIC18F45K22.c,393 :: 		DirYR = 0;
	BCF         LATC+0, 0 
;PIC18F45K22.c,394 :: 		DirYL=DirYR;          //
	BTFSC       LATC+0, 0 
	GOTO        L__main355
	BCF         LATC+0, 1 
	GOTO        L__main356
L__main355:
	BSF         LATC+0, 1 
L__main356:
;PIC18F45K22.c,395 :: 		StepsX = (unsigned int)  ( (Commands[1] << 8 ) + Commands[2]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+2, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,396 :: 		StepsY = (unsigned int)  (  (Commands[3] << 8 )+Commands[4]);
	MOVF        _Commands+3, 0 
	MOVWF       FARG_MotorXY+1 
	CLRF        FARG_MotorXY+0 
	MOVF        _Commands+4, 0 
	ADDWF       FARG_MotorXY+0, 1 
	MOVLW       0
	ADDWFC      FARG_MotorXY+1, 1 
;PIC18F45K22.c,397 :: 		MotorXY(StepsX,StepsY,4);  //4 = 4 parts
	MOVLW       4
	MOVWF       FARG_MotorXY+0 
	CALL        _MotorXY+0, 0
;PIC18F45K22.c,398 :: 		Uart1_Write(43);
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,399 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,400 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,404 :: 		case 110:
L_main41:
;PIC18F45K22.c,405 :: 		EscapeMotorZ(2000); // 2.5 mm for now
	MOVLW       208
	MOVWF       FARG_EscapeMotorZ+0 
	MOVLW       7
	MOVWF       FARG_EscapeMotorZ+1 
	CALL        _EscapeMotorZ+0, 0
;PIC18F45K22.c,406 :: 		Uart1_Write(43); // maybe not
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,407 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,408 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,410 :: 		case 111:
L_main42:
;PIC18F45K22.c,411 :: 		DirX=0;   // GO RIGHTWARDS OR DOWNWARDS WITHOUT CHECKING CORNERSWITCH
	BCF         LATD+0, 0 
;PIC18F45K22.c,412 :: 		DirYR = 0;
	BCF         LATC+0, 0 
;PIC18F45K22.c,413 :: 		DirYL=DirYR;
	BTFSC       LATC+0, 0 
	GOTO        L__main357
	BCF         LATC+0, 1 
	GOTO        L__main358
L__main357:
	BSF         LATC+0, 1 
L__main358:
;PIC18F45K22.c,414 :: 		EscapeMotorXY(Commands[1],Commands[2],Commands[3],Commands[4]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_EscapeMotorXY+0 
	MOVF        _Commands+2, 0 
	MOVWF       FARG_EscapeMotorXY+0 
	MOVF        _Commands+3, 0 
	MOVWF       FARG_EscapeMotorXY+0 
	MOVF        _Commands+4, 0 
	MOVWF       FARG_EscapeMotorXY+0 
	CALL        _EscapeMotorXY+0, 0
;PIC18F45K22.c,416 :: 		Uart1_Write(43); // maybe not
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,417 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,418 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,420 :: 		case 113:     // both back Y switches are closed
L_main43:
;PIC18F45K22.c,421 :: 		DirX=1;   // GO FRONTWARDS OR LEFTWARDS WITHOUT CHECKING CORNERSWITCH
	BSF         LATD+0, 0 
;PIC18F45K22.c,422 :: 		DirYR = 1;
	BSF         LATC+0, 0 
;PIC18F45K22.c,423 :: 		DirYL=DirYR;
	BTFSC       LATC+0, 0 
	GOTO        L__main359
	BCF         LATC+0, 1 
	GOTO        L__main360
L__main359:
	BSF         LATC+0, 1 
L__main360:
;PIC18F45K22.c,424 :: 		EscapeMotorXY(Commands[1],Commands[2],Commands[3],Commands[4]);
	MOVF        _Commands+1, 0 
	MOVWF       FARG_EscapeMotorXY+0 
	MOVF        _Commands+2, 0 
	MOVWF       FARG_EscapeMotorXY+0 
	MOVF        _Commands+3, 0 
	MOVWF       FARG_EscapeMotorXY+0 
	MOVF        _Commands+4, 0 
	MOVWF       FARG_EscapeMotorXY+0 
	CALL        _EscapeMotorXY+0, 0
;PIC18F45K22.c,425 :: 		Uart1_Write(43); // maybe not
	MOVLW       43
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,426 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,427 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,429 :: 		default:
L_main44:
;PIC18F45K22.c,430 :: 		Uart1_Write(63); //maybe aks for same instruction once more  when
	MOVLW       63
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,432 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,433 :: 		break;
	GOTO        L_main14
;PIC18F45K22.c,434 :: 		}
L_main13:
	MOVF        _Commands+0, 0 
	XORLW       68
	BTFSC       STATUS+0, 2 
	GOTO        L_main15
	MOVF        _Commands+0, 0 
	XORLW       75
	BTFSC       STATUS+0, 2 
	GOTO        L_main16
	MOVF        _Commands+0, 0 
	XORLW       76
	BTFSC       STATUS+0, 2 
	GOTO        L_main22
	MOVF        _Commands+0, 0 
	XORLW       84
	BTFSC       STATUS+0, 2 
	GOTO        L_main23
	MOVF        _Commands+0, 0 
	XORLW       86
	BTFSC       STATUS+0, 2 
	GOTO        L_main24
	MOVF        _Commands+0, 0 
	XORLW       87
	BTFSC       STATUS+0, 2 
	GOTO        L_main25
	MOVF        _Commands+0, 0 
	XORLW       89
	BTFSC       STATUS+0, 2 
	GOTO        L_main26
	MOVF        _Commands+0, 0 
	XORLW       91
	BTFSC       STATUS+0, 2 
	GOTO        L_main27
	MOVF        _Commands+0, 0 
	XORLW       93
	BTFSC       STATUS+0, 2 
	GOTO        L_main28
	MOVF        _Commands+0, 0 
	XORLW       94
	BTFSC       STATUS+0, 2 
	GOTO        L_main29
	MOVF        _Commands+0, 0 
	XORLW       95
	BTFSC       STATUS+0, 2 
	GOTO        L_main30
	MOVF        _Commands+0, 0 
	XORLW       96
	BTFSC       STATUS+0, 2 
	GOTO        L_main31
	MOVF        _Commands+0, 0 
	XORLW       97
	BTFSC       STATUS+0, 2 
	GOTO        L_main32
	MOVF        _Commands+0, 0 
	XORLW       98
	BTFSC       STATUS+0, 2 
	GOTO        L_main33
	MOVF        _Commands+0, 0 
	XORLW       99
	BTFSC       STATUS+0, 2 
	GOTO        L_main34
	MOVF        _Commands+0, 0 
	XORLW       100
	BTFSC       STATUS+0, 2 
	GOTO        L_main35
	MOVF        _Commands+0, 0 
	XORLW       101
	BTFSC       STATUS+0, 2 
	GOTO        L_main36
	MOVF        _Commands+0, 0 
	XORLW       102
	BTFSC       STATUS+0, 2 
	GOTO        L_main37
	MOVF        _Commands+0, 0 
	XORLW       103
	BTFSC       STATUS+0, 2 
	GOTO        L_main38
	MOVF        _Commands+0, 0 
	XORLW       104
	BTFSC       STATUS+0, 2 
	GOTO        L_main39
	MOVF        _Commands+0, 0 
	XORLW       105
	BTFSC       STATUS+0, 2 
	GOTO        L_main40
	MOVF        _Commands+0, 0 
	XORLW       110
	BTFSC       STATUS+0, 2 
	GOTO        L_main41
	MOVF        _Commands+0, 0 
	XORLW       111
	BTFSC       STATUS+0, 2 
	GOTO        L_main42
	MOVF        _Commands+0, 0 
	XORLW       113
	BTFSC       STATUS+0, 2 
	GOTO        L_main43
	GOTO        L_main44
L_main14:
;PIC18F45K22.c,435 :: 		}  // checksum ok
	GOTO        L_main45
L_main12:
;PIC18F45K22.c,439 :: 		Uart1_Write(63); // aks for same instruction once more
	MOVLW       63
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,440 :: 		CommandIndex=0;
	CLRF        _CommandIndex+0 
;PIC18F45K22.c,441 :: 		}
L_main45:
;PIC18F45K22.c,442 :: 		}    // for
L_main5:
	GOTO        L_main3
;PIC18F45K22.c,443 :: 		}            // main
L_end_main:
	GOTO        $+0
; end of _main

_MotorXY:

;PIC18F45K22.c,446 :: 		void MotorXY(unsigned int xSteps,unsigned int ySteps, unsigned short parts)
;PIC18F45K22.c,448 :: 		unsigned int M=0;       // M = motor steps
	CLRF        MotorXY_M_L0+0 
	CLRF        MotorXY_M_L0+1 
	CLRF        MotorXY_stepperDelayX_L0+0 
	CLRF        MotorXY_stepperDelayX_L0+1 
	CLRF        MotorXY_stepperDelayX_L0+2 
	CLRF        MotorXY_stepperDelayX_L0+3 
	CLRF        MotorXY_timerStepperDelayX_L0+0 
	CLRF        MotorXY_timerStepperDelayX_L0+1 
	CLRF        MotorXY_stepperDelayY_L0+0 
	CLRF        MotorXY_stepperDelayY_L0+1 
	CLRF        MotorXY_stepperDelayY_L0+2 
	CLRF        MotorXY_stepperDelayY_L0+3 
	CLRF        MotorXY_timerStepperDelayY_L0+0 
	CLRF        MotorXY_timerStepperDelayY_L0+1 
	CLRF        MotorXY_counterStepX_L0+0 
	CLRF        MotorXY_counterStepX_L0+1 
	CLRF        MotorXY_counterStepY_L0+0 
	CLRF        MotorXY_counterStepY_L0+1 
	CLRF        MotorXY_z_L0+0 
	CLRF        MotorXY_z_L0+1 
;PIC18F45K22.c,460 :: 		if (xSteps==ySteps)               //  ySteps <= xSteps
	MOVF        FARG_MotorXY_xSteps+1, 0 
	XORWF       FARG_MotorXY_ySteps+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY362
	MOVF        FARG_MotorXY_ySteps+0, 0 
	XORWF       FARG_MotorXY_xSteps+0, 0 
L__MotorXY362:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY46
;PIC18F45K22.c,462 :: 		stepperDelayX=XYMotorDelay*2*15;    // * 2 2*delay *15 from 14.7456MHZ clock count/sec
	MOVF        _XYMotorDelay+0, 0 
	MOVWF       R0 
	MOVF        _XYMotorDelay+1, 0 
	MOVWF       R1 
	RLCF        R0, 1 
	BCF         R0, 0 
	RLCF        R1, 1 
	MOVLW       15
	MOVWF       R4 
	MOVLW       0
	MOVWF       R5 
	CALL        _Mul_16X16_U+0, 0
	MOVF        R0, 0 
	MOVWF       MotorXY_stepperDelayX_L0+0 
	MOVF        R1, 0 
	MOVWF       MotorXY_stepperDelayX_L0+1 
	MOVLW       0
	BTFSC       R1, 7 
	MOVLW       255
	MOVWF       MotorXY_stepperDelayX_L0+2 
	MOVWF       MotorXY_stepperDelayX_L0+3 
;PIC18F45K22.c,463 :: 		timerStepperDelayX = (unsigned int)(65536-(unsigned int)(stepperdelayX));  // was+1200 start slow
	MOVF        MotorXY_stepperDelayX_L0+0, 0 
	SUBLW       0
	MOVWF       R5 
	MOVF        MotorXY_stepperDelayX_L0+1, 0 
	MOVWF       R6 
	MOVLW       0
	SUBFWB      R6, 1 
	MOVF        R5, 0 
	MOVWF       MotorXY_timerStepperDelayX_L0+0 
	MOVF        R6, 0 
	MOVWF       MotorXY_timerStepperDelayX_L0+1 
;PIC18F45K22.c,465 :: 		TMR0IE_bit=0;                              // disable timer0
	BCF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,466 :: 		TMR0H= (timerStepperDelayX & 0XFF00)>>8;
	MOVLW       0
	ANDWF       R5, 0 
	MOVWF       R3 
	MOVF        R6, 0 
	ANDLW       255
	MOVWF       R4 
	MOVF        R4, 0 
	MOVWF       R0 
	CLRF        R1 
	MOVF        R0, 0 
	MOVWF       TMR0H+0 
;PIC18F45K22.c,467 :: 		TMR0L=  timerStepperDelayX & 0X00FF;
	MOVLW       255
	ANDWF       R5, 0 
	MOVWF       TMR0L+0 
;PIC18F45K22.c,468 :: 		TMR0IE_bit=1;                             // enable timer0
	BSF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,470 :: 		for (M=0;M<parts;M++)
	CLRF        MotorXY_M_L0+0 
	CLRF        MotorXY_M_L0+1 
L_MotorXY47:
	MOVLW       0
	SUBWF       MotorXY_M_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY363
	MOVF        FARG_MotorXY_parts+0, 0 
	SUBWF       MotorXY_M_L0+0, 0 
L__MotorXY363:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY48
;PIC18F45K22.c,472 :: 		counterStepX=0;
	CLRF        MotorXY_counterStepX_L0+0 
	CLRF        MotorXY_counterStepX_L0+1 
;PIC18F45K22.c,473 :: 		do
L_MotorXY50:
;PIC18F45K22.c,475 :: 		if (YLCORNERSWITCH==0) {  Uart1_Write(47); break; } //
	BTFSC       PORTB+0, 1 
	GOTO        L_MotorXY53
	MOVLW       47
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY51
L_MotorXY53:
;PIC18F45K22.c,476 :: 		if (YRCORNERSWITCH==0) {  Uart1_Write(48); break;  }
	BTFSC       PORTB+0, 2 
	GOTO        L_MotorXY54
	MOVLW       48
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY51
L_MotorXY54:
;PIC18F45K22.c,477 :: 		if (Y2CORNERSWITCH==0) {  Uart1_Write(49); break; }
	BTFSC       PORTB+0, 3 
	GOTO        L_MotorXY55
	MOVLW       49
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY51
L_MotorXY55:
;PIC18F45K22.c,478 :: 		if (XRCORNERSWITCH==0) {  Uart1_Write(46); break; }
	BTFSC       PORTB+0, 5 
	GOTO        L_MotorXY56
	MOVLW       46
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY51
L_MotorXY56:
;PIC18F45K22.c,479 :: 		if (XLCORNERSWITCH==0) {  Uart1_Write(45);break; }
	BTFSC       PORTB+0, 4 
	GOTO        L_MotorXY57
	MOVLW       45
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY51
L_MotorXY57:
;PIC18F45K22.c,482 :: 		if((countTimerX>0) && (counterStepX<xSteps))
	MOVF        _countTimerX+0, 0 
	SUBLW       0
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY60
	MOVF        FARG_MotorXY_xSteps+1, 0 
	SUBWF       MotorXY_counterStepX_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY364
	MOVF        FARG_MotorXY_xSteps+0, 0 
	SUBWF       MotorXY_counterStepX_L0+0, 0 
L__MotorXY364:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY60
L__MotorXY320:
;PIC18F45K22.c,484 :: 		StepX=1;
	BSF         LATD+0, 1 
;PIC18F45K22.c,485 :: 		StepYR=1;
	BSF         LATC+0, 3 
;PIC18F45K22.c,486 :: 		StepYL=1;
	BSF         LATC+0, 2 
;PIC18F45K22.c,487 :: 		counterStepX++;
	INFSNZ      MotorXY_counterStepX_L0+0, 1 
	INCF        MotorXY_counterStepX_L0+1, 1 
;PIC18F45K22.c,489 :: 		if (M==0)
	MOVLW       0
	XORWF       MotorXY_M_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY365
	MOVLW       0
	XORWF       MotorXY_M_L0+0, 0 
L__MotorXY365:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY61
;PIC18F45K22.c,491 :: 		if (counterStepX<1600)
	MOVLW       6
	SUBWF       MotorXY_counterStepX_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY366
	MOVLW       64
	SUBWF       MotorXY_counterStepX_L0+0, 0 
L__MotorXY366:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY62
;PIC18F45K22.c,492 :: 		for (z=counterStepX; z <(1600- counterStepX) ;z+=160)
	MOVF        MotorXY_counterStepX_L0+0, 0 
	MOVWF       MotorXY_z_L0+0 
	MOVF        MotorXY_counterStepX_L0+1, 0 
	MOVWF       MotorXY_z_L0+1 
L_MotorXY63:
	MOVF        MotorXY_counterStepX_L0+0, 0 
	SUBLW       64
	MOVWF       R1 
	MOVF        MotorXY_counterStepX_L0+1, 0 
	MOVWF       R2 
	MOVLW       6
	SUBFWB      R2, 1 
	MOVF        R2, 0 
	SUBWF       MotorXY_z_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY367
	MOVF        R1, 0 
	SUBWF       MotorXY_z_L0+0, 0 
L__MotorXY367:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY64
;PIC18F45K22.c,493 :: 		delay_us(1);
	MOVLW       4
	MOVWF       R13, 0
L_MotorXY66:
	DECFSZ      R13, 1, 1
	BRA         L_MotorXY66
	NOP
	NOP
;PIC18F45K22.c,492 :: 		for (z=counterStepX; z <(1600- counterStepX) ;z+=160)
	MOVLW       160
	ADDWF       MotorXY_z_L0+0, 1 
	MOVLW       0
	ADDWFC      MotorXY_z_L0+1, 1 
;PIC18F45K22.c,493 :: 		delay_us(1);
	GOTO        L_MotorXY63
L_MotorXY64:
L_MotorXY62:
;PIC18F45K22.c,494 :: 		}
L_MotorXY61:
;PIC18F45K22.c,496 :: 		if (M==(parts-1))
	DECF        FARG_MotorXY_parts+0, 0 
	MOVWF       R1 
	CLRF        R2 
	MOVLW       0
	SUBWFB      R2, 1 
	MOVF        MotorXY_M_L0+1, 0 
	XORWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY368
	MOVF        R1, 0 
	XORWF       MotorXY_M_L0+0, 0 
L__MotorXY368:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY67
;PIC18F45K22.c,498 :: 		if (counterStepX> ( xSteps- 1600))
	MOVLW       64
	SUBWF       FARG_MotorXY_xSteps+0, 0 
	MOVWF       R1 
	MOVLW       6
	SUBWFB      FARG_MotorXY_xSteps+1, 0 
	MOVWF       R2 
	MOVF        MotorXY_counterStepX_L0+1, 0 
	SUBWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY369
	MOVF        MotorXY_counterStepX_L0+0, 0 
	SUBWF       R1, 0 
L__MotorXY369:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY68
;PIC18F45K22.c,499 :: 		for (z= 0; z <  (  counterStepX -  (xSteps - 1600) ) ;  z+=160)
	CLRF        MotorXY_z_L0+0 
	CLRF        MotorXY_z_L0+1 
L_MotorXY69:
	MOVLW       64
	SUBWF       FARG_MotorXY_xSteps+0, 0 
	MOVWF       R0 
	MOVLW       6
	SUBWFB      FARG_MotorXY_xSteps+1, 0 
	MOVWF       R1 
	MOVF        R0, 0 
	SUBWF       MotorXY_counterStepX_L0+0, 0 
	MOVWF       R2 
	MOVF        R1, 0 
	SUBWFB      MotorXY_counterStepX_L0+1, 0 
	MOVWF       R3 
	MOVF        R3, 0 
	SUBWF       MotorXY_z_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY370
	MOVF        R2, 0 
	SUBWF       MotorXY_z_L0+0, 0 
L__MotorXY370:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY70
;PIC18F45K22.c,500 :: 		delay_us(1);
	MOVLW       4
	MOVWF       R13, 0
L_MotorXY72:
	DECFSZ      R13, 1, 1
	BRA         L_MotorXY72
	NOP
	NOP
;PIC18F45K22.c,499 :: 		for (z= 0; z <  (  counterStepX -  (xSteps - 1600) ) ;  z+=160)
	MOVLW       160
	ADDWF       MotorXY_z_L0+0, 1 
	MOVLW       0
	ADDWFC      MotorXY_z_L0+1, 1 
;PIC18F45K22.c,500 :: 		delay_us(1);
	GOTO        L_MotorXY69
L_MotorXY70:
L_MotorXY68:
;PIC18F45K22.c,501 :: 		}
L_MotorXY67:
;PIC18F45K22.c,503 :: 		TMR0IE_bit=0;                             // disable timer0
	BCF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,504 :: 		TMR0H= (timerStepperDelayX & 0XFF00)>>8;
	MOVLW       0
	ANDWF       MotorXY_timerStepperDelayX_L0+0, 0 
	MOVWF       R3 
	MOVF        MotorXY_timerStepperDelayX_L0+1, 0 
	ANDLW       255
	MOVWF       R4 
	MOVF        R4, 0 
	MOVWF       R0 
	CLRF        R1 
	MOVF        R0, 0 
	MOVWF       TMR0H+0 
;PIC18F45K22.c,505 :: 		TMR0L=  timerStepperDelayX & 0X00FF;
	MOVLW       255
	ANDWF       MotorXY_timerStepperDelayX_L0+0, 0 
	MOVWF       TMR0L+0 
;PIC18F45K22.c,506 :: 		TMR0IE_bit=1; // enable timer1
	BSF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,507 :: 		countTimerX=0;
	CLRF        _countTimerX+0 
;PIC18F45K22.c,508 :: 		StepX=0;
	BCF         LATD+0, 1 
;PIC18F45K22.c,509 :: 		StepYL=0;
	BCF         LATC+0, 2 
;PIC18F45K22.c,510 :: 		StepYR=0;
	BCF         LATC+0, 3 
;PIC18F45K22.c,511 :: 		}
L_MotorXY60:
;PIC18F45K22.c,512 :: 		}  while (counterStepX < xSteps );
	MOVF        FARG_MotorXY_xSteps+1, 0 
	SUBWF       MotorXY_counterStepX_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY371
	MOVF        FARG_MotorXY_xSteps+0, 0 
	SUBWF       MotorXY_counterStepX_L0+0, 0 
L__MotorXY371:
	BTFSS       STATUS+0, 0 
	GOTO        L_MotorXY50
L_MotorXY51:
;PIC18F45K22.c,470 :: 		for (M=0;M<parts;M++)
	INFSNZ      MotorXY_M_L0+0, 1 
	INCF        MotorXY_M_L0+1, 1 
;PIC18F45K22.c,513 :: 		} // end M
	GOTO        L_MotorXY47
L_MotorXY48:
;PIC18F45K22.c,514 :: 		}   // xSteps = ySteps
	GOTO        L_MotorXY73
L_MotorXY46:
;PIC18F45K22.c,516 :: 		else if (xSteps==0)       // only y steps to do
	MOVLW       0
	XORWF       FARG_MotorXY_xSteps+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY372
	MOVLW       0
	XORWF       FARG_MotorXY_xSteps+0, 0 
L__MotorXY372:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY74
;PIC18F45K22.c,520 :: 		stepperDelayY=XYMotorDelay*35;    // * 2 2*delay *15 from 14.7456MHZ clock count/sec
	MOVF        _XYMotorDelay+0, 0 
	MOVWF       R0 
	MOVF        _XYMotorDelay+1, 0 
	MOVWF       R1 
	MOVLW       35
	MOVWF       R4 
	MOVLW       0
	MOVWF       R5 
	CALL        _Mul_16X16_U+0, 0
	MOVF        R0, 0 
	MOVWF       MotorXY_stepperDelayY_L0+0 
	MOVF        R1, 0 
	MOVWF       MotorXY_stepperDelayY_L0+1 
	MOVLW       0
	BTFSC       R1, 7 
	MOVLW       255
	MOVWF       MotorXY_stepperDelayY_L0+2 
	MOVWF       MotorXY_stepperDelayY_L0+3 
;PIC18F45K22.c,521 :: 		timerStepperDelayY = (unsigned int)(65536-(unsigned int)(stepperdelayY));  // was+1200 start slow
	MOVF        MotorXY_stepperDelayY_L0+0, 0 
	SUBLW       0
	MOVWF       R5 
	MOVF        MotorXY_stepperDelayY_L0+1, 0 
	MOVWF       R6 
	MOVLW       0
	SUBFWB      R6, 1 
	MOVF        R5, 0 
	MOVWF       MotorXY_timerStepperDelayY_L0+0 
	MOVF        R6, 0 
	MOVWF       MotorXY_timerStepperDelayY_L0+1 
;PIC18F45K22.c,523 :: 		TMR1IE_bit=0;   // disable timer0
	BCF         TMR1IE_bit+0, BitPos(TMR1IE_bit+0) 
;PIC18F45K22.c,524 :: 		TMR1H= (timerStepperDelayY & 0XFF00)>>8;
	MOVLW       0
	ANDWF       R5, 0 
	MOVWF       R3 
	MOVF        R6, 0 
	ANDLW       255
	MOVWF       R4 
	MOVF        R4, 0 
	MOVWF       R0 
	CLRF        R1 
	MOVF        R0, 0 
	MOVWF       TMR1H+0 
;PIC18F45K22.c,525 :: 		TMR1L=  timerStepperDelayY & 0X00FF;
	MOVLW       255
	ANDWF       R5, 0 
	MOVWF       TMR1L+0 
;PIC18F45K22.c,526 :: 		TMR1IE_bit=1;                             // enable timer0
	BSF         TMR1IE_bit+0, BitPos(TMR1IE_bit+0) 
;PIC18F45K22.c,528 :: 		for (M=0;M<parts;M++)
	CLRF        MotorXY_M_L0+0 
	CLRF        MotorXY_M_L0+1 
L_MotorXY75:
	MOVLW       0
	SUBWF       MotorXY_M_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY373
	MOVF        FARG_MotorXY_parts+0, 0 
	SUBWF       MotorXY_M_L0+0, 0 
L__MotorXY373:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY76
;PIC18F45K22.c,530 :: 		counterStepY=0;
	CLRF        MotorXY_counterStepY_L0+0 
	CLRF        MotorXY_counterStepY_L0+1 
;PIC18F45K22.c,531 :: 		do
L_MotorXY78:
;PIC18F45K22.c,533 :: 		if (YLCORNERSWITCH==0) {  Uart1_Write(47); break; } //
	BTFSC       PORTB+0, 1 
	GOTO        L_MotorXY81
	MOVLW       47
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY79
L_MotorXY81:
;PIC18F45K22.c,534 :: 		if (YRCORNERSWITCH==0) {  Uart1_Write(48); break;  }
	BTFSC       PORTB+0, 2 
	GOTO        L_MotorXY82
	MOVLW       48
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY79
L_MotorXY82:
;PIC18F45K22.c,535 :: 		if (Y2CORNERSWITCH==0) {  Uart1_Write(49); break; }
	BTFSC       PORTB+0, 3 
	GOTO        L_MotorXY83
	MOVLW       49
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY79
L_MotorXY83:
;PIC18F45K22.c,538 :: 		if((countTimerY>0) && (counterStepY<ySteps))
	MOVF        _countTimerY+0, 0 
	SUBLW       0
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY86
	MOVF        FARG_MotorXY_ySteps+1, 0 
	SUBWF       MotorXY_counterStepY_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY374
	MOVF        FARG_MotorXY_ySteps+0, 0 
	SUBWF       MotorXY_counterStepY_L0+0, 0 
L__MotorXY374:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY86
L__MotorXY319:
;PIC18F45K22.c,540 :: 		StepYR=1;
	BSF         LATC+0, 3 
;PIC18F45K22.c,541 :: 		StepYL=1;
	BSF         LATC+0, 2 
;PIC18F45K22.c,542 :: 		counterStepY++;
	INFSNZ      MotorXY_counterStepY_L0+0, 1 
	INCF        MotorXY_counterStepY_L0+1, 1 
;PIC18F45K22.c,544 :: 		if (M==0)
	MOVLW       0
	XORWF       MotorXY_M_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY375
	MOVLW       0
	XORWF       MotorXY_M_L0+0, 0 
L__MotorXY375:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY87
;PIC18F45K22.c,546 :: 		if (counterStepY<1600)
	MOVLW       6
	SUBWF       MotorXY_counterStepY_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY376
	MOVLW       64
	SUBWF       MotorXY_counterStepY_L0+0, 0 
L__MotorXY376:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY88
;PIC18F45K22.c,547 :: 		for (z=counterStepY; z <(1600- counterStepY) ;z+=160)
	MOVF        MotorXY_counterStepY_L0+0, 0 
	MOVWF       MotorXY_z_L0+0 
	MOVF        MotorXY_counterStepY_L0+1, 0 
	MOVWF       MotorXY_z_L0+1 
L_MotorXY89:
	MOVF        MotorXY_counterStepY_L0+0, 0 
	SUBLW       64
	MOVWF       R1 
	MOVF        MotorXY_counterStepY_L0+1, 0 
	MOVWF       R2 
	MOVLW       6
	SUBFWB      R2, 1 
	MOVF        R2, 0 
	SUBWF       MotorXY_z_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY377
	MOVF        R1, 0 
	SUBWF       MotorXY_z_L0+0, 0 
L__MotorXY377:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY90
;PIC18F45K22.c,548 :: 		delay_us(1);
	MOVLW       4
	MOVWF       R13, 0
L_MotorXY92:
	DECFSZ      R13, 1, 1
	BRA         L_MotorXY92
	NOP
	NOP
;PIC18F45K22.c,547 :: 		for (z=counterStepY; z <(1600- counterStepY) ;z+=160)
	MOVLW       160
	ADDWF       MotorXY_z_L0+0, 1 
	MOVLW       0
	ADDWFC      MotorXY_z_L0+1, 1 
;PIC18F45K22.c,548 :: 		delay_us(1);
	GOTO        L_MotorXY89
L_MotorXY90:
L_MotorXY88:
;PIC18F45K22.c,549 :: 		}
L_MotorXY87:
;PIC18F45K22.c,551 :: 		if (M==(parts-1))
	DECF        FARG_MotorXY_parts+0, 0 
	MOVWF       R1 
	CLRF        R2 
	MOVLW       0
	SUBWFB      R2, 1 
	MOVF        MotorXY_M_L0+1, 0 
	XORWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY378
	MOVF        R1, 0 
	XORWF       MotorXY_M_L0+0, 0 
L__MotorXY378:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY93
;PIC18F45K22.c,553 :: 		if (counterStepY> ( ySteps- 1600))
	MOVLW       64
	SUBWF       FARG_MotorXY_ySteps+0, 0 
	MOVWF       R1 
	MOVLW       6
	SUBWFB      FARG_MotorXY_ySteps+1, 0 
	MOVWF       R2 
	MOVF        MotorXY_counterStepY_L0+1, 0 
	SUBWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY379
	MOVF        MotorXY_counterStepY_L0+0, 0 
	SUBWF       R1, 0 
L__MotorXY379:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY94
;PIC18F45K22.c,554 :: 		for (z= 0; z <  (  counterStepY -  (ySteps - 1600) ) ;  z+=160)
	CLRF        MotorXY_z_L0+0 
	CLRF        MotorXY_z_L0+1 
L_MotorXY95:
	MOVLW       64
	SUBWF       FARG_MotorXY_ySteps+0, 0 
	MOVWF       R0 
	MOVLW       6
	SUBWFB      FARG_MotorXY_ySteps+1, 0 
	MOVWF       R1 
	MOVF        R0, 0 
	SUBWF       MotorXY_counterStepY_L0+0, 0 
	MOVWF       R2 
	MOVF        R1, 0 
	SUBWFB      MotorXY_counterStepY_L0+1, 0 
	MOVWF       R3 
	MOVF        R3, 0 
	SUBWF       MotorXY_z_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY380
	MOVF        R2, 0 
	SUBWF       MotorXY_z_L0+0, 0 
L__MotorXY380:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY96
;PIC18F45K22.c,555 :: 		delay_us(1);
	MOVLW       4
	MOVWF       R13, 0
L_MotorXY98:
	DECFSZ      R13, 1, 1
	BRA         L_MotorXY98
	NOP
	NOP
;PIC18F45K22.c,554 :: 		for (z= 0; z <  (  counterStepY -  (ySteps - 1600) ) ;  z+=160)
	MOVLW       160
	ADDWF       MotorXY_z_L0+0, 1 
	MOVLW       0
	ADDWFC      MotorXY_z_L0+1, 1 
;PIC18F45K22.c,555 :: 		delay_us(1);
	GOTO        L_MotorXY95
L_MotorXY96:
L_MotorXY94:
;PIC18F45K22.c,556 :: 		}
L_MotorXY93:
;PIC18F45K22.c,557 :: 		TMR1IE_bit=0;                             // disable timer0
	BCF         TMR1IE_bit+0, BitPos(TMR1IE_bit+0) 
;PIC18F45K22.c,558 :: 		TMR1H= (timerStepperDelayY & 0XFF00)>>8;
	MOVLW       0
	ANDWF       MotorXY_timerStepperDelayY_L0+0, 0 
	MOVWF       R3 
	MOVF        MotorXY_timerStepperDelayY_L0+1, 0 
	ANDLW       255
	MOVWF       R4 
	MOVF        R4, 0 
	MOVWF       R0 
	CLRF        R1 
	MOVF        R0, 0 
	MOVWF       TMR1H+0 
;PIC18F45K22.c,559 :: 		TMR1L=  timerStepperDelayY & 0X00FF;
	MOVLW       255
	ANDWF       MotorXY_timerStepperDelayY_L0+0, 0 
	MOVWF       TMR1L+0 
;PIC18F45K22.c,560 :: 		TMR1IE_bit=1; // enable timer1
	BSF         TMR1IE_bit+0, BitPos(TMR1IE_bit+0) 
;PIC18F45K22.c,561 :: 		countTimerY=0;
	CLRF        _countTimerY+0 
;PIC18F45K22.c,562 :: 		StepYR=0;
	BCF         LATC+0, 3 
;PIC18F45K22.c,563 :: 		StepYL=0;
	BCF         LATC+0, 2 
;PIC18F45K22.c,564 :: 		}
L_MotorXY86:
;PIC18F45K22.c,565 :: 		}  while (counterStepY < ySteps );
	MOVF        FARG_MotorXY_ySteps+1, 0 
	SUBWF       MotorXY_counterStepY_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY381
	MOVF        FARG_MotorXY_ySteps+0, 0 
	SUBWF       MotorXY_counterStepY_L0+0, 0 
L__MotorXY381:
	BTFSS       STATUS+0, 0 
	GOTO        L_MotorXY78
L_MotorXY79:
;PIC18F45K22.c,528 :: 		for (M=0;M<parts;M++)
	INFSNZ      MotorXY_M_L0+0, 1 
	INCF        MotorXY_M_L0+1, 1 
;PIC18F45K22.c,566 :: 		} // end M
	GOTO        L_MotorXY75
L_MotorXY76:
;PIC18F45K22.c,567 :: 		}   //ySteps >  xSteps
	GOTO        L_MotorXY99
L_MotorXY74:
;PIC18F45K22.c,569 :: 		else if (ySteps==0)       // only xSteps
	MOVLW       0
	XORWF       FARG_MotorXY_ySteps+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY382
	MOVLW       0
	XORWF       FARG_MotorXY_ySteps+0, 0 
L__MotorXY382:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY100
;PIC18F45K22.c,572 :: 		stepperDelayX=XYMotorDelay*35;    // * 2 2*delay *15 from 14.7456MHZ clock count/sec
	MOVF        _XYMotorDelay+0, 0 
	MOVWF       R0 
	MOVF        _XYMotorDelay+1, 0 
	MOVWF       R1 
	MOVLW       35
	MOVWF       R4 
	MOVLW       0
	MOVWF       R5 
	CALL        _Mul_16X16_U+0, 0
	MOVF        R0, 0 
	MOVWF       MotorXY_stepperDelayX_L0+0 
	MOVF        R1, 0 
	MOVWF       MotorXY_stepperDelayX_L0+1 
	MOVLW       0
	BTFSC       R1, 7 
	MOVLW       255
	MOVWF       MotorXY_stepperDelayX_L0+2 
	MOVWF       MotorXY_stepperDelayX_L0+3 
;PIC18F45K22.c,573 :: 		timerStepperDelayX = (unsigned int)(65536-(unsigned int)(stepperdelayX));  // was+1200 start slow
	MOVF        MotorXY_stepperDelayX_L0+0, 0 
	SUBLW       0
	MOVWF       R5 
	MOVF        MotorXY_stepperDelayX_L0+1, 0 
	MOVWF       R6 
	MOVLW       0
	SUBFWB      R6, 1 
	MOVF        R5, 0 
	MOVWF       MotorXY_timerStepperDelayX_L0+0 
	MOVF        R6, 0 
	MOVWF       MotorXY_timerStepperDelayX_L0+1 
;PIC18F45K22.c,575 :: 		TMR0IE_bit=0;                              // disable timer0
	BCF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,576 :: 		TMR0H= (timerStepperDelayX & 0XFF00)>>8;
	MOVLW       0
	ANDWF       R5, 0 
	MOVWF       R3 
	MOVF        R6, 0 
	ANDLW       255
	MOVWF       R4 
	MOVF        R4, 0 
	MOVWF       R0 
	CLRF        R1 
	MOVF        R0, 0 
	MOVWF       TMR0H+0 
;PIC18F45K22.c,577 :: 		TMR0L=  timerStepperDelayX & 0X00FF;
	MOVLW       255
	ANDWF       R5, 0 
	MOVWF       TMR0L+0 
;PIC18F45K22.c,578 :: 		TMR0IE_bit=1;                             // enable timer0
	BSF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,581 :: 		for (M=0;M<parts;M++)
	CLRF        MotorXY_M_L0+0 
	CLRF        MotorXY_M_L0+1 
L_MotorXY101:
	MOVLW       0
	SUBWF       MotorXY_M_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY383
	MOVF        FARG_MotorXY_parts+0, 0 
	SUBWF       MotorXY_M_L0+0, 0 
L__MotorXY383:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY102
;PIC18F45K22.c,583 :: 		counterStepX=0;
	CLRF        MotorXY_counterStepX_L0+0 
	CLRF        MotorXY_counterStepX_L0+1 
;PIC18F45K22.c,584 :: 		counterStepY=0;
	CLRF        MotorXY_counterStepY_L0+0 
	CLRF        MotorXY_counterStepY_L0+1 
;PIC18F45K22.c,586 :: 		do
L_MotorXY104:
;PIC18F45K22.c,588 :: 		if (XRCORNERSWITCH==0) {  Uart1_Write(46); break; }
	BTFSC       PORTB+0, 5 
	GOTO        L_MotorXY107
	MOVLW       46
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY105
L_MotorXY107:
;PIC18F45K22.c,589 :: 		if (XLCORNERSWITCH==0) {  Uart1_Write(45);break; }
	BTFSC       PORTB+0, 4 
	GOTO        L_MotorXY108
	MOVLW       45
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY105
L_MotorXY108:
;PIC18F45K22.c,592 :: 		if((countTimerX>0) && (counterStepX<xSteps))
	MOVF        _countTimerX+0, 0 
	SUBLW       0
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY111
	MOVF        FARG_MotorXY_xSteps+1, 0 
	SUBWF       MotorXY_counterStepX_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY384
	MOVF        FARG_MotorXY_xSteps+0, 0 
	SUBWF       MotorXY_counterStepX_L0+0, 0 
L__MotorXY384:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY111
L__MotorXY318:
;PIC18F45K22.c,594 :: 		StepX=1;
	BSF         LATD+0, 1 
;PIC18F45K22.c,595 :: 		counterStepX++;
	INFSNZ      MotorXY_counterStepX_L0+0, 1 
	INCF        MotorXY_counterStepX_L0+1, 1 
;PIC18F45K22.c,597 :: 		if (M==0)
	MOVLW       0
	XORWF       MotorXY_M_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY385
	MOVLW       0
	XORWF       MotorXY_M_L0+0, 0 
L__MotorXY385:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY112
;PIC18F45K22.c,599 :: 		if (counterStepX<1600)
	MOVLW       6
	SUBWF       MotorXY_counterStepX_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY386
	MOVLW       64
	SUBWF       MotorXY_counterStepX_L0+0, 0 
L__MotorXY386:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY113
;PIC18F45K22.c,600 :: 		for (z=counterStepX; z <(1600- counterStepX) ;z+=160)
	MOVF        MotorXY_counterStepX_L0+0, 0 
	MOVWF       MotorXY_z_L0+0 
	MOVF        MotorXY_counterStepX_L0+1, 0 
	MOVWF       MotorXY_z_L0+1 
L_MotorXY114:
	MOVF        MotorXY_counterStepX_L0+0, 0 
	SUBLW       64
	MOVWF       R1 
	MOVF        MotorXY_counterStepX_L0+1, 0 
	MOVWF       R2 
	MOVLW       6
	SUBFWB      R2, 1 
	MOVF        R2, 0 
	SUBWF       MotorXY_z_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY387
	MOVF        R1, 0 
	SUBWF       MotorXY_z_L0+0, 0 
L__MotorXY387:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY115
;PIC18F45K22.c,601 :: 		delay_us(1);
	MOVLW       4
	MOVWF       R13, 0
L_MotorXY117:
	DECFSZ      R13, 1, 1
	BRA         L_MotorXY117
	NOP
	NOP
;PIC18F45K22.c,600 :: 		for (z=counterStepX; z <(1600- counterStepX) ;z+=160)
	MOVLW       160
	ADDWF       MotorXY_z_L0+0, 1 
	MOVLW       0
	ADDWFC      MotorXY_z_L0+1, 1 
;PIC18F45K22.c,601 :: 		delay_us(1);
	GOTO        L_MotorXY114
L_MotorXY115:
L_MotorXY113:
;PIC18F45K22.c,602 :: 		}
L_MotorXY112:
;PIC18F45K22.c,604 :: 		if (M==(parts-1))
	DECF        FARG_MotorXY_parts+0, 0 
	MOVWF       R1 
	CLRF        R2 
	MOVLW       0
	SUBWFB      R2, 1 
	MOVF        MotorXY_M_L0+1, 0 
	XORWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY388
	MOVF        R1, 0 
	XORWF       MotorXY_M_L0+0, 0 
L__MotorXY388:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY118
;PIC18F45K22.c,606 :: 		if (counterStepX> ( xSteps- 1600))
	MOVLW       64
	SUBWF       FARG_MotorXY_xSteps+0, 0 
	MOVWF       R1 
	MOVLW       6
	SUBWFB      FARG_MotorXY_xSteps+1, 0 
	MOVWF       R2 
	MOVF        MotorXY_counterStepX_L0+1, 0 
	SUBWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY389
	MOVF        MotorXY_counterStepX_L0+0, 0 
	SUBWF       R1, 0 
L__MotorXY389:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY119
;PIC18F45K22.c,607 :: 		for (z= 0; z <  (  counterStepX -  (xSteps - 1600) ) ;  z+=160)
	CLRF        MotorXY_z_L0+0 
	CLRF        MotorXY_z_L0+1 
L_MotorXY120:
	MOVLW       64
	SUBWF       FARG_MotorXY_xSteps+0, 0 
	MOVWF       R0 
	MOVLW       6
	SUBWFB      FARG_MotorXY_xSteps+1, 0 
	MOVWF       R1 
	MOVF        R0, 0 
	SUBWF       MotorXY_counterStepX_L0+0, 0 
	MOVWF       R2 
	MOVF        R1, 0 
	SUBWFB      MotorXY_counterStepX_L0+1, 0 
	MOVWF       R3 
	MOVF        R3, 0 
	SUBWF       MotorXY_z_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY390
	MOVF        R2, 0 
	SUBWF       MotorXY_z_L0+0, 0 
L__MotorXY390:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY121
;PIC18F45K22.c,608 :: 		delay_us(1);
	MOVLW       4
	MOVWF       R13, 0
L_MotorXY123:
	DECFSZ      R13, 1, 1
	BRA         L_MotorXY123
	NOP
	NOP
;PIC18F45K22.c,607 :: 		for (z= 0; z <  (  counterStepX -  (xSteps - 1600) ) ;  z+=160)
	MOVLW       160
	ADDWF       MotorXY_z_L0+0, 1 
	MOVLW       0
	ADDWFC      MotorXY_z_L0+1, 1 
;PIC18F45K22.c,608 :: 		delay_us(1);
	GOTO        L_MotorXY120
L_MotorXY121:
L_MotorXY119:
;PIC18F45K22.c,609 :: 		}
L_MotorXY118:
;PIC18F45K22.c,610 :: 		TMR0IE_bit=0;                             // disable timer0
	BCF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,611 :: 		TMR0H= (timerStepperDelayX & 0XFF00)>>8;
	MOVLW       0
	ANDWF       MotorXY_timerStepperDelayX_L0+0, 0 
	MOVWF       R3 
	MOVF        MotorXY_timerStepperDelayX_L0+1, 0 
	ANDLW       255
	MOVWF       R4 
	MOVF        R4, 0 
	MOVWF       R0 
	CLRF        R1 
	MOVF        R0, 0 
	MOVWF       TMR0H+0 
;PIC18F45K22.c,612 :: 		TMR0L=  timerStepperDelayX & 0X00FF;
	MOVLW       255
	ANDWF       MotorXY_timerStepperDelayX_L0+0, 0 
	MOVWF       TMR0L+0 
;PIC18F45K22.c,613 :: 		TMR0IE_bit=1; // enable timer1
	BSF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,614 :: 		countTimerX=0;
	CLRF        _countTimerX+0 
;PIC18F45K22.c,615 :: 		StepX=0;
	BCF         LATD+0, 1 
;PIC18F45K22.c,616 :: 		}
L_MotorXY111:
;PIC18F45K22.c,618 :: 		}  while (counterStepX < xSteps );
	MOVF        FARG_MotorXY_xSteps+1, 0 
	SUBWF       MotorXY_counterStepX_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY391
	MOVF        FARG_MotorXY_xSteps+0, 0 
	SUBWF       MotorXY_counterStepX_L0+0, 0 
L__MotorXY391:
	BTFSS       STATUS+0, 0 
	GOTO        L_MotorXY104
L_MotorXY105:
;PIC18F45K22.c,581 :: 		for (M=0;M<parts;M++)
	INFSNZ      MotorXY_M_L0+0, 1 
	INCF        MotorXY_M_L0+1, 1 
;PIC18F45K22.c,620 :: 		} // end M
	GOTO        L_MotorXY101
L_MotorXY102:
;PIC18F45K22.c,621 :: 		}   //ySteps >  xSteps
	GOTO        L_MotorXY124
L_MotorXY100:
;PIC18F45K22.c,626 :: 		else if (xSteps>ySteps)
	MOVF        FARG_MotorXY_xSteps+1, 0 
	SUBWF       FARG_MotorXY_ySteps+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY392
	MOVF        FARG_MotorXY_xSteps+0, 0 
	SUBWF       FARG_MotorXY_ySteps+0, 0 
L__MotorXY392:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY125
;PIC18F45K22.c,629 :: 		partof1600 = (unsigned int)(((long)1600*(long)ySteps)/(long)xSteps)  ;
	MOVF        FARG_MotorXY_ySteps+0, 0 
	MOVWF       R0 
	MOVF        FARG_MotorXY_ySteps+1, 0 
	MOVWF       R1 
	MOVLW       0
	MOVWF       R2 
	MOVWF       R3 
	MOVLW       64
	MOVWF       R4 
	MOVLW       6
	MOVWF       R5 
	MOVLW       0
	MOVWF       R6 
	MOVLW       0
	MOVWF       R7 
	CALL        _Mul_32x32_U+0, 0
	MOVF        FARG_MotorXY_xSteps+0, 0 
	MOVWF       R4 
	MOVF        FARG_MotorXY_xSteps+1, 0 
	MOVWF       R5 
	MOVLW       0
	MOVWF       R6 
	MOVWF       R7 
	CALL        _Div_32x32_S+0, 0
	MOVF        R0, 0 
	MOVWF       MotorXY_PartOf1600_L0+0 
	MOVF        R1, 0 
	MOVWF       MotorXY_PartOf1600_L0+1 
;PIC18F45K22.c,630 :: 		partOf160 = (unsigned int)(((long)partOf1600*(long)ySteps)/(long)xSteps)  ;
	MOVF        R0, 0 
	MOVWF       R4 
	MOVF        R1, 0 
	MOVWF       R5 
	MOVLW       0
	MOVWF       R6 
	MOVWF       R7 
	MOVF        FARG_MotorXY_ySteps+0, 0 
	MOVWF       R0 
	MOVF        FARG_MotorXY_ySteps+1, 0 
	MOVWF       R1 
	MOVLW       0
	MOVWF       R2 
	MOVWF       R3 
	CALL        _Mul_32x32_U+0, 0
	MOVF        FARG_MotorXY_xSteps+0, 0 
	MOVWF       R4 
	MOVF        FARG_MotorXY_xSteps+1, 0 
	MOVWF       R5 
	MOVLW       0
	MOVWF       R6 
	MOVWF       R7 
	CALL        _Div_32x32_S+0, 0
	MOVF        R0, 0 
	MOVWF       MotorXY_PartOf160_L0+0 
	MOVF        R1, 0 
	MOVWF       MotorXY_PartOf160_L0+1 
;PIC18F45K22.c,631 :: 		if (partOf160==0) partOf160=1;
	MOVLW       0
	XORWF       R1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY393
	MOVLW       0
	XORWF       R0, 0 
L__MotorXY393:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY126
	MOVLW       1
	MOVWF       MotorXY_PartOf160_L0+0 
	MOVLW       0
	MOVWF       MotorXY_PartOf160_L0+1 
L_MotorXY126:
;PIC18F45K22.c,633 :: 		stepperDelayX=XYMotorDelay*2*15;    // * 2 2*delay *15 from 14.7456MHZ clock count/sec
	MOVF        _XYMotorDelay+0, 0 
	MOVWF       R0 
	MOVF        _XYMotorDelay+1, 0 
	MOVWF       R1 
	RLCF        R0, 1 
	BCF         R0, 0 
	RLCF        R1, 1 
	MOVLW       15
	MOVWF       R4 
	MOVLW       0
	MOVWF       R5 
	CALL        _Mul_16X16_U+0, 0
	MOVF        R0, 0 
	MOVWF       MotorXY_stepperDelayX_L0+0 
	MOVF        R1, 0 
	MOVWF       MotorXY_stepperDelayX_L0+1 
	MOVLW       0
	BTFSC       R1, 7 
	MOVLW       255
	MOVWF       MotorXY_stepperDelayX_L0+2 
	MOVWF       MotorXY_stepperDelayX_L0+3 
;PIC18F45K22.c,638 :: 		stepperDelayY=(unsigned int)(((long)stepperDelayX*(long)xSteps)/(long)ySteps); // +80 nearly ok
	MOVF        FARG_MotorXY_xSteps+0, 0 
	MOVWF       R0 
	MOVF        FARG_MotorXY_xSteps+1, 0 
	MOVWF       R1 
	MOVLW       0
	MOVWF       R2 
	MOVWF       R3 
	MOVF        MotorXY_stepperDelayX_L0+0, 0 
	MOVWF       R4 
	MOVF        MotorXY_stepperDelayX_L0+1, 0 
	MOVWF       R5 
	MOVF        MotorXY_stepperDelayX_L0+2, 0 
	MOVWF       R6 
	MOVF        MotorXY_stepperDelayX_L0+3, 0 
	MOVWF       R7 
	CALL        _Mul_32x32_U+0, 0
	MOVF        FARG_MotorXY_ySteps+0, 0 
	MOVWF       R4 
	MOVF        FARG_MotorXY_ySteps+1, 0 
	MOVWF       R5 
	MOVLW       0
	MOVWF       R6 
	MOVWF       R7 
	CALL        _Div_32x32_S+0, 0
	MOVF        R0, 0 
	MOVWF       MotorXY_stepperDelayY_L0+0 
	MOVF        R1, 0 
	MOVWF       MotorXY_stepperDelayY_L0+1 
	MOVLW       0
	MOVWF       MotorXY_stepperDelayY_L0+2 
	MOVWF       MotorXY_stepperDelayY_L0+3 
;PIC18F45K22.c,640 :: 		timerStepperDelayX = (unsigned int)(65536-(unsigned int)(stepperdelayX));  //
	MOVF        MotorXY_stepperDelayX_L0+0, 0 
	SUBLW       0
	MOVWF       R7 
	MOVF        MotorXY_stepperDelayX_L0+1, 0 
	MOVWF       R8 
	MOVLW       0
	SUBFWB      R8, 1 
	MOVF        R7, 0 
	MOVWF       MotorXY_timerStepperDelayX_L0+0 
	MOVF        R8, 0 
	MOVWF       MotorXY_timerStepperDelayX_L0+1 
;PIC18F45K22.c,641 :: 		timerStepperDelayY = (unsigned int)(65536-(unsigned int)(stepperdelayY) );
	MOVF        MotorXY_stepperDelayY_L0+0, 0 
	SUBLW       0
	MOVWF       R5 
	MOVF        MotorXY_stepperDelayY_L0+1, 0 
	MOVWF       R6 
	MOVLW       0
	SUBFWB      R6, 1 
	MOVF        R5, 0 
	MOVWF       MotorXY_timerStepperDelayY_L0+0 
	MOVF        R6, 0 
	MOVWF       MotorXY_timerStepperDelayY_L0+1 
;PIC18F45K22.c,643 :: 		TMR0IE_bit=0;                              // disable timer0
	BCF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,644 :: 		TMR0H= (timerStepperDelayX & 0XFF00)>>8;
	MOVLW       0
	ANDWF       R7, 0 
	MOVWF       R3 
	MOVF        R8, 0 
	ANDLW       255
	MOVWF       R4 
	MOVF        R4, 0 
	MOVWF       R0 
	CLRF        R1 
	MOVF        R0, 0 
	MOVWF       TMR0H+0 
;PIC18F45K22.c,645 :: 		TMR0L=  timerStepperDelayX & 0X00FF;
	MOVLW       255
	ANDWF       R7, 0 
	MOVWF       TMR0L+0 
;PIC18F45K22.c,646 :: 		TMR0IE_bit=1;                             // enable timer0
	BSF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,648 :: 		TMR1IE_bit=0;                             // disable timer1
	BCF         TMR1IE_bit+0, BitPos(TMR1IE_bit+0) 
;PIC18F45K22.c,649 :: 		TMR1H= (timerStepperDelayY & 0XFF00)>>8;
	MOVLW       0
	ANDWF       R5, 0 
	MOVWF       R3 
	MOVF        R6, 0 
	ANDLW       255
	MOVWF       R4 
	MOVF        R4, 0 
	MOVWF       R0 
	CLRF        R1 
	MOVF        R0, 0 
	MOVWF       TMR1H+0 
;PIC18F45K22.c,650 :: 		TMR1L=  timerStepperDelayY & 0X00FF;
	MOVLW       255
	ANDWF       R5, 0 
	MOVWF       TMR1L+0 
;PIC18F45K22.c,651 :: 		TMR1IE_bit=1;                             // enable timer1
	BSF         TMR1IE_bit+0, BitPos(TMR1IE_bit+0) 
;PIC18F45K22.c,653 :: 		for (M=0;M<parts;M++)
	CLRF        MotorXY_M_L0+0 
	CLRF        MotorXY_M_L0+1 
L_MotorXY127:
	MOVLW       0
	SUBWF       MotorXY_M_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY394
	MOVF        FARG_MotorXY_parts+0, 0 
	SUBWF       MotorXY_M_L0+0, 0 
L__MotorXY394:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY128
;PIC18F45K22.c,655 :: 		counterStepX=0;
	CLRF        MotorXY_counterStepX_L0+0 
	CLRF        MotorXY_counterStepX_L0+1 
;PIC18F45K22.c,656 :: 		counterStepY=0;
	CLRF        MotorXY_counterStepY_L0+0 
	CLRF        MotorXY_counterStepY_L0+1 
;PIC18F45K22.c,658 :: 		do
L_MotorXY130:
;PIC18F45K22.c,660 :: 		if (YLCORNERSWITCH==0) {  Uart1_Write(47); break; } //
	BTFSC       PORTB+0, 1 
	GOTO        L_MotorXY133
	MOVLW       47
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY131
L_MotorXY133:
;PIC18F45K22.c,661 :: 		if (YRCORNERSWITCH==0) {  Uart1_Write(48); break;  }
	BTFSC       PORTB+0, 2 
	GOTO        L_MotorXY134
	MOVLW       48
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY131
L_MotorXY134:
;PIC18F45K22.c,662 :: 		if (Y2CORNERSWITCH==0) {  Uart1_Write(49); break; }
	BTFSC       PORTB+0, 3 
	GOTO        L_MotorXY135
	MOVLW       49
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY131
L_MotorXY135:
;PIC18F45K22.c,663 :: 		if (XRCORNERSWITCH==0) {  Uart1_Write(46); break; }
	BTFSC       PORTB+0, 5 
	GOTO        L_MotorXY136
	MOVLW       46
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY131
L_MotorXY136:
;PIC18F45K22.c,664 :: 		if (XLCORNERSWITCH==0) {  Uart1_Write(45);break; }
	BTFSC       PORTB+0, 4 
	GOTO        L_MotorXY137
	MOVLW       45
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY131
L_MotorXY137:
;PIC18F45K22.c,667 :: 		if((countTimerX>0) && (counterStepX<xSteps))
	MOVF        _countTimerX+0, 0 
	SUBLW       0
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY140
	MOVF        FARG_MotorXY_xSteps+1, 0 
	SUBWF       MotorXY_counterStepX_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY395
	MOVF        FARG_MotorXY_xSteps+0, 0 
	SUBWF       MotorXY_counterStepX_L0+0, 0 
L__MotorXY395:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY140
L__MotorXY317:
;PIC18F45K22.c,669 :: 		StepX=1;
	BSF         LATD+0, 1 
;PIC18F45K22.c,670 :: 		counterStepX++;
	INFSNZ      MotorXY_counterStepX_L0+0, 1 
	INCF        MotorXY_counterStepX_L0+1, 1 
;PIC18F45K22.c,672 :: 		if (M==0)
	MOVLW       0
	XORWF       MotorXY_M_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY396
	MOVLW       0
	XORWF       MotorXY_M_L0+0, 0 
L__MotorXY396:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY141
;PIC18F45K22.c,674 :: 		if (counterStepX<1600)
	MOVLW       6
	SUBWF       MotorXY_counterStepX_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY397
	MOVLW       64
	SUBWF       MotorXY_counterStepX_L0+0, 0 
L__MotorXY397:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY142
;PIC18F45K22.c,675 :: 		for (z=counterStepX; z <(1600- counterStepX) ;z+=160)
	MOVF        MotorXY_counterStepX_L0+0, 0 
	MOVWF       MotorXY_z_L0+0 
	MOVF        MotorXY_counterStepX_L0+1, 0 
	MOVWF       MotorXY_z_L0+1 
L_MotorXY143:
	MOVF        MotorXY_counterStepX_L0+0, 0 
	SUBLW       64
	MOVWF       R1 
	MOVF        MotorXY_counterStepX_L0+1, 0 
	MOVWF       R2 
	MOVLW       6
	SUBFWB      R2, 1 
	MOVF        R2, 0 
	SUBWF       MotorXY_z_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY398
	MOVF        R1, 0 
	SUBWF       MotorXY_z_L0+0, 0 
L__MotorXY398:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY144
;PIC18F45K22.c,676 :: 		delay_us(1);
	MOVLW       4
	MOVWF       R13, 0
L_MotorXY146:
	DECFSZ      R13, 1, 1
	BRA         L_MotorXY146
	NOP
	NOP
;PIC18F45K22.c,675 :: 		for (z=counterStepX; z <(1600- counterStepX) ;z+=160)
	MOVLW       160
	ADDWF       MotorXY_z_L0+0, 1 
	MOVLW       0
	ADDWFC      MotorXY_z_L0+1, 1 
;PIC18F45K22.c,676 :: 		delay_us(1);
	GOTO        L_MotorXY143
L_MotorXY144:
L_MotorXY142:
;PIC18F45K22.c,677 :: 		}
L_MotorXY141:
;PIC18F45K22.c,679 :: 		if (M==(parts-1))
	DECF        FARG_MotorXY_parts+0, 0 
	MOVWF       R1 
	CLRF        R2 
	MOVLW       0
	SUBWFB      R2, 1 
	MOVF        MotorXY_M_L0+1, 0 
	XORWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY399
	MOVF        R1, 0 
	XORWF       MotorXY_M_L0+0, 0 
L__MotorXY399:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY147
;PIC18F45K22.c,681 :: 		if (counterStepX> ( xSteps- 1600))
	MOVLW       64
	SUBWF       FARG_MotorXY_xSteps+0, 0 
	MOVWF       R1 
	MOVLW       6
	SUBWFB      FARG_MotorXY_xSteps+1, 0 
	MOVWF       R2 
	MOVF        MotorXY_counterStepX_L0+1, 0 
	SUBWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY400
	MOVF        MotorXY_counterStepX_L0+0, 0 
	SUBWF       R1, 0 
L__MotorXY400:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY148
;PIC18F45K22.c,682 :: 		for (z= 0; z <  (  counterStepX -  (xSteps - 1600) ) ;  z+=160)
	CLRF        MotorXY_z_L0+0 
	CLRF        MotorXY_z_L0+1 
L_MotorXY149:
	MOVLW       64
	SUBWF       FARG_MotorXY_xSteps+0, 0 
	MOVWF       R0 
	MOVLW       6
	SUBWFB      FARG_MotorXY_xSteps+1, 0 
	MOVWF       R1 
	MOVF        R0, 0 
	SUBWF       MotorXY_counterStepX_L0+0, 0 
	MOVWF       R2 
	MOVF        R1, 0 
	SUBWFB      MotorXY_counterStepX_L0+1, 0 
	MOVWF       R3 
	MOVF        R3, 0 
	SUBWF       MotorXY_z_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY401
	MOVF        R2, 0 
	SUBWF       MotorXY_z_L0+0, 0 
L__MotorXY401:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY150
;PIC18F45K22.c,683 :: 		delay_us(1);
	MOVLW       4
	MOVWF       R13, 0
L_MotorXY152:
	DECFSZ      R13, 1, 1
	BRA         L_MotorXY152
	NOP
	NOP
;PIC18F45K22.c,682 :: 		for (z= 0; z <  (  counterStepX -  (xSteps - 1600) ) ;  z+=160)
	MOVLW       160
	ADDWF       MotorXY_z_L0+0, 1 
	MOVLW       0
	ADDWFC      MotorXY_z_L0+1, 1 
;PIC18F45K22.c,683 :: 		delay_us(1);
	GOTO        L_MotorXY149
L_MotorXY150:
L_MotorXY148:
;PIC18F45K22.c,684 :: 		}
L_MotorXY147:
;PIC18F45K22.c,686 :: 		TMR0IE_bit=0;                             // disable timer0
	BCF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,687 :: 		TMR0H= (timerStepperDelayX & 0XFF00)>>8;
	MOVLW       0
	ANDWF       MotorXY_timerStepperDelayX_L0+0, 0 
	MOVWF       R3 
	MOVF        MotorXY_timerStepperDelayX_L0+1, 0 
	ANDLW       255
	MOVWF       R4 
	MOVF        R4, 0 
	MOVWF       R0 
	CLRF        R1 
	MOVF        R0, 0 
	MOVWF       TMR0H+0 
;PIC18F45K22.c,688 :: 		TMR0L=  timerStepperDelayX & 0X00FF;
	MOVLW       255
	ANDWF       MotorXY_timerStepperDelayX_L0+0, 0 
	MOVWF       TMR0L+0 
;PIC18F45K22.c,689 :: 		TMR0IE_bit=1; // enable timer1
	BSF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,690 :: 		countTimerX=0;
	CLRF        _countTimerX+0 
;PIC18F45K22.c,691 :: 		StepX=0;
	BCF         LATD+0, 1 
;PIC18F45K22.c,692 :: 		}
L_MotorXY140:
;PIC18F45K22.c,695 :: 		if ((countTimerY>0)&&(counterStepY<ySteps))
	MOVF        _countTimerY+0, 0 
	SUBLW       0
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY155
	MOVF        FARG_MotorXY_ySteps+1, 0 
	SUBWF       MotorXY_counterStepY_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY402
	MOVF        FARG_MotorXY_ySteps+0, 0 
	SUBWF       MotorXY_counterStepY_L0+0, 0 
L__MotorXY402:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY155
L__MotorXY316:
;PIC18F45K22.c,697 :: 		StepYR=1;
	BSF         LATC+0, 3 
;PIC18F45K22.c,698 :: 		StepYL=1;
	BSF         LATC+0, 2 
;PIC18F45K22.c,699 :: 		counterStepY++;
	INFSNZ      MotorXY_counterStepY_L0+0, 1 
	INCF        MotorXY_counterStepY_L0+1, 1 
;PIC18F45K22.c,703 :: 		if (M==0)
	MOVLW       0
	XORWF       MotorXY_M_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY403
	MOVLW       0
	XORWF       MotorXY_M_L0+0, 0 
L__MotorXY403:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY156
;PIC18F45K22.c,705 :: 		if (counterStepY< partOf1600)
	MOVF        MotorXY_PartOf1600_L0+1, 0 
	SUBWF       MotorXY_counterStepY_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY404
	MOVF        MotorXY_PartOf1600_L0+0, 0 
	SUBWF       MotorXY_counterStepY_L0+0, 0 
L__MotorXY404:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY157
;PIC18F45K22.c,706 :: 		for (z=counterStepY; z <(partOf1600- counterStepY) ;z+=partOf160)
	MOVF        MotorXY_counterStepY_L0+0, 0 
	MOVWF       MotorXY_z_L0+0 
	MOVF        MotorXY_counterStepY_L0+1, 0 
	MOVWF       MotorXY_z_L0+1 
L_MotorXY158:
	MOVF        MotorXY_counterStepY_L0+0, 0 
	SUBWF       MotorXY_PartOf1600_L0+0, 0 
	MOVWF       R1 
	MOVF        MotorXY_counterStepY_L0+1, 0 
	SUBWFB      MotorXY_PartOf1600_L0+1, 0 
	MOVWF       R2 
	MOVF        R2, 0 
	SUBWF       MotorXY_z_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY405
	MOVF        R1, 0 
	SUBWF       MotorXY_z_L0+0, 0 
L__MotorXY405:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY159
;PIC18F45K22.c,707 :: 		delay_us(1);
	MOVLW       4
	MOVWF       R13, 0
L_MotorXY161:
	DECFSZ      R13, 1, 1
	BRA         L_MotorXY161
	NOP
	NOP
;PIC18F45K22.c,706 :: 		for (z=counterStepY; z <(partOf1600- counterStepY) ;z+=partOf160)
	MOVF        MotorXY_PartOf160_L0+0, 0 
	ADDWF       MotorXY_z_L0+0, 1 
	MOVF        MotorXY_PartOf160_L0+1, 0 
	ADDWFC      MotorXY_z_L0+1, 1 
;PIC18F45K22.c,707 :: 		delay_us(1);
	GOTO        L_MotorXY158
L_MotorXY159:
L_MotorXY157:
;PIC18F45K22.c,708 :: 		}
L_MotorXY156:
;PIC18F45K22.c,710 :: 		if (M==(parts-1))
	DECF        FARG_MotorXY_parts+0, 0 
	MOVWF       R1 
	CLRF        R2 
	MOVLW       0
	SUBWFB      R2, 1 
	MOVF        MotorXY_M_L0+1, 0 
	XORWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY406
	MOVF        R1, 0 
	XORWF       MotorXY_M_L0+0, 0 
L__MotorXY406:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY162
;PIC18F45K22.c,712 :: 		if (counterStepY> ( ySteps- partOf1600))
	MOVF        MotorXY_PartOf1600_L0+0, 0 
	SUBWF       FARG_MotorXY_ySteps+0, 0 
	MOVWF       R1 
	MOVF        MotorXY_PartOf1600_L0+1, 0 
	SUBWFB      FARG_MotorXY_ySteps+1, 0 
	MOVWF       R2 
	MOVF        MotorXY_counterStepY_L0+1, 0 
	SUBWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY407
	MOVF        MotorXY_counterStepY_L0+0, 0 
	SUBWF       R1, 0 
L__MotorXY407:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY163
;PIC18F45K22.c,713 :: 		for (z= 0; z <  (  counterStepY -  (ySteps - partOf1600) ) ;z+=partOf160)
	CLRF        MotorXY_z_L0+0 
	CLRF        MotorXY_z_L0+1 
L_MotorXY164:
	MOVF        MotorXY_PartOf1600_L0+0, 0 
	SUBWF       FARG_MotorXY_ySteps+0, 0 
	MOVWF       R0 
	MOVF        MotorXY_PartOf1600_L0+1, 0 
	SUBWFB      FARG_MotorXY_ySteps+1, 0 
	MOVWF       R1 
	MOVF        R0, 0 
	SUBWF       MotorXY_counterStepY_L0+0, 0 
	MOVWF       R2 
	MOVF        R1, 0 
	SUBWFB      MotorXY_counterStepY_L0+1, 0 
	MOVWF       R3 
	MOVF        R3, 0 
	SUBWF       MotorXY_z_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY408
	MOVF        R2, 0 
	SUBWF       MotorXY_z_L0+0, 0 
L__MotorXY408:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY165
;PIC18F45K22.c,714 :: 		delay_us(1);
	MOVLW       4
	MOVWF       R13, 0
L_MotorXY167:
	DECFSZ      R13, 1, 1
	BRA         L_MotorXY167
	NOP
	NOP
;PIC18F45K22.c,713 :: 		for (z= 0; z <  (  counterStepY -  (ySteps - partOf1600) ) ;z+=partOf160)
	MOVF        MotorXY_PartOf160_L0+0, 0 
	ADDWF       MotorXY_z_L0+0, 1 
	MOVF        MotorXY_PartOf160_L0+1, 0 
	ADDWFC      MotorXY_z_L0+1, 1 
;PIC18F45K22.c,714 :: 		delay_us(1);
	GOTO        L_MotorXY164
L_MotorXY165:
L_MotorXY163:
;PIC18F45K22.c,715 :: 		}
L_MotorXY162:
;PIC18F45K22.c,719 :: 		TMR1IE_bit=0;  // disable timer1
	BCF         TMR1IE_bit+0, BitPos(TMR1IE_bit+0) 
;PIC18F45K22.c,720 :: 		TMR1H= (timerStepperDelayY & 0XFF00)>>8;
	MOVLW       0
	ANDWF       MotorXY_timerStepperDelayY_L0+0, 0 
	MOVWF       R3 
	MOVF        MotorXY_timerStepperDelayY_L0+1, 0 
	ANDLW       255
	MOVWF       R4 
	MOVF        R4, 0 
	MOVWF       R0 
	CLRF        R1 
	MOVF        R0, 0 
	MOVWF       TMR1H+0 
;PIC18F45K22.c,721 :: 		TMR1L=  timerStepperDelayY & 0X00FF;
	MOVLW       255
	ANDWF       MotorXY_timerStepperDelayY_L0+0, 0 
	MOVWF       TMR1L+0 
;PIC18F45K22.c,722 :: 		countTimerY=0;
	CLRF        _countTimerY+0 
;PIC18F45K22.c,724 :: 		StepYR=0;
	BCF         LATC+0, 3 
;PIC18F45K22.c,725 :: 		StepYL=0;
	BCF         LATC+0, 2 
;PIC18F45K22.c,726 :: 		TMR1IE_bit=1; // enable timer1 Y
	BSF         TMR1IE_bit+0, BitPos(TMR1IE_bit+0) 
;PIC18F45K22.c,727 :: 		}
L_MotorXY155:
;PIC18F45K22.c,729 :: 		}  while (counterStepX < xSteps );
	MOVF        FARG_MotorXY_xSteps+1, 0 
	SUBWF       MotorXY_counterStepX_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY409
	MOVF        FARG_MotorXY_xSteps+0, 0 
	SUBWF       MotorXY_counterStepX_L0+0, 0 
L__MotorXY409:
	BTFSS       STATUS+0, 0 
	GOTO        L_MotorXY130
L_MotorXY131:
;PIC18F45K22.c,653 :: 		for (M=0;M<parts;M++)
	INFSNZ      MotorXY_M_L0+0, 1 
	INCF        MotorXY_M_L0+1, 1 
;PIC18F45K22.c,732 :: 		} // end M
	GOTO        L_MotorXY127
L_MotorXY128:
;PIC18F45K22.c,733 :: 		}   //ySteps >  xSteps
	GOTO        L_MotorXY168
L_MotorXY125:
;PIC18F45K22.c,737 :: 		else if (ySteps>xSteps)
	MOVF        FARG_MotorXY_ySteps+1, 0 
	SUBWF       FARG_MotorXY_xSteps+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY410
	MOVF        FARG_MotorXY_ySteps+0, 0 
	SUBWF       FARG_MotorXY_xSteps+0, 0 
L__MotorXY410:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY169
;PIC18F45K22.c,741 :: 		partof1600 = (unsigned int)(((long)1600*(long)xSteps)/(long)ySteps)  ;
	MOVF        FARG_MotorXY_xSteps+0, 0 
	MOVWF       R0 
	MOVF        FARG_MotorXY_xSteps+1, 0 
	MOVWF       R1 
	MOVLW       0
	MOVWF       R2 
	MOVWF       R3 
	MOVLW       64
	MOVWF       R4 
	MOVLW       6
	MOVWF       R5 
	MOVLW       0
	MOVWF       R6 
	MOVLW       0
	MOVWF       R7 
	CALL        _Mul_32x32_U+0, 0
	MOVF        FARG_MotorXY_ySteps+0, 0 
	MOVWF       R4 
	MOVF        FARG_MotorXY_ySteps+1, 0 
	MOVWF       R5 
	MOVLW       0
	MOVWF       R6 
	MOVWF       R7 
	CALL        _Div_32x32_S+0, 0
	MOVF        R0, 0 
	MOVWF       MotorXY_PartOf1600_L0+0 
	MOVF        R1, 0 
	MOVWF       MotorXY_PartOf1600_L0+1 
;PIC18F45K22.c,742 :: 		partOf160 =  (unsigned int)(((long)partOf1600*(long)xSteps)/(long)ySteps)  ;
	MOVF        R0, 0 
	MOVWF       R4 
	MOVF        R1, 0 
	MOVWF       R5 
	MOVLW       0
	MOVWF       R6 
	MOVWF       R7 
	MOVF        FARG_MotorXY_xSteps+0, 0 
	MOVWF       R0 
	MOVF        FARG_MotorXY_xSteps+1, 0 
	MOVWF       R1 
	MOVLW       0
	MOVWF       R2 
	MOVWF       R3 
	CALL        _Mul_32x32_U+0, 0
	MOVF        FARG_MotorXY_ySteps+0, 0 
	MOVWF       R4 
	MOVF        FARG_MotorXY_ySteps+1, 0 
	MOVWF       R5 
	MOVLW       0
	MOVWF       R6 
	MOVWF       R7 
	CALL        _Div_32x32_S+0, 0
	MOVF        R0, 0 
	MOVWF       MotorXY_PartOf160_L0+0 
	MOVF        R1, 0 
	MOVWF       MotorXY_PartOf160_L0+1 
;PIC18F45K22.c,743 :: 		if (partOf160==0) partOf160=1;
	MOVLW       0
	XORWF       R1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY411
	MOVLW       0
	XORWF       R0, 0 
L__MotorXY411:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY170
	MOVLW       1
	MOVWF       MotorXY_PartOf160_L0+0 
	MOVLW       0
	MOVWF       MotorXY_PartOf160_L0+1 
L_MotorXY170:
;PIC18F45K22.c,747 :: 		stepperDelayY=XYMotorDelay*2*15;    // * 2 2*delay *15 from 14.7456MHZ clock count/sec
	MOVF        _XYMotorDelay+0, 0 
	MOVWF       R0 
	MOVF        _XYMotorDelay+1, 0 
	MOVWF       R1 
	RLCF        R0, 1 
	BCF         R0, 0 
	RLCF        R1, 1 
	MOVLW       15
	MOVWF       R4 
	MOVLW       0
	MOVWF       R5 
	CALL        _Mul_16X16_U+0, 0
	MOVF        R0, 0 
	MOVWF       MotorXY_stepperDelayY_L0+0 
	MOVF        R1, 0 
	MOVWF       MotorXY_stepperDelayY_L0+1 
	MOVLW       0
	BTFSC       R1, 7 
	MOVLW       255
	MOVWF       MotorXY_stepperDelayY_L0+2 
	MOVWF       MotorXY_stepperDelayY_L0+3 
;PIC18F45K22.c,749 :: 		stepperDelayX=(unsigned int)(((long)stepperDelayY*(long)ySteps)/(long)xSteps);  // +80 nearly ok
	MOVF        FARG_MotorXY_ySteps+0, 0 
	MOVWF       R0 
	MOVF        FARG_MotorXY_ySteps+1, 0 
	MOVWF       R1 
	MOVLW       0
	MOVWF       R2 
	MOVWF       R3 
	MOVF        MotorXY_stepperDelayY_L0+0, 0 
	MOVWF       R4 
	MOVF        MotorXY_stepperDelayY_L0+1, 0 
	MOVWF       R5 
	MOVF        MotorXY_stepperDelayY_L0+2, 0 
	MOVWF       R6 
	MOVF        MotorXY_stepperDelayY_L0+3, 0 
	MOVWF       R7 
	CALL        _Mul_32x32_U+0, 0
	MOVF        FARG_MotorXY_xSteps+0, 0 
	MOVWF       R4 
	MOVF        FARG_MotorXY_xSteps+1, 0 
	MOVWF       R5 
	MOVLW       0
	MOVWF       R6 
	MOVWF       R7 
	CALL        _Div_32x32_S+0, 0
	MOVF        R0, 0 
	MOVWF       MotorXY_stepperDelayX_L0+0 
	MOVF        R1, 0 
	MOVWF       MotorXY_stepperDelayX_L0+1 
	MOVLW       0
	MOVWF       MotorXY_stepperDelayX_L0+2 
	MOVWF       MotorXY_stepperDelayX_L0+3 
;PIC18F45K22.c,750 :: 		timerStepperDelayX = (unsigned int)(65536-(unsigned int)(stepperdelayX));  //
	MOVF        MotorXY_stepperDelayX_L0+0, 0 
	SUBLW       0
	MOVWF       R7 
	MOVF        MotorXY_stepperDelayX_L0+1, 0 
	MOVWF       R8 
	MOVLW       0
	SUBFWB      R8, 1 
	MOVF        R7, 0 
	MOVWF       MotorXY_timerStepperDelayX_L0+0 
	MOVF        R8, 0 
	MOVWF       MotorXY_timerStepperDelayX_L0+1 
;PIC18F45K22.c,751 :: 		timerStepperDelayY = (unsigned int)(65536-(unsigned int)(stepperdelayY) );
	MOVF        MotorXY_stepperDelayY_L0+0, 0 
	SUBLW       0
	MOVWF       R5 
	MOVF        MotorXY_stepperDelayY_L0+1, 0 
	MOVWF       R6 
	MOVLW       0
	SUBFWB      R6, 1 
	MOVF        R5, 0 
	MOVWF       MotorXY_timerStepperDelayY_L0+0 
	MOVF        R6, 0 
	MOVWF       MotorXY_timerStepperDelayY_L0+1 
;PIC18F45K22.c,753 :: 		TMR0IE_bit=0;                              // disable timer0
	BCF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,754 :: 		TMR0H= (timerStepperDelayX & 0XFF00)>>8;
	MOVLW       0
	ANDWF       R7, 0 
	MOVWF       R3 
	MOVF        R8, 0 
	ANDLW       255
	MOVWF       R4 
	MOVF        R4, 0 
	MOVWF       R0 
	CLRF        R1 
	MOVF        R0, 0 
	MOVWF       TMR0H+0 
;PIC18F45K22.c,755 :: 		TMR0L=  timerStepperDelayX & 0X00FF;
	MOVLW       255
	ANDWF       R7, 0 
	MOVWF       TMR0L+0 
;PIC18F45K22.c,756 :: 		TMR0IE_bit=1;                             // enable timer0
	BSF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,758 :: 		TMR1IE_bit=0;                             // disable timer1
	BCF         TMR1IE_bit+0, BitPos(TMR1IE_bit+0) 
;PIC18F45K22.c,759 :: 		TMR1H= (timerStepperDelayY & 0XFF00)>>8;
	MOVLW       0
	ANDWF       R5, 0 
	MOVWF       R3 
	MOVF        R6, 0 
	ANDLW       255
	MOVWF       R4 
	MOVF        R4, 0 
	MOVWF       R0 
	CLRF        R1 
	MOVF        R0, 0 
	MOVWF       TMR1H+0 
;PIC18F45K22.c,760 :: 		TMR1L=  timerStepperDelayY & 0X00FF;
	MOVLW       255
	ANDWF       R5, 0 
	MOVWF       TMR1L+0 
;PIC18F45K22.c,761 :: 		TMR1IE_bit=1;                             // enable timer1
	BSF         TMR1IE_bit+0, BitPos(TMR1IE_bit+0) 
;PIC18F45K22.c,763 :: 		for (M=0;M<parts;M++)
	CLRF        MotorXY_M_L0+0 
	CLRF        MotorXY_M_L0+1 
L_MotorXY171:
	MOVLW       0
	SUBWF       MotorXY_M_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY412
	MOVF        FARG_MotorXY_parts+0, 0 
	SUBWF       MotorXY_M_L0+0, 0 
L__MotorXY412:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY172
;PIC18F45K22.c,765 :: 		counterStepX=0;
	CLRF        MotorXY_counterStepX_L0+0 
	CLRF        MotorXY_counterStepX_L0+1 
;PIC18F45K22.c,766 :: 		counterStepY=0;
	CLRF        MotorXY_counterStepY_L0+0 
	CLRF        MotorXY_counterStepY_L0+1 
;PIC18F45K22.c,768 :: 		do
L_MotorXY174:
;PIC18F45K22.c,770 :: 		if (YLCORNERSWITCH==0) {  Uart1_Write(47); break; } //
	BTFSC       PORTB+0, 1 
	GOTO        L_MotorXY177
	MOVLW       47
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY175
L_MotorXY177:
;PIC18F45K22.c,771 :: 		if (YRCORNERSWITCH==0) {  Uart1_Write(48); break;  }
	BTFSC       PORTB+0, 2 
	GOTO        L_MotorXY178
	MOVLW       48
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY175
L_MotorXY178:
;PIC18F45K22.c,772 :: 		if (Y2CORNERSWITCH==0) {  Uart1_Write(49); break; }
	BTFSC       PORTB+0, 3 
	GOTO        L_MotorXY179
	MOVLW       49
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY175
L_MotorXY179:
;PIC18F45K22.c,773 :: 		if (XRCORNERSWITCH==0) {  Uart1_Write(46); break; }
	BTFSC       PORTB+0, 5 
	GOTO        L_MotorXY180
	MOVLW       46
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY175
L_MotorXY180:
;PIC18F45K22.c,774 :: 		if (XLCORNERSWITCH==0) {  Uart1_Write(45);break; }
	BTFSC       PORTB+0, 4 
	GOTO        L_MotorXY181
	MOVLW       45
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
	GOTO        L_MotorXY175
L_MotorXY181:
;PIC18F45K22.c,777 :: 		if((countTimerY>0) && (counterStepY<ySteps))
	MOVF        _countTimerY+0, 0 
	SUBLW       0
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY184
	MOVF        FARG_MotorXY_ySteps+1, 0 
	SUBWF       MotorXY_counterStepY_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY413
	MOVF        FARG_MotorXY_ySteps+0, 0 
	SUBWF       MotorXY_counterStepY_L0+0, 0 
L__MotorXY413:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY184
L__MotorXY315:
;PIC18F45K22.c,779 :: 		StepYR=1;
	BSF         LATC+0, 3 
;PIC18F45K22.c,780 :: 		StepYL=1;
	BSF         LATC+0, 2 
;PIC18F45K22.c,781 :: 		counterStepY++;
	INFSNZ      MotorXY_counterStepY_L0+0, 1 
	INCF        MotorXY_counterStepY_L0+1, 1 
;PIC18F45K22.c,783 :: 		if (M==0)
	MOVLW       0
	XORWF       MotorXY_M_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY414
	MOVLW       0
	XORWF       MotorXY_M_L0+0, 0 
L__MotorXY414:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY185
;PIC18F45K22.c,785 :: 		if (counterStepY<1600)
	MOVLW       6
	SUBWF       MotorXY_counterStepY_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY415
	MOVLW       64
	SUBWF       MotorXY_counterStepY_L0+0, 0 
L__MotorXY415:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY186
;PIC18F45K22.c,786 :: 		for (z=counterStepY; z <(1600- counterStepY) ;z+=160)
	MOVF        MotorXY_counterStepY_L0+0, 0 
	MOVWF       MotorXY_z_L0+0 
	MOVF        MotorXY_counterStepY_L0+1, 0 
	MOVWF       MotorXY_z_L0+1 
L_MotorXY187:
	MOVF        MotorXY_counterStepY_L0+0, 0 
	SUBLW       64
	MOVWF       R1 
	MOVF        MotorXY_counterStepY_L0+1, 0 
	MOVWF       R2 
	MOVLW       6
	SUBFWB      R2, 1 
	MOVF        R2, 0 
	SUBWF       MotorXY_z_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY416
	MOVF        R1, 0 
	SUBWF       MotorXY_z_L0+0, 0 
L__MotorXY416:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY188
;PIC18F45K22.c,787 :: 		delay_us(1); // later less
	MOVLW       4
	MOVWF       R13, 0
L_MotorXY190:
	DECFSZ      R13, 1, 1
	BRA         L_MotorXY190
	NOP
	NOP
;PIC18F45K22.c,786 :: 		for (z=counterStepY; z <(1600- counterStepY) ;z+=160)
	MOVLW       160
	ADDWF       MotorXY_z_L0+0, 1 
	MOVLW       0
	ADDWFC      MotorXY_z_L0+1, 1 
;PIC18F45K22.c,787 :: 		delay_us(1); // later less
	GOTO        L_MotorXY187
L_MotorXY188:
L_MotorXY186:
;PIC18F45K22.c,788 :: 		}
L_MotorXY185:
;PIC18F45K22.c,790 :: 		if (M==(parts-1))
	DECF        FARG_MotorXY_parts+0, 0 
	MOVWF       R1 
	CLRF        R2 
	MOVLW       0
	SUBWFB      R2, 1 
	MOVF        MotorXY_M_L0+1, 0 
	XORWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY417
	MOVF        R1, 0 
	XORWF       MotorXY_M_L0+0, 0 
L__MotorXY417:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY191
;PIC18F45K22.c,792 :: 		if (counterStepY> ( ySteps- 1600))
	MOVLW       64
	SUBWF       FARG_MotorXY_ySteps+0, 0 
	MOVWF       R1 
	MOVLW       6
	SUBWFB      FARG_MotorXY_ySteps+1, 0 
	MOVWF       R2 
	MOVF        MotorXY_counterStepY_L0+1, 0 
	SUBWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY418
	MOVF        MotorXY_counterStepY_L0+0, 0 
	SUBWF       R1, 0 
L__MotorXY418:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY192
;PIC18F45K22.c,793 :: 		for (z= 0; z <  (  counterStepY -  (ySteps - 1600) ) ;  z+=160)
	CLRF        MotorXY_z_L0+0 
	CLRF        MotorXY_z_L0+1 
L_MotorXY193:
	MOVLW       64
	SUBWF       FARG_MotorXY_ySteps+0, 0 
	MOVWF       R0 
	MOVLW       6
	SUBWFB      FARG_MotorXY_ySteps+1, 0 
	MOVWF       R1 
	MOVF        R0, 0 
	SUBWF       MotorXY_counterStepY_L0+0, 0 
	MOVWF       R2 
	MOVF        R1, 0 
	SUBWFB      MotorXY_counterStepY_L0+1, 0 
	MOVWF       R3 
	MOVF        R3, 0 
	SUBWF       MotorXY_z_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY419
	MOVF        R2, 0 
	SUBWF       MotorXY_z_L0+0, 0 
L__MotorXY419:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY194
;PIC18F45K22.c,794 :: 		delay_us(1);
	MOVLW       4
	MOVWF       R13, 0
L_MotorXY196:
	DECFSZ      R13, 1, 1
	BRA         L_MotorXY196
	NOP
	NOP
;PIC18F45K22.c,793 :: 		for (z= 0; z <  (  counterStepY -  (ySteps - 1600) ) ;  z+=160)
	MOVLW       160
	ADDWF       MotorXY_z_L0+0, 1 
	MOVLW       0
	ADDWFC      MotorXY_z_L0+1, 1 
;PIC18F45K22.c,794 :: 		delay_us(1);
	GOTO        L_MotorXY193
L_MotorXY194:
L_MotorXY192:
;PIC18F45K22.c,795 :: 		}
L_MotorXY191:
;PIC18F45K22.c,797 :: 		TMR1IE_bit=0;                              // disable timer0
	BCF         TMR1IE_bit+0, BitPos(TMR1IE_bit+0) 
;PIC18F45K22.c,798 :: 		TMR1H= (timerStepperDelayY & 0XFF00)>>8;
	MOVLW       0
	ANDWF       MotorXY_timerStepperDelayY_L0+0, 0 
	MOVWF       R3 
	MOVF        MotorXY_timerStepperDelayY_L0+1, 0 
	ANDLW       255
	MOVWF       R4 
	MOVF        R4, 0 
	MOVWF       R0 
	CLRF        R1 
	MOVF        R0, 0 
	MOVWF       TMR1H+0 
;PIC18F45K22.c,799 :: 		TMR1L=  timerStepperDelayY & 0X00FF;
	MOVLW       255
	ANDWF       MotorXY_timerStepperDelayY_L0+0, 0 
	MOVWF       TMR1L+0 
;PIC18F45K22.c,800 :: 		TMR1IE_bit=1; // enable timer1
	BSF         TMR1IE_bit+0, BitPos(TMR1IE_bit+0) 
;PIC18F45K22.c,801 :: 		countTimerY=0;
	CLRF        _countTimerY+0 
;PIC18F45K22.c,802 :: 		StepYR=0;
	BCF         LATC+0, 3 
;PIC18F45K22.c,803 :: 		StepYL=0;
	BCF         LATC+0, 2 
;PIC18F45K22.c,804 :: 		}
L_MotorXY184:
;PIC18F45K22.c,808 :: 		if ((countTimerX>0)&&(counterStepX<xSteps))
	MOVF        _countTimerX+0, 0 
	SUBLW       0
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY199
	MOVF        FARG_MotorXY_xSteps+1, 0 
	SUBWF       MotorXY_counterStepX_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY420
	MOVF        FARG_MotorXY_xSteps+0, 0 
	SUBWF       MotorXY_counterStepX_L0+0, 0 
L__MotorXY420:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY199
L__MotorXY314:
;PIC18F45K22.c,810 :: 		StepX=1;
	BSF         LATD+0, 1 
;PIC18F45K22.c,811 :: 		counterStepX++;
	INFSNZ      MotorXY_counterStepX_L0+0, 1 
	INCF        MotorXY_counterStepX_L0+1, 1 
;PIC18F45K22.c,814 :: 		if (M==0)
	MOVLW       0
	XORWF       MotorXY_M_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY421
	MOVLW       0
	XORWF       MotorXY_M_L0+0, 0 
L__MotorXY421:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY200
;PIC18F45K22.c,816 :: 		if (counterStepX<partOf1600)
	MOVF        MotorXY_PartOf1600_L0+1, 0 
	SUBWF       MotorXY_counterStepX_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY422
	MOVF        MotorXY_PartOf1600_L0+0, 0 
	SUBWF       MotorXY_counterStepX_L0+0, 0 
L__MotorXY422:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY201
;PIC18F45K22.c,817 :: 		for (z=counterStepX; z <(partOf1600- counterStepX) ;z+=partOf160)
	MOVF        MotorXY_counterStepX_L0+0, 0 
	MOVWF       MotorXY_z_L0+0 
	MOVF        MotorXY_counterStepX_L0+1, 0 
	MOVWF       MotorXY_z_L0+1 
L_MotorXY202:
	MOVF        MotorXY_counterStepX_L0+0, 0 
	SUBWF       MotorXY_PartOf1600_L0+0, 0 
	MOVWF       R1 
	MOVF        MotorXY_counterStepX_L0+1, 0 
	SUBWFB      MotorXY_PartOf1600_L0+1, 0 
	MOVWF       R2 
	MOVF        R2, 0 
	SUBWF       MotorXY_z_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY423
	MOVF        R1, 0 
	SUBWF       MotorXY_z_L0+0, 0 
L__MotorXY423:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY203
;PIC18F45K22.c,818 :: 		delay_us(1);
	MOVLW       4
	MOVWF       R13, 0
L_MotorXY205:
	DECFSZ      R13, 1, 1
	BRA         L_MotorXY205
	NOP
	NOP
;PIC18F45K22.c,817 :: 		for (z=counterStepX; z <(partOf1600- counterStepX) ;z+=partOf160)
	MOVF        MotorXY_PartOf160_L0+0, 0 
	ADDWF       MotorXY_z_L0+0, 1 
	MOVF        MotorXY_PartOf160_L0+1, 0 
	ADDWFC      MotorXY_z_L0+1, 1 
;PIC18F45K22.c,818 :: 		delay_us(1);
	GOTO        L_MotorXY202
L_MotorXY203:
L_MotorXY201:
;PIC18F45K22.c,819 :: 		}
L_MotorXY200:
;PIC18F45K22.c,821 :: 		if (M==(parts-1))
	DECF        FARG_MotorXY_parts+0, 0 
	MOVWF       R1 
	CLRF        R2 
	MOVLW       0
	SUBWFB      R2, 1 
	MOVF        MotorXY_M_L0+1, 0 
	XORWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY424
	MOVF        R1, 0 
	XORWF       MotorXY_M_L0+0, 0 
L__MotorXY424:
	BTFSS       STATUS+0, 2 
	GOTO        L_MotorXY206
;PIC18F45K22.c,823 :: 		if (counterStepX> ( ySteps- partOf1600))
	MOVF        MotorXY_PartOf1600_L0+0, 0 
	SUBWF       FARG_MotorXY_ySteps+0, 0 
	MOVWF       R1 
	MOVF        MotorXY_PartOf1600_L0+1, 0 
	SUBWFB      FARG_MotorXY_ySteps+1, 0 
	MOVWF       R2 
	MOVF        MotorXY_counterStepX_L0+1, 0 
	SUBWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY425
	MOVF        MotorXY_counterStepX_L0+0, 0 
	SUBWF       R1, 0 
L__MotorXY425:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY207
;PIC18F45K22.c,824 :: 		for (z= 0; z <  (  counterStepX -  (xSteps - partOf1600) ); z+=partOf160)
	CLRF        MotorXY_z_L0+0 
	CLRF        MotorXY_z_L0+1 
L_MotorXY208:
	MOVF        MotorXY_PartOf1600_L0+0, 0 
	SUBWF       FARG_MotorXY_xSteps+0, 0 
	MOVWF       R0 
	MOVF        MotorXY_PartOf1600_L0+1, 0 
	SUBWFB      FARG_MotorXY_xSteps+1, 0 
	MOVWF       R1 
	MOVF        R0, 0 
	SUBWF       MotorXY_counterStepX_L0+0, 0 
	MOVWF       R2 
	MOVF        R1, 0 
	SUBWFB      MotorXY_counterStepX_L0+1, 0 
	MOVWF       R3 
	MOVF        R3, 0 
	SUBWF       MotorXY_z_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY426
	MOVF        R2, 0 
	SUBWF       MotorXY_z_L0+0, 0 
L__MotorXY426:
	BTFSC       STATUS+0, 0 
	GOTO        L_MotorXY209
;PIC18F45K22.c,825 :: 		delay_us(1);
	MOVLW       4
	MOVWF       R13, 0
L_MotorXY211:
	DECFSZ      R13, 1, 1
	BRA         L_MotorXY211
	NOP
	NOP
;PIC18F45K22.c,824 :: 		for (z= 0; z <  (  counterStepX -  (xSteps - partOf1600) ); z+=partOf160)
	MOVF        MotorXY_PartOf160_L0+0, 0 
	ADDWF       MotorXY_z_L0+0, 1 
	MOVF        MotorXY_PartOf160_L0+1, 0 
	ADDWFC      MotorXY_z_L0+1, 1 
;PIC18F45K22.c,825 :: 		delay_us(1);
	GOTO        L_MotorXY208
L_MotorXY209:
L_MotorXY207:
;PIC18F45K22.c,826 :: 		}
L_MotorXY206:
;PIC18F45K22.c,829 :: 		TMR0IE_bit=0;  // disable timer1
	BCF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,830 :: 		TMR0H= (timerStepperDelayX & 0XFF00)>>8;
	MOVLW       0
	ANDWF       MotorXY_timerStepperDelayX_L0+0, 0 
	MOVWF       R3 
	MOVF        MotorXY_timerStepperDelayX_L0+1, 0 
	ANDLW       255
	MOVWF       R4 
	MOVF        R4, 0 
	MOVWF       R0 
	CLRF        R1 
	MOVF        R0, 0 
	MOVWF       TMR0H+0 
;PIC18F45K22.c,831 :: 		TMR0L=  timerStepperDelayX & 0X00FF;
	MOVLW       255
	ANDWF       MotorXY_timerStepperDelayX_L0+0, 0 
	MOVWF       TMR0L+0 
;PIC18F45K22.c,832 :: 		countTimerX=0;
	CLRF        _countTimerX+0 
;PIC18F45K22.c,834 :: 		StepX=0;
	BCF         LATD+0, 1 
;PIC18F45K22.c,835 :: 		TMR0IE_bit=1; // enable timer1
	BSF         TMR0IE_bit+0, BitPos(TMR0IE_bit+0) 
;PIC18F45K22.c,836 :: 		}
L_MotorXY199:
;PIC18F45K22.c,838 :: 		}  while (counterStepY < ySteps );
	MOVF        FARG_MotorXY_ySteps+1, 0 
	SUBWF       MotorXY_counterStepY_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__MotorXY427
	MOVF        FARG_MotorXY_ySteps+0, 0 
	SUBWF       MotorXY_counterStepY_L0+0, 0 
L__MotorXY427:
	BTFSS       STATUS+0, 0 
	GOTO        L_MotorXY174
L_MotorXY175:
;PIC18F45K22.c,763 :: 		for (M=0;M<parts;M++)
	INFSNZ      MotorXY_M_L0+0, 1 
	INCF        MotorXY_M_L0+1, 1 
;PIC18F45K22.c,839 :: 		} // end M
	GOTO        L_MotorXY171
L_MotorXY172:
;PIC18F45K22.c,840 :: 		}   //ySteps >  xSteps
L_MotorXY169:
L_MotorXY168:
L_MotorXY124:
L_MotorXY99:
L_MotorXY73:
;PIC18F45K22.c,841 :: 		}
L_end_MotorXY:
	RETURN      0
; end of _MotorXY

_EscapeMotorXY:

;PIC18F45K22.c,846 :: 		unsigned short command4)
;PIC18F45K22.c,849 :: 		unsigned int L = 0;
	CLRF        EscapeMotorXY_L_L0+0 
	CLRF        EscapeMotorXY_L_L0+1 
;PIC18F45K22.c,851 :: 		xSteps = (unsigned int)  ( (command1 << 8 ) + command2);
	MOVF        FARG_EscapeMotorXY_command1+0, 0 
	MOVWF       R1 
	CLRF        R0 
	MOVF        FARG_EscapeMotorXY_command2+0, 0 
	ADDWF       R0, 0 
	MOVWF       R2 
	MOVLW       0
	ADDWFC      R1, 0 
	MOVWF       R3 
	MOVF        R2, 0 
	MOVWF       R4 
	MOVF        R3, 0 
	MOVWF       R5 
;PIC18F45K22.c,852 :: 		ySteps = (unsigned int)  (  (command3 << 8 )+ command4);
	MOVF        FARG_EscapeMotorXY_command3+0, 0 
	MOVWF       R7 
	CLRF        R6 
	MOVF        FARG_EscapeMotorXY_command4+0, 0 
	ADDWF       R6, 1 
	MOVLW       0
	ADDWFC      R7, 1 
;PIC18F45K22.c,854 :: 		if (xSteps==0)
	MOVLW       0
	XORWF       R3, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__EscapeMotorXY429
	MOVLW       0
	XORWF       R2, 0 
L__EscapeMotorXY429:
	BTFSS       STATUS+0, 2 
	GOTO        L_EscapeMotorXY212
;PIC18F45K22.c,856 :: 		for (L=0;L<ySteps;L++)       // -100
	CLRF        EscapeMotorXY_L_L0+0 
	CLRF        EscapeMotorXY_L_L0+1 
L_EscapeMotorXY213:
	MOVF        R7, 0 
	SUBWF       EscapeMotorXY_L_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__EscapeMotorXY430
	MOVF        R6, 0 
	SUBWF       EscapeMotorXY_L_L0+0, 0 
L__EscapeMotorXY430:
	BTFSC       STATUS+0, 0 
	GOTO        L_EscapeMotorXY214
;PIC18F45K22.c,858 :: 		StepYR=1;
	BSF         LATC+0, 3 
;PIC18F45K22.c,859 :: 		StepYL=1;
	BSF         LATC+0, 2 
;PIC18F45K22.c,860 :: 		Delay_us(200); // slow
	MOVLW       4
	MOVWF       R12, 0
	MOVLW       211
	MOVWF       R13, 0
L_EscapeMotorXY216:
	DECFSZ      R13, 1, 1
	BRA         L_EscapeMotorXY216
	DECFSZ      R12, 1, 1
	BRA         L_EscapeMotorXY216
	NOP
;PIC18F45K22.c,861 :: 		StepYR=0;
	BCF         LATC+0, 3 
;PIC18F45K22.c,862 :: 		StepYL=0;
	BCF         LATC+0, 2 
;PIC18F45K22.c,863 :: 		Delay_us(200); // slow
	MOVLW       4
	MOVWF       R12, 0
	MOVLW       211
	MOVWF       R13, 0
L_EscapeMotorXY217:
	DECFSZ      R13, 1, 1
	BRA         L_EscapeMotorXY217
	DECFSZ      R12, 1, 1
	BRA         L_EscapeMotorXY217
	NOP
;PIC18F45K22.c,856 :: 		for (L=0;L<ySteps;L++)       // -100
	INFSNZ      EscapeMotorXY_L_L0+0, 1 
	INCF        EscapeMotorXY_L_L0+1, 1 
;PIC18F45K22.c,864 :: 		}
	GOTO        L_EscapeMotorXY213
L_EscapeMotorXY214:
;PIC18F45K22.c,865 :: 		}
	GOTO        L_EscapeMotorXY218
L_EscapeMotorXY212:
;PIC18F45K22.c,866 :: 		else if (ySteps==0)       //
	MOVLW       0
	XORWF       R7, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__EscapeMotorXY431
	MOVLW       0
	XORWF       R6, 0 
L__EscapeMotorXY431:
	BTFSS       STATUS+0, 2 
	GOTO        L_EscapeMotorXY219
;PIC18F45K22.c,868 :: 		for (L=0;L<xSteps;L++)       // -100
	CLRF        EscapeMotorXY_L_L0+0 
	CLRF        EscapeMotorXY_L_L0+1 
L_EscapeMotorXY220:
	MOVF        R5, 0 
	SUBWF       EscapeMotorXY_L_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__EscapeMotorXY432
	MOVF        R4, 0 
	SUBWF       EscapeMotorXY_L_L0+0, 0 
L__EscapeMotorXY432:
	BTFSC       STATUS+0, 0 
	GOTO        L_EscapeMotorXY221
;PIC18F45K22.c,870 :: 		StepX=1;
	BSF         LATD+0, 1 
;PIC18F45K22.c,871 :: 		Delay_us(200); // slow
	MOVLW       4
	MOVWF       R12, 0
	MOVLW       211
	MOVWF       R13, 0
L_EscapeMotorXY223:
	DECFSZ      R13, 1, 1
	BRA         L_EscapeMotorXY223
	DECFSZ      R12, 1, 1
	BRA         L_EscapeMotorXY223
	NOP
;PIC18F45K22.c,872 :: 		StepX=0;
	BCF         LATD+0, 1 
;PIC18F45K22.c,873 :: 		Delay_us(200); // slow
	MOVLW       4
	MOVWF       R12, 0
	MOVLW       211
	MOVWF       R13, 0
L_EscapeMotorXY224:
	DECFSZ      R13, 1, 1
	BRA         L_EscapeMotorXY224
	DECFSZ      R12, 1, 1
	BRA         L_EscapeMotorXY224
	NOP
;PIC18F45K22.c,868 :: 		for (L=0;L<xSteps;L++)       // -100
	INFSNZ      EscapeMotorXY_L_L0+0, 1 
	INCF        EscapeMotorXY_L_L0+1, 1 
;PIC18F45K22.c,874 :: 		}
	GOTO        L_EscapeMotorXY220
L_EscapeMotorXY221:
;PIC18F45K22.c,875 :: 		}
L_EscapeMotorXY219:
L_EscapeMotorXY218:
;PIC18F45K22.c,876 :: 		}
L_end_EscapeMotorXY:
	RETURN      0
; end of _EscapeMotorXY

_Lift:

;PIC18F45K22.c,879 :: 		void Lift(unsigned int Direction, unsigned int Steps)
;PIC18F45K22.c,882 :: 		if (Direction==0) DirZ=0;       //  downwards
	MOVLW       0
	XORWF       FARG_Lift_Direction+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__Lift434
	MOVLW       0
	XORWF       FARG_Lift_Direction+0, 0 
L__Lift434:
	BTFSS       STATUS+0, 2 
	GOTO        L_Lift225
	BCF         LATE+0, 0 
	GOTO        L_Lift226
L_Lift225:
;PIC18F45K22.c,883 :: 		else DirZ=1;                    //  upwards
	BSF         LATE+0, 0 
L_Lift226:
;PIC18F45K22.c,884 :: 		for (x=0;x<Steps;x++)
	CLRF        Lift_x_L0+0 
	CLRF        Lift_x_L0+1 
L_Lift227:
	MOVF        FARG_Lift_Steps+1, 0 
	SUBWF       Lift_x_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__Lift435
	MOVF        FARG_Lift_Steps+0, 0 
	SUBWF       Lift_x_L0+0, 0 
L__Lift435:
	BTFSC       STATUS+0, 0 
	GOTO        L_Lift228
;PIC18F45K22.c,886 :: 		StepZ=0;
	BCF         LATA+0, 5 
;PIC18F45K22.c,887 :: 		ZSpeed(x,(Steps-x));
	MOVF        Lift_x_L0+0, 0 
	MOVWF       FARG_ZSpeed+0 
	MOVF        Lift_x_L0+1, 0 
	MOVWF       FARG_ZSpeed+1 
	MOVF        Lift_x_L0+0, 0 
	SUBWF       FARG_Lift_Steps+0, 0 
	MOVWF       FARG_ZSpeed+0 
	MOVF        Lift_x_L0+1, 0 
	SUBWFB      FARG_Lift_Steps+1, 0 
	MOVWF       FARG_ZSpeed+1 
	CALL        _ZSpeed+0, 0
;PIC18F45K22.c,888 :: 		StepZ=1;
	BSF         LATA+0, 5 
;PIC18F45K22.c,889 :: 		ZSpeed(x,(Steps-x));
	MOVF        Lift_x_L0+0, 0 
	MOVWF       FARG_ZSpeed+0 
	MOVF        Lift_x_L0+1, 0 
	MOVWF       FARG_ZSpeed+1 
	MOVF        Lift_x_L0+0, 0 
	SUBWF       FARG_Lift_Steps+0, 0 
	MOVWF       FARG_ZSpeed+0 
	MOVF        Lift_x_L0+1, 0 
	SUBWFB      FARG_Lift_Steps+1, 0 
	MOVWF       FARG_ZSpeed+1 
	CALL        _ZSpeed+0, 0
;PIC18F45K22.c,890 :: 		if (ZCORNERSWITCH==0)
	BTFSC       PORTB+0, 0 
	GOTO        L_Lift230
;PIC18F45K22.c,892 :: 		Uart1_Write(44);  // if top is reached
	MOVLW       44
	MOVWF       FARG_UART1_Write_data_+0 
	CALL        _UART1_Write+0, 0
;PIC18F45K22.c,893 :: 		break;
	GOTO        L_Lift228
;PIC18F45K22.c,894 :: 		}
L_Lift230:
;PIC18F45K22.c,884 :: 		for (x=0;x<Steps;x++)
	INFSNZ      Lift_x_L0+0, 1 
	INCF        Lift_x_L0+1, 1 
;PIC18F45K22.c,895 :: 		}
	GOTO        L_Lift227
L_Lift228:
;PIC18F45K22.c,896 :: 		}
L_end_Lift:
	RETURN      0
; end of _Lift

_EscapeMotorZ:

;PIC18F45K22.c,898 :: 		void EscapeMotorZ(unsigned int Steps)
;PIC18F45K22.c,901 :: 		DirZ=0;     //  always downwards !
	BCF         LATE+0, 0 
;PIC18F45K22.c,902 :: 		for (x=0;x<Steps;x++)
	CLRF        R1 
	CLRF        R2 
L_EscapeMotorZ231:
	MOVF        FARG_EscapeMotorZ_Steps+1, 0 
	SUBWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__EscapeMotorZ437
	MOVF        FARG_EscapeMotorZ_Steps+0, 0 
	SUBWF       R1, 0 
L__EscapeMotorZ437:
	BTFSC       STATUS+0, 0 
	GOTO        L_EscapeMotorZ232
;PIC18F45K22.c,904 :: 		StepZ=0;
	BCF         LATA+0, 5 
;PIC18F45K22.c,905 :: 		Delay_us(200);
	MOVLW       4
	MOVWF       R12, 0
	MOVLW       211
	MOVWF       R13, 0
L_EscapeMotorZ234:
	DECFSZ      R13, 1, 1
	BRA         L_EscapeMotorZ234
	DECFSZ      R12, 1, 1
	BRA         L_EscapeMotorZ234
	NOP
;PIC18F45K22.c,906 :: 		StepZ=1;
	BSF         LATA+0, 5 
;PIC18F45K22.c,907 :: 		Delay_us(200);
	MOVLW       4
	MOVWF       R12, 0
	MOVLW       211
	MOVWF       R13, 0
L_EscapeMotorZ235:
	DECFSZ      R13, 1, 1
	BRA         L_EscapeMotorZ235
	DECFSZ      R12, 1, 1
	BRA         L_EscapeMotorZ235
	NOP
;PIC18F45K22.c,902 :: 		for (x=0;x<Steps;x++)
	INFSNZ      R1, 1 
	INCF        R2, 1 
;PIC18F45K22.c,908 :: 		}
	GOTO        L_EscapeMotorZ231
L_EscapeMotorZ232:
;PIC18F45K22.c,909 :: 		}
L_end_EscapeMotorZ:
	RETURN      0
; end of _EscapeMotorZ

_ST28:

;PIC18F45K22.c,912 :: 		void  ST28( unsigned int Direction,unsigned int Steps) //ST 28 unipolar motor
;PIC18F45K22.c,915 :: 		int stepsTune =0;
	CLRF        ST28_stepsTune_L0+0 
	CLRF        ST28_stepsTune_L0+1 
;PIC18F45K22.c,917 :: 		for (x=0;x<Steps;x++)
	CLRF        R1 
	CLRF        R2 
L_ST28236:
	MOVF        FARG_ST28_Steps+1, 0 
	SUBWF       R2, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ST28439
	MOVF        FARG_ST28_Steps+0, 0 
	SUBWF       R1, 0 
L__ST28439:
	BTFSC       STATUS+0, 0 
	GOTO        L_ST28237
;PIC18F45K22.c,919 :: 		if (Direction ==1 ) stepsTune = stepsTune + 1;
	MOVLW       0
	XORWF       FARG_ST28_Direction+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ST28440
	MOVLW       1
	XORWF       FARG_ST28_Direction+0, 0 
L__ST28440:
	BTFSS       STATUS+0, 2 
	GOTO        L_ST28239
	INFSNZ      ST28_stepsTune_L0+0, 1 
	INCF        ST28_stepsTune_L0+1, 1 
	GOTO        L_ST28240
L_ST28239:
;PIC18F45K22.c,920 :: 		else stepsTune =  stepsTune - 1;
	MOVLW       1
	SUBWF       ST28_stepsTune_L0+0, 1 
	MOVLW       0
	SUBWFB      ST28_stepsTune_L0+1, 1 
L_ST28240:
;PIC18F45K22.c,921 :: 		if (stepsTune>7)  stepsTune = 0;
	MOVLW       128
	MOVWF       R0 
	MOVLW       128
	XORWF       ST28_stepsTune_L0+1, 0 
	SUBWF       R0, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ST28441
	MOVF        ST28_stepsTune_L0+0, 0 
	SUBLW       7
L__ST28441:
	BTFSC       STATUS+0, 0 
	GOTO        L_ST28241
	CLRF        ST28_stepsTune_L0+0 
	CLRF        ST28_stepsTune_L0+1 
L_ST28241:
;PIC18F45K22.c,922 :: 		if (stepsTune<0)  stepsTune = 7;
	MOVLW       128
	XORWF       ST28_stepsTune_L0+1, 0 
	MOVWF       R0 
	MOVLW       128
	SUBWF       R0, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ST28442
	MOVLW       0
	SUBWF       ST28_stepsTune_L0+0, 0 
L__ST28442:
	BTFSC       STATUS+0, 0 
	GOTO        L_ST28242
	MOVLW       7
	MOVWF       ST28_stepsTune_L0+0 
	MOVLW       0
	MOVWF       ST28_stepsTune_L0+1 
L_ST28242:
;PIC18F45K22.c,923 :: 		switch(stepsTune)
	GOTO        L_ST28243
;PIC18F45K22.c,925 :: 		case 0:S1F1 = 1;   S1F2 = 0; S1F3 = 0;  S1F4=  0;   break;
L_ST28245:
	BSF         LATD+0, 7 
	BCF         LATD+0, 6 
	BCF         LATD+0, 5 
	BCF         LATD+0, 4 
	GOTO        L_ST28244
;PIC18F45K22.c,926 :: 		case 1: S1F1 = 1;   S1F2 = 1; S1F3 = 0;  S1F4=  0;   break;
L_ST28246:
	BSF         LATD+0, 7 
	BSF         LATD+0, 6 
	BCF         LATD+0, 5 
	BCF         LATD+0, 4 
	GOTO        L_ST28244
;PIC18F45K22.c,927 :: 		case 2: S1F1 = 0;   S1F2 = 1; S1F3 = 0;  S1F4=  0;   break;
L_ST28247:
	BCF         LATD+0, 7 
	BSF         LATD+0, 6 
	BCF         LATD+0, 5 
	BCF         LATD+0, 4 
	GOTO        L_ST28244
;PIC18F45K22.c,928 :: 		case 3: S1F1 = 0;   S1F2 = 1; S1F3 = 1;  S1F4=  0;   break;
L_ST28248:
	BCF         LATD+0, 7 
	BSF         LATD+0, 6 
	BSF         LATD+0, 5 
	BCF         LATD+0, 4 
	GOTO        L_ST28244
;PIC18F45K22.c,929 :: 		case 4: S1F1 = 0;   S1F2 = 0; S1F3 = 1;  S1F4=  0;   break;
L_ST28249:
	BCF         LATD+0, 7 
	BCF         LATD+0, 6 
	BSF         LATD+0, 5 
	BCF         LATD+0, 4 
	GOTO        L_ST28244
;PIC18F45K22.c,930 :: 		case 5: S1F1 = 0;   S1F2 = 0; S1F3 = 1;  S1F4=  1;   break;
L_ST28250:
	BCF         LATD+0, 7 
	BCF         LATD+0, 6 
	BSF         LATD+0, 5 
	BSF         LATD+0, 4 
	GOTO        L_ST28244
;PIC18F45K22.c,931 :: 		case 6: S1F1 = 0;   S1F2 = 0; S1F3 = 0;  S1F4=  1;   break;
L_ST28251:
	BCF         LATD+0, 7 
	BCF         LATD+0, 6 
	BCF         LATD+0, 5 
	BSF         LATD+0, 4 
	GOTO        L_ST28244
;PIC18F45K22.c,932 :: 		case 7: S1F1 = 1;   S1F2 = 0; S1F3 = 0;  S1F4=  1;   break;
L_ST28252:
	BSF         LATD+0, 7 
	BCF         LATD+0, 6 
	BCF         LATD+0, 5 
	BSF         LATD+0, 4 
	GOTO        L_ST28244
;PIC18F45K22.c,933 :: 		}
L_ST28243:
	MOVLW       0
	XORWF       ST28_stepsTune_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ST28443
	MOVLW       0
	XORWF       ST28_stepsTune_L0+0, 0 
L__ST28443:
	BTFSC       STATUS+0, 2 
	GOTO        L_ST28245
	MOVLW       0
	XORWF       ST28_stepsTune_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ST28444
	MOVLW       1
	XORWF       ST28_stepsTune_L0+0, 0 
L__ST28444:
	BTFSC       STATUS+0, 2 
	GOTO        L_ST28246
	MOVLW       0
	XORWF       ST28_stepsTune_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ST28445
	MOVLW       2
	XORWF       ST28_stepsTune_L0+0, 0 
L__ST28445:
	BTFSC       STATUS+0, 2 
	GOTO        L_ST28247
	MOVLW       0
	XORWF       ST28_stepsTune_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ST28446
	MOVLW       3
	XORWF       ST28_stepsTune_L0+0, 0 
L__ST28446:
	BTFSC       STATUS+0, 2 
	GOTO        L_ST28248
	MOVLW       0
	XORWF       ST28_stepsTune_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ST28447
	MOVLW       4
	XORWF       ST28_stepsTune_L0+0, 0 
L__ST28447:
	BTFSC       STATUS+0, 2 
	GOTO        L_ST28249
	MOVLW       0
	XORWF       ST28_stepsTune_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ST28448
	MOVLW       5
	XORWF       ST28_stepsTune_L0+0, 0 
L__ST28448:
	BTFSC       STATUS+0, 2 
	GOTO        L_ST28250
	MOVLW       0
	XORWF       ST28_stepsTune_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ST28449
	MOVLW       6
	XORWF       ST28_stepsTune_L0+0, 0 
L__ST28449:
	BTFSC       STATUS+0, 2 
	GOTO        L_ST28251
	MOVLW       0
	XORWF       ST28_stepsTune_L0+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ST28450
	MOVLW       7
	XORWF       ST28_stepsTune_L0+0, 0 
L__ST28450:
	BTFSC       STATUS+0, 2 
	GOTO        L_ST28252
L_ST28244:
;PIC18F45K22.c,934 :: 		Delay_ms(2);  //  500 halfsteps/sec     = ok !
	MOVLW       39
	MOVWF       R12, 0
	MOVLW       75
	MOVWF       R13, 0
L_ST28253:
	DECFSZ      R13, 1, 1
	BRA         L_ST28253
	DECFSZ      R12, 1, 1
	BRA         L_ST28253
	NOP
;PIC18F45K22.c,917 :: 		for (x=0;x<Steps;x++)
	INFSNZ      R1, 1 
	INCF        R2, 1 
;PIC18F45K22.c,936 :: 		}        // for  Steps
	GOTO        L_ST28236
L_ST28237:
;PIC18F45K22.c,938 :: 		S1F1 = 0;   S1F2 = 0; S1F3 = 0;  S1F4=  0;
	BCF         LATD+0, 7 
	BCF         LATD+0, 6 
	BCF         LATD+0, 5 
	BCF         LATD+0, 4 
;PIC18F45K22.c,939 :: 		}
L_end_ST28:
	RETURN      0
; end of _ST28

_ZSpeed:

;PIC18F45K22.c,941 :: 		void ZSpeed(unsigned int ZStepsDone,unsigned int ZStepsToDo)
;PIC18F45K22.c,947 :: 		if (ZStepsDone<10)    Delay_us(400);
	MOVLW       0
	SUBWF       FARG_ZSpeed_ZStepsDone+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ZSpeed452
	MOVLW       10
	SUBWF       FARG_ZSpeed_ZStepsDone+0, 0 
L__ZSpeed452:
	BTFSC       STATUS+0, 0 
	GOTO        L_ZSpeed254
	MOVLW       8
	MOVWF       R12, 0
	MOVLW       167
	MOVWF       R13, 0
L_ZSpeed255:
	DECFSZ      R13, 1, 1
	BRA         L_ZSpeed255
	DECFSZ      R12, 1, 1
	BRA         L_ZSpeed255
	NOP
	NOP
L_ZSpeed254:
;PIC18F45K22.c,948 :: 		if (ZStepsDone<20)    Delay_us(400);
	MOVLW       0
	SUBWF       FARG_ZSpeed_ZStepsDone+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ZSpeed453
	MOVLW       20
	SUBWF       FARG_ZSpeed_ZStepsDone+0, 0 
L__ZSpeed453:
	BTFSC       STATUS+0, 0 
	GOTO        L_ZSpeed256
	MOVLW       8
	MOVWF       R12, 0
	MOVLW       167
	MOVWF       R13, 0
L_ZSpeed257:
	DECFSZ      R13, 1, 1
	BRA         L_ZSpeed257
	DECFSZ      R12, 1, 1
	BRA         L_ZSpeed257
	NOP
	NOP
L_ZSpeed256:
;PIC18F45K22.c,949 :: 		if (ZStepsDone<40)    Delay_us(350);
	MOVLW       0
	SUBWF       FARG_ZSpeed_ZStepsDone+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ZSpeed454
	MOVLW       40
	SUBWF       FARG_ZSpeed_ZStepsDone+0, 0 
L__ZSpeed454:
	BTFSC       STATUS+0, 0 
	GOTO        L_ZSpeed258
	MOVLW       7
	MOVWF       R12, 0
	MOVLW       178
	MOVWF       R13, 0
L_ZSpeed259:
	DECFSZ      R13, 1, 1
	BRA         L_ZSpeed259
	DECFSZ      R12, 1, 1
	BRA         L_ZSpeed259
	NOP
	NOP
L_ZSpeed258:
;PIC18F45K22.c,950 :: 		if (ZStepsDone<80)    Delay_us(300);
	MOVLW       0
	SUBWF       FARG_ZSpeed_ZStepsDone+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ZSpeed455
	MOVLW       80
	SUBWF       FARG_ZSpeed_ZStepsDone+0, 0 
L__ZSpeed455:
	BTFSC       STATUS+0, 0 
	GOTO        L_ZSpeed260
	MOVLW       6
	MOVWF       R12, 0
	MOVLW       189
	MOVWF       R13, 0
L_ZSpeed261:
	DECFSZ      R13, 1, 1
	BRA         L_ZSpeed261
	DECFSZ      R12, 1, 1
	BRA         L_ZSpeed261
	NOP
	NOP
L_ZSpeed260:
;PIC18F45K22.c,951 :: 		if (ZStepsDone<160)    Delay_us(150);
	MOVLW       0
	SUBWF       FARG_ZSpeed_ZStepsDone+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ZSpeed456
	MOVLW       160
	SUBWF       FARG_ZSpeed_ZStepsDone+0, 0 
L__ZSpeed456:
	BTFSC       STATUS+0, 0 
	GOTO        L_ZSpeed262
	MOVLW       3
	MOVWF       R12, 0
	MOVLW       222
	MOVWF       R13, 0
L_ZSpeed263:
	DECFSZ      R13, 1, 1
	BRA         L_ZSpeed263
	DECFSZ      R12, 1, 1
	BRA         L_ZSpeed263
	NOP
L_ZSpeed262:
;PIC18F45K22.c,954 :: 		if (ZStepsDone<10)    Delay_us(300);
	MOVLW       0
	SUBWF       FARG_ZSpeed_ZStepsDone+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ZSpeed457
	MOVLW       10
	SUBWF       FARG_ZSpeed_ZStepsDone+0, 0 
L__ZSpeed457:
	BTFSC       STATUS+0, 0 
	GOTO        L_ZSpeed264
	MOVLW       6
	MOVWF       R12, 0
	MOVLW       189
	MOVWF       R13, 0
L_ZSpeed265:
	DECFSZ      R13, 1, 1
	BRA         L_ZSpeed265
	DECFSZ      R12, 1, 1
	BRA         L_ZSpeed265
	NOP
	NOP
L_ZSpeed264:
;PIC18F45K22.c,955 :: 		if (ZStepsToDo<20)    Delay_us(300);
	MOVLW       0
	SUBWF       FARG_ZSpeed_ZStepsToDo+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ZSpeed458
	MOVLW       20
	SUBWF       FARG_ZSpeed_ZStepsToDo+0, 0 
L__ZSpeed458:
	BTFSC       STATUS+0, 0 
	GOTO        L_ZSpeed266
	MOVLW       6
	MOVWF       R12, 0
	MOVLW       189
	MOVWF       R13, 0
L_ZSpeed267:
	DECFSZ      R13, 1, 1
	BRA         L_ZSpeed267
	DECFSZ      R12, 1, 1
	BRA         L_ZSpeed267
	NOP
	NOP
L_ZSpeed266:
;PIC18F45K22.c,956 :: 		if (ZStepsToDo<40)    Delay_us(250);
	MOVLW       0
	SUBWF       FARG_ZSpeed_ZStepsToDo+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ZSpeed459
	MOVLW       40
	SUBWF       FARG_ZSpeed_ZStepsToDo+0, 0 
L__ZSpeed459:
	BTFSC       STATUS+0, 0 
	GOTO        L_ZSpeed268
	MOVLW       5
	MOVWF       R12, 0
	MOVLW       200
	MOVWF       R13, 0
L_ZSpeed269:
	DECFSZ      R13, 1, 1
	BRA         L_ZSpeed269
	DECFSZ      R12, 1, 1
	BRA         L_ZSpeed269
	NOP
L_ZSpeed268:
;PIC18F45K22.c,957 :: 		if (ZStepsToDo<80)    Delay_us(200);
	MOVLW       0
	SUBWF       FARG_ZSpeed_ZStepsToDo+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ZSpeed460
	MOVLW       80
	SUBWF       FARG_ZSpeed_ZStepsToDo+0, 0 
L__ZSpeed460:
	BTFSC       STATUS+0, 0 
	GOTO        L_ZSpeed270
	MOVLW       4
	MOVWF       R12, 0
	MOVLW       211
	MOVWF       R13, 0
L_ZSpeed271:
	DECFSZ      R13, 1, 1
	BRA         L_ZSpeed271
	DECFSZ      R12, 1, 1
	BRA         L_ZSpeed271
	NOP
L_ZSpeed270:
;PIC18F45K22.c,958 :: 		if (ZStepsToDo<160)    Delay_us(100);
	MOVLW       0
	SUBWF       FARG_ZSpeed_ZStepsToDo+1, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ZSpeed461
	MOVLW       160
	SUBWF       FARG_ZSpeed_ZStepsToDo+0, 0 
L__ZSpeed461:
	BTFSC       STATUS+0, 0 
	GOTO        L_ZSpeed272
	MOVLW       2
	MOVWF       R12, 0
	MOVLW       233
	MOVWF       R13, 0
L_ZSpeed273:
	DECFSZ      R13, 1, 1
	BRA         L_ZSpeed273
	DECFSZ      R12, 1, 1
	BRA         L_ZSpeed273
	NOP
L_ZSpeed272:
;PIC18F45K22.c,960 :: 		for (x=0;x<ZMotorDelay;x++) Delay_us(1);              // core
	CLRF        R1 
	CLRF        R2 
L_ZSpeed274:
	MOVLW       128
	XORWF       R2, 0 
	MOVWF       R0 
	MOVLW       128
	XORWF       _ZMotorDelay+1, 0 
	SUBWF       R0, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__ZSpeed462
	MOVF        _ZMotorDelay+0, 0 
	SUBWF       R1, 0 
L__ZSpeed462:
	BTFSC       STATUS+0, 0 
	GOTO        L_ZSpeed275
	MOVLW       4
	MOVWF       R13, 0
L_ZSpeed277:
	DECFSZ      R13, 1, 1
	BRA         L_ZSpeed277
	NOP
	NOP
	INFSNZ      R1, 1 
	INCF        R2, 1 
	GOTO        L_ZSpeed274
L_ZSpeed275:
;PIC18F45K22.c,961 :: 		}
L_end_ZSpeed:
	RETURN      0
; end of _ZSpeed

_Initialisation:

;PIC18F45K22.c,964 :: 		void Initialisation(void)
;PIC18F45K22.c,966 :: 		int x=0;
	CLRF        Initialisation_x_L0+0 
	CLRF        Initialisation_x_L0+1 
;PIC18F45K22.c,975 :: 		if (JumperZ==1)   // jumper Z is not placed, Z motor is in use
	BTFSS       PORTA+0, 1 
	GOTO        L_Initialisation278
;PIC18F45K22.c,977 :: 		DirZ=1;            //1 = upwards  0  downwards
	BSF         LATE+0, 0 
;PIC18F45K22.c,979 :: 		while(1)
L_Initialisation279:
;PIC18F45K22.c,981 :: 		Delay_us(100);  // slow
	MOVLW       2
	MOVWF       R12, 0
	MOVLW       233
	MOVWF       R13, 0
L_Initialisation281:
	DECFSZ      R13, 1, 1
	BRA         L_Initialisation281
	DECFSZ      R12, 1, 1
	BRA         L_Initialisation281
	NOP
;PIC18F45K22.c,982 :: 		StepZ=0;        //
	BCF         LATA+0, 5 
;PIC18F45K22.c,983 :: 		Delay_us(100);       //  slow
	MOVLW       2
	MOVWF       R12, 0
	MOVLW       233
	MOVWF       R13, 0
L_Initialisation282:
	DECFSZ      R13, 1, 1
	BRA         L_Initialisation282
	DECFSZ      R12, 1, 1
	BRA         L_Initialisation282
	NOP
;PIC18F45K22.c,984 :: 		StepZ=1;
	BSF         LATA+0, 5 
;PIC18F45K22.c,985 :: 		if (ZCORNERSWITCH==0) break;
	BTFSC       PORTB+0, 0 
	GOTO        L_Initialisation283
	GOTO        L_Initialisation280
L_Initialisation283:
;PIC18F45K22.c,986 :: 		}
	GOTO        L_Initialisation279
L_Initialisation280:
;PIC18F45K22.c,988 :: 		DirZ=0;     // downwards
	BCF         LATE+0, 0 
;PIC18F45K22.c,989 :: 		for(x=0;x<EscapeStepsZ;x++)   //2000=2.5 mm trap screw 8mm
	CLRF        Initialisation_x_L0+0 
	CLRF        Initialisation_x_L0+1 
L_Initialisation284:
	MOVLW       128
	XORWF       Initialisation_x_L0+1, 0 
	MOVWF       R0 
	MOVLW       128
	XORLW       7
	SUBWF       R0, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__Initialisation464
	MOVLW       208
	SUBWF       Initialisation_x_L0+0, 0 
L__Initialisation464:
	BTFSC       STATUS+0, 0 
	GOTO        L_Initialisation285
;PIC18F45K22.c,991 :: 		Delay_us(100);   //  slow  maxmost  2000 steps/sec - 2000 /16 = 125  steps /sec->250 full steps /sec
	MOVLW       2
	MOVWF       R12, 0
	MOVLW       233
	MOVWF       R13, 0
L_Initialisation287:
	DECFSZ      R13, 1, 1
	BRA         L_Initialisation287
	DECFSZ      R12, 1, 1
	BRA         L_Initialisation287
	NOP
;PIC18F45K22.c,992 :: 		StepZ=0;         //
	BCF         LATA+0, 5 
;PIC18F45K22.c,993 :: 		Delay_us(100);   // slow maxmost 2000 steps/sec
	MOVLW       2
	MOVWF       R12, 0
	MOVLW       233
	MOVWF       R13, 0
L_Initialisation288:
	DECFSZ      R13, 1, 1
	BRA         L_Initialisation288
	DECFSZ      R12, 1, 1
	BRA         L_Initialisation288
	NOP
;PIC18F45K22.c,994 :: 		StepZ=1;
	BSF         LATA+0, 5 
;PIC18F45K22.c,989 :: 		for(x=0;x<EscapeStepsZ;x++)   //2000=2.5 mm trap screw 8mm
	INFSNZ      Initialisation_x_L0+0, 1 
	INCF        Initialisation_x_L0+1, 1 
;PIC18F45K22.c,995 :: 		}
	GOTO        L_Initialisation284
L_Initialisation285:
;PIC18F45K22.c,999 :: 		DirYR = 1;  // topwards
	BSF         LATC+0, 0 
;PIC18F45K22.c,1000 :: 		DirYL=DirYR;
	BTFSC       LATC+0, 0 
	GOTO        L__Initialisation465
	BCF         LATC+0, 1 
	GOTO        L__Initialisation466
L__Initialisation465:
	BSF         LATC+0, 1 
L__Initialisation466:
;PIC18F45K22.c,1001 :: 		while(1)
L_Initialisation289:
;PIC18F45K22.c,1003 :: 		Delay_us(100);  //  slow to synchronize Y bars !
	MOVLW       2
	MOVWF       R12, 0
	MOVLW       233
	MOVWF       R13, 0
L_Initialisation291:
	DECFSZ      R13, 1, 1
	BRA         L_Initialisation291
	DECFSZ      R12, 1, 1
	BRA         L_Initialisation291
	NOP
;PIC18F45K22.c,1004 :: 		StepYR=0;
	BCF         LATC+0, 3 
;PIC18F45K22.c,1005 :: 		StepYL=0;
	BCF         LATC+0, 2 
;PIC18F45K22.c,1007 :: 		Delay_us(100);  //  slow to synchronize Y bars !
	MOVLW       2
	MOVWF       R12, 0
	MOVLW       233
	MOVWF       R13, 0
L_Initialisation292:
	DECFSZ      R13, 1, 1
	BRA         L_Initialisation292
	DECFSZ      R12, 1, 1
	BRA         L_Initialisation292
	NOP
;PIC18F45K22.c,1009 :: 		if (YRCORNERSWITCH==1) StepYR=1;
	BTFSS       PORTB+0, 2 
	GOTO        L_Initialisation293
	BSF         LATC+0, 3 
L_Initialisation293:
;PIC18F45K22.c,1010 :: 		if (YLCORNERSWITCH==1) StepYL=1; // !!! was YR
	BTFSS       PORTB+0, 1 
	GOTO        L_Initialisation294
	BSF         LATC+0, 2 
L_Initialisation294:
;PIC18F45K22.c,1011 :: 		if ((YRCORNERSWITCH==0) && (YLCORNERSWITCH==0)) break;
	BTFSC       PORTB+0, 2 
	GOTO        L_Initialisation297
	BTFSC       PORTB+0, 1 
	GOTO        L_Initialisation297
L__Initialisation321:
	GOTO        L_Initialisation290
L_Initialisation297:
;PIC18F45K22.c,1012 :: 		}
	GOTO        L_Initialisation289
L_Initialisation290:
;PIC18F45K22.c,1014 :: 		DirYR = 0;      // frontwards    = to me
	BCF         LATC+0, 0 
;PIC18F45K22.c,1015 :: 		DirYL=DirYR;     // frontwards   = to me
	BTFSC       LATC+0, 0 
	GOTO        L__Initialisation467
	BCF         LATC+0, 1 
	GOTO        L__Initialisation468
L__Initialisation467:
	BSF         LATC+0, 1 
L__Initialisation468:
;PIC18F45K22.c,1019 :: 		for(x=0;x<(EscapeStepsXY);x++)  //
	CLRF        Initialisation_x_L0+0 
	CLRF        Initialisation_x_L0+1 
L_Initialisation298:
	MOVLW       128
	XORWF       Initialisation_x_L0+1, 0 
	MOVWF       R0 
	MOVLW       128
	XORLW       2
	SUBWF       R0, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__Initialisation469
	MOVLW       88
	SUBWF       Initialisation_x_L0+0, 0 
L__Initialisation469:
	BTFSC       STATUS+0, 0 
	GOTO        L_Initialisation299
;PIC18F45K22.c,1021 :: 		Delay_us(50);  //  slow  maxmost   5000 microsteps/sec
	MOVLW       245
	MOVWF       R13, 0
L_Initialisation301:
	DECFSZ      R13, 1, 1
	BRA         L_Initialisation301
	NOP
;PIC18F45K22.c,1022 :: 		StepYR=0;        //
	BCF         LATC+0, 3 
;PIC18F45K22.c,1023 :: 		StepYL=0,
	BCF         LATC+0, 2 
;PIC18F45K22.c,1024 :: 		Delay_us(50);       // slow
	MOVLW       245
	MOVWF       R13, 0
L_Initialisation302:
	DECFSZ      R13, 1, 1
	BRA         L_Initialisation302
	NOP
;PIC18F45K22.c,1025 :: 		StepYR=1;
	BSF         LATC+0, 3 
;PIC18F45K22.c,1026 :: 		StepYL=1;
	BSF         LATC+0, 2 
;PIC18F45K22.c,1019 :: 		for(x=0;x<(EscapeStepsXY);x++)  //
	INFSNZ      Initialisation_x_L0+0, 1 
	INCF        Initialisation_x_L0+1, 1 
;PIC18F45K22.c,1027 :: 		}
	GOTO        L_Initialisation298
L_Initialisation299:
;PIC18F45K22.c,1030 :: 		DirX=1;            // to left
	BSF         LATD+0, 0 
;PIC18F45K22.c,1031 :: 		while(1)
L_Initialisation303:
;PIC18F45K22.c,1033 :: 		Delay_us(100);        // do it slowly
	MOVLW       2
	MOVWF       R12, 0
	MOVLW       233
	MOVWF       R13, 0
L_Initialisation305:
	DECFSZ      R13, 1, 1
	BRA         L_Initialisation305
	DECFSZ      R12, 1, 1
	BRA         L_Initialisation305
	NOP
;PIC18F45K22.c,1034 :: 		StepX=0;
	BCF         LATD+0, 1 
;PIC18F45K22.c,1035 :: 		Delay_us(100);       //  do it slowly
	MOVLW       2
	MOVWF       R12, 0
	MOVLW       233
	MOVWF       R13, 0
L_Initialisation306:
	DECFSZ      R13, 1, 1
	BRA         L_Initialisation306
	DECFSZ      R12, 1, 1
	BRA         L_Initialisation306
	NOP
;PIC18F45K22.c,1037 :: 		if (XLCORNERSWITCH==0) break;
	BTFSC       PORTB+0, 4 
	GOTO        L_Initialisation307
	GOTO        L_Initialisation304
L_Initialisation307:
;PIC18F45K22.c,1038 :: 		StepX=1;
	BSF         LATD+0, 1 
;PIC18F45K22.c,1039 :: 		}
	GOTO        L_Initialisation303
L_Initialisation304:
;PIC18F45K22.c,1043 :: 		DirX=0;     // to right
	BCF         LATD+0, 0 
;PIC18F45K22.c,1045 :: 		for(x=0;x<(EscapeStepsXY);x++)   // WAS 200
	CLRF        Initialisation_x_L0+0 
	CLRF        Initialisation_x_L0+1 
L_Initialisation308:
	MOVLW       128
	XORWF       Initialisation_x_L0+1, 0 
	MOVWF       R0 
	MOVLW       128
	XORLW       2
	SUBWF       R0, 0 
	BTFSS       STATUS+0, 2 
	GOTO        L__Initialisation470
	MOVLW       88
	SUBWF       Initialisation_x_L0+0, 0 
L__Initialisation470:
	BTFSC       STATUS+0, 0 
	GOTO        L_Initialisation309
;PIC18F45K22.c,1047 :: 		Delay_us(50);   // slow
	MOVLW       245
	MOVWF       R13, 0
L_Initialisation311:
	DECFSZ      R13, 1, 1
	BRA         L_Initialisation311
	NOP
;PIC18F45K22.c,1048 :: 		StepX=0;         //
	BCF         LATD+0, 1 
;PIC18F45K22.c,1049 :: 		Delay_us(50);   //  slow
	MOVLW       245
	MOVWF       R13, 0
L_Initialisation312:
	DECFSZ      R13, 1, 1
	BRA         L_Initialisation312
	NOP
;PIC18F45K22.c,1050 :: 		StepX=1;
	BSF         LATD+0, 1 
;PIC18F45K22.c,1045 :: 		for(x=0;x<(EscapeStepsXY);x++)   // WAS 200
	INFSNZ      Initialisation_x_L0+0, 1 
	INCF        Initialisation_x_L0+1, 1 
;PIC18F45K22.c,1051 :: 		}
	GOTO        L_Initialisation308
L_Initialisation309:
;PIC18F45K22.c,1052 :: 		}
L_Initialisation278:
;PIC18F45K22.c,1053 :: 		return;
;PIC18F45K22.c,1054 :: 		}
L_end_Initialisation:
	RETURN      0
; end of _Initialisation
