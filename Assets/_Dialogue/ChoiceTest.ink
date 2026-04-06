INCLUDE globals.ink
VAR visited = false

{visited == true: -> welcome}
{visited == false: -> select}

==welcome==
Welcome Back!
+{visited}[Welcome Back] -> back

==select==
Select One:
+{!visited}[TestChoice 1] -> one
+{!visited}[TestChoice 2] -> two
==back==
You already chose!
-> END
==one==
You chose one
~ currentHat = 1
-> END
==two==
You chose two
~ currentHat = 2
->END