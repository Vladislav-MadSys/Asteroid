using UnityEngine;

namespace _Project.Scripts.GameEntities.Enemies.Spawners
{

    [CreateAssetMenu(fileName = "Spawner")]
    public class SpawnerSettings : ScriptableObject
    {
        public SpawnerType Type;
        public float TimeToSpawn = 1;
        public float OffsetOutOfScreen = 0.1f;
    }
}
