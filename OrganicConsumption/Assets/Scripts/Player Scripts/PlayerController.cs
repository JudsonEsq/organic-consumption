using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // The player's current vertical and horizontal momentum.
    // Set to 0 by default in case they are ever undefined, for some reason.
    private float horiMove = 0f;
    private float vertMove = 0f;
    // Multiplier that affects how fast the player can move
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float maxSpeed = 10f;
    Rigidbody2D playerBody;

    // do you are not dead?
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horiMove = Input.GetAxis("Horizontal") * moveSpeed;
        vertMove = Input.GetAxis("Vertical") * moveSpeed;

        // This is so that the speed multiplier can be changed however we want, without
        // the player becoming insanely fast. In case we want high acceleration!
        Mathf.Clamp(horiMove, -maxSpeed, maxSpeed);
        Mathf.Clamp(vertMove, -maxSpeed, maxSpeed);

        playerBody.velocity = new Vector2(horiMove, vertMove);
    }

    public void die()
    {
        dead = true;
    }

    public void Reset()
    {
        transform.position = new Vector3(0, 0, 0);
        dead = false;
        horiMove = 0f;
        vertMove = 0f;
    }

    
}