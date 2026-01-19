using _Project.Scripts.Addressables;
using Cysharp.Threading.Tasks;
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
        [SerializeField] private Image background;
        [SerializeField] private Image ship;

        private UnityAction OnPlayButtonClickEvent;
    
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
        }

        private void OnDestroy()
        {
            playButton.onClick.RemoveListener(OnPlayButtonClickEvent);
        }
    }
}
