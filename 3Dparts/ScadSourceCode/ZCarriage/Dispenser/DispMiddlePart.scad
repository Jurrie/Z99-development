// 25 6 2018

   DiaM3 = 3.6;      // fix together   
   DiaTye=4;         // belt tying
   DiaM4=4.5;         // push the Z switch
   DiaM5 = 5.6 ;     // M5 linear shafts and M5 threaded M5 push bar   
   DiaM6=6.8;
   
   DiameterMaxAcme=28;  //
   DiameterBusAcme=10.5; // 
   
   // no need to rotate in slic3r :
   
    rotate([180,0,0]) 
    {
    
difference()
{
    
union()
    {        
        // pass M5 screw:
        
  cylinder(h=6,d=22,center=true,$fn=48);        
                
 translate([39.0,0,-26])
    cylinder(h=56,d=DiaM6+6,center=true,$fn=24);   

  translate([-39,0,-26])
    cylinder(h=56,d=DiaM6+6,center=true,$fn=24);     
 
     
minkowski()
{     
    
cube([88,12,5],center=true);        
cylinder(h=1,d=4,center=true,$fn=24);    
}

translate([35.0,0,-21]) //-35
cube([8,12,46],center=true);

translate([-35.0,0,-21])
cube([8,12,46],center=true);

// downwards, for fixing to pen holder

difference()
{
translate([0,0,-43])  //-62
cube([74,12,22],center=true);

translate([0,0,-23])  // -54
    rotate([90,0,0])
cylinder(h=18,d=64,center=true,$fn=48); 

translate([0,0,-44])  //-62
cube([42,14,22],center=true);


}

         
// push the Z switch 

 translate([-25.5,6,0])
    cylinder(h=6,d=DiaM4+8,center=true,$fn=24);  



}   // end union



// push the Z switch 

 translate([-25.5,6,0])
    cylinder(h=12,d=DiaM4,center=true,$fn=24);   





// M6 shafts 

 translate([39.0,0,-35])
    cylinder(h=80,d=DiaM6,center=true,$fn=48);   

  translate([-39,0,-35])
    cylinder(h=80,d=DiaM6,center=true,$fn=48);      
  
  
  // bus acme shaft
     cylinder(h=12,d=DiaM5,center=true,$fn=24);   // for ml syringe


          
   // FIX BOTH PARTS TOGETHER WITH M3
       
       translate([-15,0,0])
    cylinder(h=16,d=DiaM3,center=true,$fn=24);      
 
     translate([15,0,0])
    cylinder(h=16,d=DiaM3,center=true,$fn=24);    
 


//  pass syringe and M5

  translate([0,0,-48])
      cube([25,12,20],center=true);//24
 
 
 
//  tying, fix on syringe holder :

  translate([27.0,0,-48])     //
  rotate([90,0,0]) 
   scale([1,0.66,1])
  cylinder(h=25 ,d=DiaTye,center=true,$fn=24);  // 
   
  translate([-27,0,-48])     //
  rotate([90,0,0])
   scale([1,0.66,1])
  cylinder(h=25 ,d=DiaTye,center=true,$fn=24);  // 

 
 
 
// Demo frontal section displays M5 nut holder
/*
  translate([0,10,0])
      cube([80,20,100],center=true);//24
 */
 
 
      
}

}
