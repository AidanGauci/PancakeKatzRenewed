﻿using UnityEngine;
using System.Collections;

public class PETER_EnemyAI : MonoBehaviour
{

    PETER_PlayerCamera player;
    public Transform EnemyWeapon;
    public float SightDegrees;
    public float SightDistance;
    public float AttackDistance;
    public float AttackTime;
    public bool TurnToAttack;

    float timer;
    Vector3 spawnPos;

    public enum enemyState
    {
        idle,
        canSeePlayer,
        nextToPlayer,
        attacking
    }

    public enemyState currState;



    // Use this for initialization
    void Start ()
    {
	    player = FindObjectOfType<PETER_PlayerCamera>();
        EnemyWeapon.gameObject.SetActive(false);
        SightDegrees = Mathf.Clamp(SightDegrees, 0, 360);
        SightDistance = Mathf.Clamp(SightDistance, GetComponent<NavMeshAgent>().radius, 10000);
        AttackDistance = Mathf.Clamp(AttackDistance, GetComponent<NavMeshAgent>().radius, SightDistance);
        timer = 0;
        spawnPos = transform.position;
        currState = enemyState.idle;
    }
	

	// Update is called once per frame
	void Update ()
    {
        
        // Actions if currState is attacking
        if (currState == enemyState.attacking)
        {
            Vector3 scale = EnemyWeapon.transform.localScale;
            
            timer += Time.deltaTime;
            if (timer >= AttackTime)
            {
                scale.z = 7;
                EnemyWeapon.transform.localScale = scale;
                EnemyWeapon.gameObject.SetActive(false);
                currState = enemyState.idle;
                timer = 0;
            }

            if (TurnToAttack)
            {
                Vector3 AttackRot = player.transform.position;
                AttackRot.y = transform.position.y;
                transform.LookAt(AttackRot);
            }

            scale.z = Mathf.Lerp(scale.z, 20, 0.1f);
            EnemyWeapon.transform.localScale = scale;
        }


        // If the enemy isn't attacking, Determine if enemy is next to player or can see player and set currState accordingly
        if (currState != enemyState.attacking)
        {
            if (CircleCircleCheck(transform.position, AttackDistance, player.transform.position, player.GetComponent<NavMeshAgent>().radius))
            {
                currState = enemyState.nextToPlayer;
            }
            else if (CircleCircleCheck(transform.position, SightDistance, player.transform.position, player.GetComponent<NavMeshAgent>().radius))
            {
                Vector3 enemyPlayerDir = player.transform.position - transform.position;
                if (Vector3.Dot(enemyPlayerDir.normalized, transform.forward) >= SightDegrees / -180 + 1)
                {
                    currState = enemyState.canSeePlayer;
                }
                else
                {
                    currState = enemyState.idle;
                }
            }
            else
            {
                currState = enemyState.idle;
            }
        }


        // Actions if currState is idle
        if (currState == enemyState.idle)
        {
            GetComponent<NavMeshAgent>().SetDestination(spawnPos);
            GetComponent<NavMeshAgent>().Resume();
        }

        // Actions if currState is canSeePlayer
        else if (currState == enemyState.canSeePlayer)
        {
            GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
            GetComponent<NavMeshAgent>().Resume();
        }

        // Actions if currState is nextToPlayer
        else if (currState == enemyState.nextToPlayer)
        {
            GetComponent<NavMeshAgent>().Stop();
            currState = enemyState.attacking;
            EnemyWeapon.gameObject.SetActive(true);

            Attack();
        }

    }


    // Attack 
    void Attack ()
    {

    }
    

    // Kill is called when the player hits the enemy with an attack
    public void Kill ()
    {
        Destroy(this.gameObject);
    }


    // CircleCircleCheck is used to check if the perimiter of two circles collide
    bool CircleCircleCheck(Vector3 P1, float R1, Vector3 P2, float R2)
    {
        return ((P1 - P2).sqrMagnitude < ((R1 * R1) + (R1 * R2)));
    }

}