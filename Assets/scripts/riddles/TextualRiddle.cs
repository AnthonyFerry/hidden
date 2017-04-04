﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextualRiddle : MonoBehaviour {
    //Variables (publique commence par majuscule, privée commence par underscore)
    public string Answer;
    public Text UserAnswer;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnResolve()
    {
        Debug.Log(UserAnswer.text);
    }

    public void ShowText()
    {
        Debug.LogError("Bitule Shnaek");
        UIManager.Instance.KeyboardScript.Close();
    }

    void OnTriggerEnter(Collider col)
    {
        UIManager.Instance.KeyboardScript.Call(() => ShowText());
    }
}
