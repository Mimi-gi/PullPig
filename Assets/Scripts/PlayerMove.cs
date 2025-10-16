using UnityEngine;
using R3;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{
    [HideInInspector] public Vector2 moveInput = Vector2.zero;
    [SerializeField] float speed;
    public float Speed { get; private set; }
    Vector2 direction = Vector2.zero;

    [HideInInspector] public bool isDash;

    Rigidbody2D rb;


    void Start()
    {
        Speed = speed;
        rb = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        Move();
        Look();
    }

    void Move()
    {
        if (isDash)
        {
            rb.AddForce((Speed - rb.linearVelocity.magnitude * 0.5f) * 100 * moveInput);
        }
        else
        {
            rb.AddForce((Speed - rb.linearVelocity.magnitude) * 50 * moveInput);
        }
    }

    public void ReceiveForce(Rigidbody2D rb)
    {
        rb.AddForce((Speed - rb.linearVelocity.magnitude) * 50 * moveInput);
    }

    void Look()
    {
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (moveInput == Vector2.zero)
        {
            return;
        }
        direction = moveInput.normalized;
    }
}
