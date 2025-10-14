using UnityEngine;
using R3;
public class PlayerModel : MonoBehaviour
{
    public ReactiveProperty<float> Attack = new ReactiveProperty<float>();
    public ReactiveProperty<float> Stamina = new ReactiveProperty<float>();
    public ReactiveProperty<float> Time = new ReactiveProperty<float>();

    Observable<(ItemKind, float)> onGetItem;
    Observable<float> onDamage; //他からインスタンスを代入される。

}
