EXTERNAL SetHat(hatChoice)
VAR hatchoice = 0
VAR coins = 0
VAR hat_text = "Now it is time to choose a hat!"
VAR spin_text = " There is a hat in a glass case separate from the other hats"
VAR hat = 0
VAR spin = 0
VAR cow_hat = 0
VAR dunce_hat = 0
VAR burg_hat = 0
VAR spin_hat = 0
Hello, test unity stuff
+[Continue] ->second

=== second ===
Here is the second part of the dialogue, with more text. You can ask to have this read again if you weren't paying attention. You can ask for a coin, but you may only do so once.
+[Say again]->second
*[Give Coin]->coin
+[Continue]->third

=== coin ===
You have been given a coin. Cherish it forever.
~ coins = 1
+[Continue]-> second

=== third ===
{hat_text}{spin_text}
+{cow_hat < 1}[Cowboy Hat]->cowboy
+{dunce_hat < 1}[Dunce Cap]->dunce
+{burg_hat < 1}[Burger King Crown]->burger
+{coins > 0} {spin < 1} [Examine Glass]->glass
+{spin == 1} {spin_hat < 1}[Spinny Hat]->spinny
*{hat > 0} [Done] -> confirm

=== cowboy ===
You put on the cowboy hat. You feel rowdy.
~ hat_text = "You are wearing the Cowboy Hat. Try a different hat?"
~ hatchoice = 0
~ cow_hat = 1
~ dunce_hat = 0
~ burg_hat = 0
~ spin_hat = 0
~ hat = 1
+[Continue]-> third

=== dunce ===
You put on the dunce cap. You feel stupid.
~ hat_text = "You are wearing the dunce cap. Try a different hat?"
~ hatchoice = 1
~ cow_hat = 0
~ dunce_hat = 1
~ burg_hat = 0
~ spin_hat = 0
~ hat = 1
+[Continue]-> third

=== burger ===
You put on the carboard crown from Burger King. You feel powerful.
~ hat_text = "You are wearing the Burger King crown. Try a different hat?"
~ hatchoice = 2
~ cow_hat = 0
~ dunce_hat = 0
~ burg_hat = 1
~ spin_hat = 0
~ hat = 1
+[Continue]-> third

=== spinny ===
You put on the spinny hat. You feel deep regret.
~ hat_text = "You are wearing the Spinny Hat. Try a different hat?"
~ hatchoice = 3
~ cow_hat = 0
~ dunce_hat = 0
~ burg_hat = 0
~ spin_hat = 1
~ hat = 1
+[Continue]-> third

=== glass ===
You discover a hat locked behind a microtransaction. There is a slot for a coin to unlock it. The hat has a little spinny propeller on it. You feel the weight of the coin. The hat looks amazing.
+[Buy Hat]->buy
+[Ignore]->third

=== buy ===
You purchase the spinny hat. The coin falls into the slot with a thunk. The glass opens and you retrieve the spinny hat.
~ spin_text = " The glass case is empty, your coin is lost forever."
~ coins = 0
~ spin = 1
+[Continue]->third

=== confirm ===
You feel good about your hat choice. You close the dresser.
~ SetHat(hatchoice)
-> DONE
