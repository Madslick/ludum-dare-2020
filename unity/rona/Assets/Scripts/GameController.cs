/*
 GameController.cs: script to hold Game data in. I'm sure everyone has some form of this and we will need to do some 
 merging

 Harjit: seemed inefficient for enemies to use GameObject.Find() to find the player 
 every time they spawn so creating a Global variable for it and initializing it
 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameObject Player;
    public static float CameraOffsetAngle;

    public static double X, Y, W, H;

    public GameObject _player;

    public double x, y, w, h;

    [SerializeField]
    float _cameraOffsetAngle = 90;
    //called immediately when script is active
    void Awake()
    {
        if (_player != null)
        {
            GameController.Player = _player;
        }

        GameController.CameraOffsetAngle = _cameraOffsetAngle;

        GameController.X = x;
        GameController.Y = y;
        GameController.W = w;
        GameController.H = h;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
