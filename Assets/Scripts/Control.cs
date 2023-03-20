using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public GameManager GM;
    public Answer ANS;

    public FishFactory fishFactory;
    public PropFactory propFactory;
    public float delayTime = 3f;
    float lastTime = 0f;
    Vector3 leftBottomPoint, rightTopPoint;

    void Awake()
    {
        leftBottomPoint = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Mathf.Abs(Camera.main.transform.position.z)));
        rightTopPoint = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Mathf.Abs(Camera.main.transform.position.z)));
    }

    void Start() {
        InvokeRepeating("MakeItem", 10f, 15f);
    }

    void Update()
    {
        lastTime += Time.deltaTime;
        if (lastTime >= delayTime){
            lastTime = 0f;
            if (GM.idx1 > 0) {
                ANS.LoadAnswer1();
                MakeFish();
                GM.idx1 = GM.idx1 - 1;
            }
            else if (!GM.P1_isFinish) { // 如果P1未完成題目，魚再重新出現
                ANS.ReLoad1();
            }
        }
        lastTime += Time.deltaTime;
        if (lastTime >= delayTime){
            lastTime = 0f;
            if (GM.idx2 > 0) {
                ANS.LoadAnswer2();
                MakeFish();
                GM.idx2 = GM.idx2 - 1;
                GM.idy2 = GM.idy2 + 1;
            }
            else if (!GM.P2_isFinish) { // 如果P2未完成題目，魚再重新出現
                ANS.ReLoad2();
            }
        }

        if (Input.GetKeyDown("down")) // P2 control
        {
            print("down key was pressed");
        }
        if (Input.GetKeyDown("s")) // P1 control
        {
            print("s key was pressed");
        }
    }

    void MakeFish() { // 生成魚魚
        GM.fishVariety = Random.Range(0, 3);
        Fish fish = fishFactory.Get(GM.fishVariety); // 不同品種的魚魚
        if (Random.Range(0, 2) == 0) {
            fish.transform.position = new Vector3(leftBottomPoint.x - 2f, Random.Range(leftBottomPoint.y + 0.8f, rightTopPoint.y - 4.8f), 0f);
            fish.SetDesiredVelocity(Vector3.right);
        }
        else {
            fish.transform.position = new Vector3(rightTopPoint.x + 2f, Random.Range(leftBottomPoint.y + 0.8f, rightTopPoint.y - 4.8f), 0f);
            fish.SetDesiredVelocity(Vector3.left);
        }
    }

    void MakeItem() { // 生成道具
        GM.itemVariety = Random.Range(0, 4);
        Prop prop = propFactory.Get(GM.itemVariety); // 不同種類的道具
        if (Random.Range(0, 2) == 0) {
            prop.transform.position = new Vector3(leftBottomPoint.x - 2f, Random.Range(leftBottomPoint.y + 0.8f, rightTopPoint.y - 4.8f), 0f);
            prop.SetDesiredVelocity(Vector3.right);
        }
        else {
            prop.transform.position = new Vector3(rightTopPoint.x + 2f, Random.Range(leftBottomPoint.y + 0.8f, rightTopPoint.y - 4.8f), 0f);
            prop.SetDesiredVelocity(Vector3.left);
        }
    }
}
