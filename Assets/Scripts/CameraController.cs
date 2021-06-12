using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public float Speed;


    private void FixedUpdate()
    {
        Vector3 fixedZTargetPosition = new Vector3(Target.position.x, Target.position.y, transform.position.z);

        float hypotenuse = Mathf.Sqrt(Mathf.Pow(Target.position.x, 2) + Mathf.Pow(Target.position.y, 2));

        transform.position = Vector3.Lerp(transform.position, fixedZTargetPosition, (hypotenuse/Speed) * Time.deltaTime);
    }
}
