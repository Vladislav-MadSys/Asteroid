using _Project.Scripts.Addressables;
using _Project.Scripts.Saves;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts.UI.MainMenu
{
    public class MainMenuUIView : MonoBehaviour
    {
        private MainMenuUIPresenter _presenter;
        private IResourcesService _resourcesService;
    
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button buyRemoveAdsButton;
        [SerializeField] private Image background;
        [SerializeField] private Image ship;
    
        [Header("Save Conflict Panel")] 
        [SerializeField] private GameObject conflictPanel;
        [SerializeField] private TextMeshProUGUI conflictPanelText;
        [SerializeField] private Button selectLocalSaveButton;
        [SerializeField] private Button selectCloudSaveButton;
        
        private UnityAction OnPlayButtonClickEvent;
        private UnityAction OnExitButtonClickEvent;
        private UnityAction OnBuyRemoveAdsButtonClickEvent;
        private UnityAction OnSelectLocalSaveButtonClickEvent;
        private UnityAction OnSelectCloudSaveButtonClickEvent;
    
        public async void Initialize(MainMenuUIPresenter presenter, IResourcesService resourcesService)
        {
            _presenter = presenter;
            _resourcesService = resourcesService;
        
            var bgSpriteTask = _resourcesService.Load<Sprite>(AddressablesKeys.MAIN_MENU_BACKGROUND);
            var shipSpriteTask = _resourcesService.Load<Sprite>(AddressablesKeys.MAIN_MENU_SHIP);
            var (bgSprite, shipSprite) = await UniTask.WhenAll(bgSpriteTask, shipSpriteTask);
            background.sprite = bgSprite;
            ship.sprite = shipSprite;
        }

        private void Awake()
        {
            OnPlayButtonClickEvent = () =>
            {
                _presenter.OnPlayButtonClicked();
            };
            playButton.onClick.AddListener(OnPlayButtonClickEvent);
            
            OnExitButtonClickEvent = () =>
            {
                _presenter.OnExitButtonClicked();
            };
            exitButton.onClick.AddListener(OnExitButtonClickEvent);
            
            OnSelectLocalSaveButtonClickEvent = () =>
            {
                _presenter.OnSelectLocalSaveButtonClicked();
                conflictPanel.SetActive(false);
            };
            selectLocalSaveButton.onClick.AddListener(OnSelectLocalSaveButtonClickEvent);

            OnSelectCloudSaveButtonClickEvent = () =>
            {
                _presenter.OnSelectCloudSaveButtonClicked();
                conflictPanel.SetActive(false);
            };
            selectCloudSaveButton.onClick.AddListener(OnSelectCloudSaveButtonClickEvent);

            OnBuyRemoveAdsButtonClickEvent = () =>
            {
                _presenter.OnBuyRemoveAdsButtonClick();
            };
            buyRemoveAdsButton.onClick.AddListener(OnBuyRemoveAdsButtonClickEvent);
        }

        private void OnDestroy()
        {
            playButton.onClick.RemoveListener(OnPlayButtonClickEvent);
        }
        
        public void ShowPanelWithSelectingSave(SaveData localSaveData, SaveData cloudSaveData)
        {
            conflictPanelText.text =
                $"You have a cloud save from {cloudSaveData.saveTime}\nWhich one do you want to continue with?";
            conflictPanel.SetActive(true);
        }
        
    }
}
