using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;

    private void Start()
    {
        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            PlayerController.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
            UIFade.Instance.FadeToClear();
        }

        //Если это финальная сцена — показать панель победы
        if (SceneManager.GetActiveScene().name == "Scene2")
        {
            VictoryPanelController panel = FindObjectOfType<VictoryPanelController>();
            if (panel != null)
            {
                StartCoroutine(ShowVictoryWithDelay());
            }
        }
    }

    private IEnumerator ShowVictoryWithDelay()
    {
        yield return new WaitForSeconds(5f); 

        VictoryPanelController panel = FindObjectOfType<VictoryPanelController>();
        if (panel != null)
        {
            panel.ShowVictoryPanel();
        }
    }
}
