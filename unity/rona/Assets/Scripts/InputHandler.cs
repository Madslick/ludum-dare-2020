using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    float inputX;
    float inputY;

    void Start()
    {

    }

    public float getInputX() {
        return inputX;
    }

    public float getInputY() {
        return inputY;
    }

    // Update is called once per frame
    void Update()
    {
        //dashPressed = Input.GetButtonDown("Dash") ? true : false;
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }
}
