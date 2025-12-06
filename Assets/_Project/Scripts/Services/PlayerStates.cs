using System;
using UnityEngine;

namespace AsteroidGame
{

    public class PlayerStates
    {
        public event Action<Vector2> OnPlayerPositionChanged;
        public event Action<float> OnPlayerRotationChanged;
        public event Action<int> OnLaserChargesChanged;
        public event Action<float> OnLaserTimeChangedChanged;
        
        public void ChangePlayerPosition(Vector3 newPosition) => OnPlayerPositionChanged?.Invoke(newPosition);
        public void ChangePlayerRotation(float newRotation) => OnPlayerRotationChanged?.Invoke(newRotation);
        public void ChangeLaserCharges(int newCharges) => OnLaserChargesChanged?.Invoke(newCharges);
        public void ChangeLaserTime(float newTimer) => OnLaserTimeChangedChanged?.Invoke(newTimer);
    }
}
