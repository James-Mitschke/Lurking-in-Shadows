using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S_GameManager : MonoBehaviour
{
    private Slider MusicSlider;
    private Slider SoundSlider;
    private Toggle FullToggle;
    private bool FullTogg;

    private TMP_Text MusicText;
    private TMP_Text SoundText;
    private TMP_Text KeysText;

    private int MusicVolumeKey;
    private int SoundVolumeKey;
    private int FullscreenKey;

    private GameObject O_Player;
    private GameObject C_Player;
    private GameObject O_Monster;
    private GameObject C_Monster;
    private GameObject Keys;
    private GameObject[] List;
    private Vector3 O_Player_Pos;
    private Vector3 C_Player_Pos;
    private Vector3 O_Monster_Pos;
    private Vector3 C_Monster_Pos;

    private bool Paused;
    private Canvas PauseMenu;
    private GameObject PauseCamera;
    private AudioSource Music;
    private AudioSource Sound;
    private bool StartDelay;


    void Start()
    {
        FullTogg = false;

        MusicSlider = GameObject.FindGameObjectWithTag("MVSlider").GetComponent<Slider>();
        SoundSlider = GameObject.FindGameObjectWithTag("SVSlider").GetComponent<Slider>();

        FullToggle = GameObject.FindGameObjectWithTag("FullToggle").GetComponent<Toggle>();

        MusicText = GameObject.FindGameObjectWithTag("MVText").GetComponent<TMP_Text>();
        SoundText = GameObject.FindGameObjectWithTag("SVText").GetComponent<TMP_Text>();

        MusicVolumeKey = PlayerPrefs.GetInt("MusicVolume");
        SoundVolumeKey = PlayerPrefs.GetInt("SoundVolume");
        FullscreenKey = PlayerPrefs.GetInt ("Fullsreen");

        MusicSlider.value = MusicVolumeKey;
        SoundSlider.value = SoundVolumeKey;

        FullToggle.isOn = Screen.fullScreen;

        Time.timeScale = 1;
        Paused = false;

        Cursor.lockState = CursorLockMode.Locked;

        PauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>();
        PauseMenu.enabled = false;

        PauseCamera = GameObject.FindGameObjectWithTag("PauseCamera");

        PauseCamera.SetActive(false);

        StartCoroutine(PauseDelay());
        Load();
    }

    
    void Update()
    {
        if (StartDelay == false)
        {
            if (Input.GetKeyDown(KeyCode.F11))
            {
                Screen.fullScreen = !Screen.fullScreen;

                PlayerPrefs.SetInt("Fullscreen", System.Convert.ToInt32(Screen.fullScreen));
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if ((KeysText.text.Contains("collected")) && ((PlayerPrefs.GetInt("Servant Quarters") == 1) || (PlayerPrefs.GetInt("Master Bedroom") == 1) || (PlayerPrefs.GetInt("Family Dining Room") == 1) || (PlayerPrefs.GetInt("Bathroom") == 1) || (PlayerPrefs.GetInt("Basement") == 1)))
                {
                    KeysText.text = "";
                }

                if((PlayerPrefs.GetInt("Servant Quarters") == 1) && !KeysText.text.Contains("Servant Quarters"))
                {
                    KeysText.text += "\u2022 Servant Quarters Key \n";
                }

                if((PlayerPrefs.GetInt("Master Bedroom") == 1) && !KeysText.text.Contains("Master Bedroom"))
                {
                    KeysText.text += "\u2022 Master Bedroom Key \n";
                }

                if((PlayerPrefs.GetInt("Family Dining Room") == 1) && !KeysText.text.Contains("Family Dining"))
                {
                    KeysText.text += "\u2022 Family Dining Room Key \n";
                }

                if((PlayerPrefs.GetInt("Bathroom") == 1) && !KeysText.text.Contains("Bathroom"))
                {
                    KeysText.text += "\u2022 Bathroom Key \n";
                }

                if((PlayerPrefs.GetInt("Basement") == 1) && !KeysText.text.Contains("Basement"))
                {
                    KeysText.text += "\u2022 Basement Key \n";
                }

                if (Paused == false)
                {
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Music.Pause();
                    O_Player_Pos = O_Player.transform.localPosition;
                    C_Player_Pos = C_Player.transform.localPosition;
                    O_Monster_Pos = O_Monster.transform.localPosition;
                    C_Monster_Pos = C_Monster.transform.localPosition;
                    O_Player.SetActive(false);
                    C_Player.SetActive(false);
                    O_Monster.SetActive(false);
                    C_Monster.SetActive(false);
                    PauseCamera.SetActive(true);
                    PauseMenu.enabled = true;
                    Paused = true;
                }

                else
                {
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    PauseMenu.enabled = false;
                    Music.UnPause();
                    PauseCamera.SetActive(false);
                    C_Player.SetActive(true);
                    O_Player.SetActive(true);
                    O_Monster.SetActive(true);
                    C_Monster.SetActive(true);
                    Paused = false;
                }
            }
        }
    }

    public void MVUpdate()
    {
        int MusicVolumeKey = (int)MusicSlider.value;
        MusicText.text = ("Music Volume : " + MusicVolumeKey);

        PlayerPrefs.SetInt("MusicVolume", MusicVolumeKey);
    }

    public void SVUpdate()
    {
        int SoundVolumeKey = (int)SoundSlider.value;
        SoundText.text = ("Sound Volume : " + SoundVolumeKey);

        PlayerPrefs.SetInt("SoundVolume", SoundVolumeKey);
    }

    public void Fullscreen()
    {
        if (FullTogg == false)
        {
            StartCoroutine(FullTog());
        }
    }

    void Save()
    {
        PlayerPrefs.SetFloat("O_PlayerX", O_Player_Pos.x);
        PlayerPrefs.SetFloat("O_PlayerY", O_Player_Pos.y);
        PlayerPrefs.SetFloat("O_PlayerZ", O_Player_Pos.z);


        PlayerPrefs.SetFloat("C_PlayerX", C_Player_Pos.x);
        PlayerPrefs.SetFloat("C_PlayerY", C_Player_Pos.y);
        PlayerPrefs.SetFloat("C_PlayerZ", C_Player_Pos.z);


        PlayerPrefs.SetFloat("O_MonsterX", O_Monster_Pos.x);
        PlayerPrefs.SetFloat("O_MonsterY", O_Monster_Pos.y);
        PlayerPrefs.SetFloat("O_MonsterZ", O_Monster_Pos.z);


        PlayerPrefs.SetFloat("C_MonsterX", C_Monster_Pos.x);
        PlayerPrefs.SetFloat("C_MonsterY", C_Monster_Pos.y);
        PlayerPrefs.SetFloat("C_MonsterZ", C_Monster_Pos.z);
    }

    public void SaveMenu()
    {
        Save();

        SceneManager.LoadScene("MainMenu");
    }

    public void SaveDesktop()
    {
        Save();

        Application.Quit();
    }

    public void Load()
    {
        List = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < List.Length; i++)
        {
            O_Player = List[i];

            if (O_Player.name == "O_Player")
            {
                break;
            }
        }

        if (PlayerPrefs.HasKey("O_PlayerX"))
        {
            O_Player.transform.localPosition = new Vector3(PlayerPrefs.GetFloat("O_PlayerX"), PlayerPrefs.GetFloat("O_PlayerY"), PlayerPrefs.GetFloat("O_PlayerZ"));
        }
        

        for (int i = 0; i < List.Length; i++)
        {
            C_Player = List[i];

            if (C_Player.name == "C_Player")
            {
                break;
            }
        }

        if (PlayerPrefs.HasKey("C_PlayerTX"))
        {
            C_Player.transform.localPosition = new Vector3(PlayerPrefs.GetFloat("C_PlayerX"), PlayerPrefs.GetFloat("C_PlayerY"), PlayerPrefs.GetFloat("C_PlayerZ"));
        }

        List = GameObject.FindGameObjectsWithTag("Monster");

        for (int i = 0; i < List.Length; i++)
        {
            C_Monster = List[i];

            if (C_Monster.name == "C_Monster")
            {
                break;
            }
        }

        if(PlayerPrefs.HasKey("C_MonsterX"))
        {
            C_Monster.transform.localPosition = new Vector3(PlayerPrefs.GetFloat("C_MonsterX"), PlayerPrefs.GetFloat("C_MonsterY"), PlayerPrefs.GetFloat("C_MonsterZ"));
        }

        for (int i = 0; i < List.Length; i++)
        {
            O_Monster = List[i];

            if (O_Monster.name == "O_Monster")
            {
                break;
            }
        }

        if (PlayerPrefs.HasKey("O_MonsterX"))
        {
           O_Monster.transform.localPosition = new Vector3(PlayerPrefs.GetFloat("O_MonsterX"), PlayerPrefs.GetFloat("O_MonsterY"), PlayerPrefs.GetFloat("O_MonsterZ"));
        }

        Music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        Sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>();

        KeysText = GameObject.FindGameObjectWithTag("KeyText").GetComponent<TMP_Text>();

        StartDelay = false;
    }

    IEnumerator PauseDelay()
    {
        StartDelay = true;

        yield return new WaitForSeconds(2.0f);
    }

    IEnumerator FullTog()
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
