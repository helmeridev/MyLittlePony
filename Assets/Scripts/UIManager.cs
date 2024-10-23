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

    public static bool isPaused = false;


    void Start() {
        isPaused = false;
    }

    void Update() {
        HandlePauseMenu();

        if(settingsMenu.activeSelf) {
            audioText.text = audioSlider.value.ToString("0.00");
        }

        Config.masterAudio = audioSlider.value;
    }

    //General methods
    void HandlePauseMenu() {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused) {
            pauseMenu.SetActive(true);
            isPaused = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused) {
            config.WriteConfig();

            pauseMenu.SetActive(false);
            settingsMenu.SetActive(false);
            isPaused = false;
        }
    }
    public void UpdateSliders(float audio) {
        audioSlider.value = audio;
    }

    //Buttons
    public void _PM_Resume() {
        config.WriteConfig();

        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        isPaused = false;
    }
    public void _PM_Settings() {
        if(settingsMenu.activeSelf) settingsMenu.SetActive(false);
        else settingsMenu.SetActive(true);
    }
    public void _PM_Menu() {
        isPaused = false;
        SceneManager.LoadScene(0);
    }
    public void _PM_Quit() {
        Application.Quit();
    }
    public void _Settings_Close() {
        config.WriteConfig();

        settingsMenu.SetActive(false);
    }
    public void _Main_Play() {
        isPaused = false;
        SceneManager.LoadScene(1);
    }
    public void _Main_ToggleShowPath() {
        blockDataPath.SetActive(!blockDataPath.activeSelf);
        dataPathText.gameObject.SetActive(!dataPathText.gameObject.activeSelf);

        if(dataPathText.text != config.path) dataPathText.text = config.path;
    }
    public void _GameOver_Retry() {
        isPaused = false;
        SceneManager.LoadScene(1);
    }
}
