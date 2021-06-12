using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatContoller : MonoBehaviour
{
	public float accel = 10f;
	public float turnSpeed = 3f;
	public float maxSpeed = 8f;
	
	private Rigidbody2D rigidbody;

	private void Start()
    {
		rigidbody = GetComponent<Rigidbody2D>();

	}

    void Update()
	{

		////if up pressed
		//if (Input.GetAxis("Vertical") > 0)
		//{
		//	//add force
		//	rigidbody.AddRelativeForce(Vector2.up * accel);

		//	//if we are going too fast, cap speed
		//	if (rigidbody.velocity.magnitude > maxSpeed)
		//	{
		//		rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
		//	}
		//}

		//if right/left pressed add torque to turn
		if (Input.GetAxis("Vertical") != 0)
		{
			//scale the amount you can turn based on current velocity so slower turning below max speed
			float scale = Mathf.Lerp(0f, turnSpeed, (rigidbody.velocity.magnitude / maxSpeed));
			//axis is opposite what we want by default
			rigidbody.AddTorque(-Input.GetAxis("Vertical") * scale * Time.deltaTime);
		}
	}
}
