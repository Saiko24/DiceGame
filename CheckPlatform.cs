using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class CheckPlatform : MonoBehaviour {

    public StringVariable Platform;

    // Use this for initialization
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Platform.value = "Android";
        }
        else if ( Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Platform.value = "IOS";
        }
        else if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            Platform.value = "PC";
        }
    }
}
