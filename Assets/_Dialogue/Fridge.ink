INCLUDE globals.ink
VAR visited = false

{visited == true && fridgeOpen == false: -> backClosed}
{visited == true && fridgeOpen == true: -> backOpen}
{visited == false: -> open0}
{visited == true}

==backClosed==
The refrigerator hums ominously.
+[Open it] -> OpenUp
+[Leave] -> LeaveClosed

==backOpen==
The refrigerator hums ominously. The door is wide open.
+[Close it] -> CloseDoor
+[Leave] -> DoorOpen

==open0==
This appears to be a refrigerator.
~ totalInteractions += 1
+[Continue] -> open1

==open1==
On the front of the refrigerator there are several pictures of a green slime creature attached using dog footprint magnets.
+[Continue] -> open2

==open2==
The refrigerator hums incessantly, the noise filling up most of the space around it.
+[Open it] -> OpenUp
+[Leave] -> LeaveClosed

==OpenUp==
~ fridgeOpen = true
Inside, the refrigerator is completely empty and pristinely clean. The white light you would expect seems to be emitting from the back, blinding and hot.
+[Close it] -> CloseDoor
+[Leave] -> DoorOpen

==DoorOpen==
You decide to leave the door open. The white-hot light spills out into the room and casts a band of eerie glow onto the floor.
-> END

==CloseDoor==
~ fridgeOpen = false
You decide to close the door. The blinding light is mercifully cut off.
-> END

==LeaveClosed==
You decide to leave it closed.
-> END

==leaveIgnore==
You decide to leave the refrigerator as it is.
-> END
