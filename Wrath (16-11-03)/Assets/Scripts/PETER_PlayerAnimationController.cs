using UnityEngine;
using System.Collections;

public class PETER_PlayerAnimationController : MonoBehaviour
{
    
    public Animator PlayerModel;

    // Use this for initialization
    void Start ()
    {
        PlayIdle();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayIdle()
    {
        PlayerModel.SetTrigger("Idle");
        PlayerModel.ResetTrigger("Attack");
        PlayerModel.ResetTrigger("Walk");
    }

    public void PlayAttack()
    {
        PlayerModel.ResetTrigger("Idle");
        PlayerModel.SetTrigger("Attack");
        PlayerModel.ResetTrigger("Walk");
    }

    public void PlayWalk()
    {
        PlayerModel.ResetTrigger("Idle");
        PlayerModel.ResetTrigger("Attack");
        PlayerModel.SetTrigger("Walk");
    }

}
