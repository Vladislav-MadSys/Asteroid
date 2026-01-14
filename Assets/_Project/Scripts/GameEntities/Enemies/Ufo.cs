using UnityEngine;

namespace _Project.Scripts.GameEntities.Enemies
{
    public class Ufo : Enemy
    {
        public override void Kill()
        {
            _gameSessionData.AddDestroyedUfos();
            base.Kill();
        }
    }
}
