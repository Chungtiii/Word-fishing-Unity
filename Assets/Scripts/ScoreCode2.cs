using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ScoreCode2: MonoBehaviour

{

    //宣告分數參數

    public static int P2Score;

    //宣告文字UI

    public Text ShowP2Score;

    void Update()

    {

        //讓UI文字與分數同步

        ShowP2Score.text = P2Score.ToString();

    }

}