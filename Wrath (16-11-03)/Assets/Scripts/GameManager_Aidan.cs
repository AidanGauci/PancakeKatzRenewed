using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager_Aidan : MonoBehaviour {

    [HideInInspector]
    public bool isSwordTaken = false;
    [HideInInspector]
    public bool isDoorBroken = false;
    [HideInInspector]
    public bool isInGameScene = false;
    public static GameManager_Aidan instance = null;

    EndRoomAllyLocations_Aidan endRoomLocations = null;
    AllyAI_Aidan[] allAllies = null;
    bool hasInitializedVariables = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
        }

        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (!Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (isDoorBroken)
        {
            SetEndDestination();
        }
        if (isSwordTaken)
        {      
            allAllies = FindObjectsOfType<AllyAI_Aidan>();

            int prng = Random.Range(0, 100);
            for (int i = 0; i < allAllies.Length; i++)
            {
                allAllies[i].swordObtained = true;
                allAllies[i].textChoicesSeed = prng;
            }

            isSwordTaken = false;
        }
    }

    void SetEndDestination()
    {
        if (endRoomLocations == null)
        {
            endRoomLocations = FindObjectOfType<EndRoomAllyLocations_Aidan>();
        }

        if (allAllies == null)
        {
            allAllies = FindObjectsOfType<AllyAI_Aidan>();
        }

        for (int i = 0; i < allAllies.Length; i++)
        {
            int modNum = i % endRoomLocations.allEndAllyLocations.Length;
            allAllies[i].SetEndDestination(endRoomLocations.allEndAllyLocations[modNum].position);
        }

        isDoorBroken = false;
    }

    public void HasHitEndBox()
    {
        Cursor.visible = true;
        allAllies = null;
        endRoomLocations = null;
        isInGameScene = false;
        hasInitializedVariables = false;
        SceneManager.LoadScene("EndGameScene");
    }
}
