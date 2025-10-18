using UnityEngine;
using R3;
using UnityEngine.SocialPlatforms.Impl;

public class EntityModel : MonoBehaviour
{
    [SerializeField] int score;
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
            ScoreModel.INSTANCE.Score.Value += score;
            Destroy(this.gameObject);
        }
    }
    
}
