// serves only for location of M9 holes in MDF plate.
//  print 4 pieces

DiaLM12= 12.5;
DiaOuterLM12=25.6;
HeightLM12= 29.6;

difference()

    {                 
        union()   
        
    {  
     // Y:
        cylinder(h=HeightLM12,d=DiaOuterLM12,center=true,$fn=96)  ;    
     // X        
       translate([-17,2.5+7,0]) 
         rotate([90,0,0])  
       cylinder(h=20,d=DiaLM12+6,center=true,$fn=96)  ;  
    }          
      
   union() 
    {        
        
  cylinder(h=HeightLM12+1,d=DiaLM12,center=true,$fn=96)  ;    
  
        for(z=[0:36:180])
        {
        rotate([0,0,z])        
cube([(HeightLM12+2)/2+4,2.1,HeightLM12+1],center=true);        //
        }                    
                
      // remove part bottom
    translate([10.50,0,0])    
              
cube([10,50,HeightLM12+1],center=true);        //
               
        // print on one of both sides:
        
    translate([0,0,13]) 
cube([50,50,10],center=true);        //
          
         translate([0,0,-13])    
cube([50,50,10],center=true);        //
               
        // demo section :
        /*
          translate([0,0,10])    
cube([60,50,20],center=true);        //
      */        
        
       translate([-17,5.5+7,0])                
         rotate([90,0,0])         
       cylinder(h=20,d=DiaLM12,center=true,$fn=96)  ;  
        
    }  
              
}       