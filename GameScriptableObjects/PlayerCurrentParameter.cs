using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Current Parameter", menuName = "Parameters/PlayerCurrentParameter")]
public class PlayerCurrentParameter : ScriptableObject
{
    public float currentParameterValue;

    public string currentParameterName;

    public string currentParameterDescription;

    public PlayerCurrentParameter MaxValue;

    public Sprite currentParameterSprite;

    public float ParameterValue
    {
        get
        {
            return currentParameterValue;
        }
        set
        {
            currentParameterValue = value;

            if (currentParameterValue <= 0)
            {
                currentParameterValue = 0;
            }

            if (MaxValue != null)
            {
                if (currentParameterValue >= MaxValue.currentParameterValue)
                {
                    currentParameterValue = MaxValue.currentParameterValue;
                }
            }

        }
    }


}
