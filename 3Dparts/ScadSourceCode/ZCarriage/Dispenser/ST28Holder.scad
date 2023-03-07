// Dispenser driver - holder ST28 motor

    DiaFix=1.9;
   DiaTwo=2.4; // fixing  wire with hook
   DiaTye=3.8;
   DiaFour= 4.5;  // 
   DiaFive=5.5;   //
   DiaM5 = 5.6; 
   DiaM6 = 6.7; // glider, ++
                
    Xc=4.5;   //  pen midpoint position
    
    VersionWithTye = 1;
    
color("lime")    

rotate([180,0,0])
    
difference()
{ 
  union()
  {     
   
     
      // side gliders :
      
          translate([39,-8.5,7.5])
       cylinder(h=8 ,d=DiaM6+8 ,center=true,$fn=48);  //
            
          translate([-39,-8.5,7.5])
       cylinder(h=8 ,d=DiaM6+8 ,center=true,$fn=48);  //
            
      // connect side gliders:
                       
           translate([0,-8.0,7.5])
      cube([86,10,8],center=true); //46
     
  
  // connect dorsal:
 
   // FIX ROOM ROTATE STEPPER   
      
   //  stepper   
  
     translate([0,-0.75,0]) // Y -27
      cylinder(h=23.0,d=35.65,center=true,$fn=48);  //  25
        
     
   //  plunger fix   room  
  
     
    // ST fix side cylinders
      // KEEP THEM LONG !
       translate([17.5,-0.5,0]) //
      cylinder(h=23,d=9,center=true,$fn=48);  //  was 33.4
   
 
           translate([-17.5,-0.5,0]) //
      cylinder(h=23,d=9,center=true,$fn=48);  //  was 33.4   
            
  }      

  // end union
    
    // fix rotator motor :
 
       translate([17.75,-0.25,0]) //10.25
      cylinder(h=30,d=4,center=true,$fn=48);  // 4 for :3
   
 
           translate([-17.75,-0.25,0]) 
      cylinder(h=30,d=4,center=true,$fn=48);  //  4 for M3


     // check center pulley:
  /*
           translate([0,-8.5,0]) 
      cylinder(h=30,d=3,center=true,$fn=48);  //  4 for M3
*/

   // side gliders :
      
          translate([39,-8.5,0])
       cylinder(h=30 ,d=DiaM6 ,center=true,$fn=48);  //
            
          translate([-39,-8.5,0])
       cylinder(h=30 ,d=DiaM6 ,center=true,$fn=48);  //
   
      
       //  NEW  
       
       // ROTATOR ST room:     
     
     translate([0,-0.75,2.5])  // 1.5 = 2.5 mm bottom 
       cylinder(h=23,d=28.65,center=true,$fn=96);   // 20
        
    ///  entry ST
      
   translate([0,36,-4])  //  = 2 mm bottom -39+13.5 = 
       cylinder(h=34,d=66,center=true,$fn=48);   //  
   
  }
   
   


         
         