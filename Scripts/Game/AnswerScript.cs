using System.Collections;
using UnityEngine;
using TMPro;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;
    public ItemCollector itemCollector;
    public AudioManager audioManager; // Tambahkan variabel untuk AudioManager
    public AudioClip correctAnswerSound;
    public AudioClip wrongAnswerSound;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("Jawaban Benar");
            StartCoroutine(ShowCorrectAnswerHighlight(Color.green)); 
            quizManager.correct();
            audioManager.PlaySFX(correctAnswerSound); // Memainkan suara jawaban benar
        }
        else
        {
            Debug.Log("Jawaban Salah");
            StartCoroutine(ShowCorrectAnswerHighlight(Color.red));
            itemCollector.SubtractCoins(5);

            if (itemCollector.GetCoins() < 5)
            {
                StartCoroutine(DeactivateQuizUIWithDelay(1.0f));
            }

            audioManager.PlaySFX(wrongAnswerSound); // Memainkan suara jawaban salah
        }
    }

    IEnumerator ShowCorrectAnswerHighlight(Color highlightColor)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = highlightColor;
        yield return new WaitForSeconds(1.0f);
        GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.19f, 0.19f, 0.19f);
    }

    IEnumerator DeactivateQuizUIWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        quizManager.quizUI.SetActive(false);
    }
}
