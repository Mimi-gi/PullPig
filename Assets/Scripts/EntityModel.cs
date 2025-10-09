using UnityEngine;

public class EntityModel : MonoBehaviour
{
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
