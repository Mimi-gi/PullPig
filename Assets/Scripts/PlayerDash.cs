using UnityEngine;
using UnityEngine.InputSystem;
using R3;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] PlayerMove con;
    [SerializeField] float duration;
    float e;
    float elapse
    {
        get { return e; }
        set
        {
            if (value > duration)
            {
                e = duration;
            }
            else if (value < 0)
            {
                e = 0;
            }
            else
            {
                e = value;
            }
        }
    }
    //bool canDash = true;
    public ReactiveProperty<float> ratio = new ReactiveProperty<float>();

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && elapse < duration)
        {
            con.isDash = true;
        }
        else if (context.canceled)
        {
            con.isDash = false;
        }
    }

    void Update()
    {
        if (con.isDash && elapse < duration)
        {
            elapse += Time.deltaTime * 0.8f;
        }
        else if (con.isDash && elapse >= duration)
        {
            con.isDash = false;
        }

        if (!con.isDash)
        {
            elapse -= Time.deltaTime * 0.4f;
        }

        ratio.Value = (1 - elapse / duration);
    }
}
