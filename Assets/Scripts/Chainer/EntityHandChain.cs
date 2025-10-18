using R3;
using UnityEngine;
using VectorExtensions;

public class EntityHandChain : MonoBehaviour
{
    Hand Hand;
    EntityModel Entity;

    [SerializeField] float intensity = 1f;
    [SerializeField] float power = 2f;
    
    public void Set(Hand hand, EntityModel entity)
    {
        this.Hand = hand;
        this.Entity = entity;

        hand.isHanging
        .Where(v => v == false)
        .Subscribe(_ => OnDeath())
        .AddTo(this.gameObject);

        entity.OnDeath
        .Subscribe(_ => OnDeath())
        .AddTo(this.gameObject);
    }

    void OnDeath()
    {
        //演出など
        Destroy(this.gameObject, 0.2f);
    }

    void Force(GameObject A, GameObject B, float power, float intensity)
    {
        if (A == null || B == null) return;
        var del = A.transform.position - B.transform.position;

        var rb = B.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(VectorExtension.Pow(del.normalized * (del.magnitude + 0.2f), power) * intensity);
        }

    }

    void FixedUpdate()
    {
        if (Hand == null || Entity == null) return;
        Force(Hand.gameObject, Entity.gameObject, power, intensity);
        Force(Entity.gameObject, Hand.gameObject, power, intensity);
    }


}
