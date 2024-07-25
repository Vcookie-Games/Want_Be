using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementsDatabase", menuName = "ScriptableObjects/AchievementsDatabase", order = 1)]
public class AchievementDatabase : ScriptableObject
{
    public List<Achievement> achievements;
}
