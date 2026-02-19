using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // movement variables
    public Rigidbody2D rb;
    public float moveSpeed;
    private Vector2 moveDirection, dodgeDirection;
    private bool dodging = false;
    public float dodgeSpeed, dodgeSpeedMin, dodgeSpeedDrop;

    // sprite/animation variables
    public SpriteRenderer sr;
    public Sprite initSprite, fuckYou;

    //interaction variables
    public DialogueManager DialogueManager;
    bool interactable = false;
    TextAsset story = null;
    bool canMove = true; //for toggling player movement

    // input variables
    public InputActionAsset InputActions;
    public InputAction move;
    public InputAction bird;
    public InputAction dodge;
    public InputAction interact;
    public bool birdActive;
    
    void OnEnable() {
        InputActions.FindActionMap("Player").Enable();
    }
    
    void Awake() {
        move = InputSystem.actions.FindAction("Move");
        bird = InputSystem.actions.FindAction("FlipTheBird");
        dodge = InputSystem.actions.FindAction("Dodge");
        interact = InputSystem.actions.FindAction("Interact/Continue");
    }

    void FixedUpdate()
    {
        if (!dodging) {
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

        // this is admittedly a very fiddly dodge roll
        if (dodge.WasPressedThisFrame()) {
            dodgeSpeed = 15;
            dodgeDirection = moveDirection;
            rb.linearVelocity = dodgeDirection * dodgeSpeed;
            dodging = true;
        }

        if (interact.WasPressedThisFrame())
        {
            if (interactable)
            {
                DialogueManager.EnterStoryMode(story);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            interactable = true;
            story = collision.GetComponent<Interactable>().GetInk();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            interactable = false;
            story = null;
            DialogueManager.ExitStoryMode();
        }
    }
}
