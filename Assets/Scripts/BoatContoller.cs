using UnityEngine;

public class BoatContoller : MonoBehaviour
{
	public float turnSpeed = 3f;
	public float ForceScale = 2f;
	public Vector2 Position = new Vector2(0, -1);

	private Rigidbody2D rigidbody;

	private void Start()
    {
		rigidbody = GetComponent<Rigidbody2D>();

	}

    void Update()
	{
        //if right/left pressed add torque to turn
        if (Input.GetAxis("Vertical") != 0)
		{
			float scale = Mathf.Lerp(0f, turnSpeed, rigidbody.velocity.magnitude);

			float torqueOverTime = -Input.GetAxis("Vertical") * scale * Time.deltaTime;

			rigidbody.AddRelativeForce(Vector2.right * torqueOverTime * ForceScale);
			rigidbody.AddTorque(torqueOverTime*-1);
		}
	}
}
