using UnityEngine;
using System.Collections;

public class SwordAttack_Aidan : MonoBehaviour {

    PETER_PlayerAttack playerAtt;

    void Start()
    {
        playerAtt = FindObjectOfType<PETER_PlayerAttack>();
    }

	void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Enemy" && playerAtt.currState == PETER_PlayerAttack.attackState.midSwing)
        {
            Destroy(hit.gameObject);
        }
        else if (hit.tag == "EndDoor" && playerAtt.currState == PETER_PlayerAttack.attackState.midSwing)
        {
            FindObjectOfType<GameManager_Aidan>().HasHitEndBox();
        }
    }

    void OnTriggerStay(Collider hit)
    {
        if (hit.tag == "Enemy" && playerAtt.currState == PETER_PlayerAttack.attackState.midSwing)
        {
            Destroy(hit.gameObject);
        }
        else if (hit.tag == "EndDoor")
        {
            FindObjectOfType<GameManager_Aidan>().HasHitEndBox();
        }
    }
}
