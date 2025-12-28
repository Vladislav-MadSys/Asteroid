using _Project.Scripts.Saves;
using Zenject;

public class ProjectContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ISaveService>().To<SaveSystem_PlayerPrefs>().AsSingle().NonLazy();
    }
}
