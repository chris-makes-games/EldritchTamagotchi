using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    //gmae objects to hold the panel and text and choices
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private TextMeshProUGUI body;

    //ink story JSON - recieves from the player
    private Story currentStory;

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
        choicePanel.SetActive(false);
        body.text = "";
    }

    
    public void EnterStoryMode(TextAsset ink) //begin a story, accepts an ink
    {    
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

    public void ExitStoryMode()//when a story is done, exits
    {
        storyPlaying = false;
        body.text = "";
    }

    private void ContinueStory() //goes to next step of story
    {
        if (currentStory.canContinue)
        {
            body.text = currentStory.Continue();
        }
        else
        {
            ExitStoryMode();
        }
    }

    public void SetText(string s)
    {
        body.text = s;
    }
}
