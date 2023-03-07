//4 5 2018 edit 
  DiaM2 = 2.4; // fix together 
   DiaM3 = 3.7; // fix together   
   DiaM4 = 4.5;  //  // push the z switch
   DiaM5 = 5.6 ; // if threaded M5 push bar is used      
   DiaM6 = 6.8; // gliders + 0.2 !
    
    rotate([180,0,0]) // print M5 hexnut opening upwards
    {    
difference()
{

union()
    {
        
  translate([0,0,2.5])       // -2.5
cylinder(h=8,d=22,center=true,$fn=48);        
        
minkowski()
{
     translate([0,0,2.5])      // 2.5   
    
cube([88,12,7],center=true);        
cylinder(h=1,d=4,center=true,$fn=24);    
}

    translate([9,-6.5,-0.25])  // -0.25
        rotate([90,0,0])
           linear_extrude(height = 2) {
                     text("DISPENS", font = "Liberation Sans:style=Bold",size=5.5);
             } 
             
             
// push the Z switch 

 translate([-25.5,6,2.5])  // -2.5
    cylinder(h=8,d=DiaM4+8,center=true,$fn=24);   

       
}



// push the Z switch 

 translate([-25.5,6,0])
    cylinder(h=20,d=DiaM4,center=true,$fn=24);   


// ROOM FOR M5 NUT: 

 translate([0,0,0.4]) // 0.4
     cylinder(h=4.0 ,d=9.4 ,center=true,$fn=6);  //

// M6  linear shafts 

 translate([39.0,0,2])
    cylinder(h=12,d=DiaM6,center=true,$fn=48);   

  translate([-39,0,2])
    cylinder(h=12,d=DiaM6,center=true,$fn=48);      
  
  // M5 driver
     cylinder(h=30,d=DiaM5,center=true,$fn=48);   // for ml syringe


   // FIX BOTH PARTS TOGETHER WITH M3
       // FIX BOTH PARTS TOGETHER WITH M3
       
       translate([-15,0,0])
    cylinder(h=16,d=DiaM3,center=true,$fn=24);      
 
     translate([15,0,0])
    cylinder(h=16,d=DiaM3,center=true,$fn=24);    
    
 
 // demo:
 
//   translate([0,10,0])   // -10 = top  Z +10 = bottom
//  cube([100,20,100],center=true); 
 
 
      
}
}