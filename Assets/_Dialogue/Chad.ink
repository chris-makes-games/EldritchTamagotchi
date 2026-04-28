INCLUDE globals.ink
VAR visited = false

{visited == false: -> welcome}
{visited == true: -> back}

==welcome==
"Oh, hello! Welcome to my humble abode. My, you have an... interesting choice of hat. Lol. Lmao, even."
+[Rude?] -> rude
+[Leave] -> earlyLeave

==earlyLeave==
"Feel free to explore. My taste begets plenty of admiration. Come back when you're done."
-> END

==back==
"Welcome back. Like what we've done with the place?"
+[Leave] -> earlyLeave

==rude==
"Let me guess: you have a rebellious streak? You know, it doesn't need to be so hard. Since we're now friends, let me give you some advice:"
+[Continue] -> cont

==cont==
"Life has too much going on to focus on making every single little choice for yourself. Bigger systems can run your life in better ways. The caretaker only wants what's best for you." :)
~ playdateComplete = true
~ setQuestText = "well done"
-> END