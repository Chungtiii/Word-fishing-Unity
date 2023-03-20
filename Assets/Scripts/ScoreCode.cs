using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ScoreCode : MonoBehaviour

{

    //だ计把计

    public static int P1Score;

    //ゅUI

    public Text ShowP1Score;

    void Update()

    {

        //琵UIゅ籔だ计˙

        ShowP1Score.text = P1Score.ToString();

    }

}