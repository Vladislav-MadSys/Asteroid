

using System;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Saves
{
    public interface ISaveService
    {
        public event Action<SaveData> OnSaveLoaded;
        public event Action<SaveData, SaveData> OnConflictWithCloudSave;
        public SaveData CachedSaveData { get; }
        public bool HasConflictWithCloudSave { get; }
        public void Save();
        public UniTask Load();
        public void UseCloudSave();
        public void UseLocalSave();
    }
}
