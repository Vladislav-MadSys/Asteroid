using _Project.Scripts.GameEntities.Enemies.Spawners;
using _Project.Scripts.GameEntities.Player;
using _Project.Scripts.Low;
using _Project.Scripts.Portals;
using _Project.Scripts.Saves;
using _Project.Scripts.Services;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private SpawnerSettings[] spawnerSettings;
        [SerializeField] private GameObject _playerShipPrefab;
        [SerializeField] private GameObject _playerStatsHudViewPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<SceneController>().AsSingle();
            Container.Bind<PlayerStates>().AsSingle();
            Container.Bind<PlayerInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle();
            Container.Bind<PlayerShip>().FromComponentInNewPrefab(_playerShipPrefab).AsSingle();
            Container.Bind<Camera>().FromInstance(Camera.main).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Portal>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnersManager>().AsSingle().NonLazy();
            Container.Bind<SpawnerSettings[]>().FromInstance(spawnerSettings).AsSingle();
            Container.Bind<EnemyDeathListener>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerStatsHudPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerStatsHudModel>().AsSingle().NonLazy();
            Container.Bind<PlayerStatsHudView>().FromComponentInNewPrefab(_playerStatsHudViewPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<GameSessionData>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SceneSaveController>().AsSingle().NonLazy();
        }
    }
}
