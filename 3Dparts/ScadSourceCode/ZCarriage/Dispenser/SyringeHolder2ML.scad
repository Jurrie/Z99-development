
// 18  4  2018  : 2 ml syringe holder
 
   DiaTye=3.8;
  
   DiaM3=3.4;
   DiaM5=5.6;  // 
   DiaM6=6.6;
   
   DiaAcme=10.5;
   DiaSyringe=10.6;  // 2ML 10.2 + paper = 10.6
                     // 5ML 14.4+ paper = 14.8 
                   
       Height=30;   // 22
   LM6Height = 24;    // 
    
    VersionWithTye = 1;
    
color("lime")    
    
difference()
{ 
  union()
  {     
                         
   // side gliders 
          translate([39,0,-1.5])
       cylinder(h=LM6Height ,d=20 ,center=true,$fn=48);  //
            
          translate([-39,0,-1.5])
       cylinder(h=LM6Height ,d=20 ,center=true,$fn=48);  //
                
 // connect central  part:   
        translate([0,1.75,-1.5])
      cube([76,5,LM6Height],center=true);   
      
      
   // central holder:
      
         translate([0,0,17.5])// 0 , 1.5
       cylinder(h=60 ,d=DiaSyringe+8 ,center=true,$fn=48);  // 53
            
 // thickest on basis syringe:  
      
      translate([0,0,-6])// 0 , 1.5
       cylinder(h=15 ,d=DiaSyringe+12 ,center=true,$fn=48);  // 53
      
      // funnel:     
            
     translate([0,0,5.5])// 0 , 1.5
    cylinder(h=8 ,d2=DiaSyringe, d1=DiaSyringe+12 ,center=true,$fn=48);  
      
      
      // Append acme
      
      hull()
      {       
      translate([0,32,-10.5]) 
      cylinder(h=6,d=DiaAcme+15,center=true,$fn=48); 
       
       translate([0,2,-10.5])      
        cube([36,5,6],center=true);  
          }
      
        
  // make stronger on tying places:
          
  translate([28.5,0,-11.5])
      cube([12,8,4],center=true);
    translate([-28.5,0,-11.5])
      cube([12,8,4],center=true);
     
  }      

          // end union

  
 
 //  LM6 holder 
      translate([-39,0,-1.5])
       cylinder(h=19.5 ,d=12.5 ,center=true,$fn=48);  //
           
  //  LM6 holder 
      translate([39,0,-1.5])
       cylinder(h=19.5 ,d=12.5 ,center=true,$fn=48);  //
  
  
  
 // left shaft  
      translate([39,0,0])
       cylinder(h=30 ,d=DiaM6 ,center=true,$fn=48);  //
         
 // right shaft
 
      translate([-39,0,0])
       cylinder(h=30 ,d=DiaM6 ,center=true,$fn=48);  //      
             
  // ACME
    
      translate([0,32,-10]) 
      cylinder(h=10,d=DiaAcme,center=true,$fn=48); 
    
    // FIX ACME M3
    
           translate([5.8,32+5.8,-11.5]) 
      cylinder(h=10,d=DiaM3,center=true,$fn=24); 
      
           translate([5.8,32-5.8,-11.5]) 
      cylinder(h=10,d=DiaM3,center=true,$fn=24); 
  
           translate([-5.8,32+5.8,-11.5]) 
      cylinder(h=80,d=DiaM3,center=true,$fn=24);
        
      translate([-5.8,32-5.8,-11.5]) 
      cylinder(h=12,d=DiaM3,center=true,$fn=24); 
       

//  Central holder:
  
translate([0,0,18])
       cylinder(h=66 ,d=DiaSyringe,center=true,$fn=48);  //                
  
  
  // Ring to fix border of the syringe 
 
translate([0,0,-6.5])
    scale([0.6,1,1])
       cylinder(h=2.0 ,d=24,center=true,$fn=48);  //
                 
  
  
translate([0,0,-5.3])
    scale([0.6,1,1])
       cylinder(h=1.6 ,d2=24,d1=10.6,center=true,$fn=48);  //
            
    
  
  
// Remove front half of  holder:


translate([0,-1.0,10])  //
cube([24,1,80],center=true);

//  tying, fix on hex nut holder :

  translate([27.0,0,-8])     //
  rotate([90,0,0]) 
   scale([1,0.66,1])
  cylinder(h=25 ,d=DiaTye,center=true,$fn=24);  // 
   
  translate([-27,0,-8])     //
  rotate([90,0,0])
   scale([1,0.66,1])
  cylinder(h=25 ,d=DiaTye,center=true,$fn=24);  // 


//  tying :


  translate([DiaSyringe/2+6,0,-2.0])     //
  rotate([90,0,0])
  scale([0.66,1,1])
  cylinder(h=25 ,d=DiaTye,center=true,$fn=24);  // 
   
  translate([-(DiaSyringe/2+6),0,-2.0])     //
  rotate([90,0,0])
   scale([0.66,1,1])
  cylinder(h=25 ,d=DiaTye,center=true,$fn=24);  // 



// tye LM6 holders

  translate([28,0,0])     // 
  rotate([90,0,0])
  scale([0.66,1,1])
  cylinder(h=25 ,d=DiaTye,center=true,$fn=24);  // 
   
  translate([-28,0,0])     // 
  rotate([90,0,0])
  scale([0.66,1,1])
  cylinder(h=25 ,d=DiaTye,center=true,$fn=24);  // 
  
  // tye pair middle:
  
  translate([DiaSyringe/2+3.5,0,16])     // 
  rotate([90,0,0])
  scale([0.66,1,1])
  cylinder(h=20 ,d=DiaTye,center=true,$fn=24);  // 
   
  translate([-(DiaSyringe/2+3.5),0,16])     //
  rotate([90,0,0])
  scale([0.66,1,1])
  cylinder(h=20 ,d=DiaTye,center=true,$fn=24);  //  
  
   // tye pair down:
  
  translate([DiaSyringe/2+3.5,0,36])     // 
  rotate([90,0,0])
  scale([0.66,1,1])
  cylinder(h=20 ,d=DiaTye,center=true,$fn=24);  // 
   
  translate([-(DiaSyringe/2+3.5),0,36])     //
  rotate([90,0,0])
  scale([0.66,1,1])
  cylinder(h=20 ,d=DiaTye,center=true,$fn=24);  //  
  
      // lateral removal :
    
 translate([49,0,0])
  cube([20,30,30],center=true);
  
     translate([-49,0,0])
  cube([20,30,30],center=true);
    
    // make printable
    
     translate([-47,0,10])
  cube([20,11,4],center=true);
       
     translate([47,0,10])
  cube([20,11,4],center=true); 
  
  
  
  
  
  
  // check :
  
  /*
translate([0,-10,0])  //
cube([100,20,100],center=true);
 */ 
   
  }
   
   


         
         