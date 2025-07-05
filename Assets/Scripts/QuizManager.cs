using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public BackgroundAudioManager audioManager;

    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswerIndex;
    }

    public Question[] questions;

    public GameObject quizPanel;
    public GameObject mainMenuPanel;
    public GameObject quizHomeButton;

    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI[] answerButtonTexts;
    public TextMeshProUGUI resultText;

    private int currentQuestion = 0;
    private int score = 0;

    void Start()
    {
        quizPanel.SetActive(false);
        resultText.gameObject.SetActive(false);
        quizHomeButton.SetActive(false);
    }

    public void StartQuiz()
    {
        mainMenuPanel.SetActive(false);
        quizPanel.SetActive(true);
        quizHomeButton.SetActive(true);

        currentQuestion = 0;
        score = 0;

        resultText.gameObject.SetActive(false);

        ShowQuestion();
    }

    public void ShowQuestion()
    {
        Question q = questions[currentQuestion];
        questionText.text = q.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtonTexts[i].text = q.answers[i];
            int index = i; // local copy to avoid closure issue
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
            answerButtons[i].interactable = true; // make sure it's enabled
        }
    }

    public void OnAnswerSelected(int index)
    {
        Question q = questions[currentQuestion];

        bool isCorrect = index == q.correctAnswerIndex;

        if (isCorrect)
        {
            Debug.Log("Correct!");
            score++;
            audioManager.PlayFeedbackSound(true);
        }
        else
        {
            Debug.Log("Wrong!");
            audioManager.PlayFeedbackSound(false);
        }

        StartCoroutine(FlashButtonAndContinue(index, isCorrect));
    }

    private IEnumerator FlashButtonAndContinue(int buttonIndex, bool isCorrect)
    {
        // Disable all buttons
        foreach (var btn in answerButtons)
        {
            btn.interactable = false;
        }

        // Get the Image component of the clicked button
        var buttonImage = answerButtons[buttonIndex].GetComponent<Image>();

        // Save original color
        Color originalColor = buttonImage.color;

        // Set flash color
        Color flashColor = isCorrect ? Color.green : Color.red;
        buttonImage.color = flashColor;

        // Wait a moment to show feedback
        yield return new WaitForSeconds(1.2f);

        // Restore the original color
        buttonImage.color = originalColor;

        currentQuestion++;
        if (currentQuestion < questions.Length)
        {
            ShowQuestion();
        }
        else
        {
            ShowResult();
        }
    }

    public void ShowResult()
    {
        // Hide question and answers
        questionText.text = "";
        foreach (var btn in answerButtons)
        {
            btn.gameObject.SetActive(false);
        }

        // Show result
        resultText.gameObject.SetActive(true);
        resultText.text = $"You got {score} out of {questions.Length} correct!";
    }

    public void ExitQuiz()
    {
        // Reset answer buttons
        foreach (var btn in answerButtons)
        {
            btn.gameObject.SetActive(true);
        }

        quizPanel.SetActive(false);
        quizHomeButton.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
