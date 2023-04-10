using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    GameObject actualMenu;
    // Start is called before the first frame update
    void Start()
    {
        actualMenu = GameObject.Find("Canvas/MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int level)
    {
        Global.level = level;
        SceneManager.LoadScene("InGame");
    }

    public void ShowMenu(GameObject menu)
    {
        actualMenu.SetActive(false);
        menu.SetActive(true);
        actualMenu = menu;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
