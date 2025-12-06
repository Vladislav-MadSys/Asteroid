using UnityEngine;
using Zenject;
using AsteroidGame;

public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerStates>().AsSingle();
        Container.Bind<PlayerInput>().AsSingle();
        Container.Bind<GameSessionData>().AsSingle().NonLazy();
        Container.Bind<EnemyDeathListener>().AsSingle();
        Container.Bind<SpawnersManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<Camera>().FromInstance(Camera.main).AsSingle().NonLazy();
        Container.Bind<SceneController>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle();
        Container.Bind<PlayerShip>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UIController>().FromComponentInHierarchy().AsSingle();
    }
}
