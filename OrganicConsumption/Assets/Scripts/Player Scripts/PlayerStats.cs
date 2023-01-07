using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int health;
    public int scrip;
    // This should be the canvas for the game over screen
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject planter;
    PlayerController pCont;

    // Start is called before the first frame update
    void Start()
    {
        pCont = gameObject.GetComponent<PlayerController>();
        Restart();
    }

    public void Restart()
    {
        health = 3;
        transform.position = new Vector2(0, 0);
        transform.GetComponent<PlayerController>().Reset();
    }

    public void Damage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        health += amount;
    }

    private void Die()
    {
        pCont.Die();
        gameOver.SetActive(true);
        planter.SetActive(false);
        transform.GetComponent<PlayerController>().Die();
    }
}
