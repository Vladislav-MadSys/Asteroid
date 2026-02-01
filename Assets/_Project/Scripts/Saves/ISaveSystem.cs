using System;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Saves
{
    public interface ISaveSystem
    {
        public event Action <SaveData> OnSaveLoaded;
        public event Action<SaveData, SaveData> OnConflictWithCloudSave;
        public void Save();
        public UniTask Load();
        public void UseCloudSave();
        public void UseLocalSave();
        public SaveData CachedSaveData { get; }
    }
}
