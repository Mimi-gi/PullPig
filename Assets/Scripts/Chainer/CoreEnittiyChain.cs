using UnityEngine;
using VectorExtensions;
using R3;
using Unity.IntegerTime;

public class CoreEntityChain : MonoBehaviour
{
    SpriteRenderer sr;
    public EntityModel EntityModel{get; private set;}
    Rigidbody2D entityRb;
    EnemyCore core;
    [HideInInspector]public float length;

    [Header("鎖の色")]
    [SerializeField] Color chainColor = Color.yellow;
    MaterialPropertyBlock mpb;

    public void Set(EntityModel em, EnemyCore ec)
    {
        sr = this.GetComponent<SpriteRenderer>();
        mpb = new MaterialPropertyBlock();
        EntityModel = em;
        core = ec;
        entityRb = EntityModel.GetComponent<Rigidbody2D>();
        em.OnDeath
        .Subscribe(_ =>
        {
            Destroy(this.gameObject);
        });
    }

    void Force()
    {
        var del = core.transform.position - EntityModel.gameObject.transform.position;
        if (del.magnitude > core.maxr * 1.4f)
        {
            entityRb.AddForce(VectorExtension.Pow(del.normalized * (del.magnitude - core.maxr*1.4f), 1.5f));
        }
        if(del.magnitude > core.maxr)
        {
            Damage();
        }
    }

    void Sprite()
    {
        this.transform.rotation = Quaternion.FromToRotation(Vector3.right, -core.transform.position + EntityModel.transform.position);
        length = (core.transform.position - EntityModel.transform.position).magnitude;
        sr.size = new Vector2(length, 0.5f);
    }

    void ColorSet()
    {
        float ratio = EntityModel.Hp / EntityModel.MaxHp;
        Color currentColor = Color.Lerp(new Color(0, 0, 0, 1), chainColor, 1 - ratio);
        currentColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
        sr.GetPropertyBlock(mpb);
        mpb.SetColor("_AdditiveColor", currentColor);
        sr.SetPropertyBlock(mpb);
    }

    void Damage()
    {
        EntityModel.Hp -= PlayerModel.Attack.CurrentValue * Time.deltaTime*(1+ (core.transform.position - EntityModel.transform.position).magnitude/core.maxr);
    }

    void LateUpdate()
    {
        Force();
        Sprite();
        ColorSet();
    }
}
