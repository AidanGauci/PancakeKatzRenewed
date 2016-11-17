using UnityEngine;
using System.Collections;

public class DoorManager_Aidan : MonoBehaviour {

    public ParticleSystem doorParticleEffect;
    public float effectOffset = 1f;
    
    PETER_PlayerMovement playerRef;
    UIManager_Aidan UI;
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
        if (hit.tag == "Player")
        {
            if (allAlliesCollided)
            {
                FindObjectOfType<GameManager_Aidan>().isDoorBroken = true;
                if (doorParticleEffect != null)
                {
                    Destroy(Instantiate(doorParticleEffect, transform.position + (Vector3.up * effectOffset), Quaternion.Euler(new Vector3(0, -140, 180))), 2);
                    Destroy(Instantiate(doorParticleEffect, transform.position, Quaternion.Euler(new Vector3(0, -140, 180))), 2);
                }

                Destroy(gameObject);
            }
        }
    }
}
