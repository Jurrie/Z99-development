// This holder is intended for use without cover for pen or cutter.
// Tie pen or cutter
   DiaM3=3.6; // fix  with M3
   DiaM4 = 4.5; // Z switch activation
   DiaM6=6.8; // M6 shaft 
   DiaTie=3.9;  // somewhat larger
   DiaCutter=11.9;  //11.8
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
       cylinder(h=Height ,d=DiaCutter+10+5 ,center=true,$fn=48);  //53
                    
   // side LM6UU dual holder      
          translate([39,0,-10])
       cylinder(h=LM6Height ,d=20 ,center=true,$fn=48);  //
            
          translate([-39,0,-10])
       cylinder(h=LM6Height ,d=20 ,center=true,$fn=48);  //
                
 // connect central  part:   
      
        translate([0,2.5,-10])
      cube([76,5,LM6Height],center=true);   
      
  /*
     
       translate([0,-21.5,9.5])                  
      cube([32,39,5],center=true);    
        */
   // push Z switch 
/*   
    translate([24.5+0.5,-4,0])           
        cylinder(h=LM6Height-20,d=DiaM4+6, center=true,$fn=48);  //h28 
  */      
             ////////////////////////////////
             
     // central  reinforcement between LM6 shafts
             /*
 translate([14.0,-18,7.5])   //..9.5
      cube([4.0,46,8]  ,center=true); //.28           
          
            
 translate([-14.0,-18,7.5])           //
      cube([4.0,46,8]  ,center=true);   //                          
            */
    // central  reinforcement between linear shafts
   /*
     translate([0,-3,9.5])   //..9.5
      cube([82.0,10,5]  ,center=true); //               
             */                    
  }    
            // END UNION
      
       // push Z switch
       /*
       translate([24.5+0.5,-4,0])            
      cylinder(h=34,d=DiaM4, center=true,$fn=48);  //h28
    */
  
  
  
  
    
  // ACME
    /*
     translate([0,-28,10])  //    
      cylinder(h=20,d=10.5,center=true,$fn=48); 
    
    // FIX ACME     
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
       
    */
 
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
       cylinder(h=Height*3 ,d=DiaCutter,center=true,$fn=48);  
     
        
 
  
// Partial fence makes tying easier:

translate([0,-5,-60])  //
cube([1,20,50],center=true);
       
  
// Thickest part of cutter holder:
  
translate([0,6,-22.5-25-6])
       cylinder(h=4.0 ,d=16.75+0.25,center=true,$fn=48);  //17 is ok for cutter
    
 // Funnel makes it printable without support
  
translate([0,6,-25.4-25-6])
       cylinder(h=2 ,d2=16.75+0.25,d1=DiaCutter,center=true,$fn=24); 
      
     // NEW
     
            
  translate([12.5,5,-56-6])     //
  rotate([90,0,30]) 
  scale([1.5,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  //
  
   translate([-12.5,5,-56-6])     //
  rotate([90,0,-30]) 
  scale([1.5,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  //
  
            
  translate([12.5,6,-39-6])     //
  rotate([90,0,30]) 
  scale([1.5,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  //
  
   translate([-12.5,6,-39-6])     //
  rotate([90,0,-30]) 
  scale([1.5,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  //
  
                                     
  translate([12.5,6,-12+20])     //
  rotate([90,0,0])  
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  //
  
   translate([13.5,4,-9+20])     //
   cube([2.0,10,6],center=true);
 
 
   translate([-12.5,6,-12+20])     //
  rotate([90,0,0])   
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  //
   
   translate([-13.5,4,-9+20])     //
   cube([2.0,10,6],center=true);
 
        
                  
  translate([12.5,6,-14])     //
  rotate([90,0,0]) 
  scale([0.7,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  //
  
   translate([-12.5,6,-14])     //
  rotate([90,0,0]) 
  scale([0.7,1,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  //
  
  
    
  
//  fix LM6 with ties 
        
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
     
         
//  fix on pusher:  
        
  translate([22.0,0,2])     //
  rotate([90,0,0]) 
  scale([1,0.7,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  // 
                    
  translate([-22.0,0,2])     //
  rotate([90,0,0]) 
  scale([1,0.7,1])
  cylinder(h=20 ,d=DiaTie,center=true,$fn=12);  // 
       
     
  // top cutter holder removal :
 
 translate([0,16,0])
  cube([58,20,60],center=true);
     
   
     
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
         
         