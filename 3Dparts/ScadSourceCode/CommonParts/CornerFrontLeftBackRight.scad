// Print two parts,
//   1 for front left corner
//   + 1 diagonal opposition for back right
          
 // print with  vertical shaft insertions on bottom !
    
DiaAir = 1.9; // air outlet M12 bars
DiaFix = 1.9; // OPTIONAL FIX X bars
DiaM2 = 2.4;      // fix X corner switch
DiaM3 = 3.4;      // 3.5
DiaM4= 4.5; 
DiaM5 = 5.5;
DiaM7 = 7;  // alternative to fix on MDF
DiaM8=8.6;  // FIX ON GROUNDPLANE
DiaBar= 12.4;     // 12.4 for 12 mm  X bars
                  
DiaStepper=26; //minimal 23 


color("lime")

rotate([180,0,0]) // prints convenable

  difference() { 
      
     union() {        
         
     //vertical base plate           
     // print with this side on bottom 
         
           minkowski() 
         {                 
          translate([8.25,0,6.5])   // [5.5,0,1]
    cube([ 76.5,84,7],center=true);  //  73
             
     cylinder(h=1,d=6,center=true); //* (h=3,d=5
         }          
                  
     // bottom          
          
       translate([46.5,0,-22])    //  
  cube([8,90,65],center = true);  //* 94 61 6    
         
                      
         
    //  STEPPER base plate with central hole and 4 fixation holes 
         
         minkowski() {         
   translate([-10,0,-22.0])        // 10
       cube([ 3,51,60],center=true);  //
        rotate([0,90,0])      
          cylinder(h=3,d=5,center=true); //
         }   
         
    //  SIDE WALLS 
         
         difference()
         {
           translate([18,-25,-22])  //-23.5    
       cube([ 60,6,65],center=true); 
           
                     
               hull()
             {
                   translate([14,-24,-24]) // 18   
  rotate([90,0,0])  
    cylinder(h=10,d=34,center=true,$fn=96)  ; 
                 
            translate([28,-24,-24]) // 18   
  rotate([90,0,0])  
    cylinder(h=10,d=34,center=true,$fn=96)  ; 
                
                 }              
         }
                  
            difference()
         {         
        translate([18,25,-22])    //-23.5
         cube([ 60,6,65],center=true); 
             
             hull()
             {
                   translate([14,24,-24]) // 18   
  rotate([90,0,0])  
    cylinder(h=10,d=34,center=true,$fn=96)  ; 
                 
            translate([28,24,-24]) // 18   
  rotate([90,0,0])  
    cylinder(h=10,d=34,center=true,$fn=96)  ;                 
                 }             
         } 
             
         
mirror([0,180,0]) // without mirror the "R" is printed inverse
       
      translate([-17,23,-1.75-3-6])    // 30 -42 -5.5              
               rotate([0,0,90])                  
                linear_extrude(2)                 
        text("BR","Arial",size=10,direction="ltr",halign="top"); // BR= back right
          
        // 
mirror([0,180,0]) // without mirror printing was inverse
       
      translate([38,40,1.75])    // 30 -42 -5.5              
               rotate([0,0,180])                  
                linear_extrude(2)                 
        text("Front Left","Arial",size=7,direction="ltr",halign="top");
            
         
         mirror([0,180,0]) // without mirror printing was inverse
       
      translate([38,-34,1.75])    // 30 -42 -5.5              
               rotate([0,0,180])                  
                linear_extrude(2)                 
        text("Back Right","Arial",size=7,direction="ltr",halign="top");
          
         
         
mirror([0,180,0]) // without mirror the "R" is printed inverse    
      translate([6-20,-40,-1.75-3-6])    // 30 -42 -5.5              
               rotate([0,0,90])                  
                linear_extrude(2)                 
        text("FL","Arial",size=10,direction="ltr",halign="top"); // FL = Front left
        
         
         
  // fix X corner switch:  
        
         minkowski()
 {            
     translate([-11.5-20,-21,3.5])   // -22  23.5  -1
   cube([43,5,12],center=true) ; //22
    
  rotate([0,90,90])
  cylinder(h=1,d=2,center=true,$fn=48); 
}


  translate([-20,32,2-1.5-3]) // 14 = 3 mm bottom 
    cylinder(h=9+6,d=DiaBar+10,center=true,$fn=48)  ;     
         
  translate([-20,-32,2-1.5-3]) // ..50 
    cylinder(h=9+6,d=DiaBar+10,center=true,$fn=48)  ; // h 6

  // make sharp corners here:

     translate([46,40.5,4])
    cube([7,5,4],center=true);
      
     translate([46,-40.5,4])
    cube([7,5,4],center=true);
        
    // M8 FIX   on bottom
      
    translate([45,34.75,-22]) //31
    rotate([0,90,0])
      cylinder(h=10,d=DiaM8+12,center=true,$fn=24 );     // = 16.6 = ok
         
     translate([45,-34.75,-22]) // -31
       rotate([0,90,0]) 
       cylinder(h=10,d=DiaM8+12,center=true,$fn=24 );   
    }   //      end union   
    
    
      
   // M8 FIX ON BOTTOM 
      
      translate([45,34.75,-22]) //31
      rotate([0,90,0])
         cylinder(h=20,d=DiaM8,center=true,$fn=48);     // = 16.6 = ok
         
          translate([45,-34.75,-22]) //-31
          rotate([0,90,0]) 
         cylinder(h=20,d=DiaM8,center=true,$fn=48 );     
              
          
        //  FIX ON MDF is possible
    
      translate([49,34,-22+16.5]) //-10
      rotate([0,90,0])
         cylinder(h=14,d=DiaM7,center=true,$fn=24 );     // = 16.6 = ok
         
          translate([49,-34,-22+16.5]) //
          rotate([0,90,0]) 
         cylinder(h=14,d=DiaM7,center=true,$fn=24 );     
    
    // spare pockets on bottom to lift from bed:
            
     translate([42,44,10.5])
    cube([7,7,2],center=true);
    
     translate([42,-44,10.5])
    cube([7,7,2],center=true);
      
   ////*********  SPEEDUP PRINTING :

//  to print without support, dual fan is necessary :

translate([44,0,-22.5])   
       cube([14,44,49],center=true);  
                    
          
  ///*********  STEPPER               
  // big hole in stepper front plate   
  
       hull()
{
     translate([-10,0,-24])//-40                 
        rotate([0,90,0])                  
       cylinder(h=12,d=DiaStepper,center=true,$fn=48 );
       
 translate([-10,0,-24-6])//-40                 
        rotate([0,90,0])                  
       cylinder(h=12,d=DiaStepper,center=true,$fn=48 );
}


// test stepper room:

/*
  translate([-10,0,-24])//-40   
     cube([42,42,42],center=true);
*/

//   4 3 mm motor fix holes 31 mm distance in stepper plate
    
hull()
{    
     translate([-10,15.5,-24+15.5])                 
        rotate([0,90,0])                  
       cylinder(h=12,d=DiaM3,center=true,$fn=24 );
        translate([-10,15.5,-24+15.5-6])                 
        rotate([0,90,0])                  
       cylinder(h=12,d=DiaM3,center=true,$fn=24 );
    }
hull()
{                
           translate([-10,15.5,-24-15.5])                 
        rotate([0,90,0])                  
       cylinder(h=12,d=DiaM3,center=true,$fn=24 );
       
           translate([-10,15.5,-24-15.5-6])                 
        rotate([0,90,0])                  
       cylinder(h=12,d=DiaM3,center=true,$fn=24 );
         }
hull()
{                  
     translate([-10,-15.5,-24+15.5])                 
        rotate([0,90,0])                  
       cylinder(h=12,d=DiaM3,center=true,$fn=24 );
     translate([-10,-15.5,-24+15.5-6])                 
        rotate([0,90,0])                  
       cylinder(h=12,d=DiaM3,center=true,$fn=24 );   
       }
hull()
{                              
           translate([-10,-15.5,-24-15.5])                 
        rotate([0,90,0])                  
       cylinder(h=12,d=DiaM3,center=true,$fn=24 );
           translate([-10,-15.5,-24-15.5-6])                 
        rotate([0,90,0])                  
       cylinder(h=12,d=DiaM3,center=true,$fn=24 );
       }
                    
     
       
       
   // Holes for the 2 linear shafts Y axis
            
  translate([-20,32,10]) //  = 3 mm bottom 
    cylinder(h=34,d=DiaBar,center=true,$fn=48)  ;     
         
         
  translate([-20,-32,10]) //  
    cylinder(h=34,d=DiaBar,center=true,$fn=48)  ;  
      
      
       // Air outlet for shafts Y axis
            
  translate([-20,32,-10]) //  = 3 mm bottom 
    cylinder(h=10,d=DiaAir,center=true,$fn=48)  ;     
         
  translate([-20,-32,-10]) //  
    cylinder(h=10,d=DiaAir,center=true,$fn=48)  ; 
 
       
       
       // maybe fix bars
       
         for(r=[0:45:180])
         {
  translate([-20,-32,-2]) //    
 rotate([ 90,0,r+40])
    cylinder(h=30,d=DiaFix,center=true,$fn=12)  ; 
         }
       
        for(r=[0:45:180])
         {
  translate([-20,32,-2]) //   
 rotate([ 90,0,r])
    cylinder(h=30,d=DiaFix,center=true,$fn=12)  ; 
         }
                   
       
   // LARGE FRONTAL OPENING
         
    translate([18,0,-4])
       cube([50, 44,30],center=true); //36
    
     // SMALLER FRONTAL OPENING ON TOP   
       
    translate([-25,0,-4])
       cube([24.0, 36,30],center=true); //34
    
    
   // fix X corner switch :      
       
         hull()
         {             
     translate([-48.5,-20,6])   //  
      rotate([90,0,0])    
       cylinder(h=10,d=DiaM2,center=true,$fn=24) ;  
     
          translate([-48.5,-20,1])   //  
      rotate([90,0,0])    
       cylinder(h=10,d=DiaM2,center=true,$fn=24) ;  
         }
     
          hull()
         {             
     translate([-38.5,-20,1])   //  
      rotate([90,0,0])    
       cylinder(h=10,d=DiaM2,center=true,$fn=24) ;  
     
          translate([-38.5,-20,6])   //  
      rotate([90,0,0])    
       cylinder(h=10,d=DiaM2,center=true,$fn=24) ;
         }
              
       
     // fix bottom on MDF is possible ,
         // or pass wiring,
         // otherwise do not use
                
     translate([44,34,-44])                 
        rotate([0,90,0])                  
       cylinder(h=16,d=DiaM7,center=true,$fn=12 );         
            
     translate([44,-34,-44])                 
        rotate([0,90,0])                  
       cylinder(h=16,d=DiaM7,center=true,$fn=12 );
         
  }


echo(version=version());