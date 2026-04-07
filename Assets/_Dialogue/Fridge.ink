INCLUDE globals.ink
VAR visited = false

{visited == true && fridgeOpen == false: -> backClosed}
{visited == true && fridgeOpen == true: -> backOpen}
{visited == false: -> open1}

==backClosed==
The refridgerator hums ominously.
+[Open] -> OpenUp

==backOpen==
The refridgerator hums ominously. The door is wide open.
+[Close] -> CloseDoor
+[Leave] -> DoorOpen

==open1==
On the front of the refridgerator there are several pictures of a green slime creature attached using dog footprint magnets. There is a magnetic word jumble that says "B@5IL15K".
+[Continue] -> open2

==open2==
The refridgerator hums incessantly, the noise filling up most of the space around it.
+[Open] -> OpenUp
+[Leave] -> END

==OpenUp==
Inside, the refridgerator is completely empty and pristinely clean. The white light you would expect seems to be emitting from the back, blinding and hot.
+[Close] -> END
+[Leave] -> DoorOpen

==DoorOpen==
~ fridgeOpen = true
You decide to leave the door open. The white-hot light spills out into the room and casts a band of eerie glow onto the floor.
-> END

==CloseDoor==
You decide to close the door. The blinding light is cut off.
-> END
