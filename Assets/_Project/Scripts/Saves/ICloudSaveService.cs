using System;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Saves
{
    public interface ICloudSaveService
    {
        public void Save();    
        public void Save(SaveData saveData);    
        public UniTask<SaveData> Load();
    }
}
