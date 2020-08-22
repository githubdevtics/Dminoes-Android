using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelCalculator : MonoBehaviour
{
    [SerializeField]
    public levelValues[] levelvalues;
    public int currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float calculateLevel()
    {
        int value = getLevel();
        
        int points = PlayerPrefs.GetInt("points", 0);

        int divider = levelvalues[getLevel()].max - levelvalues[getLevel()].min;
        int divident = points - levelvalues[getLevel()].min;

        return  (divident * 1.0f) / divider;
    }
    public int getLevel()
    {
        int value = PlayerPrefs.GetInt("points", 0);
        for (int i = 0; i < levelvalues.Length; i++)
        {
            if (value > levelvalues[i].min && value < levelvalues[i].max)
            {
                return i;
            }
        }
        return 0;
    }

    [Serializable]
    public class levelValues
    {

        public int min;
        public int max;
    }
}


