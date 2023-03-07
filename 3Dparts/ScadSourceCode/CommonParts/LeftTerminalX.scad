/*  Right X terminal */

// 26 11 2018 Stepper 30 mm more lateral so  that his weight
//  is mainly on the lateral bar.

// Print in two parts
// See lines 220  - 235 

PartsToPrint = 0; // change to 1 to render only lower part
                 // change to 2 to render only upper part
RotationAngle =180; // 180 change to 0 for lower part, when PartsToPrint = 1
                    // so no need to rotate in slic3r    


DiaFix = 1.8;
DiaM2=2.4;
DiaM3=3.4; //3.5
DiaTie=4.5;  
DiaM4=4.45;
DiaM5=5.5;    
DiaAxis =8.35;
DiaBar=12.5; 
DiaLM=21.4;     //  LM12UU =21.4
LengthLM=31.2;  //  LM12UU 31.2
DiaStepper=24;

rotate([RotationAngle,0,0])

mirror([0,180,0])

difference()
{

union()
    {        
     // Nema17 external dimension 42.4  
                                  
   //  fix X belt :  
                  
           translate([-1.85,0,9.5])  
      cube([5,62,22],center=true); 
              
           translate([-9.55,21,9.5])  
        cube([7,28,22],center=true);        
          
          // stop for Y belt insertion, solid bottom of 1.0 mm , Y BELT SHOULD PASS FREE OTHER SIDE
      translate([-5,23,5.5])   
    cube([7,24,2],center=true);      
                    
     //     teeth belt        
      for(i=[7.5:2:33.5])
       translate([-3.6,i,9.5])  
        cube([3.2,1,22],center=true); 
      
        for(i=[-33.5:2:-7.5]) 
       translate([-3.6,i,9.5])   
        cube([3.2,1,22],center=true);  
              
        // diameter thick enough avoids Y belt oscillations
          
  translate([-0.30,31,9.5]) 
 cylinder(d=8,h=22,center=true,$fn=24);
          //  fix X belt other side:  
          
  translate([-9.55,-21,9.5])   
     cube([7,28,22],center=true);      
                
          // stop for Y belt insertion, solid bottom of 1.0 mm :
      translate([-5,-23,5.5])   
     cube([7,24,2],center=true);   
        
         // diameter thick enough avoids Y belt oscillations
          
  translate([-0.30,-31,9.5])  
 cylinder(d=8,h=22,center=true,$fn=24);      
 
  //test belt position          
      /*
              translate([-5.25,0,10])   //
      cube([1,150,6],center=true); 
        
              translate([5.25,0,10])   //
      cube([1,150,6],center=true);
      */
              
        
                mirror([180,0,0])       
         translate([55,-34.25,22])                  
          rotate([90,180,0])                
            linear_extrude(1.75)                 
              text("L","Arial",size=10,direction="ltr",halign="top");
       
               mirror([180,0,0])    
         translate([55,-34.25,-22])                  
          rotate([90,180,0])                
            linear_extrude(1.75)                 
              text("L","Arial",size=10,direction="ltr",halign="top");
              
          mirror([180,0,0])  
                  translate([46,4,22])                  
          rotate([90,180,180])    // 180              
            linear_extrude(1.75)                 
              text("L","Arial",size=10,direction="ltr",halign="top");
                           
       
         mirror([180,0,0])
          translate([46,4.0,-34])                  
          rotate([90,180,180])    // 180              
            linear_extrude(1.75)                 
              text("L","Arial",size=10,direction="ltr",halign="top");        
            
            
      
       
// MAIN:

  minkowski()
        {
        translate([-5.5,-0,0])
cube([91,60,9],center=true);   
        cylinder(d=10,h=1,center=true,$fn=24);
        }        
        
       
        // STEPPER  base plate
        
  minkowski()
        {
               
        translate([-3+3,1,-26])   
      cube([92,7,41],center=true); 
        
        
         rotate([0,90,90])     
           cylinder(d=10,h=1,center=true,$fn=24);
        }
                       
         
           //  SOLID X BAR terminal     
  minkowski()
        {        
          translate([-50.5,-15,-10]) 
      cube([ 10,30,105],center=true);  
     
         rotate([0,90,0])     
           cylinder(d=10,h=1,center=true,$fn=24);
        }        
                
    translate([-43.5,-15,-55]) 
    rotate([90,0,90])
   cylinder(d=DiaBar+10,h=25,center=true,$fn=48);
        
    translate([-43.5,-15,35])
    rotate([90,0,90])
   cylinder(d=DiaBar+10,h=25,center=true,$fn=48);  
    
// make printable without support:
 translate([-43.5,-15,-5]) 
         cube([25,11,100],center=true);
                
                  
         // appendix to fix left X wiring :            
           hull()
      {  
        translate([52,1,-47])    
         rotate([0,90,90])   
           cylinder(d=14,h=8,center=true,$fn=36);
       
          translate([45,1,-40])          
         rotate([0,90,90])   
           cylinder(d=14,h=8,center=true,$fn=36);
      }
        
    }   // END UNION
      
      
  // view bar depth:
   
   //  translate([0,0,80]) // 
    //      cube([120,100,100],center=true); 
          
    // appendix to fix left X wiring :            
             
        translate([52,1,-47])   //        
         rotate([0,90,90])    
        scale([1,0.7,1]) 
           cylinder(d=DiaTie,h=9,center=true,$fn=36);
           
// make printable without support:
 translate([-36,-15,-32]) 
    rotate([0,22,0])
         cube([26,12,20],center=true);         
   
   if (PartsToPrint==1)
   {
     translate([0,0,-50]) // 
       cube([120,100,100],center=true); 
   }
    // OR
    //  COMMENT OUT FOR UPMOST PART:
     if (PartsToPrint==2)
   {
       translate([0,0,50]) // 
       cube([120,100,100],center=true); 
   }
        
  
         // X BELT PASS
 
    minkowski()
    {
        translate([-48,-13,-22.5]) 
          cube([20,15.9,30],center=true); 
         rotate([90,0,90])
   cylinder(d=4,h=20,center=true,$fn=48);
        } 
    
    
    // pulley Nema17
       hull()
    {
    translate([22,0,-29])         
        rotate([90,0,0])                  
            cylinder(h=12,d=DiaStepper,center=true,$fn=24); 
       translate([27,0,-29])  //-10               
        rotate([90,0,0])                  
            cylinder(h=12,d=DiaStepper,center=true,$fn=24); 
    }
            
    //   4 *M3 31 mm distance fix Nema17
    
       hull()
    {
    translate([37.5,0,-44.5])             
        rotate([90,0,0])                  
            cylinder(h=12,d=DiaM3,center=true,$fn=24); 
       translate([42.5,0,-44.5])              
        rotate([90,0,0])                  
            cylinder(h=12,d=DiaM3,center=true,$fn=24); 
    }
   
     hull()
    {
    translate([37.5,0,-29+15.5])         
        rotate([90,0,0])                  
            cylinder(h=12,d=DiaM3,center=true,$fn=24); 
       translate([42.5,0,-29+15.5])           
        rotate([90,0,0])                  
            cylinder(h=12,d=DiaM3,center=true,$fn=24); 
    }
        
       hull()
    {
    translate([6.5,0,-13.5])         
        rotate([90,0,0])                  
            cylinder(h=12,d=DiaM3,center=true,$fn=24); 
       translate([11.5,0,-13.5])         
        rotate([90,0,0])                  
            cylinder(h=12,d=DiaM3,center=true,$fn=24); 
    }
      
     hull()
    {
    translate([6.5,0,-29-15.5])   
        rotate([90,0,0])                  
            cylinder(h=12,d=DiaM3,center=true,$fn=24); 
       translate([11.5,0,-29-15.5])      
        rotate([90,0,0])                  
            cylinder(h=12,d=DiaM3,center=true,$fn=24); 
    }   
    
     // Speedup printing:
          
  hull()
    {
       translate([-26,0,-27])                 
        rotate([90,0,0])                  
            cylinder(h=12,d=30,center=true,$fn=48); 
    
    
       translate([-20,0,-27])                 
        rotate([90,0,0])                  
            cylinder(h=12,d=30,center=true,$fn=48); 
    }
       
    
    
    // optional fix belt insertion extra with
   //  self tapping and approximating screws here:
    // OR fix extra with twisted iron wire
    
  translate([-6,28,18.0])  
          rotate([0,90,0])
 cylinder(d=DiaFix,h=15,center=true,$fn=12);
             
  translate([-6,14,18.0])  
          rotate([0,90,0])
 cylinder(d=DiaFix,h=15,center=true,$fn=12);
          
     translate([-6,-28,18.0])   
          rotate([0,90,0])
 cylinder(d=DiaFix,h=15,center=true,$fn=12);
             
  translate([-6,-14,18])  
          rotate([0,90,0])
 cylinder(d=DiaFix,h=15,center=true,$fn=12);
                
   // LM12UU HOLDERS
      
    translate([32,15,13]) 
    rotate([90,0,0])
   cylinder(d=DiaLM,h=LengthLM,center=true,$fn=48);

 // TIES FOR THIS LM12UU
 
   translate([21.3,23,0]) 
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12)  ;
      
     translate([42.7,23,0])  
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12)  ;
      
     translate([21.3,7,0])  
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12)  ;
      
     translate([42.7,7,0])  
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12)  ;
            
      
         translate([46,-7,0]) 
      cube([8,4,12],center=true);
            
         translate([46,-23,0]) 
      cube([8,4,12],center=true);
            
    translate([-32,15,13]) 
    rotate([90,0,0])
   cylinder(d=DiaLM,h=LengthLM,center=true,$fn=48);

 // TYES FOR THIS LM10UU
 
   translate([-42.7,7,0])  
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12)  ;
      
 translate([-21.3,7,0])
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12)  ;
      
 translate([-42.7,23,0])  
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12)  ;
      
 translate([-21.3,23,0])   
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12)  ;
      
// grooves for ties below stepper

translate([32,7,-4.5])  // 2mm deep groove for tye 
 cube([21,4,3],center=true); 

translate([32,23,-4.5])  // 2mm deep groove for tye 
 cube([21,4,3],center=true); 


    translate([32,-15,13]) 
    rotate([90,0,0])
   cylinder(d=DiaLM,h=LengthLM,center=true,$fn=48);

         translate([32+14,15+8,0]) 
      cube([8,4,12],center=true);
            
         translate([32+14,15-8,0]) 
      cube([8,4,12],center=true);
      

 // TIES FOR THIS LM12UU
   translate([21.3,-7,0])  
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12) ;
      
     translate([42.7,-7,0])    
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12)  ;
      
     translate([21.3,-23,0])   
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12)  ;
      
     translate([42.7,-15-8,0])   
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12)  ;
      

    translate([-32,-15,13]) 
    rotate([90,0,0])
   cylinder(d=DiaLM,h=LengthLM,center=true,$fn=48);

 // TIES FOR THIS LM12UU
   translate([-42.7,-23,0])   
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12)  ;
      
 translate([-21.3,-23,0]) 
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12)  ;
      
 translate([-42.7,-7,0])  
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12)  ;
      
 translate([-32+10.7,-15+8,0])    
    scale([0.6,1,1])
    cylinder(h=20,d=DiaTie,center=true,$fn=12)  ;
       
// room for 12 mm shafts !
// LEAVE THIS FOR CHECK: !!
 translate([30,0,14.5]) // z14=margin
    rotate([90,0,0])
   cylinder(d=14.4,h=120,center=true,$fn=48);
      
 translate([-30,0,14.5]) // z14=margin
    rotate([90,0,0])
   cylinder(d=14.4,h=120,center=true,$fn=48);
           
     translate([51.5,8,0])  
cylinder(d=DiaM4,h=12,center=true,$fn=12);
    
       translate([51.5-4,-8,0])  
cylinder(d=DiaM4,h=12,center=true,$fn=12);
    
   
   //  VERTICAL LM HOLDERS
  
    translate([-46,-15,35]) 
    rotate([90,0,90])
   cylinder(d=DiaBar,h=22,center=true,$fn=48);

// MAYBE fix m12 bar with self tapping screws:

for(r=[0:45:180])
    translate([-40,-15,35]) // X -46 = 4 mm bottom
    rotate([r,0,0])
   cylinder(d=DiaFix,h=30,center=true,$fn=48);

//  VERTICAL LM HOLDER
  
    translate([-46,-15,-55]) // 4 mm bottom
    rotate([90,0,90])
   cylinder(d=DiaBar,h=22,center=true,$fn=48);
      
     //  MAYBE,OPTION fix X bar 12 bar with self tapping screws:

for(r=[0:45:180])
    translate([-40,-15,-55]) // X -46 = 4 mm bottom
    rotate([r,0,0])
   cylinder(d=DiaFix,h=30,center=true,$fn=48);
     
// air escape :
 translate([-40,-15,35]) // X -46 = 4 mm bottom
    rotate([0,90,0])
   cylinder(d=DiaFix,h=30,center=true,$fn=48);

 translate([-40,-15,-55]) // X -46 = 4 mm bottom
    rotate([0,90,0])
   cylinder(d=DiaFix,h=30,center=true,$fn=48);

      
    hull()
     {   
    translate([-51,0,-22])                 
        rotate([90,0,0])                  
            cylinder(h=12,d=DiaM2,center=true,$fn=24); 
    
        translate([-47,0,-22])                 
        rotate([90,0,0])                  
            cylinder(h=12,d=DiaM2,center=true,$fn=24); 
    } 
    
        hull()
     {   
    translate([-51,0,-12])                 
        rotate([90,0,0])                  
            cylinder(h=12,d=DiaM2,center=true,$fn=24); 
    
        translate([-47,0,-12])                 
        rotate([90,0,0])                  
            cylinder(h=12,d=DiaM2,center=true,$fn=24); 
    }   
      
// pockets for easier removal from 3D printer: 
     translate([-54,26,0]) //
           cube([10,8,2],center=true);
      
      translate([-54,-28,0]) //
           cube([10,8,2],center=true);
         
    translate([-5,32,0]) //
           cube([8,8,2],center=true);
      
      translate([-5,-32,0]) //
           cube([8,8,2],center=true);
}
