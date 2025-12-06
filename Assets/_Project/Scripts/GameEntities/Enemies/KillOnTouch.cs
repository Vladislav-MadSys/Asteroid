using UnityEngine;

namespace AsteroidGame
{
    public class KillOnTouch : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerShip>(out PlayerShip playerShip))
            {
                playerShip.KillPlayer();
            }
        }
    }
}
