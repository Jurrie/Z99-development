//  Fix X2Y4Z PCB on MDF
// Print 4 pieces
// PRINT VERY SLOW OTHERWISE STILL TO NARROW FOR M4 WHEN PRINTED ONE BY ONE !

DiaM4=5.0; // large enough 
DiaOuter=10; // 10
Height = 35;             
              
   color("lightblue")
   
     
 difference() {  
  
     union()
     {  
  cylinder(h=Height ,d=DiaOuter,center=true,$fn=96); 
   }
         
  cylinder(h=Height+1 ,d= DiaM4,center=true,$fn=96); 
 
   
   
}      

 