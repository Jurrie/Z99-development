// This part is only for determination of M9 holes in MDF plate.
// Four pieces are needed 

Xcube = 90;
Ycube = 65;    
Larger= 8;       // sidewalls are 2*4 mm larger,room for 2 elevations   
DiaM2 =2.5 ;     // fix on MDF or predrill with M2 for 2* M9         
DiaM8=8.4;           
DiaLM12= 12.40;  // = for 12 mm Y bar
 
color("green")

 difference() 
 {             
 union()
 { 
  difference()
         {             
  union() {  

  cube([Xcube+Larger*2,Ycube,1.4],center = true);  //
         
  //  holders for 2 y bars  
      
   translate([-32,22.5,2.5])   // 
        cube([ 14,20,5],center=true);  //
           
       translate([32,22.5,2.5])   // 
        cube([ 14,20,5],center=true);  //
  }  // END  UNION
   
  
  //  sketch two half circular lines around "M8": 
     difference()
     {         
      translate([34.75,0,0]) //31
         cylinder(h=2,d=DiaM8+0.8,center=true,$fn=48 );     // 
                 
         translate([34.75,0,0]) //-10
         cylinder(h=3,d=DiaM8-0.8,center=true,$fn=48 );     // 
     }
    
  //  two half circular lines around M8: (sketch with pencil is possible)
   difference()
     {         
      translate([-34.75,0,0]) //-31
         cylinder(h=2,d=DiaM8+0.8,center=true,$fn=48 );     // 
                 
         translate([-34.75,0,0]) //-31
         cylinder(h=3,d=DiaM8-0.8,center=true,$fn=48 );     
     }
     
  // later for M8 :  
   
    translate([34.75,0,0]) //31
         cylinder(h=4,d=DiaM2,center=true,$fn=24 );     // 
     
          translate([-34.75,0,0]) //-31
         cylinder(h=4,d=DiaM2,center=true,$fn=24 ); 
    

 //  fix 4 corners temporary eg with small nails:        
         
      translate([-46,-26,0]) //-10
         cylinder(h=10,d=DiaM2,center=true,$fn=24 );     
     
      translate([-46,26,0]) //-10
         cylinder(h=10,d=DiaM2,center=true,$fn=24 );    
    
       translate([46,-26,0]) //-10
         cylinder(h=10,d=DiaM2,center=true,$fn=24 );     
     
      translate([46,26,0]) //-10
         cylinder(h=10,d=DiaM2,center=true,$fn=24 );   
            
       //  12 mm  bar:    
       
translate([-32,24,8])      // 23.75  =0.6 for bottom   y = 41.5-24.5 = 17.5mm is Y bar inserted
         rotate([90,0,0]) 
 cylinder(h=18.0, d= DiaLM12,center=true,$fn=80);  //18+0.5...
          
 translate([32,24.0,8])      //  23.75 =0.6 for bottom
         rotate([90,0,0]) 
 cylinder(h=18.0, d= DiaLM12,center=true,$fn=80);

// speedup printing:
 
cube([50,50,4],center=true);

translate([46,0,0])
cube([8,40,4],center=true);

translate([-46,0,0])
cube([8,40,4],center=true);
}

  //  Connection, 2 bridges  
       translate([-34.75,0,-0.2])    
  cube([12,2,0.4],center = true);  
   
       translate([34.75,0,-0.2])   
cube([12,2,0.4],center = true); 
  
  mirror([180,0,0])
   {
           translate([40,-16,1.5])                  
       rotate([180,0,180])                  
      linear_extrude(2.0)                 
              text("Z99","Arial",size=6,direction="ltr",halign="top");  
         
           translate([-26,-16,1.5])                  
       rotate([180,0,180])                  
      linear_extrude(2.0)                 
              text("AID","Arial",size=6,direction="ltr",halign="top");              
   }     
  }  // end upmost union
    
    translate([34.75,0,0]) //31
         cylinder(h=4,d=DiaM2,center=true,$fn=24 );     // = 16.6 = ok
    
          translate([-34.75,0,0]) //-31
         cylinder(h=4,d=DiaM2,center=true,$fn=24 );   
  } // end upmost difference


echo(version=version());
