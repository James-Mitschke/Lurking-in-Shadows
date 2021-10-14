using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class S_MainMenu : MonoBehaviour
{
    int MusicVolume;
    int SoundVolume;

    public Slider MVSlider;
    public Slider SVSlider;
    private Toggle FullToggle;
    public TMP_Text MVText;
    public TMP_Text SVText;

    Scene ActiveScene;

    private bool FullTogg;

    void Start()
    {
        Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen", 1));

        FullTogg = false;
        Cursor.lockState = CursorLockMode.None;
         
        ActiveScene = SceneManager.GetActiveScene();

        MusicVolume = PlayerPrefs.GetInt("MusicVolume", 100);
        SoundVolume = PlayerPrefs.GetInt("SoundVolume", 100);

        if(ActiveScene.name == "SettingsMenu")
        {
            MVSlider = GameObject.FindWithTag("MVSlider").GetComponent<Slider>();
            MVText = GameObject.FindWithTag("MVText").GetComponent<TMP_Text>();
            MVSlider.value = MusicVolume;
            MVText.text = ("Music Volume : " + MusicVolume);

            SVSlider = GameObject.FindWithTag("SVSlider").GetComponent<Slider>();
            SVText = GameObject.FindWithTag("SVText").GetComponent<TMP_Text>();
            SVSlider.value = SoundVolume;
            SVText.text = ("Sound Volume : " + SoundVolume);

            FullToggle = GameObject.FindWithTag("FullToggle").GetComponent<Toggle>();

            FullToggle.isOn = Screen.fullScreen;
        }

        if(ActiveScene.name == "ControlsMenu")
        {
            MVText = GameObject.FindWithTag("ControlsText").GetComponent<TMP_Text>();
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            Fullscreen();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("MusicVolume", MusicVolume);
        PlayerPrefs.SetInt("SoundVolume", SoundVolume);
        PlayerPrefs.SetInt("Fullscreen", System.Convert.ToInt32(Screen.fullScreen));

        SceneManager.LoadScene("Mansion_final");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("scene1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ControlsMenu()
    {
        SceneManager.LoadScene("ControlsMenu");
    }

    public void Fullscreen()
    {
        if (FullTogg == false)
        {
            StartCoroutine(FullTog());
        }
    }

    public void WASD()
    {
        MVText.text = "WASD are the keys used for movement in the forward, left, backward and right directions respectively.";
    }

    public void Shift()
    {
        MVText.text = "The left shift key is used to sprint instead of just walking when combined with the WASD keys, this button must be held to continue sprinting and you get about 10 seconds before you need to rest.";
    }

    public void Light()
    {
        MVText.text = "The F key and right click on the mouse can be used to turn on or off your lights, the scroll wheel on the mouse can be used to swap what lightsource you are using.";
    }

    public void Interact()
    {
        MVText.text = "The E key can be used to open doors, cupboards and certain objects such as drawers too. This can also be used to pick up keys!";
    }

    public void Pause()
    {
        MVText.text = "The Escape key (Esc) can be used to pause and unpause the game whilst you are playing, you can save from this menu and change some settings too.";
    }

    public void MVUpdate()
    {
        int MusicVolume = (int)MVSlider.value;
        MVText.text = ("Music Volume : " + MusicVolume);

        PlayerPrefs.SetInt("MusicVolume", MusicVolume);
    }

    public void SVUpdate()
    {
        int SoundVolume = (int)SVSlider.value;
        SVText.text = ("Sound Volume : " + SoundVolume);

        PlayerPrefs.SetInt("SoundVolume", SoundVolume);
    }

    public void RestoreDefaults()
    {
        PlayerPrefs.DeleteKey("SoundVolume");
        PlayerPrefs.DeleteKey("MusicVolume");
        PlayerPrefs.DeleteKey("Fullscreen");

        ActiveScene = SceneManager.GetActiveScene();

        if (ActiveScene.name == "SettingsMenu")
        {
            SceneManager.LoadScene("SettingsMenu");
        }
    }

    public IEnumerator FullTog()
    {
        FullTogg = true;
        yield return new WaitForSeconds(0.1f);

        if (FullToggle.isOn != Screen.fullScreen)
        {
            Screen.fullScreen = FullToggle.isOn;

            PlayerPrefs.SetInt("Fullscreen", System.Convert.ToInt32(FullToggle.isOn));
        }

        FullTogg = false;
    }
}
