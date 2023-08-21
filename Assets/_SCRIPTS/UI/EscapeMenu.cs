using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] private Button m_continueButton;
    [SerializeField] private Button m_menuButton;
    [SerializeField] private Button m_quitButton;

    private void Start()
    {
        m_continueButton.onClick.AddListener(() => { Time.timeScale = 1; gameObject.SetActive(false); });
        m_menuButton.onClick.AddListener(() => { SceneManagement.Singleton.LoadScene(SceneManagement.SCENENAMES.MENU); });
        m_quitButton.onClick.AddListener(SceneManagement.Singleton.QuitGame);
    }
}
