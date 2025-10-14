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


    void Start()
    {
        Speed = speed;

    }

    void Update()
    {
        Move();
        Look();
    }

    void Move()
    {
        if (isDash)
        {
            this.transform.position += new Vector3(moveInput.x, moveInput.y, 0f) * Speed * 2.5f * Time.deltaTime;
        }
        else
        {
            this.transform.position += new Vector3(moveInput.x, moveInput.y, 0f) * Speed * Time.deltaTime;
        }

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
