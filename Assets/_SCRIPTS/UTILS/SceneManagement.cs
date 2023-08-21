using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Singleton;

    public enum SCENENAMES
    {
        MAINGAME,
        MENU
    };

    private void Awake()
    {
        if (Singleton != null)
            Destroy(this);
        Singleton = this;
        DontDestroyOnLoad(this);
    }

    public void LoadScene(SCENENAMES name)
    {
        SceneManager.LoadScene(name.ToString());
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
