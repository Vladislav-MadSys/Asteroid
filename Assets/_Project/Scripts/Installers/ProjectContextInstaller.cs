using _Project.Scripts.Saves;
using _Project.Scripts.Services;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SaveDataConstructor>().AsSingle().NonLazy();
            Container.Bind<ISaveService>().To<SaveSystemPlayerPrefs>().AsSingle().NonLazy();
        }
    }
}
