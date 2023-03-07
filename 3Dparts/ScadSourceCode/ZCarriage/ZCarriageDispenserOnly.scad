// Z carriage for solder paste dispenser, the front part is different
// upper support LM 6 shafts above M4 hex nut


// PRINT IN 3 PARTS :

PartToPrint=0;        // CHANGE TO 1 =backside  2= mid part  3 = front part
                      // Only front part differs for other activities.
                      //  part 1 and 2 stays the same even for all modules Z99 and  Z99PP
rotationAngle = 0;  // make 90 for part 3, so no need to rotate in Slic3r
               


DiaFix=1.9;
DiaM2=2.5;
DiaM3=3.55;
DiaM4=4.4;
DiaM4Plus=5.0;
DiaTye=4.5;  

DiaM5=5.5;    

DiaM6=6.45;

DiaLM=21.4;        // LM10 =19.8  LM12 =21.4
LengthLM=31.2;    //  LM10 = 30 distance  32 *2

rotate([rotationAngle,0,0])


difference()
{
union()
    {                 
// MAIN:
      //  VERTICAL base plate  85 * 150 *10
       //  8 mm thick
  minkowski()
        {        
          translate([0,0,-3+2]) //0.5 = 3.5 mm deep 
      cube([ 75+12,7,142.0-6+4],center=true);  // 68
     
         rotate([0,90,90])     
           cylinder(d=10,h=1,center=true,$fn=24);
        }  
            
  // APPENDED FRONTAL VERTICAL base plate  85 * 150 *10       
    //   OFFSET 31
    //   also TEMP?   10+2 mm thick 
        
  minkowski()
        {        
      
          translate([0,31+0.5,-3+2]) //0.5 = 3.5 mm deep 
      cube([ 75+12,9+1,142-6+4],center=true);  // 68
           
                
         rotate([0,90,90])     
           cylinder(d=10,h=1,center=true,$fn=24);
        }        
        
                        
  // Nema 17 lifter on top 
              
  minkowski()
        {   
   translate([0,31,-82+6-6]) //
     cube([40,44,6],center=true);  // WAS 38 37 =dispenser only         
           cylinder(d=5,h=1,center=true,$fn=24);
        }
                
        
     // fusion with nema 17
                 
  minkowski()
        {  
               translate([0,31.0,-71]) //
      cube([ 58,8,18],center=true);
         rotate([0,90,0])   
        cylinder(d=5,h=1,center=true,$fn=24);
        }          
        
        
        //  Z switch attachment:
        
       minkowski()
        {    
         translate([-20.75,48.0,-72]) //-27
      cube([ 2.0,16,10],center=true);  // 16   
    rotate([0,90,0])        
        cylinder(d=10,h=1.5,center=true,$fn=24); // h=2
        }                                            
   
  // BELT ATTACHMENT     
  
            translate([35,-10,-18])   //
      cube([24,25.5,6],center=true); //21.5
        
       
          translate([-35,-10,-18])   //
      cube([24,25.5,6],center=true); //21.5
              
                
                 
       // midline-2mm so that opposite belt could pass free 
          
          translate([0,-4,-13])   //
      cube([97,3,6],center=true); //2
     
        
  translate([-40.5-6,-10,-17.75])  
         rotate([0,90,90])
 cylinder(d=6,h=25.5,center=true,$fn=24); 
                   
  translate([46.5,-10,-17.75])  
         rotate([0,90,90])
 cylinder(d=6,h=25.5,center=true,$fn=24); 
          
            for(i=[-47.5:2:-23.5]) //8
            { 
       translate([i,-10,-15.5])   //-4.4
        cube([1,25.5,3.2],center=true); //  3                
            }
               for(i=[47.5:-2:23.5]) //8
            { 
       translate([i,-10,-15.5])   //
        cube([1,25.5,3.2],center=true);  //3               
            }
                    
            translate([-35.75,-10,-9.55])   //-9.95
        cube([25.5,25.5,7],center=true);    
                     
            translate([35.75,-10,-9.55])   //-9.95
        cube([25.5,25.5,7],center=true);    
        
            
  // comment out to test belt position
          /*
              translate([0,-10,-19-5.25])   //
      cube([150,6,1],center=true); 
        
              translate([0,-10,-19+5.25])   //
      cube([150,6,1],center=true);                
              */                              
            
    // 4 blocks for rail conductors :
         
    
       minkowski()
         {         
         translate([38,50,-79])    //-37  
         cube([17,46,11.0]  ,center=true);  //
          rotate([0,180,0])
             cylinder(h=1,d=2,center=true,$fn=24);
         } 
           
              // this column 1 mm smaller for Z switch:
       minkowski()
         {         
         translate([-38.5,50,-79])  //-37    
         cube([16,46,11.0]  ,center=true);  //
          rotate([0,180,0])
             cylinder(h=1,d=2,center=true,$fn=24);
         } 
           
         
         
         
       minkowski()
         {         
         translate([39,42+7+1,55])      
         cube([17,46,11.0]  ,center=true);  //
          rotate([0,180,0])
             cylinder(h=1,d=2,center=true,$fn=24);
         }  
         
            
        
       minkowski()
         {         
         translate([-39,42+7+1,55])      
         cube([17,46,11.0]  ,center=true);  //
          rotate([0,180,0])
             cylinder(h=1,d=2,center=true,$fn=24);
         } 
                  
         
         translate([-26,36,64])                  //58
               rotate([90,180,180])                  
                linear_extrude(2.0)                 
                  text("DISPENS","Arial",size=9,direction="ltr",halign="top");
           
    }   // END UNION
                
            
    
  // COMMENT OUT THE 2 LINES BELOW TO RENDER BACKMOST PART :
    if (PartToPrint ==1 )
    {
   translate([0,45,0])          
   cube([200,80,200]  ,center=true);  
    }
        
      
   // COMMENT OUT THE 4 LINES BELOW TO RENDER MIDMOST PART :
       if (PartToPrint ==2 )
    {
    translate([0,-35,0])      
      cube([200,80,200]  ,center=true);  //
    
     translate([0,31+40,0])      
       cube([200,80,200]  ,center=true);  //                
    }
    
        
  // COMMENT OUT THE SINGLE LINE BELOW TO RENDER FRONTMOST PART :
     if (PartToPrint ==3 )
    {
   cube([200,62,200]  ,center=true);  
    }    
        
    
    
    
       // maybe fix belt with approximating screws
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
  //25 for acme
  
   translate([0,31,-50])   
     cylinder(h=124,d=22,center=true,$fn=24);         
       
             /// didn't love to add this two lines, but otherwise it printed often not nice:        
          translate([0,31,-82])   
     cube([18,22,10],center=true);        
              
              
        // spare out in front part:        
   translate([0,40,-4.5])   //               
      cube([46,46,102],center=true);    //                          
     
    
        // extra spare out in backplate for coupler M5M8:
        
   translate([0,11,-60])   
     cylinder(h=22,d=22.0,center=true,$fn=48);   //26      
          
             
      // spare out in BACK part:
        
   translate([0,0,6.5])   //               
      cube([34+12,12,57],center=true);    //             
         
         
          // extra spare out in BACK part makes insertion and operation 100 mm trapezoidal screw easier:
        
   translate([0,0,39])   //               
      cube([22,46,10],center=true);    //30
               
   
 
         
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
  
 translate([20+8,-11,-45]) // -11 = 4mm bottom
    rotate([90,0,90])
   cylinder(d=DiaLM,h=LengthLM+0.5,center=true,$fn=48);

translate([-20-8,-11,-45]) // -11 = 4mm bottom
    rotate([90,0,90])
   cylinder(d=DiaLM,h=LengthLM+0.5,center=true,$fn=48);


  // TYES FOR  UPPER LM12UU
  
   translate([-25-3-8,0,-45-10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
    
     translate([-25+16-3-8,0, -45 +10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
    
      translate([-25+16-3-8,0,-45-10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
    
    
      translate([-25-3-8,0,-45+10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
      
      translate([25+3+8,0,-45-10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
   
      translate([25-16+3+8,0,-45-10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
        
      translate([25+3+8,0,-45+10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
        
      translate([25-16+3+8,0,-45+10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
 
   

// LOWER HOLDERS:
 translate([22+8,-11,45]) // -11 = 4mm bottom
    rotate([90,0,90])
   cylinder(d=DiaLM,h=LengthLM+0.5,center=true,$fn=48);


 translate([-22-8,-11,45]) // -11 = 4mm bottom
    rotate([90,0,90])
   cylinder(d=DiaLM,h=LengthLM+0.5,center=true,$fn=48);



  // TYES FOR  LOWER LM12UU
  
   translate([-27-3-6,0,45-10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
    
     translate([-27+16-3-6,0, 45 +10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
    
      translate([-27+16-3-6,0,45-10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
    
    
      translate([-27-3-6,0,45+10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
      
      translate([27+3+6,0,45-10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
   
      translate([27-16+3+6,0,45-10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
        
      translate([27+3+6,0,45+10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
        
      translate([27-16+3+6,0,45+10.7])  //37.75  
   rotate([90,0,0])
    scale([1,0.6,1])
    cylinder(h=12,d=DiaTye,center=true,$fn=12)  ;
 


           
    // VERTICAL AXIS: 
        
         translate([39,66,-15])   // 18= 2.5 mm bottom
     cylinder(h=148,d=DiaM6,center=true,$fn=48); 
    
        
              translate([-39,66,-15])   
     cylinder(h=148,d=DiaM6,center=true,$fn=48); 
     
     
     
    // h 125 = 3 mm bottom for shaft
    // air escape
    
          translate([-39,48+15+3,58])   
     cylinder(h=12,d=DiaFix,center=true,$fn=24); 
                 
         
          translate([39,48+15+3,58])   
     cylinder(h=12,d=DiaFix,center=true,$fn=24); 
     
   //  FIX BOTH PARTS TOGETHER
      
     translate([33+6,20,-68])  //
   rotate([90,0,0])   
    cylinder(h=50,d=DiaM4,center=true,$fn=24)  ;
      
     translate([-33-6,20,-68])  //
   rotate([90,0,0])   
    cylinder(h=50,d=DiaM4,center=true,$fn=24)  ;
      
     translate([33+6,20,-68])  //
   rotate([90,0,0])   
    cylinder(h=50,d=DiaM4,center=true,$fn=24)  ;
   
     translate([-33-6,20,1])  //
   rotate([90,0,0])   
    cylinder(h=50,d=DiaM4,center=true,$fn=24)  ;
      
     translate([33+6,20,1])  //
   rotate([90,0,0])   
    cylinder(h=50,d=DiaM4,center=true,$fn=24)  ;
            
     translate([-33-6,20,66])  
   rotate([90,0,0])   
    cylinder(h=50,d=DiaM4,center=true,$fn=24)  ;
      
     translate([33+6,20,66])  // 
   rotate([90,0,0])   
    cylinder(h=50,d=DiaM4,center=true,$fn=24)  ;
        
        
        //  Somewhat larger opening dorsal, otherwise insertion is sometimes difficult
      
     translate([33+6,0,-68])  //
   rotate([90,0,0])   
    cylinder(h=10,d=DiaM4Plus,center=true,$fn=24)  ;
      
     translate([-33-6,0,-68])  //
   rotate([90,0,0])   
    cylinder(h=10,d=DiaM4Plus,center=true,$fn=24)  ;
      
     translate([33+6,0,-68])  //
   rotate([90,0,0])   
    cylinder(h=10,d=DiaM4Plus,center=true,$fn=24)  ;
   
     translate([-33-6,0,1])  //
   rotate([90,0,0])   
    cylinder(h=10,d=DiaM4Plus,center=true,$fn=24)  ;
      
     translate([33+6,0,1])  //
   rotate([90,0,0])   
    cylinder(h=10,d=DiaM4Plus,center=true,$fn=24)  ;
            
     translate([-33-6,0,66])  
   rotate([90,0,0])   
    cylinder(h=10,d=DiaM4Plus,center=true,$fn=24)  ;
      
     translate([33+6,0,66])  // 
   rotate([90,0,0])   
    cylinder(h=10,d=DiaM4Plus,center=true,$fn=24)  ;
        
                
    // 3 mm deep room for head M4:             
               
     translate([-33-6,38,-68])  //
   rotate([90,0,0])   
    cylinder(h=6,d=8.4,center=true,$fn=6)  ;
      
      
      
      
     translate([33+6,38,-68])  //
   rotate([90,0,0])   
    cylinder(h=6,d=8.4,center=true,$fn=6)  ;
   
     translate([-33-6,38,1])  //
   rotate([90,0,0])   
    cylinder(h=6,d=8.4,center=true,$fn=6)  ;
      
     translate([33+6,38,1])  //
   rotate([90,0,0])   
    cylinder(h=6,d=8.4,center=true,$fn=6)  ;
      
     translate([-33-6,38,66])  
   rotate([90,0,0])   
    cylinder(h=6,d=8.4,center=true,$fn=6)  ;
      
     translate([33+6,38,66])  // 
   rotate([90,0,0])   
    cylinder(h=6,d=8.4,center=true,$fn=6)  ; 
    
     
      
       // MAYBE FIX UPMOST M6 bars      
        
                 translate([0,66,-80])  //
   rotate([0,90,0]) 
    cylinder(h=110,d=DiaFix,center=true,$fn=8)  ;    
                    
    
        translate([39,70,-80])  //
   rotate([90,0,0])   
    cylinder(h=24,d=DiaFix,center=true,$fn=8)  ;
    
        translate([-39,70,-80])  //
   rotate([90,0,0])   
    cylinder(h=24,d=DiaFix,center=true,$fn=8)  ;
     
         // MAYBE FIX DOWNMOST M6 bars      
         
          translate([0,48.5+15+2.5,54])  //
   rotate([0,90,0]) 
    cylinder(h=110,d=DiaFix,center=true,$fn=8)  ;
                     
        translate([39,70,54])  //
   rotate([90,0,0])   
    cylinder(h=24,d=DiaFix,center=true,$fn=8)  ;
        
        translate([-39,70,54])  //
   rotate([90,0,0])   
    cylinder(h=24,d=DiaFix,center=true,$fn=8)  ;
           
  
  // pockets for easier removal: 
  
     translate([42+6,4,-15]) //
           cube([8,2.1,10],center=true);
      
        translate([-42-6,4,-15]) //
           cube([8,2.1,10],center=true);   
    
 // pockets for easier removal front part: 
  
     translate([50,31.0,-65]) //
           cube([8,2.4,8],center=true);
    
     translate([-50,31.0,-65]) //
           cube([8,2.4,8],center=true);
        
    // on top some room for the stepper SCREWS and holder:
        
             translate([0,0,-76]) //              
      cube([ 54,9,14],center=true);  // 
    
  
 //  FIX Z SWITCH     
        hull()
        {
                 translate([0,54.5,-67])  //
   rotate([0,90,0]) 
    cylinder(h=50,d=DiaM2,center=true,$fn=24);    
    
                 translate([0,54.5,-72])  //
   rotate([0,90,0]) 
    cylinder(h=50,d=DiaM2,center=true,$fn=24);    
        }
     
          hull()
        {
        
                 translate([0,44.5,-67])  //
   rotate([0,90,0]) 
    cylinder(h=50,d=DiaM2,center=true,$fn=24);    
    
                 translate([0,44.5,-72])  //
   rotate([0,90,0]) 
    cylinder(h=50,d=DiaM2,center=true,$fn=24);    
    
        }
      
        
        





  
      
}
