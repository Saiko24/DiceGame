using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;


[CreateAssetMenu(menuName = "HelpTips/Tip")]
public class HelpTip : ScriptableObject
{
    public int id;
    public string tipName;
    public string tipDescription;
}
