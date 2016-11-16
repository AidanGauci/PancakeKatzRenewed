using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class AllyAI_Aidan : MonoBehaviour {

    [HideInInspector]
    public NavMeshAgent navigator;
    [HideInInspector]
    public bool doneBefore {get; private set;}
    [HideInInspector]
    public bool swordObtained = false;
    [HideInInspector]
    public int textChoicesSeed = 1;

    //public variables
    [Header("Variables to Assign")]
    public string[] allySaveTextChoices;
    public Transform doorPos;
    public Transform lookDoorPos;
    public PETER_PlayerMovement playerT;
    public LayerMask playerLayerMask;
    public AudioClip[] talkingSounds;
    public AudioClip walkingSound;
    //public ParticleSystem disappearEffect;

    [Header("Test Variables")]
    public float textWaitTime = 3f;
    public float playerCheckDistance = 2.5f;
    public float doorCheckDistance = 0.3f;
    public float endCheckDistance = 0f;
    public float waitTimeForCollider = 1f;

    public static int currentStringIndex;

    //private variables
    Transform childT;
    Transform mainCamera;
    UIManager_Aidan UI;
    CapsuleCollider myCollider;
    float endWaitTime = float.PositiveInfinity;
    float privateTimeForCollider = float.PositiveInfinity;
    float effectWaitTime = float.PositiveInfinity;
    float currentFootstepTimer = 0;
    bool canPress = true;
    bool talkedToAlly = false;
    bool hasBeenTalkedTo = false;
    bool headingToEnd = false;
    bool atEnd = false;

    void Awake()
    {
        currentStringIndex = 0;
        myCollider = GetComponent<CapsuleCollider>();
        navigator = GetComponent<NavMeshAgent>();
        UI = FindObjectOfType<UIManager_Aidan>();
    }

    void Update()
    {
        Vector3 directionToPlayer = (transform.position - playerT.transform.position) * -1f;
        if (canPress && !hasBeenTalkedTo && swordObtained && Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, directionToPlayer, playerCheckDistance, playerLayerMask, QueryTriggerInteraction.Collide))
            {
                canPress = false;
                OnTalkTo();
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
            if (currentFootstepTimer == 0)
            {
                AudioManager_Aidan.instance.PlaySound(walkingSound, transform.position);
                currentFootstepTimer = Time.time + walkingSound.length;
            }
            else if (currentFootstepTimer <= Time.time)
            {
                AudioManager_Aidan.instance.PlaySound(walkingSound, transform.position);
                currentFootstepTimer = Time.time + walkingSound.length;
            }
            if (CircleCircleCheck(transform.position, 1, navigator.destination, doorCheckDistance))
            {
                privateTimeForCollider = Time.time + waitTimeForCollider;
                navigator.Stop();
                hasBeenTalkedTo = false;
                canPress = false;
            }
        }
        
        if (!hasBeenTalkedTo && !canPress && privateTimeForCollider <= Time.time && !headingToEnd && !atEnd)
        {
            myCollider.enabled = false;
            navigator.enabled = false;
            GetComponent<Rigidbody>().freezeRotation = true;
            transform.LookAt(lookDoorPos);
        }

        if (headingToEnd && effectWaitTime <= Time.time)
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
        AudioManager_Aidan.instance.PlaySound(talkingSounds[Random.Range(0, talkingSounds.Length)], transform.position);
        UI.OnAllyCountChange();
        endWaitTime = Time.time + textWaitTime;
        talkedToAlly = true;
        UI.ActivateText(allySaveTextChoices[currentStringIndex], textWaitTime);
        currentStringIndex++;
        if (currentStringIndex == allySaveTextChoices.Length)
        {
            currentStringIndex = 0;
            textChoicesSeed += 5;
            allySaveTextChoices = ShuffleStrings(allySaveTextChoices, textChoicesSeed);
        }
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
        effectWaitTime = Time.time + 1f;
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

    string[] ShuffleStrings(string[] array, int seed)
    {
        System.Random prng = new System.Random(seed);

        for (int i = 0; i < array.Length - 1; i++)
        {
            int randomIndex = prng.Next(i, array.Length);
            string tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }

        return array;
    }
}
