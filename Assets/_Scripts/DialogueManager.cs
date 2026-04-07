using Ink.Runtime;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    //to "read" text by character
    public float delay = 0.040f; //delay between characters in seconds
    private bool readingText = false; //keep track of if characters are being read
    public string fullText; //keep track of what all the text should be

    //game objects to hold the panel and text and choices
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private TextMeshProUGUI body;

    [SerializeField] private SoundManager soundManager;

    //to allow player to select choices
    public InputAction select;

    //choices script and list of current choices
    private ChoiceManager choiceManager;
    string[] choiceList;
    private bool waitingchoice = false;

    //will listen to variables being changed in ink scripts
    [SerializeField] private InkManager InkManager;

    //keeps track of player - to release them when they're done interacting
    [SerializeField] private PlayerController player;

    //ink story JSON - recieves from the player
    private Story currentStory;

    //singleton instance
    private static DialogueManager instance;

    private void Awake()
    {
        instance = this;//ensure singleton
        select = InputSystem.actions.FindAction("Interact/Continue");
    }

    public static DialogueManager GetInstance()
    {
        return instance;//ensure singleton
    }

    private void Start()
    {
        //starts inactive until text is shown
        body.text = "";
        choiceManager = choicePanel.GetComponent<ChoiceManager>(); //grabs the choices manager script
    }

    
    public void EnterStoryMode(TextAsset ink, bool visited) //begin a story, accepts an ink
    {    
        PlayerController.instance.rb.linearVelocity = Vector2.zero;
        currentStory = new Story(ink.text);
        if (currentStory.variablesState.TryGetDefaultVariableValue("visited"))
        {
            Debug.Log("setting visited to " + visited);
            currentStory.variablesState["visited"] = visited;
        }
        dialoguePanel.SetActive(true);
        InkManager.StartListening(currentStory);
        ContinueStory();
    }

    public void ExitStoryMode()//when a story is done, exits
    {
        choiceManager.EndChoice(); //hides buttons and selector
        InkManager.StopListening(currentStory);
        player.endStory(); //lets player move again
        SetText(""); //clears the text
    }

    public void ContinueStory() //goes to next step of story
    {
        if (readingText) //stop reading text, show full text and return
        {
            StopAllCoroutines();
            body.text = fullText;
            choiceManager.startChoice(choiceList); //sets choices
            readingText = false; //resets reading
            return;
        }
        if (waitingchoice) //player makes a choice
        {
            waitingchoice = false;
            if (currentStory.currentChoices.Count > 0) //makes a choice
            {
                currentStory.ChooseChoiceIndex(choiceManager.getSelection());
                choiceManager.EndChoice();
            }
            else //no choice to make, ends the interaction
            {
                ExitStoryMode();
            }
        }

        if (currentStory.canContinue)
        {
            fullText = currentStory.Continue();
            SayText(fullText);
            if (currentStory.currentChoices.Count > 0) //if story has more than one choice
            {
                choiceList = new string[currentStory.currentChoices.Count];
                for (int i = 0; i < currentStory.currentChoices.Count; ++i)
                {
                    Choice choice = currentStory.currentChoices[i];
                    choiceList[i] = choice.text;
                }
            }
            else
            {
                choiceList = new string[1];
                choiceList[0] = "Leave";
            }
             //sends choices as strings to the choice manager
            waitingchoice = true;
        }
        else
        {
            ExitStoryMode(); //end story if that was the last step
        }
    }

    public Ink.Runtime.Object GetInkVariable(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        InkManager.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Error: Ink variable '" + variableName + "' not found! Returning null...");
        }
        return variableValue;
    }

    public void SetText(string s) //easy way to set the text, set to blank to erase
    {
        body.text = s;
        readingText = false;
    }

    public void SayText(string text) //to "say" the letters one by one
    {
        StartCoroutine(SlowText(text));
    }

    IEnumerator SlowText(string text) //waits for the delay in-between characters of the given text
    {
        readingText = true;
        string output = "";
        for (int i = 0; i < text.Length; i++)
        {
            output = output + text[i];
            body.text = output;
            int index = char.ToUpper(text[i]) - 65; //turns char to position in alphabet
            if (index >= 0 && index < 27)
            {
                soundManager.SpeakChar(index);
            }
            
            yield return new WaitForSeconds(delay);
        }
        choiceManager.startChoice(choiceList);
        readingText = false;
    }

}
