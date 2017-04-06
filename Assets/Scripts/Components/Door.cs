using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : TriggerableObject {

    private bool _isOpen;
    private Animator _controller;

    public bool AutomaticallyClose = false;

    public bool IsOpen { get { return _isOpen; } }

	// Use this for initialization
	void Start () {
        IsActive = false;

        _controller = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    override public void Activate()
    {
        Toggle();
    }

    public void Open()
    {
        if (!IsOpen)
        {
            _controller.SetTrigger("Open");
            _isOpen = true;
        }
    }

    public void Close()
    {
        if (IsOpen)
        {
            _controller.SetTrigger("Close");
            _isOpen = false;
        }
    }

    public void Toggle()
    {
        if (IsOpen)
            Close();
        else
            Open();
    }

    void OnTriggerStay(Collider col)
    {
        var obj = col.gameObject;
        if (obj.name == "Player")
        {
            RaycastHit hit;

            var pos = obj.transform.position;
            var forward = obj.transform.forward;

                Debug.DrawRay(pos, forward, Color.green);
            if (Physics.Raycast(pos, forward, out hit, 2))
            {
                if (hit.collider.tag == "Door")
                {
                    if (!IsOpen)
                        UIManager.Instance.HintScript.Show("$", Hint.X_BUTTON);
                    if (Input.GetButtonDown("interact"))
                        Open();
                }
                    
                else
                    UIManager.Instance.HintScript.Hide();
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.name == "Player")
        {
            UIManager.Instance.HintScript.Hide();
            if (AutomaticallyClose)
                Close();
        }
    }
}
