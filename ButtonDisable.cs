using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using UnityEngine.UI;

public class ButtonDisable : MonoBehaviour {

    public BoolVariable GenerateDone;
    public Button button;

	// Update is called once per frame
	void Update () {

        if (GenerateDone.value)
        {
            button.enabled = true;
        }
        else
        {
            button.enabled = false;
        }

	}
}
