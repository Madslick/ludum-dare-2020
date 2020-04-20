using System;
using UnityEngine;
using UnityEngine.Events;


public class InputHandler : MonoBehaviour
{
    private string mode;
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

    void Start()
    {
        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            print(names[x].Length);
            if (names[x].Length == 19)
            {
                mode = "PS4-";
            }
            if (names[x].Length == 44)
            {
                mode = "Xbox-";
            }
        }

        Debug.Log(mode);
        // This stops the audio on startup because it is playing.
        onStopMoved.Invoke();
    }

    public void stopMoved()
    {
        onStopMoved.Invoke();
    }

    public void moved()
    {
        onMoved.Invoke();
    }

    public float getInputX()
    {
        return inputX;
    }

    public float getInputY()
    {
        return inputY;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(mode);
        //dashPressed = Input.GetButtonDown("Dash") ? true : false;
        inputX = Input.GetAxis(mode + "Horizontal");
        inputY = Input.GetAxis(mode + "Vertical");

        if (inputX != 0 || inputY != 0)
        {
            moved();
        }
        else
        {
            stopMoved();
        }

        if (Input.GetKey("escape"))
        {
            onEscapePressed.Invoke();
        }

        if (Input.GetButton(mode + "Fire1"))
        {
            var newAction = new AttackAction(Input.GetAxis(mode + "Horizontal-Rstick"), Input.GetAxis(mode + "Vertical-Rstick"), "Fire1");
            onFire.Invoke(newAction);
        }
        else if (Input.GetButtonUp(mode + "Fire1"))
        {
            var newAction = new AttackAction(0, 0, "Fire1");
            onFireStopped.Invoke(newAction);
        }
    }
}

public class AttackAction
{
    public AttackAction(float inputX, float inputY, string action)
    {
        this.inputX = inputX;
        this.inputY = inputY;
        this.action = action;
    }
    public float inputX;
    public float inputY;
    public string action;

    public override string ToString()
    {
        return "InputX: " + inputX + " - InputY: " + inputY + " - action: " + action;
    }
}