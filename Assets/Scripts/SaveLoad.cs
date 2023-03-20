using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SaveLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Save()
    
   {
        ScoreCode ScoreCode = GetComponent<ScoreCode>();
        Console.WriteLine (ScoreCode.P1Score);
   
      // string score = ScoreCode.P1Score;
      //  ShowP1Score.text = P1Score.ToString();
      // PlayerPrefs.SetString("Score", score);
      // PlayerPrefs.Save();
    }

    // Update is called once per frame
   
}
