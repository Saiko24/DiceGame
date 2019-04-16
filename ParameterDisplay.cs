using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SO;

public class ParameterDisplay : MonoBehaviour {

    public PlayerCurrentParameter Parameter;
    public Text ParameterText;
    public Image ParameterImage;

    // Use this for initialization
    void Start () {

        if (Parameter.currentParameterSprite != null)
        {
            ParameterImage.sprite = Parameter.currentParameterSprite;
        }

        if (ParameterText.text != null)
        {
            ParameterText.text = Parameter.ParameterValue.ToString();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (ParameterText.text != null)
        {
            if (Parameter.MaxValue == null)
            {
                ParameterText.text = Parameter.ParameterValue.ToString();
            }
            else
            {
                ParameterText.text = string.Format("{0}/{1}", Parameter.ParameterValue, Parameter.MaxValue.ParameterValue);
            }
        }
    }
}
