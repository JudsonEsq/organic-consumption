using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu instance { get; private set; }

    [SerializeField] RectTransform topPanel;
    [SerializeField] RectTransform bottomPanel;
    static int offsetPos = 1230;

    [SerializeField] RectTransform settingsSliders;

    private void Awake()
    {
        if (instance != null) Debug.LogWarning("Attempting to create multiple instances of Settings Menu.");
        else instance = this;

        topPanel.localPosition = new Vector3(-offsetPos, -1, 0);
        bottomPanel.localPosition = new Vector3(offsetPos, 0, 0);
        //offsetPos = (int)bottomPanel.localPosition.x;
    }

    public void AnimateBackPanel(bool opening)
    {
        if (opening)
        {
            topPanel.localPosition = new Vector3(-offsetPos, -1, 0);
            bottomPanel.localPosition = new Vector3(offsetPos, 0, 0);

            topPanel.DOAnchorPosX(0, 0.6f, false);
            bottomPanel.DOAnchorPosX(0, 0.6f, false).OnComplete(() => { ToggleSliders(); });
        }
        else
        {
            settingsSliders.gameObject.SetActive(false);
            topPanel.DOAnchorPosX(offsetPos, 0.6f, false);
            bottomPanel.DOAnchorPosX(-offsetPos, 0.6f, false);
        }
    }

    void ToggleSliders()
    {
        settingsSliders.transform.localScale = Vector3.zero;
        settingsSliders.gameObject.SetActive(true);
        settingsSliders.DOScale(1, 0.8f).SetEase(Ease.OutBounce);
    }
}
