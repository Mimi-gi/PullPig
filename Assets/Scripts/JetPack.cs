using UnityEngine;
using R3;
using UnityEngine.VFX;
using UnityEngine.Rendering;
using LitMotion;

public class JetPack : MonoBehaviour
{
    [Header("0が火が無い方")]
    [SerializeField] Sprite[] sprites;
    [SerializeField] PlayerMove move;
    ReactiveProperty<bool> isdash = new ReactiveProperty<bool>(false);
    VisualEffect vfx;
    SpriteRenderer sp;

    [Header("エフェクトの設定")]
    [SerializeField] Volume volume;
    void Update()
    {
        isdash.Value = move.isDash;
    }

    void Start()
    {
        vfx = this.GetComponent<VisualEffect>();
        sp = this.GetComponent<SpriteRenderer>();
        vfx.Stop();
        isdash
        .Where(v => v)
        .Subscribe(_ =>
        {
            vfx.Play();
            sp.sprite = sprites[1];
            LMotion.Create(0f, 1f, 0.2f)
            .Bind(x =>
            {
                volume.weight = x;
            });
        });

        isdash
        .Where(v => !v)
        .Subscribe(_ =>
        {
            vfx.Stop();
            sp.sprite = sprites[0];
            LMotion.Create(1f, 0f, 0.2f)
            .Bind(x =>
            {
                volume.weight = x;
            });
        });
    }

}
