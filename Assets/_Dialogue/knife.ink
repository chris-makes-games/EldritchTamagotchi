INCLUDE globals.ink

Pick up the knife?
+[Pick Up] -> grab
+[Ignore] -> leave

== grab ==
You picked up the knife.
~ holdingKnife = true
    -> END
    
==leave==
You ignore the knife.
-> END