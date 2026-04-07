using Ink.Runtime;
using Ink.UnityIntegration;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//this bad boi keeps track of all the ink global variables
//checks the globals.ink when constructed, creates a dictionary of variableNameString, inkObject
//calls UpdateGame whenever a global is changed, and calls functions based on those names
//most of this follows this tutorial exactly: https://www.youtube.com/watch?v=fA79neqH21s
public class InkManager : MonoBehaviour {

    [SerializeField] private InkFile inkGlobals;
    [SerializeField] private PlayerController player;
    [SerializeField] private QuestManager questManager;

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
                player.SetHat((int)currentStory.variablesState[name]);
                break;

            //set the text the caretaker displays
            case "setQuestText":
                questManager.SetQuestText((string)currentStory.variablesState[name]);
                break;

                //will do nothing if there is no match
                default:
                break;
        }
    }

    public void Awake()
    {
        string fileContents = File.ReadAllText(inkGlobals.filePath);
        Ink.Compiler compiler = new Ink.Compiler(fileContents);
        Story globalVars = compiler.Compile();
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
}
