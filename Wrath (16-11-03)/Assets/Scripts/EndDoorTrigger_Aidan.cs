using UnityEngine;
using System.Collections;

public class EndDoorTrigger_Aidan : MonoBehaviour {

	void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            GameManager_Aidan.instance.HasHitEndBox();
        }
    }

}
