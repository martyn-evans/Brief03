using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class AIController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float turnSpeed = 1000f;
    public Vector3 targetPos;

    private void Start()
    {
        agent = GetComponentInChildren<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    private void Update()
    {
        Debug.Log("AI controller update is called");
        if(targetPos != null)
        {
            agent.SetDestination(targetPos);
            agent.speed = 2000 * Time.deltaTime;
            transform.LookAt(targetPos);
            Vector3 direction = targetPos - transform.position;
            direction.y = transform.position.y; // direction is always going to have the same y value as the tank
            Quaternion lookRotation = Quaternion.LookRotation(direction, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
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
