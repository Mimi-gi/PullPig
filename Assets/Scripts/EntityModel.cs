using UnityEngine;
using R3;

public class EntityModel : MonoBehaviour
{
    Subject<Unit> onDeath = new Subject<Unit>();
    public Observable<Unit> OnDeath => onDeath;
    public float MaxHp;
    [HideInInspector]
    public float Hp
    {
        get { return hp; }
        set
        {
            if (value < 0)
            {
                hp = 0;
            }
            else
            {
                hp = value;
            }
        }
    }
    float hp;

    ReactiveProperty<float> HP = new ReactiveProperty<float>(1);

    void Awake()
    {
        Hp = MaxHp;
        HP.Value = Hp;
    }

    void Update()
    {
        if(Hp <= 0)
        {
            onDeath.OnNext(Unit.Default);
            Debug.Log("死亡");
            Destroy(this.gameObject);
        }
    }
    
}
