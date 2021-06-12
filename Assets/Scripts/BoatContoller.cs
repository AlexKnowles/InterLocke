using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatContoller : MonoBehaviour
{
    public float Speed = 1;
    public Vector2 Force = new Vector2(0, 1);
    public Vector2 Position = new Vector2(-1, 0);

    private Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigidbody.AddForceAtPosition(Force * Input.GetAxis("Vertical"), Position, ForceMode2D.Force);

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.angularVelocity = 0.0f;
            transform.position = Vector2.zero;
            transform.rotation = Quaternion.identity;
        }
    }
}
