using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName ="New AchievementTitle", menuName="AchievementTitle")]
public class AchievementTitle : ScriptableObject
{
    public string Title;
    public string description;
    public int score;
}
