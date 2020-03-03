using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject PausePanel;
    private bool isPanelOpen;

    void Start()
    {
        PausePanel.SetActive(false);
        isPanelOpen = false;
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
        {
            PanelSwitch();
        }
    }

    public void OnPauseBtnClick()
    {
        PanelSwitch();
    }
    public void OnContinueBtnClick()
    {
        PanelSwitch();
    }
    public void OnHomeBtnClick()
    {
        Time.timeScale = 1;
        GameManager.instance.gameState = GameState.Over;
        GameManager.instance.GameOver();
    }

    private void PanelSwitch()
    {
        isPanelOpen = isPanelOpen ? false : true;
        PausePanel.SetActive(isPanelOpen);
        Time.timeScale = isPanelOpen ? 0 : 1;
    }
}
