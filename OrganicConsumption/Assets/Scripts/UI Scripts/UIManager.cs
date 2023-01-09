using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [Header("References:"), SerializeField]
    RectTransform startScreenUI;
    [SerializeField] GameObject startButton;
    [SerializeField] RectTransform pauseScreenUI;
    [SerializeField] RectTransform hudScreenUI;
    [SerializeField] RectTransform terminationScreenUI;
    [Space, SerializeField]
    RectTransform ModalBackground;
    [SerializeField] SettingsMenu settingsUI;
    [SerializeField] RectTransform creditsUI;

    [SerializeField] AudioManager audManager;

    public bool isPlaying = true;

    private void Start()
    {
        if (instance != null) Debug.LogWarning("Attempting to create multiple instances of UI Manager.");
        else instance = this;

        SwitchActiveScreen(startScreenUI);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P)) OpenDeathScreen();
        if (Input.GetKeyDown(KeyCode.Escape)) CloseModals();
    }
    
    public RectTransform breakScreen;
    public void EnableBreakScreen()
    {
        breakScreen.gameObject.SetActive(true);
    }

    private void SwitchActiveScreen(RectTransform targetMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (targetMenu == startScreenUI)
        {
            EventSystem.current.SetSelectedGameObject(startButton);
            audManager.Play("Menu");
        }

        isPlaying = (targetMenu == hudScreenUI)? true : false;
        targetMenu.gameObject.SetActive(true);
    }
    
    public void OpenGameScreen()
    {
        SwitchActiveScreen(hudScreenUI);
        // Comunicate to Game Manager to start play
    }

    bool settingsOpen = false;
    public void ToggleSettings()
    {
        SettingsMenu.instance.AnimateBackPanel(!settingsOpen);
        settingsOpen = !settingsOpen;
        ModalBackground.gameObject.SetActive(settingsOpen);
    }

    bool creditsOpen = false;
    public void ToggleCredits()
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
        ModalBackground.gameObject.SetActive(creditsOpen);
    }

    public void QuitGame()
    {
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

    public void CloseDeathScreen()
    {
        terminationScreenUI.gameObject.SetActive(false);
    }

    public void CloseModals()
    {
        if (settingsOpen)
        {
            ToggleSettings();
        }
        else if (creditsOpen)
        {
            ToggleCredits();
        }
    }
}
