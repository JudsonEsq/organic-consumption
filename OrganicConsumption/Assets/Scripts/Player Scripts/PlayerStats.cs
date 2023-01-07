using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    int health;
    // This should be the canvas for the game over screen
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject planter;
    PlayerController pCont;

    // Start is called before the first frame update
    void Start()
    {
        pCont = gameObject.GetComponent<PlayerController>();
        restart();
    }

    public void restart()
    {
        health = 3;
        transform.position = new Vector2(0, 0);
        transform.GetComponent<PlayerController>().Reset();
    }

    public void damage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            die();
        }
    }

    private void die()
    {
        pCont.die();
        gameOver.SetActive(true);
        planter.SetActive(false);
        transform.GetComponent<PlayerController>().die();
    }
}
