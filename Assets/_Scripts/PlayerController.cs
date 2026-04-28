using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{    
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

    //the mast to use with highlightable stuff
    private SpriteMask mask;

    // sprite/animation variables
    public SpriteRenderer sr;
    [SerializeField] private Sprite walk1, walk2;
    [SerializeField] private Sprite stand1, stand2;
    [SerializeField] private Sprite dodge1, dodge2;
    [SerializeField] private GameObject hand;
    private int animationFrameCounter = 0;
    private int dodgeFrameCounter = 0;
    private bool standing, walking;

    //interaction variables
    public DialogueManager DialogueManager;
    private Interactable interactionObject; //the thing being interacted with
    bool interactable = false;
    TextAsset story = null;
    public bool canMove = true; //for toggling player movement
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

    [SerializeField] private Interactable bed; //for the wakeUp sequence
    bool awoken = false;

    private static PlayerController _instance;
    public static PlayerController instance { get { return _instance; } }

    //need some vector2s to jump the player around
    //these are the places the player needs to be when they enter these rooms
    private Vector2 enterMain;
    private Vector2 enterUnderOver;



    private void SceneLoaded(QuestManager sceneLoad)
    {
<<<<<<< Updated upstream
        interactionObject = null;
        endStory();
=======
>>>>>>> Stashed changes
        if (SceneManager.GetActiveScene().name == "EvilMain")
        {
            transform.position = enterMain;
        }
        else
        {
            transform.position = enterUnderOver;
        }
            
    }

    void OnEnable() {
        QuestManager.SceneLoadEvent += SceneLoaded;
        enterMain = new Vector2(-0.7f, -3.33f);
        enterUnderOver = new Vector2(8f, 5f);
        InputActions.FindActionMap("Player").Enable();
        mask = GetComponent<SpriteMask>();
    }
    private void OnDisable()
    {
        QuestManager.SceneLoadEvent -= SceneLoaded;
    }

    void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        move = InputSystem.actions.FindAction("Move");
        bird = InputSystem.actions.FindAction("FlipTheBird");
        dodge = InputSystem.actions.FindAction("Dodge");
        interact = InputSystem.actions.FindAction("Interact/Continue");
        menu = InputSystem.actions.FindAction("Pause/Quit");

        hatSprite = hat.GetComponent<SpriteRenderer>();

        soundSource = GetComponent<AudioSource>();

        //for intro cinematic
        if (DialogueManager.doWakeUpStory) {
            interactionObject = bed;
            story = bed.GetInk();
            startStory(); //begins wakeUp story
        }
        else if (!DialogueManager.doWakeUpStory) awoken = true;

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

        // animation stuff
        if (awoken)
        {
            animationFrameCounter++;
            dodgeFrameCounter++;

            // walking animation
            if (moveDirection.x != 0 || moveDirection.y != 0)
            {
                if (standing)
                {
                    animationFrameCounter = 5;
                    standing = false;
                    hatSprite.flipY = false;
                    hatSprite.flipX = false;
                    hat.transform.localPosition = new Vector2(0f, 1.15f);
                    hat.transform.localPosition = new Vector2(0f, 1.15f);
                }
                walking = true;
                if (animationFrameCounter == 5)
                {
                    hatSprite.flipY = false;
                    hatSprite.flipX = false;
                    hat.transform.localPosition = new Vector2(0f, 1.15f);
                    if (sr.sprite == walk1) sr.sprite = walk2;
                    else if (sr.sprite == walk2) sr.sprite = walk1;
                    else if (sr.sprite == stand1) sr.sprite = walk2;
                    else if (sr.sprite == stand2) sr.sprite = walk1;
                    else if (sr.sprite == dodge1) sr.sprite = walk2;
                    else if (sr.sprite == dodge2) sr.sprite = walk1;
                    animationFrameCounter = 0;
                    mask.sprite = sr.sprite; //sets the mask to match the sprite
                }
            }

            // idle animation
            else if (moveDirection.x == 0 || moveDirection.y == 0)
            {
                if (walking)
                {
                    animationFrameCounter = 20;
                    walking = false;
                    hatSprite.flipY = false;
                    hatSprite.flipX = false;
                    hat.transform.localPosition = new Vector2(0f, 1.15f);
                }
                standing = true;
                if (animationFrameCounter == 20)
                {
                    hatSprite.flipY = false;
                    hatSprite.flipX = false;
                    hat.transform.localPosition = new Vector2(0f, 1.15f);
                    if (sr.sprite == stand1) sr.sprite = stand2;
                    else if (sr.sprite == stand2) sr.sprite = stand1;
                    else if (sr.sprite == walk1) sr.sprite = stand2;
                    else if (sr.sprite == walk2) sr.sprite = stand1;
                    else if (sr.sprite == dodge1) sr.sprite = stand2;
                    else if (sr.sprite == dodge2) sr.sprite = stand1;
                    animationFrameCounter = 0;
                    mask.sprite = sr.sprite; //sets the mask to match the sprite
                }
            }

            // dodge animation (vertical sprite flip) (not an animation)
            if (dodging)
            {
                sr.sprite = dodge1;
                hatSprite.flipY = true;
                hatSprite.flipX = true;
                hat.transform.localPosition = new Vector2(0f, -0.21f);
                mask.sprite = sr.sprite; //sets the mask to match the sprite
            }

            if (dodgeFrameCounter == 8)
            {
                if (dodging)
                {
                    if (sr.sprite == dodge1) sr.sprite = dodge2;
                    else if (sr.sprite == dodge2) sr.sprite = dodge1;
                    mask.sprite = sr.sprite; //sets the mask to match the sprite
                }
                dodgeFrameCounter = 0;
            }

            if (animationFrameCounter >= 21) animationFrameCounter = 0;
            if (dodgeFrameCounter >= 9) dodgeFrameCounter = 0;
        }
    }

    void Update()
    {
        // reading movement input (joystick, wasd, whatever)
        if (canMove) moveDirection = move.ReadValue<Vector2>();
        else if (!canMove) moveDirection = Vector2.zero;

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
            hand.SetActive(true);
        }
        if (bird.WasReleasedThisFrame()) {
            birdActive = true;
            hand.SetActive(false);
        }

        if (canMove && moveDirection.x != 0 || moveDirection.y != 0)
        {
            WalkSound();
        }

        // this is admittedly a very fiddly dodge roll
        if (canMove) {
            if (dodge.WasPressedThisFrame()) {
                dodgeSpeed = 15;
                dodgeDirection = moveDirection;
                rb.linearVelocity = dodgeDirection * dodgeSpeed;
                dodging = true;
            }
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
        interactionObject.Visit(); //sets the interactable as being visited after story is completed
    }

    public void endStory() //to toggle player movement by Dialoguemanager
    {
        if (!awoken)
        {
            awoken = true;
        }
        canMove = true;
        storyMode = false;
        interactionObject.ToggleHighlight();
        interactable = true;
        DialogueManager.SetText(interactionObject.description);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!awoken)
        {
            return;
        }
        if (collision.CompareTag("Interaction"))
        {
            if (interactable) //already interactable with a different object
            {
                interactable = true;
                interactionObject.ToggleHighlight(); //turns highlight off on old object
                interactionObject = interactionObject = collision.GetComponent<Interactable>();
                interactionObject.ToggleHighlight(); //turn on the other one
                story = collision.GetComponent<Interactable>().GetInk();
                DialogueManager.SetText(interactionObject.description);
            }
            else
            {
                interactable = true;
                interactionObject = collision.GetComponent<Interactable>();
                interactionObject.ToggleHighlight();
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
            interactionObject.ToggleHighlight();
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
