using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour {
    public GameObject scrollbar;
    float scroll_pos = 0;
    float[] pos;
    AudioManager audioManager;

    SceneLoader sceneLoader;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void mainmenuClicked() {
        SceneManager.LoadScene("MainMenu");
        audioManager.PlaySFX(audioManager.buttonClick);
    }

    public void Level_PertamaClicked() {
        sceneLoader.LoadScene(2);
        audioManager.PlaySFX(audioManager.buttonClick);
    }

    public void Level_KeduaClicked() {
        sceneLoader.LoadScene(3);
        audioManager.PlaySFX(audioManager.buttonClick);
    }

    public void Level_KetigaClicked() {
        sceneLoader.LoadScene(4);
        audioManager.PlaySFX(audioManager.buttonClick);
    }

    void Update() {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++) {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton (0)) {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && scroll_pos < 1f) {
            scroll_pos += distance;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && scroll_pos > 0f) {
            scroll_pos -= distance;
        } else if (Input.GetKeyDown(KeyCode.D) && scroll_pos < 1f) {
            scroll_pos += distance;
        } else if (Input.GetKeyDown(KeyCode.A) && scroll_pos > 0f) {
            scroll_pos -= distance;
        } else {
            for (int i = 0; i < pos.Length; i++) {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2)) {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++) {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2)) {
                transform.GetChild (i).localScale = Vector2.Lerp (transform.GetChild(i).localScale, new Vector2(1f,1f), 0.1f);
                for (int a = 0; a < pos.Length; a++) {
                    if (a != i) {
                        transform.GetChild (a).localScale = Vector2.Lerp (transform.GetChild(a).localScale, new Vector2(0.8f,0.8f), 0.1f);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)) 
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}