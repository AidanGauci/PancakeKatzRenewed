using UnityEngine;
using System.Collections;

public class SwordAttack_Aidan : MonoBehaviour
{

    PETER_PlayerAttack player;

    void Start()
    {
        player = FindObjectOfType<PETER_PlayerAttack>();
    }

	void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Enemy" && player.currState == PETER_PlayerAttack.attackState.midSwing)
        {
            hit.GetComponent<PETER_EnemyAI>().Kill();
        }
    }

    void OnTriggerStay(Collider hit)
    {
        if (hit.tag == "Enemy" && player.currState == PETER_PlayerAttack.attackState.midSwing)
        {
            hit.GetComponent<PETER_EnemyAI>().Kill();
        }
    }

}
