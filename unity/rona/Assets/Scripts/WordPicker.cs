using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WordPicker : MonoBehaviour
{
    public TextAsset jsonFile;
    JsonWords words;
    Dictionary<int, string[]> wordMap;

    public Text manaObject;
    public Text healthObject;
    public Text playerText;

    [System.Serializable]
    public class HealthWordTyped : UnityEvent { }
    public HealthWordTyped healthWordTyped;

    [System.Serializable]
    public class ManaWordTyped : UnityEvent { }
    public ManaWordTyped manaWordTyped;


    void Start()
    {
        words = JsonUtility.FromJson<JsonWords>(jsonFile.text);

        wordMap = new Dictionary<int, string[]>();
        wordMap.Add(3, words.letters3);
        wordMap.Add(4, words.letters4);
        wordMap.Add(5, words.letters5);
        wordMap.Add(6, words.letters6);
        wordMap.Add(7, words.letters7);
        wordMap.Add(8, words.letters8);
        wordMap.Add(9, words.letters9);
        wordMap.Add(10, words.letters10);
        wordMap.Add(11, words.letters11);
        wordMap.Add(12, words.letters12);
        wordMap.Add(13, words.letters13);
        wordMap.Add(14, words.letters14);
        wordMap.Add(15, words.letters15);

        NewManaWord();
        NewHealthWord();
    }

    void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (playerText.text.Length != 0)
                {
                    playerText.text = playerText.text.Substring(0, playerText.text.Length - 1);
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                print("User entered their name: " + playerText.text);
                playerText.text = "";
            }
            else
            {
                playerText.text += c;
            }
        }

        if (playerText.text == manaObject.text) {
            NewManaWord();
            
        }

        if (playerText.text == healthObject.text) {
            NewHealthWord();
            
        }
    }

    public void NewManaWord() {
        manaObject.text = NewWord(4);
        playerText.text = "";
        AddMana();
    }

    public void NewHealthWord() {
        healthObject.text = NewWord(10);
        playerText.text = "";
        AddHealth();
    }


    string NewWord(int wordLength) {
        var index = Random.Range(0, wordMap[wordLength].Length);
        return wordMap[wordLength][index];
    }

    public void AddHealth() {
        healthWordTyped.Invoke();
    }

    public void AddMana() {
        manaWordTyped.Invoke();
    }
}

[System.Serializable]
class JsonWords {
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