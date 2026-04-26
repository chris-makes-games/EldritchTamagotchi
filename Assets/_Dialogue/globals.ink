//set for intro cinematic until player awakes
VAR awaken = false

//increments or decrements love for ending calculation
VAR love = 0

//unused for now, track how many interactions if you want
VAR totalInteractions = 0

//current hat being worn
VAR hat = 5
// 0 - Party Hat
// 1 - Cowboy Hat
// 2 - Spinny Hat
// 3 - Paper Crown
// 4 - Dunce - only used for caretaker
// 5 - No Hat (default)
VAR currentHat = 5
//sets the string for what hat currently wearing
VAR hat_text = "Many hats sit before you, begging to be worn."

//setting this will make the caretaker say it
VAR setQuestText = ""

//keeps track of fridge status
VAR fridgeOpen = false

//sink variables
VAR sinkTurned = false
VAR sinkFix = false
VAR waterTemp = ""
VAR sinkRunning = false
VAR drank = 0

//hat status variables
VAR correctHat = false
VAR hatTime = false
VAR hatNeeded = 5


//endgame variables
VAR dogKilled = 0 //0 is before, 1 is killed, 2 is spared
VAR killTime = false //when it's time to kill the dog
VAR holdingKnife = false //picked up knife?