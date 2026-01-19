using System;
using _Project.Scripts.Addressables;
using _Project.Scripts.Factories;
using _Project.Scripts.Low;
using _Project.Scripts.Services;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

public class PlayerHUDFactory : IInitializable
{
    protected SceneController _sceneController;
    private PlayerStates _playerStates;
    private GameSessionData _gameSessionData;
    private IResourcesService _resourcesService;
    
    protected PlayerStatsHudModel _model;
    protected PlayerStatsHudView _view;
    protected PlayerStatsHudPresenter _presenter;
    
    [Inject]
    private void Inject(SceneController sceneController, PlayerStates playerStates, GameSessionData gameSessionData, IResourcesService resourcesService)
    {
        _sceneController = sceneController;
        _playerStates = playerStates;
        _gameSessionData = gameSessionData;
        _resourcesService = resourcesService;
    }
    
    public async void Initialize()
    {
        _model = new PlayerStatsHudModel();
        _presenter = new PlayerStatsHudPresenter();
        var hudPrefab = await _resourcesService.Load(AddressablesKeys.PLAYER_HUD_CANVAS);
        GameObjectFactory gameObjectFactory = new GameObjectFactory(hudPrefab);
        GameObject hud = gameObjectFactory.Create();
        _view = hud.GetComponent<PlayerStatsHudView>();
        
        _model.Initialize(_playerStates, _gameSessionData);
        _presenter.Initialize(_sceneController, _model, _view);
        _view.Initialize(_presenter);
    }

}
