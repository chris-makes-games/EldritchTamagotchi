using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Runtime.CompilerServices;

public class DialogueManager : MonoBehaviour
{
     
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private Story currentStory;
    private bool storyPlaying;

    private static DialogueManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = new DialogueManager();
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        storyPlaying = false;
        dialoguePanel.SetActive(false);
    }

    public void EnterStoryMode(TextAsset ink)
    {
        currentStory = new Story(ink.text);
        storyPlaying = true;
        dialoguePanel.SetActive(true);

        if (currentStory.canContinue) 
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }

    }

    private void Update()
    {
        if (!storyPlaying)
        {
            return;
        }
    }

    private void ExitDialogueMode()
    {
        storyPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
}
