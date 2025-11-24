using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameEvents>().AsSingle();
        Container.Bind<PlayerInput>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle();
        Container.Bind<PlayerShip>().FromComponentInHierarchy().AsSingle();
    }
}
