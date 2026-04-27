INCLUDE globals.ink

//ORDER:
//party_hat - 0
//cow_hat - 1
//spin_hat - 2
//burg_hat - 3
//dunce_hat - 4 (unused here)
//empty - 5

//locked after hat sequence is finished
{correctHat > 0: -> locked}

It's your dresser. It is exclusively filled with peculiar hats.
+ [Put on a hat] -> hatmode
+{hat == 5}[Leave] -> later

==later==
You decide not to try on a hat.
-> DONE

=== leave_hat ===
You feel incredibly stylish. You close the drawers.
~ currentHat = hat
-> DONE

=== leave_nohat ===
You decide not to put on a hat, content with your lack of style.
~ currentHat = hat
-> DONE

=== hatmode === // at most only 5 options, i know it looks scary though
{hat_text}
+{hat != 0}[Party Hat] -> party
+{hat != 1}[Cowboy Hat] -> cowboy
+{hat != 2}[Propeller Hat] -> spinny
+{hat != 3}[Paper Crown] -> burger
+{hat != 5}[Remove Hat] -> remove
+{hat < 5}[Done] -> leave_hat
+{hat == 5}[Leave] -> leave_nohat

=== party ===
You put on the party hat. You are ready for the cake.
~ hat_text = "You are wearing the party hat. Try a different hat?"
~ hat = 0
+[Continue]-> hatmode

=== cowboy ===
You put on the cowboy hat. You feel rowdy.
~ hat_text = "You are wearing the cowboy hat. Try a different hat?"
~ hat = 1
+[Continue]-> hatmode

=== spinny ===
You put on the propeller hat. You don't really know why it has a propeller.
~ hat_text = "You are wearing the propeller hat. Try a different hat?"
~ hat = 2
+[Continue]-> hatmode

=== burger ===
You put on the paper crown. You feel powerful.
~ hat_text = "You are wearing the paper crown. Try a different hat?"
~ hat = 3
+[Continue]-> hatmode

=== remove ===
You take off your hat. Your bald head feels cold in the wind.
~ hat_text = "You are no longer wearing a hat. Try a hat?"
~ hat = 5
+[Continue]-> hatmode

===locked===
The dresser appears to be locked, you are stuck wearing this hat.
-> DONE
