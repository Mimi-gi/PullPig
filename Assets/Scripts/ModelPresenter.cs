using UnityEngine;
using UnityEngine.UI;
using R3;
using TMPro;
using TextExtensions;

public class ModelPresenter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI atTx;
    [SerializeField] TextMeshProUGUI tiTx;
    [SerializeField] TextMeshProUGUI scTx;
    [SerializeField] Image atIm;
    [SerializeField] Image tiIm;
    [SerializeField] Image scIm;

    void Start()
    {
        atTx.text = $"{PlayerModel.Attack.CurrentValue:F1}";
        tiTx.text = $"{PlayerModel.Time.CurrentValue:F1}";
        scTx.text = $"{ScoreModel.INSTANCE.Score.CurrentValue}";

        PlayerModel.Attack
        .Pairwise()
        .Subscribe(v =>
        {
            var d = v.Current - v.Previous;
            TextExtension.Add(atTx, d);
            atTx.text = $"{v.Current}";
        });

        PlayerModel.Time
        .Pairwise()
        .Subscribe(v =>
        {
            var d = v.Current - v.Previous;
            if (d > 1f) TextExtension.Add(tiTx, d);
            if (d < -1f) TextExtension.Subtract(tiTx, d);
            tiTx.text = $"{v.Current}";
        });

        ScoreModel.INSTANCE.Score
        .Pairwise()
        .Subscribe(v =>
        {
            var d = v.Current - v.Previous;
            TextExtension.Add(scTx, d);
            scTx.text = $"{v.Current}";
        });
    }


}
