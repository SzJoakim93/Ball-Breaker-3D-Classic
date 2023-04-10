using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text StarTxt;

    [SerializeField]
    Text TotalScores;

    [SerializeField]
    GameObject LevelButtonPack;
    Button [] levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        levelButtons = LevelButtonPack.GetComponentsInChildren<Button>(true);
        for (int i = 1; i < PlayerPrefs.GetInt("UnlockedLevels", 1); i++)
            levelButtons[i].interactable = true;

        Global.LoadState();
        StarTxt.text = Global.TotalStars.ToString();
        TotalScores.text = Global.TotalScores.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
