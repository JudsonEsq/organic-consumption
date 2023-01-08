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

    public async void Transition()
    {
        await transform.DOScale(new Vector3(0.8f, 0.8f, 1f), 0.2f).AsyncWaitForCompletion();
        transform.DOLocalMove(new Vector3(-600f, 800f, 0), 1f);
        transform.DORotate(new Vector3(0f, 0f, 45f), 1f);
    }
}
