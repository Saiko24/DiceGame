using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ DailyRewardItem")]
public class DailyRewardItem : BaseItem
{
    public PlayerCurrentParametersLibrary ParameterLibrary;
    public int changeParameterValue;

    public override void UseItem()
    {
        switch (id)
        {
            case -1:
                ParameterLibrary.PlayerCurrentParametersList[3].ParameterValue += changeParameterValue;
                break;
            case -2:
                ParameterLibrary.PlayerCurrentParametersList[4].ParameterValue += changeParameterValue;
                break;
        }
    }
}
