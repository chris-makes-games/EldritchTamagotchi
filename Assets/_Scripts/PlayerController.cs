using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // movement variables
    public Rigidbody2D rb;
    public float moveSpeed;
    private Vector2 moveDirection;

    // sprite/animation variables
    public SpriteRenderer sr;
    public Sprite initSprite, fuckYou;

    // input variables
    public InputActionAsset InputActions;
    public InputAction move;
    public InputAction bird;
    public bool birdActive;
    
    void OnEnable() {
        InputActions.FindActionMap("Player").Enable();
    }
    
    void Awake() {
        move = InputSystem.actions.FindAction("Move");
        bird = InputSystem.actions.FindAction("FlipTheBird");
    }

    void FixedUpdate()
    {
        // movement
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
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
    }
}
