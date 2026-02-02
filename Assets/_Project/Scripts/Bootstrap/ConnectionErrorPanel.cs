using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class ConnectionErrorPanel : MonoBehaviour
{
    [SerializeField] private GameObject Body;
    [SerializeField] private Button ExitButton;
    
    private AccessChecker _accessChecker;
    
    private UnityAction onExitButtonClickedEvent;
    
    [Inject]
    private void Inject(AccessChecker accessChecker)
    {
       _accessChecker = accessChecker;
    }
    
    private void Start()
    {
        _accessChecker.OnErrorConnection += ShowErrorPanel;
        
        onExitButtonClickedEvent = () =>
        {
            Application.Quit();
        };
        ExitButton.onClick.AddListener(onExitButtonClickedEvent);
    }

    private void OnDestroy()
    {
        _accessChecker.OnErrorConnection -= ShowErrorPanel;
        ExitButton.onClick.RemoveListener(onExitButtonClickedEvent);
    }

    private void ShowErrorPanel()
    {
        Body.SetActive(true);
    }
}
