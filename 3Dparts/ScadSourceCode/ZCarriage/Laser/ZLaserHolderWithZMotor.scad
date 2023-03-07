
// Laser holder with possibility of adjusting focus distance with Z motor.

   DiaM3=3.6;    // fix  with M3
   DiaM4 = 4.5;  // Z switch activation
   DiaM6=6.8;    // M6 shaft 
   DiaTie=3.9;    // fix LM6UU holders  
   Height=74;    
   LM6Height = 24;    


   color("lime")          
         
 rotate([180,0,0])  //180: no need to rotate in Slic3r        
   {        
difference()
{ 
  union()
  {                    
    
                    
   // side LM6UU dual holder     
          translate([39,0,-10])
       cylinder(h=LM6Height+20 ,d=20 ,center=true,$fn=48);  
            
          translate([-39,0,-10])
       cylinder(h=LM6Height+20 ,d=20 ,center=true,$fn=48); 
                
 // connect central  part:   
        translate([0,-6,-10])
      cube([76,5,LM6Height+20],center=true);   
      
         // backplate for laser
      
     translate([0,-6,-27.0]) 
       cube([36,5 ,76 ] ,center=true); 

      
      
  // Fix trapezoideal screw 
     
        translate([0,-22.5,9.5])                  
      cube([32,37,5],center=true);    
        
   // push Z switch       
    translate([25.5,-4,-10])           
        cylinder(h=LM6Height+20,d=DiaM4+6, center=true,$fn=48);   
             
     //  reinforcement
           
 translate([14.0,-22.5,7.5])  
      cube([4.0,37,8]  ,center=true);   
            
 translate([-14.0,-22.5,7.5])          
      cube([4.0,37,8]  ,center=true);
  }    
            // END UNION
      
   // push Z switch
       
       translate([25.5,-4,-10])            
      cylinder(h=54,d=DiaM4, center=true,$fn=48);    
  
    
  // ACME
    
     translate([0,-28,10]) 
      cylinder(h=20,d=10.5,center=true,$fn=48); 
    
    // FIX NUT OF TRAPEZOIDAL SCREW     
    translate([5.8,-28+5.8,10])   
      cylinder(h=20,d=DiaM3,center=true,$fn=24);      
                
    translate([5.8,-28-5.8,10])  
      cylinder(h=20,d=DiaM3,center=true,$fn=24); 
    
     translate([-5.8,-28+5.8,10])  
      cylinder(h=20,d=DiaM3,center=true,$fn=24);      
                
    translate([-5.8,-28-5.8,10]) 
      cylinder(h=20,d=DiaM3,center=true,$fn=24); 
       
    
     // room head M3=
        
    translate([0,-5.8,28+5.8])               
      cylinder(h=20,d=DiaM3,center=true,$fn=24);
               
        translate([0,-5.8,28-5.8])  
      cylinder(h=20,d=DiaM3,center=true,$fn=24); 
       
    
 
 //  LM6 holders 
      translate([-39,0,-10])
       cylinder(h=19.5+20 ,d=12.5 ,center=true,$fn=48); 
           
  //  LM6 holder 
      translate([39,0,-10])
       cylinder(h=19.5 +20,d=12.5 ,center=true,$fn=48); 
  
  
 // left shaft  
      translate([39,0,-10])
       cylinder(h=25+20 ,d=DiaM6 ,center=true,$fn=48);  
         
 // right shaft
 
      translate([-39,0,-10])
       cylinder(h=25+20 ,d=DiaM6 ,center=true,$fn=48); 
       
      // upwards more room for LM6UU shafts: 
  
 // left shaft  
      translate([39,0,-30])
      scale([0.6,1,1])
       cylinder(h=25 ,d=DiaM6+5 ,center=true,$fn=48);  
    
  // left shaft  
      translate([-39,0,-30])
      scale([0.6,1,1])
       cylinder(h=25 ,d=DiaM6+5 ,center=true,$fn=48);  

//  fix LM6 with ties 
        
  translate([30.0,0,0])    
  rotate([90,0,0]) 
  scale([0.7,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  
        
  translate([-30,0,0])     
  rotate([90,0,0]) 
   scale([0.7,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  
     
     
//  fix LM6 with ties  
        
  translate([30.0,0,-20])    
  rotate([90,0,0]) 
  scale([0.7,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12); 
        
  translate([-30,0,-20])     
  rotate([90,0,0]) 
   scale([0.7,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12); 
  
    // lateral removal :
 translate([49,0,-10])
  cube([20,30,30+20],center=true);
  
     translate([-49,0,-10])
  cube([20,30,30+20],center=true);
    
   // Fix the laser with M3    
                                            
     for (x=[0:1:5])
      translate([0,0,-58+x*10])
        rotate([90,0,0])     
      cylinder(h=24,d=DiaM3, center=true,$fn=24);
    
    }
 }  // end rotate
         
         