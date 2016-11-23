using UnityEngine;
using System.Collections;

public class EndGameScreen_Aidan : MonoBehaviour {

    public AnimationCurve foo;

    public Transform lookRef;
    public Transform[] playerLookPositions;
    public float jumpHeightMin = 2f;
    public float jumpHeightMax = 3f;
    public float jumpSpeed = 2f;

    float randOffset;
    float randJumpHeight;
    float currentLerpTime = 0;
    float currentDelayTime = float.PositiveInfinity;
    Vector3 currentPosition;
    Vector3 jumpPosition;

    void Awake()
    {
        transform.LookAt(lookRef);
        randOffset = Random.value;
        currentPosition = transform.position;
        randJumpHeight = Random.Range(jumpHeightMin, jumpHeightMax);
        jumpPosition = currentPosition + (Vector3.up * randJumpHeight);
        currentDelayTime = Time.time + randOffset;
    }

    void Update()
    {
        if (tag == "Player")
        {
            transform.LookAt(lookRef);
            lookRef.position = Vector3.Lerp(playerLookPositions[0].position, playerLookPositions[1].position, currentLerpTime);
            currentLerpTime += Time.deltaTime * jumpSpeed;
            if (lookRef.position == playerLookPositions[1].position)
            {
                Vector3 placeholder = playerLookPositions[0].position;
                playerLookPositions[0].position = playerLookPositions[1].position;
                playerLookPositions[1].position = placeholder;
                currentLerpTime = 0;
            }
        }
        else if (tag == "Ally")
        {
            if (currentDelayTime <= Time.time)
            {
                transform.position = Vector3.Lerp(currentPosition, jumpPosition, foo.Evaluate(currentLerpTime));
                currentLerpTime += Time.deltaTime * jumpSpeed;
                if (transform.position == jumpPosition)
                {
                    Vector3 placeholder = jumpPosition;
                    jumpPosition = currentPosition;
                    currentPosition = placeholder;
                    currentLerpTime = 0;   
                }
            }
        }
    }
}
