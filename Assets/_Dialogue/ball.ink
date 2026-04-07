INCLUDE globals.ink
VAR visited = false

{visited == false: -> sphere}
{visited == true: -> still}

==sphere==
The perfect sphereical ball seems like it should roll but it won't budge. At first it seems too heavy but it's just frozen in place.
+[Kick it] -> kick
+[Leave] -> END

==still==
The ball has not moved even a little bit.
+[Kick it] -> kick
+[Leave] -> END

==kick==
You kick the ball, and it holds fast like it was nothing. Your foot hurts.
-> END