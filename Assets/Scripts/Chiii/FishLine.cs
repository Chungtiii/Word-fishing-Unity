using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLine : MonoBehaviour
{
    public LineRenderer fishline;
    public GameObject ship;
    public Vector3 ship_pos;
    public bool catchfish=false;
    public bool Catcha = false;
    public bool GoFast = false;
    public bool GoSlow = false;
    public bool ClickBtn = false;
    public GameObject PK;
    public GameObject obj;
    public int PlayerNum;
    // Start is called before the first frame update
    void Start()
    {
        fishline.GetComponent<LineRenderer>();
        //ship_pos = ship.transform.position;
        if(PlayerNum==1)
        {
            ship_pos = new Vector3(-5.5f, 1.7f, 0);
        }
        else if(PlayerNum==2)
        {
            ship_pos = new Vector3(5.5f, 1.7f, 0);
        }
        
        //line_pos = new Vector3(ship_pos.x, ship_pos.y, ship_pos.z);
        fishline.SetPosition(0, ship_pos);
        fishline.SetPosition(1, ship_pos);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerNum==1)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ClickBtn = true;
            }
        }
        else if(PlayerNum==2)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                ClickBtn = true;
            }
        }

        if (catchfish == false&&ClickBtn)
        {
            if (ship_pos.y > -5)
            {
                fishline.SetPosition(1, ship_pos);

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
        else if(ClickBtn)
        {
            if (ship_pos.y< 1.7)
            {
                fishline.SetPosition(1, ship_pos);
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

                ObjMove(ship_pos);
            }
            else
            {
                catchfish = false;
                ClickBtn = false;
                Destroy(obj.gameObject);
                if (Catcha)
                {
                    GoSlow = false;
                    GoFast = false;
                }
                Catcha = false;
            }

        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, 1))
        {

        }
        if ((hit.collider.tag == "fish") && (ship_pos.y - hit.transform.position.y < 0.1f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            catchfish = true;
            Debug.Log("Catch Fish!!!");
            Catcha = true;
            obj = hit.transform.gameObject;
        }
        else if ((hit.collider.tag == "Prop_Fast") && (ship_pos.y - hit.transform.position.y < 0.1f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
            catchfish = true;
            Debug.Log("Go Fast!!!");
            GoFast = true;
            obj = hit.transform.gameObject;
        }
        else if ((hit.collider.tag == "Prop_Slow") && (ship_pos.y - hit.transform.position.y < 0.1f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.blue);
            catchfish = true;
            Debug.Log("Go Slow...");
            //GoSlow = true;
            PK.GetComponent<FishLine>().GoFast = false;
            PK.GetComponent<FishLine>().Catcha = false;
            PK.GetComponent<FishLine>().GoSlow = true;
            obj = hit.transform.gameObject;
        }

    }
    public void ObjMove(Vector3 pos)
    {
        obj.GetComponent<Fish_test>().enabled = false;
        obj.transform.position = pos;
    }
}
