using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NPCMovement : MonoBehaviour
{
    Vector3 m_GroundNormal;

    public float speed = 12f; // the speed our tank moves
    public float turnSpeed = 180f; // the speed that we can turn in degrees in seconds.
    public float distanceTo; // distance to next target

    public List<Transform> nodeToMoveTo = new List<Transform>();
    public Transform currentTargetNode;

    public bool enableDebug = false; // enables/disables debug logs

    private Transform tankReference; // a reference to the tank gameobject
    private Rigidbody rigidBody;// a reference to the rigidbody on our tank
    private Coroutine movingRoutine;
    private AIController characterControl;

    // Start is called before the first frame update
    void Start()
    {
        tankReference = gameObject.transform;
        rigidBody = tankReference.GetComponent<Rigidbody>(); // grab a reference to our tanks rigidbody
        characterControl = GetComponent<AIController>();
        characterControl.SetTarget(currentTargetNode.position);

        if(movingRoutine != null)
        {
            StopCoroutine(Racing());
        }
        movingRoutine = StartCoroutine(Racing());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// draws spheres where the path nodes are
    /// </summary>
    private void OnDrawGizmos()
    {
        // go through all the way points and draw a sphere
        for (int i = 0; i < nodeToMoveTo.Count; i++)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(nodeToMoveTo[i].position, 0.25f);
        }
    }

    private IEnumerator Racing()
    {
        while(this.enabled)
        {
            if(Vector3.Distance(transform.position, currentTargetNode.position) <= 0.5f)
            {
                currentTargetNode = SetNextGoal();
                characterControl.SetTarget(currentTargetNode.position);
            }
            yield return null;
        }
    }

    public void Move(Vector3 move)
    {

        if(move.magnitude > 1f)
        {
            move.Normalize();
        }
        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
    }

    /// <summary>
    /// Calculates distance from the given corner to the player
    /// </summary>
    /// <param name="cornerTransform"></param>
    public void CalculateDistance(Transform cornerTransform)
    {
        distanceTo = Vector3.Distance(tankReference.position, cornerTransform.position);
        if(enableDebug)
        {
            // Debug.Log("Distance to next corner is " + distanceTo);
        }
    }

    private Transform SetNextGoal()
    {
        int currentNode = 0;
        for (int i = 0; i < nodeToMoveTo.Count; i++)
        {
            if (nodeToMoveTo[i] == currentTargetNode)
            {
                currentNode = i;
            }
        }

        currentNode += 1;
        if (currentNode >= nodeToMoveTo.Count)
        {
            currentNode = 0;
        }

        return nodeToMoveTo[currentNode];
    }
}
