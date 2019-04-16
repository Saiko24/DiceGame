using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class RaycastBlock : MonoBehaviour {

    public GameObject raycastBlockPanel;
    public BoolVariable raycastBlockBool;
    public bool consoleCall;

    void Awake()
    {
        raycastBlockBool.value = false;
    }

    public void consoleRaycastBlockDelay()
    {
        StartCoroutine("delay");
    }

    public void raycastBlockDelay()
    {
        StartCoroutine("delay");
    }

    IEnumerator delay()
    {
        if (raycastBlockBool.value)
        {
            Debug.Log("test");
            yield return new WaitForSeconds(0.01f);
            raycastBlockPanel.SetActive(false);
            raycastBlockBool.value = false;
            consoleCall = false;
        }
        else
        {
            raycastBlockPanel.SetActive(true);
            raycastBlockBool.value = true;
        }
    }
}
