using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakShopkeep : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] PlayerController controller;
    [SerializeField] UIManager uiMan;
    [SerializeField] GameObject gameplayElements;
    [SerializeField] GameObject winScreen;

    public void PurchaseHeal(int price)
    {
        if(stats.scrip >= price)
        {
            stats.Heal(1);
            stats.scrip -= price;
        }
    }

    public void PurchaseSpeed(int price)
    {
        if(stats.scrip >= price)
        {
            controller.changeSpeed(15, 21);
            stats.scrip -= price;
        }
        
    }

    public void PurchaseHappiness(int price)
    {
        if(stats.scrip >= price)
        {
            uiMan.gameObject.SetActive(false);
            winScreen.SetActive(true);
            AudioManager audMan = FindObjectOfType<AudioManager>();
            audMan.StopAll();
            audMan.Play("Menu");
            audMan.Play("Yahoo");
        }
    }
}
