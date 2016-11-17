using UnityEngine;
using System.Collections;

public class TutorialWallTriggers_Aidan : MonoBehaviour
{
    [HideInInspector]
    public bool tutorialFinished = false;
    [HideInInspector]
    public bool guardKilled = false;
    public AllyAI_Aidan firstAllyRef;
    public Transform newPos;
    public string toSay;
    public string toSayAfter;

    UIManager_Aidan UI;

    void Start()
    {
        UI = FindObjectOfType<UIManager_Aidan>();
    }

    void Update()
    {
        print(guardKilled);
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player" && !tutorialFinished)
        {
            UI.tutorialWallTriggered = true;
            UI.swordText.text = toSay;
            UI.swordText.gameObject.SetActive(true);
            UI.swordTextBackground.gameObject.SetActive(true);
        }
        else if (hit.tag == "Player" && guardKilled && !firstAllyRef.hasTalkedToPlayer)
        {
            UI.tutorialWallTriggered = true;
            UI.swordText.text = toSayAfter;
            UI.swordText.gameObject.SetActive(true);
            UI.swordTextBackground.gameObject.SetActive(true);
        }
        if (firstAllyRef.hasTalkedToPlayer)
        {
            FindObjectOfType<PETER_PlayerMovement>().GetComponent<NavMeshAgent>().areaMask = 10001;
            transform.position = newPos.position;
        }
    }

    void OnTriggerStay(Collider hit)
    {
        if (firstAllyRef.hasTalkedToPlayer)
        {
            FindObjectOfType<PETER_PlayerMovement>().GetComponent<NavMeshAgent>().areaMask = 10001;
            transform.position = newPos.position;
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.tag == "Player")
        {
            UI.tutorialWallTriggered = false;
            UI.swordText.text = "";
            UI.swordText.gameObject.SetActive(false);
            UI.swordTextBackground.gameObject.SetActive(false);
        }
    }
}
