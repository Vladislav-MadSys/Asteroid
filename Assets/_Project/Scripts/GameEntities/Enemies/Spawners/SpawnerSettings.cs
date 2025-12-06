using System;
using UnityEngine;

namespace AsteroidGame
{

    [CreateAssetMenu(fileName = "Spawner")]
    public class SpawnerSettings : ScriptableObject
    {
        public SpawnerType Type;
        public GameObject Prefab;
        public float TimeToSpawn = 1;
        public float OffsetOutOfScreen = 0.1f;
    }
}
