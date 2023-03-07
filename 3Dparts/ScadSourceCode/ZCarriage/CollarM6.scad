
// 2 (4) pieces
// could be used as stop collar on M6 bars in stead of iron or alu one.


DiaFix=1.9; // self tapping screw M2

M6=6.5;        // for easy insertion on M6 bold  
Height=7.6;


difference()
{
cylinder(h=Height,d=M6+10,center=true,$fn=96);// 9+4.8=13.8
 cylinder(h=Height+1,d=M6,center=true,$fn=96) ;  
    
    rotate([0,90,0])
    cylinder(d=DiaFix,h=20,$fn=12,center=true);
     rotate([90,0,0])
    cylinder(d=DiaFix,h=20,$fn=12,center=true);
    
}