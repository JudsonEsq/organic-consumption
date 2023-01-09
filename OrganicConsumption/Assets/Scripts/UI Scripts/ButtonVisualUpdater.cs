using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonVisualUpdater : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI textDesc;
    [SerializeField] Image itemImage;

    [Space, SerializeField] string mealDescription;
    [SerializeField] Sprite mealSprite;

    TextMeshProUGUI mealName;
    TextMeshProUGUI cost;
    Image scripIcon;
    Image background;
    [SerializeField] Color hoverColor;


    private void Awake()
    {
        background = transform.GetChild(0).GetComponent<Image>();
        mealName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        scripIcon = transform.GetChild(2).GetComponent<Image>();
        cost = transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        itemImage.sprite = mealSprite;
        textDesc.text = mealDescription;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        background.color = hoverColor;
        mealName.color = Color.black;
        cost.color = Color.black;
        scripIcon.color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        background.color = Color.black;
        mealName.color = Color.white;
        cost.color = Color.white;
        scripIcon.color = Color.white;
    }
}
