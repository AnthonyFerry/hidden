using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour{

    private bool _isOn = false;
    public TriggerableObject Target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Toggle()
    {
        if (_isOn)
            TurnOff();
        else
            TurnOn();
    }

    void TurnOn()
    {
        if (!_isOn)
        {
            _isOn = true;
            GetComponent<Animator>().SetTrigger("On");

            Target.Activate();
        }
    }

    void TurnOff()
    {
        if (_isOn)
        {
            _isOn = false;
            GetComponent<Animator>().SetTrigger("Off");

            Target.Activate();
        }
    }

    void OnTriggerStay(Collider col)
    {
        var obj = col.gameObject;
        if (obj.name == "Player")
        {
            UIManager.Instance.HintScript.Show("$", Hint.X_BUTTON);
            if (Input.GetButtonDown("interact"))
                Toggle();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.name == "Player")
        {
            UIManager.Instance.HintScript.Hide();
        }
    }
}
