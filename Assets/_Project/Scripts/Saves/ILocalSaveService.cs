

using System;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Saves
{
    public interface ILocalSaveService
    {
        public void Save();
        public void Save(SaveData saveData);
        public SaveData Load();
    }
}
