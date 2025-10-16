using UnityEngine;
using VectorExtensions;

public class HandPLChain : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject hand;

    [SerializeField] float maxradius = 2.2f;
    [SerializeField] float power = 2f;
    [SerializeField] float intensity = 1f;
    Rigidbody2D plRb;
    Rigidbody2D handRb;
    void Start()
    {
        plRb = player.GetComponent<Rigidbody2D>();
        handRb = hand.GetComponent<Rigidbody2D>();
    }

    void Force()
    {
        var del = player.transform.position - hand.transform.position;
        if (del.magnitude > maxradius)
        {
            plRb.AddForce(-VectorExtension.Pow(del.normalized * (del.magnitude - maxradius), power) * intensity);
            handRb.AddForce(VectorExtension.Pow(del.normalized * (del.magnitude - maxradius), power) * intensity);
        }
    }

    void FixedUpdate()
    {
        Force();
    }
}
