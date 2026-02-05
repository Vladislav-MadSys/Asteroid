using _Project.Scripts.Addressables;
using _Project.Scripts.Advertisement;
using _Project.Scripts.Analytics;
using _Project.Scripts.Bootstrap;
using _Project.Scripts.Config;
using _Project.Scripts.Config.RemoteConfig;
using _Project.Scripts.Low;
using _Project.Scripts.Low.SceneController;
using _Project.Scripts.Purchases;
using _Project.Scripts.Purchases.Purchasing;
using _Project.Scripts.Saves;
using _Project.Scripts.Services;
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
            Container.BindInterfacesTo<FireBaseAnalytics>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ResourceLoader>().AsSingle().NonLazy();
            Container.Bind<ISceneController>().To<SceneController>().AsSingle();
            Container.BindInterfacesTo<AdvertisementStab>().AsSingle().NonLazy();
            Container.BindInterfacesTo<FirebaseRemoteConfig>().AsSingle().NonLazy();
            Container.Bind<ConfigData>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PurchasingStab>().AsSingle().NonLazy();
            Container.Bind<IInitializable>().To<BootstrapScene>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SaveSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<UnityCloudSaveService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LocalSaveSystemPlayerPrefs>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AccessChecker>().AsSingle().NonLazy();
        }
    }
}
