using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Button")]
    public Button playButton;
    public Button settingsButton;
    public Button quitButto;
    public Button resumebutton;
    public Button returnToMenuButton;
    public Button backButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingMenu;
    public GameObject pauseMenu;

    [Header("Text")]
    public TMP_Text volSliderTExt;
    public TMP_Text livesText;

    [Header("Slider")]
    public Slider volSlider;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
