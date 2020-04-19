using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 20;

    [SerializeField]
    float cameraOffsetAngle = 90;

    GameObject player;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameController.Player;
    }

    // Update is called once per frame
    void Update()
    {
        //get new position based on move speed
        Vector2 movePos = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        //get angle before setting position
        Vector2 moveDir = new Vector2(movePos.x - transform.position.x, movePos.y - transform.position.y);
        //set the position
        transform.position = movePos;

        //set rotation
        if (moveDir != Vector2.zero) {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            angle += cameraOffsetAngle;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        
    }
}
