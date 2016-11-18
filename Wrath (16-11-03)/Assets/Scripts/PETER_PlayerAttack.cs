using UnityEngine;
using System.Collections;

public class PETER_PlayerAttack : MonoBehaviour
{

    public Transform Weapon;
    public Transform WeaponPointerIdle;
    public Transform WeaponPointerSwing;
    public bool hasWeapon;
    public float swingSpeed;
    public float attackTime;

    public int playerHealth;


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
        hasWeapon = false;
        playerHealth = 5;
        currState = attackState.notAttacking;
        timer = 0.0f;

        Weapon.position = WeaponPointerIdle.position;
        ///Weapon.rotation = WeaponPointer.rotation;
    }


    // Update is called once per frame
    void Update()
    {

        Debug.Log(currState);


        // NOT ATTACKING STATE
        if (currState == attackState.notAttacking)
        {
            Weapon.position = WeaponPointerIdle.position;
            if (hasWeapon && Input.GetMouseButtonDown(0))
            {
                currState = attackState.beginSwing;
                timer = 0.0f;
            }
        }
        
        // BEGIN SWING STATE
        else if (currState == attackState.beginSwing)
        {
            timer += Time.deltaTime * swingSpeed;
            Weapon.position = Vector3.Lerp(WeaponPointerIdle.position, WeaponPointerSwing.position, timer);
            ///Weapon.rotation = Quaternion.Lerp(WeaponPointer.rotation, AnglePointer.rotation, timer);
            if (timer >= 1)
            {
                Weapon.position = WeaponPointerSwing.position;
                ///Weapon.rotation = AnglePointer.rotation;
                currState = attackState.midSwing;
                timer = 0.0f;
            }
        }
        
        // MID SWING STATE
        else if (currState == attackState.midSwing)
        {
            timer += Time.deltaTime * attackTime;
            Weapon.position = WeaponPointerSwing.position;
            if (timer >= 1)
            {
                currState = attackState.endSwing;
                timer = 0.0f;
            }
        }
        
        // END SWING STATE
        else if (currState == attackState.endSwing)
        {
            timer += Time.deltaTime * swingSpeed;
            Weapon.position = Vector3.Lerp(WeaponPointerSwing.position, WeaponPointerIdle.position, timer);
            ///Weapon.rotation = Quaternion.Lerp(AnglePointer.rotation, WeaponPointer.rotation, timer);
            if (timer >= 1)
            {
                Weapon.position = WeaponPointerIdle.position;
                ///Weapon.rotation = WeaponPointer.rotation;
                currState = attackState.notAttacking;
                timer = 0.0f;
            }
        }


    }

}
