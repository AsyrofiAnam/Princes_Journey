using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("------------------- Audio Source ------------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    AudioManager audioManager;

    [Header("------------------- Audio Clip --------------------")]
    public AudioClip background;
    public AudioClip level_1;
    public AudioClip level_2;
    public AudioClip level_3;
    public AudioClip Credit_Scene;
    public AudioClip hurt;
    public AudioClip death;
    public AudioClip jump;
    public AudioClip collectibles;
    public AudioClip buttonClick; // Menambahkan suara untuk button click

    private static AudioManager instance;

    private void Start()
    {
        // Mengatur clip musik pada awal permainan
        SetBackgroundMusic(background);
        audioManager = FindObjectOfType<AudioManager>();

        // Mendaftarkan metode HandleSceneUnloaded untuk event SceneManager.sceneUnloaded
        SceneManager.sceneUnloaded += HandleSceneUnloaded;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void HandleSceneUnloaded(Scene scene)
    {
        // Mengaktifkan kembali musik background saat scene "Level1" di-unload
        if (scene.name == "Level_1" || scene.name == "Level_2" || scene.name == "Level_3" || scene.name == "Credit_Scene") // Use || instead of &
        {
            SetBackgroundMusic(background);
        }
    }

    public void StopBackgroundMusic()
    {
        musicSource.Stop(); // Menghentikan sumber suara latar belakang
    }

    public void PlayBackgroundMusic()
    {
        musicSource.Play(); // Memulai musik latar belakang
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // Fungsi untuk memainkan suara saat tombol diklik
    public void PlayButtonClickSound()
    {
        SFXSource.PlayOneShot(buttonClick);
    }

    public void OnButtonClick()
    {
        // Memainkan suara saat tombol diklik
        audioManager.PlayButtonClickSound();
    }

    // Metode untuk mengatur clip musik yang dapat dipanggil saat pindah scene
    public void SetBackgroundMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        PlayBackgroundMusic();
    }

    // Contoh penggunaan saat pindah scene ke level 1
    public void LoadLevel1()
    {
        SetBackgroundMusic(level_1);
    }
    public void LoadLevel2()
    {
        SetBackgroundMusic(level_2);
    }
    public void LoadLevel3()
    {
        SetBackgroundMusic(level_3);
    }
    public void CreditScene()
    {
        SetBackgroundMusic(Credit_Scene);
    }

    private void OnDestroy()
    {
        // Jangan lupa untuk melepaskan handler event saat obyek dihancurkan
        SceneManager.sceneUnloaded -= HandleSceneUnloaded;
    }
}
