using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using System.IO;

public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem instance { get; private set; }


    [SerializeField] private AchievementDatabase achievementDatabase;
    [SerializeField] private GameObject achievementPopupPrefab;
    [SerializeField] private Transform popupParent;
    [SerializeField] private float popupDuration = 2f;


    private void Awake()
    {

        if (instance == null || instance != this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void UnlockAchievement(int id)
    {
        Achievement achievement = achievementDatabase.achievements.Find(a => a.id == Convert.ToString(id));
        if (achievement.IsCompleted) return;
        achievement.IsCompleted = true;
        SaveData(achievement);
        Debug.Log($" completed achievement {achievement.name}");
        StartCoroutine(ShowAchievementPopup(achievement));
    }

    private IEnumerator ShowAchievementPopup(Achievement achievement)
    {
        GameObject popup = Instantiate(achievementPopupPrefab, popupParent);
        TextMeshProUGUI popupText = popup.GetComponentInChildren<TextMeshProUGUI>();
        popupText.text = $"completed {achievement.name}";

        yield return new WaitForSeconds(popupDuration);

        Destroy(popup);
    }
    private void SaveData(Achievement achievement)
    {
        string json = JsonUtility.ToJson(achievement);
        string path = Application.persistentDataPath + $"/{achievement.name}Achievement.json";
        File.WriteAllText(path, json);

    }

    private void ReadData(Achievement achievement)
    {
        string path = Application.persistentDataPath + $"/{achievement.name}Achievement.json;";
        if (File.Exists(path))
        {
            string jsonFromFile = File.ReadAllText(path);
            achievement = JsonUtility.FromJson<Achievement>(jsonFromFile);
        }
    }
}


