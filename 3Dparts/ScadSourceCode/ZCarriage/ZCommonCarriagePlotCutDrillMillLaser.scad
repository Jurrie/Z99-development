// PRINT IN THREE PARTS:
// This Z carriage is used by plotter,cutter, drill, mill and laser with Z motor

PartToPrint=0; // CHANGE TO 1 =backside  2= mid part  3 = front part
                // Only front part differs for other activities.
               //  part 1 and 2 stays the same even for all modules Z99 and  Z99PP
rotationAngle = -90; //-90 make 90 for part 3, so no need to rotate in Slic3r
       
               
// Could serve for all Z carriages, except dispenser and laser without Z motor



DiaFix=1.9;
DiaM2=2.5;
DiaM3=3.5;
DiaM4=4.4;
DiaM4Plus=5.0; // makes insertion of M4 easy
DiaTie=4.5;  

DiaM5=5.5;    

DiaM6=6.45;
DiaLM=21.4;        //  LM12
LengthLM=31.2;    //  LM12


rotate([rotationAngle ,0,0])

difference()
{
union()
    {                 
// MAIN:
      //  VERTICAL base plate  85 * 150 *10
       //  8 mm thick
        
  minkowski()
        {        
          translate([0,0,-3+2]) 
      cube([ 75+12,7,140.0],center=true); 
     
         rotate([0,90,90])     
           cylinder(d=10,h=1,center=true,$fn=24);
        }  
        
            
  // APPENDED FRONTAL VERTICAL base plate  85 * 150 *10       
    //   OFFSET 32
   
        
  minkowski()
        {        
          translate([0,32,-1]) 
      cube([ 75+12,11,140],center=true);  
     
         rotate([0,90,90])     
           cylinder(d=10,h=1,center=true,$fn=24);
        }        
        
                        
  // Nema 17 lifter on top :
              
  minkowski()
        {   
            translate([0,31,-82.5]) 
      cube([ 40,40,5],center=true);     
           cylinder(d=5,h=1,center=true,$fn=24);
        }
                
    //  Z switch attachment:
               
       minkowski()
        {    
         translate([-20.75,47.0,-70]) 
      cube([ 2.0,15,20],center=true);    
    rotate([0,90,0])        
        cylinder(d=10,h=1.5,center=true,$fn=24); 
        }                         
               
//  check room switch :
    /* 
    translate([-26,47.0,-65]) //-27
      cube([ 6.4,20,20],center=true);  //    
  */
            
     // fusion for Z nema 17
               
  minkowski()
        {  
               translate([0,31,-68]) 
      cube([ 44,9,24],center=true);
         rotate([0,90,0])   
        cylinder(d=5,h=1,center=true,$fn=24);
        }        
        
  // BELT ATTACHMENT     
  
            translate([35,-10,-19])  
      cube([24,25.5,8],center=true);        
       
          translate([-35,-10,-19])   
      cube([24,25.5,8],center=true); 
                
                 
       // midline-2mm so that opposite belt could pass free 
          
          translate([0,-4,-13])  
      cube([97,3,6],center=true); 
             
  translate([-40.5-6,-10,-18.75])  
         rotate([0,90,90])
 cylinder(d=8,h=25.5,center=true,$fn=24); 
                   
  translate([46.5,-10,-18.75])  
         rotate([0,90,90])
 cylinder(d=8,h=25.5,center=true,$fn=24); 
          
            for(i=[-47.5:2:-23.5]) 
            { 
       translate([i,-10,-15.5])   
        cube([1,25.5,3.2],center=true);                
            }
               for(i=[47.5:-2:23.5]) 
            { 
       translate([i,-10,-15.5])  
        cube([1,25.5,3.2],center=true);         
            }
                    
            translate([-35.75,-10,-9.55])  
        cube([25.5,25.5,7],center=true);    
                     
            translate([35.75,-10,-9.55])  
        cube([25.5,25.5,7],center=true);    
        
            
  // comment out to test belt position
          /*
              translate([0,-10,-19-5.25])   //
      cube([150,6,1],center=true); 
        
              translate([0,-10,-19+5.25])   //
      cube([150,6,1],center=true);                
              */        
            
    // 4 blocks for rail conductors and microscope fixation:
            
       minkowski()
         {         
         translate([39.5,47,-55.5])   
         cube([16,40,12.0]  ,center=true);  
          rotate([0,180,0])
             cylinder(h=1,d=2,center=true,$fn=24);
         }
         
       minkowski()
         {         
         translate([-39.5,47,-55.5])  
         cube([16,40,12.0]  ,center=true); 
          rotate([0,180,0])
             cylinder(h=1,d=2,center=true,$fn=24);
         }                           
         
       minkowski()
         {         
         translate([39,47,53.5])      
         cube([17,40,13.0]  ,center=true);  
          rotate([0,180,0])
             cylinder(h=1,d=2,center=true,$fn=24);
         }           
                    
       minkowski()
         {         
         translate([-39,47,53.5])      
         cube([17,40,13.0]  ,center=true);  
          rotate([0,180,0])
             cylinder(h=1,d=2,center=true,$fn=24);
         } 
                  
       // was mill and drill  
         translate([-28,37,58])                  
               rotate([90,180,180])                  
                linear_extrude(2.0)                 
                  text("PLOT-CUT","Arial",size=8,direction="ltr",halign="top");
                  
         translate([-28,37,70])                  
               rotate([90,180,180])                  
                linear_extrude(2.0)                 
                  text("DRILL-MILL","Arial",size=8,direction="ltr",halign="top");
         
     
    }   // END UNION
                             
    
  // COMMENT OUT THE 2 LINES BELOW TO RENDER ONLY BACKMOST PART :
    if (PartToPrint ==1)
    {
  translate([0,45,0])          
   cube([200,80,200]  ,center=true);  //
    }
   // COMMENT OUT THE 4 LINES BELOW TO RENDER MIDMOST PART :
      
 if (PartToPrint ==2)
    {    
    translate([0,-35,0])      
      cube([200,80,200]  ,center=true);  //
    
     translate([0,31+40,0])      
       cube([200,80,200]  ,center=true);  //                
    }
          
  // COMMENT OUT THE SINGLE LINE BELOW TO RENDER FRONTMOST PART :
     if (PartToPrint ==3)
    {
    cube([200,62,200]  ,center=true);  //
    }       
    
    
   
      // maybe fix belt extra with self tapping screws
    // or twisted iron wire
        
     translate([36+6,-19.5,-15])  
            cylinder(h=24,d=DiaFix,center=true,$fn=24); 
      translate([26+6,-19.5,-15])  
            cylinder(h=24,d=DiaFix,center=true,$fn=24); 
    
      translate([-36-6,-19.5,-15])  
            cylinder(h=24,d=DiaFix,center=true,$fn=24); 
      translate([-26-6,-19.5,-15])  
            cylinder(h=24,d=DiaFix,center=true,$fn=24); 
    
    
  // Z NEMA 17 FIX  
  //  room coupler
  
  translate([0,31,-68])   
     cylinder(h=36,d=22,center=true,$fn=48);        
                
        /// didn't love to add this two lines, but otherwise it printed often not nice:        
          translate([0,31,-82])   
     cube([18,22,10],center=true);        
  
        
        // extra spare out in backplate for coupler M5M8:
        
   translate([0,11,-60])   
     cylinder(h=22,d=22.0,center=true,$fn=48);     
        
        
        // spare out in front part:
        
   translate([0,40,-4-7.5+2+6])   //               
      cube([46,46,96+4+2],center=true);    //34
                         
             
      // spare out in BACK part:
        
   translate([0,0,6.0])               
      cube([46,12,58],center=true);               
       
         
     // extra spare out in BACK part makes insertion and operation 100 mm trapezoidal screw easier:
        
   translate([0,0,39])                 
      cube([20,46,10],center=true);   
         
               
     // extra spare out in BACK part makes higher lifting possible:
        
   translate([0,0,-15])   // 
    rotate([90,0,0])
    scale([1,0.8,1])
    cylinder(h=24,d=46,center=true,$fn=48);    
      
         
  // fix Nema17 lifter
              
   translate([15.5,31+15.5,-78])   
     cylinder(h=24,d=DiaM3,center=true,$fn=24); 
         
   translate([15.5,31-15.5,-78])   
     cylinder(h=24,d=DiaM3,center=true,$fn=24); 
      
   translate([-15.5,31+15.5,-78])   
     cylinder(h=24,d=DiaM3,center=true,$fn=24); 
    
        translate([-15.5,31-15.5,-78])   
     cylinder(h=24,d=DiaM3,center=true,$fn=24);
   
   //   LM HOLDERS
  
 translate([20+8,-11,-45])
    rotate([90,0,90])
   cylinder(d=DiaLM,h=LengthLM+0.5,center=true,$fn=48);

translate([-20-8,-11,-45]) 
    rotate([90,0,90])
   cylinder(d=DiaLM,h=LengthLM+0.5,center=true,$fn=48);


  // TIES FOR  UPPER LM12UU
  
   translate([-25-3-8,0,-45-10.7])  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12) ;
    
     translate([-25+16-3-8,0, -45 +10.7])  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12)  ;
    
      translate([-25+16-3-8,0,-45-10.7])   
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12)  ;
    
    
      translate([-25-3-8,0,-45+10.7]) 
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12)  ;
      
      translate([25+3+8,0,-45-10.7])    
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12)  ;
   
      translate([25-16+3+8,0,-45-10.7])   
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12)  ;
        
      translate([25+3+8,0,-45+10.7]) 
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12)  ;
        
      translate([25-16+3+8,0,-45+10.7])  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12)  ;
    

// LOWER HOLDERS:
 translate([22+8,-11,45]) // -11 = 4mm bottom
    rotate([90,0,90])
   cylinder(d=DiaLM,h=LengthLM+0.5,center=true,$fn=48);


 translate([-22-8,-11,45]) // -11 = 4mm bottom
    rotate([90,0,90])
   cylinder(d=DiaLM,h=LengthLM+0.5,center=true,$fn=48);


  // TIES FOR  LOWER LM12UU
  
   translate([-27-3-6,0,45-10.7])  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12)  ;
    
     translate([-27+16-3-6,0, 45 +10.7])  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12)  ;
    
      translate([-27+16-3-6,0,45-10.7]) 
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12)  ;
    
    
      translate([-27-3-6,0,45+10.7]) 
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12)  ;
      
      translate([27+3+6,0,45-10.7])  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12)  ;
   
      translate([27-16+3+6,0,45-10.7])  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12)  ;
        
      translate([27+3+6,0,45+10.7]) 
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12)  ;
        
      translate([27-16+3+6,0,45+10.7])  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTie,center=true,$fn=12)  ;
            
    // VERTICAL AXIS: 
        
         translate([39,59,-5])   
     cylinder(h=146,d=DiaM6,center=true,$fn=48);  
     // Change 146 to 126 for non penetration on bottom holder !
     // without penetration could not serve to attach dust suction for mill and drill
            
              translate([-39,59,-5])   
     cylinder(h=146,d=DiaM6,center=true,$fn=48); 
     // Change 146 to 126 for non penetration on bottom holder !
     // without penetration could not serve to attach dust suction for mill and drill
             
    // air escape when h=126
    
          translate([-39,59,55])   
     cylinder(h=12,d=DiaFix,center=true,$fn=24);                  
         
          translate([39,59,55])   
     cylinder(h=12,d=DiaFix,center=true,$fn=24); 
     
      //  FIX BOTH PARTS TOGETHER
      
     translate([39,30,-68])  
   rotate([90,0,0])   
    cylinder(h=16,d=DiaM4,center=true,$fn=24)  ;
      
     translate([-39,30,-68]) 
   rotate([90,0,0])   
    cylinder(h=16,d=DiaM4,center=true,$fn=24)  ;
   
     translate([-39,30,1])  
   rotate([90,0,0])   
    cylinder(h=16,d=DiaM4,center=true,$fn=24)  ;
      
     translate([39,30,1])  
   rotate([90,0,0])   
    cylinder(h=16,d=DiaM4,center=true,$fn=24)  ;
            
     translate([-39,30,66])  
   rotate([90,0,0])   
    cylinder(h=16,d=DiaM4,center=true,$fn=24)  ;
      
     translate([39,30,66])  
   rotate([90,0,0])   
    cylinder(h=16,d=DiaM4,center=true,$fn=24)  ;       
                
 //  Somewhat larger opening dorsal, otherwise insertion front+mid Z carriage is sometimes difficult
      
     translate([33+6,0,-68])  
   rotate([90,0,0])   
    cylinder(h=10,d=DiaM4Plus,center=true,$fn=24)  ;
      
     translate([-33-6,0,-68]) 
   rotate([90,0,0])   
    cylinder(h=10,d=DiaM4Plus,center=true,$fn=24)  ;
      
     translate([33+6,0,-68])  
   rotate([90,0,0])   
    cylinder(h=10,d=DiaM4Plus,center=true,$fn=24)  ;
   
     translate([-33-6,0,1]) 
   rotate([90,0,0])   
    cylinder(h=10,d=DiaM4Plus,center=true,$fn=24)  ;
      
     translate([33+6,0,1]) 
   rotate([90,0,0])   
    cylinder(h=10,d=DiaM4Plus,center=true,$fn=24)  ;
            
     translate([-33-6,0,66])  
   rotate([90,0,0])   
    cylinder(h=10,d=DiaM4Plus,center=true,$fn=24)  ;
      
     translate([33+6,0,66]) 
   rotate([90,0,0])   
    cylinder(h=10,d=DiaM4Plus,center=true,$fn=24)  ;                      
                
  // 3 mm deep room for head M4:             
               
     translate([-33-6,38,-68]) 
   rotate([90,0,0])   
    cylinder(h=6,d=8.4,center=true,$fn=6)  ;
            
     translate([33+6,38,-68]) 
   rotate([90,0,0])   
    cylinder(h=6,d=8.4,center=true,$fn=6)  ;
   
     translate([-33-6,38,1]) 
   rotate([90,0,0])   
    cylinder(h=6,d=8.4,center=true,$fn=6)  ;
      
     translate([33+6,38,1]) 
   rotate([90,0,0])   
    cylinder(h=6,d=8.4,center=true,$fn=6)  ;
      
     translate([-33-6,38,66])  
   rotate([90,0,0])   
    cylinder(h=6,d=8.4,center=true,$fn=6)  ;
      
     translate([33+6,38,66]) 
   rotate([90,0,0])   
    cylinder(h=6,d=8.4,center=true,$fn=6)  ; 
        
      //  FIX Z SWITCH     
        hull()
        {
                 translate([0,54.5,-60])  
   rotate([0,90,0]) 
    cylinder(h=50,d=DiaM2,center=true,$fn=24);    
    
                 translate([0,54.5,-70]) 
   rotate([0,90,0]) 
    cylinder(h=50,d=DiaM2,center=true,$fn=24);    
        }
     
          hull()
        {        
                 translate([0,44.5,-60]) 
   rotate([0,90,0]) 
    cylinder(h=50,d=DiaM2,center=true,$fn=24);    
    
                 translate([0,44.5,-70]) 
   rotate([0,90,0]) 
    cylinder(h=50,d=DiaM2,center=true,$fn=24);    
    
        }
      
       // MAYBE FIX UPMOST M6 bars      
        
                 translate([0,59.5,-56])  
   rotate([0,90,0]) 
    cylinder(h=110,d=DiaFix,center=true,$fn=8)  ;    
    
                 translate([0,59.5,52])  
   rotate([0,90,0]) 
    cylinder(h=110,d=DiaFix,center=true,$fn=8)  ;
    
    
        translate([39,70,-56]) 
   rotate([90,0,0])   
    cylinder(h=24,d=DiaFix,center=true,$fn=8)  ;
    
        translate([-39,70,-56]) 
   rotate([90,0,0])   
    cylinder(h=24,d=DiaFix,center=true,$fn=8)  ;
     
         // MAYBE FIX DOWNMOST M6 bars      
         
        translate([39,70,52]) 
   rotate([90,0,0])   
    cylinder(h=24,d=DiaFix,center=true,$fn=8)  ;
   
     
        translate([-39,70,52])
   rotate([90,0,0])   
    cylinder(h=24,d=DiaFix,center=true,$fn=8)  ;
           
  
  // pockets for easier removal: 
  
     translate([42+6,4,-26])
           cube([8,2.1,10],center=true);
      
        translate([-42-6,4,-26]) 
           cube([8,2.1,10],center=true);   
    
 // pockets for easier removal front part: 
  
     translate([42+6,31.0,-15]) 
           cube([8,2.4,10],center=true);
    
     translate([-42-6,31.0,-15])
           cube([8,2.4,10],center=true);
        
    // on top some room for the stepper SCREWS and holder:
        
             translate([0,0,-76])              
      cube([ 54,9,14],center=true);   
      
}
