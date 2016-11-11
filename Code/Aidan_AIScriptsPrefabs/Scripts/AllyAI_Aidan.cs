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
    public PETER_PlayerMovement playerT;
    //public ParticleSystem disappearEffect;

    [Header("Test Variables")]
    public float textWaitTime;
    public float checkDistance = 3f;
    public float doorCheckDistanceMin = 2f;
    public float doorCheckDistanceMax = 3f;
    public float endCheckDistance = 3f;

    //private variables
    Transform childT;
    Transform mainCamera;
    TextMesh saveTextMesh;
    UIManager_Aidan UI;
    float endWaitTime;
    bool canPress = true;
    bool talkedToAlly = false;
    bool hasBeenTalkedTo = false;
    bool headingToEnd = false;

    void Start()
    {
        childT = transform.FindChild("GameObject (1)");
        saveTextMesh = childT.GetComponent<TextMesh>();
        mainCamera = Camera.main.transform;
        navigator = GetComponent<NavMeshAgent>();
        UI = FindObjectOfType<UIManager_Aidan>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPress && !hasBeenTalkedTo && (Vector3.Distance(transform.position, playerT.transform.position) <= checkDistance))
        {
            OnTalkTo();
            canPress = false;
        }

        if (talkedToAlly && endWaitTime <= Time.time)
        {
            OnFinishTalking();
        }
        else if (talkedToAlly && endWaitTime > Time.time)
        {
            childT.LookAt(mainCamera);
            childT.Rotate(Vector3.up, 180);
            transform.LookAt(playerT.transform);
        }

        if (hasBeenTalkedTo)
        {
            if (Vector3.Distance(navigator.destination, transform.position) <= Random.Range(doorCheckDistanceMin, doorCheckDistanceMax))
            {
                navigator.Stop();
                hasBeenTalkedTo = false;
                canPress = false;
            }
        }

        if (headingToEnd)
        {
            if (Vector3.Distance(navigator.destination, transform.position) <= endCheckDistance)
            {
                navigator.Stop();
                headingToEnd = false;
            }
        }
        else if (!headingToEnd)
        {
            if (Vector3.Distance(navigator.destination, transform.position) > endCheckDistance)
            {
                navigator.Resume();
                headingToEnd = true;
            }
        }
    }

    public void OnTalkTo()
    {
        UI.OnAllyCountChange();
        saveTextMesh.text = allySaveTextChoices[Random.Range(0, allySaveTextChoices.Length)];
        endWaitTime = Time.time + textWaitTime;
        talkedToAlly = true;
        childT.LookAt(mainCamera);
        childT.Rotate(Vector3.up, 180);
        transform.LookAt(playerT.transform);
    }

    void OnFinishTalking()
    {
        saveTextMesh.text = "";
        navigator.SetDestination(doorPos.position);
        canPress = true;
        talkedToAlly = false;
        hasBeenTalkedTo = true;
    }

    public void SetEndDestination(Vector3 position)
    {
        navigator.SetDestination(position);
        navigator.Resume();
        headingToEnd = true;
    }
}
