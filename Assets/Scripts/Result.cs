using UnityEngine;

[CreateAssetMenu(fileName = "Assets/SOs/Result")]
public class Result : ScriptableObject
{
    public GameMode mode;
    public int score;
    public int killNum;
}