// OPTIONAL !  LEVEL 0
// This is TOPMOST elevation
//  Level -1 , if necessary,  comes lower

Height=32; // change to make any elevation        


DiaM8=9.2; // somewhat larger
Larger= 3; //sidewalls  larger then ground on 3 of the four sides
CollarHeight=3;


  difference()
         {
             
  union() {         
      
  // Base plate
      
      
         translate([0,-Larger/2,0])
      cube([90+Larger*2,65+Larger*1,Height],center = true);  //* 94 61 6
   
      
          difference()
      {
      
      translate([0,-Larger/2,(Height+CollarHeight)/2])
      cube([90+Larger*2,65+Larger*1,CollarHeight],center = true);  //* 94 61 6
          
           translate([0,0 ,Height/2+CollarHeight/2])
      cube([90+1,65+1,CollarHeight+1],center = true);  //
          
          // 0.8 ->1 if printed with nozzle 0.
      }
          
      
       translate([-46,-34.25,-1])                  
       rotate([90,0,0])                  
       linear_extrude(2.0)                 
              text("LEVEL 0","Arial",size=8,direction="ltr",halign="top");  
                          
      
        translate([4,-34.25,-1])                  
       rotate([90,0,0])                  
       linear_extrude(2.0)                 
              text("H 32mm",size=8,direction="ltr",halign="top");  
                  
            
       rotate([0,0,180])  
        translate([-10,-31.75,-1])                  
       rotate([90,0,0])                  
       linear_extrude(2.0)                 
              text("H32",size=8,direction="ltr",halign="top");  
        
      
      
  }         // END UNION
  
  
  //  M8 bars
  hull()
  {  
      translate([34.75,0,0]) 
         cylinder(h=Height+1,d=DiaM8,center=true,$fn=24 );     
   
   translate([34.75,40,0]) 
         cylinder(h=Height+CollarHeight*2+1,d=DiaM8,center=true,$fn=24 );    
  }
      
    

  hull()
  {  
      translate([-34.75,0,0]) 
         cylinder(h=Height+1,d=DiaM8,center=true,$fn=24 );    
   
   translate([-34.75,40,0]) 
         cylinder(h=Height+CollarHeight*2+1,d=DiaM8,center=true,$fn=24 );   
  }
  
 
   // More lateral room
  hull()
  {  
      translate([33,10,0]) 
         cylinder(h=Height+1,d=12,center=true,$fn=24 );    
   
   translate([33,40,0]) 
         cylinder(h=Height+CollarHeight*2+1,d=12,center=true,$fn=24 );   
  }
      
    hull()
  {  
      translate([-33,10,0]) 
         cylinder(h=Height+1,d=12,center=true,$fn=24 );    
   
   translate([-33,40,0]) 
         cylinder(h=Height+CollarHeight*2+1,d=12,center=true,$fn=24 );   
  }
    
  
  //make entrances somewhat wider :
 
  translate([-32,30,0]) 
  rotate([0,0,30])
  cylinder(h=Height+1,d=DiaM8+20,center=true,$fn=3 );  
   
  
  translate([32,30,0]) 
  rotate([0,0,30])
  cylinder(h=Height+1,d=DiaM8+20,center=true,$fn=3 ); 
   
    
  
  // large middle spare outs
  
    hull()
  {  
      translate([-35,-19,0]) 
         cylinder(h=Height+1,d=17,center=true,$fn=48 );   
   
   translate([35,-19,0]) 
         cylinder(h=Height+CollarHeight*2+1,d=17,center=true,$fn=48 );    
  }
     
  // OTHER large middle spare outs
   
  minkowski()
  {  
  
     translate([0,11,0]) 
      cube([26,16,Height],center=true);
            
         cylinder(h=1,d=14,center=true,$fn=24 );    
   
  }
   // make removal from bed easier:
    translate([52,0,-Height/2]) 
      cube([12,10,3],center = true);  
  
     translate([-52,0,-Height/2]) 
      cube([12,10,3],center = true);  
  
  }