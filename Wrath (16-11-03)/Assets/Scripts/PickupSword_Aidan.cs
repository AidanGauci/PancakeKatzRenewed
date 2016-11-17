using UnityEngine;
using System.Collections;

public class PickupSword_Aidan : MonoBehaviour
{
    [HideInInspector]
    public bool isSwordTaken = false;
    [HideInInspector]
    public bool toSayFinished = false;

    public float checkDistance = 1f;
    public float keepTextActive = 1f;
    public string toSayBeforeSword;
    public string toSayAfterSword;

    PETER_PlayerAttack playerRef;
    TutorialWallTriggers_Aidan[] tutorialTriggers;
    UIManager_Aidan UI;
    GameManager_Aidan gameManager;
    float toSayTime;
    bool toSayActivated = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager_Aidan>();
        tutorialTriggers = FindObjectsOfType<TutorialWallTriggers_Aidan>();
        playerRef = FindObjectOfType<PETER_PlayerAttack>();
        UI = FindObjectOfType<UIManager_Aidan>();
    }

	void Update ()
    {
	    if (!isSwordTaken)
        {
            if (CircleCircleCheck(transform.position, 1, playerRef.transform.position, checkDistance))
            {
                UI.swordText.text = toSayBeforeSword;
                UI.swordText.gameObject.SetActive(true);
                UI.swordTextBackground.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    for (int i = 0; i < tutorialTriggers.Length; i++)
                    {
                        tutorialTriggers[i].tutorialFinished = true;
                    }

                    isSwordTaken = true;
                    UI.swordText.gameObject.SetActive(false);
                    UI.swordTextBackground.gameObject.SetActive(false);

                    Transform Model = playerRef.transform.FindChild("Model");
                    Transform pickaxe = Model.FindChild("PickaxeMesh");
                    Transform sword = Model.FindChild("SwordMesh");

                    pickaxe.gameObject.SetActive(false);
                    sword.gameObject.SetActive(true);
                    playerRef.hasWeapon = true;

                    GetComponentInChildren<MeshRenderer>().enabled = false;
                }
            }
            else if (!CircleCircleCheck(transform.position, 1, playerRef.transform.position, checkDistance) && !UI.tutorialInvisibleWallTriggered && !UI.tutorialWallTriggered)
            {
                UI.swordText.gameObject.SetActive(false);
                UI.swordTextBackground.gameObject.SetActive(false);
            }
        }
        else if (isSwordTaken && !UI.tutorialInvisibleWallTriggered)
        {
            if (!toSayActivated && !toSayFinished)
            {
                toSayTime = Time.time + keepTextActive;
                toSayActivated = true;
                UI.swordText.gameObject.SetActive(true);
                UI.swordText.text = toSayAfterSword;
                UI.swordTextBackground.gameObject.SetActive(true);
            }
            else if (toSayActivated && !toSayFinished)
            {
                if (toSayTime <= Time.time)
                {
                    UI.swordText.text = "";
                    UI.swordText.gameObject.SetActive(false);
                    UI.swordTextBackground.gameObject.SetActive(false);
                    toSayFinished = true;
                }
            }
        }
    }

    bool CircleCircleCheck(Vector3 P1, float R1, Vector3 P2, float R2)
    {
        return ((P1 - P2).sqrMagnitude < ((R1 * R1) + (R1 * R2)));
    }
}
