using System;
using UnityEngine;
using UnityEngine.Events;


public class InputHandler : MonoBehaviour
{
    float inputX;
    float inputY;

    [Serializable]
    public class EscapePressedEvent : UnityEvent { }
    public EscapePressedEvent onEscapePressed;

    [Serializable]
    public class FireEvent : UnityEvent<AttackAction> { }
    public FireEvent onFire;

    [Serializable]
    public class FireStoppedEvent : UnityEvent<AttackAction> { }
    public FireStoppedEvent onFireStopped;


    [Serializable]
    public class MovedEvent : UnityEvent { }
    public MovedEvent onMoved;

    [Serializable]
    public class StopMovedEvent : UnityEvent { }
    public StopMovedEvent onStopMoved;

    void Start() {
        // This stops the audio on startup because it is playing.
        onStopMoved.Invoke();
    }

    public void stopMoved() {
        onStopMoved.Invoke();
    }

    public void moved() {
        onMoved.Invoke();
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

        if (inputX != 0 || inputY != 0) {
            moved();
        } else {
            stopMoved();
        }

        if (Input.GetKey("escape")) {
            onEscapePressed.Invoke();
        }

        if (Input.GetButton("Fire1")) {
            Debug.Log(Input.GetAxis("Vertical-Rstick"));
            var newAction = new AttackAction(Input.GetAxis("Horizontal-Rstick"), Input.GetAxis("Vertical-Rstick"), "Fire1");
            onFire.Invoke(newAction);
        } else if(Input.GetButtonUp("Fire1")) {
            var newAction = new AttackAction(0, 0, "Fire1");
            onFireStopped.Invoke(newAction);
        }
    }
}

public class AttackAction {
    public AttackAction(float inputX, float inputY, string action){
        this.inputX = inputX;
        this.inputY = inputY;
        this.action = action;
    }
    public float inputX;
    public float inputY;
    public string action;

    public override string ToString(){
        return "InputX: " + inputX + " - InputY: " + inputY + " - action: " + action;
    }  
}