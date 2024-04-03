using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;
    private void Start()
    {
        KitChenGameManager.Instance.OnGamePause +=KitChenGameManager_OnGamePause;
        KitChenGameManager.Instance.OnGameUnPaused += KitChenGameManagerOnGameUnPaused;
        resumeButton.onClick.AddListener(() =>
        {
            KitChenGameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
   
        });
        
        optionsButton.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.Instance.Show(Show);
        });
        Hide();
    }

    private void KitChenGameManagerOnGameUnPaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void KitChenGameManager_OnGamePause(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
