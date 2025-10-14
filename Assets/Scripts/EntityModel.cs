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

    void Awake()
    {
        Hp = MaxHp;
    }

    void OnTriggerEnter2D()
    {
        
    }
}
