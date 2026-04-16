INCLUDE globals.ink
VAR visited = false

{running == true: -> running}
{sinkFix == true: -> fixed}
{sinkTurned == true: -> turned}
{visited == true: -> back}
{visited == false: -> select}

==back==
The sink is still just a sink. There are two knobs, labeled using the hot face and cold face emojis. There is a small empty glass on the counter nearby.
+[Turn Hot] -> terror
+[Turn Cold] -> terror

==select==
This seems to be an orginary sink. There are two knobs, labeled using the hot face and cold face emojis. There is a small empty glass on the counter.
+[Turn Hot] -> terror
+[Turn Cold] -> terror

==terror==
A terrible groaning noise emits from somewhere below, and the sink shudders.
~ sinkTurned = true
+[Continue] -> ichor

==ichor==
A single drop of thick black liquid falls from the tip of the sink nozzle with a disconcerting metallic plop.
+[Continue] -> turned

==turned==
Nothing else is coming out of the sink.
+[Turn Hot Again] -> tryAgain
+[Turn Cold Again] -> tryAgain
+[Leave] -> earlyLeave

==earlyLeave==
You decide not to try the sink again.
~ sinkFix = true
-> END

==leave==
You decide not to have a drink. {running == true: You leave the sink running}
~ drank = 2
~ love -= 1
-> END

==tryAgain==
You try turning one of the knobs and it does nothing. The knob rotates freely without resistance.
~ sinkFix = true
-> END

==fixed==
The sink is silent and ominous. There are two knobs, labeled using the hot face and cold face emojis. There is a small empty glass on the counter.
+[Turn Hot Again] ->  tryFixedHot
+[Turn Cold Again] -> tryFixedCold
+[Leave] -> ignore

==ignore==
You decide not to try the sink again.
~ drank = 2
~ hatTime = true
~ love -= 1
-> END

==tryFixedHot==
You turn the knob and hot water comes running out. It washes away the black goo near the drain. There is a small empty glass on the counter.
+[Fill the glass] -> fill
+[Turn the Cold knob too] -> tryFixedWarm
+[Turn off sink] -> off
+[Leave] -> leave
~ sinkRunning = true
~ waterTemp = "hot"

==tryFixedCold==
You turn the knob and cold water comes running out. It washes away the black goo near the drain.
+[Fill the glass] -> fill
+[Turn the Hot knob too] -> tryFixedWarm
+[Turn off sink] -> off
+[Leave] -> leave
~ sinkRunning = true
~ waterTemp = "cold"

==running==
The sink is running, {waterTemp} water is pouring into the drain. There are two knobs, labeled using the hot face and cold face emojis. There is a small empty glass on the counter.
+[Fill the glass] -> fill
+[Turn off sink] -> off
+[Leave] -> leave

==tryFixedWarm==
You turn the other knob, balancing out the temperature. Lukewarm water pours from the faucet into the drain.
+[Fill the glass] -> fill
+[Turn off sink] -> off
+[Leave] -> leave
~ sinkRunning = true
~ waterTemp = "warm"

==fill==
You fill the glass with {waterTemp} water.
+[Drink] -> drink
+[Pour Out] -> leave

==off==
You shut off the water. the sink is silent again. There are two knobs, labeled using the hot face and cold face emojis. There is a small empty glass on the counter.
~ sinkRunning = false
+[Turn Hot Again] ->  tryFixedHot
+[Turn Cold Again] -> tryFixedCold
+[Leave] -> ignore

==drink==
You have a drink of the {waterTemp} water. It tastes slightly metallic.
~ hatTime = true
~ love += 1
~ drank = 1
-> END