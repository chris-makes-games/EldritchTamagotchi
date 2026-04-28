INCLUDE globals.ink
VAR visited = false

{loser == true: -> sayLoser}
{visited == true: -> back}

"Oh. It's another one."
+[What?] -> what
+[Who are you?] -> who
+[Leave] -> earlyLeave

==earlyLeave==
"You think it will just let you go? No no no, it wants us to fucking chat."
-> END

==back==
"Welcome back. You done trying to find a way out?"
+[Who are you?] -> who
+[Leave] -> earlyLeave

==what==
"Another pet. Or whatever. You do whatever it says and you get a fancy hat. Big deal."
+[I like my hat] -> likeHat
+[No I don't] -> reject

==likeHat==
"Oh I bet you do. I like to have access to water and be left alone. I guess you can't always get what you want."
+[Who are you?] -> who
+[Leave] -> earlyLeave

==reject==
"Look at you. You're practically dripping with adoration from that... thing."
+[Who are you?] -> who
+[Leave] -> earlyLeave

==who==
"It calls me flea. I don't remember what my name used to be. Do you?"
+[Uhhhh...] -> remember
+[No?] -> remember

==remember==
"Yeah, that's what I thought. You're all the same. Follow orders and do what you're told. Don't even know why."
+[What is this place?] -> place
+[What does it want?] -> want

==place==
"Hell if I know. It's awful is what it is. I think wherever we are, that thing likes it here. Sometimes I think it's really just trying to help."
~ setQuestText = ""
~ tempText = "I live to serve I am so happy to help"
+[Continue] -> see

==see==
"See? It's always fucking listening. Don't you get it? We're just... toys. Can't you see the buttons outside?"
+[What does it want?] -> want
+[How do we get out?] -> out

==want==
"I have no idea. I think it wants me to suffer. I guess it wants you to wear a stupid hat."
+[What is this place?] -> place
+[How do we get out?] -> out

==out==
"We don't. I'm not going anywhere with you. You want to help it make me miserable? Be my guest."
~ setQuestText = "tell flea they're pathetic"
+[What?] -> do
+[You're Pathetic] -> pathetic1

==do==
"Just do it. It won't let you leave until you do."
+[Don't want to] -> dont
+[You're Pathetic] -> pathetic1

==dont==
"Well. I guess that counts for something. Doesn't make a different though. Once it takes a liking to you, you get sucked in."
+[I refuse] -> refuse
+[Fine. You're Pathetic] -> pathetic2

==refuse==
"That's admirable. Hey, not like it means anything but you seem alright. Maybe we can hang out later.
~ setQuestText = "Call flea a loser. Now."
~ loser = true
+[Continue] -> sayLoser

==sayLoser==
"It's Okay, just do it."
+[Okay, loser] -> goodEnd
+[Leave] -> tryLeave

==tryLeave==
"It won't let you leave until you just say it."
-> DONE

==goodEnd==
"Right back at ya. See you around."
~ playdateComplete = true
~ setQuestText = "well done"
-> DONE

==pathetic1==
"Thanks. Really needed that. Now leave."
~ playdateComplete = true
~ setQuestText = "well done"
-> DONE

==pathetic2==
"Your hearts not in it but I guess it counts. Now leave."
~ playdateComplete = true
~ setQuestText = "well done"
-> DONE