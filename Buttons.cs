using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Lean.Touch;
using SO;


public class Buttons : MonoBehaviour {

    public BoolVariable blockRaycast;
    public BoolVariable blockRaycastConsole;
    public LayerMask LayerMask;
    public UnityEvent response;

    protected virtual void OnEnable()
    {

        LeanTouch.OnFingerTap += OnFingerTap;

    }

    protected virtual void OnDisable()
    {
        LeanTouch.OnFingerTap -= OnFingerTap;

    }

    public void OnFingerTap(LeanFinger finger)
    {
        var ray = finger.GetWorldPosition(1.0f);
        var hit = Physics2D.OverlapPoint(ray, LayerMask);

        Debug.Log("hit " + hit);

        if (hit != null)
        {
            if (hit.gameObject.tag == "Button")
            {
                if (blockRaycastConsole.value == false)
                {
                    if (hit.gameObject == this.gameObject)
                    {
                        Debug.Log("Button");
                        Raise();
                    }
                }
            }
            else if (hit.gameObject.tag == "ButtonUI")
            {
                if (blockRaycast.value == false && blockRaycastConsole.value ==false)
                {
                    if (hit.gameObject == this.gameObject)
                    {
                        Debug.Log("ButtonUI");
                        Raise();
                    }
                }
            }
        }
        else
        {
        }
    }

    public void Raise()
    {
        response.Invoke();

    }
}
