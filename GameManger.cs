using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class GameManger : MonoBehaviour {

    public BoolVariable blockRaycast;
    public BoolVariable consolOpen;

    // Use this for initialization
    void Awake () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        blockRaycast.value = false;
        consolOpen.value = false;

        //Set screen size for Standalone
//#if UNITY_STANDALONE
        //Screen.SetResolution(420, 780, false);
       // Screen.fullScreen = false;
//#endif

    }
}
