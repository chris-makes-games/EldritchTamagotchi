INCLUDE globals.ink
VAR visited = false

{playdateComplete == true: -> stupid1}
{visited == false: -> welcome}
{visited == true: -> back}

==welcome==
"Oh, hello! Welcome to my humble abode. My, you have an... interesting choice of hat. Lol. Lmao, even."
+[Rude?] -> rude
+[Leave] -> earlyLeave

==earlyLeave==
"Feel free to explore. My taste begets plenty of admiration. Come back when you're done."
-> END

==back==
"Welcome back. Like what we've done with the place?"
+[Who are you?] -> who
+[Leave] -> earlyLeave

==who==
"I'm Krent. Sah, dude. Bet you've never seen a hat quite like this one, huh? Jealous much?"
+[No] -> no
+[A little] -> jealous

==no==
"Yeah, right. Hey, how about you let me give you a little adivce. Life is too busy to make every choice for yourself. Just sit back and relax. Doesn't that seem nice?"
~ setQuestText = ""
~ tempText = "lie detected"
+[No] -> reject
+[Maybe a little] -> jealous

==jealous==
"It's alright, I get it. Hey, you want to be like me? Don't you want a sick fucking hat and a nice place to hang it?"
+[Yes] -> likeMe
+[No] -> reject

==likeMe==
"Well, duh. Who wouldn't, am I right? Let me give you some advice:"
+[Continue] -> cont

==reject==
"Liar. Whatever. You're not going to last very long down here with an attitude. Just get out of here, you're cramping my style."
~ playdateComplete = true
~ setQuestText = ""
~ tempText = "you need to work on your social skills"
-> END

==rude==
"Let me guess: you have a rebellious streak? You know, it doesn't need to be so hard."
+[Who are you?] -> who
+[Leave] -> earlyLeave

==stupid1==
"Hey caretaker, why is this weirdo stil here? Are they stupid?"
~ setQuestText = ""
~ tempText = "Yes, that's a great observation!"
-> stupid2

==stupid2==
"Get lost. I'm tired of looking at your sad little hat."
-> END

==cont==
"Life has too much going on to focus on making every single little choice for yourself. Bigger systems can run your life in better ways. The caretaker only wants what's best for you." :)
~ playdateComplete = true
~ setQuestText = ""
~ tempText = "well done"
-> END