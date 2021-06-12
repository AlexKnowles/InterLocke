using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HingeJoint2D))]
public class Rope : MonoBehaviour
{
    public GameObject LinkPrefab;
    public int NumberOfLinks = 7;
    public GameObject Horse;

    private void Start()
    {
        GenerateRope();
    }

    private void GenerateRope()
    {
        Rigidbody2D lastLinkObject = GetComponent<Rigidbody2D>();

        for (var currentLinkId = 0; currentLinkId < NumberOfLinks; currentLinkId++)
        {
            GameObject newLinkObject = Instantiate(LinkPrefab, transform);
            newLinkObject.name = ("link_" + currentLinkId);

            HingeJoint2D newLinkjoint = newLinkObject.GetComponent<HingeJoint2D>();
            newLinkjoint.connectedBody = lastLinkObject;

            lastLinkObject = newLinkObject.GetComponent<Rigidbody2D>();

        }

        HingeJoint2D norseBody = Horse.GetComponent<HingeJoint2D>();
        norseBody.connectedBody = lastLinkObject;
    }
}
