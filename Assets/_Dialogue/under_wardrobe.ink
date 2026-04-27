INCLUDE globals.ink

It's a dusty set of drawers that look quite similar to your own.

+ [Open] -> open
+ [Leave] -> nopen

== open ==
The drawers struggle to open, requiring some force before swinging open with a loud creak. These haven't been opened in awhile.
+ [Continue] -> open2

== open2 ==
It is full of fingerless leather gloves and mismatched polka-dot socks.
+ [Close] -> closedrawers

== closedrawers ==
You feel like you've learned something you shouldn't have.
-> DONE

== nopen ==
You decide you have some respect for people's privacy.
-> DONE