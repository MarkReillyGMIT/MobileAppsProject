using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // == public fields ==

    // == private fields ==
    private Rigidbody2D rb;

    private Camera gameCamera;
    
    [SerializeField] private float speed = 5.0f;

    // == private methods ==


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // add hMovement
        // if the player presses the up arrow, then move
        float vMovement = Input.GetAxis("Vertical");
        float hMovement = Input.GetAxis("Horizontal");
        // apply a force, get the player moving.
        rb.velocity = new Vector2(hMovement * speed, 
                                  vMovement * speed);
        // keep the player on the screen
        // float xValue = Mathf.clamp(value, min, max)
        // rb.position.x 
        float yValue = Mathf.Clamp(rb.position.y, -4.36f, 4.08f);
        float xValue = Mathf.Clamp(rb.position.x, -13.25f, 13.4f);

        rb.position = new Vector2(xValue, yValue);
    }
}
