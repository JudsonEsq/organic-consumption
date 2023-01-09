using UnityEngine;
using UnityEngine.UI;

public class MiniGameVisuals : MonoBehaviour
{
    Slider bar;

    private void Awake()
    { 
        bar = transform.GetChild(0).GetComponent<Slider>();   
    }

    public void OpenMinigame(Vector3 position)
    {
        // Resets, moves Minigame to space on screen and enables graphics.
        bar.GetComponent<RectTransform>().position = position;
        bar.gameObject.SetActive(true);
    }

    public void CloseMinigame()    
    { // Simply hides the visuals
        bar.value = bar.minValue;
        bar.gameObject.SetActive(false);
    }

    public bool FillBar(float currentValue)
    {// Sets fill bar value to current value;
        bar.value += currentValue;
        
        if (bar.value >= 0.99 * bar.maxValue) return true;
        
        return false;
    }
}
