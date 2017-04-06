using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sine : MonoBehaviour {

    public SineType Type;
    public float Magnitude;
    public float Speed;

    public bool Enabled = true;

    float _sineValue;

    Vector3 _startPosition;
    Vector3 _startScale;

	// Use this for initialization
	void Start () {
        _startPosition = transform.position;
        _startScale = transform.localScale;	    
	}
	
	// Update is called once per frame
	void Update () {
        if (!Enabled)
            return;

        _sineValue = Mathf.Sin(Time.timeSinceLevelLoad * Speed) * Magnitude;

        if (Type == SineType.LeftToRight)
        {
            transform.position = _startPosition + transform.forward * _sineValue;
        }
        else if (Type == SineType.ForwardBackward)
        {
            transform.position = _startPosition + transform.right * _sineValue;
        }
        else if (Type == SineType.Vertical)
        {
            transform.position = _startPosition + transform.up * _sineValue;
        }
        else if (Type == SineType.Size)
        {
            transform.localScale = _startScale * (_sineValue + 0.2f);
        }
	}
}

public enum SineType
{
    LeftToRight,
    ForwardBackward,
    Vertical,
    Size
}