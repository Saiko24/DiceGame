using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Library", menuName = "Quests/Quest Library")]
public class QuestLibrary : ScriptableObject
{
    public List<Quest> questsList;
}