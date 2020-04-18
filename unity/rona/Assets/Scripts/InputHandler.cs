using System;
using UnityEngine;
using UnityEngine.Events;


public class InputHandler : MonoBehaviour
{
    float inputX;
    float inputY;

    [Serializable]
    public class FireEvent : UnityEvent<float> { }
    public FireEvent onFire;

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

    public void shoot() {
        onFire.Invoke(1.0f);
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

    }
}
