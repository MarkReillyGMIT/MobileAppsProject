using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private float scrollSpeed = 1.5f;

    private Material myMaterial;
    private Vector2 offset;

    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(scrollSpeed, 0f);
    }

    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
        
    }
}
