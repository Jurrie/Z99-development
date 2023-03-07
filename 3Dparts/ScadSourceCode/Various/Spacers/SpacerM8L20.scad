// Optional spacers in case M8 bolds are long
// speedup closing winged nuts
// 4 * 2 = 8 pieces


M8=9.4;        // for easy insertion on M8 bold
  // when printed with nozzle 0.8 (and HIPS) 9.4 is necessary
   // nozzle  0.4 = 8.8= ok
Height=18;


difference()
{
cylinder(h=Height,d=M8+10,center=true,$fn=96);
 cylinder(h=Height+1,d=M8,center=true,$fn=96) ;  
    
}