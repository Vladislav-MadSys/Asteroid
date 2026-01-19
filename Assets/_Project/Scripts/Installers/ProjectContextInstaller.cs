using _Project.Scripts.Addressables;
using _Project.Scripts.Analytics;
using _Project.Scripts.Saves;
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
            Container.BindInterfacesAndSelfTo<ResourceLoader>().AsSingle().NonLazy();
        }
    }
}
