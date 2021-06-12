using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    public float Speed = 10;
    public Vector2 Force = new Vector2(10, 0);
    public Vector2 Position = new Vector2(0, 10);

    private Rigidbody2D rigidbodyRef;
    private HingeJoint2D hingeJoint2D;

    private void Start()
    {
        GameManager.Instance.RegisterGameStartMethod(StartGame);

        rigidbodyRef = GetComponent<Rigidbody2D>();
        hingeJoint2D = GetComponent<HingeJoint2D>();

        hingeJoint2D.connectedAnchor = new Vector2(10.53f, -7f);
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameRunning)
        {
            return;
        }

        rigidbodyRef.AddForceAtPosition(Force * Speed * Time.deltaTime, Position, ForceMode2D.Force);
    }

    private void StartGame()
    {
        rigidbodyRef.velocity = Vector2.zero;
        rigidbodyRef.angularVelocity = 0.0f;

        hingeJoint2D.connectedAnchor = new Vector2(10.53f, -7f);

        transform.position = new Vector3(10.53f, -7f, 0);
        transform.rotation = Quaternion.identity;
    }
}
