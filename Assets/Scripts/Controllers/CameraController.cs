using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target1;
    public Transform Target2;
    public float CatchupSlowFactor;

    private Camera camera;

    private void Start()
    {
        GameManager.Instance.RegisterGameStartMethod(StartGame);
        camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        if(!GameManager.Instance.IsGameRunning)
        {
            return;
        }

        float distanceBetweenTargets = Mathf.Sqrt(Mathf.Pow(Target2.position.x - Target1.position.x, 2) + Mathf.Pow(Target2.position.y - Target1.position.y, 2));

        float desiredX = (distanceBetweenTargets / 2) * ((Target2.position.x - Target1.position.x) / distanceBetweenTargets);
        float desiredY = (distanceBetweenTargets / 2) * ((Target2.position.y - Target1.position.y) / distanceBetweenTargets);

        Vector3 centerBetweenTargets = new Vector3(Target1.position.x + desiredX, transform.position.y, transform.position.z);
        float hypotenuse = (distanceBetweenTargets / 2);

        transform.position = Vector3.Lerp(transform.position, centerBetweenTargets, (hypotenuse/ CatchupSlowFactor) * Time.deltaTime);

        camera.orthographicSize = Mathf.Max((hypotenuse), 10); 
    }

    private void StartGame()
    {
        transform.position = new Vector3(0, 0, -10);
    }
}
