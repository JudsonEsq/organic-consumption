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
    [SerializeField] Color hoverColor = new Color(0, 0, 0, 1);


    private void Start()
    {
        background = this.transform.GetChild(0).GetComponent<Image>();
        mealName = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        scripIcon = this.transform.GetChild(2).GetComponent<Image>();
        cost = this.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.background.color = hoverColor;
        this.mealName.color = Color.black;
        this.cost.color = Color.black;
        this.scripIcon.color = Color.black;

        itemImage.sprite = mealSprite;
        textDesc.text = mealDescription;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.background.color = Color.black;
        this.mealName.color = Color.white;
        this.cost.color = Color.white;
        this.scripIcon.color = Color.white;
    }
}
