using UnityEngine;
using System.Collections;

public class PETER_PlayerAnimationController : MonoBehaviour
{
    
    public Transform PlayerModel;

    // Use this for initialization
    void Start ()
    {
	
	}

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<PETER_PlayerAttack>().currState == PETER_PlayerAttack.attackState.notAttacking)
        {

        }

        // If player is attacking, rotate Player Model to face same direction as camera
        else
        {
            Vector3 ModelRot = GetComponent<PETER_PlayerCamera>().CamAim.position;
            ModelRot.y = PlayerModel.position.y;
            PlayerModel.LookAt(ModelRot);
        }

    }

}
