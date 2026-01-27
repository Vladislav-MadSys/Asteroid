using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Analytics
{
    public class FireBaseAnalytics : IAnalyticsService, IInitializable
    {
        private const string GAME_START_KEY = "GameStart";
        private const string GAME_END_KEY = "GameEnd";
        private const string LASER_USES_KEY = "LaserUses";
        
        private const string SHOTS_COUNT_KEY = "ShotsCount";
        private const string LASER_COUNT_KEY = "LaserCount";
        private const string DESTROYED_ASTEROIDS_KEY = "DestroyedAsteroids";
        private const string DESTROYED_UFO_KEY = "DestroyedUfo";
        
        private bool _isFirebaseReady = false;
        private Firebase.FirebaseApp _app;

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
        }

        public void LogGameStart()
        {
            if (!_isFirebaseReady) return;

            Firebase.Analytics.FirebaseAnalytics.LogEvent(GAME_START_KEY);
        }

        public void LogGameEnd(int shotsCount, int laserUsesCount, int destroyedAsteroids, int destroyedUfo)
        {
            if (!_isFirebaseReady) return;
            
            Firebase.Analytics.FirebaseAnalytics.LogEvent(GAME_END_KEY,
                new Firebase.Analytics.Parameter(SHOTS_COUNT_KEY, shotsCount),
                new Firebase.Analytics.Parameter(LASER_COUNT_KEY, laserUsesCount),
                new Firebase.Analytics.Parameter(DESTROYED_ASTEROIDS_KEY, destroyedAsteroids),
                new Firebase.Analytics.Parameter(DESTROYED_UFO_KEY, destroyedUfo)
                );
        }

        public void LogLaserUse()
        {
            if (!_isFirebaseReady) return;
            
            Firebase.Analytics.FirebaseAnalytics.LogEvent(LASER_USES_KEY);
        }

    }
}
