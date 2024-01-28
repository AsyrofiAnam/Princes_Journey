using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    AudioManager audioManager;
    SceneLoader sceneLoader;

    [Header("Game Over Sound")]
    public AudioClip gameOverSound;
    private AudioSource gameOverAudioSource;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameOverAudioSource = gameObject.AddComponent<AudioSource>(); // Tambahkan AudioSource baru
        HideGameOverMenu();
    }

    private void ShowGameOverMenu()
    {
        Time.timeScale = 0;
        // Nonaktifkan musik latar belakang
        audioManager.StopBackgroundMusic();
        // Memainkan suara game over
        PlayGameOverSound();
        // Aktifkan objek-objek game over menu
        gameObject.SetActive(true);
    }

    private void HideGameOverMenu()
    {
        Time.timeScale = 1;
        // Nonaktifkan objek-objek game over menu
        gameObject.SetActive(false);
        // Matikan suara game over saat menyembunyikan menu
        StopGameOverSound();
    }

    public void ToggleGameOverMenu()
    {
        ShowGameOverMenu();
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        audioManager.PlaySFX(audioManager.buttonClick);
        // Matikan suara game over saat tombol retry ditekan
        StopGameOverSound();
    }

    public void ExitToPlayMenu()
    {
        sceneLoader.LoadScene(1);
        audioManager.PlaySFX(audioManager.buttonClick);
        // Matikan suara game over saat tombol exit play menu ditekan
        StopGameOverSound();
    }

    public void ExitToMainMenu()
    {
        sceneLoader.LoadScene(0);
        audioManager.PlaySFX(audioManager.buttonClick);
        // Matikan suara game over saat tombol exit main menu ditekan
        StopGameOverSound();
    }

    private void PlayGameOverSound()
    {
        gameOverAudioSource.clip = gameOverSound;
        gameOverAudioSource.Play();
    }

    private void StopGameOverSound()
    {
        gameOverAudioSource.Stop();
    }
}
