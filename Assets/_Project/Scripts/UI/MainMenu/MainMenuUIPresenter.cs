using _Project.Scripts.Addressables;
using _Project.Scripts.Low;
using _Project.Scripts.Low.SceneController;

namespace _Project.Scripts.UI.MainMenu
{
    public class MainMenuUIPresenter
    {
        private MainMenuUIView _view;
        private MainMenuUIModel _model;
        private ISceneController _sceneController;
        private IResourcesService _resourcesService;
    
        public void Initialize(MainMenuUIView view, MainMenuUIModel model, ISceneController sceneController, IResourcesService resourcesService)
        {
            _view = view;
            _model = model;
            _sceneController = sceneController;
            _resourcesService = resourcesService;
        }
    
        public void OnPlayButtonClicked()
        {
            _resourcesService.Unload(AddressablesKeys.MAIN_MENU_BACKGROUND);
            _resourcesService.Unload(AddressablesKeys.MAIN_MENU_SHIP);
            _sceneController.LoadGameScene();
        }
    }
}
