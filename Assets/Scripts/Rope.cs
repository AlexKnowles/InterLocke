using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject EndObject1;
    public GameObject EndObject2;
    public GameObject LinkPrefab;
    public int NumberOfLinks = 7;

    private bool ropeExists;
    private LineRenderer LineRenderer;
    private List<GameObject> jointGameObjects;
    private bool firedDestoryRope;

    private void Start()
    {
        GameManager.Instance.RegisterGameStartMethod(GenerateRope);

        LineRenderer = GetComponent<LineRenderer>();
        jointGameObjects = new List<GameObject>();
    }

    private void Update()
    {
        if (ropeExists)
        {
            UpdateLinesBetweenJoints();

            if (GameManager.Instance.IsGameOver && !firedDestoryRope)
            {
                firedDestoryRope = true;
                StartCoroutine(DestroyRopeInASecond());
            }
        }
    }

    public IEnumerator DestroyRopeInASecond()
    {
        jointGameObjects.RemoveAt(0);

        yield return new WaitForSeconds(1);

        if (!GameManager.Instance.IsGameRunning)
        {
            DestroyRope();
        }
    }

    private void GenerateRope()
    {
        DestroyRope();

        GenerateJointsForRope();

        firedDestoryRope = false;
        ropeExists = true;
    }

    private void GenerateJointsForRope()
    {
        GameObject newLinkObject = Instantiate(LinkPrefab, transform);
        HingeJoint2D newLinkjoint = newLinkObject.GetComponent<HingeJoint2D>();

        Rigidbody2D lastLinkObject = newLinkObject.GetComponent<Rigidbody2D>();
        newLinkjoint.connectedBody = lastLinkObject;

        ConnectToEndObject(EndObject1, lastLinkObject);

        jointGameObjects.Add(EndObject1);
        jointGameObjects.Add(newLinkObject);

        for (var currentLinkId = 0; currentLinkId < NumberOfLinks; currentLinkId++)
        {
            lastLinkObject = CreateLink(lastLinkObject);
        }

        ConnectToEndObject(EndObject2, lastLinkObject);
        jointGameObjects.Add(EndObject2);
    }

    private void UpdateLinesBetweenJoints()
    {
        LineRenderer.positionCount = jointGameObjects.Count;
        LineRenderer.SetPositions(jointGameObjects.Select(p => p.transform.position).ToArray());
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
        jointGameObjects.Add(newLinkObject);

        HingeJoint2D newLinkjoint = newLinkObject.GetComponent<HingeJoint2D>();
        newLinkjoint.connectedBody = lastRigidBody;

        return newLinkObject.GetComponent<Rigidbody2D>();
    }

    private void DestroyRope()
    {
        jointGameObjects = new List<GameObject>();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        LineRenderer.positionCount = 0;

        ropeExists = false;
    }
}
