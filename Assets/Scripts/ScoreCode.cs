using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ScoreCode : MonoBehaviour

{

    //�ŧi���ưѼ�

    public static int P1Score;

    //�ŧi��rUI

    public Text ShowP1Score;

    void Update()

    {

        //��UI��r�P���ƦP�B

        ShowP1Score.text = P1Score.ToString();

    }

}