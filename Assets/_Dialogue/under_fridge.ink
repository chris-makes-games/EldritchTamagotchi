INCLUDE globals.ink

The refrigerator is dead silent.

+ [Open] -> open
+ [Leave] -> nopen

== open ==
You open the fridge door. It is dark and lukewarm. A lone box of half-eaten animal crackers sits on the top shelf, the rest of the fridge completely empty.
+ [Close] -> close

== close ==
That's enough refrigerator for today.
-> DONE

== nopen ==
You decide not to open it.
-> DONE