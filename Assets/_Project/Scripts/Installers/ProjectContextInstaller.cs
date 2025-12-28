using _Project.Scripts.Saves;
using _Project.Scripts.Services;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameSessionData>().AsSingle().NonLazy();
            Container.Bind<SaveDataConstructor>().AsSingle().NonLazy();
            Container.Bind<ISaveService>().To<SaveSystem_PlayerPrefs>().AsSingle().NonLazy();
        }
    }
}
