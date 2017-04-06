using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour {

    private string _alphabetReference = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
    private int _wordlength;
    private int _wordindex = 0;

    public GameObject Key;
    public Text UserAnswer;
    public Transform KeyHolder;

    public delegate void OnResolve();
    public OnResolve Callback;

    public void InitializeKeybord()
    {
        var eventSystem = FindObjectOfType<EventSystem>();

        for (int i = 0; i < _alphabetReference.Length; i++)
        {
            var key = CreateKey("Key " + _alphabetReference[i], _alphabetReference[i].ToString());

            if (i == 0)
                eventSystem.firstSelectedGameObject = key;
        }

        var validateKey = CreateKey("Key validate", "-", Callback);
        //validateKey.GetComponent<Text>().font = Resources.Load<Font>("Arial");
        var returnKey = CreateKey("Key return", ":", RemoveLetter);
        //returnKey.GetComponent<Text>().font = Resources.Load<Font>("Arial");
    }
    
    GameObject CreateKey(string goName, string value)
    {
        var key = Instantiate(Key) as GameObject;
        key.name = goName;
        key.transform.SetParent(KeyHolder);

        var buttonText = key.GetComponentInChildren<Text>();
        buttonText.text = value;

        key.GetComponent<Button>().onClick.AddListener(() => AddLetter(value[0]));
        return key;
    }

    GameObject CreateKey(string goName, string value, OnResolve callbackFunction)
    {
        var key = Instantiate(Key) as GameObject;
        key.name = goName;
        key.transform.SetParent(KeyHolder);

        var buttonText = key.GetComponentInChildren<Text>();
        buttonText.text = value;

        key.GetComponent<Button>().onClick.AddListener(() => callbackFunction());
        return key;
    }

    public void AddLetter(char letter)
    {
        var newtext = UserAnswer.text.ToCharArray();
        newtext[_wordindex] = letter;
       
        _wordindex++;

        _wordindex = Mathf.Clamp(_wordindex, 0, _wordlength-1); // évite le if, met des limites : l'utilisateur ne pourra pas dépasser le nombre de lettres maximum

        Debug.Log(newtext.ToString());
        UserAnswer.text = new string(newtext);
        Debug.Log("index " + _wordindex);
    }

    public void RemoveLetter()
    {
        var newtext = UserAnswer.text.ToCharArray();

        if ((_wordindex == _wordlength-1) && (newtext[_wordindex] != '.'))
        {
            newtext[_wordindex] = '.';
        }
        else
        {
            _wordindex--;

            _wordindex = Mathf.Clamp(_wordindex, 0, _wordlength - 1);
            newtext[_wordindex] = '.';
        }

        UserAnswer.text = new string(newtext);
        Debug.Log("index " + _wordindex);
    }

    public void Call(int wordlength, OnResolve callbackFonction)
    {
        Callback = callbackFonction;
        InitializeKeybord();
        _wordlength = wordlength;
        Open();
    }

    void Open()
    {
        UserAnswer.text = "";
        _wordindex = 0;

        GameController.Instance.State = GameState.keyboard;
        UserAnswer.gameObject.SetActive(true);
        KeyHolder.gameObject.SetActive(true);

        for (int i = 0; i<_wordlength; i++)
        {
            UserAnswer.text += ".";
        }
    }

    public void Close()
    {
        GameController.Instance.State = GameState.running;
        UserAnswer.gameObject.SetActive(false);
        KeyHolder.gameObject.SetActive(false);

        for (int i = 0; i < KeyHolder.childCount; i++)
        {
            Destroy(KeyHolder.GetChild(i).gameObject);
        }

    }

    void Update()
    {
        if (Input.GetButtonDown("cancel"))
        {
            Close();
        }
    }
}
