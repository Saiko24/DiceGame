using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SO;

public class TownInfoDisplay : MonoBehaviour {

    public IntVariable townLevel;
    public Text townLevelText;

	// Update is called once per frame
	void Update () {
        townLevelText.text = townLevel.value.ToString();
    }
}