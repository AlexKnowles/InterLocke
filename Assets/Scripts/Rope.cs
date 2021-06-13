using System.Collections;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject EndObject1;
    public GameObject EndObject2;
    public GameObject LinkPrefab;
    public int NumberOfLinks = 7;

    private bool ropeExists;

    private void Start()
    {
        GameManager.Instance.RegisterGameStartMethod(GenerateRope);
    }

    private void Update()
    {
        if(GameManager.Instance.IsGameOver && ropeExists)
        {
            StartCoroutine(DestroyRopeInASecond());            
        }
    }

    public IEnumerator DestroyRopeInASecond()
    {
        yield return new WaitForSeconds(1);

        if (!GameManager.Instance.IsGameRunning)
        {
            DestroyRope();
        }
    }

    private void GenerateRope()
    {
        DestroyRope();

        GameObject newLinkObject = Instantiate(LinkPrefab, transform);
        HingeJoint2D newLinkjoint = newLinkObject.GetComponent<HingeJoint2D>();

        Rigidbody2D lastLinkObject = newLinkObject.GetComponent<Rigidbody2D>();
        newLinkjoint.connectedBody = lastLinkObject;

        ConnectToEndObject(EndObject1, lastLinkObject);

        for (var currentLinkId = 0; currentLinkId < NumberOfLinks; currentLinkId++)
        {
            lastLinkObject = CreateLink(lastLinkObject);
        }

        ConnectToEndObject(EndObject2, lastLinkObject);

        ropeExists = true;
    }
    private void ConnectToEndObject(GameObject endObject, Rigidbody2D lastRigidBody)
    {
        HingeJoint2D endObjectHinge = endObject.GetComponent<HingeJoint2D>();
        endObjectHinge.connectedBody = lastRigidBody;
        endObjectHinge.connectedAnchor = Vector2.zero;
    }

    private Rigidbody2D CreateLink(Rigidbody2D lastRigidBody)
    {
        GameObject newLinkObject = Instantiate(LinkPrefab, transform);

        HingeJoint2D newLinkjoint = newLinkObject.GetComponent<HingeJoint2D>();
        newLinkjoint.connectedBody = lastRigidBody;

        return newLinkObject.GetComponent<Rigidbody2D>();
    }

    private void DestroyRope()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        ropeExists = false;
    }
}
