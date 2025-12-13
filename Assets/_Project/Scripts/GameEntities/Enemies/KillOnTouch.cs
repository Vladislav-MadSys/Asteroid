using _Project.Scripts.GameEntities.Player;
using UnityEngine;

namespace _Project.Scripts.GameEntities.Enemies
{
    public class KillOnTouch : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerShip playerShip))
            {
                playerShip.KillPlayer();
            }
        }
    }
}
