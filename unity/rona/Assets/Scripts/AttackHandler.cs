using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{

    public GameObject lazerBeamPrefab;
    private GameObject lazerBeam;
    void Start()
    {
        
    }

    public void Attack(AttackAction action) {

        switch(action.action) {
            case "Fire1":
                lazerBeamFiring(action);
                break;
            case "Fire2":
                break;
        }
    }

    public void StopAttack(AttackAction action) {
        switch(action.action) {
            case "Fire1":
                lazerBeamStop();
                break;
            case "Fire2":
                break;
        }
    }

    private void lazerBeamFiring(AttackAction action) {
        
        if (!lazerBeam) {
            lazerBeam = Instantiate(lazerBeamPrefab) as GameObject;
            Debug.Log("LazerBeam");
        }

        lazerBeam.transform.position = new Vector3(
            transform.position.x, 
            transform.position.y, 
            lazerBeam.transform.position.z
        );
        var angles = lazerBeam.transform.rotation.eulerAngles;
        angles.z = Mathf.Rad2Deg * Mathf.Atan2(action.inputY,  action.inputX);

        lazerBeam.transform.rotation = Quaternion.Euler(angles);
    }

    private void lazerBeamStop() {
        Destroy(lazerBeam);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
