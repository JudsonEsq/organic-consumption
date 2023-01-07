using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BreakScreenUI : MonoBehaviour
{
    [SerializeField] Image blackPanel;
    [SerializeField] RectTransform breakTimeImage;
    [SerializeField] RectTransform breakMenuUI;

    private int stepIndex = 0;
    public float speed;
    public Ease easeType;
    public int menuYOffset = 520;

    private void OnEnable()
    {
        stepIndex = 0;
        breakMenuUI.localPosition = new Vector3(0, -menuYOffset, 0);
        ProcessUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) ProcessUI();
    }

    private void ProcessUI()
    {
        switch (stepIndex)
        {
            case 0:
                blackPanel.color = new Color(0, 0, 0, 0.8f);
                SlideBreakText();
                stepIndex++;
                break;
            case 1:
                breakTimeImage.gameObject.SetActive(false);
                SlideBreakMenu(true);
                stepIndex++;
                break;
            case 2:
                SlideBreakMenu(false);
                stepIndex = 0;
                break;
        }
    }
    
    private void SlideBreakText()
    {
        breakTimeImage.localPosition = new Vector3(0, 400, 0);
        breakTimeImage.gameObject.SetActive(true);
        breakTimeImage.DOAnchorPos(Vector2.zero, speed, false).SetEase(Ease.OutElastic, 1);
    }

    private void SlideBreakMenu(bool arriving)
    {
        if (arriving)
        {
            breakMenuUI.localPosition = new Vector3(0, -menuYOffset, 0);
            breakMenuUI.gameObject.SetActive(true);
            breakMenuUI.DOAnchorPos(Vector2.zero, speed, false).SetEase(Ease.OutElastic, 0.2f);
        }
        else
        {
            breakMenuUI.DOAnchorPos(new Vector2(0, menuYOffset), 0.5f, false);
            blackPanel.DOFade(0, 1);
        }
    }
}
