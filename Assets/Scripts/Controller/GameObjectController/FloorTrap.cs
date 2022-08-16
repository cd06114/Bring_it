using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
public class FloorTrap : MonoBehaviour
{
    float movingTime = 3f;  //랜덤으로 속도 바뀌는 주기
    float rotSpeed = 0;
    public int speedRange;
    public GameObject GameObject;
    bool isMoved= false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameObject.transform.rotation.z);
        if(movingTime>0)
        {
            transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
            movingTime -= Time.deltaTime;
        }
        else
        {
            System.Random rnd = new System.Random();
            rotSpeed = rnd.Next(-speedRange, speedRange);
            movingTime = 3f;
        }
        
        //if (isMoved == false)
        //{
        //    if (GameObject.transform.rotation.z <= -0.4)
        //    {
        //        isMoved = true;
        //    }
        //        transform.Rotate(new Vector3(0, 0, -2*rotSpeed * Time.deltaTime));

        //        movingTime -= Time.deltaTime;

        //}
        //else
        //{
        //    if (GameObject.transform.rotation.z >= 0)
        //    {
        //        isMoved = false;

        //    }
        //        transform.Rotate(new Vector3(0, 0, 0.2f * rotSpeed * Time.deltaTime));
        //        movingTime -= Time.deltaTime;
        //}
    }
}
