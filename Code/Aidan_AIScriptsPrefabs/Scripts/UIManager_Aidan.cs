using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager_Aidan : MonoBehaviour {

    public Text allyText;
    public Text allyCount;
    public Text swordText;
    public Image swordTextBackground;
    public Image[] healthIcons;

    public int allyCurrentCount { get; private set; }

    public void OnAllyCountChange()
    {
        allyCurrentCount++;
        allyCount.text = allyCurrentCount + "/20";
    }
}
