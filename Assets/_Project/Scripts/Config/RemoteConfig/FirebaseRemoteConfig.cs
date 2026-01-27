using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Firebase.Extensions;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Config.RemoteConfig
{
    public class FirebaseRemoteConfig : IRemoteConfig
    {
        private const string CONFIG_KEY = "AsteroidConfig";
    
        private bool _isFirebaseReady = false;
        private Firebase.FirebaseApp _app;

        private ConfigData _config;
    
        [Inject]
        private void Inject(ConfigData config)
        {
            _config = config;
        }
    
        public void Initialize()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync()
                .ContinueWithOnMainThread((Task<Firebase.DependencyStatus> task) =>
                {
                    var dependencyStatus = task.Result;
                    if (dependencyStatus == Firebase.DependencyStatus.Available)
                    {
                        // Create and hold a reference to your FirebaseApp,
                        // where app is a Firebase.FirebaseApp property of your application class.
                        _app = Firebase.FirebaseApp.DefaultInstance;

                        // Set a flag here to indicate whether Firebase is ready to use by your app.
                        _isFirebaseReady = true;
                    }
                    else
                    {
                        UnityEngine.Debug.LogError(System.String.Format(
                            "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                        // Firebase Unity SDK is not safe to use here.
                    }
                });
            FetchDataAsync().Forget();
        }

        public async UniTask FetchDataAsync()
        {
            if(_isFirebaseReady) return;
        
            UniTask fetchTask = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero).AsUniTask();
            await fetchTask;
            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
        
            string result = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(CONFIG_KEY).StringValue;
            JsonUtility.FromJsonOverwrite(result, _config);
        }
    }
}
