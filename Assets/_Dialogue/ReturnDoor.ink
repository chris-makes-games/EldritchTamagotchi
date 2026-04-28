INCLUDE globals.ink

{playdateComplete == true: -> yes}
{loser == true: -> sayLoser}
{playdateComplete == false: -> no}

==sayLoser==
You try the handle, but it won't budge.
~ setQuestText = "convince flea they're a pathetic loser"
~ tempText = "I'm afraid I can't let you do that"
-> END

==no==
Return to your quarters?
+[Return] -> denied
+[Stay Here] -> stay

==denied==
You try the handle but it won't budge.
~ setQuestText = "You need to talk to your new friend"
~ tempText = "I'm afraid I can't let you do that"
-> END

==stay==
You decide to stay for now.
-> END

==yes==
Return to your quarters?
+[Return] -> exit
+[Stay Here] -> stay

==exit==
You leave.
~ evilReady = true
-> END 