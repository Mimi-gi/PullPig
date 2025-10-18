using UnityEngine;
using R3;

[CreateAssetMenu(fileName = "Assets/SOs/Initializer")]
public class Initializer : ScriptableObject
{
    public GameMode Mode;
    public float IniAttack;
    public float IniStamina;
    public float IniTime;
    public bool isTutrial;
}
