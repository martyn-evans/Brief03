using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]

public class NPCMovement : MonoBehaviour
{
    #region public variables
    public float speed = 2000f; // the value of the NPC forward speed
    public float turnSpeed = 1000f; // the value of the NPC turning speed
    public float distanceTo; // distance to next target

    public Vector3 targetPos;

    public List<Transform> nodeToMoveTo = new List<Transform>();
    public Transform currentTargetNode; // a reference to the current target nodes transform
    public NavMeshAgent agent; // a reference to the nav mesh agent component 

    public bool enableDebug = false; // enables/disables debug logs
    #endregion

    #region private variables
    private Transform tankReference; // a reference to the tank gameobject
    private Rigidbody rigidBody;// a reference to the rigidbody on our tank
    private Coroutine movingRoutine;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        tankReference = gameObject.transform; // the reference of the tank objects transform
        rigidBody = tankReference.GetComponent<Rigidbody>(); // the reference to the tanks rigidbody
        SetTarget(currentTargetNode.position);

        if(movingRoutine != null)
        {
            StopCoroutine(Racing());
        }
        movingRoutine = StartCoroutine(Racing());

        if (enableDebug)
        {
            Debug.Log("Coroutine called");
        }

        agent = GetComponentInChildren<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    private void Update()
    {
        if (targetPos != null)
        {
            agent.SetDestination(targetPos);
            agent.speed = speed * Time.deltaTime;
            transform.LookAt(targetPos);
            Vector3 direction = targetPos - transform.position;
            direction.y = transform.position.y; // direction is always going to have the same y value as the tank
            Quaternion lookRotation = Quaternion.LookRotation(direction, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
        }
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
            Gizmos.DrawSphere(nodeToMoveTo[i].position, 0.5f);
        }
    }

    public void SetTarget(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    private IEnumerator Racing()
    {
        while(this.enabled)
        {
            if(Vector3.Distance(transform.position, currentTargetNode.position) <= 10f)
            {
                currentTargetNode = SetNextGoal();
                SetTarget(currentTargetNode.position);
                if (enableDebug)
                {
                    Debug.Log("Setting next node");
                }
            }
            yield return null;
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

                if (enableDebug)
                {
                    Debug.Log("Current node is " + currentNode);
                }
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
