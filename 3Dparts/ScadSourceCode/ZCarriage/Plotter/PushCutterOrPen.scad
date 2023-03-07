// This holder is intended for use without cover for pen or cutter.
// Tie pen or cutter
   DiaM3=3.6; // fix  with M3
   DiaM4 = 4.5; // Z switch activation
   DiaM6=6.8; // M6 shaft 
   DiaTie=3.9;  // somewhat larger
   DiaCutter=11.9;  //11.8
   Height=60;   // 80
   LM6Height = 24;    // was 24 for one LM6UU   44 for 2



   color("lime")          
         
 rotate([0,0,0])  //180 no need to rotate in Slic3R        
   {        
difference()
{ 
  union()
  {                    
   // Central Pen - Cutter holder
      
   //  translate([0,6,-28.0-10])  // -1.5  
  //     cylinder(h=Height ,d=DiaCutter+10+5 ,center=true,$fn=48);  //53
                    
 
 // connect central  part:   
      /*
        translate([0,2.5,-10-10])
      cube([76,5,LM6Height],center=true);   
         */  
      
      // fix acme
       translate([0,-20.5,9.5])                  
     cube([32,37,5],center=true);    
       
   // push Z switch 

    translate([24.5+0.5,-3,8])           
        cylinder(h=8,d=DiaM4+7, center=true,$fn=48);  //h28 
       
             ////////////////////////////////
             
     //  reinforcement 
             
 translate([14.0,-20.5,8])   //..9.5
      cube([4.0,37,8]  ,center=true); //.28           
          
            
 translate([-14.0,-20.5,8])           //
      cube([4.0,37,8]  ,center=true);   //                          
            
    // central  reinforcement between linear shafts
   
     translate([0,-3,8])   //..9.5
      cube([86.0,6,8]  ,center=true); //   


// LM6 shafts: 
 translate([-39,0,8])
       cylinder(h=8 ,d=15 ,center=true,$fn=48);  //
     
       translate([39,0,8])
       cylinder(h=8 ,d=15 ,center=true,$fn=48);  //
   
                                 
  }    
            // END UNION
      
       // push Z switch
       
       translate([25,-3,0])            
      cylinder(h=34,d=DiaM4, center=true,$fn=48);  //h28
    
  
    
  // ACME FIX NUT TRAPEZOIDEAL SCREW:
    
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
       
    
  
 // left shaft  
      translate([39,0,5])
       cylinder(h=25 ,d=7.2 ,center=true,$fn=48);  //
         
 // right shaft
 
      translate([-39,0,5])
       cylinder(h=25 ,d=7.2 ,center=true,$fn=48);  //
       
     
// 3 mm room for pen or cutter:

translate([0,6,-25])
       cylinder(h=Height*3 ,d=DiaCutter+6,center=true,$fn=48);  
       
  
    
    }
 }  // end rotate
         
         