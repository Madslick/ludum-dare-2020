using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed = 20;
    public float distanceThreshold = 0;
    public float cameraOffsetAngle;
    public int maxHP = 100;

    [HideInInspector]
    public GameObject player;

    [HideInInspector]
    public float distanceToTarget;

    [HideInInspector]
    public Vector2 moveDir;

    // Start is called before the first frame update
    public void Start() {
        player = GameController.Player;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //other.attachedRigidbody.AddForce(-0.1F * other.attachedRigidbody.velocity);
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
        if (moveDir != Vector2.zero) {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            angle += cameraOffsetAngle;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        
    }
}
