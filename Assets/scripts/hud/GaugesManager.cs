using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugesManager : MonoBehaviour {

    public Image Life;
    public Image Experience;
    public Text Level;

    private float _maximumLife;
    private float _currentLife;
    private float _percentageLife;

    private float _maximumExperience;
    private float _currentExperience;
    private float _percentageExperience;
	
	// Update is called once per frame
	void Update ()
    {
        Experience.fillAmount = Mathf.Lerp(Experience.fillAmount, _percentageExperience, Time.deltaTime * 5);
        Life.fillAmount = Mathf.Lerp(Life.fillAmount, _percentageLife, Time.deltaTime * 5);
    }

    public void InitializeGauges(float maxLife, float currentLife, float maxXP, float currentXP, int level)
    {
        _maximumLife = maxLife;
        _maximumExperience = maxXP;

        _currentLife = currentLife;
        _currentExperience = currentXP;

        CalibrateLife(_currentLife);
        CalibrateExperience(_currentExperience);
        CalibrateLevel(level);
    }

    public void CalibrateExperience(float newValue)
    {
        _currentExperience = newValue;

        _percentageExperience = newValue / _maximumExperience;
    }

    public void CalibrateLife(float newValue)
    {
        _currentLife = newValue;

        _percentageLife = newValue / _maximumLife;
    }

    public void CalibrateLevel(int newValue)
    {
        Level.text = "Level : " + newValue;
    }
}
