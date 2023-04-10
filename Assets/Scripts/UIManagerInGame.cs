using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerInGame : MonoBehaviour
{
    [SerializeField] Text LifeTxt;
    [SerializeField] Text StarTxt;
    [SerializeField] Text ScoreTxt;
    [SerializeField] Text StarInPanelTxt;
    [SerializeField] Text ScoreInPanelTxt;
    [SerializeField] Text AmmoTxt;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject CompPanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject CompTitle;
    [SerializeField] AdManager adManager;
    private bool pause;
    public bool Pause
    {
        get { return pause; }
    }

    // Start is called before the first frame update
    void Start()
    {
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LifeChanged(int life)
    {
        LifeTxt.text = life.ToString();
    }

    public void AmmoChanged(int ammo)
    {
        AmmoTxt.text = ammo.ToString();
    }

    public void StarChanged(int star)
    {
        StarTxt.text = star.ToString();
        StarInPanelTxt.text = star.ToString();
    }

    public void ScoreChanged(int score)
    {
        ScoreTxt.text = score.ToString();
        ScoreInPanelTxt.text = score.ToString();
    }

    public void ShowGameOver()
    {
        GameOverPanel.SetActive(true);
    }

    public void ShowCompletedTitle()
    {
        pause = true;
        CompTitle.SetActive(true);
    }

    public void ShowCompletedPanel()
    {
        CompPanel.SetActive(true);
    }

    public void ShowPause()
    {
        pause = true;
        PausePanel.SetActive(true);
    }

    public void Resume()
    {
        pause = false;
        PausePanel.SetActive(false);
    }

    public void Restart()
    {
        adManager.ShowInterstitial();
        SceneManager.LoadScene("InGame");
    }

    public void Quit()
    {
        adManager.ShowInterstitial();
        SceneManager.LoadScene("Menu");
    }

    public void NextLevel()
    {
        adManager.ShowInterstitial();
        Global.level++;
        SceneManager.LoadScene("InGame");
    }
}
