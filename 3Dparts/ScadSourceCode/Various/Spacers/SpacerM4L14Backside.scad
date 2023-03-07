// Spacers between Z backpart and winged nut on Z carriage
// 6 pieces


M4=4.8;        // for easy insertion on M4 bold  
Height=14;


difference()
{
cylinder(h=Height,d=M4+7,center=true,$fn=96);
 cylinder(h=Height+1,d=M4,center=true,$fn=96) ;  
    
}