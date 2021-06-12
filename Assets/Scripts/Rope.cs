using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject EndObject1;
    public GameObject EndObject2;
    public GameObject LinkPrefab;
    public int NumberOfLinks = 7;

    private void Start()
    {
        GenerateRope();
    }

    private void GenerateRope()
    {
        Rigidbody2D lastLinkObject = CreateLink(EndObject1.GetComponent<Rigidbody2D>());

        ConnectToEndObject(EndObject1, lastLinkObject);

        for (var currentLinkId = 0; currentLinkId < NumberOfLinks; currentLinkId++)
        {
            lastLinkObject = CreateLink(lastLinkObject);
        }

        ConnectToEndObject(EndObject2, lastLinkObject);
    }
    private void ConnectToEndObject(GameObject endObject, Rigidbody2D lastRigidBody)
    {
        HingeJoint2D endObjectHinge = endObject.GetComponent<HingeJoint2D>();
        endObjectHinge.connectedBody = lastRigidBody;
    }

    private Rigidbody2D CreateLink(Rigidbody2D lastRigidBody)
    {
        GameObject newLinkObject = Instantiate(LinkPrefab, transform);

        HingeJoint2D newLinkjoint = newLinkObject.GetComponent<HingeJoint2D>();
        newLinkjoint.connectedBody = lastRigidBody;

        return newLinkObject.GetComponent<Rigidbody2D>();
    }
}
