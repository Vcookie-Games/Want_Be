using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickMan
{
    [System.Serializable]
    public class Answer
    {
        public string answer;
        public bool isCorrect;

    }

    [System.Serializable]
    public class Question
    {
        public string questionText;
        public List<Answer> answers = new List<Answer> 
    {
        new Answer(),
        new Answer(),
        new Answer(),
        new Answer()
    };
        public void CheckAnswerList()
        {
            if (answers.Count > 4)
            {
                Debug.LogWarning("Maximum 4 answers!");
                answers.RemoveAt(answers.Count - 1);
            }
            else if (answers.Count < 4)
            {
                Debug.LogWarning("4 answers reuired!");
                answers.Add(new Answer());
            }

            int correctCount = 0;
            foreach (var answer in answers)
            {

                if (answer.isCorrect)
                {
                    correctCount++;
                    if (correctCount > 1)
                    {
                        answer.isCorrect = false;
                    }
                }
                
                if (answer.answer == "")
                {
                    Debug.LogWarning("Missing answer, pls check again");
                }
            }
            if (correctCount <= 0)
            {
                Debug.LogWarning("Missing correct answer, pls check again");
            }

            if (questionText == "")
            {
                    Debug.LogWarning("Missing question, pls check again");
            }
        }
    }

    [CreateAssetMenu(fileName = "QuestionsData", menuName = "Game Data/QuestionsData")]
    public class QuestionsData : ScriptableObject
    {
        public Question[] questions;
        private void OnValidate()
        {
            if (questions != null)
            {
                foreach (var question in questions)
                {
                    question.CheckAnswerList();
                }
            }
        }
    }
}
