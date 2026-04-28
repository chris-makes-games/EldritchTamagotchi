INCLUDE globals.ink
VAR visited = false

//running
//0 - none
//1 - hot
//2 - cold
//3 - both

{sinkFix == true && sinkRunning == 0: -> fixedNone}
{sinkFix == true && sinkRunning == 1: -> fixedHot}
{sinkFix == true && sinkRunning == 2: -> fixedCold}
{sinkFix == true && sinkRunning == 3: -> fixedBoth}
{sinkTurned == true && sinkRunning == 1: -> turnedHot}
{sinkTurned == true && sinkRunning == 2: -> turnedCold}
{sinkTurned == true && sinkRunning == 3: -> turnedBoth}
{visited == true: -> back}
{visited == false: -> select}

==back==
The sink is still a strange sink. There are two knobs, labeled using the hot face and cold face emojis. There is a small empty glass on the counter nearby.
+[Turn Hot] -> terrorHot
+[Turn Cold] -> terrorCold

==select==
This seems to be an ordinary sink. There are two knobs, labeled using the hot face and cold face emojis. There is a small empty glass on the counter.
+[Turn Hot] -> terrorHot
+[Turn Cold] -> terrorCold

==terrorHot==
A terrible groaning noise emits from somewhere below, and the sink shudders.
~ sinkTurned = true
~ sinkRunning = 1
~ waterTemp = "hot"
+[Continue] -> ichorHot

==terrorCold==
A terrible groaning noise emits from somewhere below, and the sink shudders.
~ sinkTurned = true
~ sinkRunning = 2
~ waterTemp = "cold"
+[Continue] -> ichorCold

==ichorCold==
A single drop of thick black liquid falls from the tip of the sink nozzle with a disconcerting metallic plop.
+[Continue] -> turnedCold

==ichorHot==
A single drop of thick black liquid falls from the tip of the sink nozzle with a disconcerting metallic plop.
+[Continue] -> turnedHot

==allOff==
You turn off the sink. The thick black fluid slowly disappears down the drain.
~ sinkRunning = 0
+[Turn Hot] -> ichorHot
+[Turn Cold] -> ichorCold
+[Leave] -> earlyLeave

==turnedHot==
A thick black fluid occasionally drips from the sink.
~ sinkRunning = 1
~ waterTemp = "hot"
+[Turn Off Hot] -> allOff
+[Turn Cold] -> turnedBoth
+[Leave] -> earlyLeave

==turnedCold==
A thick black fluid occasionally drips from the sink.
~ sinkRunning = 2
~ waterTemp = "cold"
+[Turn Hot] -> turnedBoth
+[Turn Off Cold] -> allOff
+[Leave] -> earlyLeave

==turnedBoth==
You turn the other knob. The thick black fluid comes out in spurts.
~ sinkRunning = 3
~ waterTemp = "warm"
+[Turn Off Hot] -> turnedCold
+[Turn Off Cold] -> turnedHot
+[Leave] -> earlyLeave

==earlyLeave==
You decide to leave the sink as it is.{sinkRunning > 0: You leave the sink running, black fluid dripping from it.}
~ sinkFix = true
-> END

==leave==
You decide not to have a drink.{sinkRunning > 0: You leave the sink running}
~ drank = 2
~ love -= 1
-> END

==fixedOff==
You turn off the sink. The water quickly rushes down the drain.
~ sinkRunning = 0
+[Turn Hot] ->  tryFixedHot
+[Turn Cold] -> tryFixedCold
+[Leave] -> ignore

==fixedNone==
The sink is silent and ominous. There are two knobs, labeled using the hot face and cold face emojis. There is a small empty glass on the counter.
+[Turn Hot] ->  tryFixedHot
+[Turn Cold] -> tryFixedCold
+[Leave] -> ignore

==fixedHot==
Clear {waterTemp} water is pouring from the sink. There are two knobs, labeled using the hot face and cold face emojis. There is a small empty glass on the counter.
+[Fill the glass] -> fill
+[Turn Off Hot] ->  fixedOff
+[Turn Cold] -> tryFixedWarm
+[Leave] -> ignore
~ sinkRunning = 1
~ waterTemp = "hot"


==fixedCold==
Clear {waterTemp} water is pouring from the sink. There are two knobs, labeled using the hot face and cold face emojis. There is a small empty glass on the counter.
+[Fill the glass] -> fill
+[Turn Hot] ->  tryFixedWarm
+[Turn Off Cold] -> fixedOff
+[Leave] -> ignore
~ sinkRunning = 2
~ waterTemp = "cold"


==fixedBoth==
Clear {waterTemp} water is pouring from the sink. There are two knobs, labeled using the hot face and cold face emojis. There is a small empty glass on the counter.
+[Fill the glass] -> fill
+[Turn Off Hot] ->  fixedCold
+[Turn Off Cold] -> fixedHot
+[Leave] -> ignore
~ sinkRunning = 3
~ waterTemp = "warm"

==ignore==
You decide not to have a drink.{sinkRunning > 0: You leave the sink running}
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
~ sinkRunning = 1
~ waterTemp = "hot"

==tryFixedCold==
You turn the knob and cold water comes running out. It washes away the black goo near the drain.
+[Fill the glass] -> fill
+[Turn the Hot knob too] -> tryFixedWarm
+[Turn off sink] -> off
+[Leave] -> leave
~ sinkRunning = 2
~ waterTemp = "cold"

==tryFixedWarm==
You turn the other knob, balancing out the temperature. Lukewarm water pours from the faucet into the drain.
+[Fill the glass] -> fill
+[Turn off sink] -> off
+[Leave] -> leave
~ sinkRunning = 3
~ waterTemp = "warm"

==fill==
You fill the glass with {waterTemp} water.
+[Drink] -> drink
+[Pour Out] -> leave

==off==
You shut off the water. the sink is silent again. There are two knobs, labeled using the hot face and cold face emojis. There is a small empty glass on the counter.
+[Turn Hot] ->  tryFixedHot
+[Turn Cold] -> tryFixedCold
+[Leave] -> ignore

==drink==
You have a drink of the {waterTemp} water. It tastes slightly metallic.
~ hatTime = true
~ love += 1
~ drank = 1
-> END