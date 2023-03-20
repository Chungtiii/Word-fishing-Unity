using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_test : MonoBehaviour
{
    public bool beCatchedA = false;
    public bool beCatchedB = false;
    public GameObject p1Hook;
    public GameObject p2Hook;
    public GameObject p1Line;
    public GameObject p2Line;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(beCatchedA)
        {
            if (p1Line.transform.position.y<6.5)
            {
                this.transform.position = p1Hook.transform.position;
            }

            else
            {
                Destroy(this.gameObject);
            }
        }
        else if(beCatchedB)
        {
            if (p2Line.transform.position.y<6.5)
            {
                this.transform.position = p2Hook.transform.position;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            int rn = Random.Range(1, 100);
            if (rn < 50)
            {
                transform.Translate(-0.5f, 0, 0);
            }
            else
            {
                transform.Translate(0.5f, 0, 0);
            }
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "P1_Hook")
        {
            beCatchedA = true;
        }
        else if (other.tag == "P2_Hook")
        {
            beCatchedB = true;
        }

        this.GetComponent<BoxCollider>().enabled = false;
    }
}
