using System;
using UnityEngine;

namespace AsteroidGame
{

    public class GameEvents
    {
        public Action OnPlayerKilled;

        public Action<Vector2> OnPlayerPositionChanged;
        public Action<float> OnPlayerRotationChanged;
        public Action<int> OnLaserChargesChanged;
        public Action<float> OnLaserTimeChangedChanged;

        public void KillPlayer() => OnPlayerKilled?.Invoke();

        public void ChangePlayerPosition(Vector3 newPosition) => OnPlayerPositionChanged?.Invoke(newPosition);
        public void ChangePlayerRotation(float newRotation) => OnPlayerRotationChanged?.Invoke(newRotation);
        public void ChangeLaserCharges(int newCharges) => OnLaserChargesChanged?.Invoke(newCharges);
        public void ChangeLaserTime(float newTimer) => OnLaserTimeChangedChanged?.Invoke(newTimer);
    }
}
