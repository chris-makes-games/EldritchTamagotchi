INCLUDE globals.ink
VAR visited = false
VAR kill = -1

// this needs to do a few complex things
// 1: Needs 2 different Unity-detectable outcomes
// i.e. kill dog or don't kill dog
// 2: Needs different text if the player didn't pick up the knife
