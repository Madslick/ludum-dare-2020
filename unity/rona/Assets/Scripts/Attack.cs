using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attack : MonoBehaviour
{
    static int attackCount = 1;

    private int attackId {get; set;}
    public AttackType attackType;
    public float Dps;
    float DpsDuration{get; set;}
    public float StartingDpsDuration;
    // Start is called before the first frame update
    void Start()
    {
        attackId = attackCount;
        attackCount++;
    }

    public int GetAttackId(){
        return attackId;
    }
    public float GetDpsDuration(){
        return DpsDuration;
    }

    public void Hit() {
        if (attackType == AttackType.Enter) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
public enum AttackType {
    Enter,
    Stay, 
}