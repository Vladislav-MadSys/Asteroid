using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using Zenject;

namespace _Project.Scripts.Services
{
    public class AccessChecker : IInitializable, IDisposable
    {
        private const string URL_TO_CHECK = "https://storage.yandexcloud.net/asteroidgamedevforge";

        public event Action OnSeccessConnection;
        public event Action OnErrorConnection;
    
        private int _timeToCheck = 10;
        private CancellationTokenSource _cancellationToken;

        public void Initialize()
        {
            _cancellationToken = new CancellationTokenSource();
            CheckAccess();
        }

        public void Dispose()
        {
            _cancellationToken.Cancel();
        }

        private async UniTask CheckAccess()
        {
            try
            {
                using (var request = UnityWebRequest.Head(URL_TO_CHECK))
                {
                    request.timeout = _timeToCheck;

                    await request.SendWebRequest().ToUniTask(cancellationToken: _cancellationToken.Token);

                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        OnSeccessConnection?.Invoke();
                    }
                    else
                    {
                        OnErrorConnection?.Invoke();
                    }
                }
            }
            catch
            {
                OnErrorConnection?.Invoke();
            }
        }
    }
}
