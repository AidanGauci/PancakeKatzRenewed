using UnityEngine;
using System.Collections;

public class PETER_PlayerAttack : MonoBehaviour
{

    public Transform Weapon;
    public AnimationClip AttackAnim;
    public float beginSwingTime;
    public float endSwingTime;

    public float attackLength;
    public bool hasWeapon;
    public int playerHealth;
    public Animator playerModel;


    // ENUM FOR ATTACK STATE
    public enum attackState
    {
        notAttacking,
        beginSwing,
        midSwing,
        endSwing
    }
    public attackState currState;


    private float timer;


	// Use this for initialization
	void Start ()
    {
        beginSwingTime = Mathf.Clamp(beginSwingTime, 0, attackLength);
        endSwingTime = Mathf.Clamp(endSwingTime, 0, attackLength - beginSwingTime);
        attackLength = AttackAnim.length;
        hasWeapon = false;
        playerHealth = 5;
        playerModel = GetComponent<PETER_PlayerAnimationController>().PlayerModel;
        currState = attackState.notAttacking;
        timer = 0.0f;
    }


    // Update is called once per frame
    void Update()
    {

        Debug.Log(currState);


        // NOT ATTACKING STATE
        if (currState == attackState.notAttacking)
        {
            if (hasWeapon && Input.GetMouseButtonDown(0))
            {
                currState = attackState.beginSwing;
                timer = 0.0f;
                GetComponent<PETER_PlayerAnimationController>().PlayAttack();
            }
        }
        
        // BEGIN SWING STATE
        else if (currState == attackState.beginSwing)
        {
            timer += Time.deltaTime * playerModel.speed * 2;
            if (timer >= beginSwingTime)
            {
                currState = attackState.midSwing;
            }
        }
        
        // MID SWING STATE
        else if (currState == attackState.midSwing)
        {
            timer += Time.deltaTime * playerModel.speed * 2;
            if (timer >= attackLength - endSwingTime)
            {
                currState = attackState.endSwing;
            }
        }
        
        // END SWING STATE
        else if (currState == attackState.endSwing)
        {
            timer += Time.deltaTime * playerModel.speed * 2;
            if (timer >= attackLength)
            {
                currState = attackState.notAttacking;
            }
        }


        if (currState != attackState.notAttacking)
        {
            Vector3 ModelRot = GetComponent<PETER_PlayerCamera>().CamAim.position;
            ModelRot.y = playerModel.transform.position.y;
            playerModel.transform.LookAt(ModelRot);
        }
        
    }

}
