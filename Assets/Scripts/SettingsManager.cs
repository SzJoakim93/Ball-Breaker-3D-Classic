using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    Slider soundSlider;
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    GameObject[] languageApplyeImgs;
    [SerializeField]
    LanguageManager languageManager;
    [SerializeField]
    GameObject[] inputApplyeImgs;

    // Start is called before the first frame update
    void Start()
    {
        soundSlider.value = Settings.SoundVolume;
        musicSlider.value = Settings.MusicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSoundVolume(float x)
    {
        Settings.SoundVolume = x;
        Settings.SoundEnabled = x != 0.0f;
    }

    public void SetMusicVolume(float x)
    {
        Settings.MusicVolume = x;
        Settings.MusicEnabled = x != 0.0f;
    }

    public void SetLanguage(int x)
    {
        Settings.Language = Settings.languageIds[x];

        for (int i = 0; i < languageApplyeImgs.Length; i++)
        {
            languageApplyeImgs[i].SetActive(i == x);
        }
        
    }

    public void SetInputType(int x)
    {
        Settings.Input = (Settings.InputType)x;

        for (int i = 0; i < inputApplyeImgs.Length; i++)
        {
            inputApplyeImgs[i].SetActive(i == x);
        }
        
    }

    public void Apply()
    {
        languageManager.SetLanguage();
    }
}
