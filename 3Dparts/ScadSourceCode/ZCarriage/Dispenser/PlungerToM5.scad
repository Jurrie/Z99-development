// 18  4  2018  : 2 ml syringe holder
 
 
 DiaM5 = 5.6;
 DiaPlunger=14; // for luer lock 3 ml syringe 
 
 
color("lime")    
rotate([90,0,0])
    
difference()
{ 
  union()
  {     
      
     //   translate([0,0,-0.5])// 0 0
       cylinder(h=12 ,d=20 ,center=true,$fn=48);  //23.2
      
      
      // rotate and make printable on this side without support:
      
    translate([0,-7.5,0])
         cube([8,5,12],center=true);       
      
  }      

  // end union
  
  // nut:
  cylinder(h=14 ,d=5.6 ,center=true,$fn=48);  
  
  
  
// insert plunger:
  hull()
  { 
      translate([0,0,-4])
 cylinder(h=7 ,d=9.2 ,center=true,$fn=48);  // pass for M5 screw head
 translate([0,10,-3])
cylinder(h=7 ,d=8 ,center=true,$fn=48);  
}
  // insert plunger:
  hull()
  { 
    cylinder(h=4 ,d=DiaPlunger ,center=true,$fn=48);  //       17
     translate([0,10,0])
    cylinder(h=4 ,d=DiaPlunger ,center=true,$fn=48);    //17
  }
  
  
// demo:
  /*
   translate([20,0,0]) /// Z + 10 = bottom or Z - 10  = top
    cube([40,100,100],center=true); 
  */
  }
   
   


         
         