using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ScoreCode2: MonoBehaviour

{

    //だ计把计

    public static int P2Score;

    //ゅUI

    public Text ShowP2Score;

    void Update()

    {

        //琵UIゅ籔だ计˙

        ShowP2Score.text = P2Score.ToString();

    }

}