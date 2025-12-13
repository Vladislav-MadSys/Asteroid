using _Project.Scripts.GameEntities.Enemies.Spawners;
using _Project.Scripts.GameEntities.Player;
using _Project.Scripts.Low;
using _Project.Scripts.Portals;
using _Project.Scripts.Services;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerStates>().AsSingle();
            Container.Bind<PlayerInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<Portal>().AsSingle().NonLazy();
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
}
