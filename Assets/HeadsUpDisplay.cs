using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HeadsUpDisplay : MonoBehaviour {

    public Player Player;
    public Text LifeText, ExperienceText;

	// Use this for initialization
	void Start () {
        if (Player == null)
            Debug.Log("Player reference not found");
	}
	
	// Update is called once per frame
	void Update () {
        LifeText.text = "Vie : " + Player.Life;
        ExperienceText.text = "Experience : " + Player.XP;
	}
}
