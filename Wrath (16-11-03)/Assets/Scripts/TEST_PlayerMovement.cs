using UnityEngine;
using System.Collections;

public class TEST_PlayerMovement : MonoBehaviour
{

    public Transform AnglePointer;
    NavMeshAgent agent;
    public float speed;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
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
        newPos *= speed * Time.deltaTime * 0.01f;

        agent.Move(newPos);

    }
}
