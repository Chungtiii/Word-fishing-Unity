using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ScoreCode2: MonoBehaviour

{

    //�ŧi���ưѼ�

    public static int P2Score;

    //�ŧi��rUI

    public Text ShowP2Score;

    void Update()

    {

        //��UI��r�P���ƦP�B

        ShowP2Score.text = P2Score.ToString();

    }

}