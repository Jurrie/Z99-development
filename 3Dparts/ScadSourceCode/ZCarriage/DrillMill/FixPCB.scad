// FIX PCB ON MDF PLATE


DiaScrew = 4.4; //  bolts and nuts                 
Width=12;
Height=3.0;

Length=230;


rotate([0,0,45]) // print on bed of 20 cm


difference()
{    
    union()
    { 
   cube([Length,Width,Height],center=true);  
                   
    }

   // spare out
       translate([0,-Width/2,1.4])   // pcb height 1.6 + 1.2 mm cover
    cube([Length+1,3,Height],center=true);  // = 1.5mm cover
    
     // spare out
       translate([0,Width/2,1.4])   // pcb height 1.6 + 1.2 mm cover
    cube([Length+1,3,Height],center=true);  // = 1.5mm cover
   
    // fix
      for (x=[0:1:6])
    {  
  
  hull()
    {      
  translate([ -92+ x*30 ,0,0])           
  cylinder(h=Height+1,d=DiaScrew,center=true,$fn=48); 
         translate([ -88+ x*30 ,0,0])           
  cylinder(h=Height+1,d=DiaScrew,center=true,$fn=48); 
        
    }
       
        
        
    }
    
      
}