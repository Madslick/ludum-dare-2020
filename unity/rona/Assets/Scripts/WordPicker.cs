using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class WordPicker : MonoBehaviour
{
    public TextAsset jsonFile;
    
    public JsonWords words;
    void Start()
    {
        words = JsonUtility.FromJson<JsonWords>(jsonFile.text);

        Debug.Log(words.letters9[10]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

[System.Serializable]
public class JsonWords {
    public string[] letters3;
    public string[] letters4;
    public string[] letters5;
    public string[] letters6;
    public string[] letters7;
    public string[] letters8;
    public string[] letters9;
    public string[] letters10;
    public string[] letters11;
    public string[] letters12;
    public string[] letters13;
    public string[] letters14;
    public string[] letters15;
}