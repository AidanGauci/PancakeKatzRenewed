using UnityEngine;
using System.Collections;

public class PETER_EnemySword : MonoBehaviour
{

    UIManager_Aidan canvas;
    PETER_PlayerAttack player;

	// Use this for initialization
	void Start ()
    {
        canvas = FindObjectOfType<UIManager_Aidan>();
        player = FindObjectOfType<PETER_PlayerAttack>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player" && GetComponentInParent<PETER_EnemyAI>().currState == PETER_EnemyAI.enemyState.attacking)
        {
            if (player.playerHealth > 1)
            {
                canvas.healthIcons[player.playerHealth - 1].enabled = false;
                player.playerHealth -= 1;
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
            }
        }
    }

    //void OnTriggerStay(Collider hit)
    //{
    //    if (hit.tag == "Player" && GetComponentInParent<PETER_EnemyAI>().currState == PETER_EnemyAI.enemyState.attacking)
    //    {
    //        canvas.healthIcons[player.playerHealth - 1].enabled = false;
    //        player.playerHealth -= 1;
    //    }
    //}

}
