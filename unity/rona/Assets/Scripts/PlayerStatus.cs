using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class PlayerStatus : NetworkBehaviour
{

    [SyncVar]
    public float maxHealth;
    [SyncVar]
    private float currentHealth;

    public float addedHealthOnWord;

    public GameObject healthStatusBarGo;

    [Serializable]
    public class HealthChangedEvent : UnityEvent<float> { }
    public HealthChangedEvent healthChanged;


    public override void OnStartAuthority() {
        base.OnStartAuthority();

        Debug.Log("I TOOK AUTHORITY");
    }

    public float maxMana;
    private float currentMana;
    public float addedManaOnWord;
    public GameObject manaStatusBarGo;
    [Serializable]
    public class ManaChangedEvent : UnityEvent<float> { }
    public ManaChangedEvent manaChanged;

    public void HealthTyped() {
        currentHealth += addedHealthOnWord;
        currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
    }

    public void SetHealth(float value) {
        healthChanged.Invoke(value);
    }

    public void ManaTyped() {
        currentMana += addedManaOnWord;
        currentMana = currentMana > maxMana ? maxMana : currentMana;
    }


    public void SetMana(float value) {
        manaChanged.Invoke(value);
    }

    // Start is called before the first frame update
    void Start() {
        if (healthStatusBarGo) {
            healthStatusBarGo.GetComponent<UIStatusBar>().maxValue = maxHealth;
        } else {
            Debug.LogWarning("Please set the health status bar for the play");
        }

        if (manaStatusBarGo) {
            manaStatusBarGo.GetComponent<UIStatusBar>().maxValue = maxMana;
        } else {
            Debug.LogWarning("Please set the mana status bar for the play");
        }
    }

    // Update is called once per frame
    void Update() {
        SetHealth(currentHealth % (maxHealth + 1));
        SetMana(currentMana % (maxMana + 1));
    }
}
