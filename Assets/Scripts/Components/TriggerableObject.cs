using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class TriggerableObject : MonoBehaviour {

    public bool IsActive = false;

    abstract public void Activate();
} 
