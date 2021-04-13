using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class AIController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Vector3 targetPos;

    private void Start()
    {
        agent = GetComponentInChildren<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    private void Update()
    {
        if(targetPos != null)
        {
            agent.SetDestination(targetPos);
        }

        if(agent.remainingDistance > agent.stoppingDistance)
        {
            // want the chacacter to move
        }
        else
        {
            // want the character to stop
        }
    }

    public void SetTarget(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }
}
