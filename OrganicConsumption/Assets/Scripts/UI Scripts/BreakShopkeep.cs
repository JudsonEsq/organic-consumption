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

    public void PurchaseHeal(int amount, int price)
    {
        if(stats.scrip >= price)
        {
            stats.Heal(amount);
            stats.scrip -= price;
        }
    }

    public void PurchaseSpeed(float value, int price)
    {
        if(stats.scrip >= price)
        {
            controller.changeSpeed(value, 1.5f * value);
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
        }
    }
}
