using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int idx1; // P1有幾個選項
    public int idx2; // P2有幾個選項
    public int idy2; // P2順序
    public string fishWord; // 魚身上的字母
    public bool P1_isFinish; // P1完成當前題目
    public bool P2_isFinish; // P2完成當前題目
    public int fishVariety; // 魚魚的品種
    public int itemVariety; // 道具的種類

    public Text WinText;
    public string Winplayer;

    List<Score> scoreList = new List<Score>();

    public GameObject InputPanel;
    public InputField IF_Name;
    public Transform LBContent;
    public GameObject Prefab_LBItem;

    public Text P1Score, P2Score;
    public int Score1, Score2;

    void Start()
    {
        LoadData();
    }

    public void startBtn()
    {
        SceneManager.LoadScene(1);
    }
    public void backBtn()
    {
        SceneManager.LoadScene(0);
    }

    public void AddLeaderboard()
    {
        if (IF_Name.text.Length == 0)
            return;

        InputPanel.SetActive(false);

        scoreList.Add(new Score(IF_Name.text, Score1));
        scoreList.Sort();
        scoreList.Reverse();
        SaveData();

        for (int i = 0; i < LBContent.childCount; i++)
        {
            Destroy(LBContent.GetChild(i).gameObject);
        }
        LBContent.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

        for (int i = 0; i < scoreList.Count; i++)
        {
            GameObject item = Instantiate(Prefab_LBItem);
            item.SetActive(true);
            item.transform.SetParent(LBContent, false);

            item.transform.Find("RankingText").GetComponent<Text>().text = (i + 1).ToString();
            item.transform.Find("PlayerNameText").GetComponent<Text>().text = scoreList[i].name;
            item.transform.Find("ScoreText").GetComponent<Text>().text = scoreList[i].score.ToString();

            Vector2 V2_lbc = LBContent.GetComponent<RectTransform>().sizeDelta;
            LBContent.GetComponent<RectTransform>().sizeDelta = new Vector2(V2_lbc.x, V2_lbc.y + 100);
        }
    }

    public void LoadData()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/RankingList.txt");
        string nextLine;

        while ((nextLine = sr.ReadLine()) != null)
        {
            scoreList.Add(JsonUtility.FromJson<Score>(nextLine));
            scoreList.Sort();
            scoreList.Reverse();
        }

        sr.Close();
    }

    public void SaveData()
    {
        StreamWriter sw = new StreamWriter(Application.dataPath + "/Resources/RankingList.txt");

        if (scoreList.Count > 5)
        {
            for (int i = 5; i <= scoreList.Count; i++)
            {
                scoreList.RemoveAt(i);
            }
        }

        foreach (Score t in scoreList)
        {
            sw.WriteLine(JsonUtility.ToJson(t));
        }

        sw.Close();
    }
}
