// 
//  Level -1 , if necessary,  comes below Level ground 0




Height=40; // make any elevation 
         // 18 = same as MDF 18 mm


DiaM8=9.2; // somewhat larger
Larger= 3; // was 4 sidewalls  larger then ground on 3 of the four sides
CollarHeight=3;

  difference()
         {
             
  union() {         
      
  // Base plate        
      
         difference()
      {
      
      translate([0,-Larger,CollarHeight/2])
      cube([90+Larger*4,65+Larger*2,CollarHeight+Height],center = true);  //* 94 61 6
          
           translate([0,-Larger/2 ,Height/2+CollarHeight/2])
      cube([90+Larger*2+1,65+1+Larger,CollarHeight+1],center = true);  
          // 0.8 ->1 if printed with nozzle 0.8
                  
      }                    
      
       translate([-50,-34.25-Larger,-1])                  
       rotate([90,0,0])                  
       linear_extrude(2.0)                 
              text("LEVEL -1","Arial",size=8,direction="ltr",halign="top");  
                          
      
        translate([5,-34.25-Larger,-1])                  
       rotate([90,0,0])                  
       linear_extrude(2.0)                 
              text("H 40mm",size=8,direction="ltr",halign="top");  
              
      
       rotate([0,0,180])  
        translate([-20,-31.75,8])                  
       rotate([90,0,0])                  
       linear_extrude(2.0)                 
              text("L-1 H40",size=8,direction="ltr",halign="top");  
            
      
  }         // END UNION
  
  
  // Entrance M8 bars
  hull()
  {  
      translate([34.75,0,0]) // 31
         cylinder(h=Height+1,d=DiaM8,center=true,$fn=24 );     // = 16.6 = ok
   
   translate([34.75,40,0]) // 31 
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
    
  translate([35,15,0])  
     cube([16,22,Height+1],center = true);  //* 94 61 6
      
  
  translate([-35,15,0])  
     cube([16,22,Height+1],center = true);  //* 94 61 6
  
  
  //make entrances somewhat wider :
 
  translate([-31,30,0]) 
  rotate([0,0,30])
  cylinder(h=Height+1,d=DiaM8+16,center=true,$fn=3 );     // = 16.6 = ok
   
  
  translate([31,30,0]) 
  rotate([0,0,30])
  cylinder(h=Height+1,d=DiaM8+16,center=true,$fn=3 );     // = 16.6 = ok
   
  
  
  
  // large middle spare outs
    hull()
  {  
      translate([-34,-21,0]) 
      cube([20,20,Height+1],center=true);
       
      
        translate([34,-21,0]) 
      cube([20,20,Height+1],center=true);
    
  }
  
  
  minkowski()
  {
  
  
     translate([0,11,0]) 
      cube([26,16,Height],center=true);
            
         cylinder(h=1,d=14,center=true,$fn=24 );     // = 16.6 = ok
   
  }    
  
   // make removal from bed easier:
    translate([56,-2,-20.5]) 
      cube([12,8,4],center = true);  //* 94 61 6
  
     translate([-56,-2,-20.5]) 
      cube([12,8,4],center = true);  //* 94 61 6
  
  
  }