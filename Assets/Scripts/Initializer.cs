using UnityEngine;

[CreateAssetMenu(fileName = "Assets/SOs/BattleInitializer")]
public class Initializer : MonoBehaviour
{
    public GameMode Mode;
    public float IniAttack;
    public float IniStamina;
    public float IniTime;
}

public enum GameMode
{
    ScoreAttack,
    BossRash
}