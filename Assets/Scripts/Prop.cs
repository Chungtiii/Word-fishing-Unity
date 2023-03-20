using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    private float speed = 2f; // 道具的速度
    Vector3 velocity;
    Vector3 desiredVelocity;
    public float acceleration = 1f; // 道具的加速度
    Vector3 leftBottomPoint, rightTopPoint;
    private SpriteRenderer mySpriteRenderer;
    bool isInScreen = false; // 記錄是否進入屏幕

    int whichItem; // 道具的種類
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
        whichItem = GM.itemVariety;



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

    // 在外部僅給道具一個移動方向
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
                Destroy(this.gameObject, 2f);
            }
        }
    }

    void OnMouseDown() { // 先用滑鼠點擊，之後改成OnTrigger
        if (whichItem == 3) { // 提示道具
            ANS.tipLoad1(); // P1
            ANS.tipLoad2(); // P1
        }
        Destroy(this.gameObject);
    }

    //0108
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "P1_Hook")
        {
            if(whichItem==3)
            {
                ANS.tipLoad1();
            }

            p1Line.GetComponent<ControlLine>().Catcha = true;
            beCatchedA = true;
        }
        else if (other.tag == "P2_Hook")
        {
            if(whichItem==3)
            {
                ANS.tipLoad2();
            }

            p2Line.GetComponent<ControlLine>().Catcha = true;
            beCatchedB = true;
        }

        this.GetComponent<BoxCollider>().enabled = false;
    }
}
