using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private Vector3 targetPos;
    
    // How far away the target position is
    private float distance;
    // How far the pineapple has travelled
    private float travelled = 0;

    private Vector3 tarDirec;
    
    void setTargetPos(Vector3 tar)
    {
        targetPos = tar;
        tarDirec = targetPos - transform.position;
        distance = tarDirec.magnitude;
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
        Debug.Log(travelled);
        transform.position += tarDirec * (Time.deltaTime / 2f);
        transform.Rotate(new Vector3(0, 0, 150 * Time.deltaTime));
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("you been struck by");
            collision.gameObject.GetComponent<PlayerStats>().damage(3);
        }
    }
}
