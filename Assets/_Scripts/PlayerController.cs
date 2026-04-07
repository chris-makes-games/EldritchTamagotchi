using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    // movement variables
    public Rigidbody2D rb;
    public float moveSpeed;
    private Vector2 moveDirection, dodgeDirection;
    private bool dodging = false;
    public float dodgeSpeed, dodgeSpeedMin, dodgeSpeedDrop;

    //hats!
    [SerializeField] public Sprite[] hats; //list of hat sprites
    public GameObject hat; //the child hat object
    private SpriteRenderer hatSprite;

    // sprite/animation variables
    public SpriteRenderer sr;
    public Sprite initSprite, fuckYou;

    //interaction variables
    public DialogueManager DialogueManager;
    private Interactable interactionObject; //the thing being interacted with
    bool interactable = false;
    TextAsset story = null;
    bool canMove = true; //for toggling player movement
    bool storyMode = false;
    bool waitInput = false; //so player doesn't press interact again
    bool storyVisited; // used to keep track of if the player has interacted there before

    // input variables
    public InputActionAsset InputActions;
    public InputAction move;
    public InputAction bird;
    public InputAction dodge;
    public InputAction interact;
    public InputAction menu;
    public bool birdActive;

    //to play sounds!
    AudioSource soundSource;
    [SerializeField] AudioClip selectSound;
    [SerializeField] AudioClip errorSound;
    [SerializeField] AudioClip[] walkSounds;
    public float walkSoundDelay = 0.02f;
    private bool walkSoundPlaying = false;
    
    void OnEnable() {
        InputActions.FindActionMap("Player").Enable();
    }
    
    void Awake() {
        instance = this;
        
        move = InputSystem.actions.FindAction("Move");
        bird = InputSystem.actions.FindAction("FlipTheBird");
        dodge = InputSystem.actions.FindAction("Dodge");
        interact = InputSystem.actions.FindAction("Interact/Continue");
        menu = InputSystem.actions.FindAction("Pause/Quit");

        hatSprite = hat.GetComponent<SpriteRenderer>();

        soundSource = GetComponent<AudioSource>();

    }

    void FixedUpdate()
    {
        if (!dodging && canMove) {
            // normal movement
            rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }

        // logic for timing/speed decrease while dodging
        // prevents normal movement while dodging (commit to action)
        if (dodging && dodgeSpeed > dodgeSpeedMin) {
            dodgeSpeed -= dodgeSpeed * dodgeSpeedDrop;
        }
        if (dodgeSpeed <= dodgeSpeedMin) dodging = false;
    }

    void Update()
    {
        // reading movement input (joystick, wasd, whatever)
        moveDirection = move.ReadValue<Vector2>();

        // return to start menu with pause/quit button 
        // (i didn't know where else to put this)
        if (menu.WasPressedThisFrame()) {
            Cursor.visible = true;
            Debug.Log("Returning to main menu...");
            SceneManager.LoadScene("StartMenu");
        }

        // middle finger logic
        // might need to make this it's own method call
        // once (or if) animations are implemented to deal with that
        if (bird.WasPressedThisFrame()) {
            birdActive = true;
            sr.sprite = fuckYou;
        }
        if (bird.WasReleasedThisFrame()) {
            birdActive = true;
            sr.sprite = initSprite;
        }

        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            WalkSound();
        }

        // this is admittedly a very fiddly dodge roll
        if (dodge.WasPressedThisFrame()) {
            dodgeSpeed = 15;
            dodgeDirection = moveDirection;
            rb.linearVelocity = dodgeDirection * dodgeSpeed;
            dodging = true;
        }

        if (interact.WasPressedThisFrame())
        {
            if (interactable && !storyMode && !waitInput)
            {
                soundSource.clip = selectSound;
                soundSource.Play();
                DialogueManager.EnterStoryMode(story, storyVisited);
                interactionObject.ToggleHighlight(); //turns highlight off when you interact
                startStory();
                waitInput = true;
            }
            else if (storyMode && !waitInput) 
            {
                soundSource.clip = selectSound;
                soundSource.Play();
                DialogueManager.ContinueStory();
                waitInput = true;
            }
            else if (!interactable)
            {
                soundSource.clip = errorSound;
                soundSource.Play();
            }
        }

        if (interact.WasReleasedThisFrame()) //resets interact button
        {
            waitInput = false;
        }
    }

    public void startStory() //to toggle player movement by Dialoguemanager
    {
        canMove = false;
        storyMode = true;
        interactable = false;
    }

    public void endStory() //to toggle player movement by Dialoguemanager
    {
        interactionObject.Visit(); //sets the interactable as being visited after story is completed
        canMove = true;
        storyMode = false;
        resetInteraction();//need to wait before turning interact back on
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            if (interactable) //already interactable with a different object
            {
                interactionObject.ToggleHighlight(); //turns highlight off on old object
                interactionObject = interactionObject = collision.GetComponent<Interactable>();
                story = collision.GetComponent<Interactable>().GetInk();
                DialogueManager.SetText(interactionObject.description);
            }
            else
            {
                interactable = true;
                interactionObject = collision.GetComponent<Interactable>();
                story = collision.GetComponent<Interactable>().GetInk();
                DialogueManager.SetText(interactionObject.description); //sets the description of the interactable
            }
            
            //if it has not been visited yet
            if (!interactionObject.visited)
            {
                storyVisited = false; //this is the first visit
            }
            else
            {
                storyVisited = true; //has been visited before
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            if (collision.GetComponent<Interactable>() != interactionObject)
            {
                return;
            }
            interactable = false;
            story = null;
            DialogueManager.SetText("");
            interactionObject = null;
        }
    }

    public void SetHat(int hatChoice)
    {
        if (hatChoice < 0)
        {
            hat.SetActive(false);//turns hat off
            return;
        }
        if (hatChoice > hats.Length - 1)
        {
            Debug.Log("Error: hat index out of bounds");
            return;
        }
        hatSprite.sprite = hats[hatChoice];
    }

    IEnumerator resetInteraction()
    {
        yield return new WaitForSeconds(0.5f);
        interactable = true;
    }

    public void WalkSound()
    {
        if (!walkSoundPlaying)
        {
            walkSoundPlaying = true;
            StartCoroutine(PlaySound(walkSounds[Random.Range(0, walkSounds.Length - 1)]));
        }
        
    }

    IEnumerator PlaySound(AudioClip audio)
    {
        soundSource.clip = audio;
        soundSource.loop = false;
        soundSource.Play();
        yield return new WaitForSeconds(walkSoundDelay);
        walkSoundPlaying = false;
    }
}
