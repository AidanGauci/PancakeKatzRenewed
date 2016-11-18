using UnityEngine;
using System.Collections;

public class DefeatScreen_Aidan : MonoBehaviour {

    public Transform lookPos;

    void Update()
    {
        transform.LookAt(lookPos.position);
    }
}
