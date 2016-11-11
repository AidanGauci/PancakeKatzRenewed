using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    public enum State { Idle, Run, Attack};
    public State currentState;
    public float speed;

	void Start ()
    {
        currentState = State.Idle;
        anim.GetComponent<Animator>();
	}

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("AttackAnimate", -1);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentState = State.Run;

            StartCoroutine("RunAnimate");
            anim.Play("RunAnimate", -1);
            currentState = State.Idle;
        }
    }

    IEnumerator RunAnimate()
    {
        float percent = 0;

        while (percent < 0)
        {
            percent += Time.fixedDeltaTime;
            Vector3 newPosition = Vector3.forward * speed * Time.fixedDeltaTime;
            transform.position = Vector3.Lerp(transform.position, newPosition, 1f);
            yield return null;
        }
    }
}
