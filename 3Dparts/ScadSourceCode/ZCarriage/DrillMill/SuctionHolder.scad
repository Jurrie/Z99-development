
DiaFix=1.9;
DiaAxis=6.7; // fix on linear shaft
DiaSuction = 8.3;   // Suction tube

color("lime")

rotate([180,0,0])  // no need rotation in Slicr3

difference() 
{
union()
    {    
     
     translate([0,39,0])  //  
      cylinder(h=14 ,d=DiaAxis+8, center=true,$fn=48);  //
            
          translate([0,-39,0])  //  
      cylinder(h=14 ,d=DiaAxis+8, center=true,$fn=48);  //
     
        
        // suction tube
        
       translate([28,0,-6])  //  -9
   rotate([0,30,0])
      cylinder(h=30 ,d=DiaSuction+7, center=true,$fn=48);  //
             
          
        
    // central  reinforcement 
   
      difference()
      {   
      
          translate([-15,0,0])  //  
      cylinder(h=14 ,d=90, center=true,$fn=96); 
          
           translate([-15,0,0])  //  
      cylinder(h=15 ,d=80, center=true,$fn=96);  
          
            // make bottom flat:
    translate([-50,0,0])
    cube([100,100,30],center=true);
   
          
          
      }
      
      
      
      
      
      
     
    }    // end upper union        
        
    
    
        // pass LM6 round linear rails itselfs
   
       translate([0,39,0])  //  
      cylinder(h=30,d=DiaAxis, center=true,$fn=24);  
     
    
    
       translate([0,-39,0])  //  
      cylinder(h=30,d=DiaAxis, center=true,$fn=24);  
     
    // DiaFix
    
    rotate([0,90,0])
         translate([0,39,0])//  
      cylinder(h=24,d=DiaFix, center=true,$fn=12); 
      
    rotate([0,90,0])
         translate([0,-39,0])//  
      cylinder(h=24,d=DiaFix, center=true,$fn=12); 
    

    
    rotate([90,90,0]) 
      cylinder(h=120,d=DiaFix, center=true,$fn=12);       
          
     // fix  suction tube:
     
     translate([32,0,0])
       rotate([90,90,0]) 
      cylinder(h=30,d=DiaFix, center=true,$fn=12); 
  
     
     translate([24,0,-12])
       rotate([90,90,0]) 
      cylinder(h=30,d=DiaFix, center=true,$fn=12); 
  
       // suction tube
        
       translate([28,0,-6])  //  -9
   rotate([0,30,0])
      cylinder(h=32 ,d=DiaSuction, center=true,$fn=48);  //
             
      
     
     
    
    // make bottom flat:
    translate([0,0,20.0])
    cube([100,100,26],center=true);
    
    
}
