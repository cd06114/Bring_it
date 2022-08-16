using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bridges : MonoBehaviour
{
    public Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new System.Random();
        rigid.velocity = new Vector3(rnd.Next(-50, 50), 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(rigid.transform.position.x < -25)
        {
            System.Random rnd = new System.Random();
            rigid.velocity = new Vector3(rnd.Next(10, 30),0,0);
        }
        else if(rigid.transform.position.x > 21)
        {
            System.Random rnd = new System.Random();
            rigid.velocity = new Vector3(-rnd.Next(10, 30), 0, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bridge")
        {
            
        }
    }
}
