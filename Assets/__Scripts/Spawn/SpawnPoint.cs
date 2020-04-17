using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Used to draw wire sphere on scene and spawn gameObjects from it.
 */
public class SpawnPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,
                               0.25f);
    }
}
