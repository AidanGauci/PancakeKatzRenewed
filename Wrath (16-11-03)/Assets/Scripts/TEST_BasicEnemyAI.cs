using UnityEngine;
using System.Collections;

public class TEST_BasicEnemyAI : MonoBehaviour {

    public float reactDistance = 2f;

    Vector3 originalPosition;
    PETER_PlayerMovement playerRef;
    NavMeshAgent navigator;

    void Start()
    {
        originalPosition = transform.position;
        navigator = GetComponent<NavMeshAgent>();
        playerRef = FindObjectOfType<PETER_PlayerMovement>();
    }

    void Update()
    {
        if (CircleCircleCheck(playerRef.transform.position, 1, transform.position, reactDistance))
        {
            navigator.SetDestination(playerRef.transform.position);   
        }
        else if (!CircleCircleCheck(playerRef.transform.position, 1, transform.position, reactDistance))
        {
            navigator.SetDestination(originalPosition);
        }
    }

    bool CircleCircleCheck(Vector3 P1, float R1, Vector3 P2, float R2)
    {
        return ((P1 - P2).sqrMagnitude < ((R1 * R1) + (R1 * R2)));
    }
}
