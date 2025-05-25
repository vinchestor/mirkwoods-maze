using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] public GameObject pauseButton;
    [SerializeField] public GameObject pausePanel;
    public bool isPause = false;

    public void PauseButtonClick()
    {
        if (isPause)
        {
            isPause = false;
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            pauseButton.SetActive(true);
        }
        else
        {
            isPause = true;
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            pauseButton.SetActive(false);
        }
    }

    public void ButtonSetActiveFalse()
    {
        if (pauseButton != null)
            pauseButton.SetActive(false);
    }

    public void ButtonSetActiveTrue()
    {
        if (pauseButton != null)
            pauseButton.SetActive(true);
    }
}