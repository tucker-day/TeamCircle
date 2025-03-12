using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName ="New AchievementTitle", menuName="AchievementTitle")]
public class Title : ScriptableObject
{
    public string AchievementTitle;
    public string description;
    public int score;
}
