using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : MonoBehaviour
{
    public GameManager GM;

    // 讀取文檔
    string[][] ArrayX;
    string[] lineArray;

    // 加載題目
    public TextMesh TM_Text1; // P1當前題目
    public TextMesh TM_Text2; // P2當前題目
    public int topicIndex1 = 0; // P1第幾題
    public int topicIndex2 = 0; // P2第幾題
    public GameObject P1_Pic; // P1圖
    public GameObject P2_Pic; // P2圖
    public TextMesh P1_Ans; // P1當前答案
    public TextMesh P2_Ans; // P2當前答案
    public AudioSource wrongSource;
    public AudioSource correctSource;

    public List<int> numList = new List<int>();
    public List<int> ansList1 = new List<int>();
    public List<int> ansList2 = new List<int>();

    string ansArray1; // P1目前的答案
    string fishAns1; // P1目前釣的答案
    string ansArray2; // P2目前的答案
    string fishAns2; // P2目前釣的答案

    void Start()
    {
        for (int i = 0; i < 18; i++)
        {
            numList.Add(i);
        }
        OutOfOrder(numList);
        TextCsv();
        StartLoad1();
        StartLoad2();
    }

    /*****************讀取txt數據******************/
    void TextCsv()
    {
        // 讀取csv二進制文件  
        TextAsset binAsset = Resources.Load("ans", typeof(TextAsset)) as TextAsset;
        // 讀取每一行的内容  
        lineArray = binAsset.text.Split('\n');
        // 創建二維數組
        ArrayX = new string[lineArray.Length][];
        // 把csv中的數據儲存在二維數組中  
        for (int i = 0; i < lineArray.Length; i++)
        {
            ArrayX[i] = lineArray[i].Split(':');
        }
        // 查看保存的題目數據
        // for (int i = 0; i < ArrayX.Length; i++)
        // {
        //     for (int j = 0; j < ArrayX[i].Length; j++)
        //     {
        //         Debug.Log(ArrayX[i][j]);
        //     }
        // }
    }

    /*****************打亂題目順序******************/
    public void OutOfOrder<T>(List<T> list)
    {
        int index;
        T temp;
        for (int i = 0; i < list.Count; i++)
        {
            index = UnityEngine.Random.Range(0, list.Count);
            if (index != i)
            {
                temp = list[i];
                list[i] = list[index];
                list[index] = temp;
            }
        }
    }

    /*****************P1加載題目******************/
    public void StartLoad1()
    {
        P1_Ans.text = "";
        fishAns1 = "";
        TM_Text1.text = ArrayX[numList[topicIndex1]][0]; // 題目
        GM.idx1 = ArrayX[numList[topicIndex1]].Length - 2; // 有幾個選項
        ansArray1 = ArrayX[numList[topicIndex1]][GM.idx1 + 1];
        foreach(char i in ansArray1) {
            ansList1.Add(i); // P1的正確答案
        }
        Sprite pic1 = Resources.Load<Sprite>("Fruit/" + ansArray1);
        P1_Pic.GetComponent<SpriteRenderer>().sprite = pic1; // 圖片
    }
    public void ReLoad1()
    {
        GM.idx1 = ArrayX[numList[topicIndex1]].Length - 2; // 有幾個選項
    }
    public void LoadAnswer1()
    {
        GM.fishWord = ArrayX[numList[topicIndex1]][GM.idx1]; // 選項
    }

    /*****************P2加載題目******************/
    public void StartLoad2()
    {
        P2_Ans.text = "";
        fishAns2 = "";
        TM_Text2.text = ArrayX[numList[topicIndex2]][0]; // 題目
        GM.idx2 = ArrayX[numList[topicIndex2]].Length - 2; // 有幾個選項
        GM.idy2 = 0;
        ansArray2 = ArrayX[numList[topicIndex2]][GM.idx2 + 1];
        foreach(char i in ansArray2) {
            ansList2.Add(i); // P2的正確答案
        }
        Sprite pic2 = Resources.Load<Sprite>("Fruit/" + ansArray2);
        P2_Pic.GetComponent<SpriteRenderer>().sprite = pic2; // 圖片
    }
    public void ReLoad2()
    {
        GM.idx2 = ArrayX[numList[topicIndex2]].Length - 2; // 有幾個選項
        GM.idy2 = 0;
    }
    public void LoadAnswer2()
    {
        GM.fishWord = ArrayX[numList[topicIndex2]][GM.idy2 + 1]; // 選項
    }

    /*****************P1釣起來的字母******************/
    public void FishLoad1(string word)
    {
        if (word == ((char)ansList1[0]).ToString()) {
            fishAns1 += word;
            P1_Ans.text = fishAns1;
            ansList1.Remove((char)ansList1[0]);
            if (ansList1.Count == 0) {
                topicIndex1++;
                StartLoad1();
            }
       
            if (GM.fishVariety == 0)
            { // 不同品種的魚魚有不同速度
                ScoreCode.P1Score = ScoreCode.P1Score + 1;

            }
            else if (GM.fishVariety == 1)
            {
                ScoreCode.P1Score = ScoreCode.P1Score + 2;
            }
            else if (GM.fishVariety == 2)
            {
                ScoreCode.P1Score = ScoreCode.P1Score + 3;
            }
            correctSource.Play();
           
        }
        else {wrongSource.Play();
            if (ScoreCode.P1Score > 0)
            {
                ScoreCode.P1Score = ScoreCode.P1Score - 1;
            }
        }
    }

    /*****************P2釣起來的字母******************/
    public void FishLoad2(string word)
    {
        if (word == ((char)ansList2[0]).ToString()) {
            fishAns2 += word;
            P2_Ans.text = fishAns2;
            ansList2.Remove((char)ansList2[0]);
            if (ansList2.Count == 0) {
                topicIndex2++;
                StartLoad2();
            }
            correctSource.Play();
            if (ansList1.Count == 0)
            {
                topicIndex1++;
                StartLoad1();
            }
            if (GM.fishVariety == 0)
            { // 不同品種的魚魚有不同速度
                ScoreCode2.P2Score = ScoreCode2.P2Score + 1;

            }
            else if (GM.fishVariety == 1)
            {
                ScoreCode2.P2Score = ScoreCode2.P2Score + 2;
            }
            else if (GM.fishVariety == 2)
            {
                ScoreCode2.P2Score = ScoreCode2.P2Score + 3;
            }
        }
        else { wrongSource.Play();
            if (ScoreCode2.P2Score > 0)
            {
                ScoreCode2.P2Score = ScoreCode2.P2Score - 1;
            }
        }

    }

    /*****************P1提示道具******************/
    public void tipLoad1()
    {
        fishAns1 += ((char)ansList1[0]).ToString();
        P1_Ans.text = fishAns1;
        ansList1.Remove((char)ansList1[0]);
        if (ansList1.Count == 0) {
            topicIndex1++;
            StartLoad1();
        }
    }

    /*****************P2提示道具******************/
    public void tipLoad2()
    {
        fishAns2 += ((char)ansList2[0]).ToString();
        P2_Ans.text = fishAns2;
        ansList2.Remove((char)ansList2[0]);
        if (ansList2.Count == 0) {
            topicIndex2++;
            StartLoad2();
        }
    }
}
