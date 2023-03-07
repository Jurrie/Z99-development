//  Fix on large fan 120 mm square with M4
// print 4 pieces

DiaFan=125;
DiaM4=4.5; // somewhat larger

  difference()
         {
             
  union() {         
      
 
      cube([50,10,3],center = true);  //8
      
      translate([22,0,3])
       cube([6,10,6],center = true);  //8
      
        translate([-22,0,3])
       cube([6,10,6],center = true);  //
      
      
  }         // END UNION
  
  cylinder(h=4,d=DiaM4,center=true,$fn=24); // M4 bolds 
   
   
  }