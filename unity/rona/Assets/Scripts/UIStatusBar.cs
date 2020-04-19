using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class UIStatusBar : NetworkBehaviour
{

    public enum StatusBarType {
        Horizontal,
        Vertical, 
    }

    public StatusBarType layoutType;

    public RectTransform bar;

    [SyncVar]
    public float maxValue;
    private float value;

    // Start is called before the first frame update
    void Start() {
        if (maxValue == 0) {
            maxValue = 1;
        }
        switch(layoutType) {
            case StatusBarType.Horizontal:
                bar.pivot = new Vector2(0f, 0.5f);
                break;
            case StatusBarType.Vertical:
                bar.pivot = new Vector2(0.5f, 0f);
                break;
        }
    }


    [Command]
    public void CmdSetValue(float value) {
        CmdSetPercent(value / maxValue);
    }


    /// <summary>
    /// Pass a value between 0 and 1 to set the height of the indicator bar.
    /// </summary>
    /// <param name="percent"></param>
    [Command]
    public void CmdSetPercent(float percent) {
        switch(layoutType) {
            case StatusBarType.Horizontal:
                bar.localScale = new Vector3(percent, 1f, 1f);
                break;
            case StatusBarType.Vertical:
                bar.localScale = new Vector3(1f, percent, 1f);
                break;
        }
    }

}
