using UnityEngine.SceneManagement;

namespace AsteroidGame
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