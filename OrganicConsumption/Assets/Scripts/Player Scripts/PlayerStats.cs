using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    int health;
    // This should be the canvas for the game over screen
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject planter;
    // Start is called before the first frame update
    void Start()
    {
        restart();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void restart()
    {
        health = 3;
        transform.position = new Vector2(0, 0);
        transform.GetComponent<PlayerController>().Reset();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // MUST ASSIGN THIS TO TAG FOR DAMAGING OBJECTS
        if (collision.otherCollider.gameObject.CompareTag("damage"))
        {
            health--;
            if (health <= 0)
            {
                // For now, we will simply have this appear and disappear on death.
                // TODO: Make this rotate or in some way animate the death screen into vision. 
                die();
            }
        }
    }

    private void die()
    {
        gameOver.SetActive(true);
        planter.SetActive(false);
        transform.GetComponent<PlayerController>().die();
    }
}
