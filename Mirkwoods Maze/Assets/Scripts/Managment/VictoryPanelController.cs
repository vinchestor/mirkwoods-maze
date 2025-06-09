using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPanelController : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] public GameObject pauseButton;

    public void ShowVictoryPanel()
    {
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    public void HidePanel()
    {
        victoryPanel.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
    }
}
