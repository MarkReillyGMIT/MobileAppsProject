using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallingBehaviour : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private float speed = 5.0f;

    private Rigidbody2D rb;

    // == private methods ==
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.down * speed * Time.deltaTime;
    }
}
