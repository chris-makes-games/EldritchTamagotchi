INCLUDE globals.ink
VAR visited = false

{sinkTurned == true: -> turned}
{visited == true: -> back}
{visited == false: -> select}

==back==
The sink is still just a sink. There are two knobs, labeled using the hot face and cold face emojis.
+{!visited}[Turn Hot] -> terror
+{!visited}[Turn Cold] -> terror

==select==
This seems to be an orginary sink. There are two knobs, labeled using the hot face and cold face emojis.
+{!visited}[Turn Hot] -> terror
+{!visited}[Turn Cold] -> terror

==terror==
A terrible groaning noise emits from somewhere below, and the sink shudders.
~ sinkTurned = true
+[Continue] -> ichor

==ichor==
A single drop of thick black liquid falls from the tip of the sink nozzle with a disconcerting metallic plop.
-> END

==turned==
Nothing else is coming out of the sink.
+{!visited}[Turn Hot] -> tryAgain
+{!visited}[Turn Cold] -> tryAgain

==tryAgain==
You try turning one of the knobs and it does nothing. The knob rotates freely without resistance.
-> END