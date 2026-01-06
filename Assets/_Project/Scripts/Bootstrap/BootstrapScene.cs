using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Bootstrap
{
    public class BootstrapScene : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
