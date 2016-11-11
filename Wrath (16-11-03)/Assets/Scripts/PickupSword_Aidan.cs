using UnityEngine;
using System.Collections;

public class PickupSword_Aidan : MonoBehaviour
{

    public float checkDistance = 1f;

    PETER_PlayerAttack playerRef;
    UIManager_Aidan UI;
    public bool isSwordTaken;

    void Start()
    {
        playerRef = FindObjectOfType<PETER_PlayerAttack>();
        UI = FindObjectOfType<UIManager_Aidan>();
    }

	void Update ()
    {
	    if (!isSwordTaken)
        {
            if (CircleCircleCheck(transform.position, 1, playerRef.transform.position, checkDistance))
            {
                UI.swordText.text = "Press E to pickup Sword";
                UI.swordText.gameObject.SetActive(true);
                UI.swordTextBackground.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    isSwordTaken = true;
                    UI.swordText.gameObject.SetActive(false);
                    UI.swordTextBackground.gameObject.SetActive(false);

                    Transform Model = playerRef.transform.FindChild("Model");
                    Transform pickaxe = Model.FindChild("PickaxeMesh");
                    Transform sword = Model.FindChild("SwordMesh");

                    pickaxe.gameObject.SetActive(false);
                    sword.gameObject.SetActive(true);
                    playerRef.hasWeapon = true;

                    gameObject.SetActive(false);

                    Debug.Log("TUTORIAL COMPLETE");
                    playerRef.GetComponent<NavMeshAgent>().areaMask = 10001;
                }
            }
            else if (!CircleCircleCheck(transform.position, 1, playerRef.transform.position, checkDistance) && !UI.wallTriggered)
            {
                UI.swordText.gameObject.SetActive(false);
                UI.swordTextBackground.gameObject.SetActive(false);
            }
        }
    }

    bool CircleCircleCheck(Vector3 P1, float R1, Vector3 P2, float R2)
    {
        return ((P1 - P2).sqrMagnitude < ((R1 * R1) + (R1 * R2)));
    }
}
