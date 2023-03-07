
DiaFix=1.9;
DiaSuction = 8.3;   // Suction tube
// This part depends on vacuum cleaner in use.
// If not ok for you, adapt to your wishes.
// or tape suction tube in vacuum cleaner until air tight.


color("lime")


difference() 
{
union()
    {    
    
        
     translate([0,0,-9])  //  
      cylinder(h=17 ,d=DiaSuction+7, center=true,$fn=48);  //
            
              
      cube([24.4,7.1,35],center=true); 
          
          
      
      
      
      
      
     
    }    // end upper union        
    
          // suction tube:
    
      translate([0,0,-12])  //  
      cylinder(h=17 ,d1=DiaSuction-0.2,d2=DiaSuction+0.2, center=true,$fn=48);  //
            
    
    
         translate([0,0,10])  //  
       
      cube([21.4,4.1,35],center=true); 
        
     
     
    // fix  suction tube:
     
     translate([0,0,-10])
       rotate([90,90,0]) 
      cylinder(h=30,d=DiaFix, center=true,$fn=12); 
  
     
       
       // demo check, comment out
       
   //     translate([0,10,0])  //  
       
   //   cube([30,20,40],center=true); 
        
     
    
}
