using System;
using _Project.Scripts.Addressables;
using _Project.Scripts.Low.SceneController;
using _Project.Scripts.Purchases;
using _Project.Scripts.Saves;
using Application = UnityEngine.Application;

namespace _Project.Scripts.UI.MainMenu
{
    public class MainMenuUIPresenter : IDisposable
    {
        private const string REMOVE_ADS_PURCHASE_KEY = "com.DefaultCompany.Asteroid.removeAds";
        
        private MainMenuUIView _view;
        private MainMenuUIModel _model;
        private ISceneController _sceneController;
        private IResourcesService _resourcesService;
        private IPurchaser _purchaser;
    
        public void Initialize(MainMenuUIView view, MainMenuUIModel model, ISceneController sceneController, IResourcesService resourcesService,  IPurchaser purchaser)
        {
            _view = view;
            _model = model;
            _sceneController = sceneController;
            _resourcesService = resourcesService;
            _purchaser = purchaser;
            
            _model.OnConflictWithCloudSave += ShowPanelWithSelectingSave;
        }

        public void Dispose()
        {
            _model.OnConflictWithCloudSave -= ShowPanelWithSelectingSave;
        }
    
        public void OnPlayButtonClicked()
        {
            _resourcesService.Unload(AddressablesKeys.MAIN_MENU_BACKGROUND);
            _resourcesService.Unload(AddressablesKeys.MAIN_MENU_SHIP);
            _sceneController.LoadGameScene();
        }

        public void OnExitButtonClicked()
        {
            Application.Quit();
        }
        
        public void OnSelectLocalSaveButtonClicked()
        {
            _model.SelectSave(false);
        }

        public void OnSelectCloudSaveButtonClicked()
        {
            _model.SelectSave(true);
        }

        public void OnBuyRemoveAdsButtonClick()
        {
            _purchaser.OnPurchaseButtonClicked(REMOVE_ADS_PURCHASE_KEY);
        }
        
        private void ShowPanelWithSelectingSave(SaveData localSaveData, SaveData cloudSaveData)
        {
            _view.ShowPanelWithSelectingSave(localSaveData, cloudSaveData);
        }
    }
}
