using UnityEngine;
using System.Collections;

public class PETER_EnemyAI : MonoBehaviour
{

    public PETER_PlayerAttack player;
    public float SightDegrees;
    public float SightDistance;
    public float AttackDistance;
    public float AttackEndDelay;
    public bool TurnToAttack;

    float AttackTime;
    Animator selfAnimator;
    float timer;
    Vector3 spawnPos;

    public enum enemyState
    {
        asleep,
        idle,
        canSeePlayer,
        nextToPlayer,
        attacking
    }

    public enemyState currState;



    // Use this for initialization
    void Start ()
    {
        SightDegrees = Mathf.Clamp(SightDegrees, 0, 360);
        SightDistance = Mathf.Clamp(SightDistance, GetComponent<NavMeshAgent>().radius, 10000);
        AttackDistance = Mathf.Clamp(AttackDistance, GetComponent<NavMeshAgent>().radius, SightDistance);
        selfAnimator = GetComponentInChildren<Animator>();
        timer = 0;
        spawnPos = transform.position;
        currState = enemyState.asleep;
        PlayIdle();
        AttackTime = player.attackLength;
    }
	

	// Update is called once per frame
	void Update ()
    {

        // Actions if currState is asleep
        if (currState == enemyState.asleep)
        {
            if (player.hasWeapon == true)
            {
                currState = enemyState.idle;
            }
        }
        
        // Actions if currState is attacking
        else if (currState == enemyState.attacking)
        {
            timer += Time.deltaTime * player.playerModel.speed * 2;
            if (timer >= AttackTime + AttackEndDelay)
            {
                currState = enemyState.idle;
                timer = 0;
            }

            if (TurnToAttack)
            {
                Vector3 AttackRot = player.transform.position;
                AttackRot.y = transform.position.y;
                transform.LookAt(AttackRot);
            }
        }


        // If the enemy isn't attacking or asleep, Determine if enemy is next to player or can see player and set currState accordingly
        else
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
            PlayIdle();
        }

        // Actions if currState is canSeePlayer
        else if (currState == enemyState.canSeePlayer)
        {
            GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
            GetComponent<NavMeshAgent>().Resume();
            PlayWalk();
        }

        // Actions if currState is nextToPlayer
        else if (currState == enemyState.nextToPlayer)
        {
            GetComponent<NavMeshAgent>().Stop();
            currState = enemyState.attacking;
            PlayAttack();

            Vector3 AttackRot = player.transform.position;
            AttackRot.y = transform.position.y;
            transform.LookAt(AttackRot);
        }

    }

    void PlayIdle()
    {
        selfAnimator.SetTrigger("Idle");
        selfAnimator.ResetTrigger("Attack");
        selfAnimator.ResetTrigger("Walk");
    }
    
    void PlayAttack()
    {
        selfAnimator.ResetTrigger("Idle");
        selfAnimator.SetTrigger("Attack");
        selfAnimator.ResetTrigger("Walk");
    }
    
    void PlayWalk()
    {
        selfAnimator.ResetTrigger("Idle");
        selfAnimator.ResetTrigger("Attack");
        selfAnimator.SetTrigger("Walk");
    }

    // Kill is called when the player hits the enemy with an attack
    public void Kill()
    {
        Destroy(this.gameObject);
    }


    // CircleCircleCheck is used to check if the perimiter of two circles collide
    bool CircleCircleCheck(Vector3 P1, float R1, Vector3 P2, float R2)
    {
        return ((P1 - P2).sqrMagnitude < ((R1 * R1) + (R1 * R2)));
    }

}
