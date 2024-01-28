using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Complate : MonoBehaviour
{
    AudioManager audioManager;
    SceneLoader sceneLoader; // Tambahkan variabel untuk menyimpan referensi ke SceneLoader
    
    [Header("Level Complete Sound")]
    public AudioClip levelCompleteSound;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        sceneLoader = FindObjectOfType<SceneLoader>(); // Dapatkan referensi ke SceneLoader
        HideLevelComplateMenu();
    }

    private void ShowLevelComplateMenu()
    {
        Time.timeScale = 0; // Jeda waktu permainan
        // Nonaktifkan musik latar belakang
        audioManager.StopBackgroundMusic();
        // Memainkan suara game over
        audioManager.PlaySFX(levelCompleteSound);
        gameObject.SetActive(true);
    }

    private void HideLevelComplateMenu()
    {
        Time.timeScale = 1; // Lanjutkan waktu permainan
        // Nonaktifkan objek-objek pause menu di sini
        gameObject.SetActive(false);
    }

    public void ToggleLevelComplateMenu()
    {
        ShowLevelComplateMenu();
    }

    public void LoadNextLevel(int nextLevelIndex)
    {
        sceneLoader.LoadScene(nextLevelIndex);
        audioManager.PlaySFX(audioManager.buttonClick);
    }
    
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        audioManager.PlaySFX(audioManager.buttonClick);
    }

    public void ExitToPlayMenu()
    {
        sceneLoader.LoadScene(1);
        audioManager.PlaySFX(audioManager.buttonClick);
    }
}
