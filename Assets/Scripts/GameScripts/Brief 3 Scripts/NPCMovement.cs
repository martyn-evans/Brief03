using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float speed = 12f; // the speed our tank moves
    public float turnSpeed = 180f; // the speed that we can turn in degrees in seconds.
    public float distanceTo; // 

    public List<Transform> cornerList = new List<Transform>();
    public int currentIndex;
    public Transform currentGoal;

    public bool enableDebug = false; // enables/disables debug logs

    private Transform tankReference; // a reference to the tank gameobject
    private Rigidbody rigidBody;// a reference to the rigidbody on our tank

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        currentGoal = cornerList[currentIndex];
        tankReference = gameObject.transform;
        if (tankReference.GetComponent<Rigidbody>())
        {
            rigidBody = tankReference.GetComponent<Rigidbody>(); // grab a reference to our tanks rigidbody
        }
        else
        {
            Debug.LogError("No Rigidbody attached to the tank");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 movementVector = tankReference.forward * speed * Time.deltaTime;
        Vector3 tankMovement = new Vector3(currentGoal.position.x, transform.position.y, currentGoal.position.z);
        rigidBody.MovePosition(rigidBody.position + movementVector); // move our rigibody based on our current position + our movement vector
        tankReference.transform.Rotate(currentGoal.position.x, 0, currentGoal.position.z, Space.Self);

        transform.position = Vector3.MoveTowards(transform.position, tankMovement, speed * Time.deltaTime);
        CalculateDistance(currentGoal);
        if(distanceTo < 2f)
        {
            SetNextGoal();
            if (enableDebug)
            {
                Debug.Log("Passed first corner");
            }
        }
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

    public void SetNextGoal()
    {
        currentIndex ++;
        if(currentIndex >= cornerList.Count) // win condition?
        {
            currentIndex = 0;
        }
        currentGoal = cornerList[currentIndex];
    }
}
