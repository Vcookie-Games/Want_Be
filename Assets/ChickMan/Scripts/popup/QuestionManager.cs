using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ChickMan;
using UnityEditor;
using UnityEngine.UI;


namespace ChickMan
{
    public class QuestionManager : MonoBehaviour
    {
        [SerializeField] GameOverManager gameOverManager;
        [Space]

        public QuestionsData Questions;
        [Space]
        public TextMeshProUGUI questionText;
        [Space]
        public Button answerButton1;
        public Button answerButton2;
        public Button answerButton3;
        public Button answerButton4;
        [Space]
        [SerializeField] bool playerSlected = false;
        [SerializeField] bool isCorrect = false;

        private Question currentQuestion;
        [Space]
        public Slider timeSlider;


        void Start()
        {
            currentQuestion = RandomQuestion();
            ShowQuestions();
            setTimeSlider(10);
        }

        // Update is called once per frame
        void Update()
        {
            updateColorSlider();
            if (playerSlected && gameOverManager!=null)
            {
                StartCoroutine(CountdownCoroutine(1f, () =>
                {

                    if (isCorrect)
                    {
                        Debug.Log("tra loi dung");
                        ResetQuestion();
                        gameOverManager.ShowCorrectPanel();
                    }
                    else
                    {
                        Debug.Log("tra loi sai");
                        ResetQuestion();
                        gameOverManager.ShowWrongPanel();
                        
                    }


    ;
                }));
            }
            if (timeSlider.value <= 0 && gameOverManager != null && !playerSlected)
            {
                Debug.Log("Time out!");
                ResetQuestion();
                gameOverManager.ShowTimeout();
            }
        }
        private void ShowQuestions()
        {
            if (currentQuestion != null)
            {

                questionText.text = currentQuestion.questionText;

                showAndRandomAnswers();

                ResetButton();
            }
            else
            {
                Debug.LogWarning("No question found!");
            }
        }

        public void Answer1()
        {
            CheckAnswer(0, answerButton1);
        }
        public void Answer2()
        {
            CheckAnswer(1, answerButton2);
        }

        public void Answer3()
        {
            CheckAnswer(2, answerButton3);
        }

        public void Answer4()
        {
            CheckAnswer(3, answerButton4);
        }

        private Question RandomQuestion()
        {
            if (Questions.questions != null && Questions.questions.Length > 0)
            {
                int questionNumber = Random.Range(0, Questions.questions.Length);
                return Questions.questions[questionNumber];
            }

            Debug.LogWarning("No questions available in the dataset!");
            return null;
        }

        private void CheckAnswer(int answerIndex, Button answerButton)
        {
            if (currentQuestion != null)
            {
                if (answerIndex >= 0 && answerIndex < currentQuestion.answers.Count)
                {
                    bool isCorrect = currentQuestion.answers[answerIndex].isCorrect;
                    if (isCorrect)
                    {
                        Debug.Log("Correct answer!");
                        answerButton.image.color = Color.green;


                    }
                    else
                    {
                        Debug.Log("Wrong answer!");
                        answerButton.image.color = Color.red;

                    }
                    this.isCorrect = isCorrect;
                    playerSlected = true;
                    timeSlider.gameObject.SetActive(false);
                }
            }
            DisableRemainingButtons(answerButton);
        }
        private void ResetButton()
        {

            answerButton1.gameObject.SetActive(true);
            answerButton2.gameObject.SetActive(true);
            answerButton3.gameObject.SetActive(true);
            answerButton4.gameObject.SetActive(true);


            answerButton1.image.color = Color.white;
            answerButton2.image.color = Color.white;
            answerButton3.image.color = Color.white;
            answerButton4.image.color = Color.white;

            answerButton1.interactable = true;
            answerButton2.interactable = true;
            answerButton3.interactable = true;
            answerButton4.interactable = true;
        }

        private void setTimeSlider(int max)
        {
            timeSlider.gameObject.SetActive(true);
            timeSlider.maxValue = max;
            timeSlider.value = max;
        }
        private void updateColorSlider()
        {
            updateSlider();
            Image fill = timeSlider.transform.Find("Fill Area/Fill").GetComponent<Image>();
            if (timeSlider.value <= 3)
            {
                fill.color = Color.red;
            }
            else if (timeSlider.value <= 6)
            {
                fill.color = Color.yellow;
            }
            else
            {
                fill.color = Color.green;
            }
        }

        private void updateSlider()
        {
            float value = timeSlider.value;
            value -= Time.deltaTime;
            timeSlider.value = value;
        }
        private IEnumerator CountdownCoroutine(float time, System.Action onComplete = null)
        {

            yield return new WaitForSeconds(time);

            if (onComplete != null)
            {
                onComplete.Invoke();
            }
        }
        private void DisableRemainingButtons(Button clickedButton)
        {
            answerButton1.interactable = false;
            answerButton2.interactable = false;
            answerButton3.interactable = false;
            answerButton4.interactable = false;

            if (clickedButton != answerButton1) answerButton1.gameObject.SetActive(false);
            if (clickedButton != answerButton2) answerButton2.gameObject.SetActive(false);
            if (clickedButton != answerButton3) answerButton3.gameObject.SetActive(false);
            if (clickedButton != answerButton4) answerButton4.gameObject.SetActive(false);

        }

        private void ResetQuestion()
        {
            setTimeSlider(10);
            currentQuestion = RandomQuestion();
            ShowQuestions();
            playerSlected = false;
            isCorrect = false;
        }
        private void showAndRandomAnswers()
        {
            List<int> indices = new List<int> { 0, 1, 2, 3 };


            for (int i = 0; i < indices.Count; i++)
            {
                int randomIndex = Random.Range(i, indices.Count);
                int temp = indices[i];
                indices[i] = indices[randomIndex];
                indices[randomIndex] = temp;
            }

            answerButton1.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[indices[0]].answer;
            answerButton2.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[indices[1]].answer;
            answerButton3.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[indices[2]].answer;
            answerButton4.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[indices[3]].answer;
        }
    }
}
