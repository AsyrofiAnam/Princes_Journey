using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswer> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public TextMeshProUGUI QuestionTxt;
    public GameObject quizUI; // Public variable for the quiz UI GameObject

    private bool isPlayerTouching = false;

    private void Start()
    {
        // Disable the quiz UI at the start
        quizUI.SetActive(false);

        generateQuestion();
    }

    private void Update()
    {
        // Check if the player is touching and pressed the W key
        if (isPlayerTouching && Input.GetKeyDown(KeyCode.W))
        {
            // Show the quiz UI
            quizUI.SetActive(true);
        }
    }

    public void correct()
    {
        QnA.RemoveAt(currentQuestion);

        // Check if there are more questions
        if (QnA.Count > 0)
        {
            StartCoroutine(NextQuestionWithDelay(1.0f)); // Wait for 1 second before generating the next question
        }
        else
        {
            StartCoroutine(DeactivateQuizUIWithDelay(1.0f)); // Wait for 1 second before deactivating the quiz UI
        }
    }

    public void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    IEnumerator NextQuestionWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        generateQuestion();
    }

    public IEnumerator DeactivateQuizUIWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        quizUI.SetActive(false); // Deactivate the quiz UI
    }

    void generateQuestion()
    {
        currentQuestion = Random.Range(0, QnA.Count);

        QuestionTxt.text = QnA[currentQuestion].Question;
        SetAnswers();
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Set the flag indicating that the player is touching
            isPlayerTouching = true;
        }
    }

    // OnTriggerExit2D is called when the Collider2D other has stopped touching the trigger
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Reset the flag when the player is no longer touching
            isPlayerTouching = false;

            // Hide the quiz UI when the player is no longer touching the collider
            quizUI.SetActive(false);
        }
    }
}
