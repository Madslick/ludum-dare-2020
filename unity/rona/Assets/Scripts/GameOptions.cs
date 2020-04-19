using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOptions : MonoBehaviour
{
    public void StartGame () {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void QuitGame () {
        Application.Quit();
    }

    public void LoadSplashScreen () {
        SceneManager.LoadScene("SplashScene", LoadSceneMode.Single);
    }

    public void LoadSceneNameSingle (string name) {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

}
