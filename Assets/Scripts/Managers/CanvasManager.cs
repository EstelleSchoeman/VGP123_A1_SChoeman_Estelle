using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

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
    public TMP_Text volSliderText;
    public TMP_Text livesText;

    [Header("Slider")]
    public Slider volSlider;


    // Start is called before the first frame update
    void Start()
    {
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

    }
    void OnSliderValueChanged(float value)
    {
        volSliderText.text = value.ToString();
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
        
    }
}
