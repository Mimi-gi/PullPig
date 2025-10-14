using UnityEngine;
using VectorExtensions;
using R3;
using Unity.IntegerTime;

public class CoreEntityChain : MonoBehaviour
{
    SpriteRenderer sr;
    EntityModel entityModel;
    Rigidbody2D entityRb;
    EnemyCore core;

    [Header("鎖の色")]
    [SerializeField] Color chainColor = Color.yellow;
    MaterialPropertyBlock mpb;

    public void Set(EntityModel em, EnemyCore ec)
    {
        sr = this.GetComponent<SpriteRenderer>();
        mpb = new MaterialPropertyBlock();
        entityModel = em;
        core = ec;
        entityRb = entityModel.GetComponent<Rigidbody2D>();
    }

    void Force()
    {
        var del = core.transform.position - this.transform.position;
        if (del.magnitude > core.maxr)
        {
            entityRb.AddForce(VectorExtension.Pow(del.normalized * (del.magnitude - core.maxr), 2f));
            Damage();
        }
    }

    void Sprite()
    {
        Debug.Log("鎖");
        this.transform.rotation = Quaternion.FromToRotation(Vector3.right, -core.transform.position + entityModel.transform.position);
        sr.size = new Vector2((core.transform.position - entityModel.transform.position).magnitude, 0.5f);
    }

    void ColorSet()
    {
        float ratio = entityModel.Hp / entityModel.MaxHp;
        Debug.Log(ratio);
        Color currentColor = Color.Lerp(new Color(0, 0, 0, 1), chainColor, 1 - ratio);
        currentColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
        sr.GetPropertyBlock(mpb);
        mpb.SetColor("_AdditiveColor", currentColor);
        sr.SetPropertyBlock(mpb);
    }

    void Damage()
    {
        entityModel.Hp -= PlayerModel.Attack.CurrentValue * Time.deltaTime;
        if (entityModel.Hp <= 0)
        {
            //死亡処理
            Destroy(this.gameObject);
        }
    }

    void LateUpdate()
    {
        Force();
        Sprite();
        ColorSet();
    }
}
