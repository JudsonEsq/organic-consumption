using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CreditsSlider : MonoBehaviour
{
    RectTransform panel;

    private void Awake()
    {
        panel = transform.GetChild(0).GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        panel.localPosition = new Vector3(0, 600, 0);
        panel.DOAnchorPosY(0, 1.4f, false).SetEase(Ease.InOutElastic);
    }
}
