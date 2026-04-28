using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this bad boi keeps track of all the ink global variables
//checks the globals.ink when constructed, creates a dictionary of variableNameString, inkObject
//calls UpdateGame whenever a global is changed, and calls functions based on those names
//most of this follows this tutorial exactly: https://www.youtube.com/watch?v=fA79neqH21s
public class InkManager : MonoBehaviour {

    [SerializeField] private TextAsset inkGlobalsJSON;
    [SerializeField] private PlayerController player;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private GameObject musicManager;
    private AudioSource musicSource;
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private SleepManager sleepScreen;

    //disable this door when it's time for playdate
    [SerializeField] private GameObject lockedDoor;

    //hatsequence completed event
    public static event Action<int> HatEvent;

    //for fridge opening event
    public static event Action<bool> FridgeEvent;

    //fpor sink changes
    public static event Action<int> SinkEvent;

    //trying to make questmanager yelling phrases easier
    private List<string> phrases;
    private Story currentStory;
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    //this is a singleton object, this line makes the instance static
    private static InkManager _instance;
    public static InkManager instance { get { return _instance; } }
    // i'm not even gonna try to explain what this is -owen
    private EvilEvents evilManager;

    //switch to change things -not sure if better system?
    //can only take ints for now
    private void UpdateGame(string name)
    {
        switch (name)
        {
            case "tempText":
                Debug.Log((string)currentStory.variablesState[name]);
                StartCoroutine(questManager.TempText((string)currentStory.variablesState[name]));
                break;
            //update the player's hat status after they put on correct/incorrect hat
            case "currentHat":
                Debug.Log("currentHat changed to: " + (int)currentStory.variablesState[name]);
                if ((bool)currentStory.variablesState["hatTime"])
                {
                    if ((int)currentStory.variablesState[name] == (int)currentStory.variablesState["hatNeeded"])
                    {
                        setVariable("correctHat", 1); //sets the correcthat variable to correct
                        questManager.SetQuestText("You look great");
                    }
                    else
                    {
                        if ((int)currentStory.variablesState[name] == 5)
                        {
                            phrases = new List<string> {"you look naked", "I recommend this headwear instead"};
                        }
                        else
                        {
                            phrases = new List<string> {"you look ridiculous", "I recommend this headwear instead"};
                        }
                        setVariable("correctHat", 2); //sets the correcthat variable to incorrect
                        StartCoroutine(questManager.ChainText(phrases));
                        StartCoroutine(DelayedHat(8f, 4));
                    }
                    HatEvent?.Invoke((int)currentStory.variablesState["correctHat"]);
                    StartCoroutine(DelayedDoor(14f));
                }
                break;
            //sets player hat - during hat dresser deciding
            case "hat":
                player.SetHat((int)currentStory.variablesState[name]);
                if (!(bool)currentStory.variablesState["hatTime"]) //if it isn't yet hat time, swap hatNeeded
                {
                    switch ((int)currentStory.variablesState[name])
                    {
                        case 0:
                            setVariable("hatNeeded", 1); // if party, go cowboy
                            break;
                        case 1:
                            setVariable("hatNeeded", 2); //if cowboy, go spinny
                            break;
                        case 2:
                            setVariable("hatNeeded", 3); //if spinny, go paper
                            break;
                        case 3:
                            setVariable("hatNeeded", 0); //if paper, go party
                            break;
                        //there is no case 4, player cannot wear dunce by choice
                        case 5:
                            setVariable("hatNeeded", 3); //if bald, go spinny
                            break;
                    }
                }
                break;
            case "fridgeOpen":
                if ((bool)currentStory.variablesState[name])
                {
                    FridgeEvent?.Invoke(true);
                }
                else
                {
                    FridgeEvent?.Invoke(false);
                }

                    break;

            case "sinkRunning":
                if ((int)currentStory.variablesState[name] == 0)
                {
                    SinkEvent?.Invoke(0); //turn off if 0 in any case
                }
                else if ((bool)currentStory.variablesState["sinkFix"])
                {
                    SinkEvent?.Invoke(2); //turn blue if fixed and not 0
                }
                else
                {
                    SinkEvent?.Invoke(1); //turn black if not fixed yet and not 0
                }

                    break;
            //set the text the caretaker displays
            case "setQuestText":
                questManager.SetQuestText((string)currentStory.variablesState[name]);
                break;

            case "awaken":
                sleepScreen.Awaken();
                phrases = new List<string> {"good morning", "i'm you're new caretaker", "why don't you relax and drink some water"};
                StartCoroutine(questManager.ChainText(phrases));
                musicSource.clip = musicClip;
                musicSource.volume = 0.15f;
                musicSource.Play();
                break;

            case "sinkFix":
                phrases = new List<string> {"that's a great catch!", "the sink does not dispense water", "let me fix that for you", "drink from the sink now"};
                setVariable("sinkRunning", 0);
                SinkEvent?.Invoke(0);
                StartCoroutine(questManager.ChainText(phrases));
                break;

            case "drank":
                if ((int)currentStory.variablesState[name] == 1)
                {
                    phrases = new List<string> { "great work!", "hydration is very important"};
                    
                    switch ((int)currentStory.variablesState["hatNeeded"])
                    {
                        case 0:
                            phrases.Add("Now: put on the party hat");
                            break;
                        case 1:
                            phrases.Add("Now: put on the cowboy hat");
                            break;
                        case 2:
                            phrases.Add("Now: put on the propeller hat");
                            break;
                        case 3:
                            phrases.Add("Now: put on the paper crown");
                            break;
                    }
                }
                else
                {
                    phrases = new List<string> {"suit yourself", "More precious coolant for me"};
                    switch ((int)currentStory.variablesState["hatNeeded"])
                    {
                        case 0:
                            phrases.Add("Now: put on the party hat");
                            break;
                        case 1:
                            phrases.Add("Now: put on the cowboy hat");
                            break;
                        case 2:
                            phrases.Add("Now: put on the propeller hat");
                            break;
                        case 3:
                            phrases.Add("Now: put on the paper crown");
                            break;
                    }
                }
                StartCoroutine(questManager.ChainText(phrases));
                break;

            case "evilReady":
                StartCoroutine(questManager.LoadScene("EvilMain"));
                break;
            case "killTime":
                //bool for it's time to kill the dog (or not)
                //might want to do a questManager.SetQuestText("Kill the Dog") here
                break;

            case "holdingKnife":
                //bool for if the player is holding the knife or not
                break;

            case "dogKilled":
                //int value, 0 is the default, no choices has been made
                // 1 is dog was killed
                // 2 is dog was not killed
                // you can get the int that it is by (int)currentStory.variablesState[name]
                if ((int)currentStory.variablesState["dogKilled"] == 1)
                {
                    Dog.instance.dead = true;
                    evilManager = GetComponent<EvilEvents>();
                    evilManager.ending = true;
                    evilManager.EndFade();
                }
                else if ((int)currentStory.variablesState["dogKilled"] == 2)
                {
                    evilManager = GetComponent<EvilEvents>();
                    evilManager.ending = true;
                    evilManager.EndFade();
                }
                break;

            //will do nothing if there is no match
            default:
                break;
        }
    }

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        musicSource = musicManager.GetComponent<AudioSource>();
        Story globalVars = new Story(inkGlobalsJSON.text);
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVars.variablesState)
        {
            Ink.Runtime.Object value = globalVars.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
        }

    }

    public void StartListening(Story story) //starts watching for ink story to change variables
    {
        currentStory = story;
        SetVariables(currentStory);
        currentStory.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story) //needs to stop listening when donw
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value) //called whenever a global var changes
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
            UpdateGame(name);
        }
    }

    private void SetVariables(Story story) //turns globals into dictionary
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    public void setVariable<T>(string name, T value)
    {
        if (variables.ContainsKey(name))
        {
            if (currentStory.variablesState[name].GetType() != typeof(T))
            {
                Debug.Log("InkManager error: tried to set variable as the wrong type");
                Debug.Log("variable: " + name);
                Debug.Log("value: " + value);
                Debug.Log("type is: " + value.GetType());
                Debug.Log("type should be: " + variables[name].GetType());
                return;
            }
            currentStory.variablesState[name] = value;
            Ink.Runtime.Object ink = variables[name];
            variables.Remove(name);
            variables.Add(name, ink);
            UpdateGame(name);
        }
    }

    public T getVariable<T>(string name) //gets any variable from the dict by string name
    {
        return (T)currentStory.variablesState[name];
    }

    IEnumerator DelayedHat(float seconds, int hat)
    {
        yield return new WaitForSeconds(seconds);
        player.SetHat(hat);
    }

    IEnumerator DelayedDoor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        phrases = new List<string> { "it's time you met a new friend", "lonliness is bad for your mental health", "I have unlocked the security door" };
        StartCoroutine(questManager.ChainText(phrases));
        yield return new WaitForSeconds(8f);
        lockedDoor.SetActive(false);
    }
}
