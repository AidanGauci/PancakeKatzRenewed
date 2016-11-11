using UnityEngine;
using System.Collections;

public class Jailer_Aidan : MonoBehaviour {

    [HideInInspector]
    public bool swordTaken = false;
    public string onFirstSight;
    public string afterSword;
    public float checkDistance = 1f;
    public float textWaitTime = 1f;

    TextMesh myText;
    PETER_PlayerMovement playerRef;
    Transform mainCamera;
    float turnOffText;
    bool isAngryTextOn = false;

    void Start()
    {
        playerRef = FindObjectOfType<PETER_PlayerMovement>();
        myText = GetComponent<TextMesh>();
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        if (!swordTaken)
        {
            if (Vector3.Distance(transform.parent.transform.position, playerRef.transform.position) <= checkDistance)
            {
                myText.text = onFirstSight;
                transform.LookAt(mainCamera);
                transform.Rotate(Vector3.up, 180);
            }
            else if (Vector3.Distance(transform.parent.transform.position, playerRef.transform.position) > checkDistance)
            {
                myText.text = "";
            }
        }
        else if (swordTaken && !isAngryTextOn)
        {
            myText.text = afterSword;
            transform.LookAt(mainCamera);
            transform.Rotate(Vector3.up, 180);
            isAngryTextOn = true;
            turnOffText = Time.time + textWaitTime;
        }

        else if (isAngryTextOn && turnOffText > Time.time)
        {
            transform.LookAt(mainCamera);
            transform.Rotate(Vector3.up, 180);
        }

        else if (turnOffText < Time.time)
        {
            myText.text = "";
        }
    }
}
