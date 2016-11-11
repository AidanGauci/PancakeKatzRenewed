using UnityEngine;
using System.Collections;

public class DoorManager_Aidan : MonoBehaviour {

    public ParticleSystem doorParticleEffect;
    
    PETER_PlayerMovement playerRef;
    UIManager_Aidan UI;
    int boolCounter = 0;
    int allyCounter = 0;
    bool allAlliesCollided = false;

    void Start()
    {
        UI = FindObjectOfType<UIManager_Aidan>();
        playerRef = FindObjectOfType<PETER_PlayerMovement>();
    }

    void Update()
    {
        allyCounter = UI.allyCurrentCount;
        if (allyCounter == 20)
        {
            allAlliesCollided = true;
        }
    }

    void OnTriggerEnter(Collider hit)
    {
        print("has collided");
        if (hit.tag == "Player")
        {
            if (allAlliesCollided)
            {
                FindObjectOfType<GameManager_Aidan>().isDoorBroken = true;
                FindObjectOfType<PETER_PlayerMovement>().GetComponent<NavMeshAgent>().areaMask = 1001;
                if (doorParticleEffect != null)
                {
                    Destroy(Instantiate(doorParticleEffect, transform.position, transform.rotation), 2);
                }
                Destroy(gameObject);
            }
        }
    }
}
