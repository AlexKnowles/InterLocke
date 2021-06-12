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
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Camera.transform.position.x * (1 - ParalaxEffect) * -1;
        float distance = Camera.transform.position.x * ParalaxEffect * -1;
        
        transform.position = new Vector2(startpos + distance, transform.position.y);
        if (offset > startpos + length) startpos += length;
        else if (offset < startpos - length) startpos -= length;
    }
}
