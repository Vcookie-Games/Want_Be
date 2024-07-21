using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Score_Lst_obj
{
    public List<int> ScoreList;
    public Score_Lst_obj(List<int> ScoreList)
    { this.ScoreList = ScoreList; }

}

public class Data_Game : MonoBehaviour
{
    private static Data_Game instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public List<int> Lst_score;
    public void addCore(int score)
    {
        Lst_score.Add(score);
    }
    public int High_Score()
    {
        return Lst_score[0];
    }
    public void SaveScoresToJson()
    {
        // Check if the list is empty
        if (Lst_score == null || Lst_score.Count == 0)
        {
            Debug.LogWarning("List of scores is null or empty.");
            return;
        }

        Score_Lst_obj listScore = new Score_Lst_obj(Lst_score);
        string json = JsonUtility.ToJson(listScore, true);

        string path = Path.Combine(Application.persistentDataPath, "scores.json");
        File.WriteAllText(path, json);

        Debug.Log("JSON file saved at: " + path);
    }
    public void LoadScoresformjson()
    {
        string path = Path.Combine(Application.persistentDataPath, "scores.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Score_Lst_obj listScore = JsonUtility.FromJson<Score_Lst_obj>(json);
            Lst_score = listScore.ScoreList;
            Debug.Log("Scores loaded from JSON file.");
        }
        else
        {
            Lst_score = new List<int> { 0};
        }
    }
}
