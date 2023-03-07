// Dremel 4000 holder

DiaM3=3.5;
DiaTie  = 4.0;     
DiaM4=4.5;
DiaAxis=6.8; // pass linear shaft
DiaLM6 = 12.2;   /// FOR LM6 linear ball bearing of 6 mm 
DiaDremel=47;   // adapt if necessary

HeightLM6UU = 19;
color("lime")


 rotate([0,90,0]) // no rotation in slic3r necessary


difference() 
{
union()
    {    
        
              minkowski()
            {
 translate([3,0,0])   //..5.5 temp to 0.5
      cube([3.0,70,14]  ,center=true); //..28 temp to 48
                
   rotate([0,90,0 ])
      cylinder(h=1,d=4,center=true,$fn=24);                    
            }
               
            
     // MIDDLE PÄRT +   ACME :           
        
       translate([2.5,0,17])                  
      cube([5,32,44],center=true);    
               
    //  reinforcement acme
             
 translate([-9,14,17])  
      cube([20.0,4,44]  ,center=true); 
                                    
 translate([-9,-14,17])  
      cube([20.0,4,44]  ,center=true);      
      
       //  dremel holder :
            
           translate([-17,0,-27])  
    rotate([0,90,0])
      cylinder(h=44,d=DiaDremel+10, center=true,$fn=96);   //h50 dia dremel
                 
      // LM6UU holders:       
            
       translate([-8.0-10,-39,0])  
    rotate([0,90,0])
      cylinder(h=HeightLM6UU+7+20,d=DiaLM6+8, center=true,$fn=40);  
        
          translate([-8-10,39,0])  
    rotate([0,90,0])
      cylinder(h=HeightLM6UU+7+20 ,d=DiaLM6+8, center=true,$fn=40);  
            
    // central  reinforcement between LM6UU
           
 translate([-17,0,-6.0])   
      cube([HeightLM6UU+25,74,6]  ,center=true);  
           
        
      // Close the 2 parts of dremel holder
           translate([-17,0,-28]) 
     cube([44,88,12]  ,center=true); //
     
     
     
     
   // for M4 screw ( L50mm  ) to push Z switch
 
  translate([-1,25.5,2.5])     
   rotate([0,90,0])   
     cylinder(h=11,d=DiaM4+7 ,center=true,$fn=48); 
   
    }    // end upper union        
     
     
     // for M4 screw ( L50mm  ) to push Z switch
 
  translate([-1,25.5,2.5])  //    
   rotate([0,90,0])   
     cylinder(h=20,d=DiaM4 ,center=true,$fn=48);
    
    
        // fix dremel :
            
           translate([-7-5-4,0,-7-20]) 
    rotate([0,90,0])
      cylinder(h=54,d=DiaDremel, center=true,$fn=96);  
              
    //some funnel on top             
           translate([-36,0,-7-20]) 
    rotate([0,90,0])
      cylinder(h=8,d1=DiaDremel+5,d2=DiaDremel, center=true,$fn=96);  
            
    // fence=
           translate([-20,0,-28]) 
     cube([52,100,2]  ,center=true); //
    
    
  // ACME
    
      translate([0,0,28])             
     rotate([0,90,0 ])
      cylinder(h=12,d=10.5,center=true,$fn=48); 
    
    // FIX ACME
      translate([0,5.8,28+5.8])             
     rotate([0,90,0 ])
      cylinder(h=12,d=DiaM3,center=true,$fn=24); 
      
        translate([0,5.8,28-5.8])             
     rotate([0,90,0 ])
      cylinder(h=12,d=DiaM3,center=true,$fn=24); 
    
     // fix acme with M3:
         
      translate([0,-5.8,28+5.8])             
     rotate([0,90,0 ])
      cylinder(h=12,d=DiaM3,center=true,$fn=24);
                
        translate([0,-5.8,28-5.8])             
     rotate([0,90,0 ])
      cylinder(h=12,d=DiaM3,center=true,$fn=24); 
       
                 
   // two cylinders for LM6UU round linear rails holders
   
       translate([-8-10,-39,0])  //  ..10
    rotate([0,90,0])
      cylinder(h=19.8+20,d=DiaLM6, center=true,$fn=80); 
      
       translate([-8-10,39,0]) 
    rotate([0,90,0])
      cylinder(h=19.8+20,d=DiaLM6, center=true,$fn=80);        
      
        // pass LM6 round linear rails itselfs
   
       translate([-8,-39,0])   
    rotate([0,90,0])
      cylinder(h=80,d=DiaAxis, center=true,$fn=24); 
      
       translate([-8,39,0]) 
    rotate([0,90,0])
      cylinder(h=80,d=DiaAxis, center=true,$fn=24);  
      
      
        // make 3 d printable without support:
   
       translate([-40,-39.25,0])  
    rotate([0,90,0])    
    scale([1,0.6,1])
      cylinder(h=12,d=12, center=true,$fn=48); 
      
      
       translate([-40,39.25,0])  
    rotate([0,90,0])
    scale([1,0.6,1])
      cylinder(h=12,d=12, center=true,$fn=48); 
      
          
         // remove lateral parts of LM6UU holder:
        
           translate([-18,51,0])
      cube([50,22,40]  ,center=true);
                
           translate([-18,-51,0])
      cube([50,22,40]  ,center=true); 
             
               
     // ties of LM6UU holder:        
       
       rotate([10,0,0])
         translate([-9,29,-5])   
           scale([1,0.75,1])     
           cylinder(h=28,d=DiaTie, center=true,$fn=48); 
         
         translate([-9,40,0])    
          scale([1,0.75,1])  
      cylinder(h=24,d=DiaTie, center=true,$fn=48); 
      
   rotate([-10,0,0])
      translate([-9,-29,-5])             
        scale([1,0.75,1]) 
       cylinder(h=28,d=DiaTie, center=true,$fn=48); 
           
         translate([-9,-40,0])    
         scale([1,0.75,1])   
      cylinder(h=24,d=DiaTie, center=true,$fn=48); 
      
          // higher
       rotate([10,0,0])
         translate([-9-20,29,-5])   
           scale([1,0.75,1])     
           cylinder(h=28,d=DiaTie, center=true,$fn=48); 
         
         translate([-9-20,40,0])    
          scale([1,0.75,1])  
      cylinder(h=24,d=DiaTie, center=true,$fn=48); 
      
   rotate([-10,0,0])
      translate([-9-20,-29,-5])             
        scale([1,0.75,1]) 
       cylinder(h=28,d=DiaTie, center=true,$fn=48); 
           
         translate([-9-20,-40,0])    
         scale([1,0.75,1])   
      cylinder(h=24,d=DiaTie, center=true,$fn=48);    
            
      
      //I fix dremel with 4 * M4
     hull()
     {
        translate([-30,-36.5,-25])
      cylinder(h=20,d=DiaM4, center=true,$fn=24); 
         
         
             translate([-40,-36.5,-25])
      cylinder(h=20,d=DiaM4, center=true,$fn=24); 
      
         
     }      
            
      //II fix dremel with 4 * M4
     hull()
     {
        translate([-30,36.5,-25])
      cylinder(h=20,d=DiaM4, center=true,$fn=24); 
             translate([-40,36.5,-25])
      cylinder(h=20,d=DiaM4, center=true,$fn=24);     
      
     } 
    // III fix dremel with 4 * M4
     hull()
     {
        translate([-6,-36.5,-25])
      cylinder(h=20,d=DiaM4, center=true,$fn=24); 
          // small bridge remains lateral
          // saw it away for easier  M4 insertion         
         translate([-6,-41.0,-28])   
         cube([DiaM4+0.2,4,14],center=true);     
     }      
            
      // IV fix dremel with 4 * M4
     hull()
     {
        translate([-6,36.5,-25])
      cylinder(h=20,d=DiaM4, center=true,$fn=24); 
          // small bridge remains lateral
          // saw it away for easier  M4 insertion         
         translate([-6,41.0,-28])   
         cube([DiaM4+0.2,4,14],center=true);     
     } 
    
  
          // pockets makes removal from baseplate easier:
     
      translate([6.0,32,10 ])  
          cube([4,5,6],center=true);
    
      translate([6,-32,10 ]) 
          cube([4,5,6],center=true);    
        
}
