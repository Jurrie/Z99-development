
DiaM3=3.5;
DiaM4=4.5;
DiaTie=4.0;
DiaAxis=6.7; // pass linear shaft
DiaLM6 = 12.2;   /// FOR LM6 linear ball bearing of 6 mm 

DiaMotor= 37.6; // measured 37.2  adapt if necessary

HeightLM6UU = 19;
color("lime")

 rotate([0,90,0])   //  makes no rotation in slic3r necessary


difference() 
{
union()
    {    
             
 translate([-19,0,0])   //..5.5 temp to 0.5
      cube([4.0,70,18]  ,center=true); //..28 temp to 48
          
          
        // MIDDLE PÄRT +   ACME :           
                        
// D max 25 -> 34 :
            
         translate([-18,0,20])                  
      cube([6,32,40],center=true);    
                     
    // central  reinforcement between LM6UU
           
 translate([-16,14,17.5])   //
      cube([8.0,4,45]  ,center=true); //
         
                           
 translate([-16,-14,17.5])   //
      cube([8.0,4,45]  ,center=true); // 

  // SUPPORT 
      translate([-8,14,38])                 
      cube([ HeightLM6UU+7 ,4,4]  ,center=true); // 
   
  translate([-8,-14,38])                 
      cube([ HeightLM6UU+7 ,4,4]  ,center=true); // 
   

   
       //  motor holder :
            
           translate([-8-5,0,-7])  //  -9 
    rotate([0,90,0])
      cylinder(h=26+10,d=DiaMotor+6, center=true,$fn=96);   //h50 dia dremel
                 
      // LM6UU holders:       
      // LM6UU
            
       translate([-8.0-10,-39,0])  //  -9 
    rotate([0,90,0])
      cylinder(h=HeightLM6UU+7+20,d=DiaLM6+8, center=true,$fn=40);   //h28
        
          translate([-8-10,39,0])  //  -9
    rotate([0,90,0])
      cylinder(h=HeightLM6UU+7+20 ,d=DiaLM6+8, center=true,$fn=40);  //
            
    // central  reinforcement between LM6UU
           
 translate([-8-5,0,1.0])   //
    cube([HeightLM6UU+7+10,74,5]  ,center=true);   
    
      // for M4 screw ( L50mm  ) to push Z switch
      // this side is not in use, is here for reinforcement.
 
  translate([-13,-25.5,3])  //    
   rotate([0,90,0])   
     cylinder(h=26+10,d=DiaM4+8 ,center=true,$fn=48); // ..16.3
 
   // for M4 screw ( L50mm  ) to push Z switch
 
  translate([-8-5,25.5,3])     
   rotate([0,90,0])   
     cylinder(h=26+10,d=DiaM4+8 ,center=true,$fn=48); // ..16.3
     
     
 //  SUPPORT  ACME, CUT AWAY AFTER PRINTING !
    
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
 
  translate([-13,-25.5,3])  //    
   rotate([0,90,0])   
     cylinder(h=40,d=DiaM4 ,center=true,$fn=48); // ..16.3
 
   // for M4 screw ( L50mm  ) to push Z switch
 
  translate([-13,25.5,3])  //    
   rotate([0,90,0])   
     cylinder(h=40,d=DiaM4 ,center=true,$fn=48); // ..16.3
 
    
    
    
    
    
    
    
        // room motor :
            
           translate([-19,0,-7])  //  -9 
    rotate([0,90,0])
      cylinder(h=42,d=DiaMotor, center=true,$fn=96);   //44
              
    
        // bottom motor :
            
           translate([5,0,-7])  //  -9 
    rotate([0,90,0])
      cylinder(h=10,d=14, center=true,$fn=96);   //44
         
        
    
  // ACME
    
      translate([-8,0,28])             // 32
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
       
        
         
   // two cylinders for LM6UU round linear rails holders
   
       translate([-8-10,-39,0])  //  ..10
    rotate([0,90,0])
      cylinder(h=19.8+20,d=DiaLM6, center=true,$fn=80); 
      
       translate([-8-10,39,0])  //  ..10
    rotate([0,90,0])
      cylinder(h=19.8+20,d=DiaLM6, center=true,$fn=80);        
      
        // pass LM6 round linear rails itselfs
   
       translate([-8-10,-39,0])  // 
    rotate([0,90,0])
      cylinder(h=50,d=DiaAxis, center=true,$fn=24); 
      
       translate([-8-10,39,0])  // 
    rotate([0,90,0])
      cylinder(h=50,d=DiaAxis, center=true,$fn=24);  
      
      
        // make 3 d printable without (much) support:
   
       translate([-40,-39.25,0])  // -20
    rotate([0,90,0])
    scale([1,0.6,1])
      cylinder(h=12,d=12, center=true,$fn=48); 
      
      
       translate([-40,39.25,0])  // -20
    rotate([0,90,0])
    scale([1,0.6,1])
      cylinder(h=12,d=12, center=true,$fn=48); 
      
          
         // remove lateral parts of LM6UU holder:
        
           translate([-18,51,0])//
      cube([50,22,40]  ,center=true); //
                
           translate([-18,-51,0])//
      cube([50,22,40]  ,center=true); //               
                    
     
          // pocket makes removal from baseplate easier:
     
          
      translate([6,0,-35 ])  //..7.5-4..25 = 3.25 )
          cube([4,6,16],center=true);
        
          
     
        // FIX MOTOR
        
      translate([0,12.5,-7])             
     rotate([0,90,0 ])
      cylinder(h=12,d=DiaM3,center=true,$fn=24); 
    
        translate([0,-12.5,-7])             
     rotate([0,90,0 ])
      cylinder(h=12,d=DiaM3,center=true,$fn=24); 
    
    
    // Allow cooling :    
    
    hull()
    {
     translate([0,-10.0,-14.5])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    
         translate([0,-5.5,-18])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    }
    
     hull()
    {
     translate([0,10,-14.5])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    
         translate([0,5.5,-18])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    }
     
    hull()
    {
     translate([0,-10,1.5])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    
         translate([0,-6,5])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    }
        
    hull()
    {
     translate([0,10,1.5])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    
         translate([0,6,5])             
     rotate([0,90,0 ])
      cylinder(h=12,d=5,center=true,$fn=24); 
    }
    
      // ties of LM6UU holder:               
    
         translate([-8,-40,0])//    
         scale([1,0.75,1])   
      cylinder(h=24,d=DiaTie, center=true,$fn=24); 
    
         translate([-8,40,0])//    
          scale([1,0.75,1])  
      cylinder(h=24,d=DiaTie, center=true,$fn=24);      
    
   rotate([9,0,0])
      translate([-8,-29,-5])             
        scale([1,0.75,1]) 
       cylinder(h=48,d=DiaTie, center=true,$fn=24); 
           
       rotate([-9,0,0])
         translate([-8,29,-5])//   
           scale([1,0.75,1])     
           cylinder(h=48,d=DiaTie, center=true,$fn=24); 
     
    // ties of upper LM6UU holder:               
    
         translate([-8-20,-40,0])//    
         scale([1,0.75,1])   
      cylinder(h=24,d=DiaTie, center=true,$fn=24); 
    
         translate([-8-20,40,0])//    
          scale([1,0.75,1])  
      cylinder(h=24,d=DiaTie, center=true,$fn=24);      
    
   rotate([9,0,0])
      translate([-8-20,-29,-5])             
        scale([1,0.75,1]) 
       cylinder(h=48,d=DiaTie, center=true,$fn=24); 
           
       rotate([-9,0,0])
         translate([-8-20,29,-5])//   
           scale([1,0.75,1])     
           cylinder(h=48,d=DiaTie, center=true,$fn=24); 
     
    
          
}
