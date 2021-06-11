using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    public float Speed;
    public Vector3 Force = new Vector3(10, 0, 0);
    public Vector3 Position = new Vector3(0, 10, 0);

    private Rigidbody rigidbody;

    private Vector3 startingPosition;
    private Quaternion startingRotation;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        Speed = 1;
    }

    private void Update()
    {
        rigidbody.AddForceAtPosition(Force * Input.GetAxis("Horizontal"), Position, ForceMode.Force);

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            transform.position = startingPosition;
            transform.rotation = startingRotation;
        }
    }
}
