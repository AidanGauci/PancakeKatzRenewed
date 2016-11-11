using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class AllyAI_Aidan : MonoBehaviour {

    [HideInInspector]
    public NavMeshAgent navigator;

    //public variables
    [Header("Variables to Assign")]
    public string[] allySaveTextChoices;
    public Transform doorPos;
    public Transform actualDoorPos;
    public PETER_PlayerMovement playerT;
    public LayerMask playerLayerMask;
    //public ParticleSystem disappearEffect;

    [Header("Test Variables")]
    public float textWaitTime;
    public float checkDistance = 3f;
    public float doorCheckDistanceMin = 2f;
    public float doorCheckDistanceMax = 3f;
    public float endCheckDistance = 3f;
    public float publicWaitTimeForCollider = 1f;

    //private variables
    Transform childT;
    Transform mainCamera;
    UIManager_Aidan UI;
    CapsuleCollider myCollider;
    float endWaitTime;
    float waitTimeForCollider = float.PositiveInfinity;
    bool canPress = true;
    bool talkedToAlly = false;
    bool hasBeenTalkedTo = false;
    bool headingToEnd = false;
    bool atEnd = false;
    public bool doneBefore {get; private set;}

    void Awake()
    {
        myCollider = GetComponent<CapsuleCollider>();
        navigator = GetComponent<NavMeshAgent>();
        UI = FindObjectOfType<UIManager_Aidan>();
    }

    void Update()
    {
        Vector3 directionToPlayer = (transform.position - playerT.transform.position) * -1f;
        if (canPress && !hasBeenTalkedTo && Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, directionToPlayer, checkDistance, playerLayerMask, QueryTriggerInteraction.Collide))
            {
                 OnTalkTo();
                 canPress = false;
            }
        }

        if (talkedToAlly && endWaitTime <= Time.time)
        {
            OnFinishTalking();
        }
        else if (talkedToAlly && endWaitTime > Time.time)
        {
            transform.LookAt(playerT.transform);
        }

        if (hasBeenTalkedTo)
        {
            if (CircleCircleCheck(transform.position, 1, navigator.destination, doorCheckDistanceMax))
            {
                waitTimeForCollider = Time.time + publicWaitTimeForCollider;
                navigator.Stop();
                hasBeenTalkedTo = false;
                canPress = false;
            }
        }
        
        if (!hasBeenTalkedTo && !canPress && waitTimeForCollider <= Time.time && !headingToEnd && !atEnd)
        {
            myCollider.enabled = false;
            navigator.enabled = false;
            GetComponent<Rigidbody>().freezeRotation = true;
            transform.LookAt(actualDoorPos);
        }

        if (headingToEnd)
        {
            myCollider.enabled = true;
            navigator.enabled = true;
            GetComponent<Rigidbody>().freezeRotation = false;
            navigator.Resume();
            if (CircleCircleCheck(transform.position, 1, navigator.destination, endCheckDistance))
            {
                GetComponent<Rigidbody>().freezeRotation = true;
                navigator.Stop();
                headingToEnd = false;
            }
        }
    }

    public void OnTalkTo()
    {
        UI.OnAllyCountChange();
        endWaitTime = Time.time + textWaitTime;
        talkedToAlly = true;
        UI.ActivateText(allySaveTextChoices[Random.Range(0, allySaveTextChoices.Length)], textWaitTime);
        transform.LookAt(playerT.transform);
    }

    void OnFinishTalking()
    {
        navigator.SetDestination(doorPos.position);
        canPress = true;
        talkedToAlly = false;
        hasBeenTalkedTo = true;
    }

    public void SetEndDestination(Vector3 position)
    {
        print("function called");
        navigator.enabled = true;
        myCollider.enabled = true;
        GetComponent<Rigidbody>().freezeRotation = false;
        navigator.SetDestination(position);
        navigator.Resume();
        headingToEnd = true;
        atEnd = true;
    }

    bool CircleCircleCheck(Vector3 P1, float R1, Vector3 P2, float R2)
    {
        return ((P1 - P2).sqrMagnitude < ((R1*R1)+(R1*R2)));
    }
}
