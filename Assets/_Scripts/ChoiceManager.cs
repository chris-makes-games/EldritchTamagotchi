using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChoiceManager : MonoBehaviour
{
    //manager script
    [SerializeField] DialogueManager dialogueManager;

    //choice button objects
    [SerializeField] private GameObject[] choices;
    [SerializeField] private TMP_Text[] textList;
    [SerializeField] private GameObject selector;
    
    //keep track of state
    bool choosing = false;
    bool choiceWait = false;
    int currentChoice = 0;
    int numberOfChoices = 0;

    //input direction for choice
    //using same input as move for player
    public InputAction choiceInput;
    private Vector2 moveDirection;

    private void Awake()
    {
        choiceInput = InputSystem.actions.FindAction("Move");
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //sets all the buttons to inactive to begin
        for (int i = 0; i < choices.Length; i++)
        {
            textList[i] = choices[i].GetComponentInChildren<TMP_Text>();
            choices[i].SetActive(false);
        }
        selector.SetActive(false);

    }

    private void FixedUpdate()
    {
        if (choosing)
        {
            if (moveDirection.x > 0 && !choiceWait)
            {
                choiceWait = true;
                moveCursor(true);
            }
            else if (moveDirection.x < 0 && !choiceWait) 
            { 
                choiceWait = true;
                moveCursor(false);
            }
            if (moveDirection.x == 0)
            {
                choiceWait = false;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (choosing)
        {
            moveDirection = choiceInput.ReadValue<Vector2>();
        }
        
    }

    public void startChoice(string[] newChoices)
    {
        numberOfChoices = newChoices.Length;
        choosing = true;
        selector.SetActive(true);
        for (int i = 0; i < newChoices.Length; i++)
        {
            choices[i].SetActive(true);
            textList[i].text = newChoices[i];
        }

    }
    private void moveCursor(bool forward)
    {
        if (forward) //wrap forwards
        {
            if (currentChoice == numberOfChoices - 1)
            {
                currentChoice = 0;
            }
            else
            {
                currentChoice++;
            }
            
        }
        else //wrap backwards
        {
            if (currentChoice == 0)
            {
                currentChoice = numberOfChoices - 1;
            }
            else
            {
                currentChoice--;
            }
            
        }
        //set position based on current choice button position
        Vector2 newPos = new Vector2(choices[currentChoice].transform.localPosition.x, selector.transform.localPosition.y);
        selector.transform.localPosition = newPos;

    }

    public void EndChoice()
    {
        choosing = false;
        currentChoice = 0;
        numberOfChoices = 0;
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }
        Vector2 newPos = new Vector2(choices[0].transform.localPosition.x, selector.transform.localPosition.y);
        selector.transform.localPosition = newPos;
        selector.SetActive(false);
    }

    public int getSelection()
    {
        return currentChoice;
    }
}
