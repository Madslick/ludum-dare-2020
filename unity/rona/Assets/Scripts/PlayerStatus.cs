using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{

    public float maxHealth;

    public GameObject healthStatusBarGo;

    [Serializable]
    public class HealthChangedEvent : UnityEvent<float> { }
    public HealthChangedEvent healthChanged;


    public void SetHealth(float value) {
        healthChanged.Invoke(value);
    }
    

    // Start is called before the first frame update
    void Start() {
        healthStatusBarGo.GetComponent<UIStatusBar>().maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update() {
        SetHealth(Time.time % maxHealth);
    }
}
