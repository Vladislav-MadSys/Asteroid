using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerInput>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle();

    }
}
