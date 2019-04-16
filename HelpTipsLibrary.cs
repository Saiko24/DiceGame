using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

[CreateAssetMenu(menuName = "HelpTips/TipsLibrary")]
public class HelpTipsLibrary : ScriptableObject
{
   public List<HelpTip> tipsLibrary;
}
