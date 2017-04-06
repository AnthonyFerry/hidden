using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextualRiddle : MonoBehaviour {
    //Variables (publique commence par majuscule, privée commence par underscore)
    public string Answer;
    public Text UserAnswer;
    public bool OnTrigger = false;
    public TriggerableObject MV;
    public bool Resolved = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ((OnTrigger) && (Input.GetButtonDown("interact")) && !Resolved)
        {
            GetComponent<CineCamera>().IsActive = true;
            UIManager.Instance.KeyboardScript.Call(Answer.Length, OnResolve);
        }
		
	}

    public void OnResolve()
    {
        Debug.Log(UserAnswer.text);

        if (UserAnswer.text == Answer)
        {
            Resolved = true;
            UIManager.Instance.KeyboardScript.Close();
            GetComponent<CineCamera>().IsActive = false;
            MV.Activate();
            UIManager.Instance.HintScript.Hide();
        }
    }

    public void ShowText()
    {
        Debug.LogError("Bitule Shnaek");
        UIManager.Instance.KeyboardScript.Close();
    }

    void OnTriggerEnter(Collider col)
    {
        if (Resolved) //Si c'est résolu, quitte la fonction
            return;
        
        if (col.gameObject.name == "Player")
        {
            Debug.Log("Appuyez sur X");
            OnTrigger = true;
            UIManager.Instance.HintScript.Show("Appuyez sur $ pour activer l'énigme", Hint.X_BUTTON);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (Resolved) //Si c'est résolu, quitte la fonction
            return;

        if (col.gameObject.name == "Player")
        {
            UIManager.Instance.HintScript.Hide();
            OnTrigger = false;
        }
    }
}
