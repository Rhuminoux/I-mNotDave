using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private Button m_replayButton;
    [SerializeField] private Button m_menuButton;
    [SerializeField] private Button m_quitButton;

    private void Start()
    {
        m_replayButton.onClick.AddListener(() => { SceneManagement.Singleton.LoadScene(SceneManagement.SCENENAMES.MAINGAME); });
        m_menuButton.onClick.AddListener(() => { SceneManagement.Singleton.LoadScene(SceneManagement.SCENENAMES.MENU); });
        m_quitButton.onClick.AddListener(SceneManagement.Singleton.QuitGame);
    }
}
