using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    public float Speed = 10;
    public Vector2 Force = new Vector2(10, 0);
    public Vector2 Position = new Vector2(0, 10);

    private Rigidbody2D rigidbody;

    private Vector2 startingPosition;
    private Quaternion startingRotation;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        startingRotation = transform.rotation;
    }

    private void Update()
    {
        rigidbody.AddForceAtPosition(Force * Speed * Time.deltaTime, Position, ForceMode2D.Force);

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.angularVelocity = 0.0f;
            transform.position = startingPosition;
            transform.rotation = startingRotation;
        }
    }
}
