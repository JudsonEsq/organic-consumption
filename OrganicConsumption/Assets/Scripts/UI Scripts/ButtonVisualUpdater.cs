using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonVisualUpdater : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TextMeshPro name;
    TextMeshPro cost;

    private void Awake()
    {
        transform.GetChild(0).GetComponent<TextMeshPro>();
        //transform.GetChild(1).GetComponent<>();
        transform.GetChild(2).GetComponent<TextMeshPro>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exited");
    }
}
