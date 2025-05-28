using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject welcomePanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private PauseManager pauseManager;

    void Start()
    {
        if (ActiveWeapon.Instance != null)
        {
            ActiveWeapon.Instance.gameObject.SetActive(false);
        }

        Time.timeScale = 0f;

        if (welcomePanel != null)
            welcomePanel.SetActive(true);

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

        if (pauseManager != null)
            pauseManager.ButtonSetActiveTrue();

        if (ActiveWeapon.Instance != null)
        {
            ActiveWeapon.Instance.gameObject.SetActive(true);
        }

        Time.timeScale = 1f;
    }
}