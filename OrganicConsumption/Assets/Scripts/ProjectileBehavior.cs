using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    Vector3 targetPos;
    
    // How far away the target position is
    float distance;
    // How far the pineapple has travelled
    float travelled;

    Vector3 tarDirec;
    
    // Start is called before the first frame update
    void Start()
    {
        tarDirec = targetPos - transform.position;
        distance = tarDirec.magnitude;
        travelled = 0;
    }


    // Update is called once per frame
    void Update()
    {
        travelled += Time.deltaTime;
        if(travelled >= 1.8)
        {
            transform.GetComponent<CircleCollider2D>().enabled = true;
        }
        if(travelled >= 2)
        {
            Destroy(transform.gameObject);
        }

        float currScale = 2 * travelled - Mathf.Pow(travelled, 2) + 1;
        transform.localScale = new Vector3(currScale, currScale, currScale);
        
    }

    public void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("you been struck by");
            collision.collider.gameObject.GetComponent<PlayerStats>().damage(3);
        }
    }
}
