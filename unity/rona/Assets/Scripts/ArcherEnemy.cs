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

    public int spread;
    public float angleStep;

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
            Vector2 newDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                rigidBody.velocity = bulletVelocityFactor * newDirection;
            bullet.transform.position =  new Vector3(
                transform.position.x + (transform.localScale.x * Mathf.Cos(angle)),
                transform.position.y + (transform.localScale.y * Mathf.Sin(angle)),
                rigidBody.transform.position.z
            );


            var angle2 = 0f;
            for(int i = 1; i < spread; i++) {
                bullet = Instantiate(bulletPrefab);
                rigidBody = bullet.GetComponent<Rigidbody2D>();
                angle2 = angle + (i * angleStep * Mathf.Deg2Rad);

                rigidBody.transform.SetParent(transform.parent);
                newDirection = new Vector2(Mathf.Cos(angle2), Mathf.Sin(angle2));
                rigidBody.velocity = bulletVelocityFactor * newDirection;
                bullet.transform.position = new Vector3(
                    transform.position.x + (transform.localScale.x * Mathf.Cos(angle2)),
                    transform.position.y + (transform.localScale.y * Mathf.Sin(angle2)),
                    rigidBody.transform.position.z
                );

                Debug.Log("My X: " + transform.position.x + " bullet X " + bullet.transform.position.x);

                Debug.Log("My Y: " + transform.position.y + " bullet Y " + bullet.transform.position.y);


                bullet = Instantiate(bulletPrefab);
                rigidBody = bullet.GetComponent<Rigidbody2D>();
                angle2 = angle - (i * angleStep * Mathf.Deg2Rad);

                rigidBody.transform.SetParent(transform.parent);
                newDirection = new Vector2(Mathf.Cos(angle2), Mathf.Sin(angle2));
                rigidBody.velocity = bulletVelocityFactor * newDirection;
                bullet.transform.position =  new Vector3(
                    transform.position.x + (transform.localScale.x * Mathf.Cos(angle2)),
                    transform.position.y + (transform.localScale.y * Mathf.Sin(angle2)),
                    rigidBody.transform.position.z
                );
            }
            

        }

    }
}
