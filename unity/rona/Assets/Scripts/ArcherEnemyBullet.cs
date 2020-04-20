using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemyBullet : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision) {
        // Enemy layer = 8.
        if (collision.gameObject.layer != 8) {
            Destroy(gameObject);
        }
    }


}
