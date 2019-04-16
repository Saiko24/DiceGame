using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SO;

public class HelpTipDisplay : MonoBehaviour {

    public HelpTipsLibrary tipLibrary;
    public IntVariable tipIndex;
    public Text tipName;
    public Text tipDescription;
    public int id;

	// Update is called once per frame
	void Update () {

        tipName.text = tipLibrary.tipsLibrary[id].tipName;
        tipDescription.text = tipLibrary.tipsLibrary[id].tipDescription;
	}

    public void OnEnable()
    {
        foreach (var item in tipLibrary.tipsLibrary)
        {
            if (item.id == tipIndex.value)
            {
                id = tipLibrary.tipsLibrary.IndexOf(item);
            }
        }
    }

    public void DisableHelpTip()
    {
        gameObject.SetActive(false);
    }
}
