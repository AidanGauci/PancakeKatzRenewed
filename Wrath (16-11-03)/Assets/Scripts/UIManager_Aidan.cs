using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager_Aidan : MonoBehaviour {

    public Text allyText;
    public Text allyCount;
    public Text swordText;
    public Image swordTextBackground;
    public Text[] saveAllyTexts;
    public Image[] saveAllyBackgrounds;
    public Image[] healthIcons;
    public GameObject firstEnemy;
    public float textAfterKillGuard = 2f;
    public int allyCurrentCount { get; private set; }
    [HideInInspector]
    public bool tutorialInvisibleWallTriggered = false;
    [HideInInspector]
    public bool tutorialWallTriggered = false;

    PickupSword_Aidan pickup;
    float deactivateTime1;
    float deactivateTime2;
    float deactivateTime3 = float.PositiveInfinity;
    bool canActivate = false;

    void Start()
    {
        pickup = FindObjectOfType<PickupSword_Aidan>();
    }

    void Update()
    {
        if (deactivateTime1 <= Time.time)
        {
            saveAllyTexts[0].text = "";
            saveAllyTexts[0].gameObject.SetActive(false);
            saveAllyBackgrounds[0].gameObject.SetActive(false);
        }

        if (deactivateTime2 <= Time.time)
        {
            saveAllyTexts[1].text = "";
            saveAllyTexts[1].gameObject.SetActive(false);
            saveAllyBackgrounds[1].gameObject.SetActive(false);
        }

        if (tutorialInvisibleWallTriggered)
        {
            if (!pickup.isSwordTaken)
            {
                swordText.gameObject.SetActive(true);
                swordTextBackground.gameObject.SetActive(true);
                swordText.text = "Get back to work!";
            }
            else if (pickup.isSwordTaken && firstEnemy != null)
            {
                swordText.gameObject.SetActive(true);
                swordTextBackground.gameObject.SetActive(true);
                swordText.text = "Press left-click to attack the guard and escape!";
            }
            else if (pickup.isSwordTaken && firstEnemy == null)
            {
                swordText.gameObject.SetActive(true);
                swordTextBackground.gameObject.SetActive(true);
                swordText.text = "Now I can free the miner!";
                deactivateTime3 = Time.time + textAfterKillGuard;
                canActivate = true;
            }
        }
        else
        {
            if (pickup.isSwordTaken && pickup.toSayFinished)
            {
                swordText.gameObject.SetActive(false);
                swordTextBackground.gameObject.SetActive(false);
                swordText.text = "";
            }
        }
        if (tutorialWallTriggered)
        {
            swordText.gameObject.SetActive(true);
            swordTextBackground.gameObject.SetActive(true);
        }
        
        if (canActivate)
        {
            swordText.gameObject.SetActive(true);
            swordTextBackground.gameObject.SetActive(true);
            swordText.text = "Now I can free the miners by pressing 'E'!";
        }

        if (deactivateTime3 <= Time.time && firstEnemy == null)
        {
            swordText.gameObject.SetActive(false);
            swordTextBackground.gameObject.SetActive(false);
            swordText.text = "";
        }
    }

    public void OnAllyCountChange()
    {
        allyCurrentCount++;
        allyCount.text = allyCurrentCount + "/20";
    }

    public void ActivateText(string toSay, float stayTime)
    {
        if (saveAllyTexts[0].gameObject.activeSelf)
        {
            deactivateTime2 = Time.time + stayTime;
            saveAllyTexts[1].text = toSay;
            saveAllyTexts[1].gameObject.SetActive(true);
            saveAllyBackgrounds[1].gameObject.SetActive(true);
        }
        else
        {
            deactivateTime1 = Time.time + stayTime;
            saveAllyTexts[0].text = toSay;
            saveAllyTexts[0].gameObject.SetActive(true);
            saveAllyBackgrounds[0].gameObject.SetActive(true);
        }
    }
}
