using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager INSTANCE;
    void Awake()
    {
        if (INSTANCE == null) INSTANCE = this;
        else Destroy(this.gameObject);
    }
}
