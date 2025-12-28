using UnityEngine;

namespace _Project.Scripts.Saves
{
    public interface ISaveService
    {
        public void Save();
        public void Load();
    }
}
