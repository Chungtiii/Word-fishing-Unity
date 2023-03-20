using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLine : MonoBehaviour
{
    public bool _Up=true,_Down=true,_Left=true,_Right=true;
    public int PlayerNum;
    public Sprite[] images;
    public SpriteRenderer hook;

    //public GameObject ship;
    public Vector3 ship_pos;
    public bool catchfish = false;
    public bool Catcha = false;
    public bool GoFast = false;
    public bool GoSlow = false;
    public bool ClickBtn = false;
    public bool IsFish = false;
    public GameObject PK;
    public GameObject obj;
    public AudioSource waterSource;


    Answer ANS;
    public string fishword;
    // Start is called before the first frame update
    void Start()
    {
        ship_pos = this.transform.position;
        ANS = GameObject.Find("GameAnswer").GetComponent<Answer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Auto_Transform();
        this.transform.position = ship_pos;

        Rotate_Control();
    }

    public void Rotate_Control()
    {
        if (PlayerNum == 1)
        {
            if (this.transform.rotation.eulerAngles.z < 337f && transform.rotation.eulerAngles.z > 300f)
            {
                _Left = false;
            }
            else if (this.transform.rotation.eulerAngles.z > 17f && transform.rotation.eulerAngles.z < 300f)
            {
                _Right = false;
            }
            else
            {
                _Left = true;
                _Right = true;
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (_Left)
                {
                    this.transform.Rotate(0, 0, -0.5f);
                    hook.sprite = images[0];
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (_Right)
                {
                    this.transform.Rotate(0, 0, 0.5f);
                    hook.sprite = images[1];
                }
            }


        }

        if (PlayerNum == 2)
        {
            //this.transform.position = new Vector3(5.5f, transform.position.y, 0);

            if (this.transform.rotation.eulerAngles.z < 340f && transform.rotation.eulerAngles.z > 300f)
            {
                _Left = false;
            }
            else if (this.transform.rotation.eulerAngles.z > 20f && transform.rotation.eulerAngles.z < 300f)
            {
                _Right = false;
            }
            else
            {
                _Left = true;
                _Right = true;
            }


            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (_Left)
                {
                    this.transform.Rotate(0, 0, -0.5f);
                    hook.sprite = images[0];
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (_Right)
                {
                    this.transform.Rotate(0, 0, 0.5f);
                    hook.sprite = images[1];
                }
            }


        }
    }
    public void Auto_Transform()
    {
        if (PlayerNum == 1)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ClickBtn = true;
                waterSource.Play();
            }
        }
        else if (PlayerNum == 2)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ClickBtn = true;
                waterSource.Play();
            }
        }

        
        if (catchfish == false && ClickBtn)
        {
            if (ship_pos.y > 0.5)
            {
                //fishline.SetPosition(1, ship_pos);

                if (GoFast)
                {
                    ship_pos = new Vector3(ship_pos.x, ship_pos.y - 0.7f, ship_pos.z);
                }
                else if (GoSlow)
                {
                    ship_pos = new Vector3(ship_pos.x, ship_pos.y - 0.05f, ship_pos.z);
                }
                else
                {
                    ship_pos = new Vector3(ship_pos.x, ship_pos.y - 0.1f, ship_pos.z);
                }

            }
            else
            {
                //ship_pos = ship.transform.position;
                catchfish = true;
            }

        }
        else if (ClickBtn)
        {
            if (ship_pos.y < 7)
            {
                //fishline.SetPosition(1, ship_pos);
                if (GoFast)
                {
                    ship_pos = new Vector3(ship_pos.x, ship_pos.y + 0.7f, ship_pos.z);
                }
                else if (GoSlow)
                {
                    ship_pos = new Vector3(ship_pos.x, ship_pos.y + 0.02f, ship_pos.z);
                }
                else
                {
                    ship_pos = new Vector3(ship_pos.x, ship_pos.y + 0.1f, ship_pos.z);
                }

                //ObjMove(ship_pos);
            }
            else
            {
                catchfish = false;
                ClickBtn = false;
                //Destroy(obj.gameObject);
                if (Catcha&&IsFish==true)
                {
                    GoSlow = false;
                    GoFast = false;

                    if (PlayerNum == 1)
                    {
                        ANS.FishLoad1(fishword);
                    }
                    else if (PlayerNum == 2)
                    {
                        ANS.FishLoad2(fishword);
                    }

                    IsFish = false;
                }

                
                Catcha = false;
            }

        }
    }

    public void ObjMove(Vector3 pos)
    {
        obj.GetComponent<Fish_test>().enabled = false;
        obj.transform.position = pos;
    }


}
