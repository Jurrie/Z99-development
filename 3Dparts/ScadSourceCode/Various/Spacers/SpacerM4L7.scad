// Spacers between midpart and backpart of Z core
// 6 pieces


M4=4.8;        // for easy insertion on M4 bold  
Height=7;


difference()
{
cylinder(h=Height,d=M4+10,center=true,$fn=96);// 9+4.8=13.8
 cylinder(h=Height+1,d=M4,center=true,$fn=96) ;  
    
}