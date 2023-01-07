using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuMotion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Transition()
    {
        transform.DOScale(new Vector3(0.8f, 0.8f, 0), 0.2f);
        transform.DOMove(new Vector3(-6f, 6, 1), 1f);
    }
}
