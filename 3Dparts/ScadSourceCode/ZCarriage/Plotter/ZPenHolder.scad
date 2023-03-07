// This holder is intended for use without cover for pen or cutter.
// Tie pen

   DiaM3=3.6; // fix  with M3
   DiaM4 = 4.5; // Z switch activation
   DiaM6=6.8; // M6 shaft 
   DiaTie=3.9;  // somewhat larger
   DiaPen=11.9;  //11.8
   Height=80;   // 80 60
   LM6Height = 24+20;    // was 24 for one LM6UU   44 for 2



   color("lime")          
         
 rotate([180,0,0])  //180 no need to rotate in Slic3R        
   {        
difference()
{ 
  union()
  {                    
   // Central Pen - Cutter holder
      
     translate([0,6,-28.0])  // -1.5  
       cylinder(h=Height ,d=DiaPen+10 ,center=true,$fn=48);  
      
   // side LM6UU dual holder      
          translate([39,0,-10])
       cylinder(h=LM6Height ,d=20 ,center=true,$fn=48); 
            
          translate([-39,0,-10])
       cylinder(h=LM6Height ,d=20 ,center=true,$fn=48); 
                
 // connect central  part:   
      
        translate([0,2.5,-10])
      cube([76,5,LM6Height],center=true);   
      
                  
  }    
            // END UNION
     
  
 
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
     
  
// Partial fence makes tying easier:

translate([0,-5,-60]) 
cube([1,20,50],center=true);
       
  
      
  translate([11.5,5,-60])    
  rotate([90,0,30]) 
  scale([1.5,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12); 
  
   translate([-11.5,5,-60])     
  rotate([90,0,-30]) 
  scale([1.5,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12); 
  
            
  translate([11.5,6,-38])   
  rotate([90,0,30]) 
  scale([1.5,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12); 
  
   translate([-11.5,6,-38])     
  rotate([90,0,-30]) 
  scale([1.5,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12); 
  
                                     
  translate([11.0,6,-12+20])    
  rotate([90,0,0])  
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  
  
   translate([12.0,4,-9+20])    
   cube([2.0,10,6],center=true);
  
   translate([-11.0,6,-12+20])    
  rotate([90,0,0])   
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  
   
   translate([-12.0,4,-9+20])    
   cube([2.0,10,6],center=true);
                          
  translate([10.0,6,-14])    
  rotate([90,0,0]) 
  scale([0.7,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  
  
   translate([-10,6,-14])  
  rotate([90,0,0]) 
  scale([0.7,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);    
    
  
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
         
//  fix on pusher:  
        
  translate([22.0,0,2])   
  rotate([90,0,0]) 
  scale([1,0.7,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);
                    
  translate([-22.0,0,2])  
  rotate([90,0,0]) 
  scale([1,0.7,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);       
     
  
     
    // lateral removal :
 translate([49,0,-10])
  cube([20,30,30+20],center=true);
  
     translate([-49,0,-10])
  cube([20,30,30+20],center=true);
        
   // removal of front part
  
      translate([0,16,-25])
  cube([43,22,90],center=true);
    
    }
 }  // end rotate
         
         