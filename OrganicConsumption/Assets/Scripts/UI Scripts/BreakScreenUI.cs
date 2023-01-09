using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class BreakScreenUI : MonoBehaviour
{
    [SerializeField] Image blackPanel;
    [SerializeField] RectTransform breakTimeImage;
    [SerializeField] RectTransform breakMenuUI;
    [SerializeField] float breakDuration = 30;

    public float speed;
    public Ease easeType;
    public int menuYOffset = 520;
    private bool shopping = false;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && shopping)
        {
            StopCoroutine(ProcessUI());
            shopping = false;
        }
    }

    public void BreakTime()
    {
        breakMenuUI.localPosition = new Vector3(0, -menuYOffset, 0);
        shopping = true;
        StartCoroutine(ProcessUI());
    }

    private IEnumerator ProcessUI()
    {
        Debug.Log("ProcessUI started");
        /*
         * switch (stepIndex)
         * {
         *     case 0:
         *         blackPanel.color = new Color(0, 0, 0, 0.8f);
         *         SlideBreakText();
         *         stepIndex++;
         *         break;
         *     case 1:
         *         breakTimeImage.gameObject.SetActive(false);
         *         SlideBreakMenu(true);
         *         stepIndex++;
         *         break;
         *     case 2:
         *         SlideBreakMenu(false);
         *         stepIndex = 0;
         *         break;
         * }
         *
         */
        blackPanel.color = new Color(0, 0, 0, 0.8f);
        SlideBreakText();
        yield return new WaitForSeconds(0.75f);
        while(!Input.anyKeyDown)
        {
            yield return null;
        }
        Debug.Log("Any Key Pressed");
        breakTimeImage.gameObject.SetActive(false);
        SlideBreakMenu(true);
        yield return (new WaitForSeconds(breakDuration));
        SlideBreakMenu(false);

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
            shopping = false;
        }
    }

    public bool IsShopping()
    {
        return shopping;
    }
}
