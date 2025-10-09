using UnityEngine;
using System.Collections.Generic;
using R3;
using UnityEngine.VFX;
using System.Threading;
using ListExtensions;

public class EnemyCore : MonoBehaviour
{
    [SerializeField] GameObject ePrefab;
    [SerializeField] int num;
    [SerializeField] int score;
    public float maxr;
    public ReactiveProperty<int> eNum = new ReactiveProperty<int>(0);
    List<Collider2D> activeEnitites = new List<Collider2D>();
    Subject<Unit> onAllDeath = new Subject<Unit>();
    public Observable<Unit> OnAllDeath => onAllDeath;
    public ReactiveProperty<bool> IsMaxHanged = new ReactiveProperty<bool>();
    EnemyCoreMove ecm;

    [Header("アイテムの設定")]

    [SerializeField] protected List<GameObject> itemPrefabs; //アイテムのプレハブ
    [SerializeField] List<float> itemWeights; //アイテムの比重 
    [SerializeField] protected int itemNum;

    List<(GameObject, float)> useItemPossibility;
    List<(GameObject, float)> itemPossibility; //Agentがもちうるアイテムとその比重

    [Header("エフェクト関連")]
    [SerializeField] VisualEffectAsset toujou;
    public CancellationTokenSource cts = new CancellationTokenSource();
    [Header("鎖")]
    [SerializeField] ChainSO chainSO;
    void Start()
    {
        OnAllDeath.Subscribe(_ =>
        {
            for (int i = 0; i < itemNum; i++) { ItemChooseAndCreate(this.transform.position); }
        });
        CreateAgent();

        eNum
        .Where(n => n == 0)
        .Subscribe(_ =>
        {
            onAllDeath.OnNext(Unit.Default);
            //!ScoreModel.INSTANCE.Score.Value += score;
            //Destroy(this.gameObject);
        });
        CreateItemList();
        var fx = this.gameObject.AddComponent<VisualEffect>();
        ecm = this.GetComponent<EnemyCoreMove>();
        fx.visualEffectAsset = toujou;
    }

    public virtual void CreateAgent()
    {
        eNum.Value = num;
        for (int i = 0; i < num; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            var obj = Instantiate(ePrefab, pos + this.transform.position, Quaternion.identity);
            var col = obj.GetComponent<Collider2D>();
            var move = obj.GetComponent<EntityMove>();
            var chain = obj.AddComponent<Chain>();
            activeEnitites.Add(col);
            move.Core = this;
            move.nearColliders = activeEnitites;
            move.OnDeath
            .Subscribe(_ =>
            {
                eNum.Value--;
                activeEnitites.TryRemove(col);
            });

            move.CurState
            .Where(_ => _ == EntityState.Hanged)
            .Subscribe(_ =>
            {
                //つかまれているときの処理
            });

            move.CurState
            .Where(_ => _ == EntityState.Normal)
            .Subscribe(_ =>
            {
                //手から離れた処理
            });
            //chain.Create(this.gameObject,obj, maxr,chainSO);
        }
    }

    void CreateItemList()
    {
        if (itemPrefabs.Count == itemWeights.Count)
        {
            itemPossibility = new List<(GameObject, float)>();
            for (int i = 0; i < itemPrefabs.Count; i++)
            {
                itemPossibility.Add((itemPrefabs[i], itemWeights[i]));
            }
        }

        useItemPossibility = new List<(GameObject, float)> { };
        if (itemPossibility == null) return;
        if (itemPossibility.Count == 0) return;

        float sum = 0;
        foreach (var f in itemPossibility)
        {
            sum += f.Item2;
        }
        float c_r = 0;

        foreach (var i in itemPossibility)
        {
            useItemPossibility.Add((i.Item1, i.Item2 / sum));
            c_r += i.Item2 / sum;
        }
    }

    public void ItemChooseAndCreate(Vector3 pos)
    {
        // アイテムリストが空なら何もしない
        if (useItemPossibility == null) return;
        if (useItemPossibility.Count == 0) return;

        // 0.0から1.0の間の乱数を1回だけ生成する
        float randomValue = Random.Range(0f, 1f);
        float cumulativeProbability = 0f; // 確率を累積していくための変数

        // 各アイテムを順番にチェック
        foreach (var item in useItemPossibility)
        {
            // 自分の確率を累積値に加算
            cumulativeProbability += item.Item2;

            // 累積確率が乱数より大きくなった最初のアイテムが当選
            if (randomValue < cumulativeProbability)
            {
                // 当選したアイテムを生成
                if (item.Item1 != null)
                {
                    Debug.Log(item.Item1.name);
                    Instantiate(item.Item1, pos + new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0), Quaternion.identity);
                }
                // アイテムを1つ生成したら、それ以上ループする必要はないのでメソッドを抜ける
                return;
            }
        }
    }

}
