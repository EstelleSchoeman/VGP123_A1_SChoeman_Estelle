using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.Audio;

public class CanvasManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public static bool pausedTime = false;

    [Header("Button")]
    public Button playButton;
    public Button settingsButton;
    public Button quitButtonPause;
    public Button quitButtonTitle;
    public Button resumebutton;
    public Button returnToMenuButton;
    public Button backButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingMenu;
    public GameObject pauseMenu;

    [Header("Text")]
    public TMP_Text MasterVolSliderText;
    public TMP_Text MusicVolSliderText;
    public TMP_Text SFXVolSliderText;
    //public TMP_Text livesText;
    public TMP_Text scoreText;

    [Header("Slider")]
    public Slider MasterVolSlider;
    public Slider MusicVolSlider;
    public Slider SFXVolSlider;


    // Start is called before the first frame update
    void Start()
    {
        if (resumebutton)
        {
            resumebutton.onClick.AddListener(() => SetMenus(null, pauseMenu));
        }

        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(0));

        if (quitButtonPause)
            quitButtonPause.onClick.AddListener(Quit);

        if (quitButtonTitle)
            quitButtonTitle.onClick.AddListener(Quit);

        if (playButton)
            playButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(1));

        if (settingsButton)
            settingsButton.onClick.AddListener(() => SetMenus(settingMenu,mainMenu));

        if (backButton)
            backButton.onClick.AddListener(() => SetMenus(mainMenu, settingMenu));
        
        if(MasterVolSlider)
        {
            MasterVolSlider.onValueChanged.AddListener((value)=>OnSliderValueChanged(value, MasterVolSliderText, "MasterVol"));
            float mixerValue;
            audioMixer.GetFloat("MasterVol", out mixerValue);
            MasterVolSlider.value = mixerValue + 80;
            if (MasterVolSliderText)
                MasterVolSliderText.text = MasterVolSlider.value.ToString();
        }

        if (MusicVolSlider)
        {
            MusicVolSlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value, MusicVolSliderText, "MusicVol"));
            float mixerValue;
            audioMixer.GetFloat("MusicVol", out mixerValue);
            MusicVolSlider.value = mixerValue + 80;
            if (MusicVolSliderText)
                MusicVolSliderText.text = MusicVolSlider.value.ToString();
        }

        if (SFXVolSlider)
        {
            SFXVolSlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value, SFXVolSliderText, "sfxVol"));
            float mixerValue;
            audioMixer.GetFloat("sfxVol", out mixerValue);
            SFXVolSlider.value = mixerValue + 80;
            if (SFXVolSliderText)
                SFXVolSliderText.text = SFXVolSlider.value.ToString();
        }

        //if (livesText)
        //{
        //    GameManager.Instance.OnLifeValueChanged.AddListener(UpdateLifeText);
        //    livesText.text = "Lives:" + GameManager.Instance.lives.ToString();
        //}

        if (scoreText)
        {
            GameManager.Instance.OnScoreValueChanged.AddListener(UpdateScoreText);
            scoreText.text =  GameManager.Instance.score.ToString();
        }


    }
    void OnSliderValueChanged(float value,TMP_Text volSliderText, string paramName)
    {
        volSliderText.text = value.ToString();
        audioMixer.SetFloat(paramName, value - 80);
    }

    //void UpdateLifeText(int value)
    //{
     //   livesText.text = "Lives:" + value.ToString();
     //   
   // }

    void UpdateScoreText(int value)
    {
        scoreText.text = value.ToString();

    }


    private void Quit()
    {
    #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    # endif

    }

    void SetMenus(GameObject menuToActivate, GameObject menuToDeactivate)
    {

        if(menuToActivate)
        menuToActivate.SetActive(true);

        if(menuToDeactivate)
       menuToDeactivate.SetActive(false);
    }
    
   

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu) return;
        
        if(Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            

            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0f;
                pausedTime = true;
            }
            else
            {
                Time.timeScale = 1f;
                pausedTime = false;
            }


        }
        if (!pauseMenu.activeSelf)
        {
            Time.timeScale = 1f;
            pausedTime = false;
        }

    }
}
