using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Config config;

    [Header("Pause Menu")]
    [SerializeField] GameObject pauseMenu;

    [Header("Settings Menu")]
    [SerializeField] GameObject settingsMenu;
    [SerializeField] Slider audioSlider;
    [SerializeField] TMP_Text audioText;

    [Header("Other")]
    [SerializeField] TMP_Text dataPathText;
    [SerializeField] GameObject blockDataPath;
    [SerializeField] AudioMixer audioMixer;
    float test;
    public GameObject dialogueUI;

    public static bool isPaused = false;
    public static int openGUIs;
    public static GameObject openGUI;


    void Start() {
        Unpause();
    }

    void Update() {
        HandlePauseMenu();
        
        if(settingsMenu.activeSelf) {
            audioMixer.SetFloat("volume", audioSlider.value * 15 - 40);
            audioText.text = audioSlider.value.ToString("0.00");
        }

        Config.masterAudio = audioSlider.value;
    }

    //General methods
    void HandlePauseMenu() {
        if(pauseMenu) {
            if(Input.GetKeyDown(KeyCode.Escape) && !isPaused && CanToggleGUI()) {
                OpenGUI(pauseMenu);
                Pause();
            }
            else if(Input.GetKeyDown(KeyCode.Escape) && isPaused) {
                config.WriteConfig();

                CloseGUI(pauseMenu);
                CloseGUI(settingsMenu);
                Unpause();
            }
        }
    }
    public void UpdateSliders(float audio) {
        audioMixer.SetFloat("volume", audio * 15 - 40);
        audioSlider.value = audio;
    }

    public static void OpenGUI(GameObject gui) {
        if(!gui.activeSelf && CanToggleGUI()) {
            openGUIs += 1;
            openGUI = gui;

            Movement.canMove = false;
            gui.SetActive(true);
        }
    }
    public static void OpenGUIForce(GameObject gui) {
        if(!gui.activeSelf) {
            openGUIs += 1;

            Movement.canMove = false;
            gui.SetActive(true);
        }
    }
    public static void CloseGUI(GameObject gui) {
        if(gui.activeSelf) {
            openGUIs -= 1;
            openGUI = null;

            Movement.canMove = true;
            gui.SetActive(false);
        }
    }

    //Buttons
    public void _PM_Resume() {
        config.WriteConfig();

        CloseGUI(pauseMenu);
        CloseGUI(settingsMenu);
        Unpause();
    }
    public void _PM_Settings() {
        if(settingsMenu.activeSelf) CloseGUI(settingsMenu);
        else OpenGUIForce(settingsMenu);
    }
    public void _PM_Menu() {
        Unpause();
        LevelManager.SetLevel(0);
    }
    public void _PM_Quit() {
        Application.Quit();
    }
    public void _Settings_Close() {
        config.WriteConfig();

        CloseGUI(settingsMenu);
    }
    public void _Main_Play() {
        Unpause();
        openGUIs = 0;
        LevelManager.NextLevel();
    }
    public void _Main_ToggleShowPath() {
        blockDataPath.SetActive(!blockDataPath.activeSelf);
        dataPathText.gameObject.SetActive(!dataPathText.gameObject.activeSelf);

        if(dataPathText.text != config.path) dataPathText.text = config.path;
    }
    public void _GameOver_Retry() {
        isPaused = false;
        LevelManager.NextLevel();
    }
    /////////////////////////////////////


    void Pause() {
        isPaused = true;
        Time.timeScale = 0;
    }

    void Unpause() {
        isPaused = false;
        Movement.canMove = true;
        Time.timeScale = 1;
    }

    //GUI can toggle stuff
    public static bool CanToggleGUI() {
        return openGUIs <= 0;
    }
}
