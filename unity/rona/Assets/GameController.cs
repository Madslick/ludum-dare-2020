﻿/*
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

    [SerializeField]
    GameObject _player;

    //called immediately when script is active
    void Awake()
    {
        if (_player != null)
        {
            GameController.Player = _player;
        }
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