using UnityEngine;

public class GameSessionData : MonoBehaviour
{
    //Data
    public int Points { get; private set; } = 0;

    private void Awake()
    {
        GameEvents.OnEnemyKilled += ChangePoints;
    }

    private void OnDestroy()
    {
        GameEvents.OnEnemyKilled -= ChangePoints;
    }

    void ChangePoints(int deltaPoints)
    {
        Points += deltaPoints;
        GameEvents.ChangePoints(Points);
    }
}
