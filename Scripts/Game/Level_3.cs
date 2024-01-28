using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_3 : MonoBehaviour
{
    AudioManager audioManager;
    public PauseMenu pauseMenu;
    public GameOver gameOver;
    public Level_Complate level_Complate;
    SceneLoader sceneLoader;
    public ItemCollector itemCollector;
    public QuizManager quizManager;

    public float delayDuration = 1f;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        gameOver = FindObjectOfType<GameOver>();
        level_Complate = FindObjectOfType<Level_Complate>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        itemCollector = FindObjectOfType<ItemCollector>();
        quizManager = FindObjectOfType<QuizManager>();
    }

    private void Start()
    {
        audioManager.StopBackgroundMusic();
        audioManager.LoadLevel3();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            pauseMenu.TogglePauseMenu();
        }

        if (HasPlayerDied() || HasCoinsBelowZero())
        {
            StartCoroutine(DelayBeforeGameOver());
        }
    }

    private bool HasPlayerDied()
    {
        return HealthManager.health == 0;
    }

    private bool HasCoinsBelowZero()
    {
        return itemCollector.GetCoins() < 5;
    }

    private IEnumerator DelayBeforeGameOver()
    {
        yield return new WaitForSeconds(delayDuration);
        gameOver.ToggleGameOverMenu();
    }

    private IEnumerator DeactivateQuizAndShowLevelComplete()
    {
        yield return new WaitForSeconds(delayDuration);
        quizManager.StartCoroutine(quizManager.DeactivateQuizUIWithDelay(1.0f));
        level_Complate.ToggleLevelComplateMenu();
    }
}
