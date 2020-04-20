using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed = 20;
    public float distanceThreshold = 0;
    public float cameraOffsetAngle;
    public int maxHP = 100;
    private float currentHp;

    [HideInInspector]
    public GameObject player;

    [HideInInspector]
    public float distanceToTarget;

    [HideInInspector]
    public Vector2 moveDir;
    List<DamageSource> damageSources;

    // Start is called before the first frame update
    void Start()
    {
        damageSources = new List<DamageSource>();
        currentHp = maxHP;
        player = GameController.Player;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer == 10)
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
        if (collision.gameObject.layer == 10)
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
    // Update is called once per frame
    public void Update() {

        // Get new position based on move speed
        Vector2 movePos = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        // Get angle before setting position
        moveDir = new Vector2(movePos.x - transform.position.x, movePos.y - transform.position.y);

        distanceToTarget = Vector2.Distance(player.transform.position, transform.position);
        if (distanceToTarget > distanceThreshold) {
            //set the position
            transform.position = movePos;
        }

        //set rotation
        if (moveDir != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            angle += cameraOffsetAngle;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        List<DamageSource> addList = new List<DamageSource>();
        foreach(DamageSource dmgSource in damageSources) {
            currentHp -= dmgSource.handleDamage(Time.deltaTime);
            if (dmgSource.DpsDuration > 0) {
                addList.Add(dmgSource);
            }
        }

        damageSources = addList;

        Debug.Log(currentHp);
        if (currentHp <= 0) {
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
        this.DpsDuration = attack.GetDpsDuration();
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