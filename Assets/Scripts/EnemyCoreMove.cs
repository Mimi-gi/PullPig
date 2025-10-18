using UnityEngine;
using System.Collections.Generic;
using LitMotion;
using R3;
using VectorExtensions;
using UnityEngine.VFX;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;
public class RectRegion
{
    public Vector2 center;
    float w;
    float h;

    public RectRegion(float w, float h, Vector2 center)
    {
        this.w = w;
        this.h = h;
        this.center = center;
    }

    public RectRegion(float w, float h) : this(w, h, Vector2.zero)
    {
    }

    public bool InRegion(Vector2 p)
    {
        // Check if point p is within the rectangle centered at 'center' with width 'w' and height 'h'
        return Mathf.Abs(p.x - center.x) <= w / 2 && Mathf.Abs(p.y - center.y) <= h / 2;
    }

    public bool InRegion(Vector3 p)
    {
        return InRegion(new Vector2(p.x, p.y));
    }
}
public class EnemyCoreMove : MonoBehaviour
{
    [HideInInspector]public Transform player;
    public EnemyCore Core;
    Vector3 v;

    [Header("長方形の領域内判定用")]
    [SerializeField] float regionWidth;
    [SerializeField] float regionHeight;
    [SerializeField] float minVelocity;
    [SerializeField] float maxVelocity;
    [SerializeField] float noiseAngle;
    [SerializeField] float cycleTime;
    [SerializeField] float changeTime;

    [HideInInspector] public List<EntityMove> activeMoves = new List<EntityMove>();
    [Header("逃げるとき用")]
    [SerializeField] float scale;
    [SerializeField] float max;
    bool shiftFlag;
    float elapsedTime;
    float theta;
    bool moveflag = true;
    void Start()
    {
        Core = this.GetComponent<EnemyCore>();
        player = GameObject.FindObjectsByType<PlayerModel>(FindObjectsSortMode.None)[0].transform;
        cycleTime += Random.Range(-0.1f, 0.1f);
        Core.OnAllDeath
        .Subscribe(_ =>
        {
            moveflag = false;
        });
    }

    void LateUpdate()
    {
        if (!moveflag) return;

        Move();
        Look();
        elapsedTime += Time.deltaTime;
    }


    protected virtual void Move()
    {

        MoveControll();

        this.transform.position += v * Time.deltaTime;
    }

    Vector3 DecideVelocity()
    {
        var iv = Random.Range(minVelocity, maxVelocity);
        var region = new RectRegion(regionWidth, regionHeight, player.transform.position);
        if (!region.InRegion(this.transform.position))
        {
            var dir = (player.transform.position - this.transform.position).normalized;
            var theta = Random.Range(-noiseAngle, noiseAngle);
            return Quaternion.Euler(0, 0, theta) * dir * iv;
        }
        else
        {
            var theta = Random.Range(0, 360f);
            return Quaternion.Euler(0, 0, theta) * (new Vector3(1, 0, 0)) * iv;
        }
    }

    void MoveControll()
    {
        if (elapsedTime % cycleTime < 0.02f && !shiftFlag)
        {
            shiftFlag = true;
            var tmp = v;
            LMotion.Create(tmp, DecideVelocity(), changeTime)
            .WithOnComplete(() => shiftFlag = false)
            .Bind(x =>
            {
                v = x;
            });
        }
    }

    void Look()
    {
        if (v.magnitude < 0.01f)
        {
            return;
        }
        var theta = -Mathf.Abs(Mathf.Acos(v.y / v.magnitude)) * Mathf.Rad2Deg * Mathf.Sign(v.x);
        var current = Core.eye.transform.rotation.eulerAngles;

        Core.eye.transform.Rotate(0, 0, Mathf.DeltaAngle(current.z, theta) * Time.deltaTime * 5);
    }

    void TryFlee()
    {
        //つかまれているとき
    }

    Vector2 Center(List<EntityMove> list)
    {
        if (list.Count == 0)
        {
            return Vector2.zero;
        }
        Vector2 center = Vector2.zero;
        foreach (var ele in list)
        {
            center += ele.transform.position.CastTo2();
        }
        return center / list.Count;
    }


}
