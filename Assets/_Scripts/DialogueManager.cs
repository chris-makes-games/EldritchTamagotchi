using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    //gmae objects to hold the panel and text
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    //ink story JSON - recieves from the player
    private Story currentStory;

    //holds all story lines
    private string[] allStories;
    public int storyNumber;

    //keep track of if a story is playing - to wait when a player needs to make a choice
    private bool storyPlaying;

    //singleton instance
    private static DialogueManager instance;

    private void Awake()
    {
        instance = this;//ensure singleton
    }

    public static DialogueManager GetInstance()
    {
        return instance;//ensure singleton
    }

    private void Start()
    {
        //starts inactive until text is shown
        storyPlaying = false;
        dialoguePanel.SetActive(false);

        //creates the array
        allStories = new string[storyNumber];
    }

    
    public void EnterStoryMode(TextAsset ink) //begin a story, accepts an ink
    {
        string current = ink.text;
        string next = null;
        for (int i = 0; i < storyNumber - 1; i++)
        {
            next = allStories[i];
            allStories[i] = current;
            current = next;
        }
        currentStory = new Story(ink.text);
        storyPlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    private void Update() //brought in from the tutorial, not sure if needed
    {
        if (!storyPlaying)
        {
            return;
        }
    }

    private void ExitDialogueMode()//when a story is done, exits
    {
        storyPlaying = false;
    }

    private void ContinueStory() //goes to next step of story
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }
}
