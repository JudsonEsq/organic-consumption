using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashPrompt : MonoBehaviour
{
    
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;

    private SpriteRenderer render;
    private bool crRunning = false;
    private bool mash = false;

    // Start is called before the first frame update
    void Start()
    {
        render = transform.GetComponent<SpriteRenderer>();
    }

    public void Deactivate()
    {
        StopAllCoroutines();
        crRunning = false;
        gameObject.SetActive(false);
    }

    public void Activate(Plant.PlantState state, PlantSO.HarvestInteraction interaction)
    {
        if(state == Plant.PlantState.Deadly && interaction == PlantSO.HarvestInteraction.Mash)
        {
            mash = true;
        }
        else
        {
            mash = false;
        }
        gameObject.SetActive(true);
    }    

    // Update is called once per frame
    void Update()
    {
        if(!crRunning)
        {
            StartCoroutine(mashAnim());
        }
    }

    private IEnumerator mashAnim()
    {
        crRunning = true;
        if (render.sprite == sprite1 || !mash) render.sprite = sprite2;
        else render.sprite = sprite1;

        yield return new WaitForSeconds(0.1f);
        crRunning = false;
    }

}
