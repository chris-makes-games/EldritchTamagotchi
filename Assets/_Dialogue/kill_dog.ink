INCLUDE globals.ink
VAR visited = false

{dogKilled == 1: -> dead}
{dogKilled == 2: -> not_dead}

{holdingKnife == false: -> knifeless }
{holdingKnife == true: -> knifeful }

== knifeless ==
{~woof|bork|bark|barf|ruff|pant pant} {~woof|bork|bark|barf|ruff} {~woof|bork|bark|barf|ruff} {~woof|bork|bark|barf|ruff} {~woof|bork|bark|barf|ruff}...
// random selection of dog sounds x4
+ [...] -> knifeless_choice

== knifeful ==
{~woof|bork|bark|barf|ruff|pant pant} {~woof|bork|bark|barf|ruff} {~woof|bork|bark|barf|ruff} {~woof|bork|bark|barf|ruff} {~woof|bork|bark|barf|ruff}...
// random selection of dog sounds x4
+ [...] -> knife_choice



// THIS IS THE BIG CHOICE, LOTS OF THINGS ARE ABOUT TO HAPPEN
// make sure to keep "leave" at the top, so that if the player accidentally selects something immediately, it's the "leave" option and not something more permanent
== knifeless_choice ==
~ setQuestText = "pick up the knife"
It's your dog. You have no way to effectively harm your dog with only your slimy form.
+ [Leave] -> later
+ [Spare] -> spare

== knife_choice ==
~ setQuestText = "kill your dog"
It's your dog. You have a knife. You must determine the fate of your dog.
+ [Leave] -> later
+ [KILL] -> kill
+ [Spare] -> spare

== later ==
You don't feel like dealing with this yet.
-> END

== kill ==
You thrust the knife into your dog.
~ setQuestText = "good job"
~ dogKilled = 1
-> END

== spare ==
You decide not to kill your dog.
~ setQuestText = "pathetic"
~ dogKilled = 2
-> END

// not sure if we're using these last two, but sure, why not
== dead ==
...
-> END

== not_dead ==
{~woof|bork|bark|barf|ruff|pant pant} {~woof|bork|bark|barf|ruff} {~woof|bork|bark|barf|ruff} {~woof|bork|bark|barf|ruff} {~woof|bork|bark|barf|ruff}!!!
-> END


// this needs to do a few complex things
// 1: Needs 2 different Unity-detectable outcomes
// i.e. kill dog or don't kill dog
// 2: Needs different text if the player didn't pick up the knife
