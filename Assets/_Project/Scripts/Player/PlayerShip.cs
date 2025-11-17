using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public void KillPlayer()
    {
        GameEvents.KillPlayer();
        Destroy(gameObject);
    }
}
