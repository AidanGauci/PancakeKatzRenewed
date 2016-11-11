using UnityEngine;
using System.Collections;

public class PickupSword_Aidan : MonoBehaviour {

    public float checkDistance = 1f;

    PETER_PlayerMovement playerRef;
    UIManager_Aidan UI;
    Jailer_Aidan jailer;
    bool isSwordTaken;

    void Start()
    {
        playerRef = FindObjectOfType<PETER_PlayerMovement>();
        UI = FindObjectOfType<UIManager_Aidan>();
        jailer = FindObjectOfType<Jailer_Aidan>();
    }

	void Update ()
    {
	    if (!isSwordTaken)
        {
            if (Vector3.Distance(transform.position, playerRef.transform.position) <= checkDistance)
            {
                UI.swordText.gameObject.SetActive(true);
                UI.swordTextBackground.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    isSwordTaken = true;
                    UI.swordText.gameObject.SetActive(false);
                    UI.swordTextBackground.gameObject.SetActive(false);
                }
            }
            else if (Vector3.Distance(transform.position, playerRef.transform.position) > checkDistance)
            {
                UI.swordText.gameObject.SetActive(false);
                UI.swordTextBackground.gameObject.SetActive(false);
            }
        }
        else if (isSwordTaken)
        {
            Transform camX = playerRef.transform.FindChild("CamX");
            Transform pickaxe = camX.FindChild("Pickaxe");
            Transform sword = camX.FindChild("Sword");

            pickaxe.gameObject.SetActive(false);
            sword.gameObject.SetActive(true);
            gameObject.SetActive(false);

            jailer.swordTaken = true;
        }
    }
}
