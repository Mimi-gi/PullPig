using R3;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] float time = 1.0f;
    [HideInInspector] public float timer = 0.0f;
    Subject<Unit> onOff = new Subject<Unit>();
    public Observable<Unit> OnOff => onOff;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= time)
        {
            onOff.OnNext(Unit.Default);
            Destroy(this.gameObject);
        }
    }
}
