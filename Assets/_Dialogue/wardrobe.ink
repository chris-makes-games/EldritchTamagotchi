VAR hat = 0
VAR hat_text = "Many hats sit before you, begging to be worn."
VAR cow_hat = 0
VAR party_hat = 0
VAR burg_hat = 0
VAR spin_hat = 0


It's your wardrobe. It is exclusively filled with peculiar hats.
+ [Put on a hat.] -> hatmode
+{hat == 1}[Done.] -> leave_hat // this one shouldn't come up
+{hat == 0}[Done.] -> leave_nohat

=== leave_hat ===
You feel incredibly stylish. You close the wardrobe.
-> DONE

=== leave_nohat ===
You decide not to put on a hat, content with your lack of style.
-> DONE

=== hatmode === // at most only 5 options, i know it looks scary though
{hat_text}
+{cow_hat < 1}[Cowboy Hat.] -> cowboy
+{party_hat < 1}[Party Hat.] -> party
+{burg_hat < 1}[Paper Crown.] -> burger
+{spin_hat < 1}[Propeller Hat.] -> spinny
+{hat == 1}[Hatless.] -> remove
+{hat == 1}[Done.] -> leave_hat
+{hat == 0}[Done.] -> leave_nohat

=== cowboy ===
You put on the cowboy hat. You feel rowdy.
~ hat_text = "You are wearing the cowboy hat. Try a different hat?"
~ cow_hat = 1
~ party_hat = 0
~ burg_hat = 0
~ spin_hat = 0
~ hat = 1
+[Continue]-> hatmode

=== party ===
You put on the party hat. You are ready for the cake.
~ hat_text = "You are wearing the party hat. Try a different hat?"
~ cow_hat = 0
~ party_hat = 1
~ burg_hat = 0
~ spin_hat = 0
~ hat = 1
+[Continue]-> hatmode

=== burger ===
You put on the paper crown. You feel powerful.
~ hat_text = "You are wearing the paper crown. Try a different hat?"
~ cow_hat = 0
~ party_hat = 0
~ burg_hat = 1
~ spin_hat = 0
~ hat = 1
+[Continue]-> hatmode

=== spinny ===
You put on the propeller hat. You don't really know why it has a propeller.
~ hat_text = "You are wearing the propeller hat. Try a different hat?"
~ cow_hat = 0
~ party_hat = 0
~ burg_hat = 0
~ spin_hat = 1
~ hat = 1
+[Continue]-> hatmode

=== remove ===
You take off your hat. Your bald head feels cold in the wind.
~ hat_text = "You are no longer wearing a hat. Try a hat?"
~ cow_hat = 0
~ party_hat = 0
~ burg_hat = 0
~ spin_hat = 0
~ hat = 0
+[Continue]-> hatmode
