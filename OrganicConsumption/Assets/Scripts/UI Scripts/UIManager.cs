using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [Header("References:")]
    public RectTransform startScreenUI;
    public RectTransform pauseScreenUI;
    public RectTransform hudScreenUI;
    public SettingsMenu settingsUI;
    public RectTransform creditsUI;
    public RectTransform terminationScreenUI;

    public bool onMainMenu = true;
    bool isPaused = false;

    private void Awake()
    {
        if (instance != null) Debug.LogWarning("Attempting to create multiple instances of UI Manager.");
        else instance = this;

        SwitchActiveScreen(startScreenUI);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) TogglePause();
        if (Input.GetKeyDown(KeyCode.B)) ToggleBreak();
        if (Input.GetKeyDown(KeyCode.P)) OpenDeathScreen();
    }

    public RectTransform breakScreen;
    void ToggleBreak()
    {
        breakScreen.gameObject.SetActive(true);
    }
    private void SwitchActiveScreen(RectTransform targetMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        onMainMenu = (targetMenu == startScreenUI)? true : false;
        targetMenu.gameObject.SetActive(true);
    }
    
    public void StartGame()
    {
        Debug.Log("Starting Game...");
        SwitchActiveScreen(hudScreenUI);
        // Comunicate to Game Manager to start play
    }

    bool settingsOpen = false;
    public void OpenSettings()
    {
        SettingsMenu.instance.AnimateBackPanel(!settingsOpen);
        settingsOpen = !settingsOpen;
    }

    bool creditsOpen = false;
    public void OpenCredits()
    {
        if (!creditsOpen)
        {
            creditsUI.gameObject.SetActive(true);
        }
        else
        {
            creditsUI.gameObject.SetActive(false);
        }
        creditsOpen = !creditsOpen;
    }

    public void TogglePause()
    {
        if (onMainMenu) return;
        if (isPaused == false)
        {
            pauseScreenUI.gameObject.SetActive(true);
        }
        else
        {
            pauseScreenUI.gameObject.SetActive(false);
        }
        isPaused = !isPaused;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game....");
        if (isPaused) TogglePause();
        SwitchActiveScreen(startScreenUI);
        // Comunicate to Game Manager to stop play
    }

    public AnimationCurve terminationCurve;
    public void OpenDeathScreen()
    {
        Image background =  terminationScreenUI.GetChild(0).GetComponent<Image>();
        background.color = new Color(0, 0, 0, 0);
        terminationScreenUI.GetChild(1).localPosition = new Vector3(552, -790, 0);

        terminationScreenUI.gameObject.SetActive(true);
        background.DOFade(0.8f, 0.5f);
        terminationScreenUI.GetChild(1).DOMoveY(0, 1, true).SetEase(terminationCurve);
    }
}
