using UnityEngine;
using R3;
using System.Collections.Generic;

public class EntityMove : MonoBehaviour
{
    [SerializeField] float maxSpeed;

    [SerializeField] float randomness;

    [SerializeField] float separationAdjustment;
    [Tooltip("比例定数")]
    [SerializeField] float goalAdjustment_c;
    [Tooltip("単位。１でゲーム内距離のまま")]
    [SerializeField] float goalAdjustment_k;
    [Tooltip("距離の何乗に比例するか")]
    [SerializeField] float goalAdjustment_e;

    [HideInInspector] public List<Collider2D> nearColliders = new List<Collider2D>();
    public GameObject Core;
    Collider2D selfCol;
    Rigidbody2D selfRb;
    EntityModel entityModel;
    [SerializeField] float maxDistance;
    SpriteRenderer selfSp;
    public EntityState state;

    protected Subject<Unit> onDeath = new Subject<Unit>();
    public Observable<Unit> OnDeath => onDeath;

    protected Subject<Unit> onHanged = new Subject<Unit>();
    public Observable<Unit> OnHanged => onHanged;

    // インスペクターから設定する項目
    [SerializeField] Color targetColor;

    private MaterialPropertyBlock propBlock;
    Vector2 velocity;
    void Awake()
    {
        selfCol = this.GetComponent<Collider2D>();
        selfRb = this.GetComponent<Rigidbody2D>();
        selfSp = this.GetComponent<SpriteRenderer>();
        entityModel = this.GetComponent<EntityModel>();
        propBlock = new MaterialPropertyBlock();
    }
    public void Set()
    {

    }

    Vector2 SeparationForceBet2(Collider2D main, Collider2D other, float separationAdjustment)
    {
        Vector3 mainPosition = main.transform.position;
        Vector3 otherPosition = other.transform.position;
        var differenceVector = mainPosition - otherPosition;
        var distance = (mainPosition - otherPosition).magnitude;
        if (distance <= 0.001f) { distance = 0.01f; }
        var Force = separationAdjustment * (new Vector2(differenceVector.x, differenceVector.y)) / (distance);
        return Force;
    }

    Vector2 SeparationForce(List<Collider2D> colliders, float separationAdjustment)
    {
        if (separationAdjustment == 0) return Vector2.zero;
        Vector2 separation = Vector2.zero;
        foreach (Collider2D col in colliders)
        {
            if (col == null) { continue; }
            separation += SeparationForceBet2(selfCol, col, separationAdjustment);
        }
        return separation;
    }

    Vector2 GoalForce(GameObject obj, float bai, float tanni, float exp)
    {
        var distance = (obj.transform.position - this.transform.position).magnitude * tanni;
        var direction = (obj.transform.position - this.transform.position).normalized;
        var vec = direction * Mathf.Pow(distance, exp) * bai;
        return new Vector2(vec.x, vec.y);
    }

    void FixedUpdate()
    {
        velocity = Vector2.zero;
        velocity += SeparationForce(nearColliders, separationAdjustment);
        velocity += GoalForce(Core, goalAdjustment_c, goalAdjustment_k, goalAdjustment_e);
        selfRb.AddForce(velocity);
        selfRb.linearVelocity += new Vector2(Random.Range(-randomness, randomness), Random.Range(-randomness, randomness));
        if (selfRb.linearVelocity.magnitude > maxSpeed)
        {
            selfRb.linearVelocity = selfRb.linearVelocity.normalized * maxSpeed;
        }
    }

    void Update()
    {
        ChangeColor(targetColor, 1 - entityModel.Hp / entityModel.MaxHp, selfSp);

        if ((this.transform.position - Core.transform.position).magnitude > maxDistance)
        {
            onDeath.OnNext(Unit.Default);
            Destroy(this.gameObject);
        }
    }
    void ChangeColor(Color target, float ratio, SpriteRenderer sp)
    {
        Color currentColor = Color.Lerp(new Color(0, 0, 0, 1), target, ratio);
        currentColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
        
        sp.GetPropertyBlock(propBlock);
        propBlock.SetColor("_AdditiveColor", currentColor);
        sp.SetPropertyBlock(propBlock);
    }
}
