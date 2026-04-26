INCLUDE globals.ink
VAR visited = false

{visited == true: -> grabbed}
{visited == false: -> grab}

== grab ==
You picked up the knife.
~ holdingKnife = true
    -> END

== grabbed ==
You already picked up the knife, so there isn't a knife there anymore.
    -> END