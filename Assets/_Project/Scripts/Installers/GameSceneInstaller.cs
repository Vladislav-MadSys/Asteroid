using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameEvents>().AsSingle();
        Container.Bind<PlayerInput>().AsSingle();
        Container.Bind<Camera>().FromInstance(Camera.main).AsSingle().NonLazy();
        Container.Bind<SceneController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle();
        Container.Bind<PlayerShip>().FromComponentInHierarchy().AsSingle();
    }
}
