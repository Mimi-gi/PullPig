using UnityEngine;
using R3;
public class PlayerModel : MonoBehaviour
{
    static ReactiveProperty<float> attack = new ReactiveProperty<float>(1.0f);
    static ReactiveProperty<float> stamina = new ReactiveProperty<float>(1.0f);
    static ReactiveProperty<float> time = new ReactiveProperty<float>(1.0f);

    public static ReadOnlyReactiveProperty<float> Attack => attack.ToReadOnlyReactiveProperty();
    public static ReadOnlyReactiveProperty<float> Stamina => stamina.ToReadOnlyReactiveProperty();
    public static ReadOnlyReactiveProperty<float> Time => time.ToReadOnlyReactiveProperty();

    Observable<(ItemKind, float)> onGetItem;
    Observable<float> onDamage; //他からインスタンスを代入される。

    void Awake()
    {
        attack.Value = 1.0f;
        stamina.Value = 1.0f;
        time.Value = 1.0f;
    }

}
