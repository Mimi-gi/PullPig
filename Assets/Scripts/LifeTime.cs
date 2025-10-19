using LitMotion;
using R3;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] protected float time = 1.0f;
    [HideInInspector] public float timer = 0.0f;
    protected Subject<Unit> onOff = new Subject<Unit>();
    public Observable<Unit> OnOff => onOff;
    protected void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            onOff.OnNext(Unit.Default);
            Dispose();//少し待って破壊
        }
    }
    
    protected virtual void Dispose()
    {
        Destroy(this.gameObject);
    }
}
