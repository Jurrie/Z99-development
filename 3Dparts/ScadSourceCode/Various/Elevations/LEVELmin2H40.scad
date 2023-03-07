// Always lowest elevation, on bottom.
//  Level -1 comes on top of it


Height=40; // make any elevation 
         // 18 = same as MDF 18 mm


DiaM8=9.2; // somewhat larger, in case you print with Nozzle 0.8
Larger= 3; //sidewalls  larger then ground on 3 of the four sides
CollarHeight=3;

  difference()
         {
             
  union() {         
      
  // Base plate        
      
         difference()
      {
      
      translate([0,-(Larger+Larger/2),CollarHeight/2])
      cube([90+Larger*6,65+Larger*3,CollarHeight+Height],center = true);  //* 94 61 6
          
           translate([0,-Larger ,Height/2+CollarHeight/2])
      cube([90+Larger*4+1,65+1+Larger*2,CollarHeight+1],center = true);  //
          
          // 0.8 ->1 if printed with nozzle 0.8
                  
      }
    
       translate([-50,-34.25-Larger*2,-1])                  
       rotate([90,0,0])                  
       linear_extrude(2.0)                 
              text("LEVEL -2","Arial",size=8,direction="ltr",halign="top");  
                          
      
        translate([10,-34.25-Larger*2,-1])                  
       rotate([90,0,0])                  
       linear_extrude(2.0)                 
              text("H 40mm",size=8,direction="ltr",halign="top");  
      
      
      
       rotate([0,0,180])  
        translate([-20,-31.75,8])                  
       rotate([90,0,0])                  
       linear_extrude(2.0)                 
              text("L-2 H40",size=8,direction="ltr",halign="top");        
      
  }         // END UNION
  
  
  // Entrance M8 bars
  hull()
  {  
      translate([34.75,0,0]) 
         cylinder(h=Height+1,d=DiaM8,center=true,$fn=24 );     // = 16.6 = ok
   
   translate([34.75,40,0]) 
         cylinder(h=Height+CollarHeight*2+1,d=DiaM8,center=true,$fn=24 );     // = 16.6 = ok   
  }
          

  hull()
  {  
      translate([-34.75,0,0]) 
         cylinder(h=Height+1,d=DiaM8,center=true,$fn=24 );     // = 16.6 = ok
   
   translate([-34.75,40,0]) 
         cylinder(h=Height+CollarHeight*2+1,d=DiaM8,center=true,$fn=24 );     // = 16.6 = ok   
  }
  
 
   // More lateral room
 
  translate([36,15,0])  
     cube([20,22,Height+1],center = true);  //* 94 61 6
      
  
  translate([-36,15,0])  
     cube([20,22,Height+1],center = true);  //* 94 61 6
    
  
  //make entrances somewhat wider :
 
  translate([-31,30,0]) 
  rotate([0,0,30])
  cylinder(h=Height+1,d=DiaM8+20,center=true,$fn=3 );     
  
  translate([31,30,0]) 
  rotate([0,0,30])
  cylinder(h=Height+1,d=DiaM8+20,center=true,$fn=3 );     // = 16.6 = ok
   
  // large middle spare outs
  
    hull()
  {  
      translate([-35,-22,0]) 
      cube([24,25,Height+1],center=true);
         //cylinder(h=Height+1,d=20,center=true,$fn=24 );     // = 16.6 = ok
   
      
        translate([35,-22,0]) 
      cube([24,25,Height+1],center=true);
  }
  
  
  minkowski()
  {
  
  
     translate([0,11,0]) 
      cube([26,16,Height],center=true);
            
         cylinder(h=1,d=14,center=true,$fn=24 );     // = 16.6 = ok
   
  }
    
  
  // extra spare outs:
  /*
     translate([-45,-8,0]) 
      cube([8,12,Height+1],center=true);
    
    translate([45,-8,0]) 
      cube([8,12,Height+1],center=true);
    */
  // make removal from bed easier:
    translate([58,0,-20.5]) 
      cube([12,8,4],center = true);  //* 94 61 6
  
     translate([-58,0,-20.5]) 
      cube([12,8,4],center = true);  //* 94 61 6
       
  }