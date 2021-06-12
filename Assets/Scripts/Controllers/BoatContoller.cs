using UnityEngine;

public class BoatContoller : MonoBehaviour
{
	public float turnSpeed = 3f;
	public float ForceScale = 2f;
	public Vector2 Position = new Vector2(0, -1);
	public float DragFactor = 0.01f;

	private Rigidbody2D rigidbodyRef;
	private HingeJoint2D hingeJoint2D;

	const float BASE_DRAG = 0.2f;
	const float OPTIMAL_ANGLE = -0.707f;
	const float OPTIMAL_ANGLE_GRACE = 0.005f;

	private void Start()
    {
		GameManager.Instance.RegisterGameStartMethod(StartGame);

		rigidbodyRef = GetComponent<Rigidbody2D>();
		hingeJoint2D = GetComponent<HingeJoint2D>();

		hingeJoint2D.connectedAnchor = new Vector2(-3.45f, 0);
	}

    private void Update()
	{
		if (!GameManager.Instance.IsGameRunning)
		{
			return;
		}

		AdjustDragBasedOnAngle();

        if (Input.GetAxis("Vertical") != 0)
		{
			float scale = Mathf.Lerp(0f, turnSpeed, rigidbodyRef.velocity.magnitude);

			float torqueOverTime = -Input.GetAxis("Vertical") * scale * Time.deltaTime;

			rigidbodyRef.AddRelativeForce(Vector2.right * torqueOverTime * ForceScale);
			rigidbodyRef.AddTorque(torqueOverTime*-1);
		}
	}

	private void OnJointBreak2D(Joint2D brokenJoint)
    {
		GameManager.Instance.SetIsGameOver();
	}

	private void StartGame()
	{
		rigidbodyRef.velocity = Vector2.zero;
		rigidbodyRef.angularVelocity = 0.0f;

		hingeJoint2D.connectedAnchor = new Vector2(-3.45f, 0);

		transform.position = new Vector3(-3.45f, 0, 0);
		transform.rotation = Quaternion.identity;
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