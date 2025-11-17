using System;
using UnityEngine;

public class GameEvents
{
    public static Action<int> OnEnemyKilled;

    public static Action<int> OnPointsChanged;
    public static Action<Vector2> OnPlayerPositionChanged;
    public static Action<float> OnPlayerRotationChanged;
    public static Action<int> OnLaserChargesChanged;
    public static Action<float> OnLaserTimeChangedChanged;

    public static void KillEnemy(int points) => OnEnemyKilled?.Invoke(points);
    public static void ChangePoints(int curPoints) => OnPointsChanged?.Invoke(curPoints);
    public static void ChangePlayerPosition(Vector3 newPosition) => OnPlayerPositionChanged?.Invoke(newPosition);
    public static void ChangePlayerRotation(float newRotation) => OnPlayerRotationChanged?.Invoke(newRotation);
    public static void ChangeLaserCharges(int newCharges) => OnLaserChargesChanged?.Invoke(newCharges);
    public static void ChangeLaserTime(float newTimer) => OnLaserTimeChangedChanged?.Invoke(newTimer);
}
