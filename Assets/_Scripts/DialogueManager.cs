using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    //gmae objects to hold the panel and text
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI text0;
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;
    [SerializeField] private TextMeshProUGUI text3;

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
        text0.text = "";
        text1.text = "";
        text2.text = "";
        text3.text = "";
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

    private void ExitDialogueMode()//when a story is done, exits
    {
        storyPlaying = false;
    }

    private void ContinueStory() //goes to next step of story
    {
        if (currentStory.canContinue)
        {
            text3.text = text2.text;
            text2.text = text1.text;
            text1.text = text0.text;
            text0.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }
}
