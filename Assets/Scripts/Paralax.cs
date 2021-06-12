using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private float length, startpos;
    public GameObject Camera;
    public float ParalaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x * transform.localScale.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x * transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
    	float camerax = Camera.transform.position.x;
        if(camerax > startpos + length)
        {
            startpos += length;
        }
        if(camerax < startpos - length)
        {
            startpos -= length;
        }
        float distance = camerax * ParalaxEffect;
        transform.position = new Vector2(startpos - distance, transform.position.y);
    }
}
