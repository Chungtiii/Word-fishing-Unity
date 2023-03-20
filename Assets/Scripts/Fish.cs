using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    private float speed; // 魚的速度
    Vector3 velocity;
    Vector3 desiredVelocity;
    public float acceleration = 1f; // 魚的加速度
    Vector3 leftBottomPoint, rightTopPoint;
    private SpriteRenderer mySpriteRenderer;
    bool isInScreen = false; // 記錄是否進入屏幕
    public List<int> ansList1 = new List<int>();
    public TextMesh DA_Text; // 字母
    string fishWord; // 魚身上的字母
    GameManager GM;
    Answer ANS;


    //0108釣起
    public bool beCatchedA = false;
    public bool beCatchedB = false;
    public GameObject p1Hook;
    public GameObject p2Hook;
    public GameObject p1Line;
    public GameObject p2Line;

    void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        velocity = new Vector3(0, 0, 0);
        desiredVelocity = new Vector3(0, 0, 0);

        leftBottomPoint = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Mathf.Abs(Camera.main.transform.position.z)));
        rightTopPoint = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Mathf.Abs(Camera.main.transform.position.z)));

        ANS = GameObject.Find("GameAnswer").GetComponent<Answer>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        fishWord = GM.fishWord;
        DA_Text.text = fishWord;
        if (GM.fishVariety == 0) { // 不同品種的魚魚有不同速度
            speed = 1f;
           
        }
        else if (GM.fishVariety == 1) {
            speed = 2f;
        }
        else if (GM.fishVariety == 2) {
            speed = 3f;
        }


        //0108
        p1Hook = GameObject.Find("P1hook");
        p2Hook = GameObject.Find("P2hook");
        p1Line = GameObject.Find("P1_FishLine");
        p2Line = GameObject.Find("P2_FishLine");
    }


    void Update()
    {
        velocity = Vector3.MoveTowards(velocity, desiredVelocity, acceleration);
        if (velocity.x < 0) mySpriteRenderer.flipX = false;
        transform.localPosition += velocity * Time.deltaTime;
        Clamp();
        Die();

        //0108
        if (beCatchedA)
        {
            if (p1Line.transform.position.y < 6.5)
            {
                this.transform.position = p1Hook.transform.position;
            }

            else
            {
                Destroy(this.gameObject);
            }
        }
        else if (beCatchedB)
        {
            if (p2Line.transform.position.y < 6.5)
            {
                this.transform.position = p2Hook.transform.position;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    // 在外部僅給魚一個移動方向
    public void SetDesiredVelocity(Vector3 Input){
        Vector3.ClampMagnitude(Input, 1f);
        desiredVelocity = Input * speed;
    }

    void Clamp() {
        Vector3 position = transform.localPosition;
        if (position.x > rightTopPoint.x + 3) {
            position.x = rightTopPoint.x + 3;
            velocity.x = 0;
        }
        else if (position.x < leftBottomPoint.x - 3) {
            position.x = leftBottomPoint.x - 3;
            velocity.x = 0;
        }
        if (position.y > rightTopPoint.y) {
            position.y = rightTopPoint.y;
            velocity.y = 0;
        }
        else if (position.y < leftBottomPoint.y) {
            position.y = leftBottomPoint.y;
            velocity.y = 0;
        }
        transform.localPosition = position;
    }

    void Die() {
        float x = transform.position.x;
        float y = transform.position.y;
        if ((x >= leftBottomPoint.x && x <= rightTopPoint.x) && (y >= leftBottomPoint.y && y <= rightTopPoint.y)) {
            isInScreen = true;
        }
        else {
            if (isInScreen) {
                Destroy(this.gameObject, 2f); // 超出螢幕後2秒死亡
            }
        }
    }

    /*void OnMouseDown() { // 先用滑鼠點擊，之後改成OnTrigger
        ANS.FishLoad1(fishWord); // P1
        ANS.FishLoad2(fishWord); // P2
        Destroy(this.gameObject);
    }*/

    //0108
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="P1_Hook")
        {
            p1Line.GetComponent<ControlLine>().Catcha = true;
            p1Line.GetComponent<ControlLine>().IsFish = true;
            //ANS.FishLoad1(fishWord);
            p1Line.GetComponent<ControlLine>().fishword = fishWord;
            beCatchedA = true;
           
               
            }
           
       
        else if(other.tag=="P2_Hook")
        {
            p2Line.GetComponent<ControlLine>().Catcha = true;
            p2Line.GetComponent<ControlLine>().IsFish = true;
            //ANS.FishLoad2(fishWord);
            p2Line.GetComponent<ControlLine>().fishword = fishWord;
            beCatchedB = true;
        }

        this.GetComponent<BoxCollider>().enabled = false;
    }
   
}

