using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public GameObject bg;
    public GameObject options;
    public GameObject exit;
    AudioManager audioManager;

    void Start() {
        bg.SetActive(true);
        options.SetActive(false);
        exit.SetActive(false);
    }

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    public void playClicked() {
        SceneManager.LoadScene("PlayMenu");
        audioManager.PlaySFX(audioManager.buttonClick);
    }
    
    public void quitClicked() {
        Application.Quit();
        audioManager.PlaySFX(audioManager.buttonClick);
    }

    public void optionsClicked() {
        bg.SetActive(false);
        options.SetActive(true);
        exit.SetActive(false);
        audioManager.PlaySFX(audioManager.buttonClick); 
    }

    public void exitClicked() {
        bg.SetActive(false);
        options.SetActive(false);
        exit.SetActive(true);
        audioManager.PlaySFX(audioManager.buttonClick); 
    }

    public void backClicked() {
        bg.SetActive(true);
        options.SetActive(false);
        exit.SetActive(false);
        audioManager.PlaySFX(audioManager.buttonClick);
    }

    private void Update() {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)) && (options.activeSelf || exit.activeSelf)) 
        {
            bg.SetActive(true);
            options.SetActive(false);
            exit.SetActive(false);
        }
    }
}
