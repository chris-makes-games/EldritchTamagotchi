INCLUDE globals.ink
VAR visited = false

{doorTried == false: -> door}
{visited == false: -> door}
{visited == true: -> still}


==door==
This metal security door is thick and sturdy. There are scratch marks on it near the bottom. There is a handle to slide the door to the side, and no window.
+[Open] -> locked
+[Leave] -> leave

==still==
The door is still locked.
~ tempText = "I recommend ignoring that for now"
-> END

==locked==
The door won't budge, it's locked.
~ tempText = "That door is for your protection"
~ doorTried = true
+[Leave] -> END

==leave==
You decide to leave the door as it is.
-> END