using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Saves
{
    public class UnityCloudSaveService : ICloudSaveService, IInitializable
    {
        private const string SAVE_KEY = "basicSave";
        private const int SECONDS_TO_WAIT_UNITY_CLOUD_READY = 5;
        
        private SaveDataConstructor _saveDataConstructor;

        private bool isReady = false;
        
        public UnityCloudSaveService(SaveDataConstructor saveDataConstructor)
        {
            _saveDataConstructor = saveDataConstructor;
        }
    
        public async void Initialize()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            isReady = true;
        }

        public void Save()
        {
            if(!isReady) return;
            
            var jsonSaveData = JsonUtility.ToJson(_saveDataConstructor.GetSaveData());
            var playerData = new Dictionary<string, object>{
                {SAVE_KEY, jsonSaveData}
            };
            CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
        }
        
        public void Save(SaveData saveData)
        {
            if(!isReady) return;
            
            var jsonSaveData = JsonUtility.ToJson(saveData);
            var playerData = new Dictionary<string, object>{
                {SAVE_KEY, jsonSaveData}
            };
            CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
        }

        public async UniTask<SaveData> Load()
        {
            var canceller = new CancellationTokenSource();
            canceller.CancelAfterSlim(TimeSpan.FromSeconds(SECONDS_TO_WAIT_UNITY_CLOUD_READY));
            
            try
            {
                await UniTask.WaitUntil(() => isReady, cancellationToken: canceller.Token);
                
                var keyToLoad = new HashSet<string>
                {
                    SAVE_KEY
                };
            
                var saveData = await CloudSaveService.Instance.Data.Player.LoadAsync(keyToLoad);

                if (saveData.TryGetValue(SAVE_KEY, out var data))
                {
                    return JsonUtility.FromJson<SaveData>(data.Value.GetAsString());
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
