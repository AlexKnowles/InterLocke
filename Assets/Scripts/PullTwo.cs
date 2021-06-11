using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PullTwo : MonoBehaviour
{

    Rigidbody rb;
    public GameObject playerObj;
    public float ForceToPull;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) return;
    }

    bool IsMoving()
    {
        if (rb.velocity.magnitude > 0.06f)
        {
            return true;
        }
        return false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            rb.isKinematic = true;
        }

        if (rb.isKinematic == false)
        {
            //Pushing
            //rb.AddForce(dir * ForceToPush, ForceMode.Force);

            //Pulling
            //rb.velocity = -transform.forward.normalized * ForceToPush;

            //Working
            Vector3 playerPos = playerObj.transform.position - transform.position;
            rb.velocity = playerPos.normalized * ForceToPull;
            //rb.AddForce(playerPos.normalized * ForceToPull, ForceMode.Force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.R))
            {
                rb.isKinematic = false;
                collision.transform.parent = GameObject.Find("PullableObjects").transform;
            }
        }
    }
}