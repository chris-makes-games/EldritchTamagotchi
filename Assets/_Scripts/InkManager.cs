using Ink.Runtime;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class InkManager {

    private Story currentStory;

    private PlayerController player;
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

                //will do nothing if there is no match
                default:
                break;
        }
    }

    public InkManager(string globalsPath, PlayerController player)
    {
        this.player = player;
        string fileContents = File.ReadAllText(globalsPath);
        Ink.Compiler compiler = new Ink.Compiler(fileContents);
        Story globalVars = compiler.Compile();

        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVars.variablesState)
        {
            Ink.Runtime.Object value = globalVars.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
        }
    }

    public void StartListening(Story story)
    {
        currentStory = story;
        SetVariables(currentStory);
        currentStory.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
            UpdateGame(name);
        }
    }

    private void SetVariables(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
