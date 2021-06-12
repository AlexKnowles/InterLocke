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
    	float temp = Camera.transform.position.x * ParalaxEffect;
        float dist = Camera.transform.position.x * (1 - ParalaxEffect);

        transform.position = new Vector2((startpos + dist) , transform.position.y);

        if (temp > startpos + length) startpos += length;
        if (temp < startpos - length) startpos -= length;

    }
}
