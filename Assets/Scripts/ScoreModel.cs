using R3;
using UnityEngine;

public class ScoreModel : MonoBehaviour
{
    public static ScoreModel INSTANCE;
    public ReactiveProperty<int> Score;
    void Awake()
    {
        if (INSTANCE == null) INSTANCE = this;
        else Destroy(this.gameObject);
        Score = new ReactiveProperty<int>(0);
    }

}
