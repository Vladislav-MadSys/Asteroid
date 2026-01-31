using System;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Saves
{
    public interface ICloudSaveService
    {
        public void Save(string jsonSaveData);    
        public UniTask<SaveData> Load();
    }
}
