using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Status : MonoBehaviour
{
    public int vulnerabilityLayer = 10;
    public float maxHealth;
    private float currentHealth;

    public float addedHealthOnWord;

    public GameObject healthStatusBarGo;

    [Serializable]
    public class HealthChangedEvent : UnityEvent<float> { }
    public HealthChangedEvent healthChanged;

    List<DamageSource> damageSources;


    public float maxMana;
    private float currentMana;
    public float addedManaOnWord;
    public GameObject manaStatusBarGo;
    [Serializable]
    public class ManaChangedEvent : UnityEvent<float> { }
    public ManaChangedEvent manaChanged;


    private SpriteRenderer spriteRenderer;
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        damageSources = new List<DamageSource>();

        currentHealth = maxHealth;
        currentMana = 0; 
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer == vulnerabilityLayer)
        {
            Attack attack = collision.GetComponent<Attack>();
            if (GetDamageSource(attack) == null) {
                damageSources.Add(new DamageSource(attack));

                attack.Hit();
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.layer == vulnerabilityLayer)
        {
            Attack attack = collision.GetComponent<Attack>();
            if(attack.attackType == AttackType.Stay) {
                var dmgSource = GetDamageSource(attack);
                if(dmgSource == null) {
                    damageSources.Add(new DamageSource(attack));
                }
            }
        }
    }
    DamageSource GetDamageSource(Attack attack) {
        foreach (DamageSource dmgSource in damageSources)
        {
            if (attack.GetAttackId() == dmgSource.attackId) {
                return dmgSource;
            }
        }

        return null;
    }



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
    

    // Update is called once per frame
    void Update() {
        SetHealth(currentHealth % (maxHealth + 1));
        SetMana(currentMana % (maxMana + 1));

        List<DamageSource> addList = new List<DamageSource>();
        foreach(DamageSource dmgSource in damageSources) {
            var dmg =  dmgSource.handleDamage(Time.deltaTime);
            currentHealth -= dmg;
            if (dmgSource.DpsDuration > 0) {
                addList.Add(dmgSource);
            }
        }

        float percentage = currentHealth / maxHealth;
        float H, S, V;
        Color.RGBToHSV(spriteRenderer.color, out H, out S, out V);


        S = vulnerabilityLayer == 11 ? percentage : (1 - percentage);

        spriteRenderer.color = Color.HSVToRGB(H, S, V);

        damageSources = addList.FindAll(ds => true);
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}


class DamageSource {
    public int attackId;
    public float Dps;
    public float DpsDuration;
    public float StartingDpsDuration;

    public DamageSource(Attack attack) {
        this.Dps = attack.Dps;
        this.DpsDuration = attack.StartingDpsDuration;
        this.StartingDpsDuration = attack.StartingDpsDuration;
        this.attackId = attack.GetAttackId();
    }

    public float handleDamage(float deltaTime) {
        float damage = 0f;
        this.DpsDuration -= deltaTime;

        if (this.DpsDuration < 0) {
            var correctedTime = deltaTime - this.DpsDuration;
            damage = Dps * correctedTime;
        } else {
            damage = Dps * deltaTime;
        }


        return damage;
    }
}