using UnityEngine;

public class BoatContoller : MonoBehaviour
{
	public float turnSpeed = 3f;
	public float ForceScale = 2f;
	public Vector2 Position = new Vector2(0, -1);
	public float DragFactor = 0.01f;

	private Rigidbody2D rigidbodyRef;

	const float BASE_DRAG = 0.2f;
	const float OPTIMAL_ANGLE = -0.707f;
	const float OPTIMAL_ANGLE_GRACE = 0.005f;

	private void Start()
    {
		rigidbodyRef = GetComponent<Rigidbody2D>();
	}

    private void Update()
	{
		AdjustDragBasedOnAngle();

        if (Input.GetAxis("Vertical") != 0)
		{
			float scale = Mathf.Lerp(0f, turnSpeed, rigidbodyRef.velocity.magnitude);

			float torqueOverTime = -Input.GetAxis("Vertical") * scale * Time.deltaTime;

			rigidbodyRef.AddRelativeForce(Vector2.right * torqueOverTime * ForceScale);
			rigidbodyRef.AddTorque(torqueOverTime*-1);
		}
	}

	private void AdjustDragBasedOnAngle()
    {
		if (transform.rotation.z <= OPTIMAL_ANGLE + OPTIMAL_ANGLE_GRACE
			& transform.rotation.z >= OPTIMAL_ANGLE - OPTIMAL_ANGLE_GRACE)
		{
			rigidbodyRef.drag = BASE_DRAG;
			rigidbodyRef.angularDrag = BASE_DRAG * 2;

			return;
		}

		float newDrag = Mathf.Abs(OPTIMAL_ANGLE - transform.rotation.z) * DragFactor;

		newDrag = Mathf.Max(newDrag, BASE_DRAG);

		rigidbodyRef.drag = newDrag;
		rigidbodyRef.angularDrag = newDrag * 2;
	}
}
