using UnityEngine;
using System.Collections.Generic;
using R3;

public class EnemyCore : MonoBehaviour
{
    [SerializeField] GameObject ePrefab;
    [SerializeField] int num;
    [SerializeField] int score;
    public ReactiveProperty<int> eNum = new ReactiveProperty<int>(0);
    List<Collider2D> activeEnitites;
    Subject<Unit> onAllDeath = new Subject<Unit>();
    public Observable<Unit> OnAllDeath => onAllDeath;
    void Start()
    {
        eNum
        .Where(n => n == 0)
        .Subscribe(_ =>
        {
            onAllDeath.OnNext(Unit.Default);
            ScoreModel.INSTANCE.Score.Value += score;
            Destroy(this.gameObject);
        });
        Debug.Log("起動!");
    }
}
