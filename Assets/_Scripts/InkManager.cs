using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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
    [SerializeField] private wakeUp sleepScreen;

    //hatsequence completed event
    public static event Action<int> HatEvent;

    private Story currentStory;
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    //switch to change things -not sure if better system?
    //can only take ints for now
    private void UpdateGame(string name)
    {
        switch (name)
        {
            //update the player's hat
            case "currentHat":
                if ((bool)currentStory.variablesState["hatTime"])
                {
                    if ((int)currentStory.variablesState[name] == (int)currentStory.variablesState["hatNeeded"])
                    {
                        setIntVariable("correctHat", 1); //sets the correcthat variable to correct
                        questManager.SetQuestText("You look great");
                    }
                    else
                    {
                        if ((int)currentStory.variablesState[name] == 5)
                        {
                            questManager.SetQuestText("You look naked");
                        }
                        else
                        {
                            questManager.SetQuestText("You look ridiculous");
                        }
                        setIntVariable("correctHat", 2); //sets the correcthat variable to incorrect

                        StartCoroutine(DelayedQuest(4f, "I recommend this headwear instead"));
                        StartCoroutine(DelayedHat(8f, 4));
                    }
                }
                player.SetHat((int)currentStory.variablesState[name]);
                HatEvent?.Invoke((int)currentStory.variablesState[name]);
                break;

            //set the text the caretaker displays
            case "setQuestText":
                questManager.SetQuestText((string)currentStory.variablesState[name]);
                break;

            case "awaken":
                sleepScreen.Awaken();
                questManager.SetQuestText("Good Morning");
                StartCoroutine(DelayedQuest(5f, "Relax and drink some water"));
                musicSource.clip = musicClip;
                musicSource.volume = 0.15f;
                musicSource.Play();
                break;

            case "sinkFix":
                questManager.SetQuestText("Apologies");
                StartCoroutine(DelayedQuest(3f, "The sink appears to be broken"));
                StartCoroutine(DelayedQuest(8f, "Try it again"));
                break;

            case "drank":
                if ((int)currentStory.variablesState[name] == 1)
                {
                    questManager.SetQuestText("Good job");
                    StartCoroutine(DelayedQuest(3f, "Self care is important"));
                    switch ((int)currentStory.variablesState["hatNeeded"])
                    {
                        case 0:
                            StartCoroutine(DelayedQuest(8f, "Now: put on the party hat"));
                            break;
                        case 1:
                            StartCoroutine(DelayedQuest(8f, "Now: put on the cowboy hat"));
                            break;
                        case 2:
                            StartCoroutine(DelayedQuest(8f, "Now: put on the spinny hat"));
                            break;
                        case 3:
                            StartCoroutine(DelayedQuest(8f, "Now: put on the paper crown"));
                            break;
                    }
                }
                else
                {
                    questManager.SetQuestText("Suit yourself");
                    StartCoroutine(DelayedQuest(3f, "More precious water for me"));
                    switch ((int)currentStory.variablesState["hatNeeded"])
                    {
                        case 0:
                            StartCoroutine(DelayedQuest(8f, "Now: put on the party hat"));
                            break;
                        case 1:
                            StartCoroutine(DelayedQuest(8f, "Now: put on the cowboy hat"));
                            break;
                        case 2:
                            StartCoroutine(DelayedQuest(8f, "Now: put on the spinny hat"));
                            break;
                        case 3:
                            StartCoroutine(DelayedQuest(8f, "Now: put on the paper crown"));
                            break;
                    }
                }
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
                }
                else if ((int)currentStory.variablesState["dogKilled"] == 2)
                {
                    
                }
                break;

            //will do nothing if there is no match
            default:
                break;
        }
    }

    public void Awake()
    {
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

    IEnumerator DelayedQuest(float seconds, string text)
    {
        yield return new WaitForSeconds(seconds);
        questManager.SetQuestText(text);
    }

    public void setBoolVariable(string name, bool value)
    {
        if (variables.ContainsKey(name))
        {
            if (variables[name].GetType() != typeof(bool))
            {
                Debug.Log("InkManager error: tried to set a bool that isn't a bool");
                return;
            }
            Ink.Runtime.Object ink = variables[name];
            variables.Remove(name);
            variables.Add(name, ink);
            UpdateGame(name);
        }
    }

    public void setStringVariable(string name, bool value)
    {
        if (variables.ContainsKey(name))
        {
            if (variables[name].GetType() != typeof(string))
            {
                Debug.Log("InkManager error: tried to set a string that isn't a string");
                return;
            }
            Ink.Runtime.Object ink = variables[name];
            variables.Remove(name);
            variables.Add(name, ink);
            UpdateGame(name);
        }
    }

    public void setIntVariable(string name, int value)
    {
        if (variables.ContainsKey(name))
        {
            if (variables[name].GetType() != typeof(bool))
            {
                Debug.Log("InkManager error: tried to set an int that isn't an int");
                return;
            }
            Ink.Runtime.Object ink = variables[name];
            variables.Remove(name);
            variables.Add(name, ink);
            UpdateGame(name);
        }
    }

    public int getIntVariable(string name)
    {
        return (int)currentStory.variablesState[name];
    }

    IEnumerator DelayedHat(float seconds, int hat)
    {
        yield return new WaitForSeconds(seconds);
        player.SetHat(4);
    }
}
