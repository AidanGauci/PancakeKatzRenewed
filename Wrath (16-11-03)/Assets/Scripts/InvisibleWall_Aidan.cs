using UnityEngine;
using System.Collections;

public class InvisibleWall_Aidan : MonoBehaviour {

    public GameObject invisibleWall;

    PickupSword_Aidan sword;
    UIManager_Aidan UI;
    PETER_PlayerMovement playerRef;

    void Start()
    {
        playerRef = FindObjectOfType<PETER_PlayerMovement>();
        UI = FindObjectOfType<UIManager_Aidan>();
        sword = FindObjectOfType<PickupSword_Aidan>();
    }

	void OnTriggerEnter(Collider hit)
    {
        Debug.Log("POOP");
        if (hit.tag == "Player")
        {
            UI.wallTriggered = true;
        }
    }

    void OnTriggerExit(Collider hit)
    {
        print("hitting tutorial trigger");
        if (hit.tag == "Player")
        {
            UI.wallTriggered = false;
        }
    }
}
