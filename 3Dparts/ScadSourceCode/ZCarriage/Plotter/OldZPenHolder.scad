// THIS IS PEN HOLDER WITH FIXED ACME NUT. 
// IS FOR HISTORIC REASON HERE
// BETTER LET Z PEN HOLDER FLOAT 
// The pen holder can not serve as cutter holder.

   DiaM3=3.6;    // fix  with M3
   DiaM4 = 4.5;  // Z switch activation
   DiaM6=6.8;    // M6 shaft 
   DiaTie=3.9;    // fix LM6UU holders
   DiaPen =11.9;   //  CHANGE FOR OTHER PEN DIAMETER  
   Height=74;      
   LM6Height = 24;    


   color("lime")          
         
 rotate([180,0,0])  //180 no need to rotate in Slic3R        
   {        
difference()
{ 
  union()
  {                    
      // Central pen holder
      
     translate([0,6,-25.0])  // -1.5  
       cylinder(h=Height ,d=DiaPen+10 ,center=true,$fn=48);  //53

                    
   // side LM6UU dual holder     
          translate([39,0,-10])
       cylinder(h=LM6Height+20 ,d=20 ,center=true,$fn=48);  //
            
          translate([-39,0,-10])
       cylinder(h=LM6Height+20 ,d=20 ,center=true,$fn=48);  //
                
 // connect central  part:   
        translate([0,3,-10])
      cube([76,5,LM6Height+20],center=true);   
      
         // makes closing with M3 possible
     translate([0,6,-25.0])  // -1.5  
       cube([38,8 ,Height ] ,center=true);  //

      
      
  // Fix trapezoideal screw 
     
        translate([0,-21.5,9.5])                  
      cube([32,39,5],center=true);    
        
   // push Z switch       
    translate([24.5+0.5,-4,0])           
        cylinder(h=LM6Height,d=DiaM4+6, center=true,$fn=48);  //h28       
             ////////////////////////////////
             
     // central  reinforcement between LM6 shafts
             
 translate([14.0,-18,7.5])   //..9.5
      cube([4.0,46,8]  ,center=true); //.28           
          
            
 translate([-14.0,-18,7.5])           //
      cube([4.0,46,8]  ,center=true);   //                          
            
    // central  reinforcement between linear shafts
   
     translate([0,-3,9.5])   //..9.5
      cube([82.0,10,5]  ,center=true); //               
                                 
  }    
            // END UNION
      
       // push Z switch
       
       translate([24.5+0.5,-4,0])            
      cylinder(h=34,d=DiaM4, center=true,$fn=48);  //h28
    
  
  
  
  
    
  // ACME
    
     translate([0,-28,10])  //    
      cylinder(h=20,d=10.5,center=true,$fn=48); 
    
    // FIX NUT OF TRAPEZOIDAL SCREW     
    translate([5.8,-28+5.8,10])  //   
      cylinder(h=20,d=DiaM3,center=true,$fn=24);      
                
    translate([5.8,-28-5.8,10])  // 
      cylinder(h=20,d=DiaM3,center=true,$fn=24); 
    
     translate([-5.8,-28+5.8,10])  //   
      cylinder(h=20,d=DiaM3,center=true,$fn=24);      
                
    translate([-5.8,-28-5.8,10])  // 
      cylinder(h=20,d=DiaM3,center=true,$fn=24); 
       
    
     // room head M3=
        
    translate([0,-5.8,28+5.8])               
      cylinder(h=20,d=DiaM3,center=true,$fn=24);
               
        translate([0,-5.8,28-5.8])  
      cylinder(h=20,d=DiaM3,center=true,$fn=24); 
       
    
 
 //  LM6 holders 
      translate([-39,0,-10])
       cylinder(h=19.5+20 ,d=12.5 ,center=true,$fn=48);  //
           
  //  LM6 holder 
      translate([39,0,-10])
       cylinder(h=19.5 +20,d=12.5 ,center=true,$fn=48);  //
  
  
  
 // left shaft  
      translate([39,0,-10])
       cylinder(h=25+20 ,d=DiaM6 ,center=true,$fn=48);  //
         
 // right shaft
 
      translate([-39,0,-10])
       cylinder(h=25+20 ,d=DiaM6 ,center=true,$fn=48);  //
       
      // upwards more room for LM6UU shafts: 
  
 // left shaft  
      translate([39,0,-30])
      scale([0.6,1,1])
       cylinder(h=25 ,d=DiaM6+5 ,center=true,$fn=48);  //
    
  // left shaft  
      translate([-39,0,-30])
      scale([0.6,1,1])
       cylinder(h=25 ,d=DiaM6+5 ,center=true,$fn=48);  //
    
//  Central pen holder:

translate([0,6,-25])
       cylinder(h=Height*3 ,d=DiaPen,center=true,$fn=48); 
  
// Partial fence makes ties easier:

translate([0,-5,-60])  //
cube([1,20,50],center=true);
             
  
    // TIES :  
  
  // tie pair down or close with M3:  
  
  translate([15,6,-56])     // 9.5
  rotate([90,0,0])  
  cylinder(h=20 ,d=DiaM3,center=true,$fn=24);  // tie or closing M3 nut and bolt
    
   
  translate([-15,6,-56])     //
  rotate([90,0,0])  
  cylinder(h=20 ,d=DiaM3,center=true,$fn=24);  // 
  
   
  
//  ties or M3 higher :  
  
  translate([15,6,-39])     //-14
  rotate([90,0,0]) 
  cylinder(h=20 ,d=DiaM3,center=true,$fn=24);  // 
   
  translate([-15,6,-39])     //
  rotate([90,0,0])
  cylinder(h=20 ,d=DiaM3,center=true,$fn=24);  // 
  
    
//  fix  pen upmost
        
  translate([15.0,6,-2])     //
  rotate([90,0,0]) 
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  // 
        
  translate([-15.0,6,-2])     // 
  rotate([90,0,0])  
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  // 
    
  
//  fix LM6 with ties or elastic 
        
  translate([30.0,0,0])     //
  rotate([90,0,0]) 
  scale([0.7,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  // 
        
  translate([-30,0,0])     // 
  rotate([90,0,0]) 
   scale([0.7,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  // 
     
     
//  fix LM6 with ties  
        
  translate([30.0,0,-20])     //
  rotate([90,0,0]) 
  scale([0.7,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  // 
        
  translate([-30,0,-20])     // 
  rotate([90,0,0]) 
   scale([0.7,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  // 
     
     
    // lateral removal :
 translate([49,0,-10])
  cube([20,30,30+20],center=true);
  
     translate([-49,0,-10])
  cube([20,30,30+20],center=true);
    
    // fence to close pen holder and closing of pen holder
  
      translate([0,6,-25])
  cube([39,2,80],center=true);
   
  
    
    }
 }  // end rotate
         
         