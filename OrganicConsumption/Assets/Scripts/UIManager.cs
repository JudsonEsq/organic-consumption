using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [Header("References:")]
    public RectTransform startMenuUI;
    public RectTransform pauseMenuUI;
    public RectTransform hudUI;
    public RectTransform settingsUI;
    public RectTransform creditsUI;

    public bool onMainMenu = true;
    bool isPaused = false;

    private void Awake()
    {
        if (instance != null) Debug.LogWarning("Attempting to create multiple instances of UI Manager.");
        else instance = this;

        OpenStartMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) TogglePause();
    }

    private void OpenStartMenu()
    {
        SwitchActiveMenu(startMenuUI);
    }
    
    public void StartGame()
    {
        Debug.Log("Starting Game...");
        SwitchActiveMenu(hudUI);
    }

    bool settingsOpen = false;
    public void OpenSettings()
    {
        if (!settingsOpen)
        {
            settingsUI.transform.localPosition = new Vector3(160, -1190, 0);
            settingsUI.DOAnchorPos(new Vector2(160, 0), 1, false);
        }
        else
        {
            settingsUI.transform.localPosition = new Vector3(160, 0, 0);
            settingsUI.DOAnchorPos(new Vector2(160, 1190), 1, false);
        }
        settingsOpen = !settingsOpen;
    }

    public void OpenCredits()
    {

    }

    public void TogglePause()
    {
        if (onMainMenu) return;
        Debug.Log("TogglePause Fired.");
        if (isPaused == false)
        {
            pauseMenuUI.gameObject.SetActive(true);
            //Time.timeScale = 0f;
        }
        else
        {
            pauseMenuUI.gameObject.SetActive(false);
            //Time.timeScale = 1f;
        }
        isPaused = !isPaused;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game....");
        if (isPaused) TogglePause();
        SwitchActiveMenu(startMenuUI);
    }

    private void SwitchActiveMenu(RectTransform targetMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        onMainMenu = (targetMenu == startMenuUI)? true : false;
        targetMenu.gameObject.SetActive(true);
    }
}
