using UnityEngine;
using System.Collections;

public class InvisibleWall_Aidan : MonoBehaviour {

    UIManager_Aidan UI;

    void Start()
    {
        UI = FindObjectOfType<UIManager_Aidan>();
    }

	void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            UI.tutorialInvisibleWallTriggered = true;
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.tag == "Player")
        {
            UI.tutorialInvisibleWallTriggered = false;
        }
    }
}
