// Spacers between midpart and backpart of Z core
// Allow some space for ties that hold LM12UU
// 6 pieces


M4=4.8;        // for easy insertion on M4 bold  
Height=2.8;   // measured 3 when ready


difference()
{
cylinder(h=Height,d=M4+7,center=true,$fn=96);// 9+4.8=13.8
 cylinder(h=Height+1,d=M4,center=true,$fn=96) ;  
    
}