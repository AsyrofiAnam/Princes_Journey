using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    AudioManager audioManager;
    SceneLoader sceneLoader; // Tambahkan variabel untuk menyimpan referensi ke SceneLoader

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        sceneLoader = FindObjectOfType<SceneLoader>(); // Dapatkan referensi ke SceneLoader
        HidePauseMenu();
    }

    private void ShowPauseMenu()
    {
        Time.timeScale = 0; // Jeda waktu permainan
        // Aktifkan objek-objek pause menu di sini (tombol resume, restart, exit)
        gameObject.SetActive(true);
    }

    private void HidePauseMenu()
    {
        Time.timeScale = 1; // Lanjutkan waktu permainan
        // Nonaktifkan objek-objek pause menu di sini
        gameObject.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        if (Time.timeScale == 0)
        {
            HidePauseMenu();
        }
        else
        {
            ShowPauseMenu();
        }
    }

    public void PauseGame()
    {
        TogglePauseMenu();
        audioManager.PlaySFX(audioManager.buttonClick);

    }

    public void ResumeGame()
    {
        HidePauseMenu();
        audioManager.PlaySFX(audioManager.buttonClick);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        audioManager.PlaySFX(audioManager.buttonClick);
    }

    public void ExitToPlayMenu()
    {
        sceneLoader.LoadScene(1);
        audioManager.PlaySFX(audioManager.buttonClick);
    }
    public void GotoCreditScene()
    {
        sceneLoader.LoadScene(5);
        audioManager.PlaySFX(audioManager.buttonClick);
    }

    // ...

    // Tampilkan pause menu saat tombol pause ditekan
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         TogglePauseMenu();
    //     }
    // }
}
