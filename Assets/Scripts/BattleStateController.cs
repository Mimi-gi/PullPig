using UnityEngine;

public enum BattleWaveState
{
    Initialize,
    Normal,
    Boss,
    Reward,
    Finish
}
public class BattleStateController : MonoBehaviour
{
    BattleWaveState currentState = 0;
}