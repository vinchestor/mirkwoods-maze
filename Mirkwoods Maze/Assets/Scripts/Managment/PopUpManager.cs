using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject welcomePanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private PauseManager pauseManager;

    void Start()
    {
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.gameObject.SetActive(false);
        }

        if (welcomePanel != null)
            welcomePanel.SetActive(true);

        Time.timeScale = 0f;

        if (pauseManager != null)
            pauseManager.ButtonSetActiveFalse();
    }

    public void HideWelcomePanel()
    {
        if (welcomePanel != null)
            welcomePanel.SetActive(false);

        if (controlsPanel != null)
            controlsPanel.SetActive(true);
    }

    public void HideControlsPanel()
    {
        if (controlsPanel != null)
            controlsPanel.SetActive(false);

        Time.timeScale = 1f;

        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.gameObject.SetActive(true);
        }

        if (pauseManager != null)
            pauseManager.ButtonSetActiveTrue();
    }
}