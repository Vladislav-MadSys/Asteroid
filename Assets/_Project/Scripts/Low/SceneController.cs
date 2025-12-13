using UnityEngine.SceneManagement;

namespace _Project.Scripts.Low
{
    public class SceneController
    {
        public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadSceneWithKey(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

    }
}