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
    [SerializeField] AdManagerInterstitial adManager;
    private bool pause;
    public bool Pause
    {
        get { return pause; }
    }

    enum SelectedButton
    {
        QuitToMenu,
        NextLevel,
        RestartLevel
    }
    SelectedButton selectedButton;

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
        selectedButton = SelectedButton.RestartLevel;
        if (adManager.ShowAd())
        {
            return;
        }
        SceneManager.LoadScene("InGame");
    }

    public void Quit()
    {
        selectedButton = SelectedButton.QuitToMenu;
        if (adManager.ShowAd())
        {
            return;
        }
        SceneManager.LoadScene("Menu");
    }

    public void NextLevel()
    {
        selectedButton = SelectedButton.NextLevel;
        if (adManager.ShowAd())
        {
            return;
        }
        Global.level++;
        SceneManager.LoadScene("InGame");
    }

    public void OnAdClosedEvent() {
        switch (selectedButton)
        {
            case SelectedButton.RestartLevel:
                SceneManager.LoadScene("InGame");
                break;
            case SelectedButton.QuitToMenu:
                SceneManager.LoadScene("Menu");
                break;
            case SelectedButton.NextLevel:
                Global.level++;
                SceneManager.LoadScene("InGame");
                break;
        }
    }
}
