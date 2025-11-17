using System.Drawing;
using UnityEngine;

public class GameSessionData : MonoBehaviour
{
    public static GameSessionData Instance { get; private set; }


    //Data
    public int Points { get; private set; } = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void OnEnable()
    {
        GameEvents.OnEnemyKilled += ChangePoints;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyKilled -= ChangePoints;
    }

    void ChangePoints(int deltaPoints)
    {
        Points += deltaPoints;
        GameEvents.ChangePoints(Points);
    }

}
