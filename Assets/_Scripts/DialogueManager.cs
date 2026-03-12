using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    //game objects to hold the panel and text and choices
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private TextMeshProUGUI body;

    //to allow player to select choices
    public InputAction select;

    //choices script and list of current choices
    private ChoiceManager choiceManager;
    string[] choiceList;
    private bool waitingchoice = false;

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

    
    public void EnterStoryMode(TextAsset ink) //begin a story, accepts an ink
    {    
        currentStory = new Story(ink.text);
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    public void ExitStoryMode()//when a story is done, exits
    {
        Debug.Log("exiting story...");
        choiceManager.EndChoice(); //hides buttons and selector
        player.endStory(); //lets player move again
    }

    public void ContinueStory() //goes to next step of story
    {
        if (waitingchoice) //player makes a choice
        {
            waitingchoice = false;
            if (currentStory.currentChoices.Count > 0) //makes a choice
            {
                currentStory.ChooseChoiceIndex(choiceManager.getSelection());
                body.text = currentStory.Continue();
                choiceManager.EndChoice();
                ContinueStory();
            }
            else //no choice to make, ends the interaction
            {
                ExitStoryMode();
            }
        }

        if (currentStory.canContinue)
        {
            Debug.Log("continue? " + currentStory.canContinue);
            body.text = currentStory.Continue();
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
                choiceList[0] = "Continue";
            }
            choiceManager.startChoice(choiceList); //sends choices as strings to the choice manager
            waitingchoice = true;
        }
        else
        {
            ExitStoryMode(); //end story if that was the last step
        }
    }

    public void SetText(string s)
    {
        body.text = s;
    }

}
