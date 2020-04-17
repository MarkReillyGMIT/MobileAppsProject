using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Controls the add extra health gameObject and moves it to the left 
 */
[RequireComponent(typeof(Rigidbody2D))]
public class ExtraHealthBehaviour : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private float speed = 5.0f;

    private Rigidbody2D rb;

    // == private methods ==
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(-1 * speed, 0);
    }

    // == public method ==
    public void SetMoveSpeed(float healthSpeed)
    {
        this.speed = healthSpeed;
    }

}
