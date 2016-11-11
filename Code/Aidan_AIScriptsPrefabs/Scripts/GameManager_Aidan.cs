using UnityEngine;
using System.Collections;

public class GameManager_Aidan : MonoBehaviour {

    [HideInInspector]
    public bool isDoorBroken = false;

    EndRoomAllyLocations_Aidan endRoomLocations;
    AllyAI_Aidan[] allAllies;
    int endRoomCounter = 0;

    void Start()
    {
        endRoomLocations = FindObjectOfType<EndRoomAllyLocations_Aidan>();
        allAllies = FindObjectsOfType<AllyAI_Aidan>();
    }

    void Update()
    {
        if (isDoorBroken)
        {
            for (int i = 0; i < allAllies.Length; i++)
            {
                int modNum = i % endRoomLocations.allEndAllyLocations.Length;

                SetEndDestination(i, modNum);
            }

            isDoorBroken = false;
        }
    }

    void SetEndDestination(int i, int modNum)
    {
        allAllies[i].SetEndDestination(endRoomLocations.allEndAllyLocations[modNum].position);
    }
}
