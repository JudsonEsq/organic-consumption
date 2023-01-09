using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HPVisuals : MonoBehaviour
{
    [SerializeField] Sprite HPFull, HP2, HP1, HP0;
    Image graphic;
    int oldHealth = 3;

    private void Awake() => graphic = transform.GetChild(0).GetComponent<Image>(); 

    public void UpdateHealthVisual(int currentHealth)
    {
        if (oldHealth > currentHealth)
        {
            transform.DOPunchPosition(new Vector3(10, 0, 0), 1);
        }
        oldHealth = currentHealth;

        switch (currentHealth)
        {
            case 3:
                graphic.sprite = HPFull;
                break;
            case 2:
                graphic.sprite = HP2;
                break;
            case 1:
                graphic.sprite = HP1;
                break;
            default:
                graphic.sprite = HP0;
                break;
        }
    }
}