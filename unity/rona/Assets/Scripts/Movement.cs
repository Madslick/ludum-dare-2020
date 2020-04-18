using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    InputHandler input;
    Rigidbody2D rigidBody;
    public float velocity;

    void Start()
    {
        input = GetComponent<InputHandler>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        var inputY = input.getInputY();
        var inputX = input.getInputX();
        if (inputX == 0 && inputY == 0){
            rigidBody.velocity = new Vector2(0, 0);
            return;
        }
        var angle = Mathf.Atan2(input.getInputY(),  input.getInputX());
        var velX = Mathf.Cos(angle) * velocity;
        var velY = Mathf.Sin(angle) * velocity;


        //Debug.Log(angle);
        //Debug.Log("VelY: " + input.getInputY() + " VelX: " + input.getInputX());

        rigidBody.velocity = new Vector2(velX, velY);
        //Time.deltaTime;
        
    }
}
