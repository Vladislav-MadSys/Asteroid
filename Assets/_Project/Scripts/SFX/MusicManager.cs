using _Project.Scripts.Addressables;
using _Project.Scripts.Factories;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.SFX
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioSource MusicEmitter;
        
        private IResourcesService _resourcesService;
        private AudioClip _musicClip;

        [Inject]
        private void Inject(IResourcesService resourcesService)
        {
            _resourcesService = resourcesService;
        }
        
        private async void Start()
        {
            _musicClip = await _resourcesService.Load<AudioClip>(AddressablesKeys.BACKGROUND_MUSIC);
            
            MusicEmitter.clip = _musicClip;
            MusicEmitter.loop = true;
            MusicEmitter.Play();
        }
    }
}
