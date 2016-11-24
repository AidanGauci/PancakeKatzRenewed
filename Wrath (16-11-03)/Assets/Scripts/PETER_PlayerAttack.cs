using UnityEngine;
using System.Collections;

public class PETER_PlayerAttack : MonoBehaviour
{

    public bool godMode;

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
        attackLength = AttackAnim.length;
        playerModel = GetComponent<PETER_PlayerAnimationController>().PlayerModel;
        beginSwingTime = Mathf.Clamp(beginSwingTime, 0, (attackLength * playerModel.speed) * 2);
        endSwingTime = Mathf.Clamp(endSwingTime, 0, (attackLength * playerModel.speed * 2) - beginSwingTime);
        hasWeapon = false;
        currState = attackState.notAttacking;
        timer = 0.0f;
    }


    // Update is called once per frame
    void Update()
    {

        //Debug.Log(currState);


        // NOT ATTACKING STATE
        if (currState == attackState.notAttacking)
        {
            if (hasWeapon && Input.GetMouseButtonDown(0))
            {
                currState = attackState.beginSwing;
                timer = 0.0f;
                GetComponent<PETER_PlayerAnimationController>().PlayAttack();

                Debug.Log(currState);
            }
        }
        
        // BEGIN SWING STATE
        else if (currState == attackState.beginSwing)
        {
            timer += Time.deltaTime * playerModel.speed * 2;
            if (timer >= beginSwingTime)
            {
                currState = attackState.midSwing;

                Debug.Log(currState);
            }
        }
        
        // MID SWING STATE
        else if (currState == attackState.midSwing)
        {
            timer += Time.deltaTime * playerModel.speed * 2;
            if (timer >= attackLength - endSwingTime)
            {
                currState = attackState.endSwing;

                Debug.Log(currState);
            }
        }
        
        // END SWING STATE
        else if (currState == attackState.endSwing)
        {
            timer += Time.deltaTime * playerModel.speed * 2;
            if (timer >= attackLength)
            {
                currState = attackState.notAttacking;

                Debug.Log(currState);
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
