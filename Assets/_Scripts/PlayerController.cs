using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    private Vector2 moveDirection;
    public InputActionReference move;
    
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void Update()
    {
        moveDirection = move.action.ReadValue<Vector2>();
    }
}
