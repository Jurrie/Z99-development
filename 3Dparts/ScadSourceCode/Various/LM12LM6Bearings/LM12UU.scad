// possible PLA or even better PETG replacement for LM12UU

DiaLM12= 12.3; // ok for me, verify
DiaOuterLM12=21.2; // my result =21
HeightLM12= 29.9;  // my result = 30

difference()
    {
                 
        union()
    {    
       cylinder(h=HeightLM12,d=DiaOuterLM12,center=true,$fn=96)  ;    
    }          
      
   union() 
    {
        
  cylinder(h=HeightLM12+1,d=DiaLM12,center=true,$fn=96)  ;    
  
        for(z=[0:36:180])
        {
        rotate([0,0,z])
        
cube([(HeightLM12+2)/2,2.1,HeightLM12+1],center=true);        //
            
            
        }
        
       // some discrete funnel d 0.6 mm and 1 mm high on begin and end:
        
        translate([0,0,HeightLM12/2])
      cylinder(d1=DiaLM12,d2=DiaLM12+1.2,h=2.0,center=true,$fn=96)  ;    
    
        translate([0,0,-HeightLM12/2])
      cylinder(d2=DiaLM12,d1=DiaLM12+1.2,h=2.0,center=true,$fn=96)  ;    
   
    }  
    
    // Comment out the two lines below 
    // to print half 
    
  //  translate([0,15,0])   
  //   cube([30,30,40],center=true);
    
    
              
}       