using _Project.Scripts.GameEntities.Enemies.Spawners;
using _Project.Scripts.GameEntities.Player;
using _Project.Scripts.Low;
using _Project.Scripts.Portals;
using _Project.Scripts.Saves;
using _Project.Scripts.Services;
using _Project.Scripts.SFX;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Gameplay;
using _Project.Scripts.VFX;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private SpawnerSettings[] spawnerSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerStates>().AsSingle().NonLazy();
            Container.Bind<PlayerInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle().NonLazy();
            Container.Bind<Camera>().FromInstance(Camera.main).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Portal>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnersManager>().AsSingle().NonLazy();
            Container.Bind<SpawnerSettings[]>().FromInstance(spawnerSettings).AsSingle();
            Container.Bind<EnemyDeathListener>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerHUDFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameSessionData>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SceneSaveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<VFXManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SFXManager>().AsSingle().NonLazy();
        }
    }
}
