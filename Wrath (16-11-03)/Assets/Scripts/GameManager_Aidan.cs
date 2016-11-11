using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager_Aidan : MonoBehaviour {

    [HideInInspector]
    public bool isDoorBroken = false;
    public static GameManager_Aidan instance = null;

    EndRoomAllyLocations_Aidan endRoomLocations;
    AllyAI_Aidan[] allAllies;

    void Awake()
    {
        endRoomLocations = FindObjectOfType<EndRoomAllyLocations_Aidan>();
        allAllies = FindObjectsOfType<AllyAI_Aidan>();

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (isDoorBroken)
        {
            SetEndDestination();
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
        SceneManager.LoadScene("EndGameScene");
    }
}
