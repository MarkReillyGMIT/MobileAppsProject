using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Destroys game objects when they collide with boundary.
 */

[RequireComponent(typeof(BoxCollider2D))]
public class BoundaryCollider : MonoBehaviour
{
    //Destroys gameObjects of type Enemy, EnemyAI, Bullet , ExtraHealth
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if(enemy)
        {
            Destroy(enemy.gameObject);
        }

        var enemyAI = collision.GetComponent<EnemyAI>();
        if (enemyAI)
        {
            Destroy(enemyAI.gameObject);
        }

        var bullet = collision.GetComponent<Bullet>();
        if(bullet)
        {
            Destroy(bullet.gameObject);
        }

        var heart = collision.GetComponent<ExtraHealth>();
        if (heart)
        {
            Destroy(heart.gameObject);
        }
    }
}
