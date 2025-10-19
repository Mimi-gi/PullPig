using UnityEngine;
using UnityEngine.InputSystem;
using VectorExtensions;

public class HandPos : MonoBehaviour
{
    Vector2 lookInput = Vector2.zero;
    Camera mainCamera;
    Rigidbody2D rb;
    [SerializeField] GameObject Player;
    Rigidbody2D playerRb;
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        playerRb = Player.GetComponent<Rigidbody2D>();
    }
    public void Look(InputAction.CallbackContext context)
    {
        var device = context.control.device;

        // 2. デバイスの型を判定して処理を分岐
        if (device is Gamepad)
        {
            // デバイスがゲームパッドの場合
            //Debug.Log("入力元: Gamepad");
            if (context.ReadValue<Vector2>() == Vector2.zero)
            {
                return;
            }
            lookInput = context.ReadValue<Vector2>();
        }
        else if (device is Pointer) // Mouse, Touch, Pen など
        {
            // デバイスがポインター（マウスなど）の場合
            //Debug.Log("入力元: Pointer (Mouse)");
            Vector2 mouseInput = context.ReadValue<Vector2>();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseInput.x, mouseInput.y, mainCamera.nearClipPlane + 10f));
            var toVector = worldPosition - Player.transform.position;
            if (toVector == Vector3.zero)
            {
                return;
            }
            if (toVector.magnitude > 2.0f)
            {
                toVector = toVector.normalized * 2.0f;
            }
            lookInput = toVector * 0.5f;
        }

        lookInput *= 2;
    }

    void FixedUpdate()
    {
        var goal = Player.transform.position + new Vector3(lookInput.x, lookInput.y, 0) + playerRb.linearVelocity.CastTo3()*0.15f;
        rb.AddForce((goal - this.transform.position) * 110);
    }

    void Update()
    {
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, lookInput.normalized);
    }
}
