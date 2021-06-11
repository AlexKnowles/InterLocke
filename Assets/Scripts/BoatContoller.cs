using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatContoller : MonoBehaviour
{
    public float Speed;
    public Vector3 Force = new Vector3(0, 0, 1);
    public Vector3 Position = new Vector3(-1, 0, 1);

    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Speed = 1;
    }

    private void Update()
    {
        rigidbody.AddForceAtPosition(Force * Input.GetAxis("Vertical"), Position, ForceMode.Force);

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
    }
}
