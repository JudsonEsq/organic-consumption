using UnityEngine;
using UnityEngine.UI;

public class MiniGameVisuals : MonoBehaviour
{
    [SerializeField] Sprite KeyUp, KeyDown;
    Image visual;
    Slider bar;

    private void Awake()
    {
        visual = transform.GetChild(0).GetComponent<Image>();   
        bar = transform.GetChild(1).GetComponent<Slider>();   
    }

    public void OpenMinigame(bool isMash, Vector3 position)
    {
        // Resets, moves Minigame to space on screen and enables correct parts for minigame type.
        transform.localPosition = position;
        SetVisual(false);
        visual.gameObject.SetActive(true);
        
        if (isMash)
        {
            bar.gameObject.SetActive(false);
        }
        else
        {
            bar.gameObject.SetActive(true);
            bar.value = 0;
        }
    }

    public void CloseMinigame()    
    { // Simply hides the visuals
        visual.gameObject.SetActive(false);
        bar.gameObject.SetActive(false);
    }

    public void ToggleKey()
    {// Toggles the visual back and forth between key up and down
        if (visual.sprite == KeyUp) visual.sprite = KeyDown;
        else visual.sprite = KeyUp;
    }

    public void SetVisual(bool keyIsDown)
    {// Sets the key visual to a specific position
        if (keyIsDown) visual.sprite = KeyDown;
        else visual.sprite = KeyUp;
    }

    public void FillBar(float currentValue)
    {// Sets fill bar value to current value;
        bar.value = currentValue;
    }
}
