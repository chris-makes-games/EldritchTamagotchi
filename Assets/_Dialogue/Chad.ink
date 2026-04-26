INCLUDE globals.ink
VAR visited = false

{visited == false: -> welcome}
{visited == true: -> welcomeBack}

==welcome==
Oh hey, so you're new? Nice hat, dumbass.
-> END

==welcomeBack==
What do you want?

-> DONE