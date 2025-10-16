using UnityEngine;
using UnityEngine.VFX;
using R3;

public class ChainEffect : MonoBehaviour
{
    [SerializeField] GameObject effect;
    CoreEntityChain chain;

    void Start()
    {
        if (TryGetComponent<CoreEntityChain>(out chain))
        {
            Debug.Log("入手");
            chain.EntityModel.OnDeath
            .Subscribe(_ =>
            {
                OnDeath();
            })
            .AddTo(this.gameObject);
        }
    }

    void OnDeath()
    {
        Debug.Log("エフェクト");
        var obj = Instantiate(effect, this.transform.position, this.transform.rotation);
        var vfx = obj.GetComponent<VisualEffect>();
        vfx.SetFloat("Length", chain.length);
        vfx.Play();
        Destroy(obj, 2f);
    }

}
