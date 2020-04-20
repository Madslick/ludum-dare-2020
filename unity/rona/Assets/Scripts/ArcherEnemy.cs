using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : EnemyScript
{
    private float lastShotTime = 0;
    public float shootInterval = 1f;
    public GameObject bulletPrefab;
    public float bulletVelocityFactor = 10f;
    public float shootDistanceThreshold = 3f;

    // Update is called once per frame
    public new void Update()
    {
        base.Update();


        if (Mathf.Abs(distanceToTarget - distanceThreshold) < shootDistanceThreshold) {

            var now = Time.time;
            if (now - lastShotTime < shootInterval) {
                return;
            } else {
                lastShotTime = now;
            }

            var bullet = Instantiate(bulletPrefab);
            var rigidBody = bullet.GetComponent<Rigidbody2D>();

            rigidBody.transform.SetParent(transform.parent);
            rigidBody.velocity = bulletVelocityFactor * moveDir;
            rigidBody.transform.localPosition = transform.localPosition;

        }

    }
}
