using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public GameObject obj;
    public ControlLine controlLine;
    public ControlLine PK;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controlLine.Catcha)
        {
            this.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            this.GetComponent<BoxCollider>().enabled = true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {

        controlLine.catchfish = true;
        if(other.tag=="Prop_Slow")
        {
            Debug.Log("Hit slow...");
            PK.GoSlow = true;
        }
        if(other.tag=="Prop_Fast")
        {
            Debug.Log("Hit fast!");
            controlLine.GoFast = true;
        }

    }
}
