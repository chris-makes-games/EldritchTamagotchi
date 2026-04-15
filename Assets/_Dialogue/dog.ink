INCLUDE globals.ink
{~woof|bork|bark|barf|ruff|pant pant} {~woof|bork|bark|barf|ruff} {~woof|bork|bark|barf|ruff} {~woof|bork|bark|barf|ruff} {~woof|bork|bark|barf|ruff}!!!
// random selection of dog sounds x4

 + [Pet]-> pet
 + [Leave]-> leave
 
=== pet ===
You pet the dog. {~You feel a little slimier.|You feel like a good owner.|Good... boy? Girl? Dog?}
 + [Pet again]-> pet
 + [Leave]-> leave

=== leave ===
{~whine|whimper}...
~ setQuestText = "KILL YOUR DOG"
-> DONE
