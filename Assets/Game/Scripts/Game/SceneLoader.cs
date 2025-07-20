using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader 
{
    public static void Restart()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentScene);
    }

    public static void Exit()
    {
        Application.Quit();
    }
}
