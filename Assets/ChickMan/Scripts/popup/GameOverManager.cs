using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickMan
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField] QuestionManager questionManager;
        [SerializeField] GameObject gameOverPanel;
        [SerializeField] GameObject timeOutPanel;
        [SerializeField] GameObject correctPanel;
        [SerializeField] GameObject wrongPanel;
        [SerializeField] GameObject remainingAttemptsPanel;

        private const string RemainingAttemptsKey = "RemainingAttempts";
        [SerializeField] private int remainingAttempts = 3;
        private const string LastResetDateKey = "LastResetDate";
        private void Awake()
        {

            timeOutPanel.SetActive(false);
            correctPanel.SetActive(false);
            wrongPanel.SetActive(false);
            questionManager.gameObject.SetActive(false);
        }
        private void Start()
        {
            CheckAndResetAttemptsDaily();
            remainingAttempts = PlayerPrefs.GetInt(RemainingAttemptsKey, 3);

        }


        public void ButtonAnswerTheQuestion()
        {
            if (remainingAttempts > 0)
            {
                questionManager.gameObject.SetActive(true);
                gameOverPanel.SetActive(false);
            }
            else
            {
                Debug.Log("Da het luot");
                outOfRemainingAttempt();
            }

        }

        public void ButtonViewAds()
        {
            Debug.Log("Show quang cao");
        }
        public void ShowTimeout()
        {
            questionManager.gameObject.SetActive(false);
            timeOutPanel.SetActive(true);
        }
        public void ShowCorrectPanel()
        {
            questionManager.gameObject.SetActive(false);
            correctPanel.SetActive(true);
        }
        public void ShowWrongPanel()
        {
            questionManager.gameObject.SetActive(false);
            wrongPanel.SetActive(true);
            remainingAttempts--;
            PlayerPrefs.SetInt(RemainingAttemptsKey, remainingAttempts);
            PlayerPrefs.Save();

            Debug.Log("so lan con lai: " + remainingAttempts);
        }
        public void BackHome()
        {
            gameOverPanel.SetActive(true);
            timeOutPanel.SetActive(false);
            correctPanel.SetActive(false);
            wrongPanel.SetActive(false);
            questionManager.gameObject.SetActive(false);
            remainingAttemptsPanel.SetActive(false);
        }
        private void outOfRemainingAttempt()
        {
            remainingAttemptsPanel.SetActive(true);
            gameOverPanel.SetActive(true);
        }



        private void CheckAndResetAttemptsDaily()
        {

            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");


            string lastResetDate = PlayerPrefs.GetString(LastResetDateKey, "");
            Debug.Log(lastResetDate);

            if (lastResetDate != currentDate)
            {

                PlayerPrefs.SetInt(RemainingAttemptsKey, 3);
                PlayerPrefs.SetString(LastResetDateKey, currentDate);
                PlayerPrefs.Save();


            }
        }
    }
}
