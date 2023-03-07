// 30 07 2020 2 LM6UU on each side:
// Option to close the cutter holder with 4 M3 bolds and nuts.abs
// Fixing with ties is even so good.
// The cutter holder can also serve as pen holder but not inverse.

   DiaM3=3.6; // fix  with M3 
   DiaCutter=11.9;  //11.8
   Height=74;   


   color("lime")          
         
 rotate([180,0,0])  //180 no need to rotate in Slic3R        
   {        
difference()
{ 
  union()
  {                    
      // Central Pen - Cutter holder      
     translate([0,6,-25.0])  // -1.5  
       cylinder(h=Height ,d=DiaCutter+10+5 ,center=true,$fn=48);  //
      
     // makes closing with M3 possible
     translate([0,6,-25.0])  // -1.5  
       cube([42,8 ,Height ] ,center=true);  
  }    
   // END UNION
  
//  Central cutter holder:

translate([0,6,-25])
       cylinder(h=Height*3 ,d=DiaCutter,center=true,$fn=48);  
     
  
// Thickest part of cutter holder:
  
translate([0,6,-22.5-25])
       cylinder(h=4.0 ,d=16.75+0.25,center=true,$fn=48);  //17 is ok for cutter
    
 // Funnel makes it printable without support
  
translate([0,6,-25.4-25])
       cylinder(h=2 ,d2=16.75+0.25,d1=DiaCutter,center=true,$fn=24); 
        
  // ties or M3 pair down:  
  
  translate([17,6,-56])     //-31
  rotate([90,0,0]) 
  cylinder(h=20 ,d=DiaM3,center=true,$fn=24);  // 
      
  translate([-17,6,-56])     //
  rotate([90,0,0])  
  cylinder(h=20 ,d=DiaM3,center=true,$fn=24);  // 
     
  
//  ties or M3 higher
    
  translate([17,6,-39])     //-14
  rotate([90,0,0])   
  cylinder(h=20 ,d=DiaM3,center=true,$fn=24);  // 
   
  translate([-17,6,-39])     //
  rotate([90,0,0]) 
  cylinder(h=20 ,d=DiaM3,center=true,$fn=24);  // 
      
  
  // top cutter holder removal :
 
 translate([0,15,0])
  cube([58,20,60],center=true);
  
  // removal 
     translate([0,-1,-25])
  cube([43,14,80],center=true);  
    
    }
 }  // end rotate
         
         