INCLUDE globals.ink

The refrigerator eminates a soft, pleasant hum.
+ [Open] -> open
+ [Leave] -> nopen

== open ==
As you move to open it, the fridge automatically opens for you.
+ [Continue] -> open2

== open2 ==
A soft, yellow light illuminates the fridge, which is filled to the brim with extravagant pastries and energy drinks.
+ [Continue] -> open3

== open3 ==
You consider taking a pastry, but decide otherwise and close the door.
-> DONE

== nopen ==
You decide not to open it. It's not your fridge after all.
-> DONE