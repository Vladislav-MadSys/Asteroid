using UnityEngine.SceneManagement;

namespace _Project.Scripts.Low.SceneController
{
    public class SceneController : ISceneController
    {
        private const string MENU_SCENE_NAME = "MainMenu";
        private const string GAME_SCENE_NAME = "Game";
        
        public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene(GAME_SCENE_NAME);
        }

        public void LoadMainMenuScene()
        {
            SceneManager.LoadScene(MENU_SCENE_NAME);
        }
    }
}