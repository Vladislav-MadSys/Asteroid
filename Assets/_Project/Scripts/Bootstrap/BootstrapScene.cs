using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapScene : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("Game");
    }
}
