using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit_Scene : MonoBehaviour
{
    AudioManager audioManager;
    public string sceneToLoad = "MainMenu";

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        StartCoroutine(WaitAndLoadMainMenu(25f));
        audioManager.StopBackgroundMusic();
        audioManager.CreditScene();
    }

    IEnumerator WaitAndLoadMainMenu(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneToLoad);
    }
}
