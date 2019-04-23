using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

[CreateAssetMenu(menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public int questId;
    public Sprite rewardIcon;
    public int rewardValue;
    public PlayerCurrentParameter rewardCurrcency;
    public string questDescription;
    public IntVariable questProgress;
    public int maxQuestProgress;

}
