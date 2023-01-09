using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private int health;
    public int scrip = 0;
    private bool dead = false;
    [SerializeField] GameObject planter;
    [SerializeField] TMPro.TMP_Text scripCounter;
    PlayerController pCont;

    // Start is called before the first frame update
    void Start()
    {
        pCont = gameObject.GetComponent<PlayerController>();
        health = 3;
    }

    private void Update()
    {
        scripCounter.text = scrip.ToString();
    }

    public void Restart()
    {
        dead = false;
        scrip = 0;
        health = 3;
        transform.position = new Vector2(0, 0);
        transform.GetComponent<PlayerController>().Reset();
        planter.GetComponent<Planter>().Activate();
    }

    public void Damage(int amount)
    {
        health -= amount;
        if(health <= 0 && !dead)
        {
            StartCoroutine(Die());
        }
    }

    public void Heal(int amount)
    {
        health += amount;
    }

    private IEnumerator Die()
    {
        dead = true;
        pCont.Die();
        planter.GetComponent<Planter>().HardReset(true);
        yield return new WaitForSeconds(0.5f);
        UIManager.instance.OpenDeathScreen();
    }
}
