    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources: MonoBehaviour {

    public const int MAX_LIFE = 100;
    public const int MAX_EXP = 100;

    int _life = 50;
    int _exp = 0;
    int _level = 0;

    public int life { get { return _life; } }
    public int experience { get { return _exp; } }
    public int level { get { return _level; } }

    void Start()
    {
        FindObjectOfType<GaugesManager>().InitializeGauges(MAX_LIFE, life, MAX_EXP, experience, level);
    }

    public void AddLife(int value = 1)
    {
        var newValue = _life + value;
        _life = (newValue > MAX_LIFE) ? MAX_LIFE : newValue;

        FindObjectOfType<GaugesManager>().CalibrateLife(life);
    }

    public void SubstractLife(int value = 1)
    {
        var newValue = _life - value;
        _life = (newValue < 0) ? 0 : newValue;

        FindObjectOfType<GaugesManager>().CalibrateLife(life);
    }

    public void AddExperience(int value = 1)
    {
        var newValue = _exp + value;
        
        if (newValue <= MAX_EXP)
        {
            _exp = newValue;
        }
        else
        {
            AddLevel();
            var valueToAdd = value - (MAX_EXP - _exp);
            _exp = valueToAdd;
        }

        FindObjectOfType<GaugesManager>().CalibrateExperience(experience);
    }

    public void AddLevel(int value = 1)
    {
        _level += value;
        FindObjectOfType<GaugesManager>().CalibrateLevel(level);
    }

    public void DisplayStats()
    {
        Debug.Log("Player stats :");
        Debug.Log("Life = " + life);
        Debug.Log("-------------------");
        Debug.Log("Experience = " + experience);
        Debug.Log("-------------------");
        Debug.Log("Level = " + level);
        Debug.Log("-------------------");
    }
}
