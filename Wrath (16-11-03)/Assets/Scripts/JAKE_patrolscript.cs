using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JAKE_patrolscript : MonoBehaviour {

    public bool repeatPath;

    public Transform[] patrol_positions = new Transform[2];    // Public array for choosing patrol points
    List<Transform> position_queue = new List<Transform>();     // Private list for handling movement

    private Vector3 targetPos;                                  // Current target position for agent
    private NavMeshAgent agent;                                 // Reference to navmeshagent


    void Start () {

        agent = gameObject.GetComponent<NavMeshAgent>();        // Reference to navmeshagent

        foreach (Transform tfm in patrol_positions)             // Migrate patrol positions to a list
        {
            position_queue.Add(tfm);
        }
        
        targetPos = patrol_positions[0].position;                 // Set new destination to first position in queue
        

        if (repeatPath)
        {
            position_queue.Add(position_queue[0]);              // If repeatPath is selected, put this position at the back of the queue 
        }

        position_queue.Remove(position_queue[0]);               // Remove this position from the front of the queue


        agent.SetDestination(targetPos);                        // Tell agent to move to destination

	}
	

	void Update () {

        //This vector represents the distance between the object and its destination
        Vector3 difference = gameObject.transform.position - targetPos;

        //When distance between this object and its destination is less than 0.1 and it has another place to get to...
        if (difference.magnitude < 1.1f && position_queue.Count > 0)
        {
            targetPos = position_queue[0].position;             // Set new destination to the first position in queue

            if (repeatPath)
            {
                position_queue.Add(position_queue[0]);          // If repeatPath is selected, put this position at the back of the queue 
            }
            position_queue.Remove(position_queue[0]);           // Remove this position from the front of the queue

        }

        agent.SetDestination(targetPos);                        // Tell agent to move to destination
        agent.Resume();
    }

}
