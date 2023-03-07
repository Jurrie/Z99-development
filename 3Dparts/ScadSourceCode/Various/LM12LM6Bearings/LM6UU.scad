// Is alternative for LM6UU holders, PETG is better then PLA.

 DiaLM6=6.45; // verify and adjust if necessary

DiaOuterLM6=11.75; 
HeightLM6= 18.8;   // = length

difference()
    {
                 
        union()
    {    
       cylinder(h=HeightLM6,d=DiaOuterLM6,center=true,$fn=96)  ;    
      
    
    
    }
    
    
  cylinder(h=HeightLM6+1,d=DiaLM6,center=true,$fn=96)  ;    
  
    
    
        for(z=[0:60:180])
        {
        rotate([0,0,z])
        
cube([8,1.8,HeightLM6+1],center=true);        //1.8
        }
        
       // some discrete funnel d 0.6 mm and 1 mm high on begin end end:
        /*
        translate([0,0,HeightLM10/2])
      cylinder(d1=DiaLM6,d2=DiaLM10+1.2,h=2.0,center=true,$fn=96)  ;    
    
        translate([0,0,-HeightLM6/2])
      cylinder(d2=DiaLM6,d1=DiaLM6+1.2,h=2.0,center=true,$fn=96)  ;
        */   
   
    
              
}       