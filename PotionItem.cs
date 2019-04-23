using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ Potion")]
public class PotionItem : BaseItem
{
    public PlayerCurrentParametersLibrary ParameterLibrary;
    public int changeParameterValue;


    public override void UseItem()
    {
        switch (id)
        {
            case 0:
                ParameterLibrary.PlayerCurrentParametersList[0].ParameterValue += changeParameterValue;
                break;
            case 1:
                ParameterLibrary.PlayerCurrentParametersList[1].ParameterValue += changeParameterValue;
                break;

        }
    }
}
