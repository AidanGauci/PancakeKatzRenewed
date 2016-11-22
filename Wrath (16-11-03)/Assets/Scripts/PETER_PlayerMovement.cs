using UnityEngine;
using System.Collections;

public class PETER_PlayerMovement : MonoBehaviour
{

    public Transform AnglePointer;

    NavMeshAgent agent;
    public float speed;

    public Vector3 newPos;

    Animator playerModel;


	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        playerModel = GetComponent<PETER_PlayerAnimationController>().PlayerModel;
    }
	

	// Update is called once per frame
	void Update ()
    {

        if (GetComponent<PETER_PlayerAttack>().currState == PETER_PlayerAttack.attackState.notAttacking)
        {

            Vector3 Angle = AnglePointer.position - this.transform.position;
            newPos = new Vector3(0, 0, 0);

            if (Input.GetKey("w"))
            {
                newPos.x += Angle.x;
                newPos.z += Angle.z;
            }

            if (Input.GetKey("a"))
            {
                newPos.x -= Angle.z;
                newPos.z += Angle.x;
            }

            if (Input.GetKey("s"))
            {
                newPos.x -= Angle.x;
                newPos.z -= Angle.z;
            }

            if (Input.GetKey("d"))
            {
                newPos.x += Angle.z;
                newPos.z -= Angle.x;
            }

            if (newPos != new Vector3(0, 0, 0))
            {
                newPos.Normalize();
                newPos *= speed * 0.1f * Time.deltaTime;

                agent.Move(newPos);

                newPos *= 100000;
                newPos.y = playerModel.transform.position.y;
                playerModel.transform.LookAt(playerModel.transform.position + newPos);
                GetComponent<PETER_PlayerAnimationController>().PlayWalk();
            }
            else
            {
                GetComponent<PETER_PlayerAnimationController>().PlayIdle();
            }
            
        }

    }


    public void TutorialDone()
    {
        print("tutorial finished");
        agent.areaMask = 10001;
    }

}
