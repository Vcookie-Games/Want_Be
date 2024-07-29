using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementsDatabase", menuName = "ScriptableObjects/AchievementsDatabase", order = 1)]
public class AchievementDatabase : ScriptableObject
{
    public List<Achievement> achievements;
    private Dictionary<string, Achievement> achievementDictionary;


    private void OnEnable()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        achievementDictionary = new Dictionary<string, Achievement>();
        foreach (var achievement in achievements)
        {
            achievementDictionary[achievement.id] = achievement;
        }
    }

    public Achievement GetAchievementById(string id)
    {
        achievementDictionary.TryGetValue(id, out Achievement achievement);
        return achievement;
    }
}
