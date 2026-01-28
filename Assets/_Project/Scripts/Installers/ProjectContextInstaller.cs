using _Project.Scripts.Addressables;
using _Project.Scripts.Advertisement;
using _Project.Scripts.Analytics;
using _Project.Scripts.Config;
using _Project.Scripts.Config.RemoteConfig;
using _Project.Scripts.Low;
using _Project.Scripts.Purchases;
using _Project.Scripts.Purchases.Purchasing;
using _Project.Scripts.Saves;
using Firebase.Analytics;
using Unity.Services.LevelPlay;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SaveDataConstructor>().AsSingle().NonLazy();
            Container.Bind<ISaveService>().To<SaveSystemPlayerPrefs>().AsSingle().NonLazy();
            Container.BindInterfacesTo<FireBaseAnalytics>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ResourceLoader>().AsSingle().NonLazy();
            Container.Bind<SceneController>().AsSingle();
            Container.BindInterfacesTo<LevelPlayAdvertisement>().AsSingle().NonLazy();
            Container.BindInterfacesTo<FirebaseRemoteConfig>().AsSingle().NonLazy();
            Container.Bind<ConfigData>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PurchaserUnityIAP>().AsSingle().NonLazy();
        }
    }
}
