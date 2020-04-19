using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public float speed = 20;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = new Vector2();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            velocity.y = speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            velocity.y = -speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity.x = -speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocity.x = speed * Time.deltaTime;
        }

        rigidBody.velocity = velocity;
    }
}
