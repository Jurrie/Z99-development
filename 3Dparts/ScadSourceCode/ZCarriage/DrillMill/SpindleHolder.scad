// has his own support below trapezoidal nut. 
// Saw it away after printing
// without some elevation of the trapezoidal nut, drill goes not deep enough 
// For spindle 300 watt and diameter 52 mm

DiaFix=1.9; // maybe self tapping screws

DiaM3=3.5;
DiaM4=4.5;
DiaTie=4.0;
DiaAxis=6.7; // pass linear shaft
DiaLM6 = 12.2;   /// FOR LM6 linear ball bearing of 6 mm 

DiaMotor= 52.4; // measured 52  adapt if necessary


HeightLM6UU = 19; // 19 -> 44
color("lime")

rotate([0,90,0])   //  makes no rotation in slic3r necessary


difference() 
{
union()
    {    
             
 translate([-19,0,0])   //..5.5 temp to 0.5
      cube([4.0,70,18]  ,center=true); //..28 temp to 48
          
          
        // MIDDLE PÄRT +   ACME :           
          
         translate([-18,0,20])                  
      cube([6,32,40],center=true);    
                     
    //   reinforcement for acme
           
 translate([-20,14,17.5])   //
      cube([20.0,4,45]  ,center=true); //
         
                           
 translate([-20,-14,17.5])   //
      cube([20.0,4,45]  ,center=true); // 

  // SUPPORT 
  
      translate([-8,14,38])                 
      cube([ HeightLM6UU+7 ,4,4]  ,center=true); // 
   
  translate([-8,-14,38])                 
      cube([ HeightLM6UU+7 ,4,4]  ,center=true); // 
   

   
       //  motor holder :            
      translate([-8-10,0,-7-4-4])  //  -9 
           rotate([0,90,0])
             cylinder(h=26+20,d=DiaMotor+7, center=true,$fn=96);   //h50 dia dremel
                
               
               
      // LM6UU holders:       
           
       translate([-8.0-10,-39,0])  
    rotate([0,90,0])
      cylinder(h=HeightLM6UU+7+20,d=DiaLM6+8, center=true,$fn=40);   
        
          translate([-8-10,39,0]) 
    rotate([0,90,0])
      cylinder(h=HeightLM6UU+7+20 ,d=DiaLM6+8, center=true,$fn=40);  
            
    // central  reinforcement between LM6UU
           
 translate([-8-10,0,1.0])   
      cube([HeightLM6UU+7+20,74,5]  ,center=true);
           
    
    
      // for M4 screw ( L50mm  ) to push Z switch
      // this one is nut used,serves as reinforcement
 
  translate([-8-10,-25.5,4])  //    
   rotate([0,90,0])   
     cylinder(h=26+20,d=DiaM4+8 ,center=true,$fn=48); 
 
   // for M4 screw ( L50mm  ) to push Z switch
 
  translate([-8-10,25.5,4])  //    
   rotate([0,90,0])   
     cylinder(h=26+20,d=DiaM4+8 ,center=true,$fn=48); 
     
     
      //SUPPORT  ACME, CUT AWAY AFTER PRINTING !
    
      translate([-8,0,28])             
     rotate([0,90,0 ])
      cylinder(h=HeightLM6UU+7,d=12.5,center=true,$fn=48); 
     
     
      // SUPPORT 
      translate([-8,5.8,28+5.8])             
     rotate([0,90,0 ])
      cylinder(h=HeightLM6UU+7,d=DiaM3+2,center=true,$fn=24); 
      
       // SUPPORT 
      translate([-8,5.8,28-5.8])             
     rotate([0,90,0 ])
      cylinder(h=HeightLM6UU+7,d=DiaM3+2,center=true,$fn=24); 
        // SUPPORT 
      translate([-8,-5.8,28+5.8])             
     rotate([0,90,0 ])
      cylinder(h=HeightLM6UU+7,d=DiaM3+2,center=true,$fn=24); 
        // SUPPORT 
      translate([-8,-5.8,28-5.8])             
     rotate([0,90,0 ])
      cylinder(h=HeightLM6UU+7,d=DiaM3+2,center=true,$fn=24);      
     
    }    // end upper union        
    
    
        // for M4 screw ( L50mm  ) to push Z switch
 
  translate([-18,-25.5,4])  //    
   rotate([0,90,0])   
     cylinder(h=50,d=DiaM4 ,center=true,$fn=48);
 
   // for M4 screw ( L50mm  ) to push Z switch
 
  translate([-18,25.5,4])  //    
   rotate([0,90,0])   
     cylinder(h=50,d=DiaM4 ,center=true,$fn=48); 
    
    
        // room motor :
            
           translate([-30,0,-11-4])  // -20 = 4 mm bottom
    rotate([0,90,0])
      cylinder(h=62,d=DiaMotor, center=true,$fn=96);   
              
    
        // bottom motor opening :
            
           translate([5,0,-11-4])  
    rotate([0,90,0])
      cylinder(h=10,d=27, center=true,$fn=96);  
         
        
    
  // ACME
    
      translate([-8,0,28])            
     rotate([0,90,0 ])
      cylinder(h=30,d=10.5,center=true,$fn=48); 
    
    // FIX ACME WITH M3
      translate([-8,5.8,28+5.8])             
     rotate([0,90,0 ])
      cylinder(h=30,d=DiaM3,center=true,$fn=24); 
      
        translate([-8,5.8,28-5.8])             
     rotate([0,90,0 ])
      cylinder(h=30,d=DiaM3,center=true,$fn=24); 
                 
      translate([-8,-5.8,28+5.8])             
     rotate([0,90,0 ])
      cylinder(h=30,d=DiaM3,center=true,$fn=24);
                
        translate([-8,-5.8,28-5.8])             
     rotate([0,90,0 ])
      cylinder(h=30,d=DiaM3,center=true,$fn=24); 
       
        
         
   // two cylinders for LM6UU rail holders
   
       translate([-8-10,-39,0])  
    rotate([0,90,0])
      cylinder(h=19.8+20,d=DiaLM6, center=true,$fn=80); 
      
       translate([-8-10,39,0])  
    rotate([0,90,0])
      cylinder(h=19.8+20,d=DiaLM6, center=true,$fn=80);        
      
        // pass LM6 round linear rails itselfs
   
       translate([-18,-39,0])  
    rotate([0,90,0])
      cylinder(h=50,d=DiaAxis, center=true,$fn=24); 
      
       translate([-18,39,0]) 
    rotate([0,90,0])
      cylinder(h=50,d=DiaAxis, center=true,$fn=24);  
      
      
        // make 3 d printable without (much) support:
   
       translate([-40,-39.25,0])  
    rotate([0,90,0])
    scale([1,0.6,1])
      cylinder(h=12,d=12, center=true,$fn=48); 
            
       translate([-40,39.25,0])  
    rotate([0,90,0])
    scale([1,0.6,1])
      cylinder(h=12,d=12, center=true,$fn=48); 
           
     
         // remove lateral parts of LM6UU holder:
        
           translate([-12,51,0])
      cube([60,22,40]  ,center=true);
                
           translate([-12,-51,0])
      cube([60,22,40]  ,center=true);               
                    
     
          // pocket makes removal from baseplate easier:
     
      translate([5.5,0,-44 ]) 
          cube([4,8,4],center=true);
  
        // FIX MOTOR
        
      translate([0,19,-7-4-4])         
     rotate([0,90,0 ])
      cylinder(h=12,d=DiaM4+1,center=true,$fn=24); 
    
        translate([0,-19,-7-4-4])            
     rotate([0,90,0 ])
      cylinder(h=12,d=DiaM4+1,center=true,$fn=24); 
      
      
       translate([0,19,-7-4-4])         
     rotate([0,90,0 ])
      cylinder(h=12,d=DiaM4+1,center=true,$fn=24); 
    
        translate([0,0,-7-4-4-19])            
     rotate([0,90,0 ])
      cylinder(h=12,d=DiaM4+1,center=true,$fn=24); 
    
       translate([0,0,-7-4-4+19])            
     rotate([0,90,0 ])
      cylinder(h=12,d=DiaM4+1,center=true,$fn=24); 
    
    // Allow cooling :    
    /*
    hull()
    {
     translate([0,-14.0,-20.5])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    
         translate([0,-7,-27])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    }
    
     hull()
    {
     translate([0,14,-20.5])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    
         translate([0,7,-27])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    }
     
    hull()
    {
     translate([0,-14.0,-2.5])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    
         translate([0,-7,4])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    }
        
    hull()
    {
     translate([0,14.0,-2.5])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    
         translate([0,7,4])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    }
    */
      // ties of LM6UU holder - external
       
    
         translate([-8,-40,0])    
         scale([1,0.75,1])   
      cylinder(h=24,d=DiaTie, center=true,$fn=24); 
    
         translate([-8,40,0])    
          scale([1,0.75,1])  
      cylinder(h=24,d=DiaTie, center=true,$fn=24); 
      
    
   rotate([9,0,0])
      translate([-8,-29,5])             
        scale([1,0.75,1]) 
       cylinder(h=24,d=DiaTie, center=true,$fn=24); 
           
       rotate([-9,0,0])
         translate([-8,29,5])//   
           scale([1,0.75,1])     
           cylinder(h=24,d=DiaTie, center=true,$fn=24); 
     
    
    // ties of upmost LM6UU holder:        
       
    
         translate([-28,-40,0])    
         scale([1,0.75,1])   
      cylinder(h=24,d=DiaTie, center=true,$fn=24); 
    
         translate([-28,40,0])    
          scale([1,0.75,1])  
      cylinder(h=24,d=DiaTie, center=true,$fn=24); 
      
    
   rotate([9,0,0])
      translate([-28,-29,5])             
        scale([1,0.75,1]) 
       cylinder(h=24,d=DiaTie, center=true,$fn=24); 
          
       rotate([-9,0,0])
         translate([-28,29,5])//   
           scale([1,0.75,1])     
           cylinder(h=24,d=DiaTie, center=true,$fn=24);     
    
    // Did not need to fix the motor,
    // just here for who needs them
   
         translate([-8,16,-43])              
           cylinder(h=12,d=DiaFix, center=true,$fn=24); 
     
           translate([-28,16,-43])               
           cylinder(h=12,d=DiaFix, center=true,$fn=24); 
     
           translate([-8,-16,-43])               
           cylinder(h=12,d=DiaFix, center=true,$fn=24); 
     
           translate([-28,-16,-43])               
           cylinder(h=12,d=DiaFix, center=true,$fn=24); 
         
          
}
