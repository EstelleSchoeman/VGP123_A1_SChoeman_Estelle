using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;

public class CanvasManager : MonoBehaviour
{
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
    public TMP_Text volSliderText;
    public TMP_Text livesText;

    [Header("Slider")]
    public Slider volSlider;


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
        
        if(volSlider)
        {
            volSlider.onValueChanged.AddListener(OnSliderValueChanged);
            if(volSliderText)
                volSliderText.text = volSlider.value.ToString();
        }

        if(livesText)
        {
            GameManager.Instance.OnLifeValueChanged.AddListener(UpdateLifeText);
            livesText.text = "Lives:" + GameManager.Instance.lives.ToString();
        }

    }
    void OnSliderValueChanged(float value)
    {
        volSliderText.text = value.ToString();
    }

    void UpdateLifeText(int value)
    {
        livesText.text = "Lives:" + value.ToString();
        
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
