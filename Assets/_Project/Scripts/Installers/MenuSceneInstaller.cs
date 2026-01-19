
using _Project.Scripts.UI.MainMenu;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class MenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainMenuUIFactory>().AsSingle().NonLazy();
        }
    }
}
