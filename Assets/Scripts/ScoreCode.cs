using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ScoreCode : MonoBehaviour

{

    //宣告分數參數

    public static int P1Score;

    //宣告文字UI

    public Text ShowP1Score;

    void Update()

    {

        //讓UI文字與分數同步

        ShowP1Score.text = P1Score.ToString();

    }

}