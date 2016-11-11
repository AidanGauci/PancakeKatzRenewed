using UnityEngine;
using System.Collections;

public class PETER_PlayerMovement : MonoBehaviour
{

    public Transform AnglePointer;
    NavMeshAgent agent;
    public float speed;


	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
	}
	

	// Update is called once per frame
	void Update ()
    {
        Vector3 Angle = AnglePointer.position - this.transform.position;
        Vector3 newPos = new Vector3(0, 0, 0);

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

        newPos.Normalize();
        newPos *= speed * 0.1f * Time.deltaTime;

        agent.Move(newPos);



        /*
        if (Input.GetKey("g"))
        {
            GameObject[] doorBoulders = GameObject.FindGameObjectsWithTag("door");
            foreach (GameObject removable in doorBoulders)
            {
                removable.GetComponent<MeshRenderer>().enabled = false;
                removable.GetComponent<ParticleSystem>().Play();
            }
            agent.areaMask = 10011;
            Debug.Log("'g' is pressed. Giving player access to all areas.");
        }
        */



    }


    public void TutorialDone()
    {
        print("tutorial finished");
        agent.areaMask = 10001;
    }

}
