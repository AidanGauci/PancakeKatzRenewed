using UnityEngine;
using System.Collections;

public class TutorialWallTriggers : MonoBehaviour
{
    [HideInInspector]
    public bool tutorialFinished = false;
    public string toSay;

    UIManager_Aidan UI;

    void Start()
    {
        UI = FindObjectOfType<UIManager_Aidan>();
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player" && !tutorialFinished)
        {
            UI.tutorialWallTriggered = true;
            UI.swordText.text = toSay;
            UI.swordText.gameObject.SetActive(true);
            UI.swordTextBackground.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.tag == "Player" && !tutorialFinished)
        {
            UI.tutorialWallTriggered = false;
            UI.swordText.text = "";
            UI.swordText.gameObject.SetActive(false);
            UI.swordTextBackground.gameObject.SetActive(false);
        }
    }
}
