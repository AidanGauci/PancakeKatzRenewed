using UnityEngine;
using System.Collections;

public class DoorManager_Aidan : MonoBehaviour {

    public ParticleSystem wallParticleEffect;
    public ParticleSystem doorParticleEffect;
    public ParticleSystem massParticleEffect;
    public Transform massParticleSpawn;
    public Transform particleSpawn1;
    public Transform particleSpawn2;
    public Transform doorParticleSpawn;
    
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
                    Destroy(Instantiate(wallParticleEffect, particleSpawn1.position, particleSpawn1.rotation), 2);
                    Destroy(Instantiate(wallParticleEffect, particleSpawn2.position, particleSpawn2.rotation), 2);
                    Destroy(Instantiate(massParticleEffect, massParticleSpawn.position, massParticleSpawn.rotation), 2);
                    Destroy(Instantiate(doorParticleEffect, doorParticleSpawn.position, doorParticleSpawn.rotation), 2);
                }

                Destroy(gameObject);
            }
        }
    }
}
