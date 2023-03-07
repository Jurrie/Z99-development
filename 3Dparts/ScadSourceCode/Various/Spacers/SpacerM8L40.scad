// Optionale spacers in case M8 bolds are long
// speedup closing winged nuts
// 4 * 2 = 8 pieces

M8=9.0;        // ! for easy insertion on M8 bold
  

Height=40;


difference()
{
cylinder(h=Height,d=M8+10,center=true,$fn=96);
 cylinder(h=Height+1,d=M8,center=true,$fn=96) ;  
    
}