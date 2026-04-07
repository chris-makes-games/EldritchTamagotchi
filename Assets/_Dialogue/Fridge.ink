INCLUDE globals.ink
VAR visited = false

{visited == true && fridgeOpen == false: -> backClosed}
{visited == true && fridgeOpen == true: -> backOpen}
{visited == false: -> open0}

==backClosed==
The refridgerator hums ominously.
+[Open it] -> OpenUp
+[Leave] -> LeaveClosed

==backOpen==
The refridgerator hums ominously. The door is wide open.
+[Close it] -> CloseDoor
+[Leave] -> DoorOpen

==open0==
This appears to be a refridgerator.
~ totalInteractions += 1
+[Continue] -> open1

==open1==
On the front of the refridgerator there are several pictures of a green slime creature attached using dog footprint magnets.
+[Continue] -> open2

==open2==
The refridgerator hums incessantly, the noise filling up most of the space around it.
+[Open it] -> OpenUp
+[Leave] -> END

==OpenUp==
Inside, the refridgerator is completely empty and pristinely clean. The white light you would expect seems to be emitting from the back, blinding and hot.
+[Close it] -> END
+[Leave] -> DoorOpen

==DoorOpen==
~ fridgeOpen = true
You decide to leave the door open. The white-hot light spills out into the room and casts a band of eerie glow onto the floor.
-> END

==CloseDoor==
{visited == true: totalInteractions += 1}
~ visited = true
~ fridgeOpen = false
You decide to close the door. The blinding light is mercifully cut off.
-> END

==LeaveClosed==
~ visited = true
You decide to leave it closed.
-> END
