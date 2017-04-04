using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour {

    private string _alphabetReference = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
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
            var key = Instantiate(Key) as GameObject;
            key.name = "Key " + _alphabetReference[i];
            key.transform.SetParent(KeyHolder);

            var buttonText = key.GetComponentInChildren<Text>();
            buttonText.text = _alphabetReference[i].ToString();

            key.GetComponent<Button>().onClick.AddListener(() => AddLetter(buttonText.text[0]));

            if (i == 0)
                eventSystem.firstSelectedGameObject = key;
        }

        var key2 = Instantiate(Key) as GameObject;
        key2.name = "Key validate";
        key2.transform.SetParent(KeyHolder);

        var buttonText2 = key2.GetComponentInChildren<Text>();
        buttonText2.text = "-";

        key2.GetComponent<Button>().onClick.AddListener(() => Callback());
    }

    public void AddLetter(char letter)
    {
        UserAnswer.text += letter;
    }

    public void Call(OnResolve callbackFonction)
    {
        Callback = callbackFonction;
        InitializeKeybord();
        Open();
    }

    void Open()
    {
        GameController.Instance.State = GameState.keyboard;
        UserAnswer.gameObject.SetActive(true);
        KeyHolder.gameObject.SetActive(true);
    }

    public void Close()
    {
        GameController.Instance.State = GameState.running;
        UserAnswer.gameObject.SetActive(false);
        KeyHolder.gameObject.SetActive(false);

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        UserAnswer.text = "";
    }
}
